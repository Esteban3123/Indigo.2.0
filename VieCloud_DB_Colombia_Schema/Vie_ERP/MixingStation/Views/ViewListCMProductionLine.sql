

CREATE view [MixingStation].[ViewListCMProductionLine] 
as 

select CONCAT(cm.Id, '-', cmPl.Id, '-', cmU.Id) Id,
cm.Id CMConfigurationId, 
cm.Code + ' - ' + cm.Name CMConfigurationCodeName, 
pl.Id ProductionLineId,
pl.Code + ' - ' + pl.Name ProductionLineCodeName, 
cmU.UserCode,
'CM: ' + cm.Code + ' - ' + cm.Name + ' LP: ' + pl.Code + ' - ' + pl.Name CodeName
from MixingStation.CMConfiguration cm
INNER JOIN MixingStation.CMMixingProducitonLine cmPl on cmPl.Id_CMConfiguration = cm.Id
INNER JOIN MixingStation.ProductionLine pl on pl.Id = cmPl.Id_ProductionLine
INNER JOIN MixingStation.CMConfigurationUsers cmU on cmU.CMConfigurationId = cm.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada que relaciona las configuraciones de mezclado (CM) con sus líneas de producción asignadas y los usuarios autorizados para cada configuración. Combina la configuración de mezclado, la línea de producción y el usuario en un único registro identificable, generando etiquetas legibles con código y nombre tanto de la configuración CM como de la línea de producción. Se utiliza para consultar rápidamente qué líneas de producción están asociadas a cada configuración de estación de mezclado y qué usuarios tienen acceso a ellas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCMProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCMProductionLine';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista combinada de configuraciones de estación de mezcla con sus líneas de producción asociadas y los usuarios autorizados, presentando códigos y nombres concatenados para selección/visualización.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros relacionados en CMConfiguration, CMMixingProducitonLine, ProductionLine y CMConfigurationUsers (los INNER JOIN excluyen configuraciones sin línea o sin usuarios).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id de salida es siempre la concatenación de los IDs de configuración, línea-mezcla y usuario separados por ''-''.; Solo se incluyen configuraciones que tengan al menos una línea de producción vinculada (vía CMMixingProducitonLine) y al menos un usuario autorizado (vía CMConfigurationUsers).; Las etiquetas legibles se construyen como ''Code - Name'' para configuración y línea, y ''CM: ... LP: ...'' para la descripción combinada.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezcla (Mixing Station); Configuración de estación de mezcla; Línea de producción; Usuario autorizado de configuración', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada combinación (CMConfiguration × línea de producción asociada × usuario autorizado), con un Id compuesto CONCAT(cm.Id,''-'',cmPl.Id,''-'',cmU.Id).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMConfiguration; MixingStation.CMMixingProducitonLine; MixingStation.ProductionLine; MixingStation.CMConfigurationUsers', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCMProductionLine';
GO
