
CREATE VIEW [dbo].[ViewAdmissionsToReportSlipOut]  
AS
SELECT
    LTRIM(RTRIM(ING.NUMINGRES)) AS AdmissionCode,
    ING.NUMINGRES AS AdmissionCodeWithOutTrim,
    ING.IFECHAING AS AdmissionDate,
    ING.TIPOINGRE AS AdmissionType,
    ING.GENCAREGROUP AS AdmissionCaregroupId,
    ING.ICAUSAING AS AdmissionReason,
    ING.ITIPORIES AS AdmissionRiskType,
    CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE))) AS AdmissionCentAtencCodeName,
    CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))) AS AdmissionUniFuncCodeName,
    c.Name AS CityName,
    ING.IINGREPOR AS PlaceEntry,
    COALESCE(ING.CODPANATE, '') AS BenefitPlan,
    COALESCE(ING.IAUTORIZA, '') AS AuthorizationNumber,
    COALESCE(ING.IPTELEFON, '') AS ResponsiblePhone,
    ING.IPRNOMBRE AS ResponsibleName,
    PAT.IPCODPACI AS PatientCode,
    PAT.IPFECNACI AS PatientBirth,
    PAT.IPSEXOPAC AS PatientGenus,
    COALESCE(PAT.IPESTRATO, '') AS PatientEstrato,
    PAT.IPTIPOPAC AS PatientType,
    PAT.IPTIPOAFI AS PatientAfiliation,
    PAT.IPTIPODOC AS PatientDocumentType,
    NIV.NIVCODIGO AS NivelCode,
    NIV.NIVDESCRI AS NivelName,
    NIV.NIVPORCMO AS NivelModeratorSharePercentage,
    NIV.NIVPORCOP AS NivelCoPayContribPercentage,
    NIV.NIVPORSUB AS NivelCoPaySubsiPercentage,
    NIV.NIVPORVIN AS NivelCoPayVincuPercentage,
    NIV.NIVSISBEN AS NivelSisben,
    NIV.TOPEVECMO AS NivelModeratorShareTop,
    NIV.TOPEVECOP AS NivelCoPayContribTop,
    NIV.TOPEVESUB AS NivelCoPaySubsibTop,
    NIV.TOPEVEVIN AS NivelCoPayVincuTop,
    NIV.TOPANUCMO AS NivelModeratorShareTopYear,
    NIV.TOPANUCOP AS NivelCoPayContribTopYear,
    NIV.TOPANUSUB AS NivelCoPaySubsibTopYear,
    NIV.TOPANUVIN AS NivelCoPayVincuTopYear,
    ING.IESTADOIN AS Status,
    ENT.Code AS EntityCode,
    ENT.Name AS EntityName,
    ING.GENCONENTITY AS HealthAdministratorId,
    '' AS PatientEntityCode,
    '' AS PatientEntityName,
    PAT.GENCONENTITY AS PatientEntityId,
    PAT.IPTELMOVI AS PatientPhone,
    PAT.GENCAREGROUP AS PatientCareGroupId,
    LTRIM(RTRIM(PAT.IPNOMCOMP)) AS PatientName,
    LTRIM(RTRIM(ISNULL(h.DESCCAMAS, ING.CODICAMHO))) AS BedStay,
    ING.ILIQUIDAC AS LiquidationType,
    '- Ingresos' AS StatusName,
    ISNULL(EGRE.FECALTPAC, CONVERT(date, '19000101')) AS EgressDate,
    PRO.NOMMEDICO AS NameMedic,
    BS.Code AS CodeSlipOut,
    ISNULL(BS.CreationDate, CONVERT(date, '19000101')) AS DateRequest,
    BS.CreationUser AS UserCreate,
    OU.Id AS IdOperatingUnit
FROM dbo.ADINGRESO AS ING
INNER JOIN dbo.INPACIENT AS PAT ON ING.IPCODPACI = PAT.IPCODPACI
INNER JOIN dbo.ADNIVELES AS NIV ON PAT.NIVCODIGO = NIV.NIVCODIGO
INNER JOIN dbo.ADCENATEN AS CEN ON ING.CODCENATE = CEN.CODCENATE
INNER JOIN Contract.HealthAdministrator AS ENT ON ENT.Id = ING.GENCONENTITY
LEFT JOIN Billing.SlipOut AS BS ON BS.AdmissionNumber = ING.NUMINGRES
LEFT JOIN dbo.HCREGEGRE AS EGRE ON ING.NUMINGRES = EGRE.NUMINGRES
LEFT JOIN dbo.INPROFSAL AS PRO ON EGRE.CODPROSAL = PRO.CODPROSAL
OUTER APPLY
(
    SELECT TOP (1)
        e.CODICAMAS
    FROM dbo.CHREGESTA AS e
    WHERE e.NUMINGRES = ING.NUMINGRES
    ORDER BY e.ID DESC
) AS EStancia
LEFT JOIN dbo.CHCAMASHO AS h ON EStancia.CODICAMAS = h.CODICAMAS
LEFT JOIN dbo.INUNIFUNC AS UFU ON ISNULL(h.UFUCODIGO, ING.UFUCODIGO) = UFU.UFUCODIGO
LEFT JOIN Payroll.FunctionalUnit AS fu ON UFU.UFUCODIGO = fu.Code
LEFT JOIN Payroll.BranchOffice AS bo ON bo.Id = fu.BranchOfficeId
LEFT JOIN Common.City AS c ON c.Id = bo.CityId
LEFT JOIN common.OperatingUnit AS OU ON OU.IdCity = c.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ingresos (admisiones) de pacientes que deben ser reportados en comprobantes de salida (Slip Out) para facturación. Integra datos del episodio de ingreso (tipo de ingreso, causa, estado, fecha), información del paciente (identificación, nombre, fecha de nacimiento, sexo, afiliación, tipo de documento), nivel de cobertura con sus porcentajes y topes de cuota moderadora y copagos, centro de atención, unidad funcional, ciudad, administradora de salud (EPS/pagador), egreso hospitalario con fecha de alta y médico tratante, y el comprobante de salida (Slip Out) asociado con su código, fecha y usuario de creación. Sirve como fuente principal para la generación y reporte de comprobantes de egreso o remisiones de facturación, permitiendo conocer qué ingresos ya tienen Slip Out generado y cuáles están pendientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToReportSlipOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToReportSlipOut';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de admisiones (paciente, nivel, centro, administradora, cama actual, egreso y comprobante de salida) para listar ingresos candidatos a generar/asociar un slip out de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada admisión debe tener paciente válido (INNER JOIN con INPACIENT); El paciente debe tener un nivel de atención registrado (INNER JOIN con ADNIVELES); La admisión debe tener centro de atención válido (INNER JOIN con ADCENATEN); La admisión debe tener una administradora de salud asociada en Contract.HealthAdministrator vía GENCONENTITY', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen admisiones que tengan paciente, nivel, centro y administradora de salud válidos; El estado de cama/unidad funcional reflejado corresponde al estado más reciente del ingreso (mayor ID en CHREGESTA); El campo StatusName se fija siempre en ''- Ingresos''; PatientEntityCode y PatientEntityName se devuelven siempre vacíos; El slip out, egreso, factura y profesional pueden no existir (LEFT JOIN), por lo que la admisión se lista aunque aún no tenga comprobante de salida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso; Paciente; Nivel de atención; Copago contributivo/subsidiado/vinculado; Cuota moderadora; Topes de copago anuales y por evento; Centro de atención; Unidad funcional; Administradora de salud (EPS); Plan de beneficios; Autorización; Estrato; Afiliación; Sisben; Cama hospitalaria; Egreso/Alta; Profesional de salud; Slip out (comprobante de salida); Factura; Liquidación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewAdmissionsToReportSlipOut: Devuelve una fila distinta por admisión enriquecida con datos de paciente, nivel, centro, administradora, cama actual, egreso, profesional de alta y slip out.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(h.DESCCAMAS, ING.CODICAMHO) → Toma la descripción de la cama del último estado (CHREGESTA → CHCAMASHO); si no existe, usa la cama registrada en la admisión; si ISNULL(h.UFUCODIGO, ING.UFUCODIGO) → La unidad funcional se toma de la cama actual del último estado; si no existe, se usa la unidad funcional de la admisión; si Subconsulta MAX(e.ID) por NUMINGRES en CHREGESTA → Se selecciona únicamente el último registro de estado/estancia del paciente para determinar cama y unidad funcional vigentes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; Contract.HealthAdministrator; Billing.SlipOut; dbo.HCREGEGRE; Billing.Invoice; dbo.INPROFSAL; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; Payroll.FunctionalUnit; Payroll.BranchOffice; Common.City', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReportSlipOut';
GO
