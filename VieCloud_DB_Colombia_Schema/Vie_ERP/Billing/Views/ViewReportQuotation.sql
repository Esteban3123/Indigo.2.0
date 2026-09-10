

CREATE VIEW [Billing].[ViewReportQuotation]
AS
SELECT        
	q.Id, 
	q.Code, 
	q.DocumentDate, 
	q.QuotationType, 

		CASE q.QuotationType 
		WHEN 1 THEN 'Intrahospitalario' 
		ELSE 'Ambulatoria' 
		END AS QuotationTypeName,

	q.AdmissionNumber, 
	q.Description, 
	q.OperatingUnitId, 
	q.Status, 
                         
		CASE q.Status WHEN 1 THEN 'Registrado' 
		WHEN 2 THEN 'Confirmado' 
		ELSE 'Anulado' 
		END AS StatusName, 
	
	q.StatusInAuthorization, 
	q.StatusInBilling, 
	q.ThirdPartyId, 
	tp.Nit, 
	tp.Nit + ' - ' + tp.Name AS ThirdPartyDescription,
	ISNULL(tp.Nit, i.IPCODPACI) PatientCode,
	ISNULL(tp.Name, p.IPNOMCOMP) PatientName,
	ISNULL(tp.Nit, i.IPCODPACI) + ' - ' + ISNULL(tp.Name, p.IPNOMCOMP) PatientDescription,
	ISNULL(cg.Code + ' - ' + cg.Name, '') CareGroupCodeName,
	q.CreationUser
FROM    Billing.Quotation AS q WITH (nolock) 
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de cotizaciones de facturación que consolida la información de encabezado de cada cotización (código, fecha, tipo, estado) junto con los datos del paciente o tercero (cédula, nombre completo) y el grupo de atención (contrato) asociado. Clasifica cada cotización como Intrahospitalaria o Ambulatoria y muestra su estado en el ciclo de vida: Registrado, Confirmado o Anulado. Integra las tablas de cotizaciones, detalles de dispensación farmacéutica, órdenes de servicio, ingresos/admisiones del paciente, datos maestros del paciente (INPACIENT/ADINGRESO), grupos de atención contractual y terceros (aseguradoras o entidades pagadoras). Se usa para reportería de gestión de cotizaciones, permitiendo identificar rápidamente quién cotizó, qué tipo de atención corresponde, bajo qué contrato o grupo de atención, y en qué estado se encuentra dentro del proceso de autorización y facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportQuotation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportQuotation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte de cotizaciones de facturación que consolida datos del encabezado con descripciones legibles de tipo y estado, identificación del paciente o tercero y el grupo de atención asociado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El CareGroup asociado a la cotización es único: cuando se obtiene desde los detalles, se toma el mínimo CareGroupId agrupado por QuotationId, unificando detalles farmacéuticos y de órdenes de servicio.; El tercero tiene prioridad sobre el paciente del ingreso para identificar al sujeto de la cotización (Nit/Name reemplazan a IPCODPACI/IPNOMCOMP cuando existen).; El GENCAREGROUP del ingreso prevalece sobre el CareGroup derivado de los detalles de la cotización.; QuotationType solo distingue dos categorías: 1=Intrahospitalario, cualquier otro valor se interpreta como Ambulatoria.; Status reconoce únicamente tres estados de negocio: Registrado (1), Confirmado (2) y Anulado (cualquier otro).; Las cotizaciones se exponen aunque no tengan ingreso, paciente, tercero, detalles o grupo de atención (todos los joins son LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cotización; Tipo de cotización (intrahospitalario/ambulatorio); Ingreso/Admisión; Paciente; Tercero; Grupo de atención (CareGroup); Estado de autorización; Estado de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewReportQuotation: Devuelve una fila por cotización (Billing.Quotation) con lectura NOLOCK, enriquecida con descripciones derivadas (QuotationTypeName, StatusName, ThirdPartyDescription, PatientDescription, CareGroupCodeName).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si q.QuotationType = 1 → QuotationTypeName = ''Intrahospitalario'' else QuotationTypeName = ''Ambulatoria''; si q.Status = 1 → StatusName = ''Registrado'' else Si Status=2 → ''Confirmado''; en otro caso → ''Anulado''; si tp.Nit IS NOT NULL (existe tercero asociado a la cotización) → PatientCode/PatientName/PatientDescription se toman del tercero (tp.Nit, tp.Name) else Se toman del paciente del ingreso (i.IPCODPACI, p.IPNOMCOMP); si i.GENCAREGROUP IS NOT NULL (el ingreso tiene grupo de atención) → CareGroup se resuelve por i.GENCAREGROUP else Se usa el menor CareGroupId encontrado en los detalles de cotización (farmacéuticos u órdenes de servicio)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Quotation; Billing.QuotationPharmaceuticalDispensingDetail; Billing.QuotationServiceOrderDetail; ADINGRESO; INPACIENT; Contract.CareGroup; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotation';
GO
