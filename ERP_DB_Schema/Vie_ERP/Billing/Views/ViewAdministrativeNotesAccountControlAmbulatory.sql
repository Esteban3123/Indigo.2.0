
























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