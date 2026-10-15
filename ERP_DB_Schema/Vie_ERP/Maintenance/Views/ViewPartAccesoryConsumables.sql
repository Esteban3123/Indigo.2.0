

CREATE VIEW [Maintenance].[ViewPartAccesoryConsumables]
as
(

	--Select p.Id, Code, [Name], 1 As TypeDetail, 'Parte' As TypeName, pp.PhysicalAssetId As PhysicalAssetId
	--From FixedAsset.FixedAssetPartsAccesoriesConsumables p With(Nolock)
	--inner join [FixedAsset].[FixedAssetPhysicalAssetParts] pp on pp.PartAccesoriesConsumiblesId = p.Id


	--Union All

	Select Concat(Id, 2) As Id, Id As DocumentId, Code, [Name], 2 As TypeDetail, 'ACCESORIOS' As TypeName, 0 As PhysicalAssetId
	From Maintenance.Accessory With(Nolock)

	Union All

	Select Concat(Id, 3) As Id,  Id As DocumentId, Code, [Name], 3 As TypeDetail, 'CONSUMIBLES' As TypeName, 0 As PhysicalAssetId
	From Maintenance.Consumable With(Nolock)
)