
CREATE VIEW [Authorization].[ViewAuthorizationOutsourcedServicesAdmissionInformation]
AS
SELECT	aos.Id, 
		aos.Id AuthorizationOutsourcedServicesId,
		aos.AdmissionNumber,
		CONCAT(cg.Code, ' - ', cg.Name) AS CareGroupCodeName, 
		CONCAT(ISNULL(tp.Nit, i.IPCODPACI), ' - ', ISNULL(tp.Name, p.IPNOMCOMP)) PatientCodeName
FROM [Authorization].AuthorizationOutsourcedServices aos
LEFT JOIN
(
	SELECT AuthorizationOutsourcedServicesId, MIN(CareGroupId) CareGroupId
	FROM
	(
		SELECT AuthorizationOutsourcedServicesId, CareGroupId
		FROM [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail
		UNION ALL
		SELECT AuthorizationOutsourcedServicesId, CareGroupId
		FROM [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail
	) qd
	GROUP BY AuthorizationOutsourcedServicesId
) aosd ON aos.Id = aosd.AuthorizationOutsourcedServicesId
LEFT JOIN .ADINGRESO i WITH (NOLOCK) ON i.NUMINGRES = aos.AdmissionNumber
LEFT JOIN .INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = i.IPCODPACI
LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = aosd.CareGroupId
LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = aos.ThirdPartyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de admisión asociada a cada autorización de servicio tercerizado (outsourcing). Para cada autorización, expone el número de ingreso del paciente, el grupo de atención (contrato) al que pertenece —obtenido a partir del detalle de órdenes de servicio o dispensaciones farmacéuticas— y la identificación del paciente o del tercero proveedor (NIT o cédula, con nombre). Sirve como fuente de consulta rápida para relacionar autorizaciones de outsourcing con el episodio de hospitalización o atención, el grupo contractual aplicable y los datos básicos de identificación del paciente o proveedor, facilitando reportes de auditoría, facturación y seguimiento de servicios contratados externamente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de admisión, paciente, tercero y grupo de atención asociada a cada autorización de servicios tercerizados, para visualización resumida.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La autorización de servicios tercerizados debe existir en Authorization.AuthorizationOutsourcedServices (tabla base del FROM).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Por cada autorización tercerizada se reporta a lo sumo un CareGroup, calculado como MIN(CareGroupId) sobre la unión de detalles farmacéuticos y de orden de servicio.; La identificación mostrada prioriza al tercero (ThirdParty) sobre el paciente de la admisión cuando ambos existen.; Se preservan todas las autorizaciones aunque no tengan admisión, paciente, detalle, grupo de atención o tercero asociados (todos los JOIN son LEFT).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios tercerizados; Admisión; Paciente; Tercero (proveedor/contratista); Grupo de atención; Dispensación farmacéutica; Orden de servicio', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewAuthorizationOutsourcedServicesAdmissionInformation: Devuelve por cada autorización tercerizada el código y nombre del grupo de atención (CONCAT(cg.Code,'' - '',cg.Name)) y del paciente/tercero (CONCAT(ISNULL(tp.Nit,i.IPCODPACI),'' - '',ISNULL(tp.Name,p.IPNOMCOMP))).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe ThirdPartyId asociado a la autorización (tp.Nit/tp.Name no nulos) → Se muestra el NIT y nombre del tercero como identificación del paciente/beneficiario else Se muestra el código del paciente (i.IPCODPACI) y su nombre completo (p.IPNOMCOMP) provenientes de la admisión; si Existen detalles en AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail o AuthorizationOutsourcedServicesServiceOrderDetail con CareGroupId → Se toma el menor CareGroupId (MIN) de la unión de ambos detalles para mostrar el grupo de atención else CareGroupCodeName queda nulo', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationOutsourcedServices; Authorization.AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail; Authorization.AuthorizationOutsourcedServicesServiceOrderDetail; ADINGRESO; INPACIENT; Contract.CareGroup; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAuthorizationOutsourcedServicesAdmissionInformation';
GO
