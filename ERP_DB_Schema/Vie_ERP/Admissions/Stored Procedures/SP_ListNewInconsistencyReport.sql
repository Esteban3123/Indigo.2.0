

CREATE PROCEDURE [Admissions].[SP_ListNewInconsistencyReport]
@PACIENTE VARCHAR(50),
@INGRESO VARCHAR(50),
@EMPRESA VARCHAR(50),
@CONSECUTIVO VARCHAR(50)

AS
BEGIN
	SET NOCOUNT ON;

	Select T2.Codigo, T2.FechaFormato, T1.NombreEmpresa, T1.NIT, T2.CodigoPrestador, T2.DireccionPrestador, T2.TelefonoPrestador, T2.DepartamentoPrestador,
		   T2.CDPrestador, T2.MunicipioPrestador, T2.CMPRestador, T2.NombrePagador, T2.CodigoPagador, T2.InconsistenciaTipo1, T2.InconsistenciaTipo2,
		   T2.IPPRIAPEL, T2.IPSEGAPEL, T2.IPPRINOMB, T2.IPSEGNOMB, T2.TipoDocumento, T2.IPCODPACI, T2.IPFECNACI, T2.DIRECCION, T2.TELEFONO, T2.MunicipioUsuario,
		   T2.CMUsuario, T2.CorreoUsuario, T2.DireccionAlternativa, T3.NombreEmergencia, T3.TelefonoEmergencia, T2.Observaciones, T2.PersonaReporta
	from 
	(SELECT RTRIM(INDNOMEMP) AS NombreEmpresa, INDNUMIDE AS NIT FROM INEMPRESU WHERE INDCODEMP = @EMPRESA) AS T1 --Datos empresa
	OUTER APPLY 
	(	
		SELECT 
		CODIPSSEC AS CodigoPrestador, RTRIM(DIRCENATE) AS DireccionPrestador, NUMTELCEN as TelefonoPrestador,RTRIM(nomdepart) AS DepartamentoPrestador, H.depcodigo AS CDPrestador, RTRIM(MUNNOMBRE) AS MunicipioPrestador, G.MUNCODIGO AS CMPRestador,	
		
		CONCAT(FORMAT(FECINFORM, 'yyy'),FORMAT(FECINFORM, 'MM'), FORMAT(FECINFORM, 'dd'), CODCONCEC) AS Codigo, Format(FECINFORM, 'yyy-MM-dd hh:mm') AS FechaFormato, NUMINFORM, E.IPPRIAPEL, E.IPSEGAPEL, E.IPPRINOMB, E.IPSEGNOMB, dbo.TipoDocumento(E.IPTIPODOC) AS TipoDocumento, E.IPCODPACI, E.IPFECNACI, dbo.Only_Patient_Address(E.IPCODPACI) AS DIRECCION , IIF(E.IPTELMOVI <> '', 
		E.IPTELMOVI, E.IPTELEFON) AS TELEFONO, dbo.patient_municipality(E.IPCODPACI) AS MunicipioUsuario, dbo.patient_municipalityCode(E.IPCODPACI) AS CMUsuario, CORELEPAC AS CorreoUsuario, (select TOP 1  Address	from Admissions.PatientAddress WITH(NOLOCK) WHERE IPCODPACI = E.IPCODPACI and IsMain = 0) AS DireccionAlternativa, NOMENTIDA AS NombrePagador, CODADMPAG AS CodigoPagador, 
		CASE TIPINCBAS WHEN '1' THEN 'X' END AS InconsistenciaTipo1,CASE TIPINCBAS WHEN '2' THEN 'X' END AS InconsistenciaTipo2, OBSGENINC AS Observaciones, CONCAT(RTRIM(B.CODUSUARI), ' - ',RTRIM(NOMUSUARI))  AS PersonaReporta
		FROM ADINCBASD A 
		INNER JOIN SEGusuaru B ON A.CODUSUARI=B.CODUSUARI 
		INNER JOIN ADCENATEN C ON A.CODCENATE=C.CODCENATE 
		INNER JOIN INENTIDAD D ON A.CODENTIDA=D.CODENTIDA 
		INNER JOIN INPACIENT E ON A.IPCODPACI=E.IPCODPACI 
		INNER JOIN INMUNICIP G ON C.DEPMUNCOD=G.DEPMUNCOD 
		INNER JOIN INDEPARTA H ON G.DEPCODIGO=H.DEPCODIGO
		WHERE CODCONCEC = @CONSECUTIVO
	) AS T2 --Datos reporte
	OUTER APPLY 
	(Select CONCAT(PRINOMBRE, ' ', SEGNOMBRE, ' ', PRIAPELLI, ' ', SEGAPELLI) AS NombreEmergencia, TELACOMPA AS TelefonoEmergencia from ADACOMPAN WHERE IPCODPACI = @PACIENTE AND NUMINGRES = @INGRESO AND  RESPONSAB = 2) AS T3 --Datos contacto de emergencia
END