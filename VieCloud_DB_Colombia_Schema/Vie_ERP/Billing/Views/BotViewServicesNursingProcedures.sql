

CREATE VIEW [Billing].[BotViewServicesNursingProcedures]
AS 

SELECT adin.GENCAREGROUP, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, 
		v.* 
FROM [dbo].[ViewServiceProceduresNursingProcedures] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 7
WHERE v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.Tipo = 'Facturable' AND v.SkipLiquidation = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación automática para identificar procedimientos de enfermería facturables que aún no han sido procesados. Combina los servicios de enfermería pendientes (ViewServiceProceduresNursingProcedures) con los datos del ingreso del paciente (admisión activa o en proceso, con grupo de atención asignado), la administradora de salud responsable (EPS o pagador) y la unidad operativa de la sede. Filtra únicamente los registros de tipo ''Facturable'', no omitidos de liquidación, con ingresos vigentes y sin registro previo en el log del bot (BotServicesProceduresLog), garantizando que solo se presenten procedimientos pendientes de liquidar. Sirve como fuente de datos para el proceso automatizado de facturación de procedimientos de enfermería por ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesNursingProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesNursingProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos de enfermería facturables pendientes de procesamiento por el bot de facturación, enriquecidos con datos del ingreso, administradora de salud y unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe existir y estar referenciado por NUMINGRES en la vista de procedimientos de enfermería.; El ingreso debe tener asignado un grupo de cuidado (GENCAREGROUP IS NOT NULL).; El ingreso debe estar en estado vacío o ''P'' (IESTADOIN IN ('''',''P'')).; Debe existir una administradora de salud (HealthAdministrator) cuyo Id coincida con GENCONENTITY del ingreso.; Debe existir al menos un registro en Common.OperatingUnit (se toma el primero).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo procedimiento expuesto pertenece a un ingreso con grupo de cuidado asignado.; Todo procedimiento expuesto está marcado como ''Facturable'' y no omite liquidación (SkipLiquidation = 0).; Nunca se exponen procedimientos ya registrados en el log del bot con DataSourceType = 7.; El OperativeUnitId siempre corresponde a la primera unidad operativa registrada en Common.OperatingUnit (TOP 1 sin ORDER BY).; WITH CHECK OPTION: cualquier modificación a través de la vista debe seguir cumpliendo los filtros definidos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'procedimientos de enfermería; facturación; ingreso/admisión del paciente; administradora de salud (EPS); grupo de cuidado; unidad operativa; liquidación; bot de procesamiento de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BotViewServicesNursingProcedures: Solo retorna filas donde v.Seleccione = 0, v.Tipo = ''Facturable'', v.SkipLiquidation = 0 y no exista log previo en BotServicesProceduresLog con DataSourceType = 7 para esa Row.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si botslog.Id IS NULL (LEFT JOIN sobre BotServicesProceduresLog con DataSourceType = 7 y mismo Row) → Incluye el procedimiento en el resultado (no procesado aún por el bot) else Excluye el procedimiento (ya fue procesado por el bot); si adin.IESTADOIN IN ('''',''P'') → Considera el ingreso como elegible para facturación de procedimientos de enfermería else Excluye los procedimientos del ingreso', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewServiceProceduresNursingProcedures; dbo.ADINGRESO; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesNursingProcedures';
GO
