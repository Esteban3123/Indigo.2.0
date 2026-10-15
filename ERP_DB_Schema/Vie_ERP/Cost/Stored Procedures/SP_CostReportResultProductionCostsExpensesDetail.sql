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