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