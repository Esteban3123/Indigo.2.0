

CREATE VIEW [Billing].[BotViewServicesProceduresTherapy]
AS 

SELECT adin.GENCAREGROUP, adin.CODCENATE, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, 
		v.* 
FROM [dbo].[ViewServicesProceduresTherapy] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 6
WHERE v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.SkipLiquidation = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación para identificar servicios, procedimientos y terapias pendientes de liquidación automática. Combina los registros de ingresos activos o en proceso (urgencias y hospitalizaciones con estado vacío o ''P'') con la información del contrato de la administradora de salud (EPS/pagador) y la unidad operativa de la sede, filtrando únicamente aquellos registros que aún no han sido procesados por el bot (sin registro en el log de procesamiento), que no deben omitirse en la liquidación y que tienen grupo de atención asignado. Sirve como fuente de datos del proceso automatizado de facturación de servicios y procedimientos de terapia, garantizando que solo se envíen al bot los registros nuevos y pendientes de ciclo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresTherapy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresTherapy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los servicios y procedimientos de terapia pendientes de liquidación de ingresos activos, enriquecidos con datos del grupo asistencial, entidad pagadora y unidad operativa, para ser procesados por el bot de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe existir y estar relacionado a la fila de la vista por NUMINGRES.; La entidad pagadora del ingreso (GENCONENTITY) debe corresponder a un HealthAdministrator existente.; Debe existir al menos un registro en Common.OperatingUnit (se toma el TOP 1 sin ORDER BY como unidad operativa).; El ingreso debe tener un GENCAREGROUP (grupo asistencial) asignado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca expone servicios/procedimientos ya procesados por el bot (existe registro en BotServicesProceduresLog con DataSourceType=6 para la misma Row).; Nunca expone filas marcadas como seleccionadas (Seleccione<>0) ni con SkipLiquidation activo.; Sólo considera ingresos cuyo estado IESTADOIN sea vacío o ''P'' (pendiente/activo), excluyendo otros estados como anulados o cerrados.; Sólo considera ingresos con grupo asistencial (GENCAREGROUP) definido.; WITH CHECK OPTION garantiza que cualquier modificación a través de la vista mantenga las condiciones del WHERE.; La unidad operativa retornada es la misma para todas las filas (TOP 1 de Common.OperatingUnit).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'servicios y procedimientos de terapia; ingreso/admisión de paciente; grupo asistencial (care group); entidad pagadora / administradora de salud; unidad operativa / sede; liquidación de facturación; bot de procesamiento de facturación; estado del ingreso', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BotViewServicesProceduresTherapy: Devuelve sólo filas con v.Seleccione=0, v.SkipLiquidation=0, adin.IESTADOIN IN ('''',''P''), adin.GENCAREGROUP NOT NULL y sin registro previo en BotServicesProceduresLog con DataSourceType=6 para esa Row.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewServicesProceduresTherapy; dbo.ADINGRESO; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresTherapy';
GO
