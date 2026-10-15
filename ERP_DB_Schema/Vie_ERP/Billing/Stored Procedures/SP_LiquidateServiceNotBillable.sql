
-- =============================================
-- Author:      Cristhian Salazar
-- Create Date: 04/01/2024
-- Description: SP para incluir y cambiar valor a cero servicios no facturables
-- =============================================
CREATE PROCEDURE [Billing].[SP_LiquidateServiceNotBillable]
(
    @RevenueControlDetailId int
)
AS
BEGIN
    SET NOCOUNT ON

	update sod set SettlementType = 3, SubTotalSalesPrice = 0, ThirdPartyDiscount = 0, ThirdPartyDiscountPercentage = 0, TotalSalesPrice = 0, GrandTotalSalesPrice = 0,GrossValue=0,TaxValue=0
	from Billing.RevenueControlDetail rcd
	inner join Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
	inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
	inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
	inner join [Contract].[CareGroupBillingItemsRestriction] ci on ci.CareGroupId = rcd.CareGroupId
	inner join Inventory.InventoryProduct pro on pro.Id = sod.ProductId
	inner join Inventory.ProductSubGroup ps on ps.Id = pro.ProductSubGroupId
	inner join Inventory.ProductGroup pg on pg.Id = pro.ProductGroupId
	inner join [Contract].[BillingItemsRestriction] br on br.Id = ci.BillingItemsRestrictionId
	inner join [Contract].BillingItemsRestrictionDetail rd 
		on rd.BillingItemsRestrictionId = br.Id and (rd.ProductId = pro.Id or rd.ProductSubGroupId = ps.Id or rd.ProductGroupId = pg.Id)
	inner join [Contract].BillingItemsRestrictionDetailCondition con on con.BillingItemsRestrictionDetailId = rd.Id
	where rcd.Id = @RevenueControlDetailId and 
		case 
			when ConditionType = 2 then iif(con.FunctionalUnitId = fu.Id, 1, 0)
			when ConditionType = 3 then iif(con.UnitTypeId = fu.UnitType, 1, 0)
			when ConditionType = 5 then iif(con.ManualType = sod.RateManualType, 1, 0)
			else 1
		end = 1
		and case 
			when rd.LogicalOperator = 1 then 1
			when ConditionType2 = 2 then iif(con.FunctionalUnitId2 = fu.Id, 1, 0)
			when ConditionType2 = 3 then iif(con.UnitTypeId2 = fu.UnitType, 1, 0)
			when ConditionType2 = 5 then iif(con.ManualType2 = sod.RateManualType, 1, 0)
			else 1
		end = 1

	update sodd set GrandTotalSalesPrice = 0, GrandTotalDiscount = 0, ThirdPartySalesPrice = 0,
		PatientPercentage = 0, SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, ApplyRecoveryFee = 1	, SubTotalSalesPrice =0,GrandTotalTaxes=0,DeductibleValue=0,InsurerCoveredValue=0	
	from Billing.RevenueControlDetail rcd
	inner join Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
	inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
	inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
	inner join [Contract].[CareGroupBillingItemsRestriction] ci on ci.CareGroupId = rcd.CareGroupId
	inner join Inventory.InventoryProduct pro on pro.Id = sod.ProductId
	inner join Inventory.ProductSubGroup ps on ps.Id = pro.ProductSubGroupId
	inner join Inventory.ProductGroup pg on pg.Id = pro.ProductGroupId
	inner join [Contract].[BillingItemsRestriction] br on br.Id = ci.BillingItemsRestrictionId
	inner join [Contract].BillingItemsRestrictionDetail rd 
		on rd.BillingItemsRestrictionId = br.Id and (rd.ProductId = pro.Id or rd.ProductSubGroupId = ps.Id or rd.ProductGroupId = pg.Id)
	inner join [Contract].BillingItemsRestrictionDetailCondition con on con.BillingItemsRestrictionDetailId = rd.Id
	where rcd.Id = @RevenueControlDetailId and 
		case 
			when ConditionType = 2 then iif(con.FunctionalUnitId = fu.Id, 1, 0)
			when ConditionType = 3 then iif(con.UnitTypeId = fu.UnitType, 1, 0)
			when ConditionType = 5 then iif(con.ManualType = sod.RateManualType, 1, 0)
			else 1
		end = 1
		and case 
			when rd.LogicalOperator = 1 then 1
			when ConditionType2 = 2 then iif(con.FunctionalUnitId2 = fu.Id, 1, 0)
			when ConditionType2 = 3 then iif(con.UnitTypeId2 = fu.UnitType, 1, 0)
			when ConditionType2 = 5 then iif(con.ManualType2 = sod.RateManualType, 1, 0)
			else 1
		end = 1


	update sod set SettlementType = 3, SubTotalSalesPrice = 0, ThirdPartyDiscount = 0, ThirdPartyDiscountPercentage = 0, TotalSalesPrice = 0, GrandTotalSalesPrice = 0,GrossValue=0,TaxValue=0
	from Billing.RevenueControlDetail rcd
	inner join Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
	inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
	inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
	inner join [Contract].[CareGroupBillingItemsRestriction] ci on ci.CareGroupId = rcd.CareGroupId
	inner join [Contract].IPSService ser on ser.Id = sod.IPSServiceId
	inner join [Contract].CUPSEntity ce on ce.Id = sod.CUPSEntityId
	inner join [Contract].CupsSubgroup csub on csub.Id = ce.CUPSSubGroupId
	inner join [Contract].[BillingItemsRestriction] br on br.Id = ci.BillingItemsRestrictionId
	inner join [Contract].BillingItemsRestrictionDetail rd 
		on rd.BillingItemsRestrictionId = br.Id and (rd.CUPSEntityId = ce.Id or rd.CUPSGroupId = csub.CupsGroupId or rd.CUPSSubgroupId = csub.Id)
	inner join [Contract].BillingItemsRestrictionDetailCondition con on con.BillingItemsRestrictionDetailId = rd.Id
	left join [Contract].UVRRange ur on ur.Id = con.UVRRangeId
	left join [Contract].UVRRange ur2 on ur2.Id = con.UVRRangeId2
	where rcd.Id = @RevenueControlDetailId and 
		case 
			when ConditionType = 2 then iif(con.FunctionalUnitId = fu.Id, 1, 0)
			when ConditionType = 3 then iif(con.UnitTypeId = fu.UnitType, 1, 0)
			when ConditionType = 5 then iif(con.ManualType = sod.RateManualType, 1, 0)
			when ConditionType = 6 then iif(con.SurgicalGroupId = ser.SurgicalGroupId, 1, 0)
			when ConditionType = 7 then iif(ser.UVRNumber between ur.InitialUVR and ur.EndUVR, 1, 0)
			else 1
		end = 1
		and case 
			when rd.LogicalOperator = 1 then 1
			when ConditionType2 = 2 then iif(con.FunctionalUnitId2 = fu.Id, 1, 0)
			when ConditionType2 = 3 then iif(con.UnitTypeId2 = fu.UnitType, 1, 0)
			when ConditionType2 = 5 then iif(con.ManualType2 = sod.RateManualType, 1, 0)
			when ConditionType2 = 6 then iif(con.SurgicalGroupId2 = ser.SurgicalGroupId, 1, 0)
			when ConditionType2 = 7 then iif(ser.UVRNumber between ur2.InitialUVR and ur2.EndUVR, 1, 0)
			else 1
		end = 1

	update sodd set GrandTotalSalesPrice = 0, GrandTotalDiscount = 0, ThirdPartySalesPrice = 0,
		PatientPercentage = 0, SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, ApplyRecoveryFee = 1	, SubTotalSalesPrice =0,GrandTotalTaxes=0,DeductibleValue=0,InsurerCoveredValue=0	
	from Billing.RevenueControlDetail rcd
	inner join Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
	inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
	inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
	inner join [Contract].[CareGroupBillingItemsRestriction] ci on ci.CareGroupId = rcd.CareGroupId
	inner join [Contract].IPSService ser on ser.Id = sod.IPSServiceId
	inner join [Contract].CUPSEntity ce on ce.Id = sod.CUPSEntityId
	inner join [Contract].CupsSubgroup csub on csub.Id = ce.CUPSSubGroupId
	inner join [Contract].[BillingItemsRestriction] br on br.Id = ci.BillingItemsRestrictionId
	inner join [Contract].BillingItemsRestrictionDetail rd 
		on rd.BillingItemsRestrictionId = br.Id and (rd.CUPSEntityId = ce.Id or rd.CUPSGroupId = csub.CupsGroupId or rd.CUPSSubgroupId = csub.Id)
	inner join [Contract].BillingItemsRestrictionDetailCondition con on con.BillingItemsRestrictionDetailId = rd.Id
	left join [Contract].UVRRange ur on ur.Id = con.UVRRangeId
	left join [Contract].UVRRange ur2 on ur2.Id = con.UVRRangeId2
	where rcd.Id = @RevenueControlDetailId and 
		case 
			when ConditionType = 2 then iif(con.FunctionalUnitId = fu.Id, 1, 0)
			when ConditionType = 3 then iif(con.UnitTypeId = fu.UnitType, 1, 0)
			when ConditionType = 5 then iif(con.ManualType = sod.RateManualType, 1, 0)
			when ConditionType = 6 then iif(con.SurgicalGroupId = ser.SurgicalGroupId, 1, 0)
			when ConditionType = 7 then iif(ser.UVRNumber between ur.InitialUVR and ur.EndUVR, 1, 0)
			else 1
		end = 1
		and case 
			when rd.LogicalOperator = 1 then 1
			when ConditionType2 = 2 then iif(con.FunctionalUnitId2 = fu.Id, 1, 0)
			when ConditionType2 = 3 then iif(con.UnitTypeId2 = fu.UnitType, 1, 0)
			when ConditionType2 = 5 then iif(con.ManualType2 = sod.RateManualType, 1, 0)
			when ConditionType2 = 6 then iif(con.SurgicalGroupId2 = ser.SurgicalGroupId, 1, 0)
			when ConditionType2 = 7 then iif(ser.UVRNumber between ur2.InitialUVR and ur2.EndUVR, 1, 0)
			else 1
		end = 1
END