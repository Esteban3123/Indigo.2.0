CREATE VIEW [Billing].[ViewQuotationAdmissionInformation]
AS
SELECT	q.Id,
		q.Id QuotationId, 
		CONCAT(cg.Code, ' - ', cg.Name) AS CareGroupCodeName, 
		ISNULL(tp.Nit, i.IPCODPACI) + ' - ' + ISNULL(tp.Name, p.IPNOMCOMP) PatientCodeName
FROM Billing.Quotation q
LEFT JOIN
(
	SELECT QuotationId, MIN(CareGroupId) CareGroupId
	FROM
	(
		SELECT QuotationId, CareGroupId
		FROM Billing.QuotationPharmaceuticalDispensingDetail
		UNION ALL
		SELECT QuotationId, CareGroupId
		FROM Billing.QuotationServiceOrderDetail
	) qd
	GROUP BY QuotationId
) qd ON q.Id = qd.QuotationId
LEFT JOIN .ADINGRESO i ON q.AdmissionNumber = i.NUMINGRES
LEFT JOIN .INPACIENT p ON p.IPCODPACI = i.IPCODPACI
LEFT JOIN Contract.CareGroup cg ON cg.Id = ISNULL(i.GENCAREGROUP, qd.CareGroupId)
LEFT JOIN Common.ThirdParty tp ON q.ThirdPartyId = tp.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida información de encabezado de cotizaciones de facturación junto con los datos de la admisión del paciente o del tercero asociado. Para cada cotización muestra el grupo de atención (código y nombre) determinado a partir del ingreso del paciente o de los detalles de los ítems cotizados (servicios o dispensación farmacéutica), y la identificación con nombre del paciente o del tercero pagador (NIT/nombre de la entidad o cédula/nombre del paciente). Sirve como fuente de consulta rápida para pantallas y reportes de facturación que necesitan presentar, en una sola fila por cotización, quién es el paciente o entidad responsable y bajo qué grupo de atención o contrato se cotizó la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewQuotationAdmissionInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewQuotationAdmissionInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por cotización los datos descriptivos de grupo de atención y de paciente/tercero, combinando información del ingreso hospitalario y del detalle de la cotización para presentación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cotización debe existir en Billing.Quotation; el resto de relaciones (ingreso, paciente, grupo de atención, tercero) son opcionales por uso de LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El CareGroupId derivado del detalle de cotización es siempre el mínimo entre los presentes en dispensación farmacéutica y órdenes de servicio para esa cotización.; El grupo de atención del ingreso (GENCAREGROUP) tiene prioridad sobre el derivado de los detalles de la cotización.; La identificación del tercero (NIT) prevalece sobre la del paciente cuando ambas están presentes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cotización de facturación; Ingreso hospitalario (admisión); Paciente; Tercero (NIT); Grupo de atención (CareGroup); Dispensación farmacéutica; Orden de servicio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cotización con identificador, descriptivo de grupo de atención y descriptivo combinado código-nombre del paciente o tercero.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe valor en i.GENCAREGROUP (grupo de atención del ingreso) → Se usa el grupo de atención del ingreso para enlazar Contract.CareGroup else Se usa el menor CareGroupId encontrado en los detalles de la cotización (dispensación farmacéutica u orden de servicio); si Existe tercero asociado a la cotización (q.ThirdPartyId con registro en Common.ThirdParty) → El descriptivo del paciente/tercero usa NIT y nombre del tercero else Se usa el código y nombre del paciente del ingreso (INPACIENT vía ADINGRESO)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Quotation; Billing.QuotationPharmaceuticalDispensingDetail; Billing.QuotationServiceOrderDetail; ADINGRESO; INPACIENT; Contract.CareGroup; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewQuotationAdmissionInformation';
GO
