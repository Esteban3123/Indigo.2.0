CREATE PROCEDURE [Billing].[SP_GetPrepaidPaymentHealth]
	@InvoiceId INT
AS
BEGIN
	/*Declare Variables*/
	DECLARE @AssignedAmount NUMERIC(20, 2) = 0
	DECLARE @TotalPayment NUMERIC(20, 2) = 0
	DECLARE @Invoice as Table (
			Id int,
			DocumentType TINYINT,
			CareGroupId INT,
			InvoiceDate DATETIME
	)
	DECLARE @DetailsTable as Table (
			InvoiceId int,
			CollectionConcept varchar(2),
			SubTotalPatientSalesPrice numeric(20, 2),
			ServiceDate DateTime,
			InvoiceIdCP int
	)

	/*Declare Variable Table*/
	DECLARE @ResponseTable as Table (
			ConceptCollection varchar(2),
			PatientValue Numeric(20, 2),
			DocumentDate DateTime,
			Description varchar(250)
		)
	DECLARE @TableConceptCollection Table (
			InvoiceId INT,
			ConceptCollection VARCHAR(2),
			PatientValue Numeric(20, 2)
		)
	DECLARE @TableTotalPaymentMethods TABLE (
			InvoiceId int,
			PatientValue Numeric(20, 2),
			ThirdPartyValue numeric(20,2),
			DocumentDate DateTime
		)
		/*Validacion si no es fact salud EAPB con/sin contrato*/
		IF NOT EXISTS(
			SELECT 1
			FROM Billing.Invoice WITH(NOLOCK)
			where Id = @InvoiceId
				and DocumentType in (1, 2, 4)
		) 
		BEGIN
			SELECT *
			from @ResponseTable return
		END;

	/* Inserto la informacion de la factura */
	INSERT INTO @Invoice (
			Id,
			DocumentType,
			CareGroupId,
			InvoiceDate
		)
	SELECT i.Id,
		i.DocumentType,
		i.CareGroupId,
		i.InvoiceDate
	FROM Billing.Invoice i WITH(NOLOCK)
	where i.Id = @InvoiceId

	/*Se llenan los detalles de acuerdo al tipo de factura, si se trata de factura capitada u otra. */
	IF EXISTS( 
	SELECT 1 
		FROM @Invoice i
		INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
		WHERE i.DocumentType = 4 
		AND cg.MethodFixedAmountCollectionReport = 1
	) 
	BEGIN /* Facturas Capitadas */
		WITH CTE_InvoiceEntityCapitated as (
			SELECT i.Id,
				COALESCE(iecP.InitialDate, iec.InitialDate) as InitialDate,
				COALESCE(iecP.EndDate, iec.EndDate) as EndDate,
				i.CareGroupId,
				iec.InvoiceCategoryId
			FROM @Invoice i
				join Billing.InvoiceEntityCapitated iec WITH(NOLOCK) on i.Id = iec.InvoiceId
				LEFT JOIN Billing.InvoiceEntityCapitated iecP WITH(NOLOCK) on iec.PreviousRIPSInvoice = iecp.Id
			where (
					iec.InvoicePeriod is null
					OR iec.InvoicePeriod = 2
				)
		)
		INSERT INTO @DetailsTable (
			InvoiceId, --1 
			CollectionConcept, --2 
			SubTotalPatientSalesPrice, --3 
			ServiceDate, --4
			InvoiceIdCP --5
		)
		SELECT
			cte.Id, --1
			CASE
				WHEN id.SubTotalPatientSalesPrice = 0 or id.RecoveryFeeType = 1 THEN '05' --No aplica
				WHEN id.RecoveryFeeType = 2 THEN '02' --Cuota moderadora
				WHEN id.RecoveryFeeType = 3 THEN '01' --Copago
				WHEN id.RecoveryFeeType = 4 THEN '03' --Bono
				ELSE '05'
			END CollectionConcept, --2 
			id.SubTotalPatientSalesPrice, --3 
			id.ServiceDate, --4
			i.Id --5 Número de la factura de registro de servicio o de la monto fijo?
		FROM Billing.InvoiceDetail id
		JOIN Billing.Invoice i on i.Id = id.InvoiceId
		JOIN CTE_InvoiceEntityCapitated cte 
			ON i.CareGroupId = cte.CareGroupId and i.InvoiceCategoryId = cte.InvoiceCategoryId
						and cast(i.InvoiceDate as DATE) BETWEEN cast(cte.InitialDate as DATE) and CAST( cte.EndDate as DATE)
		WHERE i.DocumentType = 5 and i.Status = 1
		
	END

	ELSE BEGIN /* Facturas distintas a capita */
		INSERT INTO @DetailsTable (
			InvoiceId, --1 
			CollectionConcept, --2 
			SubTotalPatientSalesPrice, --3 
			ServiceDate, --4
			InvoiceIdCP --5
		)
		SELECT 
			id.InvoiceId, --1 
			CASE
				WHEN id.SubTotalPatientSalesPrice = 0 or id.RecoveryFeeType = 1 THEN '05' --No aplica
				WHEN id.RecoveryFeeType = 2 THEN '02' --Cuota moderadora
				WHEN id.RecoveryFeeType = 3 THEN '01' --Copago
				WHEN id.RecoveryFeeType = 4 THEN '03' --Bono
				ELSE '05'
			END CollectionConcept, --2 
			id.SubTotalPatientSalesPrice, --3 
			id.ServiceDate, --4
			0 --5
		FROM Billing.InvoiceDetail id WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
		WHERE 
			id.InvoiceId = @InvoiceId
			AND sod.IsDelete = 0
			AND 
			(
				sod.IncludeServiceOrderDetailId IS NULL OR ( sod.IncludeServiceOrderDetailId IS NOT NULL AND sod.recoveryratio IS NOT NULL)
			)
	END


	/* Se totalizan los valores de los conceptos */
	INSERT INTO @TableConceptCollection
	SELECT g.InvoiceId,
		g.CollectionConcept,
		sum(g.PatientValue) PatientValue
	FROM (
			SELECT dt.InvoiceId,
				dt.CollectionConcept,
				sum(dt.SubTotalPatientSalesPrice) PatientValue
			FROM @DetailsTable dt
			GROUP by dt.InvoiceId,
				dt.CollectionConcept
		) g
	GROUP by g.InvoiceId,
		g.CollectionConcept


	/* Si se trata de una factura cápita, retorno la respuesta de una vez. */
	IF EXISTS(
		SELECT 1 
		FROM @Invoice i
		INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
		WHERE i.DocumentType = 4 
		AND cg.MethodFixedAmountCollectionReport = 1
	)
	BEGIN
		INSERT INTO @ResponseTable (ConceptCollection,PatientValue,DocumentDate,Description)
		SELECT	COALESCE(tcc.ConceptCollection,'05') ConceptCollection,
				CASE
					WHEN tcc.ConceptCollection ='05' then 0
					ELSE tcc.PatientValue
				END PatientValue,
				i.InvoiceDate,
				CASE tcc.ConceptCollection
					when '01'	THEN 'Copago'
					when '02'	THEN 'Cuota moderadora'
					when '03'	THEN 'Bono'
					ELSE 'Cuota de recuperación/Copago'
				END Descriptions
		FROM @TableConceptCollection tcc
		LEFT JOIN @Invoice i on i.Id = @InvoiceId
		WHERE tcc.PatientValue > 0

		/* Retornamos respuesta inmediata, ya que lógica siguiente aplica para facturas evento */
		SELECT	ConceptCollection,
				PatientValue,
				DocumentDate,
				Description
		FROM @ResponseTable
		WHERE PatientValue >0
	
		RETURN
	END
		
	/*Busqueda e insercion de la factura de recaudo del paciente */
	INSERT INTO @TableTotalPaymentMethods(InvoiceId,PatientValue,DocumentDate)	 
	SELECT ic.InvoiceId,bb.TotalValue,bb.DocumentDate
	FROM Billing.InvoiceCopay ic WITH(NOLOCK)
	JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.Id
	WHERE ic.InvoiceId = @InvoiceId	

	/*Busqueda e insercion de la factura de recaudo de la entidad, desde liquidacion*/
	INSERT INTO @TableTotalPaymentMethods(InvoiceId,PatientValue, ThirdPartyValue,DocumentDate)	 
	SELECT ipa.InvoiceId, 0 , pa.Value, pa.DocumentDate 
	FROM Billing.InvoicePortfolioAdvance ipa WITH(NOLOCK)
	JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = ipa.PortfolioAdvanceId
	WHERE ipa.InvoiceId = @InvoiceId AND pa.ThirdPartyBeneficiaryId IS NOT NULL

	/*Busqueda e insercion de la factura de recaudo de la entidad, desde factura monto fijo*/
	INSERT INTO @TableTotalPaymentMethods(InvoiceId,PatientValue, ThirdPartyValue,DocumentDate)	 
	SELECT ipa.InvoiceId, 0 , pa.Value, pa.DocumentDate 
	FROM Billing.InvoicePortfolioAdvance ipa WITH(NOLOCK)
	JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = ipa.PortfolioAdvanceId
	JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
	JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
	JOIN Billing.Invoice i ON i.Id = ipa.InvoiceId
	WHERE ipa.InvoiceId = @InvoiceId AND crc.IsFixedAmountInvoiceAdvance = 1 AND crd.ThirdPartyBeneficiaryId IS NULL AND i.DocumentType = 4

	/*Almaceno en memoria el valor total del pago*/
	SELECT @TotalPayment = SUM(PatientValue) 
	FROM @TableTotalPaymentMethods 
	WHERE InvoiceId = @InvoiceId AND PatientValue > 0

	/*Inserto segun los conceptos de recaudo*/
	INSERT INTO @ResponseTable (ConceptCollection,PatientValue,DocumentDate,Description)
	SELECT	COALESCE(tmpc.ConceptCollection,'05') ConceptCollection,
			CASE
				WHEN tmpc.ConceptCollection ='05' then 0
				ELSE tmpc.PatientValue
			END PatientValue,
			tmpp.DocumentDate,
			CASE tmpc.ConceptCollection
				when '01'	THEN 'Copago'
				when '02'	THEN 'Cuota moderadora'
				when '03'	THEN 'Bono'
				ELSE 'Cuota de recuperación/Copago'
			END Descriptions
	FROM @TableTotalPaymentMethods tmpp
	LEFT join @TableConceptCollection tmpc  on tmpc.InvoiceId =tmpp.InvoiceId
	WHERE tmpp.PatientValue > 0

	UNION ALL

	SELECT  '04'
			,tmpp.ThirdPartyValue
			,tmpp.DocumentDate
			,'Cuota de Anticipo' AS Descriptions
	FROM @TableTotalPaymentMethods tmpp
	WHERE tmpp.ThirdPartyValue > 0

	-- Calcular el monto total ya asignado
	SELECT @AssignedAmount = SUM(PatientValue) FROM @ResponseTable WHERE ConceptCollection <> '05'

	-- Calcular el valor restante para "No Aplica"
	IF (@TotalPayment > COALESCE(@AssignedAmount,0)) BEGIN
		UPDATE @ResponseTable
		SET PatientValue = @TotalPayment - COALESCE(@AssignedAmount,0)
		WHERE ConceptCollection = '05'
	END

	SELECT	ConceptCollection,
			PatientValue,
			DocumentDate,
			Description
	FROM @ResponseTable
	WHERE PatientValue >0

	RETURN
	
END