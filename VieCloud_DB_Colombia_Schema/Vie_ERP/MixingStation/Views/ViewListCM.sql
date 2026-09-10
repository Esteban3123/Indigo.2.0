

CREATE view [MixingStation].[ViewListCM] 
as 

select CONCAT(cm.Id, '-', cmca.Id, '-', cmu.Id) Id,
cm.Code CMCode, cm.Name CMName, cm.Code + ' - ' + cm.Name CMCodeName,
RTRIM(LTRIM(ca.CODCENATE)) CareCenterCode, RTRIM(LTRIM(ca.NOMCENATE)) CareCenterName, RTRIM(LTRIM(ca.CODCENATE)) + ' - ' + RTRIM(LTRIM(ca.NOMCENATE)) CareCenterCodeName,
cmu.UserCode, cmu.UserId,
cm.Code + ' - ' + cm.Name + '   ' + RTRIM(LTRIM(ca.CODCENATE)) + ' - ' + RTRIM(LTRIM(ca.NOMCENATE)) Description,
cm.Id CMConfigurationId
from MixingStation.CMConfiguration cm
inner join MixingStation.CMCenterAttention cmca on cmca.IdMixingStation = cm.Id
inner join .ADCENATEN ca on ca.CODCENATE = cmca.CodeCenterAttention
inner join MixingStation.CMConfigurationUsers cmu on cmu.CMConfigurationId = cm.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de estaciones de mezcla (CM) con sus centros de atención y usuarios asignados. Combina la configuración de cada estación de mezcla, el centro de atención (sede o clínica) al que pertenece y los usuarios habilitados para operar en ella, generando una descripción legible con código y nombre tanto de la estación como del centro. Sirve para poblar selectores, filtros y reportes donde se necesite identificar qué estación de mezcla está disponible en qué sede y para qué usuario.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCM';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la lista consolidada de configuraciones de estaciones de mezcla cruzadas con sus centros de atención y usuarios autorizados, enriquecida con códigos y nombres legibles.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las configuraciones de estación de mezcla deben tener al menos un centro de atención asociado en CMCenterAttention para aparecer.; Las configuraciones deben tener al menos un usuario asignado en CMConfigurationUsers para aparecer.; El código del centro de atención (CodeCenterAttention) debe existir en ADCENATEN.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id resultante es único por la tripleta (configuración, centro de atención asignado, usuario asignado).; Los códigos y nombres del centro de atención se entregan sin espacios laterales (RTRIM/LTRIM sobre CODCENATE y NOMCENATE).; El campo Description concatena código y nombre de la configuración con código y nombre del centro de atención.; Solo se exponen combinaciones que cumplen los tres INNER JOIN: configuración con centro válido en ADCENATEN y con usuario asignado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezcla (Mixing Station); Centro de atención; Configuración de estación de mezcla; Usuario autorizado de configuración', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListCM: Devuelve una fila por cada combinación (CMConfiguration, CMCenterAttention, CMConfigurationUsers) generando un Id compuesto como CONCAT(cm.Id,''-'',cmca.Id,''-'',cmu.Id).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMConfiguration; MixingStation.CMCenterAttention; ADCENATEN; MixingStation.CMConfigurationUsers', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCM';
GO
