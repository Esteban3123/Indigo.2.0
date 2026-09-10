

CREATE VIEW [Authorization].[ViewReportAuthorizationOutsourcedServices]
AS
SELECT
	aos.Id, 
	aos.Code, 
	aos.DocumentDate, 
	IIF(aos.Type = 1, 'Intrahospitalario', 'Ambulatoria') AS TypeName, 
	aos.AdmissionNumber, 
	ISNULL(cg.Code + ' - ' + cg.Name, '') AS CareGroupCodeName, 
	ISNULL(fu.Code + ' - ' + fu.Name, '') AS FunctionalUnitCodeName, 
	tp.Nit ThirdPartyNit, 
	tp.Name AS ThirdPartyName, 
	ISNULL(tp.Nit, i.IPCODPACI) PatientCode, 
	ISNULL(tp.Name, p.IPNOMCOMP) PatientName, 
	aos.Description, 
	aos.OperatingUnitId, 
	CASE aos.Status 
		WHEN 1 THEN 'Registrado' 
		WHEN 2 THEN 'Confirmado' 
		ELSE 'Anulado' 
	END AS StatusName, 
	aos.StatusInAuthorization, 
	aos.StatusInBilling, 
	aos.CreationUser 
FROM [Authorization].AuthorizationOutsourcedServices aos WITH (NOLOCK) 
LEFT JOIN
(
	SELECT AuthorizationOutsourcedServicesId, MIN(CareGroupId) CareGroupId, MIN(FunctionalUnitId) FunctionalUnitId
	FROM
	(
		SELECT AuthorizationOutsourcedServicesId, CareGroupId, FunctionalUnitId
		FROM [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail
		UNION ALL
		SELECT AuthorizationOutsourcedServicesId, CareGroupId, PerformsFunctionalUnitId
		FROM [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail
	) qd
	GROUP BY AuthorizationOutsourcedServicesId
) aosd ON aos.Id = aosd.AuthorizationOutsourcedServicesId
LEFT JOIN .ADINGRESO i WITH (NOLOCK) ON i.NUMINGRES = aos.AdmissionNumber
LEFT JOIN .INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = i.IPCODPACI
LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = aosd.CareGroupId
LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fu.Id = aosd.FunctionalUnitId
LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = aos.ThirdPartyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida las autorizaciones de servicios tercerizados (outsourcing), integrando datos del encabezado de la autorización, el grupo de atención y la unidad funcional obtenidos del detalle de dispensación farmacéutica y de órdenes de servicio, el tercero proveedor o contratista externo, y —cuando aplica— el paciente vinculado a través del ingreso hospitalario. Muestra información clave como el código y fecha del documento, el tipo de atención (intrahospitalaria o ambulatoria), el número de ingreso, el NIT y nombre del tercero o del paciente, el estado del documento (Registrado, Confirmado o Anulado), el estado en el proceso de autorización y en facturación, y el usuario que lo creó. Sirve para reportería y seguimiento de autorizaciones emitidas a prestadores externos, permitiendo identificar el proveedor, el paciente atendido, la unidad funcional ejecutora y el avance en el ciclo de autorización y cobro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewReportAuthorizationOutsourcedServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewReportAuthorizationOutsourcedServices';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida autorizaciones de servicios tercerizados con su grupo de atención, unidad funcional, tercero proveedor y datos del paciente/ingreso para su explotación analítica.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las autorizaciones deben existir en Authorization.AuthorizationOutsourcedServices.; El AdmissionNumber debe coincidir con NUMINGRES en ADINGRESO para resolver el paciente vía INPACIENT.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El CareGroup y la FunctionalUnit reportados corresponden al mínimo Id encontrado entre los detalles de dispensación farmacéutica y de orden de servicio de la autorización.; Los detalles de dispensación farmacéutica y de orden de servicio se unifican (UNION ALL) compartiendo el mismo concepto de CareGroupId y FunctionalUnitId (PerformsFunctionalUnitId en orden de servicio).; Solo existen tres estados representables: Registrado (1), Confirmado (2) y Anulado (cualquier otro).; Solo existen dos tipos representables: Intrahospitalario (1) y Ambulatoria (cualquier otro).; Si no hay tercero asociado, la identificación del ''beneficiario'' del documento se sustituye por la del paciente del ingreso.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios tercerizados (outsourcing); Tipo de atención intrahospitalaria/ambulatoria; Grupo de atención (CareGroup); Unidad funcional; Tercero proveedor; Paciente e ingreso hospitalario; Estado de autorización (Registrado/Confirmado/Anulado); Estado en autorización y en facturación; Dispensación farmacéutica; Orden de servicio', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewReportAuthorizationOutsourcedServices: Devuelve una fila por cada autorización de servicio tercerizado, enriquecida con el menor CareGroupId y menor FunctionalUnitId asociados a sus detalles (dispensación farmacéutica u orden de servicio).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si aos.Type = 1 → TypeName = ''Intrahospitalario'' else TypeName = ''Ambulatoria''; si aos.Status = 1 → StatusName = ''Registrado'' else Si Status=2 → ''Confirmado''; cualquier otro valor → ''Anulado''; si Existe ThirdPartyId en la autorización (tp no nulo) → PatientCode/PatientName se toman del tercero (Nit/Name) else Se usan los datos del paciente desde INPACIENT (IPCODPACI/IPNOMCOMP)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationOutsourcedServices; Authorization.AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail; Authorization.AuthorizationOutsourcedServicesServiceOrderDetail; ADINGRESO; INPACIENT; Contract.CareGroup; Payroll.FunctionalUnit; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServices';
GO
