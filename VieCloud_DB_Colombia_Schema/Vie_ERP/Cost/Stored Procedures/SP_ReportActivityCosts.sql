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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de costos por actividad (servicio CUPS) para un período y rango de centros de producción seleccionados. Consolida la estimación nativa de costos del mes cerrado —incluyendo distribución de costos directos, mano de obra directa e indirecta, activos fijos, dispensación y transferencias— junto con las cantidades ejecutadas, el valor unitario de cada actividad y las ventas promedio, para calcular el costo estándar, la desviación y el resultado económico por actividad. Sirve para el análisis de rentabilidad y control de gestión de costos hospitalarios, comparando lo estimado versus lo facturado por servicio CUPS en cada centro de producción del período cerrado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportActivityCosts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportActivityCosts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de costos por actividad y CUPS para un mes cerrado, consolidando distribuciones primarias y secundarias, calculando valores unitarios, desviaciones frente al costo estándar y resultado frente al precio promedio de venta.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de filtros debe contener nodo /Data con Year y Month válidos; Debe existir configuración en Cost.CostSetting para el año y mes solicitados (de lo contrario @AverageStandarCostActivity quedará nulo y caerá en la rama distinta de 1); Debe existir información en Cost.CostEstimationNative para el período consultado; Si se proveen códigos de centro de producción, deben respetar el orden alfabético del rango Start <= End', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los cálculos siempre se acotan al año y mes recibidos en los filtros; Si no se reciben rangos de centro de producción, se usa ''0'' a ''ZZZZZZZZZZ'' como rango por defecto; Las distribuciones reportadas suman siempre la porción primaria más la secundaria; La columna Distribution refleja secundaria solo si existen distribuciones secundarias en el período; en caso contrario refleja la inicial; Las divisiones por StandarCostValue y AverageUnitSales nunca producen división por cero (se sustituye divisor por 1); Los valores de distribución se reescalan a magnitud unitaria multiplicándolos por (EstimatedValue/Distribution)/Quantity; Cuando el costo promedio por actividad no está habilitado (AverageStandardCostActivity ≠ 1), se ocultan StandarCostValue, StandarCostDeviation y CostDeviationPercentage; Cualquier excepción no aborta el procedimiento: se materializa como fila de error con código ''999''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción / centro de costo; Tipo de centro (Operativo, Administrativo, Logístico); CUPS (Clasificación Única de Procedimientos en Salud); Actividad de costo; Distribución primaria e inicial de costos; Distribución secundaria de costos; Mano de obra directa e indirecta; Activos fijos, dispensación, traslados; Costo estándar y desviación de costo; Promedio de venta unitaria; Resultado de actividad y porcentaje de resultado; Mes contable cerrado; Parámetros de costo (CostSetting)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_RESULT: Cuando AverageStandardCostActivity de CostSetting ≠ 1, se retorna el conjunto sin columnas de costo estándar y ordenado por CostProductionCenterCode, CUPSEntityCode, CostActivityCode, ActivityResult; [RETURN_RESULT] RETURN_RESULT: Cuando AverageStandardCostActivity = 1, se retorna la tabla completa de resultados incluyendo costo estándar, desviación y porcentaje de desviación; [INSERT] @Table_Result: Inserta una fila por combinación centro/CUPS/actividad del período con distribuciones consolidadas (primaria+secundaria), cantidades, valor unitario, valor estimado, venta promedio y costo estándar (0 si no existe); [UPDATE] @Table_Result: Multiplica todas las distribuciones (DirectCost, AutoCost, ManPower directa/indirecta/total, FixedAsset, Dispensing, Transfer, Distribution) por el factor UnitaryCalculation = (EstimatedValue/Distribution)/Quantity, o por 0 si Distribution o Quantity son cero; [INSERT] @Table_Result: En caso de error en el TRY, inserta fila con CostProductionCenterCode=''999'' y CostProductionCenterName=ERROR_MESSAGE()+'' - Linea: ''+ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en CostEstimationNative algún registro del año/mes con SecondaryDistribution distinto de cero → Se toma SecondaryDistribution como Distribution else Se toma InitialDistribution como Distribution; si Distribution = 0 o Quantity = 0 al calcular factor unitario → El factor UnitaryCalculation se fija en 0 (anula las distribuciones unitarias) else Se calcula UnitaryCalculation = (EstimatedValue / Distribution) / Quantity; si StandarCostValue = 0 al calcular el porcentaje de desviación → Se usa 1 como divisor para evitar división por cero; si AverageUnitSales <= 0 al calcular el porcentaje de resultado de actividad → Se usa 1 como divisor para evitar división por cero o negativos; si Parámetro AverageStandardCostActivity de CostSetting es distinto de 1 → Se devuelve un resultado proyectado sin las columnas de costo estándar (StandarCostValue, StandarCostDeviation, CostDeviationPercentage), ordenado por centro, CUPS, actividad y resultado else Se devuelve la tabla completa incluyendo costos estándar y desviaciones; si CenterType = 1 / 2 / 3 / otro → Se etiqueta como ''Operativo'' / ''Administrativo'' / ''Logístico'' / ''N/A'' respectivamente; si Ocurre cualquier error en el bloque TRY → Se inserta una fila con código ''999'' y el mensaje de error junto a la línea como nombre del centro', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostSetting; Cost.CostProductionCenter; Cost.ClosedMonth; Cost.ClosedMonthCUPSEntity; Contract.CUPSEntity; Cost.ClosedMonthCUPSEntityByTotalSales; Cost.ClosedMonthCostActivity; Cost.CostActivity; Cost.StandarCostDetails', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportActivityCosts';
-- GO
