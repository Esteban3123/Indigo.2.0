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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad completa del ciclo de cartera por factura: consolida en una sola vista la información de cada factura emitida a una entidad o aseguradora (NIT, nombre, número de factura, fecha, valor y saldo), junto con sus traslados de cartera, notas débito, notas crédito y el estado de glosas u objeciones radicadas. Integra las tablas de cuentas por cobrar, terceros, transferencias de cartera y notas de cartera para ofrecer una trazabilidad longitudinal que permite conocer en qué estado se encuentra cada cobro: si fue radicado, trasladado, ajustado con nota débito o crédito, objetado por la entidad pagadora, reiterado, conciliado con la IPS o la EPS, o enviado a cobro jurídico. Es la vista central para auditoría de cartera, seguimiento de glosas, gestión de cobro y reportes financieros de recuperación de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ZTrazabilidadCartera';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ZTrazabilidadCartera';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de trazabilidad integral de cartera que consolida por factura los traslados, notas débito/crédito, recibos de caja y glosas radicadas, mostrando estados, valores y saldos asociados al proceso de recaudo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por cobrar deben pertenecer a terceros con PersonType = 2 para ser incluidas como facturas base.; Los traslados de cartera (PortfolioTransfer) solo se consideran si Status = 2 (confirmado/aprobado).; Las notas de cartera (PortfolioNote) solo se consideran si Status = 2.; Los recibos de caja solo se consideran si Status = 2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consolidan movimientos de cartera (traslados y notas) en estado confirmado (Status=2).; La separación entre nota débito y nota crédito se determina exclusivamente por el campo Nature (1=débito, 2=crédito).; Solo entidades/terceros jurídicos (PersonType=2) son consideradas como entidades de la factura base.; Los valores de traslado y notas se calculan agregando (SUM) por número de factura, mientras consecutivo y fecha se obtienen como MAX.; Las fechas de evaluación y coordinación de glosa se truncan a tipo date.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cuenta por cobrar; Cartera; Traslado de cartera; Nota débito; Nota crédito; Recibo de caja; Glosa; Objeción; Radicación de factura; Reiteración; Conciliación IPS; Conciliación EPS; Cobro jurídico; Saldo de glosa; Entidad pagadora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ZTrazabilidadCartera: Devuelve una fila por factura con sus eventos asociados (traslados, notas débito/crédito, glosas) usando LEFT OUTER JOIN; las facturas sin eventos relacionados aparecen igualmente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PortfolioStatus de la cuenta por cobrar → Mapea el estado de factura: 1=''Sin Radicar'', 2=''Radicada Sin Confirmar'', 3=''Radicada Entidad'', 7=''Certificacion Parcial'', 8=''Devolucion Factura''. else El valor adicional 1=''Dificil Recaudo'' es inalcanzable por colisión con el primer WHEN.; si Nature de PortfolioNote → Nature=1 clasifica la nota como Nota Débito; Nature=2 la clasifica como Nota Crédito.; si state de GlosaObjectionsReceptionC → Mapea estado de recepción de objeción: 1=''Sin_Confirmar'', 2=''Confirmado_Radicado'', 3=''Oficio_Con_Respuesta'', 4=''Anulada''.; si state de GlosaPortfolioGlosada → Mapea el estado de la glosa entre 15 estados de proceso (pendiente confirmación, evaluación, oficio, reiteración, conciliación, glosa con respuesta, pago parcial, traslado a cobro jurídico, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Glosas.GlosaObjectionsReceptionC; Common.Customer; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZTrazabilidadCartera';
GO
