

CREATE VIEW [Billing].[ViewUpdateJournalVoucherToInvoice]
AS
	select 
			i.Id as InvoiceId,
			i.InvoiceNumber,
			jv.Id AS JournalVoucherId
	from Billing.Invoice AS i WITH(NOLOCK)
	JOIN GeneralLedger.JournalVouchers AS jv WITH(NOLOCK) ON i.Id =jv.EntityId and jv.EntityName ='Invoice'
	JOIN GeneralLedger.LegalBook AS lb WITH(NOLOCK) on jv.LegalBookId =lb.Id
	WHERE i.JournalVoucherId =0 and lb.OfficialBook =1 and NOT jv.Detail LIKE 'Anulación%'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que identifica facturas de venta que aún no tienen asociado un comprobante contable (voucher de diario) en el sistema de contabilidad. Cruza las facturas emitidas con los comprobantes registrados en el libro oficial contable, filtrando únicamente aquellos comprobantes que no corresponden a anulaciones y cuyas facturas todavía no tienen el vínculo contable registrado. Sirve como insumo para procesos de actualización o sincronización contable, permitiendo detectar y corregir facturas pendientes de enlazar con su respectivo comprobante en el libro mayor oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewUpdateJournalVoucherToInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewUpdateJournalVoucherToInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los pares factura–comprobante de diario oficial pendientes de enlace, para actualizar en la factura el JournalVoucherId correspondiente, excluyendo comprobantes de anulación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewUpdateJournalVoucherToInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.JournalVouchers con EntityName=''Invoice'' apuntando al Id de una factura cuyo JournalVoucherId aún sea 0.; El comprobante debe estar enlazado a un libro legal con OfficialBook=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewUpdateJournalVoucherToInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas que aún no han sido vinculadas a un comprobante de diario (JournalVoucherId = 0).; Solo se incluyen comprobantes asociados a un libro contable marcado como oficial (lb.OfficialBook = 1).; Se excluyen comprobantes cuyo detalle inicia con ''Anulación'' (NOT jv.Detail LIKE ''Anulación%'').; El emparejamiento entre factura y comprobante se establece por jv.EntityId = i.Id y jv.EntityName = ''Invoice''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewUpdateJournalVoucherToInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Comprobante de diario; Libro oficial; Anulación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewUpdateJournalVoucherToInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; GeneralLedger.JournalVouchers; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewUpdateJournalVoucherToInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewUpdateJournalVoucherToInvoice';
GO
