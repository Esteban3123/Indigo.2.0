CREATE VIEW [Billing].[ViewReportBillingNoteWithDetails]
AS

  WITH  CTE_BillingNote as (
							SELECT	bn.Id as BillingNoteId,
									bn.Nature,
									bn.EntityId,
									bn.EntityName,
									bn.Code
							FROM Billing.BillingNote bn 
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
							JOIN Portfolio.PortfolioNote pn  ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  on pn.Id = pnara.PortfolioNoteId
							JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad 
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
		i.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		NULL as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id  on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i  on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod  on sod.Id=id.ServiceOrderDetailId
		JOIN Inventory.InventoryProduct p  ON sod.ProductId = p.Id
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId

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
		i.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		IIf(tax.TaxClassificationType =2, null, tax.percentage)  as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id  on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i  on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod  on sod.Id=id.ServiceOrderDetailId
		JOIN Inventory.InventoryProduct p  ON sod.ProductId = p.Id
		LEFT jOIN GeneralLedger.GeneralLedgerIVA tax  ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId
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
		i.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		NULL as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id  on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i  on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod  on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips  ON ips.Id = sod.IPSServiceId
		JOIN Contract.CUPSEntity ce  ON ce.Id = sod.CUPSEntityId 
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId
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
		i.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		IIf(tax.TaxClassificationType =2, null, tax.percentage)  as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetail id  on ntd.EntityNameD ='InvoiceDetail' and ntd.EntityIdD = id.Id
		JOIN Billing.Invoice i  on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod  on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips  ON ips.Id = sod.IPSServiceId
		JOIN Contract.CUPSEntity ce  ON ce.Id = sod.CUPSEntityId 
		LEFT jOIN GeneralLedger.GeneralLedgerIVA tax  ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId
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
		i.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		null as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetailSurgical ids  on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='InvoiceDetailSurgical'
		JOIN Billing.InvoiceDetail id  on ids.InvoiceDetailId =id.Id
		JOIN Billing.Invoice i  on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod  on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips  ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId
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
		i.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		IIf(tax.TaxClassificationType =2, null, tax.percentage)  as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.InvoiceDetailSurgical ids  on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='InvoiceDetailSurgical'
		JOIN Billing.InvoiceDetail id  on ids.InvoiceDetailId =id.Id
		JOIN Billing.Invoice i  on i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod  on sod.Id=id.ServiceOrderDetailId
		JOIN Contract.IPSService ips  ON ips.Id = ids.IPSServiceId
		LEFT jOIN GeneralLedger.GeneralLedgerIVA tax  ON tax.Id = ntd.TaxId
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId
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
		ar.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		null as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.ServiceOrderDetailSurgical ids  on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='ServiceOrderDetailSurgical'
		join Portfolio.AccountReceivable ar  on ar.Id=ntd.AccountReceivableId
		left join billing.invoice i  on i.Id = ar.InvoiceId
		JOIN Contract.IPSService ips  ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = ntd.MainAccountId
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId

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
		ar.InvoiceNumber as GroupDetails,
		1 Quantity,
		ntd.BaseValue As BaseValue,
		ntd.TaxValue As TaxValue,
		IIf(tax.TaxClassificationType =2, null, tax.percentage)  as IvaPercentage,
		ntd.BaseValue + ntd.TaxValue TotalValue,
		i.cufe as CUFE
	FROM CTE_NoteTypeDetail ntd
		JOIN Billing.ServiceOrderDetailSurgical ids  on ntd.EntityIdD = ids.Id and ntd.EntityNameD ='ServiceOrderDetailSurgical'
		join Portfolio.AccountReceivable ar  on ar.Id=ntd.AccountReceivableId
		JOIN Contract.IPSService ips  ON ips.Id = ids.IPSServiceId
		JOIN GeneralLedger.GeneralLedgerIVA tax  ON tax.Id = ntd.TaxId
		left join billing.invoice i  on i.Id = ar.InvoiceId
		JOIN GeneralLedger.MainAccounts ma  on ma.Id = tax.IdAccountSale
		LEFT JOIN Payroll.CostCenter cc  ON cc.Id = ntd.CostCenterId

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
		'Conceptos Adicionales' as GroupDetails,
		1 Quantity,
		pnd.BaseValue As BaseValue,
		pnd.IvaRate As TaxValue,
		null as IvaPercentage,
		pnd.TotalConcept TotalValue,
		'' as CUFE
	from CTE_BillingNote bn
	JOIN  Portfolio.PortfolioNote pn  on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
	JOIN Portfolio.PortfolioNoteDetail pnd  on pnd.PortfolioNoteId = pn.Id
	JOIN Portfolio.PortfolioNoteConcept pnc  on pnc.Id = pnd.PortfolioNoteConceptId
	JOIN GeneralLedger.MainAccounts ma  on pnd.MainAccountId = ma.Id
	LEFT JOIN Payroll.CostCenter cc  on cc.Id = pnd.CostCenterId
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
		'Conceptos Adicionales' as GroupDetails,
		1 Quantity,
		pnd.BaseValue As BaseValue,
		pnd.IvaRate As TaxValue,
		IIf(iva.TaxClassificationType =2, null, iva.percentage)  as IvaPercentage,
		pnd.TotalConcept TotalValue,
		'' as CUFE
	from CTE_BillingNote bn
	JOIN  Portfolio.PortfolioNote pn  on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
	JOIN Portfolio.PortfolioNoteDetail pnd  on pnd.PortfolioNoteId = pn.Id
	JOIN Portfolio.PortfolioNoteConcept pnc  on pnc.Id = pnd.PortfolioNoteConceptId
	LEFT jOIN GeneralLedger.GeneralLedgerIVA iva  on iva.Id = pnd.IdGeneralLedgerIVA
	JOIN GeneralLedger.MainAccounts ma  on iva.IdAccountSale = ma.Id
	LEFT JOIN Payroll.CostCenter cc  on cc.Id = pnd.CostCenterId
	where pnd.RetentionConceptId IS NULL and pn.NoteType = 6
-------------------------------------------------------------------------------------------------------------------------------
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle contable de las notas de facturación (crédito y débito) asociadas a glosas y notas de cartera de tipo ajuste. Integra los encabezados de notas de facturación con sus líneas de detalle de cuentas por cobrar, descomponiendo cada movimiento en valor base e impuestos (IVA) por separado, y los cruza con el detalle de los servicios o productos facturados (servicios CUPS/IPS y productos de inventario). Para cada línea expone el código y nombre del servicio o producto, la cuenta contable con su número y descripción, el centro de costo cuando aplica, y clasifica el valor como crédito o débito según la naturaleza de la nota. Agrupa los movimientos por número de factura de origen, sirviendo como base para reportes contables y de auditoría de notas crédito/débito relacionadas con glosas y ajustes de cartera.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportBillingNoteWithDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportBillingNoteWithDetails';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte el detalle contable de notas de facturación originadas en notas de cartera (tipo 6 o glosas), exponiendo por línea producto/servicio, cuenta, centro de costo, valor base e IVA discriminados como crédito o débito según la naturaleza.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La nota de facturación debe estar enlazada a una entidad con EntityName=''PortfolioNote''.; La PortfolioNote asociada debe tener NoteType=6 o EntityName=''Glosas'' para entrar al CTE de detalles.; Cada PortfolioNoteAccountReceivableDetail debe estar referenciado a un anticipo (PortfolioNoteAccountReceivableAdvance) válido.; Para detalles tipo InvoiceDetail/InvoiceDetailSurgical debe existir factura (Invoice) y orden de servicio (ServiceOrderDetail) ligadas.; Para resolver IVA, ntd.TaxId debe corresponder a un registro de GeneralLedger.GeneralLedgerIVA con IdAccountSale válido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada detalle origina exactamente dos filas en el reporte (base e IVA) identificadas por el sufijo 1 o 2 en el Id.; Un mismo registro nunca tiene CreditValue y DebitValue distintos de cero simultáneamente: depende exclusivamente de Nature.; Para líneas de IVA, la cuenta contable proviene siempre de GeneralLedgerIVA.IdAccountSale, no del MainAccountId del detalle.; Las líneas de productos de inventario solo aplican cuando ServiceOrderDetail.RecordType=2; las de servicios IPS/CUPS solo cuando RecordType=1.; Las líneas etiquetadas como ''Conceptos Adicionales'' provienen exclusivamente de PortfolioNoteDetail con NoteType=6.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de facturación (crédito/débito); Nota de cartera (PortfolioNote); Glosas; Anticipo de cuenta por cobrar; Cuenta contable / Plan de cuentas (PUC); Centro de costo; IVA / Impuesto al valor agregado; Factura de venta; Orden de servicio; Servicios CUPS / IPS; Detalle quirúrgico; Conceptos adicionales de cartera; Retenciones; RIPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewReportBillingNoteWithDetails: Por cada detalle se emiten dos filas: sufijo Id ''...1'' con BaseValue y sufijo ''...2'' con TaxValue (IVA), permitiendo separar base e impuesto en el reporte.; [RETURN_RESULT] Billing.ViewReportBillingNoteWithDetails: Si ntd.Nature=1 (crédito) el valor se asigna a CreditValue y DebitValue=0; si Nature=2 (débito) se asigna a DebitValue y CreditValue=0.; [RETURN_RESULT] Billing.ViewReportBillingNoteWithDetails: Para PortfolioNoteDetail (NoteType=6) la asignación se invierte: Nature=2 → CreditValue, Nature=1 → DebitValue.; [RETURN_RESULT] Billing.ViewReportBillingNoteWithDetails: CostCenterCode solo se muestra cuando MainAccounts.HandlesCostCenter=1; en caso contrario es NULL.; [RETURN_RESULT] Billing.ViewReportBillingNoteWithDetails: Las líneas de PortfolioNoteDetail se etiquetan con GroupDetails=''Conceptos Adicionales''; las demás usan el InvoiceNumber de la factura o cuenta por cobrar.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pn.NoteType = 6 OR ISNULL(pn.EntityName,'''') = ''Glosas'' → Se incluyen los detalles de PortfolioNoteAccountReceivableDetail en el CTE de procesamiento. else La nota se excluye del reporte.; si ntd.EntityNameD = ''InvoiceDetail'' y sod.RecordType = 2 → Se reporta el ítem como producto de inventario (Inventory.InventoryProduct). else Si RecordType=1 se reporta como servicio (Contract.IPSService + Contract.CUPSEntity).; si ntd.EntityNameD = ''InvoiceDetailSurgical'' → Se resuelve el servicio vía Billing.InvoiceDetailSurgical → IPSService (cirugía facturada).; si ntd.EntityNameD = ''ServiceOrderDetailSurgical'' → Se resuelve el servicio vía Billing.ServiceOrderDetailSurgical y se toma el InvoiceNumber desde Portfolio.AccountReceivable (no de factura directa).; si Fila de IVA (sufijo 2) sobre PortfolioNoteDetail → Solo se incluye cuando pnd.RetentionConceptId IS NULL y pn.NoteType=6, evitando líneas de retención. else Las líneas con concepto de retención no generan fila de IVA.; si ma.HandlesCostCenter = 1 → Se expone el código del centro de costo asociado. else CostCenterCode = NULL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingNote; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableDetail; Billing.InvoiceDetail; Billing.Invoice; Billing.ServiceOrderDetail; Inventory.InventoryProduct; GeneralLedger.MainAccounts; Payroll.CostCenter; GeneralLedger.GeneralLedgerIVA; Contract.IPSService; Contract.CUPSEntity; Billing.InvoiceDetailSurgical; Billing.ServiceOrderDetailSurgical; Portfolio.AccountReceivable; Portfolio.PortfolioNoteDetail; Portfolio.PortfolioNoteConcept', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBillingNoteWithDetails';
GO

