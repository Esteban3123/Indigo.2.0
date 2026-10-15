


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