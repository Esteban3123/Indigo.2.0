
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