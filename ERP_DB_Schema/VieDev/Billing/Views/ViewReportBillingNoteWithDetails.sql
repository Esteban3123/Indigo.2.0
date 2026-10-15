
 
 
 
CREATE VIEW [Billing].[ViewReportBillingNoteWithDetails]
AS

 WITH  CTE_BillingNote as (
							SELECT	bn.Id as BillingNoteId,
									bn.Nature,
									bn.EntityId,
									bn.EntityName,
									bn.Code
							FROM Billing.BillingNote bn WITH (NOLOCK)
							),
		CTE_NoteTypeDetail as (

							SELECT	bn.*,
									pnarad.BaseValue,
									pnarad.TaxValue,
									pnarad.EntityId EntityIdD,
									pnarad.EntityName EntityNameD,
									pnarad.MainAccountId,
									pnarad.CostCenterId,
									pn.NoteType,
									pn.code PortfolioNoteCode,
									pnarad.TaxId,
									pnara.AccountReceivableId
							FROM CTE_BillingNote bn
							JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
							JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK)
								ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId						
							where pn.NoteType = 6 OR ISNULL(pn.EntityName,'') ='Glosas')
 
 SELECT 
		Concat(id.Id,1) Id,
		ntd.BillingNoteId,
		p.Code, 
		p.Name, 
		p.CodeAlternative,
		p.CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.BaseValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.BaseValue,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		i.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
		JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId

	UNION ALL

	 SELECT 
		Concat(id.Id,2) Id,
		ntd.BillingNoteId, 
		p.Code, 
		p.Name, 
		p.CodeAlternative,
		p.CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.TaxValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.TaxValue,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		i.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
		JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
		JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId
	WHERE sod.RecordType =2
	---------------------------------------------------------------------------------------------------------------------------------
	UNION ALL

	SELECT 
		Concat(id.Id,1) Id,
		ntd.BillingNoteId,  
		ips.Code, 
		ips.Name, 
		ce.Code CodeAlternative,
		ce.RIPSCode CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.BaseValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.BaseValue,0) DebitValue,
		ce.Description NameAlternative,
		ce.RIPSDescription NameAlternativeTwo,
		i.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
		JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId 
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId
	WHERE  sod.RecordType =1 

	UNION ALL

	SELECT 
		Concat(id.Id,2) Id,
		ntd.BillingNoteId,  
		ips.Code, 
		ips.Name, 
		ce.Code CodeAlternative,
		ce.RIPSCode CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.TaxValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.TaxValue,0) DebitValue,
		ce.Description NameAlternative,
		ce.RIPSDescription NameAlternativeTwo,
		i.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
		JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId 
		JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId
	WHERE sod.RecordType =1 
-------------------------------------------------------------------------------------------------------------------
	UNION ALL

	SELECT 
		Concat(ids.Id,1) Id,
		ntd.BillingNoteId,  
		ips.Code, 
		ips.Name, 
		null CodeAlternative,
		null CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.BaseValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.BaseValue,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		i.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetailSurgical ids WITH(NOLOCK) on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='InvoiceDetailSurgical'
		JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ids.InvoiceDetailId =id.Id
		JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId
	WHERE sod.RecordType =1

	UNION ALL

	SELECT 
		Concat(ids.Id,2) Id,
		ntd.BillingNoteId,  
		ips.Code, 
		ips.Name, 
		null CodeAlternative,
		null CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.TaxValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.TaxValue,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		i.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetailSurgical ids WITH(NOLOCK) on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='InvoiceDetailSurgical'
		JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ids.InvoiceDetailId =id.Id
		JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId
	WHERE sod.RecordType =1 
-------------------------------------------------------------------------------------------------------------------------

	UNION ALL

	SELECT 
		Concat(ids.Id,1) Id,
		ntd.BillingNoteId,  
		ips.Code, 
		ips.Name, 
		null CodeAlternative,
		null CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.BaseValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.BaseValue,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		ar.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.ServiceOrderDetailSurgical ids WITH(NOLOCK) on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='ServiceOrderDetailSurgical'
		join Portfolio.AccountReceivable ar WITH(NOLOCK) on ar.Id=ntd.AccountReceivableId
		JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId

	UNION ALL

	SELECT 
		Concat(ids.Id,2) Id,
		ntd.BillingNoteId,  
		ips.Code, 
		ips.Name, 
		null CodeAlternative,
		null CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(ntd.Nature=1,ntd.TaxValue,0) CreditValue,
		IIF(ntd.Nature=2,ntd.TaxValue,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		ar.InvoiceNumber as GroupDetails
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.ServiceOrderDetailSurgical ids WITH(NOLOCK) on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='ServiceOrderDetailSurgical'
		join Portfolio.AccountReceivable ar WITH(NOLOCK) on ar.Id=ntd.AccountReceivableId
		JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id = ntd.CostCenterId

--------------------------------------------------------------------------------------------------------------------------
	UNION ALL
	
	select  
		Concat(pnd.Id,1) Id ,
		bn.BillingNoteId,  
		pnc.Code, 
		pnc.Name, 
		null CodeAlternative,
		null CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(pnd.Nature=2, pnd.Value,0) CreditValue,
		IIF(pnd.Nature=1,pnd.Value,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		'Conceptos Adicionales' as GroupDetails
	from CTE_BillingNote bn
	JOIN  Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
	JOIN Portfolio.PortfolioNoteDetail pnd WITH(NOLOCK) on pnd.PortfolioNoteId = pn.Id
	JOIN Portfolio.PortfolioNoteConcept pnc WITH(NOLOCK) on pnc.Id = pnd.PortfolioNoteConceptId
	JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on pnd.MainAccountId = ma.Id
	LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) on cc.Id = pnd.CostCenterId
	where  pn.NoteType = 6

	UNION ALL
	
	select  
		Concat(pnd.Id,2) Id,
		bn.BillingNoteId,  
		pnc.Code, 
		pnc.Name, 
		null CodeAlternative,
		null CodeAlternativeTwo,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		iif(ma.HandlesCostCenter =1, cc.Code,null) CostCenterCode,
		IIF(pnd.Nature=2,pnd.IvaRate,0) CreditValue,
		IIF(pnd.Nature=1,pnd.IvaRate,0) DebitValue,
		NULL NameAlternative,
		NULL NameAlternativeTwo,
		'Conceptos Adicionales' as GroupDetails
	from CTE_BillingNote bn
	JOIN  Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
	JOIN Portfolio.PortfolioNoteDetail pnd WITH(NOLOCK) on pnd.PortfolioNoteId = pn.Id
	JOIN Portfolio.PortfolioNoteConcept pnc WITH(NOLOCK) on pnc.Id = pnd.PortfolioNoteConceptId
	JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on iva.Id = pnd.IdGeneralLedgerIVA
	JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on iva.IdAccountSale = ma.Id
	LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) on cc.Id = pnd.CostCenterId
	where pnd.RetentionConceptId IS NULL and pn.NoteType = 6
-------------------------------------------------------------------------------------------------------------------------------