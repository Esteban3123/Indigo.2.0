

CREATE VIEW [Billing].[ViewImagesAccountControlAmbulatory]
AS

select ima.AUTO Id,
IIF(ima.GENSERVICEORDER is null, 0, 1) Selection, ima.FECORDMED MedicalOrderDate, cups.CODSERIPS CupsCode, cups.DESSERIPS CupsName,
RTRIM(LTRIM(cups.CODSERIPS)) + ' - ' + RTRIM(LTRIM(cups.DESSERIPS)) CupsDescription,
prof.CODPROSAL ProfessionalCode, prof.NOMMEDICO ProfessionalName, prof.CODESPEC1 ProfessionalEspecialty,
RTRIM(LTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalDescription, prof.CODIGONIT ProfessionalNit,
ima.CANSERIPS Quantity, ima.NUMEFOLIO FolioNumber, ima.IPCODPACI PatientCode, cups.APLICARIAS ApplyRIAS,
ima.UFUCODIGO FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName,
RTRIM(LTRIM(ima.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitDescription,
ima.NUMINGRES AdmissionNumber, ima.CODCENATE CareCenterCode, ima.[AUTO] EntityId, ISNULL(ima.GENSERVICEORDER, 0) ServiceOrderId,
ISNULL(ima.IDRIASCUPS, 0) RiasCupsId, ISNULL(r.CODPRO + ' - ' + r.NOMBRE, '') RiasDescription,
ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, ISNULL(cd.Id, 0) ContractDescriptionId, ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
from .HCORDIMAG ima WITH(NOLOCK)
inner join .INCUPSIPS cups WITH(NOLOCK) on cups.CODSERIPS = ima.CODSERIPS
inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = ima.CODPROSAL
inner join .INUNIFUNC fu WITH(NOLOCK) on fu.UFUCODIGO = ima.UFUCODIGO
left join .RIASCUPS rc WITH(NOLOCK) on rc.ID = ima.IDRIASCUPS
left join .RIAS r WITH(NOLOCK) on r.ID = rc.IDRIAS
left join Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) on cecd.Id = ima.IDDESCRIPCIONRELACIONADA
left join Contract.ContractDescriptions cd WITH(NOLOCK) on cd.Id = cecd.ContractDescriptionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de cuentas de imágenes diagnósticas (radiología, ecografías, tomografías, resonancias) para pacientes ambulatorios. Consolida cada orden de imagen con su código y nombre CUPS, el profesional que la solicitó, la unidad funcional donde se generó, la cantidad, el número de folio y los datos del paciente e ingreso. Enriquece cada servicio con su clasificación RIAS (Rutas Integrales de Atención en Salud) y con la descripción de contrato y concepto de facturación asociados, permitiendo verificar qué órdenes ya tienen orden de servicio generada y cuáles están pendientes. Se utiliza en el módulo de facturación ambulatoria para auditar, controlar y preparar el cobro de estudios de imagen solicitados en historia clínica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewImagesAccountControlAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewImagesAccountControlAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de imágenes diagnósticas ambulatorias con sus datos de CUPS, profesional, unidad funcional, RIAS y descripción de contrato, marcando si ya tienen orden de servicio generada para control y facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas HCORDIMAG, INCUPSIPS, INPROFSAL e INUNIFUNC deben tener integridad referencial por CODSERIPS, CODPROSAL y UFUCODIGO para que las filas aparezcan en la vista.; Los registros de imágenes deben existir en HCORDIMAG; sin coincidencia en los catálogos CUPS/Profesional/Unidad Funcional la fila queda excluida.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes de imágenes que tengan CUPS, profesional y unidad funcional válidos (INNER JOIN obliga existencia en INCUPSIPS, INPROFSAL e INUNIFUNC).; El indicador Selection siempre es 0 o 1 según exista o no GENSERVICEORDER (orden de servicio) asociada.; Los identificadores ServiceOrderId, RiasCupsId, CUPSEntityContractDescriptionId y ContractDescriptionId nunca son NULL en la salida: se reemplazan por 0 cuando no hay relación.; RiasDescription y ContractDescriptionCodeName retornan cadena vacía cuando no hay vínculo con RIAS o con descripción de contrato.; Las descripciones compuestas (CupsDescription, ProfessionalDescription, FunctionalUnitDescription) siempre se entregan en formato ''CODIGO - NOMBRE'' con espacios recortados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica de imágenes diagnósticas; Servicio CUPS; Profesional de la salud (médico); Especialidad médica; Unidad funcional; Centro de atención; Admisión / ingreso ambulatorio; Orden de servicio; RIAS (Rutas Integrales de Atención en Salud); Descripción de contrato; Folio; Paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewImagesAccountControlAmbulatory: Devuelve una fila por cada registro de HCORDIMAG enriquecido con catálogos, marcando Selection=1 si GENSERVICEORDER no es nulo y 0 en caso contrario.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ima.GENSERVICEORDER IS NULL → Selection = 0 (ítem aún no tiene orden de servicio generada) else Selection = 1 (ítem ya está vinculado a una orden de servicio)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.RIASCUPS; dbo.RIAS; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewImagesAccountControlAmbulatory';
GO
