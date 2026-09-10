

CREATE VIEW [dbo].[ViewAdmissionsToLiquidationConfirm]
  
AS

/*SELECT DISTINCT
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
FROM dbo.ADINGRESO AS ING 
INNER JOIN dbo.INPACIENT AS PAT ON ING.IPCODPACI = PAT.IPCODPACI
INNER JOIN dbo.ADNIVELES AS NIV ON PAT.NIVCODIGO = NIV.NIVCODIGO
INNER JOIN dbo.ADCENATEN AS CEN ON ING.CODCENATE = CEN.CODCENATE
INNER JOIN dbo.INUNIFUNC AS UFU ON ING.UFUCODIGO = UFU.UFUCODIGO
where ING.IESTADOIN = 'F'*/

SELECT DISTINCT
LTRIM(RTRIM(ING.NUMINGRES)) AS AdmissionCode, ING.NUMINGRES AS AdmissionCodeWithOutTrim, ING.IFECHAING AS AdmissionDate, ING.TIPOINGRE AS AdmissionType, ING.GENCAREGROUP AS AdmissionCaregroupId
, ING.ICAUSAING AS AdmissionReason, ING.ITIPORIES AS AdmissionRiskType, CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE))) AS AdmissionCentAtencCodeName
, CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))) AS AdmissionUniFuncCodeName, ING.IINGREPOR AS PlaceEntry, LTRIM(RTRIM(COALESCE (ING.CODPANATE, ''))) AS BenefitPlan
, COALESCE (ING.IAUTORIZA, '') AS AuthorizationNumber, COALESCE (ING.IPTELEFON, '') AS ResponsiblePhone, ING.IPRNOMBRE AS ResponsibleName, PAT.IPCODPACI AS PatientCode, PAT.IPFECNACI AS PatientBirth
, PAT.IPSEXOPAC AS PatientGenus, COALESCE (PAT.IPESTRATO, '0') AS PatientEstrato, PAT.IPTIPOPAC AS PatientType, PAT.IPTIPOAFI AS PatientAfiliation, PAT.IPTIPODOC AS PatientDocumentType
, NIV.NIVCODIGO AS NivelCode, LTRIM(RTRIM(NIV.NIVDESCRI)) AS NivelName, NIV.NIVPORCMO AS NivelModeratorSharePercentage, NIV.NIVPORCOP AS NivelCoPayContribPercentage
, NIV.NIVPORSUB AS NivelCoPaySubsiPercentage, NIV.NIVPORVIN AS NivelCoPayVincuPercentage, NIV.NIVSISBEN AS NivelSisben, NIV.TOPEVECMO AS NivelModeratorShareTop
, NIV.TOPEVECOP AS NivelCoPayContribTop, NIV.TOPEVESUB AS NivelCoPaySubsibTop, NIV.TOPEVEVIN AS NivelCoPayVincuTop, NIV.TOPANUCMO AS NivelModeratorShareTopYear
, NIV.TOPANUCOP AS NivelCoPayContribTopYear, NIV.TOPANUSUB AS NivelCoPaySubsibTopYear, NIV.TOPANUVIN AS NivelCoPayVincuTopYear,  ING.IESTADOIN AS Status
, ING.GENCONENTITY AS HealthAdministratorId, PAT.GENCONENTITY AS PatientEntityId, PAT.IPTELMOVI AS PatientPhone
, PAT.GENCAREGROUP AS PatientCareGroupId, LTRIM(RTRIM(PAT.IPNOMCOMP)) AS PatientName, LTRIM(RTRIM(ING.CODICAMHO)) AS BedStay, ING.ILIQUIDAC AS LiquidationType, '- Ingresos' AS StatusName
, ING.GENCONENTITY as EntityId, COALESCE(PHA.Code, '') AS PatientEntityCode, COALESCE(PHA.Name, '') AS PatientEntityName, COALESCE(AHA.Code, '') AS EntityCode, COALESCE(AHA.Code, '') AS EntityName
, CONCAT(CG.Code, ' - ', CG.Name) as CareGroupCodeName, CG.CareGroupType, CONCAT(CG.Code, ' - ', CG.Name) AS AdmissionCareGroupCodeName, EGR.FECALTPAC, CONCAT(LTRIM(RTRIM(EGF.UFUCODIGO)), ' - ', LTRIM(RTRIM(EGF.UFUDESCRI))) AS UniFuncEgre,
Case Ing.TIPOINGRE When 1 Then 'Ambulatorio' When 2 Then 'Hospitalario' End As AdmissionTypeName,ING.TRATAESPECIA as TratamientoEspecial
FROM dbo.ADINGRESO AS ING with(nolock)
INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
INNER JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
inner join Billing.Invoice bi with(nolock) on bi.AdmissionNumber = ING.NUMINGRES
LEFT JOIN Contract.HealthAdministrator AS PHA WITH(NOLOCK) ON PAT.GENCONENTITY = PHA.Id
LEFT JOIN Contract.HealthAdministrator AS AHA WITH(NOLOCK) ON ING.GENCONENTITY = AHA.Id
LEFT JOIN Contract.CareGroup AS CG WITH(NOLOCK) ON PAT.GENCAREGROUP = CG.Id
LEFT JOIN Contract.CareGroup AS ACG WITH(NOLOCK) ON ING.GENCAREGROUP = ACG.Id
LEFT JOIN dbo.HCREGEGRE AS EGR WITH(NOLOCK) ON EGR.NUMINGRES = ING.NUMINGRES
LEFT JOIN dbo.INUNIFUNC AS EGF WITH(NOLOCK) ON EGF.UFUCODIGO = EGR.UFUCODIGO
where ING.IESTADOIN = 'F' --and (select count(*) from VIEPITA.Billing.Invoice where AdmissionNumber = ING.NUMINGRES) > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ingresos de pacientes que están en estado ''Facturado'' (IESTADOIN = ''F'') y que ya tienen al menos una factura emitida, consolidando toda la información necesaria para confirmar y procesar su liquidación. Combina datos del ingreso (tipo, fecha, causa, riesgo, cama, tipo de liquidación, tratamiento especial), del paciente (cédula, nombre, sexo, fecha de nacimiento, tipo de afiliación, teléfono), del nivel de copago (porcentajes y topes de cuota moderadora, copago y pagos por vinculación/subsidio), del centro de atención y la unidad funcional de ingreso y de egreso, de la EPS o administradora de salud tanto del ingreso como del paciente, y del grupo de atención del contrato. Es la fuente principal para los módulos de confirmación de liquidación de cuentas, permitiendo identificar qué ingresos facturados están listos para ser liquidados definitivamente, con todos los parámetros de cobro, copago y datos del pagador disponibles en una sola consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationConfirm';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista admisiones en estado finalizado (''F'') que ya tienen factura emitida, enriquecidas con datos del paciente, nivel de atención, centro, unidad funcional, administradora de salud, grupo de cuidado y egreso, para confirmar su liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe estar en estado ''F'' (IESTADOIN=''F'').; Debe existir al menos una factura en Billing.Invoice cuyo AdmissionNumber coincida con la admisión (INNER JOIN).; El paciente referido por la admisión debe existir en INPACIENT.; El paciente debe tener un nivel de atención válido en ADNIVELES.; La admisión debe tener centro de atención válido en ADCENATEN y unidad funcional válida en INUNIFUNC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros expuestos corresponden a admisiones finalizadas (estado ''F'').; Todos los registros expuestos tienen factura emitida en Billing.Invoice.; StatusName se fija siempre en ''- Ingresos''.; PatientEstrato nunca es NULL: se sustituye por ''0'' cuando no exista.; BenefitPlan, AuthorizationNumber, ResponsiblePhone, códigos/nombres de entidades del paciente y de la admisión nunca son NULL: se sustituyen por cadena vacía.; Los códigos de admisión, plan de beneficios, nombre del nivel, nombre del paciente y cama se entregan sin espacios en blanco al inicio/fin (LTRIM/RTRIM).; EntityCode y EntityName se exponen con el mismo valor (Code de la administradora de la admisión).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso; Liquidación; Facturación; Paciente; Nivel de atención; Copago; Cuota moderadora; Topes de copago (evento y anual) por régimen contributivo/subsidiado/vinculado; Centro de atención; Unidad funcional; Administradora de salud (EPS); Grupo de cuidado (CareGroup); Egreso hospitalario; Tipo de ingreso (ambulatorio/hospitalario); Tratamiento especial; Plan de beneficios; Autorización; SISBEN; Estrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve solo admisiones con IESTADOIN=''F'' y con factura asociada en Billing.Invoice (vía INNER JOIN por AdmissionNumber).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.TIPOINGRE = 1 → AdmissionTypeName = ''Ambulatorio'' else Si TIPOINGRE = 2 entonces ''Hospitalario''; en otro caso NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; dbo.INUNIFUNC; Billing.Invoice; Contract.HealthAdministrator; Contract.CareGroup; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirm';
GO
