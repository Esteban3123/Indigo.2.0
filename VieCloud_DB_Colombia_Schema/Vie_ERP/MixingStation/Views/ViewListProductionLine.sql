

CREATE view [MixingStation].[ViewListProductionLine] 
as 

select CONCAT(1, '-', cm.Id, '-', ca.Id, '-', pludt.Id) Id,
cm.Id CMConfigurationId, ca.CodeCenterAttention CareCenterCode, 
pl.Id ProductionLineId, pl.Code ProductionLineCode, pl.Name ProductionLineName, pl.Code + ' - ' + pl.Name ProductionLineCodeName,
pludt.Id_UnitDoseType UnitDoseTypeId, 1 SourceType
from MixingStation.CMConfiguration cm
inner join MixingStation.CMCenterAttention ca on ca.IdMixingStation = cm.Id
inner join MixingStation.ProductionLine pl on pl.Id = ca.IdProductionLine
inner join MixingStation.ProductionLineUnitDoseType pludt on pludt.Id_ProductionLine = pl.Id

union all

select CONCAT(2, '-', cm.Id, '-', ca.Id, '-', pludt.Id) Id,
cm.Id CMConfigurationId, ecc.Code CareCenterCode, 
pl.Id ProductionLineId, pl.Code ProductionLineCode, pl.Name ProductionLineName, pl.Code + ' - ' + pl.Name ProductionLineCodeName,
pludt.Id_UnitDoseType UnitDoseTypeId, 2 SourceType
from MixingStation.CMConfiguration cm
inner join MixingStation.CMExternalCareCenter ca on ca.CMConfigurationId = cm.Id
inner join MixingStation.ExternalCareCenter ecc on ecc.Id = ca.ExternalCareCenterId
inner join MixingStation.ProductionLine pl on pl.Id = ca.ProductionLineId
inner join MixingStation.ProductionLineUnitDoseType pludt on pludt.Id_ProductionLine = pl.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de líneas de producción farmacéutica disponibles en la estación de mezcla, combinando dos orígenes: centros de atención internos (configurados en CMCenterAttention) y centros de atención externos (registrados en ExternalCareCenter). Para cada combinación muestra la configuración de mezcla, el código y nombre del centro de atención, el código y nombre de la línea de producción, y el tipo de dosis unitaria que puede procesar esa línea. Se utiliza para poblar selectores o reportes que necesiten conocer qué líneas de producción y tipos de dosis unitaria están habilitados por centro de atención, ya sea interno o externo, dentro de una configuración de estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListProductionLine';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en una sola lista las líneas de producción habilitadas por estación de mezcla, combinando los centros de atención internos y los centros de atención externos, junto con los tipos de dosis unitaria que cada línea procesa.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CMCenterAttention debe referenciar una estación de mezcla (IdMixingStation) y una línea de producción (IdProductionLine) existentes.; Cada CMExternalCareCenter debe referenciar una CMConfiguration, un ExternalCareCenter y una ProductionLine existentes.; Cada línea de producción debe tener al menos un registro en ProductionLineUnitDoseType para aparecer en el resultado (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen líneas de producción que tengan al menos un tipo de dosis unitaria asociado (INNER JOIN con ProductionLineUnitDoseType).; Cada fila representa la combinación única estación de mezcla + centro de atención (interno o externo) + línea de producción + tipo de dosis unitaria.; El campo SourceType discrimina inequívocamente el origen del centro de atención: 1=interno, 2=externo.; El CareCenterCode siempre corresponde al código del centro de atención asociado, sea interno (CodeCenterAttention) o externo (ExternalCareCenter.Code).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezcla; Línea de producción; Centro de atención; Centro de atención externo; Tipo de dosis unitaria; Configuración de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListProductionLine: Devuelve filas con SourceType=1 cuando provienen de CMCenterAttention (centros de atención internos) y SourceType=2 cuando provienen de CMExternalCareCenter (centros de atención externos).; [RETURN_RESULT] MixingStation.ViewListProductionLine: El Id de cada fila se construye como CONCAT(SourceType,''-'',CMConfigurationId,''-'',CareCenterId,''-'',ProductionLineUnitDoseTypeId), garantizando unicidad entre las dos fuentes.; [RETURN_RESULT] MixingStation.ViewListProductionLine: ProductionLineCodeName se compone como Code + '' - '' + Name de la línea de producción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = CMCenterAttention (centro de atención interno) → SourceType=1 y CareCenterCode = ca.CodeCenterAttention; si Origen = CMExternalCareCenter (centro de atención externo) → SourceType=2 y CareCenterCode = ecc.Code (código del ExternalCareCenter)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMConfiguration; MixingStation.CMCenterAttention; MixingStation.ProductionLine; MixingStation.ProductionLineUnitDoseType; MixingStation.CMExternalCareCenter; MixingStation.ExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListProductionLine';
GO
