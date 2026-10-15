
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