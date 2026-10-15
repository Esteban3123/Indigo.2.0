


CREATE VIEW [Inventory].[ViewValidateQuantityPurchaseOrderDetail]
AS



select row_number() over (order by tempEV.PurchaseOrderDetailId asc) Id, 
	   tempEV.PurchaseOrderId, 
	   tempEV.PurchaseOrderDetailId, 
	   sum(tempEV.QuantityRegister + tempRE.QuantityRegister) QuantityRegister,
	   sum(tempEV.QuantityConfirmed + tempRE.QuantityConfirmed) QuantityConfirmed
	   from 
	   (
	   		select pod.PurchaseOrderId, pod.Id PurchaseOrderDetailId, 
	   		isnull(evd.QuantityRegister, 0) QuantityRegister, 
	   		isnull(evd.QuantityConfirmed, 0) QuantityConfirmed
	   		from Inventory.PurchaseOrderDetail pod
	   		left join
	   	(
	   		select evd.PurchaseOrderDetailId, 
	   		sum(case when ev.Status <> 2 then evd.Quantity else 0 end) QuantityRegister, 
	   		sum(case when ev.Status = 2 then evd.Quantity else 0 end) QuantityConfirmed
	   		from Inventory.EntranceVoucherDetail evd 
	   		inner join Inventory.EntranceVoucher ev on evd.EntranceVoucherId = ev.Id	
	   		group by evd.PurchaseOrderDetailId
	   	) evd on pod.Id = evd.PurchaseOrderDetailId
	   ) tempEV
	   inner join 
	   (
	   		select pod.PurchaseOrderId, pod.Id PurchaseOrderDetailId, 
	   		isnull(red.QuantityRegister, 0) QuantityRegister, 
	   		isnull(red.QuantityConfirmed, 0) QuantityConfirmed
	   		from Inventory.PurchaseOrderDetail pod
	   		left join
	   	(
	   		select red.PurchaseOrderDetailId, 
	   		sum(case when re.Status <> 2 then red.Quantity else 0 end) QuantityRegister, 
	   		sum(case when re.Status = 2 then red.Quantity else 0 end) QuantityConfirmed
	   		from Inventory.RemissionEntranceDetail red 
	   		inner join Inventory.RemissionEntrance re on red.RemissionEntranceId = re.Id	
	   		group by red.PurchaseOrderDetailId
	   	) red on pod.Id = red.PurchaseOrderDetailId
	   ) tempRE on tempRE.PurchaseOrderId = tempEV.PurchaseOrderId and tempRE.PurchaseOrderDetailId = tempEV.PurchaseOrderDetailId
	   group by tempEV.PurchaseOrderId, tempEV.PurchaseOrderDetailId