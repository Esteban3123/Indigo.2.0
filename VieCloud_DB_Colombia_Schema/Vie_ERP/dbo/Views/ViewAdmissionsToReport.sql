

CREATE VIEW [dbo].[ViewAdmissionsToReport]
  
AS

SELECT DISTINCT
LTRIM(RTRIM(ING.NUMINGRES)) AS AdmissionCode, ING.NUMINGRES AS AdmissionCodeWithOutTrim, ING.IFECHAING AS AdmissionDate, ING.TIPOINGRE AS AdmissionType, ING.GENCAREGROUP AS AdmissionCaregroupId
, ING.ICAUSAING AS AdmissionReason, ING.ITIPORIES AS AdmissionRiskType, CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE))) AS AdmissionCentAtencCodeName
, CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))) AS AdmissionUniFuncCodeName, ING.IINGREPOR AS PlaceEntry, COALESCE (ING.CODPANATE, '') AS BenefitPlan
, COALESCE (ING.IAUTORIZA, '') AS AuthorizationNumber, COALESCE (ING.IPTELEFON, '') AS ResponsiblePhone, ING.IPRNOMBRE AS ResponsibleName, PAT.IPCODPACI AS PatientCode, PAT.IPFECNACI AS PatientBirth
, PAT.IPSEXOPAC AS PatientGenus, COALESCE (PAT.IPESTRATO, '') AS PatientEstrato, PAT.IPTIPOPAC AS PatientType, PAT.IPTIPOAFI AS PatientAfiliation, PAT.IPTIPODOC AS PatientDocumentType
, NIV.NIVCODIGO AS NivelCode, NIV.NIVDESCRI AS NivelName, NIV.NIVPORCMO AS NivelModeratorSharePercentage, NIV.NIVPORCOP AS NivelCoPayContribPercentage
, NIV.NIVPORSUB AS NivelCoPaySubsiPercentage, NIV.NIVPORVIN AS NivelCoPayVincuPercentage, NIV.NIVSISBEN AS NivelSisben, NIV.TOPEVECMO AS NivelModeratorShareTop
, NIV.TOPEVECOP AS NivelCoPayContribTop, NIV.TOPEVESUB AS NivelCoPaySubsibTop, NIV.TOPEVEVIN AS NivelCoPayVincuTop, NIV.TOPANUCMO AS NivelModeratorShareTopYear
, NIV.TOPANUCOP AS NivelCoPayContribTopYear, NIV.TOPANUSUB AS NivelCoPaySubsibTopYear, NIV.TOPANUVIN AS NivelCoPayVincuTopYear, ING.IESTADOIN AS Status, '' AS EntityCode
, '' AS EntityName, ING.GENCONENTITY AS HealthAdministratorId, '' AS PatientEntityCode, '' AS PatientEntityName, PAT.GENCONENTITY AS PatientEntityId, PAT.IPTELMOVI AS PatientPhone
, PAT.GENCAREGROUP AS PatientCareGroupId, LTRIM(RTRIM(PAT.IPNOMCOMP)) AS PatientName, LTRIM(RTRIM(ING.CODICAMHO)) AS BedStay, ING.ILIQUIDAC AS LiquidationType, '- Ingresos' AS StatusName
FROM dbo.ADINGRESO AS ING with(nolock)
INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
INNER JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
inner join Billing.Invoice bi with(nolock) on bi.AdmissionNumber = ING.NUMINGRES
where ING.IESTADOIN = 'F' or ING.IESTADOIN = 'P'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las admisiones de pacientes (ingresos) que tienen factura asociada y se encuentran en estado Facturado o Pendiente, orientada a la generación de reportes de facturación y RIPS. Integra datos del episodio de ingreso (número de ingreso, fecha, tipo, causa, autorización, cama), información demográfica y de identificación del paciente (cédula, nombre, sexo, fecha de nacimiento, tipo de afiliación), nivel de copago y cuota moderadora aplicable, centro de atención, unidad funcional o servicio, y encabezado de factura. Sirve como fuente principal para reportes de cartera, auditoría de cuentas médicas y conciliación de ingresos facturados con la información clínica y administrativa del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida información de admisiones (ingresos) facturadas o pendientes, junto con datos de paciente, nivel de atención, centro y unidad funcional, para reportes administrativos y de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener paciente asociado en INPACIENT; El paciente debe estar asignado a un nivel de atención válido en ADNIVELES; El ingreso debe referenciar un centro de atención existente en ADCENATEN; El ingreso debe referenciar una unidad funcional existente en INUNIFUNC; Debe existir al menos una factura en Billing.Invoice asociada al número de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan admisiones con factura emitida (INNER JOIN con Billing.Invoice); Solo se incluyen admisiones en estado ''F'' o ''P''; Los códigos de admisión, centro, unidad funcional y nombre del paciente se entregan sin espacios en blanco al inicio o final (LTRIM/RTRIM); Campos opcionales (plan de beneficios, autorización, teléfono responsable, estrato) se devuelven como cadena vacía si son NULL; EntityCode, EntityName, PatientEntityCode y PatientEntityName se devuelven siempre vacíos; StatusName siempre se devuelve con valor literal ''- Ingresos''; Se eliminan duplicados por SELECT DISTINCT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso de paciente; Tipo de ingreso; Causa de ingreso; Tipo de riesgo de ingreso; Centro de atención; Unidad funcional; Plan de beneficios; Número de autorización; Paciente; Tipo de afiliación; Estrato socioeconómico; Nivel de atención; Cuota moderadora; Copago contributivo; Copago subsidiado; Copago vinculado; Topes de copago (evento y anual); SISBEN; Administradora de salud (EPS); Cama de estancia; Tipo de liquidación; Factura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve solo ingresos cuyo estado (IESTADOIN) sea ''F'' (facturado) o ''P'' (pendiente), y que tengan factura asociada en Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN = ''F'' OR ING.IESTADOIN = ''P'' → Se incluye el ingreso en el resultado else Se excluye del reporte', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; dbo.INUNIFUNC; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToReport';
GO
