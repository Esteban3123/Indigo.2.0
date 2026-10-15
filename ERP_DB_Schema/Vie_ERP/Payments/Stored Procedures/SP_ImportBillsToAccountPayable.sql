-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-02-05
-- Description:	Procedimiento que se encarga de el copyPaste de facturas del form de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ImportBillsToAccountPayable] 
	@XmlObject as Xml,
	@XmlParameters as Xml
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CountFields INT, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- AccountPayable ---
		HeadId INT,
		Term INT,
		BillNumber VARCHAR(100), 
		BillDate DATETIME, 
		Currency VARCHAR(5),
		InvoiceValue NUMERIC(20,4),
		Coments VARCHAR(MAX), 
		BillingCostCenterCode VARCHAR(20), 
		-- AccountPayableDetailConcept --
		AccountPayableConceptCode VARCHAR(20), 
		ThirdPartyNit VARCHAR(20), 
		CostCenterCode VARCHAR(20), 
		Detail VARCHAR(500), 
		Nature TINYINT, 
		[Value] NUMERIC(20,4),
		RetentionConceptCode VARCHAR(20),
		BaseValue NUMERIC(20,4),
		[Percentage] NUMERIC(18,3),
		DeductibleIva TINYINT,
		RateIvaCode VARCHAR(100),
		RateIvaId INT,
		IvaValue DECIMAL(18,2),
		TotalConcept DECIMAL(18,2)
	)
	
	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- AccountPayable ---
		HeadId INT,
		IdAccount INT,
		NumberNameMainAccount VARCHAR(500),
		IdCostCenter INT,
        DescriptionCostCenter VARCHAR(500),
		BillNumber VARCHAR(100), 
		BillDate DATETIME,
		CurrencyAbbreviation VARCHAR(5),
		Term INT,
		ExpirationDate DATETIME,
		Coments VARCHAR(MAX), 
		InvoiceValue NUMERIC(20,4),
		[Value] NUMERIC(20,4),
		Balance NUMERIC(20,4),		
		-- AccountPayableDetailConcept --
		DetailIdConceptAccountPayable INT,
		DetailDescriptionPaymentConcept VARCHAR(500), 
		DetailIdAccount INT,
		DetailNumberNameMainAccount VARCHAR(500),
		DetailIdThirdParty INT,
		DetailDescriptionThirdParty VARCHAR(500),
		DetailIdCostCenter INT,
        DetailDescriptionCostCenter VARCHAR(500),
		DetailNature TINYINT,
		DetailBaseValue NUMERIC(20,4),
		DetailBillingValue NUMERIC(20,4),
		DetailValue NUMERIC(20,4),
		DetailIdRetentionConcept INT,
		DetailDescriptionRetentionConcept VARCHAR(500),
		DetailPercentage NUMERIC(18,3),
		DetailDetail VARCHAR(500),
		DetailDeferredCausation BIT,
		DeductibleIva TINYINT,
		RateIvaId INT,
		IvaValue DECIMAL(18,2),
		TotalConcept DECIMAL(18,2),
		TaxRegistration INT
	)
	
	--Parametros
	DECLARE @SupplierId INT,
			@IdAccount INT,
			@NumberNameMainAccount VARCHAR(500),
			@DeductivleIva tinyint,
			@OfficialCurrencyId INT,
			@TaxRegistration INT

	BEGIN TRY

		SELECT 
			@SupplierId = t.x.value('SupplierId[1]','int'),
			@IdAccount = t.x.value('IdAccount[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		
		SELECT 
			@IdAccount = ma.Id,
			@NumberNameMainAccount = CONCAT(ma.Number, ' - ', ma.Name)
		FROM GeneralLedger.MainAccounts ma
		WHERE ma.Id = @IdAccount
		
		INSERT INTO @TableXmlObject
			(
				CountFields, 
				StatusField, 
				MessageField, 
				DeductibleIva,
				BillNumber,
				BillDate, 
				Currency,
				Term,
				InvoiceValue, 
				Coments, 
				BillingCostCenterCode,
				AccountPayableConceptCode,
				ThirdPartyNit, 
				CostCenterCode, 
				Detail,
				Nature, 
				[Value], 
				RetentionConceptCode, 
				RateIvaCode,
				BaseValue,
				[Percentage], 
			    IvaValue, 
				TotalConcept,
				RateIvaId
			)
			SELECT 
				t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(MAX)') as MessageField,
				-- AccountPayable ---
				IIF(t.x.value('DeductibleIva[1]','varchar(20)') = '', 0, t.x.value('DeductibleIva[1]','varchar(20)')) as DeductibleIva,
				t.x.value('BillNumber[1]','varchar(100)') as BillNumber,
				t.x.value('BillDate[1]','datetime') as BillDate,
				t.x.value('Currency[1]','varchar(5)') as Currency,
				IIF(t.x.value('Term[1]','varchar(20)') = '', 0, t.x.value('Term[1]','varchar(20)')) as Term,
				t.x.value('InvoiceValue[1]','NUMERIC(20,4)') as InvoiceValue,
				t.x.value('Coments[1]','varchar(MAX)') as Coments,
				t.x.value('BillingCostCenterCode[1]','varchar(20)') as BillingCostCenterCode,
				-- AccountPayableDetailConcept --
				t.x.value('AccountPayableConceptCode[1]','varchar(20)') as AccountPayableConceptCode,
				t.x.value('ThirdPartyNit[1]','varchar(20)') as ThirdPartyNit,
				ISNULL(t.x.value('CostCenterCode[1]','varchar(20)'),'') as CostCenterCode,
				t.x.value('Detail[1]','varchar(500)') as Detail,
				IIF(t.x.value('Nature[1]','varchar(20)') = '', 0, t.x.value('Nature[1]','varchar(20)')) as Nature,
				t.x.value('Value[1]','NUMERIC(20,4)') as [Value],
				t.x.value('RetentionConceptCode[1]','varchar(20)') as RetentionConceptCode,
			    t.x.value('RateIva[1]','varchar(20)') as RateIvaCode,
				t.x.value('BaseValue[1]','NUMERIC(20,4)') as BaseValue,
				IIF(t.x.value('Percentage[1]','varchar(20)') = '', 0, t.x.value('Percentage[1]','varchar(20)')) as [Percentage],
				NULL as TotalConcept,
				NULL as IvaValue,
				NULL as RateIvaId
			FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/

		------------------------IVA DESCONTABLE---------------------
		
		--validacion para saber si por medio del proveedor maneja iva descontable
		if EXISTS (select 1 from Common.Supplier s
					join Common.ThirdParty tp on tp.Id = s.IdThirdParty 
					where s.Id = @SupplierId and tp.PersonType =1  and tp.ContributionType=0) BEGIN
					UPDATE  @TableXmlObject SET DeductibleIva = NULL
		end

		--Se saca moneda oficial en caso de que no la digiten
		SELECT @OfficialCurrencyId = OfficialCurrencyId  from GeneralLedger.CompanySettings

		--actualizo los valores del plazo segun el proveedor y moneda
		update TB SET
			Term = s.TimeLimitDays
		FROM @TableXmlObject TB
		JOIN Common.Supplier S ON s.Id = @SupplierId
		
		--Validacion en caso de que el concepto no maneje iva se actualizan los campos  a null y totalconcept queda con el valor base
		IF EXISTS (SELECT 1 FROM @TableXmlObject TB
		JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
		WHERE (APC.HandleTaxes=0 AND APC.HandlesRetention=1) OR (APC.HandleTaxes=0 AND APC.HandlesRetention=0) AND  (tb.DeductibleIva=0 OR tb.DeductibleIva IS NULL)) BEGIN
			UPDATE TB SET 
				TB.RateIvaId=NULL, 
				IvaValue= NULL, 
				Value = TB.BaseValue,
				TotalConcept= TB.BaseValue 
			FROM @TableXmlObject TB
			JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
			WHERE (APC.HandleTaxes=0 AND APC.HandlesRetention=1) OR (APC.HandleTaxes=0 AND APC.HandlesRetention=0) AND (tb.DeductibleIva=0 OR tb.DeductibleIva IS NULL)
		END

		--en caso de que maneje iva por proveedor y concepto se calculan los valores de iva y el total del concepto
		IF EXISTS (SELECT 1 FROM @TableXmlObject TB
		JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
		WHERE APC.HandleTaxes=1 AND APC.HandlesRetention=0 AND tb.DeductibleIva IS NOT NULL) BEGIN
			--se valida que hayan digitado un codigo de tarifa, si no se eliminan estos detalles
			IF EXISTS (SELECT 1 FROM @TableXmlObject TB
						JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
						LEFT JOIN GeneralLedger.GeneralLedgerIVA GLI WITH(NOLOCK) ON GLI.Code = TB.RateIvaCode
						WHERE APC.HandleTaxes=1 AND APC.HandlesRetention=0 AND tb.DeductibleIva IS NOT NULL AND GLI.Id IS NULL) BEGIN
					
				UPDATE TB
					SET TB.StatusField = 0,
						TB.MessageField = 'La factura ' + tb.BillNumber + ' con el concepto de codigo: '+ tb.AccountPayableConceptCode + ' maneja iva y no le ingresó una tarifa valida de IVA'
				FROM @TableXmlObject TB
				JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
				LEFT JOIN GeneralLedger.GeneralLedgerIVA GLI WITH(NOLOCK) ON GLI.Code = TB.RateIvaCode
				WHERE APC.HandleTaxes=1 AND APC.HandlesRetention=0 AND tb.DeductibleIva IS NOT NULL AND GLI.Id IS NULL
			
				--Se elimiman los registros que no cumplen las condiciones
				DELETE TB FROM  @TableXmlObject TB
						JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
					    JOIN GeneralLedger.GeneralLedgerIVA GLI WITH(NOLOCK) ON GLI.Code = TB.RateIvaCode
						WHERE APC.HandleTaxes=1 AND APC.HandlesRetention=0 AND tb.DeductibleIva IS NOT NULL AND GLI.Id IS NULL
			END
			
			--En caso de que si maneje iva, se valida que la tarifa digitada si sea valida
			UPDATE TB SET TB.RateIvaId= GLI.Id, 
					TB.IvaValue= ROUND(((TB.BaseValue*GLI.Percentage)/100),2), 
					TB.Value = IIF(GLI.Percentage IS NULL, TB.BaseValue ,ROUND((TB.BaseValue+((TB.BaseValue*GLI.Percentage)/100)),2)),
					TB.TotalConcept= IIF(GLI.Percentage IS NULL, TB.BaseValue ,ROUND((TB.BaseValue+((TB.BaseValue*GLI.Percentage)/100)),2))
			FROM @TableXmlObject TB
			JOIN Payments.AccountPayableConcepts APC WITH(NOLOCK) ON APC.Code = TB.AccountPayableConceptCode
		    JOIN GeneralLedger.GeneralLedgerIVA GLI WITH(NOLOCK) ON GLI.Code = TB.RateIvaCode
			WHERE APC.HandleTaxes=1 AND APC.HandlesRetention=0 AND tb.DeductibleIva IS NOT NULL
		END
	

		----------------------- AccountPayable -----------------------

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN GeneralLedger.MainAccounts ma ON @IdAccount = ma.Id
			LEFT JOIN Payroll.CostCenter cc ON t.BillingCostCenterCode = cc.Code
			WHERE t.StatusField = 1
				AND (ma.HandlesCostCenter = 1 AND cc.Id IS NULL)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'No existe el centro de costo ' + t.CostCenterCode +  ' de la factura asociado al registro ' + convert(VARCHAR(3),t.Id)
			FROM @TableXmlObject t
			JOIN GeneralLedger.MainAccounts ma ON @IdAccount = ma.Id
			LEFT JOIN Payroll.CostCenter cc ON t.BillingCostCenterCode = cc.Code
			WHERE t.StatusField = 1
				AND (ma.HandlesCostCenter = 1 AND cc.Id IS NULL)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
			WHERE t.StatusField = 1 AND ap.Status <> 3
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' ya existe con el proveedor'
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
			WHERE t.StatusField = 1 AND ap.Status <> 3
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND CAST(t.BillDate AS DATE) > [Common].[GETDATE]()
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La fecha de la factura del registro ' + convert(VARCHAR(3),t.Id) + ' no puede superar la fecha actual'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND CAST(t.BillDate AS DATE) > [Common].[GETDATE]()
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.Term > 0)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El plazo del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser menor a 0'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND t.Term < 0
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.InvoiceValue > 0)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor de la factura del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser igual o menor a 0'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.InvoiceValue > 0)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN @TableXmlObject t2 
				ON t.BillNumber = t2.BillNumber
					AND t.Id < t2.Id
			WHERE t.StatusField = 1
				AND 
				(
					t.BillDate <> t2.BillDate
					OR
					t.Term <> t2.Term
					OR
					t.InvoiceValue <> t2.InvoiceValue
					OR
					t.Coments <> t2.Coments
					OR
					t.BillingCostCenterCode <> t2.BillingCostCenterCode
				)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'Los datos de la factura del registro ' + convert(VARCHAR(3),t.Id) + ' no coinciden'
			FROM @TableXmlObject t
			JOIN @TableXmlObject t2 
				ON t.BillNumber = t2.BillNumber
					AND t.Id < t2.Id
			WHERE t.StatusField = 1
				AND 
				(
					t.BillDate <> t2.BillDate
					OR
					t.Term <> t2.Term
					OR
					t.InvoiceValue <> t2.InvoiceValue
					OR
					t.Coments <> t2.Coments
					OR
					t.BillingCostCenterCode <> t2.BillingCostCenterCode
				)
		END
		
		-- Se consulta el TaxRegistration para la CxP
		SELECT 
			@TaxRegistration = TaxRegistration
		FROM GeneralLedger.CompanySettings

		-- Validamos que el TaxRegistration sea válido
		IF @TaxRegistration IS NULL OR @TaxRegistration = 0 --TaxRegistration inválido
		BEGIN
		UPDATE TB
			SET TB.StatusField = 0,
				TB.MessageField =  'El TaxRegistration es inválido, debe parametrizarse correctamente en Organización y Definición del Tenant' 
			FROM @TableXmlObject TB
		END

		----------------------- AccountPayableDetailConcept -----------------------

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			LEFT JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			WHERE t.StatusField = 1
				AND apc.Id IS NULL
				AND t.AccountPayableConceptCode <> ''
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'No existe el concepto ' + t.AccountPayableConceptCode +  ' asociado al registro ' + convert(VARCHAR(3),t.Id)
			FROM @TableXmlObject t
			LEFT JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			WHERE t.StatusField = 1
				AND apc.Id IS NULL
				AND t.AccountPayableConceptCode <> ''
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			WHERE t.StatusField = 1
				AND apc.ConceptType <> 2
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El concepto ' + t.AccountPayableConceptCode +  ' asociado al registro ' + convert(VARCHAR(3),t.Id) + ' debe ser de tipo general'
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			WHERE t.StatusField = 1
				AND apc.ConceptType <> 2
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			LEFT JOIN Common.ThirdParty tp ON t.ThirdPartyNit = tp.Nit
			WHERE t.StatusField = 1
				AND (ma.HandlesThirdParty = 1 AND tp.Id IS NULL)
				AND t.ThirdPartyNit <> ''
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'No existe el tercero ' + t.ThirdPartyNit +  ' asociado al registro ' + convert(VARCHAR(3),t.Id)
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			LEFT JOIN Common.ThirdParty tp ON t.ThirdPartyNit = tp.Nit
			WHERE t.StatusField = 1
				AND (ma.HandlesThirdParty = 1 AND tp.Id IS NULL)
				AND t.ThirdPartyNit <> ''
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			LEFT JOIN Payroll.CostCenter cc ON t.CostCenterCode = cc.Code
			WHERE t.StatusField = 1
				AND (ma.HandlesCostCenter = 1 AND cc.Id IS NULL)
				AND t.CostCenterCode <> ''
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'No existe el centro de costo ' + t.CostCenterCode +  ' del concepto asociado al registro ' + convert(VARCHAR(3),t.Id)
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			LEFT JOIN Payroll.CostCenter cc ON t.CostCenterCode = cc.Code
			WHERE t.StatusField = 1
				AND (ma.HandlesCostCenter = 1 AND cc.Id IS NULL)
				AND t.CostCenterCode <> ''
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND t.AccountPayableConceptCode <> ''
				AND NOT (t.Nature = 1 OR t.Nature = 2)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La naturaleza del registro ' + convert(VARCHAR(3),t.Id) + ' debe ser 1 si es Debito o 2 si es Credito'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND t.AccountPayableConceptCode <> ''
				AND NOT (t.Nature = 1 OR t.Nature = 2)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND t.AccountPayableConceptCode <> ''
				AND NOT (t.Value > 0)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor del concepto del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser igual o menor a 0'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND t.AccountPayableConceptCode <> ''
				AND NOT (t.Value > 0)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			LEFT JOIN GeneralLedger.RetentionConcepts rc ON t.RetentionConceptCode = rc.Code
			WHERE t.StatusField = 1
				AND t.RetentionConceptCode <> ''
				AND (ma.RetencionType <> 0 AND rc.Id IS NULL)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'No existe el concepto de retencion ' + t.ThirdPartyNit +  ' asociado al registro ' + convert(VARCHAR(3),t.Id)
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			LEFT JOIN GeneralLedger.RetentionConcepts rc ON t.RetentionConceptCode = rc.Code
			WHERE t.StatusField = 1
				AND t.RetentionConceptCode <> ''
				AND (ma.RetencionType <> 0 AND rc.Id IS NULL)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			JOIN GeneralLedger.RetentionConcepts rc ON t.RetentionConceptCode = rc.Code
			WHERE t.StatusField = 1
				AND (ma.RetencionType <> 0 AND NOT (ISNULL(t.BaseValue, 0) > 0))
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor base del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser menor a 0'
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			JOIN GeneralLedger.RetentionConcepts rc ON t.RetentionConceptCode = rc.Code
			WHERE t.StatusField = 1
				AND (ma.RetencionType <> 0 AND NOT (ISNULL(t.BaseValue, 0) > 0))
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			JOIN GeneralLedger.RetentionConcepts rc ON t.RetentionConceptCode = rc.Code
			WHERE t.StatusField = 1
				AND 
				(
					ma.RetencionType <> 0 
					AND 
					t.Value <> ROUND(
						(t.BaseValue * rc.Rate) / 100, 
						CASE rc.TypeRounding
							WHEN 1 THEN 0
							WHEN 2 THEN -1
							WHEN 3 THEN -2
							WHEN 4 THEN -3
							WHEN 5 THEN 1
							WHEN 6 THEN 2
							ELSE 4
						END
					)
				)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor del concepto (' + CAST(CAST(t.Value AS FLOAT) AS VARCHAR(20)) + ') del registro ' + convert(VARCHAR(3),t.Id) + ' no corresponde con el valor calculado de la retencion (' +
						CAST(CAST(
							(
								ROUND(
									(t.BaseValue * rc.Rate) / 100, 
									CASE rc.TypeRounding
										WHEN 1 THEN 0
										WHEN 2 THEN -1
										WHEN 3 THEN -2
										WHEN 4 THEN -3
										WHEN 5 THEN 1
										WHEN 6 THEN 2
										ELSE 4
									END
								)
							) AS FLOAT) AS VARCHAR(20))
					 + ')'
			FROM @TableXmlObject t
			JOIN Payments.AccountPayableConcepts apc ON t.AccountPayableConceptCode = apc.Code
			JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
			JOIN GeneralLedger.RetentionConcepts rc ON t.RetentionConceptCode = rc.Code
			WHERE t.StatusField = 1
				AND 
				(
					ma.RetencionType <> 0 
					AND 
					t.Value <> ROUND(
						(t.BaseValue * rc.Rate) / 100, 
						CASE rc.TypeRounding
							WHEN 1 THEN 0
							WHEN 2 THEN -1
							WHEN 3 THEN -2
							WHEN 4 THEN -3
							WHEN 5 THEN 1
							WHEN 6 THEN 2
							ELSE 4
						END
					)
				)
		END

		/************************************* INSERTAR ERRORES *************************************/

		-- Si existen errores para alguno de los detalles de la factura, la marcamos como erronea
		UPDATE t
			SET t.StatusField = 0,
				t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' tiene un error en uno de sus detalles'
		FROM @TableXmlObject t
		JOIN @TableXmlObject t2 
			ON t.BillNumber = t2.BillNumber 
				AND t2.StatusField = 0
		WHERE t.StatusField = 1
		
		INSERT INTO @TableResult 
			(
				StatusField, MessageField, HeadId, BillNumber
			)
			SELECT StatusField, MessageField, 0, BillNumber
			FROM @TableXmlObject
			WHERE StatusField = 0

		-- Identificamos todas las facturas con su cabecera
		UPDATE t
			SET t.HeadId = t2.Id
		FROM @TableXmlObject t
		JOIN
		(
			SELECT t2.BillNumber, MIN(t2.Id) Id
			FROM @TableXmlObject t2 
			WHERE t2.StatusField = 1
			GROUP BY t2.BillNumber
		) t2 ON t.BillNumber = t2.BillNumber 
		WHERE t.StatusField = 1
		
		/************************************* INSERTAMOS LAS CUENTAS POR PAGAR VALIDAS *************************************/

		INSERT INTO @TableResult
		(
			StatusField, MessageField, 
			-- AccountPayable ---
			HeadId,
			IdAccount,
			NumberNameMainAccount,
			IdCostCenter,
			DescriptionCostCenter,
			BillNumber,
			BillDate,
			Term,
			ExpirationDate,
			Coments,
			InvoiceValue,
			-- AccountPayableDetailConcept --
			DetailIdConceptAccountPayable,
			DetailDescriptionPaymentConcept,
			DetailIdAccount,
			DetailNumberNameMainAccount,
			DetailIdThirdParty,
			DetailDescriptionThirdParty,
			DetailIdCostCenter,
			DetailDescriptionCostCenter,
			DetailNature,
			DetailBaseValue,
			DetailValue,
			DetailIdRetentionConcept,
			DetailDescriptionRetentionConcept,
			DetailPercentage,
			DetailDetail,
			DetailDeferredCausation,
			CurrencyAbbreviation,
			RateIvaId,
			IvaValue,
			DeductibleIva,
			TotalConcept,
			TaxRegistration
		)
		SELECT 1, '', 
			-- AccountPayable ---
			t.HeadId AS HeadId,
			@IdAccount AS IdAccount,
			@NumberNameMainAccount AS NumberNameMainAccount,
			cc.Id AS IdCostCenter, 
			IIF(cc.Id IS NULL, '', CONCAT(cc.Code, ' - ' , cc.Name)) AS DescriptionCostCenter,
			t.BillNumber AS BillNumber,
			t.BillDate AS BillDate,
			t.Term AS Term,
			DATEADD(DAY, t.Term, t.BillDate) ExpirationDate,
			t.Coments Coments,
			t.InvoiceValue InvoiceValue,
			-- AccountPayableDetailConcept --
			d_apc.Id AS DetailIdConceptAccountPayable,
			CONCAT(d_apc.Code, ' - ' , d_apc.Name) AS DetailDescriptionPaymentConcept,
			d_ma.Id AS DetailIdAccount,
			CONCAT(d_ma.Number, ' - ' , d_ma.Name) AS DetailNumberNameMainAccount,
			d_tp.Id AS DetailIdThirdParty,
			IIF(d_tp.Id IS NULL, '', CONCAT(d_tp.Nit, ' - ' , d_tp.Name)) AS DetailDescriptionThirdParty,
			d_cc.Id AS DetailIdCostCenter,
			IIF(d_cc.Id IS NULL, '', CONCAT(d_cc.Code, ' - ' , d_cc.Name)) AS DetailDescriptionCostCenter,
			t.Nature AS DetailNature,
			t.BaseValue AS DetailBaseValue,
			t.Value AS DetailValue,
			d_rc.Id AS DetailIdRetentionConcept,
			IIF(d_rc.Id IS NULL, '', CONCAT(d_rc.Code, ' - ' , d_rc.Name)) AS DetailDescriptionRetentionConcept,
			d_rc.Rate AS DetailPercentage,
			t.Detail AS DetailDetail,
			d_apc.DeferredCausation AS DetailDeferredCausation,
			t.Currency as CurrencyAbbreviation,
			t.RateIvaId,
			t.IvaValue,
			t.DeductibleIva,
			t.TotalConcept,
			@TaxRegistration
		FROM @TableXmlObject t
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = @IdAccount
		LEFT JOIN Payments.AccountPayableConcepts d_apc ON t.AccountPayableConceptCode = d_apc.Code
		LEFT JOIN GeneralLedger.MainAccounts d_ma ON d_apc.IdAccount = d_ma.Id
		LEFT JOIN Payroll.CostCenter cc ON ma.HandlesCostCenter = 1 AND t.BillingCostCenterCode = cc.Code
		LEFT JOIN Common.ThirdParty d_tp ON d_ma.HandlesThirdParty = 1 AND t.ThirdPartyNit = d_tp.Nit
		LEFT JOIN Payroll.CostCenter d_cc ON d_ma.HandlesCostCenter = 1 AND t.CostCenterCode = d_cc.Code
		LEFT JOIN GeneralLedger.RetentionConcepts d_rc ON d_ma.RetencionType <> 0 AND t.RetentionConceptCode = d_rc.Code
		WHERE t.StatusField = 1
		
		/************************************* CALCULAMOS EL VALOR DE LAS FACTURAS *************************************/

		-- Valor facturado del concepto de retención debe ser el de la factura (InvoiceValue), no el valor neto (t2.Value)
		UPDATE t
			SET
				t.Value = t2.Value,
				t.Balance = t2.Value,
				t.DetailBillingValue = IIF(t.DetailIdRetentionConcept IS NULL, 0, t.InvoiceValue)
			FROM @TableResult t
			JOIN
			(
				SELECT t.HeadId, SUM(IIF(t.DetailNature = 1, t.DetailValue, t.DetailValue * -1)) Value
				FROM @TableResult t
				WHERE t.StatusField = 1
				GROUP BY t.HeadId
			) t2 ON t.HeadId = t2.HeadId
			WHERE t.StatusField = 1

	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (StatusField, MessageField)
		VALUES (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * FROM @TableResult
END