CREATE VIEW [MedicalFees].[ViewListMedicalFeesLiquidationDetail]
AS
SELECT mfc.AdmissionNumber,
       CONCAT(RTRIM(p.IPCODPACI), ' - ', p.IPNOMCOMP) AS PatientCode,
       CONCAT(t.Nit, ' - ', t.Name) AS ThirdPartyDescription,
       so.Code AS ServiceOrderCode,
       CONCAT(ips.Code, ' - ', ips.Name) AS IPSServiceName,
       mfc.AmountPayable,
       mfc.InvoiceQuantity,
       mfc.TotalAmountPayable,
       mfc.MedicalFeesContractValue,
       mfc.InvoiceReversal,
       mfc.Status AS StatusCausation,
       mfc.CausationDate,
       mfld.Id,
       mfld.LiquidationType,
       mfld.MedicalFeesLiquidacionId,
       mfld.MedicalFeesCausationId,
       sod.ServiceDate,
	   ind.InvoiceId,
	   mfc.InvoiceDetailId
FROM MedicalFees.MedicalFeesLiquidationDetail mfld
    INNER JOIN MedicalFees.MedicalFeesCausation mfc
        ON mfc.Id = mfld.MedicalFeesCausationId
    INNER JOIN Common.ThirdParty t
        ON t.Id = mfc.ThirdPartyId
    INNER JOIN Billing.ServiceOrder so
        ON so.Id = mfc.ServiceOrderId
    INNER JOIN Billing.ServiceOrderDetail sod
        ON sod.Id = mfc.ServiceOrderDetailId
    INNER JOIN Contract.IPSService ips
        ON ips.Id = sod.IPSServiceId
	INNER JOIN Billing.InvoiceDetail ind WITH(NOLOCK) 
		ON mfc.InvoiceDetailId = ind.Id
    LEFT JOIN dbo.INPACIENT p
        ON p.IPCODPACI = mfc.PatientCode;
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de liquidaciones de honorarios médicos, combinando cada línea de liquidación con su causación correspondiente, el paciente atendido (cédula y nombre), la entidad pagadora o tercero (NIT y razón social), la orden de servicio, el servicio o procedimiento realizado (código CUPS y nombre), la fecha del servicio, la factura asociada y los valores económicos clave: monto a pagar por unidad, cantidad facturada, total a pagar, valor del contrato de honorarios y si hubo reversión de factura. Sirve como fuente principal para reportes y consultas de liquidación de honorarios médicos, permitiendo rastrear qué se le pagó a cada profesional por cada servicio prestado a un paciente en un ingreso específico, cruzando información de causaciones, órdenes de servicio, detalle de factura y datos maestros del paciente y del tercero pagador.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListMedicalFeesLiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListMedicalFeesLiquidationDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista el detalle de liquidaciones de honorarios médicos junto con su causación, paciente, tercero, orden y detalle de servicio, servicio IPS y detalle de factura asociados.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada causación referenciada debe tener tercero, orden de servicio, detalle de orden de servicio, servicio IPS y detalle de factura existentes para aparecer en la vista.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de liquidación que tengan causación, tercero, orden de servicio, detalle de orden, servicio IPS y detalle de factura asociados (INNER JOIN).; El paciente es opcional: si no existe en el maestro de pacientes, el detalle de liquidación se sigue mostrando (LEFT JOIN).; La descripción del paciente y del tercero se presenta concatenando código y nombre con separador '' - ''.; La lectura del detalle de factura se hace con NOLOCK, permitiendo lecturas sucias sobre facturación.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de honorarios médicos; Causación de honorarios médicos; Paciente; Tercero; Orden de servicio; Servicio IPS; Factura / detalle de factura; Admisión', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] -: Devuelve el detalle de liquidación enriquecido con datos de causación, paciente (LEFT JOIN sobre dbo.INPACIENT por PatientCode), tercero, orden/detalle de servicio, servicio IPS y detalle de factura.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesLiquidationDetail; MedicalFees.MedicalFeesCausation; Common.ThirdParty; Billing.ServiceOrder; Billing.ServiceOrderDetail; Contract.IPSService; Billing.InvoiceDetail; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationDetail';
GO
