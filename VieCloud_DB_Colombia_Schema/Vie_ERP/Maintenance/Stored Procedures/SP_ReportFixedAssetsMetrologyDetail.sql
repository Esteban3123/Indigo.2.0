

-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 2021-02-11
-- Description:	Procedimiento para el INFORME CERTIFICADOS DE CALIBRACION EQUIPOS BIOMEDICOS
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_ReportFixedAssetsMetrologyDetail]

		@xmlCriterias AS XML,
		@xmlFilters AS XML				
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE 	
			
			-- CRITERIOS --
			@ReportType INT,
			@GroupBy	INT,
			@MaintenancePlan BIT,
			--FILTROS--
			@ClosingDate DATE,
			@PhysicalAssetId VARCHAR(MAX),
			@FixedAssetItemId VARCHAR(MAX),
			@MaintenanceResponsibleId VARCHAR(MAX),
			@FixedAssetItemCatalogId VARCHAR(MAX),
			
			---------------------------------------------------------------------------------------
			 
			@FilterByPhysicalAssetId BIT = 0,
			@FilterByFixedAssetItemId BIT = 0,
			@FilterByMaintenanceResponsibleId BIT = 0,
			@FilterByFixedAssetItemCatalogId BIT = 0

	DECLARE @Table_PhysicalAssetId AS TABLE(Id INT)
	DECLARE @Table_FixedAssetItemId AS TABLE(Id INT)
	DECLARE @Table_MaintenanceResponsibleId AS TABLE(Id INT)
	DECLARE @Table_FixedAssetItemCatalogId AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@MaintenancePlan = t.x.value('MaintenancePlan[1]','bit'),
				@ReportType = t.x.value('ReportType[1]','int')
				FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@ClosingDate = t.x.value('ClosingDate[1]','date'),
				---------------------------------------------------------------------------------------
				@PhysicalAssetId = t.x.value('PhysicalAssetId [1]','varchar(max)'),
				@FixedAssetItemId = t.x.value('FixedAssetItemId[1]','varchar(max)'),
				@MaintenanceResponsibleId = t.x.value('MaintenanceResponsibleId[1]','varchar(max)'),
				@FixedAssetItemCatalogId = t.x.value('FixedAssetItemCatalogId[1]','varchar(max)')
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

		
		/********************************** OBTENCION DE DATOS **********************************/
	IF @ReportType = 2
	BEGIN
		SELECT 
			d.*
		FROM
		(
				SELECT	v.Id AS Id,
						v.Plate AS Plate,
						CONCAT(v.ArticleCode,'-',v.ArticleName)AS ArticleCodeName,
						v.TrademarkCodeName AS TrademarkCodeName,
						v.Model AS Model,
						v.Serie AS Serie,
						f.Type As Type,
						CONCAT(cc.Code, '-',cc.Description) AS ItemCatalog,
						v.Programado AS programmed,
						CASE WHEN v.ProximoMantenimiento is null 
							Then 'No Programado' 
							ELSE CAST (v.ProximoMantenimiento AS VARCHAR) 
						END AS NextMaintenance,
						CASE WHEN mw.Consecutive is null 
							Then 'No Aplica' 
							ELSE CAST (mw.Consecutive AS VARCHAR) 
						END AS CalibrationNumber,
						@ClosingDate AS ClosingDate,
						CASE WHEN V.ProximoMantenimiento IS NULL 
							THEN 'No Aplica'
							ELSE IIF(DATEDIFF(DAY,@ClosingDate,V.ProximoMantenimiento) < 0, 'vencido' , CAST(DATEDIFF(DAY,@ClosingDate,V.ProximoMantenimiento)AS VARCHAR))
						END AS DaysToExpire
				FROM [Maintenance].[ViewMaintenanceProgramming] AS v
				LEFT JOIN FixedAsset.FixedAssetInventoryType f WITH (NOLOCK) ON v.InventoryTypeId =f.Id
				LEFT JOIN FixedAsset.FixedAssetPhysicalAsset mr WITH (NOLOCK) ON v.PhysetAssetId = mr.Id
				LEFT JOIN FixedAsset.FixedAssetItem mh WITH (NOLOCK) ON mr.ItemId = mh.Id
				INNER JOIN FixedAsset.FixedAssetItemCatalog cc WITH (NOLOCK)  ON  mh.ItemCatalogId =cc.Id
				LEFT JOIN Maintenance.ResponsibleCatalogOfArticles o WITH (NOLOCK) ON cc.Id =o.ItemCatalogId
				LEFT JOIN Maintenance.MaintenanceResponsible kk WITH (NOLOCK) ON o.ResponsibleId = kk.Id
				LEFT JOIN common.ThirdParty th WITH (NOLOCK) ON kk.ThirdPartyId = th.Id
				LEFT JOIN Maintenance.MaintenancePlanProgramated mmp WITH (NOLOCK)  ON mr.Id = mmp.FixedAssetPhysicalId
				LEFT JOIN Maintenance.WorkOrder mw WITH (NOLOCK) ON v.PhysetAssetId = mw.PhysicalAssetId
				--*************************************************************************************
				LEFT JOIN @Table_PhysicalAssetId tpt ON v.PhysetAssetId = tpt.Id
				LEFT JOIN @Table_FixedAssetItemId tc ON v.ItemId = tc.Id
				LEFT JOIN @Table_MaintenanceResponsibleId tdt ON KK.Id = tdt.Id
				LEFT JOIN @Table_FixedAssetItemCatalogId ts ON cc.Id = ts.Id
				WHERE 
				(
					(@MaintenancePlan = 0 AND mmp.FixedAssetPhysicalId IS NULL)
					OR
					(@MaintenancePlan = 1 AND mmp.FixedAssetPhysicalId IS NOT NULL)
				)
					AND	(@FilterByPhysicalAssetId = 0 OR tpt.Id IS NOT NULL)
					AND	(@FilterByFixedAssetItemId= 0 OR tc.Id IS NOT NULL) 
					AND	(@FilterByMaintenanceResponsibleId = 0 OR tdt.Id IS NOT NULL)
					AND	(@FilterByFixedAssetItemCatalogId = 0 OR ts.Id IS NOT NULL)
					--AND (f.Type = 1)			
		) AS d
		ORDER BY 1, 2
	END
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe detallado de certificados de calibración de equipos biomédicos (metrología) para activos fijos. Combina la programación de mantenimiento, los activos físicos, sus ítems y catálogos, los responsables de mantenimiento y las órdenes de trabajo para mostrar por cada equipo: su placa, código, marca, modelo, serie, tipo de inventario, catálogo al que pertenece, fecha de próximo mantenimiento, número de calibración y días para vencer o estado de vencimiento respecto a una fecha de corte. Permite filtrar por activo físico, ítem, responsable de mantenimiento y catálogo de ítems, y distingue equipos con o sin plan de mantenimiento programado. Es utilizado para el control y seguimiento de la vigencia de calibraciones y certificaciones de dispositivos biomédicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el detalle del informe de certificados de calibración de equipos biomédicos, listando activos físicos con su próximo mantenimiento, número de calibración y días para vencimiento respecto a una fecha de corte.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con MaintenancePlan y ReportType; @xmlFilters debe contener nodo /Data con ClosingDate y los IDs de filtro como CSV; Las listas CSV de IDs (PhysicalAssetId, FixedAssetItemId, MaintenanceResponsibleId, FixedAssetItemCatalogId) deben ser convertibles a INT vía dbo.Split; El reporte solo produce filas cuando @ReportType = 2', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los días para vencer siempre se calculan respecto a @ClosingDate (fecha de corte); El reporte es mutuamente excluyente entre activos con plan de mantenimiento programado y sin plan, según @MaintenancePlan; Solo se ejecuta la lógica de reporte cuando @ReportType = 2; Los errores nunca se propagan: siempre se devuelven como resultset con Code=''999''; Los filtros operan en modo opcional: si la lista CSV viene vacía/NULL, no restringen el resultado', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Certificado de calibración; Equipos biomédicos; Activo fijo físico; Plan de mantenimiento programado; Orden de trabajo (consecutivo de calibración); Próximo mantenimiento; Responsable de mantenimiento; Catálogo de ítems; Fecha de corte; Vencimiento de calibración', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando @ReportType = 2, retorna el listado de activos con Plate, artículo, marca, modelo, serie, tipo, catálogo, próximo mantenimiento, número de calibración, fecha de corte y días para vencer; [RETURN_RESULT] Resultset: Cuando ProximoMantenimiento es NULL, NextMaintenance se reporta como ''No Programado'' y DaysToExpire como ''No Aplica''; [RETURN_RESULT] Resultset: Cuando DATEDIFF(DAY,@ClosingDate,ProximoMantenimiento) < 0, DaysToExpire se reporta como ''vencido''; en caso contrario, los días restantes; [RETURN_RESULT] Resultset: Cuando WorkOrder.Consecutive es NULL, CalibrationNumber se reporta como ''No Aplica''; [RAISERROR] Resultset: Ante cualquier error en TRY, retorna fila con Code=''999'', mensaje y línea de error en lugar de propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ReportType = 2 → Ejecuta la consulta principal de detalle de calibración else No retorna resultset (no hay rama alternativa definida); si @MaintenancePlan = 0 → Solo incluye activos SIN registro en MaintenancePlanProgramated (mmp.FixedAssetPhysicalId IS NULL) else Si @MaintenancePlan = 1, solo incluye activos CON plan programado (mmp.FixedAssetPhysicalId IS NOT NULL); si ISNULL(@PhysicalAssetId,'''') <> '''' → Activa filtro por activo físico y carga IDs en tabla temporal restringiendo el resultado else No aplica filtro por activo físico; si ISNULL(@FixedAssetItemId,'''') <> '''' → Activa filtro por ítem de activo fijo else No aplica filtro por ítem; si ISNULL(@MaintenanceResponsibleId,'''') <> '''' → Activa filtro por responsable de mantenimiento else No aplica filtro por responsable; si ISNULL(@FixedAssetItemCatalogId,'''') <> '''' → Activa filtro por catálogo de ítem else No aplica filtro por catálogo', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.ViewMaintenanceProgramming; FixedAsset.FixedAssetInventoryType; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; Maintenance.ResponsibleCatalogOfArticles; Maintenance.MaintenanceResponsible; common.ThirdParty; Maintenance.MaintenancePlanProgramated; Maintenance.WorkOrder', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologyDetail';
-- GO
