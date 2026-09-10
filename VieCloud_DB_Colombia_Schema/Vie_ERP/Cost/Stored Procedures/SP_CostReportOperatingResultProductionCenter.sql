-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 06/05/2017
-- Description:	Store para el reporte de resultado de operaciones por centro de produccion
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportOperatingResultProductionCenter]
	@InitialMonth int,
	@EndMonth int,
	@Year int,
	@CodePCenterIni varchar(50),
    @CodePCenterFin varchar(50),
	@OrderBy int
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Table_Result AS TABLE
	(
		ProductionCenterCode VARCHAR(20), 
		ProductionCenterName VARCHAR(500),
		TotalDistribution DECIMAL(20,4) DEFAULT(0),
		TotalSales DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		SET @CodePCenterIni = IIF(@CodePCenterIni = '', NULL, @CodePCenterIni)
		SET @CodePCenterFin = IIF(@CodePCenterFin = '', NULL, @CodePCenterFin)

		/**********************************  OBTENCION DE LA ESTIMACIONES DE COSTOS **********************************/

		INSERT INTO @Table_Result
		(
			ProductionCenterCode, ProductionCenterName, TotalDistribution, TotalSales
		)
		SELECT cpc.Code,cpc.Name, SUM(cen.SecondaryDistribution) AS TotalDistribution, SUM(cen.TotalSales) TotalSales
		FROM Cost.CostEstimationNative cen WITH (NOLOCK)
		JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON cen.ProductionCenterId = cpc.Id
		WHERE (cen.Year = @Year AND (cen.Month >= @InitialMonth OR cen.Month <= @EndMonth))
			AND cpc.Code BETWEEN ISNULL(@CodePCenterIni, '0') AND ISNULL(@CodePCenterFin, 'ZZZZZZZZZZZZZZZZZZZ')
		GROUP BY cpc.Code, cpc.Name

	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			ProductionCenterCode, ProductionCenterName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	/*************************************************** RESULTADO ***************************************************/

	SELECT	CASE @OrderBy
				WHEN 1 THEN tr.ProductionCenter
				WHEN 2 THEN CAST(tr.TotalCost AS VARCHAR(500))
				WHEN 3 THEN CAST(tr.BillingValue AS VARCHAR(500))
				WHEN 4 THEN CAST(tr.Diference AS VARCHAR(500))
				WHEN 5 THEN CAST(tr.Margin AS VARCHAR(500))
				WHEN 6 THEN CAST(tr.Utility AS VARCHAR(500))
			END OrderBy,
			*
	FROM
	(
		SELECT	CONCAT(tr.ProductionCenterCode, ' - ', tr.ProductionCenterName) ProductionCenter,
				TotalDistribution TotalCost,
				TotalSales BillingValue,
				TotalSales - TotalDistribution Diference,
				IIF
				(
					TotalDistribution = 0, 
					IIF(TotalSales = 0, 0, 1), 
					(TotalSales - TotalDistribution) / TotalDistribution
				) Margin,
				IIF
				(
					TotalSales = 0, 
					0, 
					(TotalSales - TotalDistribution) / TotalSales
				) Utility
		FROM @Table_Result tr
		WHERE TotalDistribution <> 0 OR TotalSales <> 0
	) tr
	ORDER BY 1
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de resultado operativo por centro de producción: genera el estado de resultados de cada centro de producción para un rango de meses y año indicados, consultando la estimación nativa de costos (Cost.CostEstimationNative) y cruzándola con el catálogo de centros de producción (Cost.CostProductionCenter). Para cada centro calcula el costo total distribuido (distribución secundaria), el valor facturado o ventas, la diferencia entre ambos, el margen sobre costos y la utilidad sobre ventas. Permite filtrar por rango de códigos de centro de producción y ordenar los resultados por cualquiera de las columnas calculadas; es utilizado en reportería gerencial y de contabilidad de costos para evaluar la rentabilidad operativa por unidad de negocio.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Generar un reporte gerencial de resultado operativo por centro de producción, comparando el costo distribuido contra las ventas y calculando diferencia, margen y utilidad para un rango de meses, año y centros.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El año debe corresponder a registros existentes en la estimación de costos; Los códigos de centro de producción deben existir en el catálogo de centros; Los rangos de centros vacíos ('''') se interpretan como sin filtro inferior/superior', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Centros con TotalDistribution = 0 y TotalSales = 0 se excluyen del resultado final; Cuando no hay costo distribuido pero sí ventas, el margen se fuerza a 1 (100%) para evitar división por cero; Cuando no hay ventas, la utilidad se fuerza a 0 para evitar división por cero; Los rangos de código de centro vacíos se tratan como sin límite (''0'' a ''ZZZZZZZZZZZZZZZZZZZ''); Los errores en tiempo de ejecución no abortan el reporte: se devuelven como una fila con código ''999''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Estimación de costos; Distribución secundaria de costos; Ventas / Facturación; Margen; Utilidad; Resultado operativo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve por centro de producción: ProductionCenter, TotalCost (SecondaryDistribution), BillingValue (TotalSales), Diference, Margin y Utility, ordenados por la columna seleccionada en @OrderBy; [RETURN_RESULT] (resultset): Si ocurre una excepción al consultar las estimaciones, devuelve una sola fila con código ''999'' y mensaje de error + línea en lugar de los datos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TotalDistribution = 0 y TotalSales = 0 → Margin = 0 else Si TotalDistribution = 0 y TotalSales <> 0, Margin = 1; en otro caso Margin = (TotalSales - TotalDistribution) / TotalDistribution; si TotalSales = 0 → Utility = 0 else Utility = (TotalSales - TotalDistribution) / TotalSales; si Valor de @OrderBy (1..6) → Se proyecta como columna OrderBy el campo correspondiente: 1=ProductionCenter, 2=TotalCost, 3=BillingValue, 4=Diference, 5=Margin, 6=Utility; si Error en la obtención de estimaciones (CATCH) → Se inserta una fila con código ''999'' y el mensaje de error junto con la línea como nombre del centro', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultProductionCenter';
-- GO
