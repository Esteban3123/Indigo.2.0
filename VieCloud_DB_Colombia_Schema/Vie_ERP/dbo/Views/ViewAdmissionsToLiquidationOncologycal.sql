

CREATE VIEW [dbo].[ViewAdmissionsToLiquidationOncologycal]

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
, CONCAT(CG.Code, ' - ', CG.Name) as CareGroupCodeName, CG.CareGroupType, CONCAT(CG.Code, ' - ', CG.Name) AS AdmissionCareGroupCodeName, EGR.FECALTPAC, CONCAT(LTRIM(RTRIM(EGF.UFUCODIGO)), ' - ', LTRIM(RTRIM(EGF.UFUDESCRI))) AS UniFuncEgre
, ING.TRATAESPECIA as TratamientoEspecial
FROM 
(
	SELECT rc.AdmissionNumber
	FROM Billing.RevenueControl rc WITH (NOLOCK)
	JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
	WHERE rcd.Status = 1
	GROUP BY rc.AdmissionNumber
) rc
JOIN dbo.ADINGRESO AS ING with(nolock) ON rc.AdmissionNumber = ing.NUMINGRES
JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
LEFT JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
LEFT JOIN dbo.INENTIDAD INE with(nolock) ON ING.CODENTIDA = INE.CODENTIDA
LEFT JOIN Contract.HealthAdministrator AS PHA WITH(NOLOCK) ON PAT.GENCONENTITY = PHA.Id
LEFT JOIN Contract.HealthAdministrator AS AHA WITH(NOLOCK) ON ING.GENCONENTITY = AHA.Id
LEFT JOIN Contract.CareGroup AS CG WITH(NOLOCK) ON PAT.GENCAREGROUP = CG.Id
LEFT JOIN Contract.CareGroup AS ACG WITH(NOLOCK) ON ING.GENCAREGROUP = ACG.Id
LEFT JOIN dbo.HCREGEGRE AS EGR WITH(NOLOCK) ON EGR.NUMINGRES = ING.NUMINGRES
LEFT JOIN dbo.INUNIFUNC AS EGF WITH(NOLOCK) ON EGF.UFUCODIGO = EGR.UFUCODIGO
where ING.IESTADOIN IN (' ', 'P')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista las admisiones u hospitalizaciones de pacientes oncológicos pendientes de liquidación o en proceso de pago. Integra datos del ingreso (número de admisión, fecha, tipo, causa, autorización, cama), del paciente (cédula, nombre, fecha de nacimiento, sexo, tipo de afiliación, teléfono), del centro de atención y la unidad funcional donde fue atendido, del nivel socioeconómico con sus porcentajes y topes de cuota moderadora y copago (contributivo, subsidiado y vinculado, tanto por evento como anuales), y de la entidad pagadora o EPS con su grupo de atención y tipo de contrato. Solo incluye ingresos que tienen al menos un folio de facturación activo en el control de ingresos (RevenueControl/RevenueControlDetail con estado activo) y cuyo estado de ingreso es abierto o pendiente, sirviendo como fuente principal para los procesos de liquidación oncológica, validación de cobros y generación de RIPS o facturas a aseguradoras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationOncologycal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationOncologycal';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las admisiones oncológicas en estado abierto o pendiente que tienen control de ingresos con detalle activo, consolidando datos del paciente, entidad pagadora, nivel de copago, centro/unidad funcional y egreso, para alimentar procesos de liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en Billing.RevenueControlDetail con Status = 1 vinculado a un RevenueControl cuya AdmissionNumber coincida con dbo.ADINGRESO.NUMINGRES.; La admisión debe estar en estado '' '' (activa) o ''P'' (pendiente).; El paciente, centro de atención y unidad funcional referenciados por la admisión deben existir (joins internos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan admisiones con estado '' '' o ''P''.; Solo se listan admisiones con al menos un detalle de control de ingresos activo (Status=1).; El StatusName retornado siempre es la constante ''- Ingresos''.; Los códigos de centro de atención, unidad funcional y grupo de cuidado se devuelven concatenados como ''codigo - nombre''.; Los campos opcionales de plan, autorización, teléfono y estrato nunca se devuelven NULL (se sustituyen por '''' o ''0'').; Se aplica DISTINCT garantizando una fila única por admisión a pesar de los LEFT JOIN con egreso/entidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso de paciente; Liquidación oncológica; Control de ingresos (RevenueControl); Paciente; Entidad administradora de salud (EPS/ARS); Nivel de atención; Copago y cuota moderadora; Topes de copago (evento y anual) por modalidad contributivo/subsidiado/vinculado; Centro de atención; Unidad funcional; Grupo de cuidado (CareGroup); Egreso hospitalario; Tratamiento especial; Autorización; Plan de beneficios; Estrato y SISBEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewAdmissionsToLiquidationOncologycal: Devuelve filas DISTINCT de admisiones cuyo IESTADOIN está en ('' '',''P'') y que poseen al menos un RevenueControlDetail con Status=1, etiquetadas con StatusName=''- Ingresos''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN IN ('' '', ''P'') → Incluye la admisión en el resultado (estados activos o pendientes de liquidación). else Excluye admisiones en otros estados (egresadas, anuladas, facturadas, etc.).; si rcd.Status = 1 en Billing.RevenueControlDetail → La admisión es candidata a liquidación y se incorpora vía el subquery de RevenueControl. else Si ningún detalle de RevenueControl está activo, la admisión no aparece.; si COALESCE sobre CODPANATE, IAUTORIZA, IPTELEFON, IPESTRATO, PHA.Code/Name, AHA.Code → Sustituye nulos por '''' o ''0'' para garantizar valores no nulos en la salida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControl; Billing.RevenueControlDetail; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADNIVELES; dbo.INENTIDAD; Contract.HealthAdministrator; Contract.CareGroup; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationOncologycal';
GO
