-- =============================================
-- Author:		Jhefersson Muñoz
-- Create date: 04/11/2016
-- Description:	Reporte de Produccion de costos o resultado de la operacion
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportResultProductionCostsExpenses]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Year INT,
			@MonthStart INT,
			@MonthEnd INT,
			@CodeProductionStart VARCHAR(20),
			@CodeProductionEnd VARCHAR(20)

	DECLARE @Table_Result AS TABLE
	(
		Id INT IDENTITY(1,1),
		Month INT,
		ProductionCenterId INT,
		ProductionCenterCode VARCHAR(20),
		ProductionCenterName VARCHAR(200),
		GeneralExpenses DECIMAL(20,4),
		ManPowerDistribution DECIMAL(20,4),
		FixedAssetDistribution DECIMAL(20,4),
		DispensingDistribution DECIMAL(20,4),
		TransferDistribution DECIMAL(20,4),
		InitialDistribution DECIMAL(20,4),
		AdministrativeValue DECIMAL(20,4),
		LogisticValue DECIMAL(20,4),
		Total DECIMAL(20,4),
		BillingValue DECIMAL(20,4),
		UtilityValue DECIMAL(20,4)
	)

	BEGIN TRY
		
		--Se obtienen los datos de los criterios
		SELECT	@Year = t.x.value('Year[1]','int'),
				@MonthStart = t.x.value('MonthStart[1]','int'),
				@MonthEnd = t.x.value('MonthEnd[1]','int'),
				@CodeProductionStart = t.x.value('CodeProductionStart[1]','varchar(20)'),
				@CodeProductionEnd = t.x.value('CodeProductionEnd[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SELECT	@CodeProductionStart = IIF(@CodeProductionStart = '', NULL, @CodeProductionStart),
				@CodeProductionEnd = IIF(@CodeProductionEnd = '', NULL, @CodeProductionEnd)

		/********************************************  OBTENCION DE DATOS ********************************************/

		INSERT INTO @Table_Result
			SELECT	cen.Month, 
					cpc.Id, cpc.Code ProductionCenterCode, cpc.Name ProductionCenterName,
					cen.DirectCostDistribution + cen.AutoCostDistribution,
					cen.ManPowerDistributionDirect + cen.ManPowerDistributionInDirect,
					cen.FixedAssetDistribution,
					cen.DispensingDistribution,
					cen.TransferDistribution,
					cen.InitialDistribution,
					0,
					0,
					cen.SecondaryDistribution,
					cen.TotalSales,
					cen.TotalSales - cen.SecondaryDistribution
			FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
			JOIN Cost.CostEstimationNative cen WITH (NOLOCK) ON cpc.Id = cen.ProductionCenterId
			WHERE (cen.Year = @Year AND cen.Month >= @MonthStart AND cen.Month <= @MonthEnd)
				AND cpc.Code BETWEEN ISNULL(@CodeProductionStart, '0') AND ISNULL(@CodeProductionEnd, 'ZZZZZZZZZZZZZZZZZZZ')

		/****************************************** DISTRIBUCION SECUNDARIA ******************************************/

		UPDATE tr
			SET tr.AdministrativeValue = cdds.AdministrativeValue,
				tr.LogisticValue = cdds.LogisticValue
		FROM @Table_Result tr
		JOIN
		(
			SELECT	cdds.Month, cdds.ProductionCenterId,
					SUM(IIF(cdds.CenterType = 2, cdds.Value, 0)) AdministrativeValue,
					SUM(IIF(cdds.CenterType = 2, 0, cdds.Value)) LogisticValue
			FROM
			(
						SELECT	cdds.Year, cdds.Month, 
						cds.ProductionCenterId, cpc.CenterType,
						SUM(ISNULL(cddsdr.Value, cddsd.Value)) * -1 Value
				FROM Cost.CostProductionCenter cpc
				JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId
				JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				WHERE cdds.Status = 2 AND (cdds.Year = @Year AND cdds.Month >= @MonthStart AND cdds.Month <= @MonthEnd)
				GROUP BY cdds.Year, cdds.Month, cds.ProductionCenterId, cpc.CenterType
			UNION ALL
				SELECT	cdds.Year, cdds.Month, 
						ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) ProductionCenterId, cpc.CenterType,
						SUM(ISNULL(cddsdr.Value, cddsd.Value)) Value
				FROM Cost.CostProductionCenter cpc
				JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId
				JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				WHERE cdds.Status = 2 AND (cdds.Year = @Year AND cdds.Month >= @MonthStart AND cdds.Month <= @MonthEnd)
				GROUP BY cdds.Year, cdds.Month, ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId), cpc.CenterType
			) cdds
			GROUP BY cdds.Month, cdds.ProductionCenterId
		) cdds ON tr.Month = cdds.Month AND tr.ProductionCenterId = cdds.ProductionCenterId
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

	SELECT	@Year Year, r.Month,
			r.ProductionCenterCode Code, r.ProductionCenterName Name,
			-----------------------------------------------------------------------------------------------------------
			r.GeneralExpenses,
			IIF(r.Total = 0, 0, r.GeneralExpenses / r.Total) GeneralExpensesRate,
			r.ManPowerDistribution,
			IIF(r.Total = 0, 0, r.ManPowerDistribution / r.Total) ManPowerDistributionRate,
			r.FixedAssetDistribution,
			IIF(r.Total = 0, 0, r.FixedAssetDistribution / r.Total) FixedAssetDistributionRate,
			r.DispensingDistribution,
			IIF(r.Total = 0, 0, r.DispensingDistribution / r.Total) DispensingDistributionRate,
			r.TransferDistribution,
			IIF(r.Total = 0, 0, r.TransferDistribution / r.Total) TransferDistributionRate,			
			-----------------------------------------------------------------------------------------------------------
			r.InitialDistribution,
			IIF(r.Total = 0, 0, r.InitialDistribution / r.Total) InitialDistributionRate,
			r.AdministrativeValue,
			IIF(r.Total = 0, 0, r.AdministrativeValue / r.Total) AdministrativeValueRate,
			r.LogisticValue,
			IIF(r.Total = 0, 0, r.LogisticValue / r.Total) LogisticValueRate,
			-----------------------------------------------------------------------------------------------------------
			r.Total,
			r.BillingValue,
			r.UtilityValue,
			IIF(r.BillingValue = 0, 0, r.UtilityValue / r.BillingValue) UtilityValueRate
	FROM @Table_Result r
	WHERE 
	(
		r.GeneralExpenses <> 0
		OR
		r.ManPowerDistribution <> 0
		OR
		r.FixedAssetDistribution <> 0
		OR
		r.DispensingDistribution <> 0
		OR
		r.TransferDistribution <> 0	
		OR
		r.InitialDistribution <> 0
		OR
		r.AdministrativeValue <> 0
		OR
		r.LogisticValue <> 0
		OR
		r.Total <> 0
		OR
		r.BillingValue <> 0
	)
END