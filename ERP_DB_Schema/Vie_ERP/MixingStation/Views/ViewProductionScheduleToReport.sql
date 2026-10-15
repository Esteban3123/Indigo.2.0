CREATE view [MixingStation].[ViewProductionScheduleToReport]
As

with CampaignValidation as (
	select cdv.Id
		, pr.ATCId
		, pr.SupplieId
		, cdv.CampaignDetailId
		, cdv.DeliveredQuantity
		, cdv.DevolutionQuantity
		, bs.BatchCode
	from MixingStation.CampaignDetailValidation cdv
	inner join Inventory.InventoryProduct pr on cdv.ProductId = pr.Id
	inner join Inventory.BatchSerial bs on cdv.BatchSerialId = bs.Id
)
SELECT 
	cdi.Id
	, cd.Id as CampaignDetailId
	, ISNULL([ip].ATCId, cdi.AtcId) as AtcId
	, ISNULL([ip].SupplieId, cdi.SupplyId) as SupplyId
	, cdi.ProductId as ProductId--_Eliminar
	, cdi.ItemType
	, atc.Concentration
	,	case 
			when cdi.AtcId is not null then concat(atc.Code, ' - ', atc.Name)
			when cdi.SupplyId is not null then concat(su.Code, ' - ', su.SupplieName)

			when patc.Id is not null then concat(patc.Code, ' - ', patc.Name)
			when psu.Id is not null then concat(psu.Code, ' - ', psu.SupplieName)

			else concat([ip].Code, ' - ', [ip].Name)
		end as ItemCodeName
	,	STUFF((
			SELECT ', ' + [BatchCode]
			FROM CampaignValidation
			WHERE CampaignDetailId = cd.Id And 
				(ATCId = cdi.AtcId or ATCId = ip.ATCId or SupplieId = cdi.SupplyId or SupplieId = ip.SupplieId)
			FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
		  ,1,2,'') AS BatchCodes
	, Count(cv.BatchCode) as CantidadLotes -- 
	, cdi.RequestQuantity as RequestQuantities
	, Sum(cv.DeliveredQuantity) as DispensingQuantities
	, 0 as IsCold
	, 1 as IsEnvironment
	, 0 as AditionalQuantities
	, sum(rmdd.Quantity) as DevolutionQuantities
	, ps.CreationDate as ProductionDate
	, ps.Code as ProductionScheduleCode
	, udt.Description as UnitDoseTypeName
	, ps.CreationUser as UserCode
FROM MixingStation.ProductionSchedule ps
inner join MixingStation.ProductionScheduleDetail psd on psd.ProductionScheduleId = ps.Id
inner join MixingStation.CampaignDetail cd on psd.CampaignDetailId = cd.Id
inner join MixingStation.CampaignDetailItems cdi on cdi.CampaignDetailId = cd.Id
inner join MixingStation.UnitDoseType udt on cd.UnitDoseTypeId = udt.Id
left join Inventory.ATC atc on cdi.AtcId = atc.Id
left join Inventory.InventorySupplie su on cdi.SupplyId = su.Id
left join Inventory.InventoryProduct [ip] on cdi.ProductId = [ip].Id

left join Inventory.ATC patc on ip.ATCId = patc.Id
left join Inventory.InventorySupplie psu on ip.SupplieId = psu.Id

left join CampaignValidation cv on cv.CampaignDetailId = cd.Id And 
	(cv.ATCId = cdi.AtcId or cv.ATCId = ip.ATCId or cv.SupplieId = cdi.SupplyId or cv.SupplieId = ip.SupplieId)
left join MixingStation.RawMaterialDevolutionDetail rmdd on rmdd.CampaignDetailValidationId = cv.Id
where psd.CampaignDetailId = 241
group by cdi.Id
	, cd.Id
	, ip.ATCId
	, cdi.AtcId
	, ip.SupplieId
	, cdi.SupplyId
	, cdi.ProductId
	, cdi.ItemType
	, atc.Concentration
	, atc.Code, atc.Name
	, su.Code, su.SupplieName
	, ip.Code, ip.Name
	, ps.CreationUser
	, ps.Code
	, udt.Description
	, ps.CreationDate
	, cdi.RequestQuantity
	, patc.Code
	, patc.Name
	, psu.Code
	, psu.SupplieName
	, psu.Id
	, patc.Id