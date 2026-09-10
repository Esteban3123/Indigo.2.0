

-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 2021-02-11
-- Description:	Procedimiento para el INFORME CERTIFICADOS DE CALIBRACION EQUIPOS BIOMEDICOS
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_ReportFixedAssetsMetrologySummary]

		@xmlCriterias AS XML,
		@xmlFilters AS XML				
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE 	
			
			-- CRITERIOS --
			@ReportType			INT,
			@GroupBy			INT,
			@MaintenancePlan	BIT,
			--FILTROS--
			@ClosingDate		DATE,
			@PhysicalAssetId	VARCHAR(MAX),
			@FixedAssetItemId	VARCHAR(MAX),
			@MaintenanceResponsibleId	VARCHAR(MAX),
			@FixedAssetItemCatalogId	VARCHAR(MAX),
			
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
	IF @ReportType = 1
	BEGIN
		SELECT 
			d.*
		FROM
		(
				SELECT	--v.Id
						f.Type as Type,
						cc.Description AS ItemCatalog,
						v.Programado AS programmed,
						COUNT(f.type) AS CountType,
						CASE WHEN V.ProximoMantenimiento IS NULL 
							THEN 3
							ELSE IIF(DATEDIFF(DAY,@ClosingDate,V.ProximoMantenimiento) < 0, 2 , 1)
						END AS ExpiredOrNot, -- 1 sin vencer, 2 vencido, 3 no aplica
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
					GROUP by f.Type, cc.Description, v.Programado, v.ProximoMantenimiento
					
		) AS d
		GROUP By d.CountType, d.Type, d.ItemCatalog, d.programmed,d.ExpiredOrNot,d.DaysToExpire
		ORDER BY 1, 2
	END
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe resumen de metrología (certificados de calibración) de activos fijos biomédicos. Consolida información de programación de mantenimiento, tipo de inventario, catálogo de ítems y responsables de mantenimiento para determinar, por cada categoría de equipo, cuántos tienen su certificado vigente, vencido o no aplica respecto a una fecha de corte. Permite filtrar por activo físico, ítem, catálogo de ítem y responsable de mantenimiento, además de discriminar si el equipo está o no incluido en un plan de mantenimiento programado. El resultado se usa para reportería de cumplimiento metrológico y seguimiento del estado de calibración de equipos biomédicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el resumen del informe de certificados de calibración de equipos biomédicos, agrupando activos físicos por tipo y catálogo y clasificándolos según vigencia de su próximo mantenimiento respecto a una fecha de corte.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con MaintenancePlan y ReportType.; @xmlFilters debe contener nodo /Data con ClosingDate y los identificadores opcionales (PhysicalAssetId, FixedAssetItemId, MaintenanceResponsibleId, FixedAssetItemCatalogId) como listas separadas por comas convertibles a INT.; Solo se ejecuta la consulta principal cuando @ReportType = 1.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ExpiredOrNot solo toma valores 1 (sin vencer), 2 (vencido) o 3 (no aplica).; La vigencia se calcula siempre comparando ProximoMantenimiento contra la fecha de corte @ClosingDate.; Los filtros opcionales se ignoran cuando su flag respectivo es 0; cuando son 1, exigen coincidencia con la tabla temporal cargada desde la lista CSV.; El filtro por @MaintenancePlan es excluyente: o solo activos con plan programado o solo activos sin plan.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Certificados de calibración; Equipos biomédicos; Mantenimiento programado; Metrología; Activo fijo físico; Catálogo de ítems de activo fijo; Responsable de mantenimiento; Próximo mantenimiento; Vencimiento respecto a fecha de corte', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @ReportType=1, devuelve conjunto agrupado por Type, ItemCatalog, programmed, ExpiredOrNot y DaysToExpire con conteo de activos por tipo.; [RETURN_RESULT] RESULTSET: En caso de error en TRY/CATCH, retorna una fila con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @MaintenancePlan = 0 → Solo incluye activos SIN registro en Maintenance.MaintenancePlanProgramated (mmp.FixedAssetPhysicalId IS NULL).; si @MaintenancePlan = 1 → Solo incluye activos CON registro en Maintenance.MaintenancePlanProgramated (mmp.FixedAssetPhysicalId IS NOT NULL).; si V.ProximoMantenimiento IS NULL → Clasifica como ExpiredOrNot=3 (''No Aplica''). else Si DATEDIFF(DAY,@ClosingDate,ProximoMantenimiento) < 0 → ExpiredOrNot=2 (''vencido''); en otro caso ExpiredOrNot=1 (''sin vencer'') y DaysToExpire = días restantes.; si ISNULL(@PhysicalAssetId,'''') <> '''' (idem para FixedAssetItemId, MaintenanceResponsibleId, FixedAssetItemCatalogId) → Activa el flag de filtro correspondiente y carga los IDs en la tabla temporal vía dbo.Split, restringiendo el resultado a esos IDs.; si @ReportType <> 1 → No se ejecuta ninguna consulta y no retorna filas (salvo error).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.ViewMaintenanceProgramming; FixedAsset.FixedAssetInventoryType; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; Maintenance.ResponsibleCatalogOfArticles; Maintenance.MaintenanceResponsible; common.ThirdParty; Maintenance.MaintenancePlanProgramated', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrologySummary';
-- GO
