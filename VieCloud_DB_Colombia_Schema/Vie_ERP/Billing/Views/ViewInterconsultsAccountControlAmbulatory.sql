

CREATE VIEW [Billing].[ViewInterconsultsAccountControlAmbulatory]
AS

select inter.AUTO Id,
IIF(inter.GENSERVICEORDER is null, 0, 1) Selection, inter.FECORDMED MedicalOrderDate, cups.CODSERIPS CupsCode, cups.DESSERIPS CupsName,
RTRIM(LTRIM(cups.CODSERIPS)) + ' - ' + RTRIM(LTRIM(cups.DESSERIPS)) CupsDescription,
prof.CODPROSAL ProfessionalCode, prof.NOMMEDICO ProfessionalName, prof.CODESPEC1 ProfessionalEspecialty,
RTRIM(LTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalDescription, prof.CODIGONIT ProfessionalNit,
inter.CANSERIPS Quantity, inter.NUMEFOLIO FolioNumber, inter.IPCODPACI PatientCode, cups.APLICARIAS ApplyRIAS,
inter.UFUCODIGO FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName,
RTRIM(LTRIM(inter.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitDescription,
inter.NUMINGRES AdmissionNumber, inter.CODCENATE CareCenterCode, inter.[AUTO] EntityId, ISNULL(inter.GENSERVICEORDER, 0) ServiceOrderId,
ISNULL(inter.IDRIASCUPS, 0) RiasCupsId, ISNULL(r.CODPRO + ' - ' + r.NOMBRE, '') RiasDescription,
ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, ISNULL(cd.Id, 0) ContractDescriptionId, ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
from .HCORDINTE inter WITH(NOLOCK)
inner join .INCUPSIPS cups WITH(NOLOCK) on cups.CODSERIPS = inter.CODSERIPS
inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = inter.CODPROSAL
inner join .INUNIFUNC fu WITH(NOLOCK) on fu.UFUCODIGO = inter.UFUCODIGO
left join .RIASCUPS rc WITH(NOLOCK) on rc.ID = inter.IDRIASCUPS
left join .RIAS r WITH(NOLOCK) on r.ID = rc.IDRIAS
left join Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) on cecd.Id = inter.IDDESCRIPCIONRELACIONADA
left join Contract.ContractDescriptions cd WITH(NOLOCK) on cd.Id = cecd.ContractDescriptionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de interconsultas ambulatorias para facturación: consolida las órdenes médicas de interconsulta (servicios, procedimientos o exámenes solicitados durante un ingreso) junto con el código y nombre del servicio CUPS, el profesional de salud que ordenó el servicio, la unidad funcional donde se generó la orden y el número de admisión del paciente. Enriquece cada interconsulta con su clasificación RIAS (Ruta Integral de Atención en Salud) y con la descripción de contrato asociada al CUPS, permitiendo identificar si el servicio ya tiene una orden de servicio generada (campo de selección) y bajo qué concepto o grupo de facturación debe cobrarse. Se usa en el módulo de facturación para revisar, seleccionar y controlar qué interconsultas ambulatorias pendientes deben ser facturadas, vinculando el servicio clínico con el marco contractual y los reportes RIPS-RIAS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewInterconsultsAccountControlAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewInterconsultsAccountControlAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las interconsultas (órdenes médicas) ambulatorias con datos de CUPS, profesional, unidad funcional, RIAS y vínculo contractual, marcando si ya tienen orden de servicio generada, para el control de cuenta de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La interconsulta (HCORDINTE) debe tener un CODSERIPS existente en INCUPSIPS, un CODPROSAL existente en INPROFSAL y una UFUCODIGO existente en INUNIFUNC para aparecer en la vista', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen interconsultas que tengan CUPS, profesional y unidad funcional válidos (INNER JOIN obliga existencia en INCUPSIPS, INPROFSAL e INUNIFUNC); La asociación a RIAS y a descripciones de contrato es opcional (LEFT JOIN); cuando no existe se devuelven 0 o cadena vacía vía ISNULL; ServiceOrderId, RiasCupsId, CUPSEntityContractDescriptionId y ContractDescriptionId nunca son NULL en la salida (ISNULL a 0); Se construyen descripciones concatenadas ''código - nombre'' para CUPS, profesional, unidad funcional y descripción de contrato como campos de presentación; Todas las lecturas usan WITH(NOLOCK), por lo que pueden incluir lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'interconsulta; orden médica; CUPS; profesional de la salud; especialidad; unidad funcional; centro de atención; paciente; admisión/ingreso; RIAS (Rutas Integrales de Atención en Salud); descripción de contrato; folio; orden de servicio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewInterconsultsAccountControlAmbulatory: Devuelve una fila por cada registro de HCORDINTE que tenga CUPS, profesional y unidad funcional válidos, enriquecida con datos de RIAS y descripciones contractuales cuando existan; [RETURN_RESULT] Billing.ViewInterconsultsAccountControlAmbulatory: Selection = 1 cuando GENSERVICEORDER no es nulo; Selection = 0 cuando es nulo, señalando si la interconsulta ya generó orden de servicio; [RETURN_RESULT] Billing.ViewInterconsultsAccountControlAmbulatory: ApplyRIAS se toma de INCUPSIPS.APLICARIAS, indicando si el CUPS aplica a Rutas Integrales de Atención en Salud', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si inter.GENSERVICEORDER IS NULL → Selection = 0 (interconsulta no asociada a una orden de servicio generada) else Selection = 1 (interconsulta ya tiene orden de servicio generada)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.RIASCUPS; dbo.RIAS; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewInterconsultsAccountControlAmbulatory';
GO
