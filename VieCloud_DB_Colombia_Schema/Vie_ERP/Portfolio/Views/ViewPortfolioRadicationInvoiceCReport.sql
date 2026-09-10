

CREATE VIEW [Portfolio].[ViewPortfolioRadicationInvoiceCReport]
AS

select * from Portfolio.RadicateInvoiceC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que expone todos los registros de radicación de facturas de cartera tipo C por cliente (aseguradoras, pagadores o empresas). Consolida la información de cada radicado de cobro incluyendo consecutivo, fechas de radicación y documento, estado del trámite, usuario radicador, observaciones, fechas de confirmación y datos de auditoría de creación y modificación. Sirve como fuente para reportes de gestión de cartera, seguimiento de facturas radicadas ante pagadores y control del proceso de cobro y glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioRadicationInvoiceCReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioRadicationInvoiceCReport';
GO
