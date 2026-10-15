-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-18
-- Description:	Procedimiento para el reporte de edades de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportActivityCosts]
    @xmlFilters AS XML
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE -- FILTROS --
        @Year INT,
        @Month INT,
        @ProductionCenterStart VARCHAR(MAX),
        @ProductionCenterEnd VARCHAR(MAX),
        ----------------------------------
        @ExistSecondaryDistribution BIT,
        @AverageStandarCostActivity BIT

    DECLARE @Table_Result AS TABLE
    (
        CostProductionCenterCode VARCHAR(20),
        CostProductionCenterName VARCHAR(100), 
        CenterTypeName VARCHAR(100),
        CUPSEntityCode VARCHAR(20), 
        CUPSEntityDescription VARCHAR(300),
        CostActivityCode VARCHAR(20),
        CostActivityDescription VARCHAR(500),
        -----------------------------------------------------------------------
        DirectCostDistribution DECIMAL(18,2),
        AutoCostDistribution DECIMAL(18,2),
        ManPowerDistributionDirect DECIMAL(18,2),
        ManPowerDistributionInDirect DECIMAL(18,2),
        ManPowerDistribution DECIMAL(18,2),
        FixedAssetDistribution DECIMAL(18,2),
        DispensingDistribution DECIMAL(18,2),
        TransferDistribution DECIMAL(18,2),
        Distribution DECIMAL(18,2),
        -----------------------------------------------------------------------
        Quantity INT,
        ActivityUnitValue DECIMAL(18,2),
        CalculatedActivityUnitValue DECIMAL(18,2),
        EstimatedValue DECIMAL(18,2),
        AverageUnitSales DECIMAL(18,2),
        StandarCostValue DECIMAL(18,2),
        StandarCostDeviation DECIMAL(18,2),
        CostDeviationPercentage DECIMAL(18,2),
        ActivityResult DECIMAL(18,2),
        ActivityResultPercentage DECIMAL(18,2)
    )

    BEGIN TRY
        /******************************************** CRITERIOS Y FILTROS ********************************************/

        -- Se obtienen los datos de los filtros
        SELECT 
            @Year = t.x.value('Year[1]','int'),
            @Month = t.x.value('Month[1]','int'),
            @ProductionCenterStart = t.x.value('ProductionCenterStart[1]','varchar(max)'),
            @ProductionCenterEnd = t.x.value('ProductionCenterEnd[1]','varchar(max)')
        FROM @xmlFilters.nodes('/Data') t(x)

        SELECT @ExistSecondaryDistribution = 1
        FROM Cost.CostEstimationNative cen
        WHERE cen.Year = @Year 
            AND cen.Month = @Month
            AND cen.SecondaryDistribution <> 0
        
        -- Obtenemos el valor de costo promedio por actividades de los parametros de costos
        SELECT @AverageStandarCostActivity = cs.AverageStandardCostActivity
        FROM Cost.CostSetting cs WHERE cs.Year = @Year AND cs.Month = @Month

        SELECT @ProductionCenterStart = IIF(@ProductionCenterStart = '', '0', @ProductionCenterStart),
               @ProductionCenterEnd = IIF(@ProductionCenterEnd = '', 'ZZZZZZZZZZ', @ProductionCenterEnd)

        /********************************************  OBTENCION DE DATOS ********************************************/

		;WITH CTE_ResultTemp AS (
			SELECT cpc.Code CostProductionCenterCode, 
                   cpc.Name CostProductionCenterName,
                   CASE cpc.CenterType
                       WHEN 1 THEN 'Operativo'
                       WHEN 2 THEN 'Administrativo'
                       WHEN 3 THEN 'Logístico'
                       ELSE 'N/A'
                   END CenterTypeName,
                   c.Code CUPSEntityCode, c.Description CUPSEntityDescription,    
                   ca.Code CostActivityCode, ca.Description CostActivityDescription,    
                   --------------------------------------------------------------------------------------------------------------------
                   (cen.SecondaryDirectCostDistribution + cen.DirectCostDistribution) DirectCostDistribution, 
                   (cen.SecondaryAutoCostDistribution + cen.AutoCostDistribution) AutoCostDistribution,
                   (cen.SecondaryManPowerDistributionDirect + cen.ManPowerDistributionDirect) ManPowerDistributionDirect, 
                   (cen.SecondaryManPowerDistributionInDirect + cen.ManPowerDistributionInDirect) ManPowerDistributionInDirect,
                   (cen.SecondaryManPowerDistributionDirect + cen.SecondaryManPowerDistributionInDirect + cen.ManPowerDistributionDirect + cen.ManPowerDistributionInDirect) ManPowerDistribution,
                   (cen.SecondaryFixedAssetDistribution + cen.FixedAssetDistribution) FixedAssetDistribution,
                   (cen.SecondaryDispensingDistribution + cen.DispensingDistribution) DispensingDistribution,
                   (cen.SecondaryTransferDistribution + cen.TransferDistribution) TransferDistribution,
                   IIF(@ExistSecondaryDistribution = 1, cen.SecondaryDistribution, cen.InitialDistribution) Distribution,
                   --------------------------------------------------------------------------------------------------------------------
                   cmc.Quantity,
                   cmc.UnitValue ActivityUnitValue,            
                   cmc.EstimatedValue / cmc.Quantity CalculatedActivityUnitValue,
                   cmc.EstimatedValue,
                   cmcts.AverageSales AverageUnitSales,
                   ISNULL(scd.StandarCostValue, 0) StandarCostValue
            FROM Cost.CostEstimationNative cen
            JOIN Cost.CostProductionCenter cpc ON cen.ProductionCenterId = cpc.Id
            JOIN Cost.ClosedMonth cm ON cen.Year = cm.Year AND cen.Month = cm.Month
            LEFT JOIN Cost.ClosedMonthCUPSEntity cmc ON cm.Id = cmc.ClosedMonthId AND cpc.Id = cmc.ProductionCenterId
            LEFT JOIN Contract.CUPSEntity c ON cmc.CUPSEntityId = c.Id
			LEFT JOIN Cost.ClosedMonthCUPSEntityByTotalSales cmcts ON cm.Id = cmcts.ClosedMonthId AND c.Id = cmcts.CUPSEntityId
            LEFT JOIN Cost.ClosedMonthCostActivity cmca ON cm.Id = cmca.ClosedMonthId AND cpc.Id = cmca.CostProductionCenterId AND c.Id = cmca.CUPSEntityId
            LEFT JOIN Cost.CostActivity ca ON cmca.CostActivityId = ca.Id            
            LEFT JOIN Cost.StandarCostDetails scd ON scd.CostActivityId = ca.Id OR scd.CupsId = c.Id
            WHERE cen.Year = @Year AND cen.Month = @Month
                AND cpc.Code BETWEEN COALESCE(NULLIF(@ProductionCenterStart, ''), cpc.Code) 
				AND COALESCE(NULLIF(@ProductionCenterEnd, ''), cpc.Code)
		),
		CTE_CalculateResult AS (
			SELECT 
				*,
				rt.StandarCostValue - rt.CalculatedActivityUnitValue StandarCostDeviation,
				(rt.StandarCostValue - rt.CalculatedActivityUnitValue) * 100 / IIF(rt.StandarCostValue = 0, 1, rt.StandarCostValue) CostDeviationPercentage,
				rt.AverageUnitSales - rt.CalculatedActivityUnitValue ActivityResult,
				(rt.AverageUnitSales - rt.CalculatedActivityUnitValue) * 100 / IIF(rt.AverageUnitSales <= 0, 1, rt.AverageUnitSales) ActivityResultPercentage
			FROM CTE_ResultTemp rt
		)		

        INSERT INTO @Table_Result
			SELECT * FROM CTE_CalculateResult

        /*********************************************  CALCULO UNITARIO *********************************************/

		;WITH CTE_UpdateValuesResult AS (
			SELECT
				tr.CostActivityCode,
				tr.CUPSEntityCode,
				IIF(tr.Distribution = 0 OR tr.Quantity = 0, 0, (tr.EstimatedValue / tr.Distribution) / tr.Quantity) UnitaryCalculation
			FROM @Table_Result tr
		)
        UPDATE tr
            SET tr.DirectCostDistribution = tr.DirectCostDistribution * cte.UnitaryCalculation,
			tr.AutoCostDistribution = tr.AutoCostDistribution * cte.UnitaryCalculation,
			tr.ManPowerDistributionDirect = tr.ManPowerDistributionDirect * cte.UnitaryCalculation,
			tr.ManPowerDistributionInDirect = tr.ManPowerDistributionInDirect * cte.UnitaryCalculation,
			tr.ManPowerDistribution = tr.ManPowerDistribution * cte.UnitaryCalculation,
			tr.FixedAssetDistribution = tr.FixedAssetDistribution * cte.UnitaryCalculation,
			tr.DispensingDistribution = tr.DispensingDistribution * cte.UnitaryCalculation,
			tr.TransferDistribution = tr.TransferDistribution * cte.UnitaryCalculation,
			tr.Distribution = tr.Distribution * cte.UnitaryCalculation
        FROM @Table_Result tr
		JOIN CTE_UpdateValuesResult cte 
		ON cte.CostActivityCode = tr.CostActivityCode AND cte.CUPSEntityCode = tr.CUPSEntityCode
    END TRY

    BEGIN CATCH
        INSERT INTO @Table_Result 
        (
            CostProductionCenterCode, CostProductionCenterName
        )
        SELECT '999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
    END CATCH

    IF @AverageStandarCostActivity <> 1
    BEGIN
        SELECT 
            CostProductionCenterCode, 
            CostProductionCenterName,
            CenterTypeName,
			CUPSEntityCode,
            CUPSEntityDescription,
			CostActivityCode,
            CostActivityDescription,    
            --------------------------------------------------------------------------------------------------------------------
            DirectCostDistribution, 
            AutoCostDistribution,
            ManPowerDistributionDirect, 
            ManPowerDistributionInDirect,
            ManPowerDistribution,
            FixedAssetDistribution,
            DispensingDistribution,
            TransferDistribution,
            Distribution,
            --------------------------------------------------------------------------------------------------------------------
            Quantity,
            ActivityUnitValue,            
            CalculatedActivityUnitValue,
            EstimatedValue,
            AverageUnitSales,
            ActivityResult,
            ActivityResultPercentage
        FROM @Table_Result
        ORDER BY CostProductionCenterCode, CUPSEntityCode, CostActivityCode, ActivityResult
    END
	ELSE
    BEGIN
        SELECT *
        FROM @Table_Result
    END
END