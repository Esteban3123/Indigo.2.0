

CREATE view [MixingStation].[ViewListCMCenterAttention] 
as 

	Select 
		CONCAT(C.IdMixingStation,AD.CODCENATE) AS Id, 
		C.IdMixingStation AS 'MixingStation', 
		RTRIM(AD.CODCENATE) AS 'CodeCenterAttention', 
		RTRIM(AD.NOMCENATE) AS 'CenterAttention',	
		RTRIM(AD.CODCENATE) + ' - ' + RTRIM(AD.NOMCENATE) AS 'CenterAttentionDescription', 
		string_agg(C.IdProductionLine, ', ') AS ProductionLineIds
	from MixingStation.CMCenterAttention C with(NOLOCK)
	INNER JOIN dbo.ADCENATEN AD with(NOLOCK) ON AD.CODCENATE = C.CodeCenterAttention
	Where C.StateCA = 1
	GROUP BY C.IdMixingStation,AD.CODCENATE,AD.NOMCENATE

UNION ALL

	Select 
		CONCAT(EX.Code,CM.CMConfigurationId) AS Id,
		CM.CMConfigurationId AS 'MixingStation', 
		RTRIM(EX.Code) AS 'CodeCenterAttention', 
		RTRIM(EX.Description) AS 'CenterAttention', 
		RTRIM(EX.Code) + ' - ' + RTRIM(EX.Description) AS 'CenterAttentionDescription',
		string_agg(CM.ProductionLineId, ', ') AS ProductionLineIds
	from [MixingStation].[CMExternalCareCenter] CM with(NOLOCK)
	INNER JOIN MixingStation.ExternalCareCenter EX with(NOLOCK) ON EX.Id = CM.ExternalCareCenterId
	Where CM.Status = 1
	GROUP BY CM.CMConfigurationId, EX.Code, EX.Description
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista unificada de centros de atención vinculados a estaciones de mezcla (Mixing Stations), tanto internos como externos. Combina los centros de atención habilitados en el sistema (sedes, clínicas u hospitales registrados en ADCENATEN) con centros de atención externos configurados en el módulo de farmacia/mezclas, mostrando solo los que están activos. Para cada centro expone su código, nombre descriptivo y las líneas de producción asociadas. Sirve como catálogo de consulta para asignar y visualizar qué centros de atención están configurados en cada estación de mezcla, incluyendo las líneas de producción habilitadas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCMCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCMCenterAttention';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista consolidada de centros de atención (internos y externos) habilitados por estación de mezcla, agrupando las líneas de producción asociadas a cada combinación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Para que un centro de atención interno aparezca, debe existir su código en dbo.ADCENATEN coincidiendo con CMCenterAttention.CodeCenterAttention.; Para que un centro externo aparezca, el ExternalCareCenterId de CMExternalCareCenter debe existir en MixingStation.ExternalCareCenter.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen centros de atención cuyo estado esté activo: StateCA = 1 para internos y Status = 1 para externos.; Las líneas de producción asociadas a un mismo par (estación de mezcla, centro de atención) se concatenan en una sola cadena separada por '', '' usando STRING_AGG.; El identificador del registro (Id) se construye uniendo el id de la estación de mezcla con el código del centro de atención, garantizando unicidad por combinación.; La descripción del centro de atención se presenta en formato ''Código - Nombre'' con espacios eliminados (RTRIM).; La vista unifica en un único conjunto de resultados (UNION ALL) los centros de atención internos (ADCENATEN) y los externos (ExternalCareCenter).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estación de mezcla; centro de atención; centro de atención externo; línea de producción', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.CMCenterAttention: Cuando CMCenterAttention.StateCA = 1 y existe coincidencia con dbo.ADCENATEN.CODCENATE, se retorna una fila por cada par (IdMixingStation, CODCENATE) con sus líneas de producción agregadas.; [RETURN_RESULT] MixingStation.CMExternalCareCenter: Cuando CMExternalCareCenter.Status = 1 y existe coincidencia con MixingStation.ExternalCareCenter.Id, se retorna una fila por cada par (CMConfigurationId, EX.Code) con sus líneas de producción agregadas, anexada vía UNION ALL a los centros internos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMCenterAttention; dbo.ADCENATEN; MixingStation.CMExternalCareCenter; MixingStation.ExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMCenterAttention';
GO
