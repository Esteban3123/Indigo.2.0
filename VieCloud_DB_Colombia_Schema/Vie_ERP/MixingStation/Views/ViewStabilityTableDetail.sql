
CREATE VIEW [MixingStation].[ViewStabilityTableDetail] 
AS
SELECT 
	CONCAT(st.Id, '-', std.Id, '-', stdr.Id, '-', stdd.Id) Id,
	st.Id StabilityTableId,
	std.ATCId AtcIdMainMedicine, --
	std.ProductId ProductIdMainMedicine,  --
	std.UnitDoseTypeId,
	std.AllowableDoses,
	std.HourStabilityProduct StabilityMainMedicine,
	stdr.ATCId AtcIdReconstituent,
	stdr.HourStability StabilityReconstituent,
	stdd.ATCId AtcIdVehicle,
	stdd.HourStability StabilityVehicle
FROM MixingStation.StabilityTable st WITH(NOLOCK)
JOIN MixingStation.StabilityTableDetail std WITH(NOLOCK) ON st.Id = std.StabilityTableId
LEFT JOIN MixingStation.StabilityTableDetailReconstitution stdr WITH(NOLOCK) ON std.Id = stdr.StabilityTableDetailId
LEFT JOIN MixingStation.StabilityTableDetailDilution stdd WITH(NOLOCK) ON std.Id = stdd.StabilityTableDetailId
WHERE st.Status = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo de las tablas de estabilidad de medicamentos en la estación de mezclas, combinando el medicamento principal con su reconstituyente y su vehículo de dilución. Integra las tablas de encabezado de estabilidad, el detalle del producto principal (código ATC, producto, tipo de dosis unitaria, dosis permitidas y horas de estabilidad), las horas de estabilidad del reconstituyente y las horas de estabilidad del vehículo o diluyente. Solo expone registros de tablas de estabilidad activas (estado vigente). Se utiliza para consultar las condiciones de estabilidad físico-química que deben respetarse al preparar mezclas intravenosas o medicamentos reconstituidos, apoyando el control de calidad y seguridad en farmacia de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewStabilityTableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewStabilityTableDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, de manera consolidada, el detalle de estabilidad de medicamentos en la estación de mezclas, combinando el medicamento principal con sus datos de reconstitución y dilución, para tablas de estabilidad activas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en StabilityTable con Status = 1 (vigentes/activos).; Cada StabilityTableDetail debe estar asociado a una StabilityTable existente (JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles cuya tabla de estabilidad maestra está activa (Status = 1).; Reconstituyente y vehículo son opcionales: el detalle del medicamento principal siempre aparece, aunque no tenga reconstitución ni dilución asociada.; El Id sintético resultante combina las claves de las cuatro entidades, garantizando trazabilidad por fila al origen.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'tabla de estabilidad; medicamento principal; reconstituyente; vehículo de dilución; ATC (clasificación de medicamento); dosis unitaria; horas de estabilidad; estación de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve una fila por cada combinación de tabla de estabilidad activa, su detalle, y opcionalmente su reconstitución y dilución; el Id se construye concatenando los Ids de las cuatro entidades (st-std-stdr-stdd).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si st.Status = 1 → Incluye la tabla de estabilidad y su detalle en el resultado else Se excluye por completo del resultado (no se exponen tablas inactivas); si Existe StabilityTableDetailReconstitution para el detalle → Se reportan ATCId y horas de estabilidad del reconstituyente else Las columnas de reconstituyente quedan en NULL (LEFT JOIN); si Existe StabilityTableDetailDilution para el detalle → Se reportan ATCId y horas de estabilidad del vehículo de dilución else Las columnas de vehículo quedan en NULL (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.StabilityTable; MixingStation.StabilityTableDetail; MixingStation.StabilityTableDetailReconstitution; MixingStation.StabilityTableDetailDilution', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStabilityTableDetail';
GO
