

CREATE VIEW [Billing].[ViewAdministrativeNotesAccountControlAmbulatory]
AS

select CONCAT(nac.ID, nad.ID) Id,
IIF(nacups.GENSERVICEORDER is null, 0, 1) Selection, nac.FECHACREACION MedicalOrderDate, cups.CODSERIPS CupsCode, cups.DESSERIPS CupsName,
RTRIM(LTRIM(cups.CODSERIPS)) + ' - ' + RTRIM(LTRIM(cups.DESSERIPS)) CupsDescription,
seg.CODUSUARI ProfessionalCode, seg.NOMUSUARI ProfessionalName, '' ProfessionalEspecialty,
RTRIM(LTRIM(seg.CODUSUARI)) + ' - ' + RTRIM(LTRIM(seg.NOMUSUARI)) ProfessionalDescription, prof.CODIGONIT ProfessionalNit,
1 Quantity, '' FolioNumber, nac.IPCODPACI PatientCode, cups.APLICARIAS ApplyRIAS,
'' FunctionalUnitCode, '' FunctionalUnitName,
'' FunctionalUnitDescription,
nac.NUMINGRES AdmissionNumber, nac.CODCENATE CareCenterCode, nacups.ID EntityId, ISNULL(nacups.GENSERVICEORDER, 0) ServiceOrderId,
0 RiasCupsId, '' RiasDescription,
NV.VARIABLE Variable
from .NTNOTASADMINISTRATIVASC nac WITH(NOLOCK)
inner join .NTNOTASADMINISTRATIVASD nad WITH(NOLOCK) on nad.IDNTNOTASADMINISTRATIVASC = nac.ID
inner join .NTVARIABLES nv on nv.ID = NAD.IDNTVARIABLE
inner join .NTNOTASADMINISTRATIVASCUPS nacups WITH(NOLOCK) on nacups.IDNTNOTASADMINISTRATIVASD = nad.ID
inner join .INCUPSIPS cups WITH(NOLOCK) on cups.CODSERIPS = nacups.CODSERIPS
inner join .SEGusuaru seg WITH(NOLOCK) on seg.CODUSUARI = nac.CODUSUARI
inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = seg.CODUSUARI
where nac.NOTANOFACTURABLE = 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las notas administrativas ambulatorias facturables junto con los servicios CUPS asociados, para el control de cuentas en facturación ambulatoria. Combina el encabezado de la nota (fecha, paciente, ingreso, centro de atención), el detalle de variables clínicas registradas, el servicio CUPS vinculado (código y descripción), y el profesional de salud que la generó (código, nombre, NIT). Excluye las notas marcadas como no facturables, por lo que solo expone los registros que pueden generar cobro. Es utilizada por el módulo de Billing para revisar y seleccionar servicios de notas administrativas pendientes de facturar en la cuenta del paciente ambulatorio, incluyendo información sobre si ya se generó una orden de servicio asociada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las notas administrativas facturables del ámbito ambulatorio con su detalle de CUPS, variable, profesional tratante y estado de generación de orden de servicio, para soporte al control de cuenta en facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario que crea la nota (CODUSUARI) debe existir simultáneamente como usuario del sistema (SEGusuaru) y como profesional de la salud (INPROFSAL) para que la nota sea visible.; El código CUPS de la nota debe estar registrado en el catálogo INCUPSIPS.; La nota debe tener al menos un detalle (NTNOTASADMINISTRATIVASD) con variable asociada (NTVARIABLES) y al menos un CUPS asociado (NTNOTASADMINISTRATIVASCUPS).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen notas administrativas marcadas como facturables (NOTANOFACTURABLE = 0).; El identificador de fila de la vista se construye concatenando el ID del encabezado y el ID del detalle de la nota administrativa, garantizando unicidad por par cabecera-detalle.; Cada fila siempre tiene un CUPS válido existente en INCUPSIPS, un profesional registrado tanto en SEGusuaru como en INPROFSAL, y una variable definida en NTVARIABLES (joins INNER).; La cantidad expuesta es siempre 1 por línea de detalle CUPS.; Los campos FolioNumber, ProfessionalEspecialty, FunctionalUnitCode/Name/Description y RiasDescription se devuelven siempre vacíos, y RiasCupsId siempre 0 (no se calculan en esta vista).; Si no existe orden de servicio generada, ServiceOrderId se normaliza a 0 mediante ISNULL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota administrativa; Orden de servicio; CUPS; Profesional de la salud; Paciente; Ingreso/Admisión; Centro de atención; RIAS (Rutas Integrales de Atención en Salud); Variable clínica/administrativa; Atención ambulatoria', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] NTNOTASADMINISTRATIVASC: Filtra exclusivamente notas con NOTANOFACTURABLE = 0; las notas marcadas como no facturables no se retornan.; [RETURN_RESULT] NTNOTASADMINISTRATIVASCUPS: Marca Selection=1 cuando nacups.GENSERVICEORDER no es nulo (ya existe orden de servicio); en caso contrario Selection=0.; [RETURN_RESULT] INCUPSIPS: Expone el indicador ApplyRIAS (cups.APLICARIAS) por cada CUPS para señalar si aplica a Rutas Integrales de Atención en Salud.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si nacups.GENSERVICEORDER IS NULL → Selection = 0 y ServiceOrderId = 0 (la nota CUPS aún no ha generado orden de servicio) else Selection = 1 y ServiceOrderId = nacups.GENSERVICEORDER (ya tiene orden de servicio asociada)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'NTNOTASADMINISTRATIVASC; NTNOTASADMINISTRATIVASD; NTVARIABLES; NTNOTASADMINISTRATIVASCUPS; INCUPSIPS; SEGusuaru; INPROFSAL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdministrativeNotesAccountControlAmbulatory';
GO
