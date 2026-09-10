
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de liquidación que marca como NO FACTURABLES (valor cero) los servicios —tanto medicamentos/insumos como procedimientos CUPS— de un folio de control de ingresos específico, cuando dichos servicios cumplen las restricciones de facturación definidas en el contrato del grupo de atención del paciente. Opera sobre el detalle de órdenes de servicio y su distribución financiera, poniendo en cero precios, descuentos, impuestos, cuotas moderadoras, copagos y cobertura del asegurador. Se usa durante el proceso de liquidación de facturación para excluir automáticamente conceptos que, según las reglas contractuales (por unidad funcional, tipo de tarifa manual, grupo quirúrgico o rango UVR), no deben ser cobrados ni al tercero pagador ni al paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateServiceNotBillable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateServiceNotBillable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca como no facturables los servicios de un folio de control de ingresos que cumplen restricciones contractuales, poniendo en cero todos los valores económicos del detalle de la orden y de su distribución.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateServiceNotBillable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un RevenueControlDetail con el Id recibido y debe tener un CareGroupId asociado a una CareGroupBillingItemsRestriction.; Deben existir BillingItemsRestriction, BillingItemsRestrictionDetail y BillingItemsRestrictionDetailCondition configuradas para el grupo de atención del folio.; Los detalles de orden de servicio (ServiceOrderDetail) deben estar enlazados al folio vía ServiceOrderDetailDistribution.RevenueControlDetailId.; Para evaluación por producto: el ServiceOrderDetail debe tener ProductId que coincida (directo, por subgrupo o por grupo) con el detalle de restricción.; Para evaluación por CUPS: el ServiceOrderDetail debe tener IPSServiceId y CUPSEntityId que coincidan (entidad CUPS, subgrupo o grupo) con el detalle de restricción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateServiceNotBillable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se afectan registros pertenecientes al RevenueControlDetail recibido como parámetro (filtro rcd.Id = @RevenueControlDetailId en todas las sentencias).; Un servicio marcado como no facturable siempre queda con SettlementType=3 y todos sus valores monetarios (bruto, impuestos, descuentos, totales) en cero, tanto en el detalle como en su distribución.; La distribución de un servicio no facturable siempre queda con RecoveryFeeType=1 y ApplyRecoveryFee=1.; Las restricciones se evalúan sobre dos ejes independientes: por producto (vía InventoryProduct/ProductSubGroup/ProductGroup) y por CUPS (vía CUPSEntity/CupsSubgroup/CupsGroup); ambos ejes usan la misma lógica de doble condición unida por LogicalOperator.; La evaluación por rango UVR y por grupo quirúrgico solo aplica al eje CUPS, no al eje de productos de inventario.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateServiceNotBillable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.ServiceOrderDetail: Cuando el ítem (por ProductId/ProductSubGroupId/ProductGroupId) cumple las dos condiciones (ConditionType y ConditionType2 según LogicalOperator) de la restricción contractual del CareGroup del folio, se fija SettlementType=3 (no facturable) y se ponen en cero SubTotalSalesPrice, ThirdPartyDiscount, ThirdPartyDiscountPercentage, TotalSalesPrice, GrandTotalSalesPrice, GrossValue y TaxValue.; [UPDATE] Billing.ServiceOrderDetailDistribution: Cuando el ítem (por producto/subgrupo/grupo) cumple las condiciones de restricción, se ponen en cero GrandTotalSalesPrice, GrandTotalDiscount, ThirdPartySalesPrice, PatientPercentage, SubTotalPatientSalesPrice, SubTotalSalesPrice, GrandTotalTaxes, DeductibleValue, InsurerCoveredValue y se fijan RecoveryFeeType=1 y ApplyRecoveryFee=1.; [UPDATE] Billing.ServiceOrderDetail: Cuando el ítem (por CUPSEntityId/CUPSSubgroupId/CUPSGroupId) cumple las dos condiciones (incluye también ConditionType 6=grupo quirúrgico y 7=rango UVR del IPSService) de la restricción del CareGroup, se fija SettlementType=3 y se ponen en cero SubTotalSalesPrice, ThirdPartyDiscount, ThirdPartyDiscountPercentage, TotalSalesPrice, GrandTotalSalesPrice, GrossValue y TaxValue.; [UPDATE] Billing.ServiceOrderDetailDistribution: Cuando el ítem (por CUPS/subgrupo/grupo CUPS, incluyendo evaluación por grupo quirúrgico y rango UVR) cumple las condiciones, se ponen en cero los valores económicos de la distribución y se fijan RecoveryFeeType=1 y ApplyRecoveryFee=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateServiceNotBillable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConditionType = 2 → La condición se cumple si con.FunctionalUnitId coincide con la unidad funcional que ejecuta (sod.PerformsFunctionalUnitId).; si ConditionType = 3 → La condición se cumple si con.UnitTypeId coincide con fu.UnitType (tipo de unidad funcional).; si ConditionType = 5 → La condición se cumple si con.ManualType coincide con sod.RateManualType (tipo de manual tarifario).; si ConditionType = 6 (solo evaluación CUPS) → La condición se cumple si con.SurgicalGroupId coincide con ser.SurgicalGroupId del IPSService.; si ConditionType = 7 (solo evaluación CUPS) → La condición se cumple si ser.UVRNumber está entre InitialUVR y EndUVR del UVRRange referenciado por con.UVRRangeId.; si rd.LogicalOperator = 1 → Se omite la evaluación de la segunda condición (se considera satisfecha automáticamente). else Se evalúa la segunda condición usando ConditionType2 con la misma lógica (FunctionalUnitId2/UnitTypeId2/ManualType2/SurgicalGroupId2/UVRRangeId2).; si ConditionType (o ConditionType2) distinto a los valores manejados → La condición se considera satisfecha por defecto (else 1).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateServiceNotBillable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateServiceNotBillable';
-- GO
