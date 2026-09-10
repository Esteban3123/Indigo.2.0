

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de facturación que consolida todos los procedimientos odontológicos realizados por paciente e ingreso, integrando tanto los tratamientos aplicados por diente (odontograma) como otros tratamientos odontológicos adicionales. Combina información del control odontológico (folio, fecha, número de ingreso, código del paciente, centro de atención), el profesional tratante, la unidad funcional, el código y descripción del procedimiento dental (catálogo de tratamientos) y el código CUPS con su nombre y descripción. Además indica si el procedimiento ya fue asociado a una orden de servicio de facturación, si aplica RIAS, y la descripción de la ruta integral de atención en salud (RIAS) vinculada. Se utiliza para la liquidación, revisión y reporte de servicios odontológicos facturables, incluyendo generación de RIPS odontológicos y verificación de glosas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewOdontologyProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewOdontologyProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los procedimientos odontológicos realizados (tratamientos por diente y otros tratamientos) con sus datos de CUPS, profesional, unidad funcional, RIAS y estado de vinculación a orden de servicio, para soporte del proceso de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas ODONTOCONTROL, ODONTODIENTE, ODONTODIENTETRATA, ODONTOOTROSTRA, ODOPARTRA, INCUPSIPS, INUNIFUNC e INPROFSAL deben tener integridad referencial por sus códigos (CONSECTRA, CODSERIPS, UFUCODIGO, CODPROSAL).; El profesional (CODPROSAL) y la unidad funcional (UFUCODIGO) registrados en ODONTOCONTROL deben existir en sus catálogos maestros, de lo contrario la fila se excluye.; El código CUPS del tratamiento debe existir en INCUPSIPS para que el procedimiento aparezca en la vista.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un procedimiento odontológico agregado por código CUPS, profesional, unidad funcional, folio e ingreso, con cantidad sumarizada.; El identificador Id se construye concatenando IDs de ODONTOCONTROL + ODONTODIENTE/ODONTOOTROSTRA + ODONTODIENTETRATA/CONSECTRA, garantizando unicidad por línea de tratamiento.; Solo se incluyen procedimientos cuyo CUPS exista en INCUPSIPS (INNER JOIN obligatorio).; Los tratamientos provienen de dos fuentes unidas por UNION: tratamientos por diente (ODONTODIENTETRATA) y otros tratamientos (ODONTOOTROSTRA).; RiasCupsId se normaliza a 0 cuando es NULL en ODONTOCONTROL.; ServiceOrderId se normaliza a 0 cuando no existe vínculo en BillingOdontologyProcedureServiceOrder.; ApplyRIAS proviene de la columna APLICARIAS del CUPS, indicando si el procedimiento es aplicable a una RIAS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimiento odontológico; Odontograma; CUPS (codificación de servicios de salud); RIAS (Rutas Integrales de Atención en Salud); Folio de atención; Número de ingreso/admisión; Profesional de salud; Unidad funcional; Centro de atención; Orden de servicio de facturación; Especialidad profesional', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve procedimientos odontológicos agrupados por procedimiento/CUPS/profesional/unidad/folio/ingreso, marcando Selection=1 si ya existen en Billing.BillingOdontologyProcedureServiceOrder por AdmissionNumber+Folio+ProcedureCode, y Selection=0 en caso contrario.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si opso.Id IS NULL (no existe vínculo en BillingOdontologyProcedureServiceOrder para AdmissionNumber+Folio+ProcedureCode) → Selection = 0 (procedimiento aún no asociado a orden de servicio) else Selection = 1 (procedimiento ya asociado a una orden de servicio de facturación); si od.TIPOODONTOGRAMA = ''T'' en la rama de ODONTODIENTE/ODONTODIENTETRATA → Solo se incluyen tratamientos del odontograma tipo ''T'' (definitivo/tratamiento); si Existe RiasCupsId con coincidencia en RIASCUPS y RIAS → RiasDescription = CODPRO + '' - '' + NOMBRE else RiasDescription = '''' (cadena vacía cuando no hay RIAS asociada)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ODONTOCONTROL; dbo.ODONTODIENTE; dbo.ODONTODIENTETRATA; dbo.ODONTOOTROSTRA; dbo.ODOPARTRA; dbo.INCUPSIPS; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.RIASCUPS; dbo.RIAS; Billing.BillingOdontologyProcedureServiceOrder', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOdontologyProcedures';
GO
