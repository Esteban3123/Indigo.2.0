

CREATE VIEW [Billing].[ViewLaboratoriesAccountControlAmbulatory]
AS

select lab.AUTO Id,
IIF(lab.GENSERVICEORDER is null, 0, 1) Selection, lab.FECORDMED MedicalOrderDate, cups.CODSERIPS CupsCode, cups.DESSERIPS CupsName,
RTRIM(LTRIM(cups.CODSERIPS)) + ' - ' + RTRIM(LTRIM(cups.DESSERIPS)) CupsDescription,
prof.CODPROSAL ProfessionalCode, prof.NOMMEDICO ProfessionalName, prof.CODESPEC1 ProfessionalEspecialty,
RTRIM(LTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalDescription, prof.CODIGONIT ProfessionalNit,
lab.CANSERIPS Quantity, lab.NUMEFOLIO FolioNumber, lab.IPCODPACI PatientCode, cups.APLICARIAS ApplyRIAS,
lab.UFUCODIGO FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName,
RTRIM(LTRIM(lab.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitDescription,
lab.NUMINGRES AdmissionNumber, lab.CODCENATE CareCenterCode, lab.[AUTO] EntityId, ISNULL(lab.GENSERVICEORDER, 0) ServiceOrderId,
ISNULL(lab.IDRIASCUPS, 0) RiasCupsId, ISNULL(r.CODPRO + ' - ' + r.NOMBRE, '') RiasDescription,
ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, ISNULL(cd.Id, 0) ContractDescriptionId, ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
from .HCORDLABO lab WITH(NOLOCK)
inner join .INCUPSIPS cups WITH(NOLOCK) on cups.CODSERIPS = lab.CODSERIPS
inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = lab.CODPROSAL
inner join .INUNIFUNC fu WITH(NOLOCK) on fu.UFUCODIGO = lab.UFUCODIGO
left join .RIASCUPS rc WITH(NOLOCK) on rc.ID = lab.IDRIASCUPS
left join .RIAS r WITH(NOLOCK) on r.ID = rc.IDRIAS
left join Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) on cecd.Id = lab.IDDESCRIPCIONRELACIONADA
left join Contract.ContractDescriptions cd WITH(NOLOCK) on cd.Id = cecd.ContractDescriptionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de cuentas de laboratorio clínico para pacientes ambulatorios. Consolida las órdenes médicas de laboratorio (exámenes solicitados) con el catálogo de servicios CUPS, el profesional de la salud que ordenó el examen, la unidad funcional de atención y la información de contratos y facturación asociada. Permite a los módulos de facturación y control de cuentas identificar qué exámenes de laboratorio ambulatorios han sido ordenados, si ya generaron una orden de servicio, a qué concepto de contrato pertenecen y si aplican clasificación RIAS, facilitando la auditoría, liquidación y reporte de servicios prestados a pacientes ambulatorios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewLaboratoriesAccountControlAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de laboratorio ambulatorias con datos del CUPS, profesional, unidad funcional, RIAS y descripción de contrato, indicando si ya tienen orden de servicio generada, para el control de cuenta.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro en HCORDLABO debe tener un CUPS existente en INCUPSIPS, un profesional existente en INPROFSAL y una unidad funcional existente en INUNIFUNC.; Las relaciones con RIAS, RIASCUPS y descripciones de contrato son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes de laboratorio que tengan CUPS, profesional y unidad funcional válidos (INNER JOIN obligatorio con INCUPSIPS, INPROFSAL e INUNIFUNC).; Los identificadores numéricos opcionales (ServiceOrderId, RiasCupsId, CUPSEntityContractDescriptionId, ContractDescriptionId) se normalizan a 0 cuando son NULL.; Las descripciones textuales asociadas (RiasDescription, ContractDescriptionCodeName) se normalizan a cadena vacía cuando no existe relación.; Los códigos y nombres se concatenan con formato ''CODIGO - NOMBRE'' usando RTRIM/LTRIM para descripciones compuestas (CUPS, profesional, unidad funcional, contrato).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica de laboratorio; CUPS (procedimientos); Profesional de la salud / especialidad; Unidad funcional; Centro de atención; Admisión / folio; Paciente ambulatorio; RIAS (Rutas Integrales de Atención en Salud); Orden de servicio; Descripción de contrato / facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas de HCORDLABO enriquecidas con catálogos; marca Selection=1 si GENSERVICEORDER no es NULL, en otro caso 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si lab.GENSERVICEORDER IS NULL → Selection = 0 (la orden de laboratorio aún no tiene orden de servicio generada) else Selection = 1 (ya tiene orden de servicio asociada)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.RIASCUPS; dbo.RIAS; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesAccountControlAmbulatory';
GO
