

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