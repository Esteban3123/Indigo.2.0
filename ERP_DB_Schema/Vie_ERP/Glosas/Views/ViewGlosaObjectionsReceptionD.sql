


CREATE VIEW [Glosas].[ViewGlosaObjectionsReceptionD]
AS
SELECT 
	gord.GlosaObjectionsReceptionCId,
	d.InvoiceNumber, 
	d.Id, 
	cast(0 as varchar) as IdQx, 
	d.CostCenterCode, 
	d.ServiceCode, 
	d.ServiceName, 
	d.ValueServiceManual, 
	d.UnitValue, 
	d.Ammount, 
	ar.Balance BalanceInvoice,
	d.InvoicedValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.ThirdPartySalesPrice, NULL), d.InvoicedValue) EntityValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.SubTotalPatientSalesPrice, NULL), 0) PatientValue,
	gmg.CodeGlosa codeConcept,
	r.Code codeResponsale,
	gmg.ValueGlosado ValueGlosa,
	isnull(gmg.ValuePendingConciliation,0) ValuePendingConciliation,	
	gmg.RationaleGlosa,
	gord.DocumentType,
	'' comment
FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = gord.InvoiceNumber
JOIN Glosas.GlosaInvoiceDetail d WITH (NOLOCK) ON gord.InvoiceNumber = d.InvoiceNumber
LEFT JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON id.Id = d.InvoiceDetailNativeId
LEFT JOIN Glosas.GlosaInvoiceDetailQX qx WITH (NOLOCK) ON d.Id = qx.InvoiceDetailId  
LEFT JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON d.Id = gmg.InvoiceDetailId AND gmg.ValuePendingConciliation > 0
LEFT JOIN Glosas.Responsible r WITH (NOLOCK) ON gmg.ResponsibleId = r.Id
WHERE d.InvoicedValue > 0 AND qx.id IS NULL 

UNION ALL 

SELECT 
	gord.GlosaObjectionsReceptionCId,
	d.InvoiceNumber, 
	d.Id, 
	cast(d.ServiceCode as varchar) as IdQx, 
	qx.CostCenterCode, 
	qx.ServiceCode, 
	qx.ServiceName, 
	qx.ValueServiceManual, 
	qx.UnitValue, 
	qx.Ammount, 
	ar.Balance BalanceInvoice,
	qx.InvoicedValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.ThirdPartySalesPrice, NULL), qx.InvoicedValue) EntityValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.SubTotalPatientSalesPrice, NULL), 0) PatientValue,
	gmg.CodeGlosa codeConcept,
	r.Code codeResponsale,
	gmg.ValueGlosado ValueGlosa,
	isnull(gmg.ValuePendingConciliation,0) ValuePendingConciliation,	
	gmg.RationaleGlosa,
	gord.DocumentType,
	gmg.RationaleGlosa comment
FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = gord.InvoiceNumber
JOIN Glosas.GlosaInvoiceDetail d WITH (NOLOCK) ON gord.InvoiceNumber = d.InvoiceNumber
JOIN Glosas.GlosaInvoiceDetailQX qx WITH (NOLOCK) ON d.Id = qx.InvoiceDetailId
LEFT JOIN Billing.InvoiceDetail id with(NOLOCK) ON id.Id = d.InvoiceDetailNativeId
LEFT JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON d.Id = gmg.InvoiceDetailId AND qx.Id = gmg.InvoiceDetailIdQX AND gmg.ValuePendingConciliation > 0
LEFT JOIN Glosas.Responsible r WITH (NOLOCK) ON gmg.ResponsibleId = r.Id
WHERE qx.InvoicedValue > 0