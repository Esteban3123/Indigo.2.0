CREATE VIEW [Portfolio].[ViewRadicateDetailInvoiceCapitated]
AS
SELECT        rd.RadicateInvoiceCId, i.Id, i.OperatingUnitId, i.DocumentType, i.InvoiceNumber, i.RevenueControlDetailId, i.AdmissionNumber, i.HealthAdministratorId, i.ThirdPartyId, i.PatientCode, i.CareGroupId, i.InvoiceDate, 
                         i.InvoiceExpirationDate, i.TotalInvoice, i.CapitationInitialDate, i.CapitationEndDate, i.CapitationlPatientsAmount, i.CapitationPatientValue, i.ThirdPartySalesValue, i.ThirdPartyDiscountValue, 
                         i.ResponsibleRecoveryFee, i.TotalPatientSalesPrice, i.PatientDiscount, i.PatientDiscountPercentage, i.TotalPatientWithDiscount, i.ValueVoucher, i.CashReceiptId, i.JournalVoucherId, i.PatientPaidValue, 
                         i.ThirdPartyAccountReceivableValue, i.PatientAccountReceivableValue, i.PatientAccountReceivableId, i.PatientType, i.PatientAffiliatedType, i.PatientPaidAbility, i.PatientSocialClass, i.CREETaxRetentionValue, 
                         i.CREETaxRetentionBaseValue, i.Observation, i.InvoiceCategoryId, i.Status, i.InvoicedUser, i.InvoicedDate, i.AnnulmentUser, i.AnnulmentDate, i.ReversalReasonId, i.DescriptionReversal, i.TimeStamp
FROM            Portfolio.RadicateInvoiceD AS rd INNER JOIN
                         Billing.Invoice AS i ON rd.InvoiceNumber = i.InvoiceNumber
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que combina el detalle de facturas radicadas ante entidades pagadoras (EPS, aseguradoras) con la información completa del encabezado de cada factura de capitación emitida. Une la tabla de radicación con la factura correspondiente para exponer en un solo resultado los datos del paciente, el número de ingreso, los valores de capitación (valor por afiliado, fechas del período, cantidad de pacientes capitados), los descuentos, saldos por cobrar al tercero y al paciente, y el estado de la factura. Sirve para reportería de cartera y seguimiento de cobro de contratos de capitación radicados ante pagadores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewRadicateDetailInvoiceCapitated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewRadicateDetailInvoiceCapitated';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de facturas capitadas radicadas ante pagadores cruzando la radicación con los datos de la factura emitida (valores, capitación, estados y cuentas por cobrar).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una factura en Billing.Invoice cuyo InvoiceNumber coincida con el registrado en Portfolio.RadicateInvoiceD para que la fila sea visible.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone facturas que cuentan con un detalle de radicación asociado (INNER JOIN entre RadicateInvoiceD e Invoice).; El emparejamiento entre radicación y factura se realiza exclusivamente por InvoiceNumber, no por Id.; Cada fila representa la combinación de una factura de venta con su renglón de radicación correspondiente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Facturación capitada; Capitación (período y valor por paciente); Entidades pagadoras (EPS/aseguradoras); Cuentas por cobrar a tercero y a paciente; Anulación y reversión de facturas', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.RadicateInvoiceD: Devuelve únicamente facturas que tienen al menos un registro en Portfolio.RadicateInvoiceD coincidente por InvoiceNumber; las facturas sin radicación quedan excluidas por el INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceD; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateDetailInvoiceCapitated';
GO
