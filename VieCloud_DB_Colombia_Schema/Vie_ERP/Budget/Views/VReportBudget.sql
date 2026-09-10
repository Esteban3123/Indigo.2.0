
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte presupuestal y de recaudo de cartera que consolida en una sola consulta todos los movimientos de cobro y cruce de cuentas por cobrar. Integra cinco tipos de transacciones mediante UNION ALL: facturas aplicadas con anticipos, recibos de caja, cruces de anticipo contra cuentas por cobrar, anulaciones de factura y notas de tesorería. Para cada movimiento expone el número de factura, fecha, NIT y nombre del tercero pagador (EPS, aseguradora, empresa), régimen de salud, consecutivo y fecha de radicado, código y fecha del cruce, usuario que generó el registro y valor total. Sirve como fuente para reportes de seguimiento de recaudo, gestión de cartera y presupuesto, permitiendo trazabilidad completa entre facturas emitidas, anticipos recibidos, pagos en tesorería y traslados de cartera por pagador y régimen.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'VReportBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'VReportBudget';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte los movimientos de cruce entre anticipos de cartera y cuentas por cobrar (aplicaciones, recibos de caja, transferencias) y sus reversos (anulaciones, notas de tesorería y de cartera), exponiendo factura, tercero, régimen, radicado y valor neto.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por cobrar deben tener cuenta contable sin radicar (AccountWithoutRadicateId) para resolver el régimen vía Portfolio.GetRegimes() sobre MainAccounts.Number.; Para asociar consecutivo y fecha de radicado, debe existir RadicateInvoiceD con Devolution=0 y State=''2'' y RadicateInvoiceC con State=''2'' sobre el mismo InvoiceNumber.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El IVA recaudado (Portfolio.AccountReceivableIVACollected) siempre se resta del valor del cruce cuando existe, garantizando que el Total represente capital sin IVA.; Sólo se asocian radicaciones aprobadas (State=''2'') y no devolución (Devolution=0).; Las filas de tipo anulación/nota tienen Total ≤ 0; las de aplicación/recibo/transferencia tienen Total ≥ 0.; El régimen se resuelve siempre vía la cuenta contable de la CxC sin radicar (AccountWithoutRadicateId).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipos de cartera; Cuentas por cobrar; Factura; Recibo de caja; Transferencia de cartera; Nota de tesorería; Nota de cartera; Anulación de factura; Radicación de factura; IVA recaudado; Régimen; Tercero', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.VReportBudget: Aplicaciones de anticipo a factura: incluye filas (Factura - {ipa.Id}) sólo cuando Billing.Invoice.DocumentType=5 y Portfolio.AccountReceivable.AccountReceivableType=6, con Total=ipa.Value.; [RETURN_RESULT] Budget.VReportBudget: Cruces por recibo de caja: incluye filas (Recibo de Caja - {crar.Id}) sólo cuando Treasury.CashReceipts.Status IN (2,4); Total = crar.Value - ISNULL(aric.Value,0) descontando IVA recaudado.; [RETURN_RESULT] Budget.VReportBudget: Cruces por transferencia de cartera: incluye filas (Cruce de Anticipo vs CxC - {ptd.Id}) sólo cuando Portfolio.PortfolioTransfer.Status IN (2,4); Total = ptd.Value - ISNULL(aric.Value,0).; [RETURN_RESULT] Budget.VReportBudget: Anulaciones de factura: incluye filas (Anulación de Factura - {ipa.Id}) cuando Invoice.DocumentType=5 y AccountReceivable.AccountReceivableType=6, con Total=ipa.Value*-1, FechaCruce=Invoice.AnnulmentDate y usuario=Invoice.AnnulmentUser.; [RETURN_RESULT] Budget.VReportBudget: Notas de tesorería: incluye filas (Nota de Tesoreria - {crar.Id}) sólo cuando Treasury.TreasuryNote.Status=2; Total = (crd.Value - ISNULL(aric.Value,0)) * -1.; [RETURN_RESULT] Budget.VReportBudget: Notas de cartera: incluye filas (Nota de Cartera - {ptd.Id}) sólo cuando Portfolio.PortfolioNote.Status=2; Total = (ptd.Value - ISNULL(aric.Value,0)) * -1.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.DocumentType = 5 AND ar.AccountReceivableType = 6 → Las ramas de aplicación y anulación sólo consideran facturas de tipo 5 con CxC tipo 6 (anticipos).; si cr.Status IN (2,4) → Sólo recibos de caja en estado 2 o 4 son tratados como cruces vigentes.; si pt.Status IN (2,4) → Sólo transferencias de cartera en estado 2 o 4 son tratadas como cruces vigentes.; si tn.Status = 2 / pn.Status = 2 → Sólo notas de tesorería o cartera en estado 2 generan reversos en el reporte.; si Filas correspondientes a anulación / notas (tesorería y cartera) → Se registran con Total negativo (multiplicado por -1) para reversar el cruce previo. else Las filas de aplicación, recibo y transferencia se registran con Total positivo.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Billing.Invoice; Billing.InvoicePortfolioAdvance; Portfolio.PortfolioAdvance; Common.ThirdParty; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Portfolio.AccountReceivableIVACollected; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Treasury.TreasuryNote; Contract.CareGroup; Portfolio.PortfolioNote', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudget';
GO
