
CREATE VIEW [dbo].[ViewAdmissionsToLiquidation]
  
AS

SELECT DISTINCT LTRIM(RTRIM(ING.NUMINGRES)) AS AdmissionCode, ING.NUMINGRES AS AdmissionCodeWithOutTrim, ING.IFECHAING AS AdmissionDate, ING.TIPOINGRE AS AdmissionType, ING.GENCAREGROUP AS AdmissionCaregroupId
, ING.ICAUSAING AS AdmissionReason, ING.ITIPORIES AS AdmissionRiskType, CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE))) AS AdmissionCentAtencCodeName
, CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))) AS AdmissionUniFuncCodeName, ING.IINGREPOR AS PlaceEntry, LTRIM(RTRIM(COALESCE (ING.CODPANATE, ''))) AS BenefitPlan
, COALESCE (ING.IAUTORIZA, '') AS AuthorizationNumber, COALESCE (ING.IPTELEFON, '') AS ResponsiblePhone, ING.IPRNOMBRE AS ResponsibleName, PAT.IPCODPACI AS PatientCode, PAT.IPFECNACI AS PatientBirth
, PAT.IPSEXOPAC AS PatientGenus, COALESCE (PAT.IPESTRATO, '0') AS PatientEstrato, PAT.IPTIPOPAC AS PatientType, PAT.IPTIPOAFI AS PatientAfiliation, PAT.IPTIPODOC AS PatientDocumentType
, NIV.NIVCODIGO AS NivelCode, LTRIM(RTRIM(NIV.NIVDESCRI)) AS NivelName, NIV.NIVPORCMO AS NivelModeratorSharePercentage, NIV.NIVPORCOP AS NivelCoPayContribPercentage
, NIV.NIVPORSUB AS NivelCoPaySubsiPercentage, NIV.NIVPORVIN AS NivelCoPayVincuPercentage, NIV.NIVSISBEN AS NivelSisben, NIV.TOPEVECMO AS NivelModeratorShareTop
, NIV.TOPEVECOP AS NivelCoPayContribTop, NIV.TOPEVESUB AS NivelCoPaySubsibTop, NIV.TOPEVEVIN AS NivelCoPayVincuTop, NIV.TOPANUCMO AS NivelModeratorShareTopYear
, NIV.TOPANUCOP AS NivelCoPayContribTopYear, NIV.TOPANUSUB AS NivelCoPaySubsibTopYear, NIV.TOPANUVIN AS NivelCoPayVincuTopYear, ING.IESTADOIN AS Status
, ING.GENCONENTITY AS HealthAdministratorId, PAT.GENCONENTITY AS PatientEntityId, PAT.IPTELMOVI AS PatientPhone
, PAT.GENCAREGROUP AS PatientCareGroupId, LTRIM(RTRIM(PAT.IPNOMCOMP)) AS PatientName, LTRIM(RTRIM(ING.CODICAMHO)) AS BedStay, ING.ILIQUIDAC AS LiquidationType, '- Ingresos' AS StatusName
, ING.GENCONENTITY as EntityId, COALESCE(PHA.Code, '') AS PatientEntityCode, COALESCE(PHA.Name, '') AS PatientEntityName, COALESCE(AHA.Code, '') AS EntityCode, COALESCE(AHA.Code, '') AS EntityName
, CONCAT(CG.Code, ' - ', CG.Name) as CareGroupCodeName, CG.CareGroupType, CONCAT(ACG.Code, ' - ', ACG.Name) AS AdmissionCareGroupCodeName, EGR.FECALTPAC, CONCAT(LTRIM(RTRIM(EGF.UFUCODIGO)), ' - ', LTRIM(RTRIM(EGF.UFUDESCRI))) AS UniFuncEgre
, ING.TRATAESPECIA as TratamientoEspecial, rc.Id As RevenueControlId
, tpp.Id As ThirdPartyPatientId,
Case Ing.TIPOINGRE When 1 Then 'Ambulatorio' When 2 Then 'Hospitalario' End As AdmissionTypeName,
SB.IncomeLockType
FROM dbo.ADINGRESO AS ING with(nolock)
INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
LEFT JOIN dbo.INENTIDAD INE with(nolock) ON ING.CODENTIDA = INE.CODENTIDA
LEFT JOIN Contract.HealthAdministrator AS PHA WITH(NOLOCK) ON PAT.GENCONENTITY = PHA.Id
LEFT JOIN Contract.HealthAdministrator AS AHA WITH(NOLOCK) ON ING.GENCONENTITY = AHA.Id
LEFT JOIN Contract.CareGroup AS CG WITH(NOLOCK) ON PAT.GENCAREGROUP = CG.Id
LEFT JOIN Contract.CareGroup AS ACG WITH(NOLOCK) ON ING.GENCAREGROUP = ACG.Id
LEFT JOIN dbo.HCREGEGRE AS EGR WITH(NOLOCK) ON EGR.NUMINGRES = ING.NUMINGRES
LEFT JOIN dbo.INUNIFUNC AS EGF WITH(NOLOCK) ON EGF.UFUCODIGO = EGR.UFUCODIGO
LEFT JOIN Billing.RevenueControl rc with(nolock) ON rc.AdmissionNumber = LTRIM(RTRIM(ING.NUMINGRES))
left join Common.ThirdParty tpp with(nolock) on tpp.Nit = ltrim(rtrim(pat.IPCODPACI))
left join Billing.SettingsBilling sb on sb.IdOperatingUnit = ACG.OperativeUnitId
left join dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
where ING.IESTADOIN = ' ' or ING.IESTADOIN = 'P' or ING.IESTADOIN = 'B'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los ingresos o admisiones de pacientes pendientes de liquidar o en proceso de facturación (estados abierto, pendiente y en borrador). Integra datos del episodio de ingreso (número de admisión, fecha, tipo ambulatorio u hospitalario, motivo, cama), información demográfica y de identificación del paciente, centro de atención, unidad funcional, entidad pagadora (EPS/ARS/aseguradora), grupo de atención y plan de beneficios. Incorpora además los porcentajes y topes de cuota moderadora, copago y cuota de recuperación según el nivel del paciente (SISBEN/estrato), el registro de egreso hospitalario cuando existe, el control de ingresos y topes de cobro (RevenueControl), y el tercero asociado al paciente. Se utiliza como fuente principal del proceso de liquidación y facturación para identificar qué admisiones están listas o en curso de ser cobradas a la entidad pagadora correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las admisiones elegibles para liquidación (en estado en blanco, ''P'' o ''B'') consolidando datos del paciente, centro, unidad funcional, administradora, grupo de cuidado, egreso, niveles de copago/cuota moderadora y control de ingresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe existir en ADINGRESO con un paciente válido en INPACIENT, un centro de atención válido en ADCENATEN y una unidad funcional válida en INUNIFUNC (joins INNER).; El estado de la admisión debe ser '' '' (en curso), ''P'' (pendiente) o ''B'' para ser candidata a liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo expone admisiones cuyo estado sea espacio en blanco, ''P'' o ''B'' (estados elegibles para liquidación).; El nombre de estado siempre se reporta como ''- Ingresos''.; Los códigos de centro, unidad funcional, grupo de cuidado y nombre de paciente se entregan sin espacios en blanco a izquierda/derecha.; Plan de beneficios, número de autorización y teléfono del responsable nunca son NULL (se sustituyen por cadena vacía).; El estrato del paciente nunca es NULL (se sustituye por ''0'').; Los códigos/nombres de entidad y administradora del paciente nunca son NULL (cadena vacía si faltan).; El cruce con Common.ThirdParty se hace asumiendo que el código del paciente actúa como NIT del tercero.; El cruce con Billing.SettingsBilling se hace por la unidad operativa del CareGroup de la admisión.; Una admisión aparece una sola vez (DISTINCT) aun cuando existan múltiples relaciones en las tablas unidas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso; Liquidación; Paciente; Centro de atención; Unidad funcional; Plan de beneficios; Autorización; Administradora de salud (EPS/ARS); Grupo de cuidado (CareGroup); Egreso hospitalario; Cuota moderadora; Copago (contributivo, subsidiado, vinculado); Topes de copago; Nivel/Sisbén; Estrato; Control de ingresos (RevenueControl); Tercero; Tipo de admisión (Ambulatorio/Hospitalario); Cama / estancia; Tratamiento especial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewAdmissionsToLiquidation: Devuelve únicamente admisiones donde IESTADOIN está en ('' '', ''P'', ''B''), enriquecidas con paciente, centro, unidad funcional, entidad, administradora, grupos de cuidado, egreso, niveles y control de ingresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IESTADOIN = '' '' OR IESTADOIN = ''P'' OR IESTADOIN = ''B'' → Se incluye la admisión en la vista (candidata a liquidación) else Se excluye la admisión; si TIPOINGRE = 1 → Tipo de admisión se etiqueta como ''Ambulatorio''; si TIPOINGRE = 2 → Tipo de admisión se etiqueta como ''Hospitalario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INENTIDAD; Contract.HealthAdministrator; Contract.CareGroup; dbo.HCREGEGRE; Billing.RevenueControl; Common.ThirdParty; Billing.SettingsBilling; dbo.ADNIVELES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidation';
GO
