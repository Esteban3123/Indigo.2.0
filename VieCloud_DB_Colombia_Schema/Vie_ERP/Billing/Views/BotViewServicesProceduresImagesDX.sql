

CREATE VIEW [Billing].[BotViewServicesProceduresImagesDX]
AS  

SELECT adin.GENCAREGROUP, adin.CODCENATE, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, 
		v.* 
FROM [dbo].[VServicesProceduresImagesDx] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 2
WHERE v.TIPO in ('Estudios Realizados', 'Estudios Realizados en Sitio') and v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.SkipLiquidation = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación para identificar estudios de imágenes diagnósticas (radiografías, ecografías, tomografías y similares realizados en sitio o en el servicio) que aún no han sido liquidados ni procesados automáticamente. Combina los servicios de imágenes pendientes (VServicesProceduresImagesDx) con los datos del ingreso del paciente (ADINGRESO), la administradora de salud o EPS correspondiente (HealthAdministrator) y la unidad operativa de la institución, filtrando únicamente ingresos activos o en proceso (estado ''P'' o vacío), con grupo de atención definido y que no tengan registro previo en el log del bot de procesamiento (BotServicesProceduresLog). Su propósito es alimentar el proceso automatizado de facturación de imágenes diagnósticas, evitando reprocesar registros ya gestionados o marcados para omitir liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresImagesDX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresImagesDX';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los estudios/imágenes diagnósticas pendientes de liquidación, asociados a ingresos activos y a su administradora de salud, para ser procesados por el bot de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe existir y estar relacionado con el registro de servicios mediante NUMINGRES; El ingreso debe tener una administradora de salud (GENCONENTITY) registrada en Contract.HealthAdministrator; Debe existir al menos una unidad operativa en Common.OperatingUnit (se toma TOP 1)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila resultante pertenece a un ingreso con grupo de cuidado (GENCAREGROUP) definido; Cada fila resultante está vinculada a una administradora de salud existente (HealthAdministrator); Nunca se incluyen registros ya logueados por el bot con DataSourceType=2; OperativeUnitId siempre corresponde al primer registro de Common.OperatingUnit; WITH CHECK OPTION garantiza que cualquier modificación a través de la vista respete los filtros definidos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estudios diagnósticos / imágenes diagnósticas; ingreso/admisión del paciente; administradora de salud (EPS/aseguradora); liquidación de servicios; grupo de cuidado; unidad operativa/sede; bot de facturación; tercero pagador (ThirdPartyId)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BotViewServicesProceduresImagesDX: Devuelve solo registros con TIPO en (''Estudios Realizados'',''Estudios Realizados en Sitio''), Seleccione=0, SkipLiquidation=0, IESTADOIN del ingreso en ('''',''P''), GENCAREGROUP no nulo y sin log previo en BotServicesProceduresLog con DataSourceType=2', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.TIPO IN (''Estudios Realizados'',''Estudios Realizados en Sitio'') → Incluye solo estudios diagnósticos realizados (no otros tipos de servicio); si adin.IESTADOIN IN ('''',''P'') → Considera únicamente ingresos en estado vacío o ''P'' (presumiblemente activos/pendientes); si botslog.Id IS NULL (LEFT JOIN con DataSourceType=2) → Excluye filas que ya fueron procesadas previamente por el bot para este origen de datos; si v.Seleccione = 0 AND v.SkipLiquidation = 0 → Solo registros no seleccionados manualmente y que no deben omitirse de la liquidación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.VServicesProceduresImagesDx; dbo.ADINGRESO; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresImagesDX';
GO
