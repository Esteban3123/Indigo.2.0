
CREATE VIEW [dbo].[ZtrazabilidadCartera2]
AS
SELECT        TOP (100) PERCENT fact.Nit, fact.Entidad, fact.NroFactura, fact.FechaFactura, fact.ValorFactura, fact.SaldoFactura, fact.EstadoFactura, tras.ConsecTrasl, tras.FechaTraslado, tras.ValorTraslado, notd.ConsecNotaDebito, 
                         notd.FechaNotaDebito, notd.ValorNotaDebito, notc.ConsecNotaCredito, notc.FechaNotaCredito AS Expr1, notc.ValorNotaCredito, glos.ConsecRadObejcion, glos.Oficio, glos.NombreEntidad, glos.FechaRadicacion, 
                         glos.RadicadoNumero, glos.EstadoRecpObjecion, glos.NroFactura AS Expr3, glos.ValorEntidad, glos.SaldoFactura AS Expr4, glos.ValorAceptado1Estancia, glos.ValorReiterado, glos.SaldoValorReiterado, 
                         glos.ValorAceptado2Instancia, glos.ValorAceptadoConciliacionIPS, glos.ValorAceptadoConciliacionEPS, glos.ValorPendienteConciliacion, glos.ValorCobroJuridico, glos.EstadoGlosa, glos.RazonGlosa, glos.FechaGlosa, 
                         glos.RazonReiteracion, glos.FechaReiteracion, glos.RazonConciliacion, glos.FechaConciliacion, glos.JustificacionGlosa, glos.JustificacionReiteracion, glos.CodigoGlosa, glos.Detalle1, glos.CodigoGlosaEvalu, 
                         glos.Detalle2
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
                                                         d.InvoiceNumber AS NroFactura, d.InvoiceValueEntity AS ValorEntidad, d.BalanceInvoice AS SaldoFactura, f.ValueAcceptedFirstInstance AS ValorAceptado1Estancia, f.ValueReiterated AS ValorReiterado, 
                                                         f.ValueReiterationBalance AS SaldoValorReiterado, f.ValueAcceptedSecondInstance AS ValorAceptado2Instancia, f.ValueAcceptedIPSconciliation AS ValorAceptadoConciliacionIPS, 
                                                         f.ValueAcceptedEAPBconciliation AS ValorAceptadoConciliacionEPS, f.ValuePendingConciliation AS ValorPendienteConciliacion, f.LegalTransferValue AS ValorCobroJuridico, 
                                                         CASE WHEN f.state = 1 THEN 'Pendiente Evaluar Glosa' WHEN f.state = 2 THEN 'Glosa Evaluada' WHEN f.state = 3 THEN 'Pendiente Evaluar Reiteracion' WHEN f.state = 4 THEN 'Reiteracion Evaluada' WHEN f.state
                                                          = 5 THEN 'Pendiente Conciliar' WHEN f.state = 6 THEN 'Conciliado' END AS EstadoGlosa, f.RationaleGlosa AS RazonGlosa, f.RationaleDateGlosa AS FechaGlosa, f.RationaleReiteration AS RazonReiteracion, 
                                                         f.RationaleDateReiteration AS FechaReiteracion, f.RationaleConciliation AS RazonConciliacion, f.RationaleDateConciliation AS FechaConciliacion, f.JustificationGlosaText AS JustificacionGlosa, 
                                                         f.JustificationReiterationText AS JustificacionReiteracion, g.Code AS CodigoGlosa, g.NameSpecific AS Detalle1, h.Code AS CodigoGlosaEvalu, h.NameSpecific AS Detalle2
                               FROM            Glosas.GlosaObjectionsReceptionC AS a INNER JOIN
                                                         Common.Customer AS b ON a.CustomerId = b.Id INNER JOIN
                                                         Glosas.GlosaObjectionsReceptionD AS c ON c.GlosaObjectionsReceptionCId = a.Id INNER JOIN
                                                         Glosas.GlosaPortfolioGlosada AS d ON d.Id = c.PortfolioGlosaId INNER JOIN
                                                         Glosas.GlosaInvoiceDetail AS e ON e.ObjectionsReceptionDId = c.Id INNER JOIN
                                                         Glosas.GlosaMovementGlosa AS f ON f.InvoiceDetailId = e.Id LEFT OUTER JOIN
                                                         Common.ConceptGlosas AS g ON g.Id = f.CodeGlosaId LEFT OUTER JOIN
                                                         Common.ConceptGlosas AS h ON h.Id = f.IdGlosaEvaluation) AS glos ON fact.NroFactura = glos.NroFactura
ORDER BY fact.FechaFactura
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de trazabilidad de cartera que consolida, por número de factura, el ciclo completo de cobro a entidades pagadoras (EPS/aseguradoras): datos de la cuenta por cobrar, traslados de cartera confirmados, notas débito y crédito aprobadas, recibos de caja aplicados y el proceso de glosas (radicación de objeciones, estados de glosa, valores aceptados por instancia, reiteración, conciliación IPS/EAPB y cobro jurídico). Está orientada a reporting y seguimiento de gestión de cartera.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la trazabilidad de cartera por factura, integrando datos de la cuenta por cobrar con sus traslados, notas débito/crédito, recibos de caja y glosas/objeciones recibidas, traduciendo estados numéricos a descripciones legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por cobrar en Portfolio.AccountReceivable asociadas a terceros con PersonType = 2 (entidad/empresa).; Los movimientos de traslado, notas y recibos relevantes deben tener Status = 2 (confirmado/aprobado) para incluirse.; Las notas de cartera deben distinguir su naturaleza: Nature = 1 para débito, Nature = 2 para crédito.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas por cobrar de terceros con PersonType = 2 (entidades, no personas naturales).; Solo traslados, notas y recibos con Status = 2 son agregados; movimientos en otros estados se ignoran.; El valor trasladado por factura se calcula como SUM(Value) y la fecha/consecutivo del traslado mediante MAX (último traslado).; Las notas débito/crédito se separan estrictamente por la columna Nature (1=débito, 2=crédito).; La factura es el eje de unión: traslados, notas, recibos y glosas se anexan vía LEFT OUTER JOIN, por lo que una factura sin movimientos sigue apareciendo.; El estado PortfolioStatus tiene un mapeo defectuoso: el valor 1 está duplicado para ''Sin Radicar'' y ''Dificil Recaudo'', por lo que ''Dificil Recaudo'' nunca se mostrará.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cartera; Cuenta por cobrar; Traslado de cartera; Nota débito; Nota crédito; Recibo de caja; Glosa; Objeción de glosa; Radicación; Reiteración; Conciliación IPS; Conciliación EPS; Cobro jurídico; Entidad pagadora; Difícil recaudo; Certificación parcial; Devolución de factura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un dataset por factura con datos de cartera, traslados, notas débito, notas crédito y glosas, ordenado por FechaFactura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PortfolioStatus de la cuenta por cobrar → Se mapea a etiqueta: 1=''Sin Radicar'', 2=''Radicada Sin Confirmar'', 3=''Radicada Entidad'', 7=''Certificacion Parcial'', 8=''Devolucion Factura'', 1=''Dificil Recaudo'' (duplicado).; si Estado (state) de la recepción de objeciones de glosa → 1=''Sin_Confirmar'', 2=''Confirmado_Radicado'', 3=''Oficio_Con_Respuesta'', 4=''Anulada''.; si Estado (state) del movimiento de glosa → 1=''Pendiente Evaluar Glosa'', 2=''Glosa Evaluada'', 3=''Pendiente Evaluar Reiteracion'', 4=''Reiteracion Evaluada'', 5=''Pendiente Conciliar'', 6=''Conciliado''.; si Nature de la nota de cartera → Si Nature=1 se contabiliza como nota débito; si Nature=2 como nota crédito.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Glosas.GlosaObjectionsReceptionC; Common.Customer; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaInvoiceDetail; Glosas.GlosaMovementGlosa; Common.ConceptGlosas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ZtrazabilidadCartera2';
GO
