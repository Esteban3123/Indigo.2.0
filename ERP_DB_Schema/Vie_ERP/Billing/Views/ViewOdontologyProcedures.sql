


















CREATE VIEW [Billing].[ViewOdontologyProcedures]
AS



select tempInfo.GenerateId AS Id,
case when opso.Id is null then 0 else 1 end Selection, tempInfo.ProcedureCode, tempInfo.ProcedureName, tempInfo.CupsCode, tempInfo.CupsName, tempInfo.CupsDescription,
SUM(tempInfo.Quantity) Quantity, tempInfo.FolioNumber, tempInfo.RegisterDate, tempInfo.ProfessionalCode, tempInfo.ProfessionalName, tempInfo.ProfessionalDescription,
tempInfo.FunctionalUnitCode, tempInfo.FunctionalUnitName, tempInfo.FunctionalUnitDescription, tempInfo.AdmissionNumber, tempInfo.PatientCode, tempInfo.CareCenterCode,
tempInfo.ProfessionalEspecialty, tempInfo.ProfessionalNit, ISNULL(opso.ServiceOrderId, 0) ServiceOrderId, cups.APLICARIAS ApplyRIAS,
tempInfo.RiasCupsId, ISNULL(r.CODPRO + ' - ' + r.NOMBRE, '') RiasDescription
from (
	select op.CONSECTRA ProcedureCode, rtrim(ltrim(op.DESCRITRA)) ProcedureName, 
	odt.CODSERIPS CupsCode, cu.DESSERIPS CupsName, rtrim(ltrim(odt.CODSERIPS)) + ' - ' + rtrim(ltrim(cu.DESSERIPS)) CupsDescription,
	SUM(odt.CANTIDAD) Quantity, oc.NUMEFOLIO FolioNumber, oc.FECHAREG RegisterDate, 
	prof.CODPROSAL ProfessionalCode, prof.NOMMEDICO ProfessionalName, rtrim(ltrim(prof.CODPROSAL)) + ' - ' + rtrim(ltrim(prof.NOMMEDICO)) ProfessionalDescription,
	fu.UFUCODIGO FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName, rtrim(ltrim(fu.UFUCODIGO)) + ' - ' + rtrim(ltrim(fu.UFUDESCRI)) FunctionalUnitDescription,
	oc.NUMINGRES AdmissionNumber, oc.IPCODPACI PatientCode, oc.CODCENATE CareCenterCode, prof.CODESPEC1 ProfessionalEspecialty, prof.CODIGONIT ProfessionalNit,
	ISNULL(oc.IDRIASCUPS, 0) RiasCupsId, CONCAT(oc.ID, od.ID, odt.ID) GenerateId
	from .ODONTOCONTROL oc WITH(NOLOCK)
	inner join .ODONTODIENTE od WITH(NOLOCK) on od.IDODONTOCONTROL = oc.ID and od.TIPOODONTOGRAMA = 'T'
	inner join .INUNIFUNC fu WITH(NOLOCK) on fu.UFUCODIGO = oc.UFUCODIGO
	inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = oc.CODPROSAL
	inner join .ODONTODIENTETRATA odt WITH(NOLOCK) on odt.IDODONTODIENTE = od.ID
	inner join .ODOPARTRA op WITH(NOLOCK) on op.CONSECTRA = odt.CONSECTRA
	inner join .INCUPSIPS cu WITH(NOLOCK) on cu.CODSERIPS = odt.CODSERIPS
	group by op.DESCRITRA, odt.CODSERIPS, cu.DESSERIPS, oc.NUMEFOLIO, oc.FECHAREG, prof.CODPROSAL, prof.NOMMEDICO, fu.UFUCODIGO, fu.UFUDESCRI, 
	oc.NUMINGRES, oc.IPCODPACI, oc.CODCENATE, prof.CODESPEC1, prof.CODIGONIT, op.CONSECTRA, oc.IDRIASCUPS, odt.CANTIDAD, oc.ID, od.ID, odt.ID
	union
	select op.CONSECTRA ProcedureCode, rtrim(ltrim(op.DESCRITRA)) ProcedureName, 
	oot.CODSERIPS CupsCode, cu.DESSERIPS CupsName, rtrim(ltrim(oot.CODSERIPS)) + ' - ' + rtrim(ltrim(cu.DESSERIPS)) CupsDescription,
	SUM(oot.CANTIDAD) Quantity, oc.NUMEFOLIO FolioNumber, oc.FECHAREG RegisterDate, 
	prof.CODPROSAL ProfessionalCode, prof.NOMMEDICO ProfessionalName, rtrim(ltrim(prof.CODPROSAL)) + ' - ' + rtrim(ltrim(prof.NOMMEDICO)) ProfessionalDescription,
	fu.UFUCODIGO FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName, rtrim(ltrim(fu.UFUCODIGO)) + ' - ' + rtrim(ltrim(fu.UFUDESCRI)) FunctionalUnitDescription,
	oc.NUMINGRES AdmissionNumber, oc.IPCODPACI PatientCode, oc.CODCENATE CareCenterCode, prof.CODESPEC1 ProfessionalEspecialty, prof.CODIGONIT ProfessionalNit,
	ISNULL(oc.IDRIASCUPS, 0) RiasCupsId, CONCAT(oc.ID, oot.ID, op.CONSECTRA) GenerateId
	from .ODONTOCONTROL oc WITH(NOLOCK)
	inner join .ODONTOOTROSTRA oot WITH(NOLOCK) on oot.IDODONTOCONTROL = oc.ID
	inner join .ODOPARTRA op WITH(NOLOCK) on op.CONSECTRA = oot.CONSECTRA
	inner join .INUNIFUNC fu WITH(NOLOCK) on fu.UFUCODIGO = oc.UFUCODIGO
	inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = oc.CODPROSAL
	inner join .INCUPSIPS cu WITH(NOLOCK) on cu.CODSERIPS = oot.CODSERIPS
	group by op.DESCRITRA, oot.CODSERIPS, cu.DESSERIPS, oc.NUMEFOLIO, oc.FECHAREG, prof.CODPROSAL, prof.NOMMEDICO, fu.UFUCODIGO, fu.UFUDESCRI, 
	oc.NUMINGRES, oc.IPCODPACI, oc.CODCENATE, prof.CODESPEC1, prof.CODIGONIT, op.CONSECTRA, oc.IDRIASCUPS, oot.CANTIDAD, oc.ID, oot.ID
) tempInfo
inner join .INCUPSIPS cups WITH(NOLOCK) on cups.CODSERIPS = tempInfo.CupsCode
left join Billing.BillingOdontologyProcedureServiceOrder opso WITH(NOLOCK) on opso.AdmissionNumber = tempInfo.AdmissionNumber and opso.Folio = tempInfo.FolioNumber and opso.ProcedureCode = tempInfo.ProcedureCode
left join .RIASCUPS rc WITH(NOLOCK) on rc.ID = tempInfo.RiasCupsId
left join .RIAS r WITH(NOLOCK) on r.ID = rc.IDRIAS
group by tempInfo.ProcedureName, tempInfo.CupsCode, tempInfo.CupsName, tempInfo.CupsDescription,
tempInfo.FolioNumber, tempInfo.RegisterDate, tempInfo.ProfessionalCode, tempInfo.ProfessionalName, tempInfo.ProfessionalDescription,
tempInfo.FunctionalUnitCode, tempInfo.FunctionalUnitName, tempInfo.FunctionalUnitDescription, tempInfo.AdmissionNumber, tempInfo.PatientCode, tempInfo.CareCenterCode,
tempInfo.ProfessionalEspecialty, tempInfo.ProfessionalNit, tempInfo.ProcedureCode, opso.Id, opso.ServiceOrderId, cups.APLICARIAS, tempInfo.RiasCupsId, r.CODPRO, r.NOMBRE, tempInfo.GenerateId