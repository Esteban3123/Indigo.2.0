CREATE VIEW [Billing].[ViewRelatedInvoices]
AS
SELECT	CONCAT(rc.AdmissionNumber, '-', i.Id) Id, 
		rc.AdmissionNumber,
		i.Id InvoiceId,
		i.InvoiceNumber,
		i.InvoiceDate,
		i.ThirdPartySalesValue
FROM Billing.RevenueControl rc
JOIN Billing.RevenueControlDetail rcd ON rc.Id = rcd.RevenueControlId
JOIN Billing.Invoice i ON rcd.Id = i.RevenueControlDetailId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Facturas relacionadas a ingresos de pacientes: consolida en una sola consulta las facturas de cobro vinculadas a cada admisión, cruzando el control de ingresos (topes de cuota moderadora y copago) con el detalle de folios y las facturas emitidas. Para cada admisión muestra el número de ingreso, el identificador y número de factura, la fecha de facturación y el valor cobrado a terceros pagadores (EPS, aseguradoras, empresas). Sirve como base para reportes de facturación por ingreso, conciliación de cuentas y trazabilidad de todas las facturas asociadas a una misma hospitalización o atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRelatedInvoices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRelatedInvoices';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las facturas relacionadas a cada admisión, vinculándolas a través del control de ingresos y su detalle, para consultar número, fecha y valor de venta a terceros por admisión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en Billing.RevenueControl con su detalle (RevenueControlDetail) y al menos una factura en Billing.Invoice referenciando ese detalle para que la fila aparezca.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen facturas que tengan un detalle de control de ingresos asociado (INNER JOIN sobre RevenueControlDetailId), excluyendo facturas huérfanas.; Cada fila de la vista corresponde a una factura ligada a una única admisión vía su control de ingresos.; El identificador de la vista se construye concatenando el número de admisión con el Id de factura, garantizando unicidad por par (admisión, factura).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión; Control de ingresos; Folio de facturación; Factura; Venta a terceros', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Devuelve un conjunto de facturas (Id compuesto AdmissionNumber-InvoiceId, número de admisión, datos de la factura y valor de venta a terceros) resultado del JOIN entre RevenueControl, RevenueControlDetail e Invoice.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControl; Billing.RevenueControlDetail; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRelatedInvoices';
GO
