
CREATE VIEW [Budget].[ViewReportListRadicate]
AS

select ROW_NUMBER() OVER(ORDER BY c.Regimen, c.Tipo) as consecutivo, c.Regimen, c.Tipo, c.valorRC as ValorRC, c.ValorLiquidacion as ValorLiquidacion,
c.valorRC - c.ValorLiquidacion as Diferencia, c.DocumentDate
from
(
select   Budget.fnEntityType(cg.EntityType) as Regimen, Budget.fnCollectionType(i.DocumentType) as Tipo, cr.DocumentDate, 
ipa.Value as ValorLiquidacion, cr.Value as ValorRC
from Treasury.CashReceipts as cr inner join  Portfolio.PortfolioAdvance as pa on cr.id = pa.CashReceiptId
inner join Billing.InvoicePortfolioAdvance as ipa on ipa.PortfolioAdvanceId = pa.Id inner join Billing.Invoice as i on i.Id = ipa.InvoiceId  and i.[Status] =1
inner join [Contract].CareGroup as cg on i.CareGroupId = cg.Id 
 where (cr.[Status] = 2) or
(cr.[Status] = 4 and month(cr.ReversedDate) <> month(cr.DocumentDate))
UNION all
select  'Particulares' as Regimen, 'Ventas de Contado Productos' as Tipo, cr.DocumentDate, 
crar.Value as ValorLiquidacion, cr.Value as ValorRC
from Treasury.CashReceipts as cr inner join Treasury.CashReceiptDetails as crd on cr.Id = crd.IdCashReceipt
inner join Treasury.CashReceiptAccountReceivable as crar on crd.id = crar.CashReceiptDetailId
inner join Billing.Invoice as i on i.InvoiceNumber = crar.InvoiceNumber 
 where (cr.[Status] = 2) or
(cr.[Status] = 4 and month(cr.ReversedDate) <> month(cr.DocumentDate))
) as c
--group by c.Regimen, c.Tipo
--order by Regimen
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de recaudos radicados que consolida los cobros efectivos registrados en tesorería, cruzando recibos de caja con anticipos de cartera aplicados a facturas y con pagos directos a cuentas por cobrar de facturas de contado. Para cada registro muestra el régimen del pagador (tipo de entidad según el grupo de atención del contrato), el tipo de recaudo o cobro, la fecha del recibo de caja, el valor liquidado en la factura y el valor real del recibo, calculando la diferencia entre ambos. Incluye recibos en estado activo y aquellos anulados en un mes distinto al que fueron emitidos, lo que permite identificar inconsistencias entre lo facturado y lo efectivamente cobrado. Sirve para reportería de presupuesto, conciliación de cartera y seguimiento de recaudo por régimen y tipo de cobro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewReportListRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewReportListRadicate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista comparativa entre el valor de recibos de caja y el valor liquidado/aplicado a facturas, segmentado por régimen y tipo de recaudo, para conciliar diferencias en la radicación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las funciones Budget.fnEntityType y Budget.fnCollectionType deben estar definidas para mapear códigos a régimen y tipo legible.; Las facturas deben tener Status=1 (activas) para incluirse en el primer subconjunto.; Para ventas de contado por número de factura, debe existir correspondencia entre crar.InvoiceNumber e Invoice.InvoiceNumber.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran recibos en estado 2 (vigentes) o estado 4 (reversados) cuando la reversión cae en mes distinto al de emisión.; Las ventas de contado de productos siempre se etiquetan como régimen ''Particulares''.; El consecutivo se asigna ordenando por Regimen y luego Tipo.; La diferencia reportada es ValorRC menos ValorLiquidacion (sin valor absoluto).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibo de caja; Anticipo de cartera; Factura; Régimen; Grupo de atención (CareGroup); Reversión de recibo; Ventas de contado; Liquidación; Radicación; Particulares', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.ViewReportListRadicate: Devuelve filas con consecutivo, Regimen, Tipo, ValorRC, ValorLiquidacion, Diferencia (ValorRC - ValorLiquidacion) y DocumentDate, ordenadas por Regimen y Tipo.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cr.[Status] = 2 → Incluye el recibo de caja en el reporte (recibo aplicado/vigente).; si cr.[Status] = 4 AND MONTH(cr.ReversedDate) <> MONTH(cr.DocumentDate) → Incluye el recibo aunque esté reversado, siempre que la reversión ocurra en un mes distinto al de emisión. else Si Status=4 y la reversión es en el mismo mes que la emisión, se excluye del reporte.; si Origen del recaudo vinculado a anticipo de cartera (PortfolioAdvance) con factura activa (Invoice.Status=1) → Clasifica la fila usando Budget.fnEntityType(cg.EntityType) como Régimen y Budget.fnCollectionType(i.DocumentType) como Tipo. else Si proviene de detalle de recibo aplicado a cuenta por cobrar por InvoiceNumber, clasifica como Régimen=''Particulares'' y Tipo=''Ventas de Contado Productos''.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.fnEntityType; Budget.fnCollectionType', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Portfolio.PortfolioAdvance; Billing.InvoicePortfolioAdvance; Billing.Invoice; Contract.CareGroup; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportListRadicate';
GO
