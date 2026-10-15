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