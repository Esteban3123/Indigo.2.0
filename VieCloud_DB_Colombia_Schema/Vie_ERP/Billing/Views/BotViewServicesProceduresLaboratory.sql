CREATE VIEW [Billing].[BotViewServicesProceduresLaboratory]
AS  

SELECT adin.GENCAREGROUP, adin.CODCENATE, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, 
		v.* 
FROM [dbo].[VServicesProcedures] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 1
WHERE v.TIPO in('Realizados','Muestra Recolectada') and v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.SkipLiquidation = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación automática para identificar servicios de laboratorio (exámenes realizados o muestras recolectadas) que aún no han sido liquidados ni procesados. Combina los procedimientos/servicios pendientes de la vista VServicesProcedures con los datos del ingreso del paciente (ADINGRESO), la administradora de salud o EPS pagadora (HealthAdministrator) y la unidad operativa de la sede. Filtra únicamente ingresos activos o en proceso (estado vacío o ''P''), con grupo de atención asignado, que no hayan sido previamente procesados por el bot (sin registro en BotServicesProceduresLog) y que no estén marcados para omitir liquidación. Sirve como fuente de trabajo para el procesamiento automatizado de facturación de laboratorios, incluyendo datos como el grupo de atención, centro de atención, entidad contratante, identificador externo del pagador (NIT/ThirdPartyId) y la sede de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresLaboratory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresLaboratory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los servicios y procedimientos de laboratorio pendientes de liquidación que el bot de facturación debe procesar, enriquecidos con datos del ingreso, la administradora de salud y la unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'dbo.VServicesProcedures debe exponer columnas TIPO, Seleccione, NUMINGRES, Row y SkipLiquidation.; dbo.ADINGRESO debe contener el ingreso (NUMINGRES) referenciado por el servicio.; El ingreso debe tener una administradora de salud (GENCONENTITY) registrada en Contract.HealthAdministrator.; Debe existir al menos un registro en Common.OperatingUnit para resolver la unidad operativa.; Billing.BotServicesProceduresLog se usa como filtro de exclusión por DataSourceType = 1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone servicios cuyo tipo sea ''Realizados'' o ''Muestra Recolectada''.; Solo expone servicios no seleccionados previamente (Seleccione = 0) y no marcados para omitir liquidación (SkipLiquidation = 0).; Solo considera ingresos cuyo estado (IESTADOIN) sea vacío ('''') o ''P'' (pendiente/activo), excluyendo otros estados.; Exige que el ingreso tenga grupo de cuidado (GENCAREGROUP) asignado; nunca expone ingresos sin GENCAREGROUP.; Exige que el ingreso tenga una administradora de salud existente en Contract.HealthAdministrator (ThirdPartyId resoluble vía INNER JOIN).; Excluye servicios que ya fueron procesados por el bot con DataSourceType = 1 (registro existente en BotServicesProceduresLog).; La unidad operativa siempre se asigna como el primer Id encontrado en Common.OperatingUnit (asume una única unidad operativa relevante).; Por WITH CHECK OPTION, cualquier modificación a través de la vista debe seguir cumpliendo los filtros (tipo, estado, no seleccionado, no procesado por bot, etc.).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios y procedimientos de laboratorio; Ingreso/admisión del paciente; Administradora de salud (pagador); Liquidación/Facturación; Unidad operativa (sede); Bot de procesamiento de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BotViewServicesProceduresLaboratory: Devuelve servicios de VServicesProcedures con TIPO IN (''Realizados'',''Muestra Recolectada''), Seleccione=0, SkipLiquidation=0, ingreso con IESTADOIN IN ('''',''P'') y GENCAREGROUP NOT NULL, y sin registro previo en BotServicesProceduresLog con DataSourceType=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.VServicesProcedures; dbo.ADINGRESO; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresLaboratory';
GO
