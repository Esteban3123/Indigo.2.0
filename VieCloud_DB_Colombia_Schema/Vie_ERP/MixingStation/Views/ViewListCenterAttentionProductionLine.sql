CREATE view [MixingStation].[ViewListCenterAttentionProductionLine] 
as select c.CODCENATE, c.CODCENATE as item2, c.NOMCENATE, c.DEPMUNCOD, i.MUNNOMBRE, a.IdProductionLine, p.Name, a.IdMixingStation
from dbo.ADCENATEN c, dbo.INMUNICIP i, MixingStation.CMCenterAttention a, MixingStation.ProductionLine p
where c.CODCENATE = a.IdCenterAttention
and p.Id = a.IdProductionLine
and c.DEPMUNCOD = i.DEPMUNCOD
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los centros de atención (sedes, clínicas, hospitales) que tienen asignada una línea de producción dentro de una estación de mezcla (MixingStation). Integra la información del catálogo de centros de atención con el municipio donde se ubica cada sede, y la relaciona con la línea de producción y la estación de mezcla configuradas para ese centro. Se usa para reportería y configuración operativa que requiere saber qué sede pertenece a qué línea de producción y estación de mezcla, mostrando el código y nombre del centro de atención, el municipio, la línea de producción y el identificador de la estación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCenterAttentionProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCenterAttentionProductionLine';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los centros de atención junto con su municipio, la línea de producción asignada y la estación de mezcla vinculada, para selección/visualización en el módulo de MixingStation.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada centro de atención en CMCenterAttention debe existir en dbo.ADCENATEN (IdCenterAttention = CODCENATE).; Cada centro debe tener un municipio válido en dbo.INMUNICIP coincidente por DEPMUNCOD.; Cada registro de CMCenterAttention debe referenciar una ProductionLine existente (IdProductionLine = ProductionLine.Id).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen centros de atención que están asociados a al menos una línea de producción en CMCenterAttention (los centros sin asignación quedan excluidos).; Solo se exponen centros cuyo DEPMUNCOD existe en INMUNICIP; los que no tienen municipio válido se excluyen.; CODCENATE se proyecta dos veces (como CODCENATE e item2), garantizando un identificador duplicado para consumo en UI.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'centro de atención; municipio; línea de producción; estación de mezcla', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListCenterAttentionProductionLine: Devuelve una fila por cada combinación centro-línea de producción registrada en CMCenterAttention cuyas claves resuelven contra ADCENATEN, INMUNICIP y ProductionLine (INNER JOIN implícito vía cláusula WHERE).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCENATEN; dbo.INMUNICIP; MixingStation.CMCenterAttention; MixingStation.ProductionLine', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCenterAttentionProductionLine';
GO
