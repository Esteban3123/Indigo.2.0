CREATE FUNCTION [MixingStation].[GetExistingPackageDetail]
(
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
        SELECT	ppd.PackageId,
				ppd.ComponentType, ppd.PreparationType,
				ppd.ProductId, ppd.AtcId, ppd.SupplieId,
				ppd.MainMedicine, ppd.Vehicle, ppd.Thinner,
				ppd.Quantity, ppd.MeasurementUnitId,
				ppd.Volume, ppd.VolumeMeasureUnit, ppd.VolumeTotal,
				ppd.Concentration, ppd.Dilution, ppd.Osmolarity, ppd.Density,
				ppd.AmountTime, ppd.TimeUnit,
				ppd.NPTItemOrder,
				Count(1) TotalRows
		FROM MixingStation.PackageDetail ppd 
		GROUP BY ppd.PackageId,
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
    FROM MixingStation.Package pp
	WHERE pp.UnitDoseTypeId = @UnitDoseTypeId
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
			WHERE pp.Id = ppd.PackageId

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
			WHERE pp.Id = ppd.PackageId						
		)
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de la estación de mezclas que busca si ya existe un paquete (fórmula magistral o preparación farmacéutica) registrado en el sistema cuya composición de componentes sea exactamente igual a la lista de ingredientes que se le pasa como parámetro. Compara cada componente del detalle propuesto contra los detalles almacenados en MixingStation.PackageDetail, verificando que coincidan en tipo de componente, tipo de preparación, producto, código ATC, insumo, medicamento principal, vehículo, diluyente, cantidad, unidad de medida, volumen, concentración, dilución, osmolaridad, densidad y tiempo de administración, sin diferencias en ninguna dirección. Si encuentra un paquete del mismo tipo de dosis unitaria (UnitDoseTypeId) con composición idéntica, retorna su identificador y código, permitiendo reutilizar fórmulas ya existentes y evitar duplicados en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetExistingPackageDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetExistingPackageDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Localiza paquetes de mezcla farmacéutica ya existentes cuyo detalle de componentes coincide exactamente (en composición y multiplicidad) con el detalle recibido como parámetro, para un tipo de dosis unitaria dado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un UnitDoseTypeId válido que identifique el tipo de dosis unitaria a buscar.; El parámetro tabla @PackageDetails debe contener el conjunto de componentes del paquete a comparar, con la misma estructura (MixingStation.PackageDetailType) que MixingStation.PackageDetail.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La comparación entre el paquete existente y el detalle parametrizado se realiza agrupando por todas las columnas funcionales del detalle (componente, preparación, producto, ATC, insumo, medicamento principal, vehículo, diluyente, cantidad, unidad de medida, volumen, volumen total, concentración, dilución, osmolaridad, densidad, tiempo, unidad de tiempo y orden NPT) más el conteo de filas repetidas (TotalRows), por lo que dos paquetes solo se consideran iguales si coinciden exactamente en todas esas dimensiones y multiplicidad.; Un paquete se considera coincidente solo cuando la doble verificación con EXCEPT en ambos sentidos no devuelve filas, garantizando equivalencia bidireccional entre el detalle almacenado y el detalle recibido (igualdad de conjuntos multivaluados).; Solo se evalúan paquetes cuyo UnitDoseTypeId coincide con el parámetro recibido; paquetes de otros tipos de dosis unitaria nunca se consideran candidatos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paquete de mezcla; detalle de paquete; tipo de dosis unitaria; componente; preparación; medicamento principal; vehículo; diluyente; concentración; dilución; osmolaridad; densidad; NPT (nutrición parenteral total)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.Package: Devuelve Id y Code de los paquetes (MixingStation.Package) cuyo UnitDoseTypeId coincide con el parámetro y cuyo detalle agrupado en MixingStation.PackageDetail es exactamente equivalente al detalle agrupado del parámetro @PackageDetails (verificado mediante doble EXCEPT en ambas direcciones).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.Package; MixingStation.PackageDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExistingPackageDetail';
GO
