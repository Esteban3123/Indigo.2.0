
CREATE VIEW [MedicalFees].[ViewListPaymentsMedicalFeesLiquidation]
AS
SELECT mfc.Id as MedicalFeesCausationId,
       mfc.AdmissionNumber,
       mfc.PatientCode,
       CONCAT(RTRIM(mfc.PatientCode), ' - ', RTRIM(p.IPNOMCOMP)) AS PatientDescription,
       mfc.HealthProfessionalCode,
       mfc.ThirdPartyId,
       CONCAT(t.Nit, ' - ', t.Name) AS ThirdPartyDescription,
       mfc.MedicalFeesContractId,
       mfc.CausationDate,
       mfc.ServiceOrderId,
       so.Code AS CodeServiceOrder,
       mfc.ServiceOrderDetailId,
       mfc.AmountPayable,
       mfc.MedicalFeesContractValue,
       mfc.InvoiceQuantity,
       mfc.TotalAmountPayable,
       mfc.MedicalFeePaid,
       mfc.InvoiceReversal,
       mfc.ReassessmentForReversal,
       mfc.ReassessmentForObjection,
       mfc.ObjectionAccepted,
       mfc.Status,
       CONCAT(mfc.AdmissionNumber, ' - ', mfc.PatientCode) AS AdmissionNumberPatientCode,
       CONCAT(ips.Code, ' - ', ips.Name) AS IPSServiceName,
       sod.ServiceDate,
	   ind.InvoiceId,
	   mfc.InvoiceDetailId
FROM [MedicalFees].MedicalFeesCausation mfc WITH (NOLOCK)
    INNER JOIN [Common].ThirdParty t WITH (NOLOCK)
        ON mfc.ThirdPartyId = t.Id
    INNER JOIN [Billing].ServiceOrder so WITH (NOLOCK)
        ON mfc.ServiceOrderId = so.Id
    INNER JOIN [Billing].ServiceOrderDetail sod WITH (NOLOCK)
        ON mfc.ServiceOrderDetailId = sod.Id
    INNER JOIN [Contract].IPSService ips WITH (NOLOCK)
        ON sod.IPSServiceId = ips.Id
	INNER JOIN Billing.InvoiceDetail ind WITH(NOLOCK) ON mfc.InvoiceDetailId = ind.Id
    LEFT JOIN dbo.INPACIENT p WITH (NOLOCK)
        ON mfc.PatientCode = p.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la liquidación de honorarios médicos causados, integrando la causación de honorarios con los datos del paciente (cédula y nombre), el tercero pagador (NIT y razón social), la orden de servicio, el detalle del servicio prestado (código y nombre CUPS), la fecha de prestación y el detalle de factura asociado. Muestra los valores clave del pago: monto a pagar, valor del contrato de honorarios, cantidad facturada, total a pagar, estado del pago, reversiones, revaluaciones y objeciones aceptadas. Sirve para consultar y reportar el estado de pagos de honorarios médicos por profesional, paciente, ingreso, contrato y tercero pagador, permitiendo trazabilidad completa desde la causación hasta la factura.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListPaymentsMedicalFeesLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado consolidado de causaciones de honorarios médicos con datos descriptivos del paciente, tercero, orden de servicio, servicio IPS y factura asociada, para soporte de la liquidación de pagos.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La causación debe tener un tercero existente en Common.ThirdParty; La causación debe estar asociada a una orden de servicio (Billing.ServiceOrder) y a un detalle de orden (Billing.ServiceOrderDetail); El detalle de orden debe estar vinculado a un servicio IPS del catálogo (Contract.IPSService); La causación debe tener un detalle de factura asociado en Billing.InvoiceDetail', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen causaciones que tengan correspondencia obligatoria con tercero, orden de servicio, detalle de orden, servicio IPS y detalle de factura (INNER JOIN); La descripción del paciente combina código y nombre completo, aun cuando el paciente no exista en el maestro (LEFT JOIN permite nulos); La descripción del tercero siempre concatena NIT y nombre; La descripción del servicio IPS siempre concatena código y nombre; Se expone el identificador de factura (InvoiceId) tomado desde el detalle de factura asociado a la causación', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación de honorarios médicos; Liquidación de pagos a profesionales; Paciente; Tercero (NIT); Orden de servicio; Detalle de orden de servicio; Servicio IPS; Factura / detalle de factura; Reverso de factura; Reliquidación por reverso; Reliquidación por objeción; Objeción aceptada; Admisión', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada causación de honorarios médicos que tenga vínculos válidos con tercero, orden de servicio, detalle de orden, servicio IPS y detalle de factura; el paciente puede no existir (LEFT JOIN a INPACIENT).', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesCausation; Common.ThirdParty; Billing.ServiceOrder; Billing.ServiceOrderDetail; Contract.IPSService; Billing.InvoiceDetail; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListPaymentsMedicalFeesLiquidation';
GO
