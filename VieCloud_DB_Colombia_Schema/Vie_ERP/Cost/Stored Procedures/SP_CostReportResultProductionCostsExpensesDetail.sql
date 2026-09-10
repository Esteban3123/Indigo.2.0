-- =============================================
-- Author:		Jhefersson Muñoz
-- Create date: 04/11/2016
-- Description:	Reporte de Produccion de costos o resultado de la operacion
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportResultProductionCostsExpensesDetail]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year INT,
			@MonthStart INT,
			@MonthEnd INT,
			@DetailType INT,
			@CodeProductionStart VARCHAR(20),
			@CodeProductionEnd VARCHAR(20)

	DECLARE @Table_Result AS TABLE
	(
		Id INT IDENTITY(1,1),
		Month INT,
		CenterType TINYINT,
		ProductionCenterId INT,
		ProductionCenterCode VARCHAR(20),
		ProductionCenterName VARCHAR(200),		
		AccountId INT,
		AccountNumber VARCHAR(50),
		AccountName VARCHAR(200),
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(20),
		ThirdPartyName VARCHAR(500),
		Value DECIMAL(20,4)
	)

	BEGIN TRY
		
		--Se obtienen los datos de los criterios
		SELECT	@Year = t.x.value('Year[1]','int'),
				@MonthStart = t.x.value('MonthStart[1]','int'),
				@MonthEnd = t.x.value('MonthEnd[1]','int'),
				@DetailType = t.x.value('DetailType[1]','int'),
				@CodeProductionStart = t.x.value('CodeProductionStart[1]','varchar(20)'),
				@CodeProductionEnd = t.x.value('CodeProductionEnd[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SELECT	@CodeProductionStart = IIF(@CodeProductionStart = '', NULL, @CodeProductionStart),
				@CodeProductionEnd = IIF(@CodeProductionEnd = '', NULL, @CodeProductionEnd)

		/********************************************  OBTENCION DE DATOS ********************************************/

		INSERT INTO @Table_Result
			SELECT	glb.Month, cpc.CenterType,
					cpc.Id ProductionCenterId, cpc.Code ProductionCenterCode, cpc.Name ProductionCenterName,
					ma.Id AccountId, ma.Number AccountNumber, ma.Name AccountName,
					tp.Id ThirdPartyId, tp.Nit ThirdPartyNit, tp.Name ThirdPartyName,
					SUM((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1)) Value
			FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
			JOIN Cost.CostProductionCenterHomologation cpch WITH (NOLOCK) ON cpc.Id = cpch.ProductionCenterId
			JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
			JOIN GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK) ON cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter				
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
			JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
			LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON glb.IdThirdParty = tp.Id		
			WHERE (glb.Year = @Year AND glb.Month >= @MonthStart AND glb.Month <= @MonthEnd)
				AND cpc.Code BETWEEN ISNULL(@CodeProductionStart, '0') AND ISNULL(@CodeProductionEnd, 'ZZZZZZZZZZZZZZZZZZZ')
				AND cpch.HomologationType = @DetailType
			GROUP BY	glb.Month, cpc.CenterType,
						cpc.Id, cpc.Code, cpc.Name,
						ma.Id, ma.Number, ma.Name,
						tp.Id, tp.Nit, tp.Name
	END TRY
	BEGIN CATCH	
		DELETE FROM @Table_Result

		INSERT INTO @Table_Result 
		(
			ProductionCenterCode, ProductionCenterName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	-------------------------------------------------------------------------------------------------------------------

	SELECT	r.ProductionCenterCode, r.ProductionCenterName,
			r.AccountNumber, r.AccountName,
			r.ThirdPartyNit, r.ThirdPartyName,
			SUM(r.Value) TotalValue
	FROM @Table_Result r	
	GROUP BY	r.ProductionCenterCode, r.ProductionCenterName,
				r.AccountNumber, r.AccountName,
				r.ThirdPartyNit, r.ThirdPartyName
	HAVING SUM(r.Value) <> 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el detalle de costos y gastos de producción por centro de producción, mostrando para cada centro su cuenta contable de origen (homologada según el tipo de detalle solicitado), el tercero asociado y el valor neto del período. Cruza los centros de producción con su homologación contable y los saldos del libro mayor (débitos menos créditos ajustados por la naturaleza de la cuenta) para un año y rango de meses determinados. Permite filtrar por rango de códigos de centro de producción y por tipo de detalle de homologación, facilitando el análisis del resultado operacional y la estructura de costos por área productiva. Se usa en reportes gerenciales y contables de costos hospitalarios o empresariales para identificar qué cuentas y terceros generan el gasto en cada centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera reporte detallado de costos y gastos de producción agrupado por centro de producción, cuenta contable y tercero, calculando saldos netos según la naturaleza contable, dentro de un rango de meses y códigos de centro.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir homologación de centros de producción (CostProductionCenterHomologation) coincidente con el HomologationType recibido.; Los saldos contables deben existir en GeneralLedgerBalance para el año y rango de meses indicados.; El XML de criterios debe incluir Year, MonthStart, MonthEnd y DetailType.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El signo del valor reportado depende exclusivamente de la naturaleza de la clase de cuenta (1=débito positivo, otro=negativo).; Solo se consideran saldos del año exacto solicitado y dentro del rango de meses inclusivo.; Únicamente se incluyen homologaciones cuyo tipo coincide con el DetailType solicitado.; Las filas con valor neto consolidado igual a cero nunca se devuelven al cliente.; Los errores nunca propagan excepción al llamador; se entregan como una fila marcada con código ''999''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Homologación contable; Cuenta contable (PUC); Naturaleza contable (débito/crédito); Saldo libro mayor; Tercero; Costos y gastos de producción', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando glb.Year=@Year, Month entre @MonthStart y @MonthEnd, código del centro entre rango (o todos si es NULL) y HomologationType=@DetailType, se inserta el valor neto SUM((Debit-Credit) * (1 si naturaleza=1 sino -1)) agrupado por mes/centro/cuenta/tercero.; [DELETE] @Table_Result: Si ocurre error en el TRY se vacía la tabla y se inserta una fila con código ''999'' y el mensaje de error junto con la línea.; [INSERT] @Table_Result: En CATCH se inserta una única fila de diagnóstico con ProductionCenterCode=''999'' y ProductionCenterName=ERROR_MESSAGE()+'' - Linea: ''+ERROR_LINE().; [RETURN_RESULT] RESULT: Se retorna el agregado por centro/cuenta/tercero excluyendo filas cuyo SUM(Value)=0 (HAVING SUM(r.Value) <> 0).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mac.Nature = 1 → Multiplica (Debit-Credit) por 1 (cuenta de naturaleza débito conserva signo) else Multiplica (Debit-Credit) por -1 (cuenta de naturaleza crédito invierte signo); si @CodeProductionStart o @CodeProductionEnd vienen vacíos → Se reemplazan por NULL y se aplica rango ''0'' a ''ZZZZZZZZZZZZZZZZZZZ'' (sin filtro efectivo); si SUM(r.Value) = 0 al consolidar → Excluye la fila del resultado final else Incluye la fila en el resultado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpensesDetail';
-- GO
