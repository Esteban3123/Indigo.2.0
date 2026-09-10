
CREATE VIEW [Billing].[BotViewServicesOxygenConsumption]
AS 

SELECT v.*, adin.GENCAREGROUP, adin.GENCONENTITY, ha.ThirdPartyId, (select top 1 Id from [Common].[OperatingUnit]) AS OperativeUnitId, cg.TypeLiquidationOxygen
FROM [dbo].[ViewOxygenConsumption] v
	INNER JOIN [dbo].[ADINGRESO] adin on adin.NUMINGRES = v.NUMINGRES
	INNER JOIN [Contract].[CareGroup] cg ON cg.Id = adin.GENCAREGROUP 
	INNER JOIN [Contract].[HealthAdministrator] ha ON ha.Id = adin.GENCONENTITY
	LEFT JOIN [Billing].[BotServicesProceduresLog] botslog ON botslog.Row = v.Row AND botslog.DataSourceType = 8
WHERE v.Seleccione = 0 and adin.IESTADOIN IN('','P') AND adin.GENCAREGROUP IS NOT NULL AND botslog.Id IS NULL AND v.SkipLiquidation = 0

WITH CHECK OPTION ;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de facturación automática para identificar los consumos de oxígeno pendientes de liquidar en pacientes activos (en observación o en proceso de ingreso). Combina el consumo de oxígeno registrado por el sistema con los datos del ingreso del paciente (número de ingreso, grupo de atención y entidad contratante/EPS), el identificador externo de la administradora de salud, la unidad operativa de la sede y el tipo de liquidación de oxígeno definido en el contrato. Filtra únicamente los registros que aún no han sido procesados por el bot de servicios y procedimientos, que no están marcados para omitir liquidación y cuyo ingreso se encuentre activo, garantizando que cada consumo de oxígeno sea facturado una sola vez de forma automatizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesOxygenConsumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'BotViewServicesOxygenConsumption';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los consumos de oxígeno pendientes de liquidación, enriquecidos con datos del ingreso, grupo de cuidado, administradora de salud y unidad operativa, para ser procesados por el bot de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe tener GENCAREGROUP no nulo y referenciar un CareGroup válido.; El ingreso debe referenciar una HealthAdministrator válida vía GENCONENTITY.; Debe existir al menos una OperatingUnit en Common.OperatingUnit (se selecciona TOP 1).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen consumos de oxígeno cuyo ingreso tenga grupo de cuidado (GENCAREGROUP) asignado.; Filtra siempre por DataSourceType=8 al verificar el log del bot, identificando esta vista como fuente de datos 8.; WITH CHECK OPTION garantiza que cualquier modificación a través de la vista respete los filtros (Seleccione=0, SkipLiquidation=0, estado del ingreso, etc.).; Siempre adjunta la misma OperatingUnit (TOP 1) como unidad operativa a todas las filas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consumo de oxígeno; Ingreso/admisión hospitalaria; Grupo de cuidado (CareGroup); Administradora de salud (tercero pagador); Liquidación; Tipo de liquidación de oxígeno; Unidad operativa; Bot de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BotViewServicesOxygenConsumption: Devuelve filas de ViewOxygenConsumption con Seleccione=0, SkipLiquidation=0, ingreso en estado '''' o ''P'' y sin registro previo en BotServicesProceduresLog con DataSourceType=8 para esa Row.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si adin.IESTADOIN IN ('''',''P'') → Incluye el ingreso (estado vacío o ''P'' = pendiente/activo) else Excluye el ingreso del resultado; si botslog.Id IS NULL (LEFT JOIN con DataSourceType=8) → Incluye la fila por no haber sido procesada previamente por el bot else Excluye la fila ya registrada en el log del bot; si v.Seleccione = 0 AND v.SkipLiquidation = 0 → Considera el consumo como pendiente de liquidación else Excluye consumos ya seleccionados o marcados para omitir liquidación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewOxygenConsumption; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; Billing.BotServicesProceduresLog; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'BotViewServicesOxygenConsumption';
GO
