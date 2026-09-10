CREATE VIEW [Portfolio].[ViewAccountReceivableRadication]
AS
	SELECT	CAST(rid.Id AS VARCHAR(20)) Id, 
			ar.Id AccountReceivableId, 
			ri.RadicatedConsecutive, 
			ri.ConfirmDate RadicatedDate
	FROM Portfolio.AccountReceivable ar
	JOIN Portfolio.RadicateInvoiceD rid ON ar.InvoiceNumber = rid.InvoiceNumber
	JOIN Portfolio.RadicateInvoiceC ri ON rid.RadicateInvoiceCId = ri.Id
	WHERE rid.Devolution = 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada cuenta por cobrar con su radicado de cobro ante la entidad pagadora (EPS, aseguradora o empresa), mostrando el consecutivo del radicado y la fecha de confirmación. Cruza las cuentas por cobrar con el detalle de facturas radicadas y el encabezado del radicado, excluyendo las facturas que han sido devueltas. Sirve para consultar en qué radicado quedó registrada cada factura pendiente de cobro y cuándo fue confirmada su presentación, útil para seguimiento de cartera y glosas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewAccountReceivableRadication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewAccountReceivableRadication';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada cuenta por cobrar, las radicaciones de su factura ante el pagador (consecutivo y fecha de confirmación), excluyendo las marcadas como devolución.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia por InvoiceNumber entre Portfolio.AccountReceivable y Portfolio.RadicateInvoiceD para que la cuenta por cobrar aparezca en la vista.; El detalle de radicación debe tener asociado un encabezado válido (RadicateInvoiceCId) en Portfolio.RadicateInvoiceC.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone radicaciones cuya marca de devolución es 0 (no devueltas), excluyendo facturas devueltas por el pagador.; El cruce entre cuenta por cobrar y radicación se realiza por número de factura (InvoiceNumber), por lo que cada cuenta por cobrar se asocia a las radicaciones que comparten ese número.; La fecha de radicación reportada corresponde a la fecha de confirmación (ConfirmDate) del encabezado de radicación, no a la fecha de creación.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cuenta por cobrar; radicación de factura; consecutivo de radicado; fecha de confirmación de radicado; devolución de factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivable: Devuelve una fila por cada combinación cuenta por cobrar–detalle de radicación cuando InvoiceNumber coincide y rid.Devolution = 0; el Id del detalle se entrega como VARCHAR(20).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableRadication';
GO
