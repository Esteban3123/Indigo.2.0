

CREATE view [Billing].[InvoiceWithoutElectronicDocument] as (
SELECT Id, InvoiceNumber from Billing.Invoice
where Id not in (select EntityId from Billing.ElectronicDocument where EntityName = 'Invoice')
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Facturas de venta que aún no tienen documento electrónico generado ante la DIAN. Identifica las facturas registradas en el sistema que no cuentan con su correspondiente factura electrónica (XML/CUFE) emitida, cruzando la tabla de facturas con la de documentos electrónicos. Es útil para controlar y gestionar pendientes de facturación electrónica, detectar facturas sin enviar a la DIAN y garantizar el cumplimiento de la obligación de facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'InvoiceWithoutElectronicDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'InvoiceWithoutElectronicDocument';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las facturas que aún no tienen un documento electrónico asociado registrado ante la DIAN.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Billing.Invoice debe contener las facturas a evaluar; Billing.ElectronicDocument debe usar ''Invoice'' como valor de EntityName para asociar documentos electrónicos a facturas', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos electrónicos cuyo EntityName sea exactamente ''Invoice'' para determinar la relación con facturas; Una factura se considera sin documento electrónico cuando no existe ningún registro en Billing.ElectronicDocument que la referencie como entidad ''Invoice''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Documento electrónico; Facturación electrónica DIAN', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Devuelve Id e InvoiceNumber de las facturas cuyo Id NO aparece en Billing.ElectronicDocument con EntityName=''Invoice''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.ElectronicDocument', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'InvoiceWithoutElectronicDocument';
GO
