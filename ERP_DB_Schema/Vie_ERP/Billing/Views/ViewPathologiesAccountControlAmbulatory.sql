





















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