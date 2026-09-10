CREATE FUNCTION [MixingStation].[GetExistingPackagePersonalizedDetail]
(
    @AssociatedPackageId INT,
    @UnitDoseTypeId INT,
    @PackageDetails MixingStation.PackageDetailType READONLY
)
RETURNS TABLE
AS
RETURN
(
    WITH ParamGrouped AS (
        SELECT	ppd.ComponentType, ppd.PreparationType,
				ppd.ProductId, ppd.AtcId, ppd.SupplieId,
				ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
				ppd.Quantity, ppd.MeasurementUnitId,
				ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
				ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
				ppd.AmountTime, ppd.TimeUnit,
				ppd.NPTItemOrder,
				Count(1) TotalRows
		FROM @PackageDetails ppd
		GROUP BY ppd.ComponentType, ppd.PreparationType,
			ppd.ProductId, ppd.AtcId, ppd.SupplieId,
			ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
			ppd.Quantity, ppd.MeasurementUnitId,
			ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
			ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
			ppd.AmountTime, ppd.TimeUnit,
			ppd.NPTItemOrder
    ), 
	DetailGrouped AS (
        SELECT	ppd.PackagePersonalizedId,
				ppd.ComponentType, ppd.PreparationType,
				ppd.ProductId, ppd.AtcId, ppd.SupplieId,
				ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
				ppd.Quantity, ppd.MeasurementUnitId,
				ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
				ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
				ppd.AmountTime, ppd.TimeUnit,
				ppd.NPTItemOrder,
				Count(1) TotalRows
		FROM MixingStation.PackagePersonalizedDetail ppd 
		GROUP BY ppd.PackagePersonalizedId,
			ppd.ComponentType, ppd.PreparationType,
			ppd.ProductId, ppd.AtcId, ppd.SupplieId,
			ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
			ppd.Quantity, ppd.MeasurementUnitId,
			ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
			ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
			ppd.AmountTime, ppd.TimeUnit,
			ppd.NPTItemOrder
    )

	------------------------------------------------------------------

    SELECT	pp.Id, pp.Code
    FROM MixingStation.PackagePersonalized pp
	WHERE pp.AssociatedPackageId = @AssociatedPackageId
		AND pp.UnitDoseTypeId = @UnitDoseTypeId
		AND NOT EXISTS (
			SELECT	ppd.ComponentType, ppd.PreparationType,
					ppd.ProductId, ppd.AtcId, ppd.SupplieId,
					ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
					ppd.Quantity, ppd.MeasurementUnitId,
					ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
					ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
					ppd.AmountTime, ppd.TimeUnit,
					ppd.NPTItemOrder,
					ppd.TotalRows
			FROM DetailGrouped ppd 
			WHERE pp.Id = ppd.PackagePersonalizedId

			EXCEPT

			SELECT	ppd.ComponentType, ppd.PreparationType,
					ppd.ProductId, ppd.AtcId, ppd.SupplieId,
					ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
					ppd.Quantity, ppd.MeasurementUnitId,
					ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
					ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
					ppd.AmountTime, ppd.TimeUnit,
					ppd.NPTItemOrder,
					ppd.TotalRows
			FROM ParamGrouped ppd
		)
		AND NOT EXISTS (
			SELECT	ppd.ComponentType, ppd.PreparationType,
					ppd.ProductId, ppd.AtcId, ppd.SupplieId,
					ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
					ppd.Quantity, ppd.MeasurementUnitId,
					ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
					ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
					ppd.AmountTime, ppd.TimeUnit,
					ppd.NPTItemOrder,
					ppd.TotalRows
			FROM ParamGrouped ppd

			EXCEPT

			SELECT	ppd.ComponentType, ppd.PreparationType,
					ppd.ProductId, ppd.AtcId, ppd.SupplieId,
					ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
					ppd.Quantity, ppd.MeasurementUnitId,
					ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
					ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
					ppd.AmountTime, ppd.TimeUnit,
					ppd.NPTItemOrder,
					ppd.TotalRows
			FROM DetailGrouped ppd 
			WHERE pp.Id = ppd.PackagePersonalizedId						
		)
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que verifica si ya existe un paquete personalizado de mezclas (preparaciones farmacéuticas en estación de mezclas) con exactamente los mismos componentes que se están intentando registrar. Recibe un paquete asociado, un tipo de dosis unitaria y una lista de detalle de componentes propuesta, y devuelve el identificador y código del paquete personalizado existente cuyo detalle sea idéntico (mismos productos, vehículos, diluyentes, concentraciones, volúmenes, osmolaridad, densidad, tiempos y unidades de medida, sin diferencias en más ni en menos). Se utiliza para evitar duplicar fórmulas magistrales o preparaciones NPT (nutrición parenteral total) personalizadas que ya fueron configuradas previamente, garantizando unicidad en la estación de mezclas farmacéuticas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetExistingPackagePersonalizedDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetExistingPackagePersonalizedDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Localiza paquetes personalizados existentes cuya composición de detalle coincide exactamente (mismas filas y multiplicidades) con el conjunto de componentes recibido como parámetro, para un paquete asociado y tipo de unidosis dado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de tabla MixingStation.PackageDetailType debe estar definido y poblado con los componentes a comparar.; Deben existir registros en MixingStation.PackagePersonalized filtrables por AssociatedPackageId y UnitDoseTypeId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La comparación se realiza sobre composición agrupada (ComponentType, PreparationType, ProductId, AtcId, SupplieId, MainMedicine, Vehicle, Thinner, Quantity, MeasurementUnitId, Volume, VolumeMeasureUnit, VolumeTotal, Concentration, Dilution, Osmolarity, Density, AmountTime, TimeUnit, NPTItemOrder) más el conteo TotalRows, garantizando equivalencia bidireccional de multiconjuntos.; Solo se consideran paquetes personalizados ligados al mismo AssociatedPackageId y UnitDoseTypeId que los parámetros.; La equivalencia exige doble EXCEPT vacío: ningún componente sobra ni falta respecto al parámetro.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paquete personalizado de mezclas; Detalle de componentes (medicamento principal, vehículo, diluyente); Tipo de unidosis; Preparación magistral / NPT (NPTItemOrder); Concentración, dilución, osmolaridad, densidad; Volumen y unidad de medida', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.PackagePersonalized: Devuelve Id y Code de los paquetes personalizados cuyo AssociatedPackageId y UnitDoseTypeId coinciden con los parámetros y cuyo conjunto agrupado de detalle es idéntico (en columnas de composición y conteo de filas TotalRows) al conjunto agrupado de @PackageDetails.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS (DetailGrouped EXCEPT ParamGrouped) AND NOT EXISTS (ParamGrouped EXCEPT DetailGrouped) para un PackagePersonalized.Id → El paquete se incluye en el resultado por considerarse equivalente en composición. else El paquete se descarta porque su detalle difiere del conjunto recibido.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.PackagePersonalized; MixingStation.PackagePersonalizedDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackagePersonalizedDetail';
GO
