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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de resultados de producción de costos y gastos por centro de producción para un período determinado (año, rango de meses y rango de centros de producción). Consolida los costos directos, mano de obra, activos fijos, dispensación, transferencias y distribución inicial obtenidos desde la estimación nativa de costos, y los complementa con los valores administrativos y logísticos provenientes de la distribución secundaria de costos (redistribución entre centros). El resultado final presenta, por mes y centro de producción, cada componente de costo con su valor absoluto y su participación porcentual sobre el total, junto con el valor facturado y la utilidad operacional (ventas menos costos), sirviendo como base para análisis de rentabilidad y control de gestión financiera por unidad productiva.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de producción de costos o resultado de la operación, consolidando por mes y centro de producción los costos directos, distribuciones (mano de obra, activos, dispensación, traslados, inicial, administrativa, logística), ventas y utilidad con sus tasas.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener Year, MonthStart, MonthEnd, CodeProductionStart y CodeProductionEnd; Deben existir datos en Cost.CostEstimationNative para el año y rango de meses indicados; Para que la distribución secundaria se considere, los registros en Cost.CostDirectDistributionSecondary deben tener Status = 2', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'GeneralExpenses se calcula como DirectCostDistribution + AutoCostDistribution; ManPowerDistribution se calcula como ManPowerDistributionDirect + ManPowerDistributionInDirect; Total corresponde a SecondaryDistribution de Cost.CostEstimationNative; BillingValue corresponde a TotalSales y UtilityValue = TotalSales - SecondaryDistribution; Sólo se consideran distribuciones secundarias con Status = 2; Los valores del centro origen en la distribución secundaria se restan (multiplicados por -1) y los del centro destino redistribuido se suman; No se devuelven filas con todos los valores numéricos en cero (excepto UtilityValue) para depurar el reporte; Errores no propagan excepción al cliente; se materializan como una fila con código ''999''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Distribución secundaria de costos; Redistribución de costos; Mano de obra directa e indirecta; Activos fijos; Dispensación; Traslados; Costos administrativos; Costos logísticos; Ventas / Facturación; Utilidad; Resultado de la operación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Inserta una fila por centro de producción y mes con datos de Cost.CostEstimationNative cuando cen.Year=@Year, cen.Month entre @MonthStart y @MonthEnd, y cpc.Code está entre @CodeProductionStart (o ''0'' si null) y @CodeProductionEnd (o ''ZZZZZZZZZZZZZZZZZZZ'' si null); [UPDATE] @Table_Result: Actualiza AdministrativeValue cuando cpc.CenterType = 2 y LogisticValue cuando CenterType <> 2, sumando los valores de la distribución secundaria (negativos para el centro origen, positivos para el centro destino redistribuido); [DELETE] @Table_Result: Si ocurre cualquier excepción dentro del TRY, se borra el contenido acumulado para limpiar resultados parciales; [INSERT] @Table_Result: En CATCH inserta una fila con ProductionCenterCode=''999'' y ProductionCenterName con ERROR_MESSAGE() + línea, como mecanismo de reporte de error; [RETURN_RESULT] RESULT: Devuelve el reporte sólo de filas donde al menos uno de los valores (GeneralExpenses, ManPowerDistribution, FixedAssetDistribution, DispensingDistribution, TransferDistribution, InitialDistribution, AdministrativeValue, LogisticValue, Total, BillingValue) sea distinto de cero, calculando tasas como valor/Total y UtilityValueRate como UtilityValue/BillingValue (0 si el divisor es 0)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodeProductionStart o @CodeProductionEnd vienen como cadena vacía → Se convierten a NULL y luego se reemplazan por ''0'' y ''ZZZZZZZZZZZZZZZZZZZ'' respectivamente para no filtrar el rango; si cpc.CenterType = 2 en la distribución secundaria → El valor se acumula como AdministrativeValue else El valor se acumula como LogisticValue; si r.Total = 0 al calcular tasas → Las tasas (GeneralExpensesRate, ManPowerDistributionRate, etc.) se devuelven como 0 para evitar división por cero; si r.BillingValue = 0 → UtilityValueRate se devuelve como 0 else UtilityValueRate = UtilityValue / BillingValue; si Se produce un error en el TRY → Se vacía el resultado y se inserta una fila de error con código ''999'' y el mensaje + línea', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostEstimationNative; Cost.CostDistributionSecondary; Cost.CostDirectDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostDirectDistributionSecondaryDetailRedistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportResultProductionCostsExpenses';
-- GO
