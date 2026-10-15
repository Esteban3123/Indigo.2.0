CREATE VIEW [dbo].[ZTrazabilidadCartera]
AS
SELECT        fact.Nit, fact.Entidad, fact.NroFactura, fact.FechaFactura, fact.ValorFactura, fact.SaldoFactura, fact.EstadoFactura, tras.ConsecTrasl, tras.FechaTraslado, tras.ValorTraslado, notd.ConsecNotaDebito, notd.FechaNotaDebito, 
                         notd.ValorNotaDebito, notc.ConsecNotaCredito, notc.FechaNotaCredito, notc.ValorNotaCredito, glos.ConsecRadObejcion, glos.Oficio, glos.NombreEntidad, glos.FechaRadicacion AS FechaRadicacionFactura, 
                         glos.RadicadoNumero, glos.EstadoRecpObjecion, glos.ValorEntidad, glos.ValorAceptado1Estancia, glos.ValorReiterado, glos.SaldoValorReiterado, glos.ValorAceptado2Instancia, glos.ValorAceptadoConciliacionIPS, 
                         glos.ValorAceptadoConciliacionEPS, glos.SaldoGlosa, glos.ValorCobroJuridico, glos.SaldoCobroJuridico, CONVERT(date, glos.FechaEvalucionGlosa) AS FechaEvalucionGlosa, CONVERT(date, glos.FechaCoordinacionGlosa) 
                         AS FechaCoordinacionGlosa, glos.EstadoGlosa
FROM            (SELECT        a.InvoiceNumber AS NroFactura, a.AccountReceivableDate AS FechaFactura, b.Nit, b.Name AS Entidad, 
                                                    CASE WHEN PortfolioStatus = 1 THEN 'Sin Radicar' WHEN PortfolioStatus = 2 THEN 'Radicada Sin Confirmar' WHEN PortfolioStatus = 3 THEN 'Radicada Entidad' WHEN PortfolioStatus = 7 THEN 'Certificacion Parcial'
                                                     WHEN PortfolioStatus = 8 THEN 'Devolucion Factura' WHEN PortfolioStatus = 1 THEN 'Dificil Recaudo' END AS EstadoFactura, a.Value AS ValorFactura, a.Balance AS SaldoFactura
                          FROM            Portfolio.AccountReceivable AS a INNER JOIN
                                                    Common.ThirdParty AS b ON a.ThirdPartyId = b.Id AND b.PersonType = 2) AS fact LEFT OUTER JOIN
                             (SELECT        tra1.ConsecTrasl, tra1.NroFactura, tra1.FechaTraslado, tra2.ValorTraslado
                               FROM            (SELECT        c.InvoiceNumber AS NroFactura, MAX(a.Code) AS ConsecTrasl, MAX(a.DocumentDate) AS FechaTraslado
                                                         FROM            Portfolio.PortfolioTransfer AS a INNER JOIN
                                                                                   Portfolio.PortfolioTransferDetail AS b ON a.Id = b.PortfolioTrasferId INNER JOIN
                                                                                   Portfolio.AccountReceivable AS c ON c.Id = b.AccountReceivableId
                                                         WHERE        (a.Status = 2)
                                                         GROUP BY c.InvoiceNumber, a.DocumentDate) AS tra1 INNER JOIN
                                                             (SELECT        c.InvoiceNumber AS NroFactura, SUM(b.Value) AS ValorTraslado
                                                               FROM            Portfolio.PortfolioTransfer AS a INNER JOIN
                                                                                         Portfolio.PortfolioTransferDetail AS b ON a.Id = b.PortfolioTrasferId INNER JOIN
                                                                                         Portfolio.AccountReceivable AS c ON c.Id = b.AccountReceivableId
                                                               WHERE        (a.Status = 2)
                                                               GROUP BY c.InvoiceNumber) AS tra2 ON tra1.NroFactura = tra2.NroFactura) AS tras ON fact.NroFactura = tras.NroFactura LEFT OUTER JOIN
                             (SELECT        not1.ConsecNotaDebito, not1.FechaNotaDebito, not1.InvoiceNumber AS NroFactura, not2.ValorNotaDebito
                               FROM            (SELECT        MAX(a.Code) AS ConsecNotaDebito, MAX(a.NoteDate) AS FechaNotaDebito, c.InvoiceNumber
                                                         FROM            Portfolio.PortfolioNote AS a INNER JOIN
                                                                                   Portfolio.PortfolioNoteAccountReceivableAdvance AS b ON a.Id = b.PortfolioNoteId INNER JOIN
                                                                                   Portfolio.AccountReceivable AS c ON c.Id = b.AccountReceivableId
                                                         WHERE        (a.Nature = 1) AND (a.Status = 2)
                                                         GROUP BY c.InvoiceNumber) AS not1 LEFT OUTER JOIN
                                                             (SELECT        c.InvoiceNumber, SUM(b.AdjusmentValue) AS ValorNotaDebito
                                                               FROM            Portfolio.PortfolioNote AS a INNER JOIN
                                                                                         Portfolio.PortfolioNoteAccountReceivableAdvance AS b ON a.Id = b.PortfolioNoteId INNER JOIN
                                                                                         Portfolio.AccountReceivable AS c ON c.Id = b.AccountReceivableId
                                                               WHERE        (a.Nature = 1) AND (a.Status = 2)
                                                               GROUP BY c.InvoiceNumber) AS not2 ON not1.InvoiceNumber = not2.InvoiceNumber) AS notd ON fact.NroFactura = notd.NroFactura LEFT OUTER JOIN
                             (SELECT        not1_1.ConsecNotaCredito, not1_1.FechaNotaCredito, not1_1.InvoiceNumber AS NroFactura, not2_1.ValorNotaCredito
                               FROM            (SELECT        MAX(a.Code) AS ConsecNotaCredito, MAX(a.NoteDate) AS FechaNotaCredito, c.InvoiceNumber
                                                         FROM            Portfolio.PortfolioNote AS a INNER JOIN
                                                                                   Portfolio.PortfolioNoteAccountReceivableAdvance AS b ON a.Id = b.PortfolioNoteId INNER JOIN
                                                                                   Portfolio.AccountReceivable AS c ON c.Id = b.AccountReceivableId
                                                         WHERE        (a.Nature = 2) AND (a.Status = 2)
                                                         GROUP BY c.InvoiceNumber) AS not1_1 INNER JOIN
                                                             (SELECT        c.InvoiceNumber, SUM(b.AdjusmentValue) AS ValorNotaCredito
                                                               FROM            Portfolio.PortfolioNote AS a INNER JOIN
                                                                                         Portfolio.PortfolioNoteAccountReceivableAdvance AS b ON a.Id = b.PortfolioNoteId INNER JOIN
                                                                                         Portfolio.AccountReceivable AS c ON c.Id = b.AccountReceivableId
                                                               WHERE        (a.Nature = 2) AND (a.Status = 2)
                                                               GROUP BY c.InvoiceNumber) AS not2_1 ON not1_1.InvoiceNumber = not2_1.InvoiceNumber) AS notc ON fact.NroFactura = notc.NroFactura LEFT OUTER JOIN
                             (SELECT        a.Code AS ConsecRC, a.DocumentDate AS FechaRecibo, d.InvoiceNumber AS NroFactura, b.Value AS ValorRecibo
                               FROM            Treasury.CashReceipts AS a INNER JOIN
                                                         Treasury.CashReceiptDetails AS b ON a.Id = b.IdCashReceipt INNER JOIN
                                                         Treasury.CashReceiptAccountReceivable AS c ON c.CashReceiptDetailId = b.Id INNER JOIN
                                                         Portfolio.AccountReceivable AS d ON d.Id = c.AccountReceivableId
                               WHERE        (a.Status = 2)) AS reci ON fact.NroFactura = reci.NroFactura LEFT OUTER JOIN
                             (SELECT        a.RadicatedConsecutive AS ConsecRadObejcion, a.DocumentNumber AS Oficio, b.Nit, b.Name AS NombreEntidad, d.RadicatedDate AS FechaRadicacion, d.RadicatedNumber AS RadicadoNumero, 
                                                         CASE WHEN a.state = 1 THEN 'Sin_Confirmar' WHEN a.state = 2 THEN 'Confirmado_Radicado' WHEN a.state = 3 THEN 'Oficio_Con_Respuesta' WHEN a.state = 4 THEN 'Anulada' END AS EstadoRecpObjecion, 
                                                         d.InvoiceNumber AS NroFactura, d.InvoiceValueEntity AS ValorEntidad, d.BalanceInvoice AS SaldoFactura, d.ValueAcceptedFirstInstance AS ValorAceptado1Estancia, d.ValueReiterated AS ValorReiterado, 
                                                         d.ValueReiterationBalance AS SaldoValorReiterado, d.ValueAcceptedSecondInstance AS ValorAceptado2Instancia, d.ValueAcceptedIPSconciliation AS ValorAceptadoConciliacionIPS, 
                                                         d.ValueAcceptedEAPBconciliation AS ValorAceptadoConciliacionEPS, d.BalanceGlosa AS SaldoGlosa, d.LegalTransferValue AS ValorCobroJuridico, d.BalanceLegal AS SaldoCobroJuridico, 
                                                         d.EvaluationDateGlosa AS FechaEvalucionGlosa, d.CoordinationDateGlosa AS FechaCoordinacionGlosa, 
                                                         CASE WHEN d .state = 1 THEN 'Pediente Confirmado Glosa' WHEN d .state = 2 THEN 'Pendiente Evaluacion Glosa' WHEN d .state = 3 THEN 'Pendiente envio de oficio' WHEN d .state = 4 THEN 'Pendiente confirmar reiteracion'
                                                          WHEN d .state = 5 THEN 'Pendiente evaluacion reitreacion' WHEN d .state = 6 THEN 'Pendiente conciliacion' WHEN d .state = 7 THEN 'Pendiente de confirmar factura conciliacion' WHEN d .state = 8 THEN 'Conciliación'
                                                          WHEN d .state = 9 THEN 'Conciliacion parcial' WHEN d .state = 11 THEN 'Glosa con Respuesta' WHEN d .state = 12 THEN 'Reiteracion con Respuesta' WHEN d .state = 13 THEN 'Pendiente confirmar Pago Parcial'
                                                          WHEN d .state = 14 THEN 'Confirmado Pago Parcial' WHEN d .state = 15 THEN 'Traslado a Cobro Juridico' END AS EstadoGlosa
                               FROM            Glosas.GlosaObjectionsReceptionC AS a INNER JOIN
                                                         Common.Customer AS b ON a.CustomerId = b.Id INNER JOIN
                                                         Glosas.GlosaObjectionsReceptionD AS c ON c.GlosaObjectionsReceptionCId = a.Id INNER JOIN
                                                         Glosas.GlosaPortfolioGlosada AS d ON d.Id = c.PortfolioGlosaId) AS glos ON fact.NroFactura = glos.NroFactura