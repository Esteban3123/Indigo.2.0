

CREATE VIEW [Billing].[BotViewServicesProceduresNoQx]
AS 

SELECT adin.GENCAREGROUP, adin.CODCENATE, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, 
		v.* 
FROM [dbo].[VServicesProceduresNoQx] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 4
	INNER JOIN [Contract].[CUPSEntity] cups ON cups.Code = v.CODSERIPS
WHERE v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.Tipo = 'Realizados' AND v.SkipLiquidation = 0
	AND cups.TherapyProcedure = 0 AND cups.OxigenService = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación automática para identificar servicios y procedimientos NO quirúrgicos que están pendientes de liquidar. Combina los servicios realizados (desde VServicesProceduresNoQx) con los datos del ingreso del paciente (admisión activa o en proceso), la administradora de salud (EPS/pagador) y el catálogo CUPS, excluyendo procedimientos de terapia y oxígeno, registros ya procesados por el bot y liquidaciones omitidas. Enriquece cada registro con el grupo de atención, el centro de atención, la entidad contratante y el identificador externo del pagador (ThirdPartyId), entregando al bot exactamente los renglones de servicios que aún deben facturarse.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresNoQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresNoQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos no quirúrgicos ya realizados pendientes de liquidación automática por el bot de facturación, enriquecidos con datos del ingreso, administradora de salud y unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe al menos una unidad operativa registrada en Common.OperatingUnit (se toma TOP 1 sin orden definido).; El ingreso (ADINGRESO) debe tener entidad pagadora (GENCONENTITY) registrada en Contract.HealthAdministrator.; El código de servicio (CODSERIPS) debe existir en el catálogo Contract.CUPSEntity.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen procedimientos cuyo CUPS NO sea terapia (TherapyProcedure=0) ni servicio de oxígeno (OxigenService=0); estos se excluyen explícitamente del flujo de este bot.; Se excluyen filas ya procesadas por el bot de DataSourceType=4 (LEFT JOIN con IS NULL sobre BotServicesProceduresLog).; Solo aplica a procedimientos marcados como ''Realizados'' y no marcados para omitir liquidación (SkipLiquidation=0).; Solo se consideran ingresos activos o pendientes (IESTADOIN IN ('''',''P'')) y con grupo de cuidado (GENCAREGROUP) asignado.; WITH CHECK OPTION: cualquier modificación a través de la vista debe seguir cumpliendo los filtros del WHERE.; La OperativeUnitId asignada es la misma para todas las filas (TOP 1 sin ORDER BY de Common.OperatingUnit).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'procedimientos no quirúrgicos; ingreso/admisión de paciente; administradora de salud (EPS); CUPS; terapias; servicio de oxígeno; liquidación/facturación; grupo de cuidado; unidad operativa; bot de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas de VServicesProceduresNoQx solo cuando Seleccione=0, Tipo=''Realizados'', SkipLiquidation=0, no existe registro previo en BotServicesProceduresLog con DataSourceType=4 para esa fila, el ingreso está en estado ''''/''P'' con grupo de cuidado asignado, y el CUPS no es terapia ni servicio de oxígeno.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.VServicesProceduresNoQx; dbo.ADINGRESO; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Contract.CUPSEntity; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresNoQx';
GO
