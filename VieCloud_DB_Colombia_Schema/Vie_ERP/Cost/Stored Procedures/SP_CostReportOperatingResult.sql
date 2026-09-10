-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create Modified: 10/04/2017
-- Description:	Stored para el reporte de Structure Organizacional Mayorizado
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportOperatingResult]
	@InitialMonth INT,
	@EndMonth INT,
	@Year INT,
	@CodePCenterIni VARCHAR(50),
    @CodePCenterFin VARCHAR(50),
    @StructureOfCostId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Table_Result AS TABLE
	(
		Id VARCHAR(MAX),
		OrganizationalStructureParentId INT,
		OrganizationalStructureId INT,
		OrganizationalStructureCode VARCHAR(20), 
		OrganizationalStructureName VARCHAR(500),
		OrganizationalStructureLevel TINYINT,
		TotalDistribution DECIMAL(20,4) DEFAULT(0),
		TotalSales DECIMAL(20,4) DEFAULT(0),
		Filter BIT DEFAULT(0)
	)

	BEGIN TRY

		SET @CodePCenterIni = IIF(@CodePCenterIni = '', NULL, @CodePCenterIni)
		SET @CodePCenterFin = IIF(@CodePCenterFin = '', NULL, @CodePCenterFin)
		SET @StructureOfCostId = IIF(@StructureOfCostId = 0, NULL, @StructureOfCostId)

		/********************************* OBTENCION DE LA ESTRUCTURA ORGANIZACIONAL *********************************/

		DECLARE @LevelRows INT = 1,
				@CurrentLevel INT = 0

		WHILE @LevelRows > 0
		BEGIN
			SELECT TOP 1
				@CurrentLevel = Level
			FROM Cost.CostOrganizationalStructureOfCosts
			WHERE Level > @CurrentLevel
			ORDER BY Level

			SET @LevelRows = @@ROWCOUNT
			IF @LevelRows = 0
			BEGIN
				BREAK
			END

			INSERT INTO @Table_Result
			(
				Id, OrganizationalStructureParentId, OrganizationalStructureId, OrganizationalStructureCode, OrganizationalStructureName, OrganizationalStructureLevel
			)
			SELECT CONCAT(tr.Id, '*IND*', cosc.Code), cosc.ParentId, cosc.Id, cosc.Code, cosc.Name, cosc.Level
			FROM Cost.CostOrganizationalStructureOfCosts cosc
			LEFT JOIN @Table_Result tr ON cosc.ParentId = tr.OrganizationalStructureId
			WHERE Level = @CurrentLevel
		END

		/****************************  OBTENCION DE VALORES POR ESTRUCTURA ORGANIZACIONAL ****************************/

		UPDATE tr 
			SET tr.TotalDistribution = cen.TotalDistribution,
				tr.TotalSales = cen.TotalSales,
				tr.Filter = cen.Filter
		FROM @Table_Result tr
		JOIN
		(
			SELECT	cosc.Id OrganizationalStructureId,
					SUM(cen.SecondaryDistribution) AS TotalDistribution,
					SUM(cen.TotalSales) TotalSales,
					MAX
					(	
						IIF(cpc.Code BETWEEN ISNULL(@CodePCenterIni, '0') AND ISNULL(@CodePCenterFin, 'ZZZZZZZZZZZZZZZZZZZ'), IIF(ISNULL(@StructureOfCostId, cosc.Id) = cosc.Id, 1, 0), 0)
					) Filter
			FROM Cost.CostEstimationNative cen WITH (NOLOCK)
			JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON cen.ProductionCenterId = cpc.Id
			JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
			WHERE (cen.Year = @Year AND (cen.Month >= @InitialMonth OR cen.Month <= @EndMonth))
			GROUP BY cosc.Id
		) cen ON tr.OrganizationalStructureId = cen.OrganizationalStructureId

		UPDATE @Table_Result SET Filter = 1 WHERE OrganizationalStructureId = @StructureOfCostId

		/***********************************************  MAYORIZACION ***********************************************/

		DECLARE @LevelMax INT

		SELECT	@LevelRows = 1,
				@CurrentLevel = 0,
				@LevelMax = MAX(OrganizationalStructureLevel) FROM @Table_Result

		WHILE @LevelRows > 0
		BEGIN
			SELECT TOP 1
				@CurrentLevel = OrganizationalStructureLevel
			FROM @Table_Result
			WHERE @LevelMax > OrganizationalStructureLevel
			ORDER BY OrganizationalStructureLevel DESC

			SET @LevelRows = @@ROWCOUNT
			IF @LevelRows = 0 OR @CurrentLevel = @LevelMax
			BEGIN
				BREAK
			END

			UPDATE tr
				SET tr.TotalDistribution += gtr.TotalDistribution,
					tr.TotalSales += gtr.TotalSales
			FROM @Table_Result tr
			JOIN
			(
				SELECT	OrganizationalStructureParentId,
						SUM(TotalDistribution) TotalDistribution,
						SUM(TotalSales) TotalSales
				FROM @Table_Result
				GROUP BY OrganizationalStructureParentId
			) gtr ON tr.OrganizationalStructureId = gtr.OrganizationalStructureParentId
			WHERE tr.OrganizationalStructureLevel = @CurrentLevel

			SET @LevelMax = @CurrentLevel
		END

		/**************************************************  FILTRO **************************************************/

		IF @StructureOfCostId IS NOT NULL OR (@CodePCenterIni IS NOT NULL AND @CodePCenterFin IS NOT NULL)
		BEGIN
			DECLARE @StructureOfCostRows INT = 1,
					@ParentId INT

			WHILE @StructureOfCostRows > 0
			BEGIN
				SELECT TOP 1
					@ParentId = trp.OrganizationalStructureId
				FROM @Table_Result tr
				JOIN @Table_Result trp ON tr.OrganizationalStructureParentId = trp.OrganizationalStructureId
				WHERE tr.Filter = 1 AND trp.Filter = 0

				SET @StructureOfCostRows = @@ROWCOUNT
				IF @StructureOfCostRows = 0
				BEGIN
					BREAK
				END

				UPDATE trp
					SET trp.Filter = 1
				FROM @Table_Result tr
				JOIN @Table_Result trp ON tr.OrganizationalStructureParentId = trp.OrganizationalStructureId
				WHERE tr.Filter = 1 AND trp.Filter = 0
			END

			DELETE @Table_Result WHERE Filter = 0
		END

	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			OrganizationalStructureCode, OrganizationalStructureName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	/*************************************************** RESULTADO ***************************************************/

	SELECT	OrganizationalStructureParentId ParentId,
			OrganizationalStructureId Id,
			OrganizationalStructureCode,
			OrganizationalStructureName,
			OrganizationalStructureLevel Level,
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
	ORDER BY tr.Id
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de Resultado de Operaciones por estructura organizacional de costos, consolidando los valores de distribución secundaria de costos y ventas (facturación) por centro de producción para un rango de meses, año y rango de centros de producción indicados. Recorre jerárquicamente la estructura organizacional de costos (CostOrganizationalStructureOfCosts), acumula (mayoriza) los valores desde los niveles inferiores hacia los superiores, y calcula para cada nodo la diferencia entre ventas y costos totales junto con el margen operacional. Permite filtrar por un nodo específico de la estructura de costos (@StructureOfCostId) o por un rango de códigos de centros de producción (@CodePCenterIni / @CodePCenterFin), devolviendo el árbol jerárquico con costo total, valor de facturación, diferencia y porcentaje de margen para análisis gerencial de rentabilidad operativa.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResult';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResult';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte mayorizado de resultado de operaciones por estructura organizacional de costos, calculando costo total, facturación, diferencia, margen y utilidad para un rango de meses, año y filtros opcionales de centro de producción y estructura.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cost.CostOrganizationalStructureOfCosts debe contener la jerarquía de niveles consistente (ParentId apuntando a Ids existentes y Level secuencial).; Cost.CostProductionCenter debe estar asociado a una estructura organizacional vía OrganizationalStructureOfCostId.; Cost.CostEstimationNative debe contener registros con Year/Month coherentes para el rango solicitado.; El año y los meses inicial/final deben proveerse; cadenas vacías en códigos de centro y 0 en StructureOfCostId se interpretan como ausencia de filtro.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos vacíos de centro de producción y el StructureOfCostId=0 se normalizan a NULL para tratarlos como ''sin filtro''.; La carga de la estructura organizacional se realiza nivel a nivel ascendente, garantizando que cada nodo se enlace mediante ParentId con su jerarquía superior ya cargada.; La mayorización acumula TotalDistribution y TotalSales desde los niveles hijos hacia los niveles padres, recorriendo desde el nivel máximo hasta el mínimo.; Cuando se aplica filtro, todo nodo cuyo descendiente tenga Filter=1 también queda con Filter=1, preservando la rama jerárquica completa hacia la raíz.; Solo se devuelven filas con TotalDistribution distinto de 0 o TotalSales distinto de 0.; Margin y Utility nunca generan división por cero: se controla con IIF cuando el denominador es 0.; El rango de meses se evalúa con OR (Month >= @InitialMonth OR Month <= @EndMonth), permitiendo rangos que cruzan el cierre de año.; El nodo cuyo Id coincide con @StructureOfCostId siempre queda marcado con Filter=1 antes de la propagación.; Los errores no abortan la ejecución: se devuelven como una fila con código ''999''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Centro de producción; Centro de costo; Distribución secundaria de costos; Ventas / Facturación; Margen; Utilidad; Mayorización jerárquica; Resultado de operaciones', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @Table_Result: Devuelve un SELECT final con ParentId, Id, código, nombre, nivel, TotalCost, BillingValue, Diference, Margin y Utility filtrando filas donde TotalDistribution<>0 o TotalSales<>0, ordenado por el Id jerárquico.; [INSERT] @Table_Result: Cuando ocurre un error en el TRY, inserta una fila con OrganizationalStructureCode=''999'' y OrganizationalStructureName=ERROR_MESSAGE()+'' - Linea: ''+ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @StructureOfCostId IS NOT NULL OR (@CodePCenterIni IS NOT NULL AND @CodePCenterFin IS NOT NULL) → Propaga Filter=1 hacia los nodos padres y elimina del resultado las estructuras con Filter=0 else Conserva todas las estructuras sin aplicar filtrado por centro de producción ni por estructura de costos; si TotalDistribution = 0 al calcular Margin → Si TotalSales=0 retorna 0, en caso contrario retorna 1 (evita división por cero) else Calcula Margin = (TotalSales - TotalDistribution) / TotalDistribution; si TotalSales = 0 al calcular Utility → Retorna 0 (evita división por cero) else Calcula Utility = (TotalSales - TotalDistribution) / TotalSales; si Error capturado en BEGIN CATCH → Inserta una fila con código ''999'' y el mensaje + línea del error como nombre de la estructura', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostOrganizationalStructureOfCosts; Cost.CostEstimationNative; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResult';
-- GO
