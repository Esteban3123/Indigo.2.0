
CREATE PROCEDURE [Maintenance].[SP_ReportMaintenanceRepairs]
		@xmlCriterias AS XML,
		@xmlFilters AS XML				
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE 	
			
			-- CRITERIOS --									
			@DateStart Date,
			@DateEnd Date,		

			--FILTROS--			
			@State TINYINT,
			@PhysicalAssetId VARCHAR(MAX),
			@FixedAssetItemId VARCHAR(MAX),
			@MaintenanceResponsibleId VARCHAR(MAX),
			@FixedAssetItemCatalogId VARCHAR(MAX),
			@Quantity INT,
			---------------------------------------------------------------------------------------
			 
			@FilterByPhysicalAssetId BIT = 0,
			@FilterByFixedAssetItemId BIT = 0,
			@FilterByMaintenanceResponsibleId BIT = 0,
			@FilterByFixedAssetItemCatalogId BIT = 0,
			@FilterQuantity INT = 0,
			@FilterState TINYINT = 0
	
	DECLARE @Table_PhysicalAssetId AS TABLE(Id INT)
	DECLARE @Table_FixedAssetItemId AS TABLE(Id INT)
	DECLARE @Table_MaintenanceResponsibleId AS TABLE(Id INT)
	DECLARE @Table_FixedAssetItemCatalogId AS TABLE(Id INT)
	DECLARE @Table_State AS TABLE(Id INT)
	DECLARE @Table_Quantity AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date')
				FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@State = t.x.value('State[1]','int'),
				---------------------------------------------------------------------------------------
				@PhysicalAssetId = t.x.value('PhysicalAssetId [1]','varchar(max)'),
				@FixedAssetItemId = t.x.value('FixedAssetItemId[1]','varchar(max)'),
				@MaintenanceResponsibleId = t.x.value('MaintenanceResponsibleId[1]','varchar(max)'),
				@FixedAssetItemCatalogId = t.x.value('FixedAssetItemCatalogId[1]','varchar(max)'),
				@Quantity = t.x.value('Quantity[1]','int')
				FROM @xmlFilters.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------
		IF ISNULL(@PhysicalAssetId, '') <> ''
		BEGIN
			SET @FilterByPhysicalAssetId = 1		
			INSERT INTO @Table_PhysicalAssetId
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@PhysicalAssetId, ',')
		END

		IF ISNULL(@FixedAssetItemId, '') <> ''
		BEGIN
			SET @FilterByFixedAssetItemId = 1

			INSERT INTO @Table_FixedAssetItemId
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@FixedAssetItemId, ',')
		END

		IF ISNULL(@MaintenanceResponsibleId, '') <> ''
		BEGIN
			SET @FilterByMaintenanceResponsibleId = 1

			INSERT INTO @Table_MaintenanceResponsibleId
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@MaintenanceResponsibleId, ',')
		END

		IF ISNULL(@FixedAssetItemCatalogId, '') <> ''
		BEGIN
			SET @FilterByFixedAssetItemCatalogId = 1

			INSERT INTO @Table_FixedAssetItemCatalogId
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@FixedAssetItemCatalogId, ',')
		END		

		IF ISNULL(@State, '') <> ''
		BEGIN
			SET @FilterState = 1

			INSERT INTO @Table_State
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@State, ',')
		END	

		IF ISNULL(@Quantity, '') <> ''
		BEGIN
			SET @FilterQuantity = 1

			INSERT INTO @Table_Quantity
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@Quantity, ',')
		END	

		/********************************** OBTENCION DE DATOS **********************************/	
	BEGIN		
			SELECT	
					fapa.Plate AS Plate,
					CONCAT(fait.Code,'-',fait.Description)AS ItemCodeName,
					fat.Name AS TrademarkName,
					fapa.Model AS Model,
					fapa.Serie AS Serie,					
					Sum(Iif(mp.Type = 3,1,0)) As Preventive,					
					Sum(Iif(mp.Type = 4,1,0)) As Corrective,					
					COUNT(Iif(mp.Type IN (3,4),1,0)) AS TotalMaintenance																			
			FROM [Maintenance].[MaintenanceProtocol] AS mp				
			JOIN Maintenance.WorkOrder wo ON mp.Id = wo.ProtocolId
			--LEFT JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON  wo.MaintenanceProgrametedId = mpp.Id					
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
			LEFT JOIN FixedAsset.FixedAssetItem fait WITH (NOLOCK) ON fapa.ItemId = fait.Id
			JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id		
			INNER JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK)  ON  fait.ItemCatalogId =faic.Id
			LEFT JOIN Maintenance.ResponsibleCatalogOfArticles rca WITH (NOLOCK) ON faic.Id =rca.ItemCatalogId
			LEFT JOIN Maintenance.MaintenanceResponsible kk WITH (NOLOCK) ON rca.ResponsibleId = kk.Id
			
			--*************************************************************************************
			LEFT JOIN @Table_PhysicalAssetId tpt ON fapa.Id = tpt.Id
			LEFT JOIN @Table_FixedAssetItemId tc ON fait.Id = tc.Id
			LEFT JOIN @Table_MaintenanceResponsibleId tdt ON KK.Id = tdt.Id
			LEFT JOIN @Table_FixedAssetItemCatalogId ts ON faic.Id = ts.Id
			LEFT JOIN @Table_State tss ON wo.State = tss.Id
			WHERE wo.ProgramDate BETWEEN Cast(@DateStart As DATETIME) AND Cast(@DateEnd As DATETIME)
				AND (@FilterByPhysicalAssetId = 0 OR tpt.Id IS NOT NULL)
				AND	(@FilterByFixedAssetItemId = 0 OR tc.Id IS NOT NULL) 
				AND	(@FilterByMaintenanceResponsibleId = 0 OR tdt.Id IS NOT NULL)
				AND	(@FilterByFixedAssetItemCatalogId = 0 OR ts.Id IS NOT NULL)				
				AND (@FilterState = 0 OR tss.Id IS NOT NULL)
				AND mp.Type IN (3, 4)									
			GROUP BY fapa.Plate,mp.Type,fait.Code,fait.Description,fapa.Model,fapa.Serie,wo.PhysicalAssetId,fat.Name
			HAVING
			(			
				(@Quantity = 0)
				OR
				(@Quantity < 10 AND COUNT(1) = @Quantity)
				OR
				(@Quantity >= 10 AND COUNT(1) >= @Quantity)
			)				
	END
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de reparaciones y mantenimientos de activos fijos en un rango de fechas. Consolida órdenes de trabajo (correctivas y preventivas) cruzando protocolos de mantenimiento, activos físicos, ítems de inventario, marcas y responsables, agrupando los resultados por placa, modelo, serie e ítem del activo. Permite filtrar por activo físico, tipo de ítem, catálogo de ítems, responsable de mantenimiento, estado de la orden y cantidad de intervenciones, siendo útil para reportería de gestión de mantenimiento de equipos y bienes de la organización.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportMaintenanceRepairs';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportMaintenanceRepairs';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte agregado de mantenimientos preventivos y correctivos por activo físico dentro de un rango de fechas, aplicando filtros opcionales y umbrales de cantidad.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con DateStart y DateEnd válidos para acotar wo.ProgramDate.; Los IDs en los filtros XML (PhysicalAssetId, FixedAssetItemId, MaintenanceResponsibleId, FixedAssetItemCatalogId, State, Quantity) deben ser convertibles a INT cuando vengan informados (lista CSV separada por comas).; Existen relaciones íntegras entre WorkOrder.ProtocolId→MaintenanceProtocol.Id y FixedAssetPhysicalAsset.TrademarkId→FixedAssetTrademark.Id (JOIN no LEFT).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se contemplan protocolos de tipo 3 (Preventivo) y 4 (Correctivo); cualquier otro tipo de MaintenanceProtocol queda excluido.; El rango de fechas se aplica sobre WorkOrder.ProgramDate (fecha programada), no sobre fecha de ejecución.; Los filtros multivalor se interpretan como listas CSV; un filtro vacío o NULL se considera ''sin filtro''.; La semántica del filtro Quantity cambia de igualdad estricta (<10) a umbral mínimo (>=10).; Los errores nunca se propagan: se capturan y retornan como resultset con Code=''999''.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mantenimiento preventivo; Mantenimiento correctivo; Orden de trabajo; Protocolo de mantenimiento; Activo fijo físico (placa, serie, modelo); Catálogo de ítems de activo fijo; Marca de activo fijo; Responsable de mantenimiento', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por placa, código/descripción de ítem, marca, modelo y serie con conteos de mantenimientos preventivos (mp.Type=3), correctivos (mp.Type=4) y total, solo cuando mp.Type IN (3,4) y wo.ProgramDate entre @DateStart y @DateEnd.; [RETURN_RESULT] resultset: En caso de excepción (CATCH) devuelve un resultset con Code=''999'', el mensaje y la línea del error en lugar del reporte.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PhysicalAssetId/@FixedAssetItemId/@MaintenanceResponsibleId/@FixedAssetItemCatalogId/@State/@Quantity vienen no vacíos → Activa el flag de filtro correspondiente y carga la tabla variable con los IDs parseados vía dbo.Split por coma; el WHERE exige coincidencia (tX.Id IS NOT NULL). else Si el filtro está apagado (flag=0) no se aplica restricción adicional sobre esa dimensión.; si @Quantity = 0 → No se filtra por cantidad de mantenimientos en el HAVING (se devuelven todos los grupos).; si @Quantity > 0 AND @Quantity < 10 → Solo se devuelven grupos cuyo COUNT(1) sea exactamente igual a @Quantity.; si @Quantity >= 10 → Solo se devuelven grupos cuyo COUNT(1) sea mayor o igual a @Quantity (umbral mínimo).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenanceProtocol; Maintenance.WorkOrder; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetTrademark; FixedAsset.FixedAssetItemCatalog; Maintenance.ResponsibleCatalogOfArticles; Maintenance.MaintenanceResponsible', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMaintenanceRepairs';
-- GO
