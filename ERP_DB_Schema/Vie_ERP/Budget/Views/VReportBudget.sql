
CREATE VIEW [Budget].[VReportBudget]
AS
	SELECT	CONCAT('Factura - ', ipa.Id) Id,
			ar.InvoiceNumber InvoiceNumber,
			ar.AccountReceivableDate FechaFactura,
			tp.Nit,
			tp.Name Nombre,
			pr.RegimenName Regimen,
			NULL ConsecutivoRadicado,
			NULL FechaRadicado,
			pa.Code CodigoCruce,
			ar.AccountReceivableDate FechaCruce,
			ar.CreationUser,
			ipa.Value Total
	FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
	JOIN Billing.InvoicePortfolioAdvance ipa ON i.Id = ipa.InvoiceId
	JOIN Portfolio.PortfolioAdvance pa ON ipa.PortfolioAdvanceId = pa.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	-------------------------------------------------------------------------------------------------------------------	
	WHERE i.DocumentType = 5 AND ar.AccountReceivableType = 6
UNION ALL
	SELECT	CONCAT('Recibo de Caja - ', crar.Id) Id,
			ar.InvoiceNumber InvoiceNumber,
			ar.AccountReceivableDate FechaFactura,
			tp.Nit,
			tp.Name Nombre,
			pr.RegimenName Regimen,
			ri.RadicatedConsecutive ConsecutivoRadicado,
			ri.RadicatedDate FechaRadicado,
			cr.Code CodigoCruce,
			cr.DocumentDate FechaCruce,
			cr.CreationUser,
			crar.Value - ISNULL(aric.Value, 0) Total
	FROM Treasury.CashReceipts cr WITH (NOLOCK)
	JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
	JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON crar.AccountReceivableId = ar.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ar.InvoiceNumber = rid.InvoiceNumber AND rid.Devolution = 0 AND rid.State = '2'
	LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON rid.RadicateInvoiceCId = ri.Id AND ri.State = '2'
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON crar.Id = aric.CashReceiptAccountReceivableId
	WHERE cr.Status IN (2, 4)
UNION ALL
	SELECT	CONCAT('Cruce de Anticipo vs CxC - ', ptd.Id) Id,
			ar.InvoiceNumber InvoiceNumber,
			ar.AccountReceivableDate FechaFactura,
			tp.Nit,
			tp.Name Nombre,
			pr.RegimenName Regimen,
			ri.RadicatedConsecutive ConsecutivoRadicado,
			ri.RadicatedDate FechaRadicado,
			pt.Code CodigoCruce,
			pt.DocumentDate FechaCruce,
			pt.CreationUser,
			ptd.Value - ISNULL(aric.Value, 0) Total
	FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
	JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ptd.AccountReceivableId = ar.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ar.InvoiceNumber = rid.InvoiceNumber AND rid.Devolution = 0 AND rid.State = '2'
	LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON rid.RadicateInvoiceCId = ri.Id AND ri.State = '2'
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON ptd.Id = aric.PortfolioTransferDetailId
	WHERE pt.Status IN (2, 4)
---------------------------------------------------------------------------------------------------------------
UNION ALL
---------------------------------------------------------------------------------------------------------------
	SELECT	CONCAT('Anulación de Factura - ', ipa.Id) Id,
			ar.InvoiceNumber InvoiceNumber,
			ar.AccountReceivableDate FechaFactura,
			tp.Nit,
			tp.Name Nombre,
			pr.RegimenName Regimen,
			NULL ConsecutivoRadicado,
			NULL FechaRadicado,
			pa.Code CodigoCruce,
			i.AnnulmentDate FechaCruce,
			i.AnnulmentUser CreationUser,
			ipa.Value * -1 Total
	FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
	JOIN Billing.InvoicePortfolioAdvance ipa ON i.Id = ipa.InvoiceId
	JOIN Portfolio.PortfolioAdvance pa ON ipa.PortfolioAdvanceId = pa.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	-------------------------------------------------------------------------------------------------------------------	
	WHERE i.DocumentType = 5 AND ar.AccountReceivableType = 6
UNION ALL
	SELECT	CONCAT('Nota de Tesoreria - ', crar.Id) Id,
			ar.InvoiceNumber InvoiceNumber,
			ar.AccountReceivableDate FechaFactura,
			tp.Nit,
			tp.Name Nombre,
			pr.RegimenName Regimen,
			ri.RadicatedConsecutive ConsecutivoRadicado,
			ri.RadicatedDate FechaRadicado,
			tn.Code CodigoCruce,
			tn.NoteDate FechaCruce,
			tn.CreationUser,
			(crd.Value - ISNULL(aric.Value, 0)) * -1 Total
	FROM Treasury.TreasuryNote tn WITH (NOLOCK)
	JOIN Treasury.CashReceipts cr WITH (NOLOCK) ON tn.CashReceiptId = cr.Id
	JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
	JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON crar.AccountReceivableId = ar.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
	LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ar.InvoiceNumber = rid.InvoiceNumber AND rid.Devolution = 0 AND rid.State = '2'
	LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON rid.RadicateInvoiceCId = ri.Id AND ri.State = '2'
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON crar.Id = aric.CashReceiptAccountReceivableId
	WHERE tn.Status = 2
UNION ALL
	SELECT	CONCAT('Nota de Cartera - ', ptd.Id) Id,
			ar.InvoiceNumber InvoiceNumber,
			ar.AccountReceivableDate FechaFactura,
			tp.Nit,
			tp.Name Nombre,
			pr.RegimenName Regimen,
			ri.RadicatedConsecutive ConsecutivoRadicado,
			ri.RadicatedDate FechaRadicado,
			pn.Code CodigoCruce,
			pn.NoteDate FechaCruce,
			pn.CreationUser,
			(ptd.Value - ISNULL(aric.Value, 0)) * -1 Total
	FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
	JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK) ON pn.PortfolioTransferId = pt.Id
	JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ptd.AccountReceivableId = ar.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ar.InvoiceNumber = rid.InvoiceNumber AND rid.Devolution = 0 AND rid.State = '2'
	LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON rid.RadicateInvoiceCId = ri.Id AND ri.State = '2'
	-------------------------------------------------------------------------------------------------------
	LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON ptd.Id = aric.PortfolioTransferDetailId
	WHERE pn.Status = 2