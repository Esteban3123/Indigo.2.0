
-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 2019-11-20
-- Description:	Reporte de estado de flujo de efectivo
-- =============================================
CREATE PROCEDURE [Treasury].[SP_CashFlowStatus]
	-- Add the parameters for the stored procedure here
	@Parameters as xml
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @InitialDate as dateTIME
	DECLARE @EndDate as dateTIME
	DECLARE @CashFlowConceptType as varchar(max)
	DECLARE @Activity as varchar(max)
	DECLARE @CashFlowConcept as varchar(max)
	DECLARE @Opcion as tinyint
	DECLARE @DocumentType as tinyint
	DECLARE @DocumentCode AS VARCHAR(30)
	DECLARE @Id as int
    DECLARE @InitialDateComp as dateTIME
	DECLARE @EndDateComp as dateTIME

	select 
	@InitialDate = t.x.value('InitialDate[1]','datetime'),
	@EndDate = t.x.value('EndDate[1]','datetime'),
	@CashFlowConceptType = t.x.value('CashFlowConceptType[1]','varchar(max)'),
	@Activity = t.x.value('Activity[1]','varchar(max)'),
	@CashFlowConcept = t.x.value('CashFlowConcept[1]','varchar(max)'),
	@DocumentType = t.x.value('DocumentType[1]','tinyint'),
	@DocumentCode = t.x.value('DocumentCode[1]','varchar(30)'),
	@Opcion = t.x.value('Opcion[1]','tinyint'),
	@Id = t.x.value('Id[1]','int'),
	@InitialDateComp = t.x.value('InitialDateComp[1]','datetime'),
	@EndDateComp = t.x.value('EndDateComp[1]','datetime')
	from @Parameters.nodes('/Parameters') t(x)

	if @Opcion is null set @Opcion = 0

	if @Opcion = 3
	begin
		declare @PreviousInitDate DATETIME
		DECLARE @ParametersAux xml
		declare @PreviousEndDate DATETIME
		if (select id from Treasury.TreasuryBalancePeriod where periodo = DATEADD(SECOND, -1 ,@InitialDate)) is null
		BEGIN
			SELECT @PreviousInitDate = Max(Periodo) From Treasury.TreasuryBalancePeriod WHERE Periodo < @InitialDate
			if @PreviousInitDate is NULL
			BEGIN
				SELECT @PreviousInitDate = Min(DocumentDate) From Treasury.TreasuryBalance
			END
			ELSE
			BEGIN
				set @PreviousInitDate = DATEADD(SECOND, 1, @PreviousInitDate)
			END
			set @PreviousEndDate = DATEADD(SECOND, -1, @InitialDate)
			if @PreviousInitDate is not null and @PreviousEndDate is not null AND @PreviousInitDate < @PreviousEndDate 
			BEGIN
				set @ParametersAux = N'<Parameters><InitialDate>' + CONVERT(VARCHAR(19), @PreviousInitDate, 20) +'</InitialDate><EndDate>'+ CONVERT(VARCHAR(19), @PreviousEndDate, 20) +'</EndDate><Opcion>3</Opcion></Parameters>'
				EXEC  [Treasury].[SP_CashFlowStatus] @PARAMETERS = @ParametersAux
			END
		ENd
		if @InitialDateComp IS NOT NULL AND (select id from Treasury.TreasuryBalancePeriod where periodo = DATEADD(SECOND, -1, @InitialDateComp)) is null
		BEGIN
			SELECT @PreviousInitDate = Max(Periodo) From Treasury.TreasuryBalancePeriod WHERE Periodo < @InitialDateComp
			if @PreviousInitDate is NULL
			BEGIN
				SELECT @PreviousInitDate = Min(DocumentDate) From Treasury.TreasuryBalance
			END
			ELSE
			BEGIN
				SET @PreviousInitDate = DATEADD(SECOND, 1, @PreviousInitDate)
			END
			set @PreviousEndDate = DATEADD(SECOND, -1, @InitialDateComp)
			if @PreviousInitDate is not null and @PreviousEndDate is not null AND @PreviousInitDate < @PreviousEndDate 
			BEGIN
				set @ParametersAux = N'<Parameters><InitialDate>' + CONVERT(VARCHAR(19), @PreviousInitDate,20) +'</InitialDate><EndDate>'+ CONVERT(VARCHAR(20), @PreviousEndDate, 20) +'</EndDate><Opcion>3</Opcion></Parameters>'
				EXEC  [Treasury].[SP_CashFlowStatus] @PARAMETERS = @ParametersAux
			END
			
		ENd
	end

	if @Opcion IN (0,1,3)
	begin

		DECLARE @TABLA AS TABLE
		(IdResource INT NULL
		,IdConceptoFlujoEfectivo int NULL
		,IdTipo tinyint NULL
		,IdActividad tinyint NULL
		,IdTipoDocumento tinyint NULL
		,Codigo varchar(20)
		,Fecha DATETIME
		,IdTercero int NULL
		,ValorCredito numeric(20,4)
		,ValorDebito numeric(20,4)
		,ConceptoFlujoEfectivo varchar(100) NULL
		,Tipo varchar(7) NULL
		,Actividad varchar(12) NULL
		,Documento varchar(30) NULL
		,NitTercero varchar(20) NULL
		,Tercero varchar(300) NULL
		,Detalle varchar(max) NULL
		,IdTB INT NULL
		,ReversedDate DateTime NULL
		)

		--Recibos de caja
		insert into @TABLA(IdResource, IdConceptoFlujoEfectivo, IdTipo, IdActividad, IdTipoDocumento, Codigo, Fecha, IdTercero, ValorCredito, ValorDebito, IdTB, ReversedDate)
		select 
		CR.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, TB.DocumentType
		, CR.Code
		, TB.DocumentDate
		, CR.IdThirdParty
		, Sum(CRD.Value)
		, 0
		, TB.Id
		, CASE WHEN @Opcion = 3 THEN CR.ReversedDate ELSE NULL END
		from Treasury.TreasuryBalance TB WITH(NOLOCK)
		INNER JOIN Treasury.CashReceipts CR WITH(NOLOCK)
		ON CR.Code = TB.DocumentNumber AND TB.DocumentType = 1
		INNER JOIN Treasury.CashReceiptDetails CRD WITH(NOLOCK)
		ON CRD.IdCashReceipt = CR.Id
		AND CRD.Nature = 2
		LEFT JOIN Treasury.CashFlowConcept CFC WITH(NOLOCK)
		ON CFC.ID = CRD.IdCashFlowConcept
		WHERE 
		(@DocumentType IS NULL OR @DocumentType = 1)
		AND (@DocumentCode IS NULL OR CR.CODE = @DocumentCode)
		AND ( (TB.DocumentDate BETWEEN cast( @InitialDate AS Date)AND cast (@EndDate AS Date)AND (@Opcion = 3 OR CR.ReversedDate IS NULL OR CR.ReversedDate > cast(@EndDate as date))) 
			OR 
			  (@InitialDateComp IS NOT NULL AND TB.DocumentDate BETWEEN @InitialDateComp AND @EndDateComp AND (@Opcion = 3 OR CR.ReversedDate IS NULL OR CR.ReversedDate > @EndDateComp))
			)
		AND ( CRD.IdCashFlowConcept IS NULL OR @CashFlowConceptType IS NULL OR CFC.TypeConcept IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@CashFlowConceptType, ',')))
		AND ( CRD.IdCashFlowConcept IS NULL OR @Activity IS NULL OR CFC.Activity IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@Activity, ',')))
		AND ( CRD.IdCashFlowConcept IS NULL OR @CashFlowConcept IS NULL OR CRD.IdCashFlowConcept IN (SELECT convert(INT, rtrim(Data)) FROM dbo.Split(@CashFlowConcept, ',')))
		group by  
		CR.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, TB.DocumentType
		, CR.Code
		, TB.DocumentDate
		, CR.IdThirdParty
		, TB.Id
		, CASE WHEN @Opcion = 3 THEN CR.ReversedDate ELSE NULL END

		UPDATE T SET Detalle = CR.Detail
		FROM @TABLA T
		INNER JOIN Treasury.CashReceipts CR WITH(NOLOCK)
		ON CR.Id = T.IdResource AND T.IdTipoDocumento = 1

		--comprobantes de egreso
		insert into @TABLA(IdResource, IdConceptoFlujoEfectivo, IdTipo, IdActividad, IdTipoDocumento, Codigo, Fecha, IdTercero, ValorCredito, ValorDebito, IdTB, ReversedDate)
		select 
		VT.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, TB.DocumentType
		, VT.Code
		, TB.DocumentDate
		, VT.IdThirdParty
		, Sum(CASE WHEN VTD.NATURE = 2 THEN VTD.Value ELSE 0 END)
		, Sum(CASE WHEN VTD.NATURE = 1 THEN VTD.Value ELSE 0 END)
		, TB.Id
		, CASE WHEN @OPCION = 3 THEN VT.ReversedDate ELSE NULL END
		from Treasury.TreasuryBalance TB WITH(NOLOCK)
		inner join Treasury.VoucherTransaction VT WITH(NOLOCK)
		ON VT.Code = TB.DocumentNumber AND VT.VoucherClass = 1 --PAGOS, Si se quita voucherclass se debe agregar id de caja o id de banco para que no duplique los valores
		AND TB.DocumentType = 2
		INNER JOIN Treasury.VoucherTransactionDetails VTD WITH(NOLOCK)
		ON VTD.IdVoucherTransaction = VT.ID
		LEFT JOIN Treasury.CashFlowConcept CFC WITH(NOLOCK)
		ON CFC.ID = VTD.IdCashFlowConcept
		WHERE 
		(@DocumentType IS NULL OR @DocumentType = 2)
		AND (@DocumentCode IS NULL OR VT.CODE = @DocumentCode)
		AND ( (TB.DocumentDate BETWEEN cast (@InitialDate as date)AND cast (@EndDate as date)AND (@Opcion = 3 OR VT.ReversedDate IS NULL OR VT.ReversedDate > cast (@EndDate as date))) 
			OR 
			  (@InitialDateComp IS NOT NULL AND TB.DocumentDate BETWEEN @InitialDateComp AND @EndDateComp AND (@Opcion = 3 OR VT.ReversedDate IS NULL OR VT.ReversedDate > @EndDateComp))
			)
		AND ( VTD.IdCashFlowConcept IS NULL OR @CashFlowConceptType IS NULL OR CFC.TypeConcept IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@CashFlowConceptType, ',')))
		AND ( VTD.IdCashFlowConcept IS NULL OR @Activity IS NULL OR CFC.Activity IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@Activity, ',')))
		AND ( VTD.IdCashFlowConcept IS NULL OR @CashFlowConcept IS NULL OR VTD.IdCashFlowConcept IN (SELECT convert(INT, rtrim(Data)) FROM dbo.Split(@CashFlowConcept, ',')))
		GROUP BY
		VT.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, TB.DocumentType
		, VT.Code
		, TB.DocumentDate
		, VT.IdThirdParty
		, TB.Id
		, CASE WHEN @Opcion = 3 THEN VT.ReversedDate ELSE NULL END

		UPDATE T SET Detalle = VT.Detail
		FROM @TABLA T
		INNER JOIN Treasury.VoucherTransaction VT WITH(NOLOCK)
		ON VT.Id = T.IdResource AND T.IdTipoDocumento = 2

		--notas
		insert into @TABLA(IdResource, IdConceptoFlujoEfectivo, IdTipo, IdActividad, IdTipoDocumento, Codigo, Fecha, IdTercero, ValorCredito, ValorDebito, IdTB)
		select 
		TN.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, TB.DocumentType
		, TN.Code
		, TB.DocumentDate
		, NULL
		, Sum(CASE WHEN TND.NATURE = 2 THEN TND.Value ELSE 0 END)
		, Sum(CASE WHEN TND.NATURE = 1 THEN TND.Value ELSE 0 END)
		, TB.Id
		from Treasury.TreasuryBalance TB WITH(NOLOCK)
		inner join Treasury.TreasuryNote TN WITH(NOLOCK)
		ON TN.Code = TB.DocumentNumber  
		AND TB.DocumentType = 4
		INNER JOIN Treasury.TreasuryNoteDetail TND WITH(NOLOCK)
		ON TND.TreasuryNoteId = TN.ID
		LEFT JOIN Treasury.CashFlowConcept CFC WITH(NOLOCK)
		ON CFC.ID = TND.IdCashFlowConcept
		WHERE 
		(@DocumentType IS NULL OR @DocumentType = 4)
		AND (@DocumentCode IS NULL OR TN.CODE = @DocumentCode)
		AND ( (TB.DocumentDate BETWEEN cast (@InitialDate as date)AND cast(@EndDate as date)) 
			OR 
			  (@InitialDateComp IS NOT NULL AND TB.DocumentDate BETWEEN @InitialDateComp AND @EndDateComp)
			)
		AND ( TND.IdCashFlowConcept IS NULL OR @CashFlowConceptType IS NULL OR CFC.TypeConcept IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@CashFlowConceptType, ',')))
		AND ( TND.IdCashFlowConcept IS NULL OR @Activity IS NULL OR CFC.Activity IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@Activity, ',')))
		AND ( TND.IdCashFlowConcept IS NULL OR @CashFlowConcept IS NULL OR TND.IdCashFlowConcept IN (SELECT convert(INT, rtrim(Data)) FROM dbo.Split(@CashFlowConcept, ',')))
		GROUP BY
		TN.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, TB.DocumentType
		, TN.Code
		, TB.DocumentDate
		, TB.Id

		UPDATE T SET Detalle = TN.Description
		FROM @TABLA T
		INNER JOIN Treasury.TreasuryNote TN WITH(NOLOCK)
		ON TN.Id = T.IdResource AND T.IdTipoDocumento = 4

		--cruce de cuentas -  CXC
		insert into @TABLA(IdResource, IdConceptoFlujoEfectivo, IdTipo, IdActividad, IdTipoDocumento, Codigo, Fecha, IdTercero, ValorCredito, ValorDebito, ReversedDate )
		select 
		CAS.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, 5
		, CAS.Code
		, CAS.Fecha
		, CAS.ThirdPartyId
		, Sum(CASE WHEN CAS.Nature = 2 THEN CAS.CrossingValue ELSE 0 END)
		, Sum(CASE WHEN CAS.Nature = 1 THEN CAS.CrossingValue ELSE 0 END)
		, CASE WHEN @Opcion = 3 THEN CAS.ReversedDate ELSE NULL END
		FROM
		(SELECT CA.ID, CA.CODE, CA.ThirdPartyId
		--, Fecha = COALESCE(CA.ConfirmationDate, CA.ModificationDate, CA.CreationDate)
		,Fecha = CA.ConfirmationDate
		, Nature = 2, CXC.CrossingValue, CXC.IdCashFlowConcept, CA.ReversedDate
		from Treasury.CrossingAccount CA WITH(NOLOCK)
		INNER JOIN Treasury.CrossingAccountDetailCxC CXC WITH(NOLOCK)
		ON CXC.CrossingAccountId = CA.ID
		WHERE 
		(@DocumentType IS NULL OR @DocumentType = 5)
		AND (@DocumentCode IS NULL OR CA.CODE = @DocumentCode)
		AND CA.Status IN (2,4)
		AND ( (CA.ConfirmationDate BETWEEN cast(@InitialDate as date)AND cast(@EndDate as date)AND (@Opcion = 3 OR CA.ReversedDate IS NULL OR CA.ReversedDate > cast(@EndDate as date))) 
			OR 
			  (@InitialDateComp IS NOT NULL AND CA.ConfirmationDate BETWEEN @InitialDateComp AND @EndDateComp AND (@Opcion = 3 OR CA.ReversedDate IS NULL OR CA.ReversedDate > @EndDateComp))
			)
		UNION ALL
		SELECT CA.ID, CA.CODE, CA.ThirdPartyId
		--, Fecha = COALESCE(CA.ConfirmationDate, CA.ModificationDate, CA.CreationDate)
		,Fecha = CA.ConfirmationDate
		, Nature = 1, CXP.CrossingValue, CXP.IdCashFlowConcept, CA.ReversedDate
		from Treasury.CrossingAccount CA WITH(NOLOCK)
		INNER JOIN Treasury.CrossingAccountDetailCxP CXP WITH(NOLOCK)
		ON CXP.CrossingAccountId = CA.ID
		WHERE 
		(@DocumentType IS NULL OR @DocumentType = 5)
		AND (@DocumentCode IS NULL OR CA.CODE = @DocumentCode)
		AND CA.Status IN (2,4)
		AND ( (CA.ConfirmationDate BETWEEN cast(@InitialDate as date) AND cast(@EndDate as date)AND (@Opcion = 3 OR CA.ReversedDate IS NULL OR CA.ReversedDate > cast(@EndDate as date))) 
			OR 
			  (@InitialDateComp IS NOT NULL AND CA.ConfirmationDate BETWEEN @InitialDateComp AND @EndDateComp AND (@Opcion = 3 OR CA.ReversedDate IS NULL OR CA.ReversedDate > @EndDateComp))
			)
		UNION ALL
		SELECT CA.ID, CA.CODE, CA.ThirdPartyId
		--, Fecha = COALESCE(CA.ConfirmationDate, CA.ModificationDate, CA.CreationDate)
		, Fecha = CA.ConfirmationDate
		, OC.Nature, CrossingValue = OC.Value, OC.IdCashFlowConcept, CA.ReversedDate
		from Treasury.CrossingAccount CA WITH(NOLOCK)
		INNER JOIN Treasury.CrossingAccountDetailOtherConcept OC WITH(NOLOCK)
		ON OC.CrossingAccountId = CA.ID
		WHERE 
		(@DocumentType IS NULL OR @DocumentType = 5)
		AND (@DocumentCode IS NULL OR CA.CODE = @DocumentCode)
		AND CA.Status IN (2,4)
		AND ( (CA.ConfirmationDate BETWEEN cast(@InitialDate as date)AND cast(@EndDate as date)AND (@Opcion = 3 OR CA.ReversedDate IS NULL OR CA.ReversedDate > cast(@EndDate as date))) 
			OR 
			  (@InitialDateComp IS NOT NULL AND CA.ConfirmationDate BETWEEN @InitialDateComp AND @EndDateComp AND (@Opcion = 3 OR CA.ReversedDate IS NULL OR CA.ReversedDate > @EndDateComp))
			)
		) CAS
		LEFT JOIN Treasury.CashFlowConcept CFC WITH(NOLOCK)
		ON CFC.ID = CAS.IdCashFlowConcept
		WHERE 
		( CAS.IdCashFlowConcept IS NULL OR @CashFlowConceptType IS NULL OR CFC.TypeConcept IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@CashFlowConceptType, ',')))
		AND ( CAS.IdCashFlowConcept IS NULL OR @Activity IS NULL OR CFC.Activity IN (SELECT convert(TINYINT, rtrim(Data)) FROM dbo.Split(@Activity, ',')))
		AND ( CAS.IdCashFlowConcept IS NULL OR @CashFlowConcept IS NULL OR CAS.IdCashFlowConcept IN (SELECT convert(INT, rtrim(Data)) FROM dbo.Split(@CashFlowConcept, ',')))
		GROUP BY
		CAS.Id
		,CFC.Id
		, CFC.TypeConcept
		, CFC.Activity
		, CAS.Code
		, CAS.Fecha
		, CAS.ThirdPartyId
		, CASE WHEN @Opcion = 3 THEN CAS.ReversedDate ELSE NULL END

		UPDATE T SET Detalle = CA.Description
		FROM @TABLA T
		INNER JOIN Treasury.CrossingAccount CA WITH(NOLOCK)
		ON CA.Id = T.IdResource AND T.IdTipoDocumento = 5

		IF @Opcion = 3
		BEGIN
			insert into @TABLA(IdTipoDocumento, Codigo, Fecha, ValorCredito, ValorDebito)
			select 
			TB.DocumentType
			, TB.DocumentNumber
			, TB.DocumentDate
			, Sum(CASE WHEN TB.Nature = 2 THEN TB.ValueMovement ELSE 0 END)
			, Sum(CASE WHEN TB.Nature = 1 THEN TB.ValueMovement ELSE 0 END)
			from Treasury.TreasuryBalance TB WITH(NOLOCK)
			LEFT OUTER JOIN @TABLA T ON T.IdTB = TB.Id
			WHERE 
			((TB.DocumentDate BETWEEN cast(@InitialDate as date)AND cast(@EndDate as date)) OR (@InitialDateComp IS NOT NULL AND TB.DocumentDate BETWEEN @InitialDateComp AND @EndDateComp))
			AND T.IdTB IS NULL
			group by  
			TB.DocumentType
			, TB.DocumentNumber
			, TB.DocumentDate
		END

		UPDATE T SET ConceptoFlujoEfectivo = CASE WHEN T.IdConceptoFlujoEfectivo IS NULL THEN 'Otro' ELSE '' END, Tipo = CASE T.IdTipo WHEN 1 THEN 'Ingreso' WHEN 2 THEN 'Egreso' ELSE 'Otro' END
		, Actividad = CASE T.IdActividad WHEN 1 THEN 'Inversión' WHEN 2 THEN 'Operación' WHEN 3 THEN 'Financiación' ELSE 'Otra' END
		, Documento = 
			CASE T.IdTipoDocumento 
			WHEN 1 THEN 'Recibo de caja'
			WHEN 2 THEN 'Comprobante de egreso'
			WHEN 3 THEN 'Consignación' --no se consultan
			WHEN 4 THEN 'Nota'
			WHEN 5 THEN 'Cruce de cuentas'
			ELSE 'Otro' END
		FROM @TABLA T

		UPDATE T SET ConceptoFlujoEfectivo = CFC.NameConcept
		from @TABLA T
		INNER JOIN Treasury.CashFlowConcept CFC  WITH(NOLOCK)
		ON CFC.ID = T.IdConceptoFlujoEfectivo

		UPDATE T SET NitTercero = TP.Nit, Tercero = TP.Name
		from @TABLA T
		INNER JOIN Common.ThirdParty TP  WITH(NOLOCK)
		ON TP.ID = T.IdTercero
		
		IF @Opcion = 0 --reporte flujo de efectivo en tesoreria
		BEGIN
			SELECT 
			Documento
			,Codigo
			,Fecha = Convert(varchar(19),Fecha,20)
			,NitTercero
			,Tercero
			,Detalle
			,ValorCredito
			,ValorDebito
			,ConceptoFlujoEfectivo
			,Actividad
			,Tipo
			FROM @TABLA
		END

		IF @Opcion = 1 --listar documentos en reclasificacion de conceptos de flujo de efectivo
		BEGIN
			Select  
			Seleccionado = cast(0 as bit) 
			,IdResource
			,Documento
			,Codigo
			,NitTercero
			,Tercero	
			,Detalle
			,ValorCredito
			,ValorDebito
			,PreviousCfeId = isnull(IdConceptoFlujoEfectivo, 0)
			,CurrentCfeId = 0
			,PreviousCfeName = ConceptoFlujoEfectivo
			,CurrentCfeName = ''
			,Tipo
			,Actividad 
			,IdTipo
			,IdActividad
			from @TABLA 
		END

		if @Opcion = 3 --Reporte flujo de efectivo desde el modulo de contabilidad
		BEGIN
			DECLARE @TABLA1 AS TABLE
			(TipoRegistro TINYINT
			,Actividad varchar(12)
			,ConceptoFlujoEfectivo varchar(100) NULL
			,PeriodoValor NUMERIC(20,4)
			,ComparativoValor NUMERIC(20,4)
			)

			--movimientos del periodo
			insert into @TABLA1(TipoRegistro, Actividad, ConceptoFlujoEfectivo, PeriodoValor, ComparativoValor)
			SELECT 
			TipoRegistro = Cast(1 as tinyint)
			,T.Actividad
			,T.ConceptoFlujoEfectivo
			,PeriodoValor = SUM(CASE WHEN T.Fecha BETWEEN cast(@InitialDate as date) AND cast(@EndDate as date) AND T.ReversedDate IS NOT NULL AND T.ReversedDate BETWEEN cast(@InitialDate as date) AND cast(@EndDate as date)THEN 0 
									WHEN T.ReversedDate IS NOT NULL AND T.ReversedDate BETWEEN cast(@InitialDate as date) AND cast(@EndDate as date) THEN T.ValorDebito - T.ValorCredito
									WHEN T.Fecha BETWEEN cast(@InitialDate as date) AND cast(@EndDate as date) THEN T.ValorCredito - T.ValorDebito
									ELSE 0
								END)
			,ComparativoValor =  SUM(CASE WHEN @InitialDateComp IS NULL THEN 0
									WHEN T.Fecha BETWEEN @InitialDateComp AND @EndDateComp AND T.ReversedDate IS NOT NULL AND T.ReversedDate BETWEEN @InitialDateComp AND @EndDateComp THEN 0 
									WHEN T.ReversedDate IS NOT NULL AND T.ReversedDate BETWEEN @InitialDateComp AND @EndDateComp THEN T.ValorDebito - T.ValorCredito
									WHEN T.Fecha BETWEEN @InitialDateComp AND @EndDateComp THEN T.ValorCredito - T.ValorDebito
									ELSE 0
								END)
			FROM @TABLA T
			GROUP BY
			T.Actividad
			,T.ConceptoFlujoEfectivo
			
			
			--SALDOS INICIALES periodo anterior
			insert into @TABLA1(TipoRegistro, Actividad, ConceptoFlujoEfectivo, PeriodoValor, ComparativoValor)
			SELECT 
			TipoRegistro = Cast(0 as TINYINT)
			,Actividad = ''
			,ConceptoFlujoEfectivo = ''
			,PeriodoValor = isnull((SELECT BALANCE FROM Treasury.TreasuryBalancePeriod where Periodo = DATEADD(SECOND, -1, cast(@InitialDate as date))),0)
			,ComparativoValor = CASE WHEN @InitialDateComp IS NULL THEN 0 ELSE  isnull((SELECT BALANCE FROM Treasury.TreasuryBalancePeriod where Periodo = DATEADD(SECOND, -1, @InitialDateComp)),0) END
		
			if (SELECT ID FROM Treasury.TreasuryBalancePeriod where Periodo = @EndDate) is NULL
			BEGIN
				INSERT INTO Treasury.TreasuryBalancePeriod (Periodo, Balance)
				select @EndDate, sum(PeriodoValor) FROM @TABLA1 
			END

			if @EndDateComp is NOT null AND (SELECT ID FROM Treasury.TreasuryBalancePeriod where Periodo = @EndDateComp) is NULL
			BEGIN
				INSERT INTO Treasury.TreasuryBalancePeriod (Periodo, Balance)
				select @EndDateComp, sum(ComparativoValor) FROM @TABLA1 
			END

			select TipoRegistro, Actividad, ConceptoFlujoEfectivo, PeriodoValor, ComparativoValor from @TABLA1
		
		END

		

	END
	
	IF @Opcion = 2 --consultar detalles de una reclasificacion de conceptos de flujjo de efectivo
	BEGIN
		Select  
		Seleccionado = cast(0 as bit) 
		--,DocumentId = CFRD.DocumentId
		,Documento = CASE CFR.DocumentType 
						WHEN 1 THEN 'Recibo de caja'
						WHEN 2 THEN 'Comprobante de egreso'
						WHEN 3 THEN 'Consignación' --no se consultan
						WHEN 4 THEN 'Nota'
						WHEN 5 THEN 'Cruce de cuentas'
						ELSE 'Otro' END
		,Codigo = CASE CFR.DocumentType 
						WHEN 1 THEN CR.Code
						WHEN 2 THEN VT.Code
						WHEN 3 THEN '' --no se consultan
						WHEN 4 THEN TN.Code
						WHEN 5 THEN CA.Code
						ELSE '' END
		,NitTercero = TP.Nit	
		,Tercero = TP.Name	
		,Detalle = CASE CFR.DocumentType 
						WHEN 1 THEN CR.DETAIL
						WHEN 2 THEN VT.DETAIL
						WHEN 3 THEN '' --no se consultan
						WHEN 4 THEN TN.Description
						WHEN 5 THEN CA.Description
						ELSE '' END
		,ValorCredito = CFRD.CreditValue
		,ValorDebito = CFRD.DebitValue
		,PreviousCfeId = isnull(CFRD.PreviousCashFlowConcept, 0)
		,CurrentCfeId = isnull(CFRD.CurrentCashFlowConcept, 0)
		,PreviousCfeName = CASE WHEN CFC.ID IS NULL THEN '' ELSE CFC.Code + '-' + CFC.NameConcept END
		,CurrentCfeName = CASE WHEN CCFC.ID IS NULL THEN '' ELSE CCFC.Code + '-' + CCFC.NameConcept END
		,Tipo = CASE CFC.TypeConcept WHEN 1 THEN 'Ingreso' WHEN 2 THEN 'Egreso' ELSE 'Otro' END
		,Actividad = CASE CFC.Activity WHEN 1 THEN 'Inversión' WHEN 2 THEN 'Operación' WHEN 3 THEN 'Financiación' ELSE 'Otra' END
		from 
		Treasury.CashFlowReclassification CFR WITH(NOLOCK)
		INNER JOIN Treasury.CashFlowReclassificationDetail CFRD  WITH(NOLOCK)
		ON CFRD.IdCashFlowReclassification = CFR.Id
		LEFT OUTER JOIN Treasury.CashFlowConcept CFC WITH(NOLOCK)
		ON CFC.Id = CFRD.PreviousCashFlowConcept
		LEFT OUTER JOIN Treasury.CashFlowConcept CCFC WITH(NOLOCK)
		ON CCFC.Id = CFRD.CurrentCashFlowConcept
		LEFT OUTER JOIN Treasury.CashReceipts CR WITH(NOLOCK)
		ON CR.ID = CFRD.DocumentId AND CFR.DocumentType = 1
		LEFT OUTER JOIN Treasury.VoucherTransaction VT WITH(NOLOCK)
		ON VT.ID = CFRD.DocumentId AND CFR.DocumentType = 2
		LEFT OUTER JOIN Treasury.TreasuryNote TN WITH(NOLOCK)
		ON TN.ID = CFRD.DocumentId AND CFR.DocumentType = 4
		LEFT OUTER JOIN Treasury.CrossingAccount CA WITH(NOLOCK)
		ON CA.Id = CFRD.DocumentId AND CFR.DocumentType = 5
		LEFT OUTER JOIN Common.ThirdParty TP  WITH(NOLOCK)
		ON TP.Id = CASE CFR.DocumentType 
					WHEN 1 THEN CR.IdThirdParty
				    WHEN 2 THEN VT.IdThirdParty
					WHEN 5 THEN CA.ThirdPartyId
					ELSE NULL
					END
		
		WHERE CFR.ID = @Id
	END
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de Estado de Flujo de Efectivo de Tesorería para un rango de fechas, clasificando los movimientos de caja (ingresos y egresos) según los conceptos y actividades del flujo de efectivo (operación, inversión o financiación). Consolida información de recibos de caja, detalles de cobros y saldos de tesorería por período, permitiendo comparar dos períodos distintos y calcular saldos acumulados tomando como base los cierres de período registrados en TreasuryBalancePeriod. Es utilizado para reportería financiera y contable que muestra cómo se generó y utilizó el efectivo en la organización durante un período determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_CashFlowStatus';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_CashFlowStatus';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Flujo de efectivo (cash flow); Recibo de caja; Comprobante de egreso; Nota de tesorería; Cruce de cuentas (CxC/CxP); Concepto de flujo de efectivo (Ingreso/Egreso/Otro); Actividad (Inversión/Operación/Financiación); Saldo de periodo de tesorería; Reclasificación de conceptos de flujo de efectivo; Tercero (NIT, nombre); Reverso de documento (ReversedDate); Reporte comparativo entre periodos', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Opcion IS NULL → Se asigna @Opcion = 0 (modo reporte de flujo de efectivo en tesorería por defecto); si @Opcion = 3 y no existe periodo previo registrado en Treasury.TreasuryBalancePeriod para (@InitialDate - 1 segundo) → Se calcula recursivamente el periodo anterior (desde el último Periodo < @InitialDate o el Min(DocumentDate) de TreasuryBalance hasta @InitialDate-1seg) y se invoca recursivamente SP_CashFlowStatus con Opcion=3 para generar el saldo histórico; si @Opcion = 3 y @InitialDateComp NOT NULL y no existe TreasuryBalancePeriod para (@InitialDateComp - 1 segundo) → Se ejecuta recursivamente el mismo cálculo de saldo previo para el periodo comparativo; si @Opcion IN (0,1,3) → Se construye la tabla agregada @TABLA con recibos de caja (DocumentType=1), comprobantes de egreso (VoucherClass=1, DocumentType=2), notas (DocumentType=4) y cruces de cuenta (DocumentType=5, Status IN (2,4)); si @Opcion = 3 → Adicionalmente inserta en @TABLA los movimientos de TreasuryBalance no asociados a ningún documento ya cargado (T.IdTB IS NULL); si @Opcion = 0 → Retorna el detalle de movimientos de @TABLA como reporte de flujo de efectivo en tesorería; si @Opcion = 1 → Retorna los documentos de @TABLA con campos para reclasificación de conceptos de flujo de efectivo (Seleccionado, PreviousCfeId, CurrentCfeId); si @Opcion = 3 → Calcula @TABLA1 con movimientos del periodo y comparativo, agrega saldo inicial desde TreasuryBalancePeriod del periodo anterior, persiste el saldo de cierre en TreasuryBalancePeriod si no existe, y retorna el reporte para el módulo de contabilidad else No se realiza el cálculo de saldos ni la persistencia; si @Opcion = 3 y no existe registro en TreasuryBalancePeriod con Periodo=@EndDate → INSERT en TreasuryBalancePeriod con (Periodo=@EndDate, Balance=SUM(PeriodoValor) de @TABLA1); si @Opcion = 3 y @EndDateComp NOT NULL y no existe registro en TreasuryBalancePeriod con Periodo=@EndDateComp → INSERT en TreasuryBalancePeriod con (Periodo=@EndDateComp, Balance=SUM(ComparativoValor) de @TABLA1); si @Opcion = 2 → Retorna el detalle de una reclasificación específica (CashFlowReclassification.ID = @Id) uniendo el documento original según DocumentType (1=Recibo, 2=Egreso, 4=Nota, 5=Cruce); si Para cálculo de PeriodoValor/ComparativoValor en @Opcion=3: la fecha de reverso (ReversedDate) cae dentro del rango → El movimiento se invierte (ValorDebito - ValorCredito); si tanto Fecha como ReversedDate están en el rango, el neto es 0; si solo Fecha está en el rango, suma ValorCredito - ValorDebito', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Treasury.SP_CashFlowStatus; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.TreasuryBalancePeriod; Treasury.TreasuryBalance; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashFlowConcept; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.TreasuryNote; Treasury.TreasuryNoteDetail; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxC; Treasury.CrossingAccountDetailCxP; Treasury.CrossingAccountDetailOtherConcept; Common.ThirdParty; Treasury.CashFlowReclassification; Treasury.CashFlowReclassificationDetail', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowStatus';
-- GO
