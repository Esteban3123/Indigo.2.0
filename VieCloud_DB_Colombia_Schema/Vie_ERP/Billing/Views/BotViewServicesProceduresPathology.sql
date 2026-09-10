

CREATE VIEW [Billing].[BotViewServicesProceduresPathology]
AS  

SELECT adin.GENCAREGROUP, adin.CODCENATE, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, 
		v.* 
FROM [dbo].[VServicesProceduresPathologies] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 3
WHERE v.TIPO = 'Realizados' and v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.SkipLiquidation = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación automática para identificar servicios, procedimientos y patologías realizados que aún no han sido procesados ni liquidados. Combina los procedimientos realizados (desde VServicesProceduresPathologies) con los datos del ingreso del paciente (ADINGRESO), la administradora de salud o EPS pagadora (HealthAdministrator) y la unidad operativa o sede de atención (OperatingUnit). Filtra únicamente los registros con estado de ingreso activo o pendiente, que pertenezcan a un grupo de atención válido, que no hayan sido previamente procesados por el bot (ausentes en BotServicesProceduresLog) y que no estén marcados para omitir la liquidación, garantizando que solo se envíen al proceso de facturación los servicios pendientes de cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresPathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesProceduresPathology';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos de patología realizados sobre ingresos activos/pendientes que aún no han sido procesados por el bot de facturación, enriquecidos con datos del ingreso, administradora de salud y unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe existir y estar relacionado con el registro de patología por NUMINGRES.; El ingreso debe tener una administradora de salud (GENCONENTITY) válida en Contract.HealthAdministrator.; Debe existir al menos una unidad operativa registrada en Common.OperatingUnit.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila retornada pertenece a un ingreso con GENCAREGROUP no nulo.; Toda fila retornada está asociada a una administradora de salud existente (INNER JOIN HealthAdministrator).; El OperativeUnitId siempre corresponde al primer registro de Common.OperatingUnit (TOP 1 sin ORDER BY).; WITH CHECK OPTION garantiza que cualquier modificación a través de la vista debe seguir cumpliendo los filtros del WHERE.; Una fila ya registrada en BotServicesProceduresLog con DataSourceType=3 nunca aparece en la vista (idempotencia del bot).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'procedimientos de patología; ingreso/admisión de paciente; administradora de salud (EPS/pagador); grupo de cuidado; unidad operativa/sede; facturación automatizada (bot); liquidación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BotViewServicesProceduresPathology: Devuelve solo procedimientos de patología con TIPO=''Realizados'', Seleccione=0, SkipLiquidation=0, ingreso en estado ('''' o ''P''), grupo de cuidado no nulo y sin registro previo en BotServicesProceduresLog para DataSourceType=3.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.TIPO = ''Realizados'' → Solo se incluyen procedimientos marcados como realizados; los demás tipos se excluyen.; si adin.IESTADOIN IN ('''',''P'') → Solo ingresos en estado vacío o ''P'' (pendiente) son elegibles; otros estados se descartan.; si botslog.Id IS NULL (LEFT JOIN con DataSourceType=3) → Solo se incluyen filas que no tengan log de procesamiento previo del bot para fuente tipo 3.; si v.Seleccione = 0 AND v.SkipLiquidation = 0 → Se excluyen los ya seleccionados o marcados para omitir liquidación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.VServicesProceduresPathologies; dbo.ADINGRESO; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesProceduresPathology';
GO
