

CREATE VIEW [Billing].[ViewPathologiesAccountControlAmbulatory]
AS

select pat.AUTO Id,
IIF(pat.GENSERVICEORDER is null, 0, 1) Selection, pat.FECORDMED MedicalOrderDate, cups.CODSERIPS CupsCode, cups.DESSERIPS CupsName,
RTRIM(LTRIM(cups.CODSERIPS)) + ' - ' + RTRIM(LTRIM(cups.DESSERIPS)) CupsDescription,
prof.CODPROSAL ProfessionalCode, prof.NOMMEDICO ProfessionalName, prof.CODESPEC1 ProfessionalEspecialty,
RTRIM(LTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalDescription, prof.CODIGONIT ProfessionalNit,
pat.CANSERIPS Quantity, pat.NUMEFOLIO FolioNumber, pat.IPCODPACI PatientCode, cups.APLICARIAS ApplyRIAS,
pat.UFUCODIGO FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName,
RTRIM(LTRIM(pat.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitDescription,
pat.NUMINGRES AdmissionNumber, pat.CODCENATE CareCenterCode, pat.[AUTO] EntityId, ISNULL(pat.GENSERVICEORDER, 0) ServiceOrderId,
ISNULL(pat.IDRIASCUPS, 0) RiasCupsId, ISNULL(r.CODPRO + ' - ' + r.NOMBRE, '') RiasDescription,
ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, ISNULL(cd.Id, 0) ContractDescriptionId, ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
from .HCORDPATO pat WITH(NOLOCK)
inner join .INCUPSIPS cups WITH(NOLOCK) on cups.CODSERIPS = pat.CODSERIPS
inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = pat.CODPROSAL
inner join .INUNIFUNC fu WITH(NOLOCK) on fu.UFUCODIGO = pat.UFUCODIGO
left join .RIASCUPS rc WITH(NOLOCK) on rc.ID = pat.IDRIASCUPS
left join .RIAS r WITH(NOLOCK) on r.ID = rc.IDRIAS
left join Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) on cecd.Id = pat.IDDESCRIPCIONRELACIONADA
left join Contract.ContractDescriptions cd WITH(NOLOCK) on cd.Id = cecd.ContractDescriptionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de cuenta para órdenes de patología e imágenes diagnósticas en atención ambulatoria. Consolida cada examen o procedimiento diagnóstico ordenado (orden médica) con su código y nombre CUPS, el profesional que lo solicitó, la unidad funcional de atención, el número de ingreso y el centro de atención, permitiendo identificar si ya tiene orden de servicio generada. Integra además la clasificación RIAS del procedimiento y la descripción de contrato asociada (concepto de facturación), lo que la hace útil para el control de facturación ambulatoria, conciliación de cuentas y generación de RIPS. Sirve como base para que el área de facturación verifique qué servicios de patología e imágenes están pendientes de facturar, a qué contrato corresponden y bajo qué ruta integral de atención se clasifican.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewPathologiesAccountControlAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewPathologiesAccountControlAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las patologías/procedimientos ambulatorios solicitados en órdenes médicas con sus códigos CUPS, profesional, unidad funcional, RIAS y descripción contractual, para control y selección en facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en HCORDPATO con CODSERIPS, CODPROSAL y UFUCODIGO referenciados en INCUPSIPS, INPROFSAL e INUNIFUNC respectivamente (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila tiene CUPS, profesional y unidad funcional válidos por los INNER JOIN.; ServiceOrderId, RiasCupsId, CUPSEntityContractDescriptionId y ContractDescriptionId nunca son NULL en la salida (se sustituyen por 0 vía ISNULL).; RiasDescription y ContractDescriptionCodeName nunca son NULL en la salida (se sustituyen por '''' vía ISNULL).; Selection es siempre 0 o 1 (binario derivado de existencia de orden de servicio).; EntityId e Id provienen ambos de pat.AUTO (mismo identificador expuesto dos veces).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Patología ambulatoria; Orden médica; CUPS (códigos de procedimientos); Profesional de salud y especialidad; Unidad funcional; Centro de atención; Folio / Número de ingreso; RIAS (Rutas Integrales de Atención en Salud); Descripción de contrato / facturación; Orden de servicio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewPathologiesAccountControlAmbulatory: Devuelve una fila por registro de HCORDPATO enriquecido con datos de CUPS, profesional, unidad funcional, RIAS y contrato; marca Selection=1 si GENSERVICEORDER no es nulo, 0 en caso contrario.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pat.GENSERVICEORDER IS NULL → Selection = 0 (la patología aún no tiene orden de servicio generada) else Selection = 1 (ya tiene orden de servicio generada); si RIAS asociada existe (rc/r encontrados vía IDRIASCUPS) → RiasDescription = CODPRO + '' - '' + NOMBRE else RiasDescription = '''' (cadena vacía); si Existe descripción contractual relacionada (cecd vía IDDESCRIPCIONRELACIONADA y cd vinculada) → Se exponen CUPSEntityContractDescriptionId, ContractDescriptionId y ContractDescriptionCodeName con Code+Name else Estos campos quedan en 0/cadena vacía vía ISNULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDPATO; INCUPSIPS; INPROFSAL; INUNIFUNC; RIASCUPS; RIAS; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPathologiesAccountControlAmbulatory';
GO
