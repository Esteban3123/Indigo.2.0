
CREATE VIEW [dbo].[ViewAdmissionsToReportSlipOutReport]
  
AS

SELECT DISTINCT
sum(rcd.TotalFolio) as TotalFolio,LTRIM(RTRIM(ING.NUMINGRES)) AS AdmissionCode, ING.NUMINGRES AS AdmissionCodeWithOutTrim, ING.IFECHAING AS AdmissionDate, ING.TIPOINGRE AS AdmissionType, ING.GENCAREGROUP AS AdmissionCaregroupId
, ING.ICAUSAING AS AdmissionReason, ING.ITIPORIES AS AdmissionRiskType, CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE))) AS AdmissionCentAtencCodeName
, LTRIM(RTRIM(UFU.UFUCODIGO)) AS AdmissionUniFuncCode, LTRIM(RTRIM(UFU.UFUDESCRI)) AS AdmissionUniFuncName, CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))) AS AdmissionUniFuncCodeName, ING.IINGREPOR AS PlaceEntry, COALESCE (ING.CODPANATE, '') AS BenefitPlan
, COALESCE (ING.IAUTORIZA, '') AS AuthorizationNumber, COALESCE (ING.IPTELEFON, '') AS ResponsiblePhone, ING.IPRNOMBRE AS ResponsibleName, PAT.IPCODPACI AS PatientCode, PAT.IPFECNACI AS PatientBirth
, PAT.IPSEXOPAC AS PatientGenus, COALESCE (PAT.IPESTRATO, '') AS PatientEstrato, PAT.IPTIPOPAC AS PatientType, PAT.IPTIPOAFI AS PatientAfiliation, PAT.IPTIPODOC AS PatientDocumentType
, NIV.NIVCODIGO AS NivelCode, NIV.NIVDESCRI AS NivelName, NIV.NIVPORCMO AS NivelModeratorSharePercentage, NIV.NIVPORCOP AS NivelCoPayContribPercentage
, NIV.NIVPORSUB AS NivelCoPaySubsiPercentage, NIV.NIVPORVIN AS NivelCoPayVincuPercentage, NIV.NIVSISBEN AS NivelSisben, NIV.TOPEVECMO AS NivelModeratorShareTop
, NIV.TOPEVECOP AS NivelCoPayContribTop, NIV.TOPEVESUB AS NivelCoPaySubsibTop, NIV.TOPEVEVIN AS NivelCoPayVincuTop, NIV.TOPANUCMO AS NivelModeratorShareTopYear
, NIV.TOPANUCOP AS NivelCoPayContribTopYear, NIV.TOPANUSUB AS NivelCoPaySubsibTopYear, NIV.TOPANUVIN AS NivelCoPayVincuTopYear, ING.IESTADOIN AS Status, ENT.Code AS EntityCode
, ENT.Name AS EntityName, ING.GENCONENTITY AS HealthAdministratorId, '' AS PatientEntityCode, '' AS PatientEntityName, PAT.GENCONENTITY AS PatientEntityId, PAT.IPTELMOVI AS PatientPhone
, PAT.GENCAREGROUP AS PatientCareGroupId, LTRIM(RTRIM(PAT.IPNOMCOMP)) AS PatientName, LTRIM(RTRIM(ING.CODICAMHO)) AS BedStay, ING.ILIQUIDAC AS LiquidationType, '- Ingresos' AS StatusName
, EGRE.FECALTPAC As EgressDate, PRO.NOMMEDICO AS NameMedic, BS.Code As CodeSlipOut , BS.CreationDate AS DateRequest, BS.CreationUser as UserCreate
,ISNULL(OU.Id, 0) AS IdOperatingUnit
FROM dbo.ADINGRESO AS ING with(nolock)
inner join Billing.RevenueControl as rc on rc.AdmissionNumber = ING.NUMINGRES
inner join Billing.RevenueControlDetail as rcd on rcd.RevenueControlId = rc.Id
INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
INNER JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
INNER JOIN Contract.HealthAdministrator AS ENT with(nolock) ON ENT.Id = ING.GENCONENTITY
LEFT OUTER JOIN Billing.SlipOut AS BS with(nolock) ON BS.AdmissionNumber = ING.NUMINGRES
LEFT OUTER JOIN dbo.HCREGEGRE AS EGRE with(nolock) ON ING.NUMINGRES = EGRE.NUMINGRES
--LEFT OUTER JOIN Billing.Invoice bi with(nolock) on bi.AdmissionNumber = ING.NUMINGRES
LEFT OUTER JOIN dbo.INPROFSAL as PRO with(nolock) ON EGRE.CODPROSAL = PRO.CODPROSAL
LEFT JOIN Payroll.FunctionalUnit AS fu WITH (NOLOCK) ON UFU.UFUCODIGO = fu.Code
LEFT JOIN Payroll.BranchOffice AS bo WITH (NOLOCK) ON bo.Id = fu.BranchOfficeId
LEFT JOIN Common.City AS c WITH (NOLOCK) ON c.Id = bo.CityId
LEFT JOIN common.OperatingUnit AS OU WITH (NOLOCK) ON OU.IdCity = c.Id
group by LTRIM(RTRIM(ING.NUMINGRES)), ING.NUMINGRES, ING.IFECHAING, ING.TIPOINGRE, ING.GENCAREGROUP
, ING.ICAUSAING, ING.ITIPORIES, CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE)))
, LTRIM(RTRIM(UFU.UFUCODIGO)), LTRIM(RTRIM(UFU.UFUDESCRI)), CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))), ING.IINGREPOR, COALESCE (ING.CODPANATE, '')
, COALESCE (ING.IAUTORIZA, ''), COALESCE (ING.IPTELEFON, ''), ING.IPRNOMBRE, PAT.IPCODPACI, PAT.IPFECNACI
, PAT.IPSEXOPAC, COALESCE (PAT.IPESTRATO, ''), PAT.IPTIPOPAC, PAT.IPTIPOAFI, PAT.IPTIPODOC
, NIV.NIVCODIGO, NIV.NIVDESCRI, NIV.NIVPORCMO, NIV.NIVPORCOP
, NIV.NIVPORSUB, NIV.NIVPORVIN, NIV.NIVSISBEN, NIV.TOPEVECMO
, NIV.TOPEVECOP, NIV.TOPEVESUB, NIV.TOPEVEVIN, NIV.TOPANUCMO
, NIV.TOPANUCOP, NIV.TOPANUSUB, NIV.TOPANUVIN, ING.IESTADOIN, ENT.Code
, ENT.[Name], ING.GENCONENTITY, PAT.GENCONENTITY, PAT.IPTELMOVI
, PAT.GENCAREGROUP, LTRIM(RTRIM(PAT.IPNOMCOMP)), LTRIM(RTRIM(ING.CODICAMHO)), ING.ILIQUIDAC
, EGRE.FECALTPAC, PRO.NOMMEDICO, BS.Code, BS.CreationDate, BS.CreationUser ,ISNULL(OU.Id, 0)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de admisiones (ingresos) de pacientes que tienen comprobantes de salida (slip out) de facturación, integrando datos del episodio de ingreso, el paciente, la unidad funcional, el centro de atención, la EPS o entidad pagadora, el nivel de afiliación con sus topes y porcentajes de cuota moderadora y copago, el detalle de folios facturados y el egreso hospitalario con el médico tratante. Sirve como fuente principal para el reporte de slip out, permitiendo visualizar por cada admisión el total facturado, la fecha de alta, el código del comprobante de salida y los datos de auditoría de su creación, orientada a gestión de facturación, control de cobros y seguimiento de egresos de pacientes hospitalizados o en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToReportSlipOutReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToReportSlipOutReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de admisiones con su control de ingresos, datos del paciente, nivel de copago, centro de atención, unidad funcional, administradora de salud, egreso y comprobante de salida (slip out) para alimentar un reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe existir en ADINGRESO y tener al menos un registro en Billing.RevenueControl con su detalle en Billing.RevenueControlDetail (INNER JOIN).; El paciente referenciado en la admisión debe existir en INPACIENT.; El paciente debe tener un nivel de atención válido en ADNIVELES.; La admisión debe tener centro de atención válido en ADCENATEN y unidad funcional válida en INUNIFUNC.; La admisión debe estar asociada a una administradora de salud existente en Contract.HealthAdministrator.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan admisiones que tienen al menos un detalle de control de ingresos (RevenueControlDetail).; TotalFolio se entrega como suma agregada por admisión.; Campos opcionales como plan de beneficios, número de autorización, teléfono del responsable y estrato se normalizan a cadena vacía cuando son NULL.; Los códigos de admisión, unidad funcional, centro de atención, nombre del paciente y cama se entregan sin espacios (LTRIM/RTRIM).; El StatusName se fija siempre como ''- Ingresos''.; PatientEntityCode y PatientEntityName se devuelven siempre como cadena vacía.; Si no existe slip out o egreso para la admisión, esos campos quedan en NULL pero la admisión igual se reporta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión de paciente; Tipo y causa de ingreso; Tipo de riesgo de ingreso; Centro de atención; Unidad funcional; Plan de beneficios; Número de autorización; Responsable del paciente; Paciente (datos demográficos, sexo, estrato, tipo afiliación, tipo documento); Nivel de atención y porcentajes de cuota moderadora/copago (contributivo, subsidiado, vinculado); Topes de copago por evento y anuales; Sisbén; Administradora de salud (EPS/ARS/pagador); Cama de hospitalización; Tipo de liquidación; Egreso hospitalario; Médico tratante; Comprobante de salida (slip out) de facturación; Folio de facturación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewAdmissionsToReportSlipOutReport: Devuelve filas agrupadas por admisión sumando TotalFolio del detalle de control de ingresos; el comprobante de salida, egreso y médico de egreso son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; Billing.RevenueControl; Billing.RevenueControlDetail; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Billing.SlipOut; dbo.HCREGEGRE; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOutReport';
GO
