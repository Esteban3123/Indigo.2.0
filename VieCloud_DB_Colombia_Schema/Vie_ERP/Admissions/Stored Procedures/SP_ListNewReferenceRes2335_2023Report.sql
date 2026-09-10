
CREATE PROCEDURE [Admissions].[SP_ListNewReferenceRes2335_2023Report]
@PACIENTE VARCHAR(50),
@INGRESO VARCHAR(50),
@EMPRESA VARCHAR(50),
@TABLAHC VARCHAR(50),
@FOLIO VARCHAR(10),
@TIPOSOLICITUD VARCHAR(50),
@CONSECUTIVO VARCHAR(50)

AS
BEGIN
    
	declare @SQl as nvarchar(max)

	set @SQL = 'Select T2.Codigo, T2.FechaFormato, T1.NombreEmpresa, T1.NIT, T2.CodigoPrestador, T2.DireccionPrestador, T2.TelefonoPrestador, T2.DepartamentoPrestador,
		   T2.CDPrestador, T2.MunicipioPrestador, T2.CMPrestador, T2.NombrePagador, T2.CodigoPagador, T2.IPPRIAPEL, T2.IPSEGAPEL, T2.IPPRINOMB, T2.IPSEGNOMB, 
		   T2.TipoDocumento, T2.IPCODPACI, T2.IPFECNACI, T2.DIRECCION, T2.TELEFONO, T2.MunicipioUsuario, T2.CMUsuario, T2.CorreoUsuario, T2.DireccionAlternativa, 
		   T2.CodCausaAtencion, T2.CausaAtencion, T2.CodigoRemite, T2.CodPrioridadAtencion, T2.PrioridadAtencion, T2.CodTipoAtencion, T2.TipoAtencion, 
		   T2.CodModalidadAtencion, T2.ModalidadAtencion, T2.Folio, T3.NombreEmergencia, T3.TelefonoEmergencia, T2.Observaciones, 
		   T2.UsuarioReporta, T2.DestinoPaciente, T2.DestinoPacienteDescripcion
	from 
	
	(
		SELECT RTRIM(INDNOMEMP) AS NombreEmpresa, INDNUMIDE AS NIT FROM INEMPRESU WHERE INDCODEMP = '''+@EMPRESA+'''
	) AS T1 --Datos empresa
	OUTER APPLY
	(
		SELECT 
		CONCAT(FORMAT(ISNULL(T.FECSOLICIT, P.FECSOLICIT), ''yyyy''),FORMAT(ISNULL(T.FECSOLICIT, P.FECSOLICIT), ''MM''), FORMAT(ISNULL(T.FECSOLICIT, P.FECSOLICIT), ''dd''), CODCONCEC) AS Codigo, 
		Format(ISNULL(T.FECSOLICIT, P.FECSOLICIT), ''yyyy-MM-dd HH:mm'') AS FechaFormato, CODIPSSEC AS CodigoPrestador, RTRIM(DIRCENATE) AS DireccionPrestador, NUMTELCEN as TelefonoPrestador, 
		RTRIM(nomdepart) AS DepartamentoPrestador, E.depcodigo AS CDPrestador, RTRIM(MUNNOMBRE) AS MunicipioPrestador, D.MUNCODIGO AS CMPrestador, HA.Name AS NombrePagador, 
		HA.HealthEntityCode AS CodigoPagador, B.IPPRIAPEL, B.IPSEGAPEL, B.IPPRINOMB, B.IPSEGNOMB, dbo.TipoDocumento(B.IPTIPODOC) AS TipoDocumento, B.IPCODPACI, B.IPFECNACI,
		dbo.Only_Patient_Address(B.IPCODPACI) AS DIRECCION, IIF(B.IPTELMOVI <> '''', B.IPTELMOVI, B.IPTELEFON) AS TELEFONO, dbo.patient_municipality(B.IPCODPACI) AS MunicipioUsuario, 
		dbo.patient_DepartmentAndMunicipality(B.IPCODPACI) AS CMUsuario, CORELEPAC AS CorreoUsuario, (select TOP 1  Address	from Admissions.PatientAddress WITH(NOLOCK) WHERE IPCODPACI = B.IPCODPACI and IsMain = 0) AS DireccionAlternativa,
		I.ICAUSAING AS CodCausaAtencion, dbo.Causeofattention(ICAUSAING) AS CausaAtencion, CODIPSSEC AS CodigoRemite, 
		ISNULL(CASE T.RequestPriority WHEN 1 THEN ''01'' WHEN 2 THEN ''02'' END, CASE P.RequestPriority WHEN 1 THEN ''01'' WHEN 2 THEN ''02'' END) AS CodPrioridadAtencion,
		ISNULL(CASE T.RequestPriority WHEN 1 THEN ''Prioritaria'' WHEN 2 THEN ''No prioritaria'' END, CASE P.RequestPriority WHEN 1 THEN ''Prioritaria'' WHEN 2 THEN ''No prioritaria'' END) AS PrioridadAtencion,
		ISNULL(T.OBSERVACIO, P.OBSERVACIO) As Observaciones,
		CASE WHEN (select COUNT(UFUTIPUNI) As UnidadUrgencias from ADINGRESO A INNER JOIN INUNIFUNC B On A.UFUCODIGO = B.UFUCODIGO where numingres = '''+@INGRESO+''' AND UFUTIPUNI = 1) > 0 THEN ''01'' 
		WHEN (select COUNT(ISNULL(T.RequestPriority, P.RequestPriority)) AS TipoPrioritaria from HCREFCONT T INNER JOIN HCREFCONP P ON T.IDHCREFCONP = P.AUTO where T.numingres = '''+@INGRESO+''' AND (T.RequestType = 1 OR P.RequestType = '+@TIPOSOLICITUD+') AND (T.RequestPriority = 1 OR P.RequestPriority = 1)) > 0 THEN ''02''
		WHEN (select COUNT(ISNULL(T.RequestPriority, P.RequestPriority)) AS TipoPrioritaria from HCREFCONT T INNER JOIN HCREFCONP P ON T.IDHCREFCONP = P.AUTO where T.numingres = '''+@INGRESO+''' AND (T.RequestType = 1 OR P.RequestType = '+@TIPOSOLICITUD+') AND (T.RequestPriority = 1 OR P.RequestPriority = 1)) > 0 THEN ''03''
		END AS CodTipoAtencion,
		CASE WHEN (select COUNT(UFUTIPUNI) As UnidadUrgencias from ADINGRESO A INNER JOIN INUNIFUNC B On A.UFUCODIGO = B.UFUCODIGO where numingres = '''+@INGRESO+''') > 0 THEN ''Servicios y tecnologías en casos posteriores a urgencia'' 
		WHEN (select COUNT(ISNULL(T.RequestPriority, P.RequestPriority)) AS TipoPrioritaria from HCREFCONT T INNER JOIN HCREFCONP P ON T.IDHCREFCONP = P.AUTO where T.numingres = '''+@INGRESO+''' AND (T.RequestType = 1 OR P.RequestType = '+@TIPOSOLICITUD+') AND (T.RequestPriority = 2 OR P.RequestPriority = 2)) > 0 THEN ''Servicios y tecnologías en atención prioritaria''
		WHEN (select COUNT(ISNULL(T.RequestPriority, P.RequestPriority)) AS TipoPrioritaria from HCREFCONT T INNER JOIN HCREFCONP P ON T.IDHCREFCONP = P.AUTO where T.numingres = '''+@INGRESO+''' AND (T.RequestType = 1 OR P.RequestType = '+@TIPOSOLICITUD+') AND (T.RequestPriority = 2 OR P.RequestPriority = 2)) > 0 THEN ''Servicios y tecnologías de salud electivos o programables no prioritaria''
		END AS TipoAtencion, [dbo].[AdmissionModalitiesCodeRIPS](I.IdAdmissionModalities) as CodModalidadAtencion, [dbo].[AdmissionModalities](I.IdAdmissionModalities) AS ModalidadAtencion, 
		CONCAT(RTRIM(T.CODUSUMOD), '' - '', RTRIM(SU.NOMUSUARI)) AS UsuarioReporta, A.NUMEFOLIO AS Folio,
		IIF(I.IINGREPOR = 4 And A.INDICAPAC = 10,''05'', IIF(A.INDICAPAC = 10 ,''04'',IIF(A.INDICAPAC IN (2,3,4,5,6,8,13,19,21,22),''08'',IIF(A.INDICAPAC =12 and ((SELECT COUNT(IPCODPACI) FROM HCREFCONT WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND RequestType = '+@TIPOSOLICITUD+' AND EXTRAMURAL = 1) > 0), ''03'',CASE A.INDICAPAC WHEN 16 THEN ''01'' WHEN 15 THEN ''01'' WHEN 11 THEN ''02'' WHEN 9 THEN ''06'' WHEN 12 THEN ''01'' WHEN 1 THEN ''03'' WHEN 7 THEN ''03'' END)))) AS DestinoPaciente,
		IIF(I.IINGREPOR = 4 And A.INDICAPAC = 10,''Contra referido a otra institución'', IIF(A.INDICAPAC = 10 ,''Referido a otra institución'',IIF(A.INDICAPAC IN (2,3,4,5,6,8,13,19,21,22),''Paciente continua en el servicio (corte facturación)'',IIF(A.INDICAPAC =12 and ((SELECT COUNT(IPCODPACI) FROM HCREFCONT WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND RequestType = '+@TIPOSOLICITUD+' AND EXTRAMURAL = 1) > 0), ''Paciente derivado a otro servicio'',CASE A.INDICAPAC WHEN 16 THEN ''Paciente con destino a su domicilio'' WHEN 15 THEN ''Paciente con destino a su domicilio'' WHEN 11 THEN ''Paciente muerto'' WHEN 9 THEN ''Derivado o referido a hospitalización domiciliaria'' WHEN 12 THEN ''Paciente con destino a su domicilio'' WHEN 1 THEN ''Paciente derivado a otro servicio'' WHEN 7 THEN ''Paciente derivado a otro servicio'' END)))) AS DestinoPacienteDescripcion
		FROM dbo.' + @TABLAHC + ' A 
		INNER JOIN dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI 
		INNER JOIN dbo.INUbicaci C ON B.AUUBICACI=C.AUUBICACI 
		INNER JOIN dbo.INMunicip D ON C.DEPMUNCOD=D.DEPMUNCOD 
		INNER JOIN dbo.INDEPARTA E ON D.DEPCODIGO=E.depcodigo 
		INNER JOIN dbo.ADCENATEN G ON A.CODCENATE=G.CODCENATE 
		INNER JOIN dbo.ADINGRESO I ON A.NUMINGRES=I.NUMINGRES 
		LEFT JOIN Admissions.AdmissionModalities AM ON I.IdAdmissionModalities = AM.Id
		INNER JOIN dbo.INENTIDAD H ON I.CODENTIDA=H.CODENTIDA 
		INNER JOIN Contract.HealthAdministrator HA ON H.CODENTIDA = HA.Code
		LEFT JOIN dbo.INPROFSAL PR ON I.CODPROEGR=PR.CODPROSAL 
		LEFT JOIN dbo.INESPECIA ES ON A.CODESPTRA=ES.CODESPECI 
		LEFT OUTER JOIN dbo.HCREGEGRE L ON A.NUMINGRES=L.NUMINGRES  
		LEFT JOIN dbo.HCREFCONT T ON A.IPCODPACI = T.IPCODPACI AND A.NUMINGRES = T.NUMINGRES AND RequestType = '+@TIPOSOLICITUD+'
		LEFT JOIN dbo.HCREFCONP P ON T.IDHCREFCONP = P.AUTO
		LEFT JOIN dbo.SEGusuaru SU ON SU.CODUSUARI=T.CODUSUMOD
		LEFT JOIN Admissions.PatientAddress PA ON B.IPCODPACI = PA.IPCODPACI  
		WHERE A.IPCODPACI='''+@PACIENTE+''' AND A.NUMINGRES='''+@INGRESO+''' AND A.NUMEFOLIO='''+@FOLIO+''' AND T.AUTO='''+@CONSECUTIVO+'''
	) AS T2 --Datos referencia
	OUTER APPLY 
	(	
		Select CONCAT(PRINOMBRE, '' '', SEGNOMBRE, '' '', PRIAPELLI, '' '', SEGAPELLI) AS NombreEmergencia, TELACOMPA AS TelefonoEmergencia from ADACOMPAN WHERE IPCODPACI = '''+@PACIENTE+''' AND NUMINGRES = '''+@INGRESO+''' AND RESPONSAB = 2
	) AS T3 --Datos contacto de emergencia'

	--print @sql
	exec sp_executesql  @SQl 
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de referencias y contrarreferencias de pacientes según la Resolución 2335 de 2023 del Ministerio de Salud de Colombia. A partir del número de ingreso, la cédula del paciente, la empresa, el folio, el tipo de solicitud y un consecutivo, construye dinámicamente una consulta que consolida los datos del prestador (IPS), del pagador (aseguradora), del paciente (identificación, fecha de nacimiento, dirección, teléfono, correo), así como la información clínica y administrativa de la referencia: causa de atención, prioridad, tipo y modalidad de atención, destino del paciente y observaciones. Está diseñado para alimentar el formulario oficial RIPS de referencias exigido por la normativa vigente, cruzando datos de admisiones, historia clínica de referencias (HCREFCONT/HCREFCONP), unidades funcionales y datos maestros de empresa y paciente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y ejecuta dinámicamente la consulta que arma el reporte de referencia/contrarreferencia conforme a la Resolución 2335 de 2023, integrando datos de empresa, prestador, paciente, ingreso, pagador, contacto de emergencia y destino del paciente.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@EMPRESA debe existir en INEMPRESU (INDCODEMP) para obtener nombre y NIT del prestador.; @TABLAHC debe ser el nombre de una tabla válida del esquema dbo (se concatena en SQL dinámico).; @PACIENTE, @INGRESO y @FOLIO deben corresponder a un registro existente en la tabla de historia clínica indicada y en ADINGRESO.; @CONSECUTIVO debe coincidir con HCREFCONT.AUTO de la referencia a reportar.; @TIPOSOLICITUD debe ser un valor numérico válido para HCREFCONT.RequestType (se inyecta directo en el SQL).; Debe existir relación válida entre paciente, ubicación, municipio y departamento (joins INNER en INPACIENT/INUbicaci/INMunicip/INDEPARTA).', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve la fila de empresa cuya INDCODEMP coincide con el parámetro de empresa.; El contacto de emergencia se obtiene exclusivamente de ADACOMPAN con RESPONSAB = 2.; La dirección alternativa proviene del primer registro de Admissions.PatientAddress con IsMain = 0.; Los códigos de prioridad y destino siguen el catálogo numérico definido por la Resolución 2335 de 2023.; El reporte se filtra siempre por paciente, ingreso, folio y consecutivo de la referencia (HCREFCONT.AUTO).; Si A.INDICAPAC = 12 sin referencia extramural, el destino se asigna como domicilio (''01'').; El SQL se construye dinámicamente concatenando el nombre de la tabla de historia clínica recibido en @TABLAHC.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia y contrarreferencia; Resolución 2335 de 2023; Tipo de atención (urgencia, prioritaria, electiva); Modalidad de admisión; Prioridad de solicitud; Destino del paciente al egreso; Causa de atención; Pagador / Administradora de salud (EPS); Contacto de emergencia / acompañante responsable; Centro de atención / IPS prestador; Folio de admisión; Atención extramural', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe ingreso asociado a una unidad funcional con UFUTIPUNI = 1 (unidad de urgencias) → CodTipoAtencion = ''01'' y TipoAtencion = ''Servicios y tecnologías en casos posteriores a urgencia''; si No es urgencia y existe HCREFCONT/HCREFCONP con RequestPriority = 1 (prioritaria) para el ingreso y tipo de solicitud → CodTipoAtencion = ''02'' y TipoAtencion = ''Servicios y tecnologías en atención prioritaria''; si Existen referencias con RequestPriority = 2 (no prioritaria) → TipoAtencion = ''Servicios y tecnologías de salud electivos o programables no prioritaria'' (CodTipoAtencion ''03''); si I.IINGREPOR = 4 AND A.INDICAPAC = 10 → DestinoPaciente = ''05'' Contra referido a otra institución; si A.INDICAPAC = 10 (y no aplica la anterior) → DestinoPaciente = ''04'' Referido a otra institución; si A.INDICAPAC IN (2,3,4,5,6,8,13,19,21,22) → DestinoPaciente = ''08'' Paciente continúa en el servicio (corte facturación); si A.INDICAPAC = 12 y existe HCREFCONT con EXTRAMURAL = 1 para el paciente/ingreso/tipo de solicitud → DestinoPaciente = ''03'' Paciente derivado a otro servicio; si A.INDICAPAC IN (16,15,12) → DestinoPaciente = ''01'' Paciente con destino a su domicilio; si A.INDICAPAC = 11 → DestinoPaciente = ''02'' Paciente muerto; si A.INDICAPAC = 9 → DestinoPaciente = ''06'' Derivado o referido a hospitalización domiciliaria; si A.INDICAPAC IN (1,7) → DestinoPaciente = ''03'' Paciente derivado a otro servicio; si RequestPriority = 1 en HCREFCONT/HCREFCONP → CodPrioridadAtencion = ''01'' / ''Prioritaria'' else RequestPriority = 2 → ''02'' / ''No prioritaria''; si B.IPTELMOVI distinto de vacío → TELEFONO = IPTELMOVI else TELEFONO = IPTELEFON', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumento; dbo.Only_Patient_Address; dbo.patient_municipality; dbo.patient_DepartmentAndMunicipality; dbo.Causeofattention; dbo.AdmissionModalitiesCodeRIPS; dbo.AdmissionModalities', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU; dbo.INPACIENT; dbo.INUbicaci; dbo.INMunicip; dbo.INDEPARTA; dbo.ADCENATEN; dbo.ADINGRESO; Admissions.AdmissionModalities; dbo.INENTIDAD; Contract.HealthAdministrator; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCREGEGRE; dbo.HCREFCONT; dbo.HCREFCONP; dbo.SEGusuaru; Admissions.PatientAddress; dbo.ADACOMPAN; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewReferenceRes2335_2023Report';
-- GO
