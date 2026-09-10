

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de una inconsistencia de base de datos de afiliados (ADINCBASD) identificada por su número consecutivo, consolidando en un único resultado los datos de la empresa prestadora (razón social y NIT desde INEMPRESU), los datos del centro de atención incluyendo dirección, teléfono, municipio y departamento (ADCENATEN, INMUNICIP, INDEPARTA), los datos demográficos y de contacto del paciente (INPACIENT), la entidad pagadora o EPS responsable (INENTIDAD), el tipo de inconsistencia reportada (tipo 1 o tipo 2), el usuario que registró la novedad (SEGusuaru) y el contacto de emergencia o acompañante del paciente (ADACOMPAN). Se utiliza para imprimir o visualizar el formulario oficial de reporte de inconsistencia de afiliación en el proceso de admisión, identificando al paciente por cédula, el ingreso y el consecutivo del reporte.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListNewInconsistencyReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListNewInconsistencyReport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultado los datos necesarios para imprimir/visualizar el reporte de inconsistencias básicas de admisión: información de la empresa, del centro de atención prestador, del paciente, del pagador, tipo de inconsistencia y contacto de emergencia.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El consecutivo del reporte de inconsistencia (CODCONCEC) debe existir en ADINCBASD con relaciones válidas hacia SEGUSUARU, ADCENATEN, INENTIDAD, INPACIENT, INMUNICIP e INDEPARTA; de lo contrario el bloque T2 retorna vacío.; La empresa debe existir en INEMPRESU (INDCODEMP) para obtener nombre y NIT; si no existe, no hay fila base T1.; Para obtener el contacto de emergencia debe existir un registro en ADACOMPAN para el paciente e ingreso indicados con RESPONSAB = 2.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código del reporte se construye concatenando año, mes, día de FECINFORM más CODCONCEC en formato yyyMMdd+consecutivo.; La fecha del reporte se formatea como ''yyy-MM-dd hh:mm''.; Las inconsistencias tipo 1 y tipo 2 son mutuamente excluyentes en el resultado: solo una de las columnas se marca con ''X'' según TIPINCBAS.; Se prefiere el teléfono móvil sobre el fijo cuando el móvil no está vacío.; Como dirección alternativa se considera únicamente una dirección no principal (IsMain = 0) del paciente.; El contacto de emergencia es exclusivamente el acompañante con RESPONSAB = 2 para el ingreso indicado.; La persona que reporta se muestra como ''CODUSUARI - NOMUSUARI''.; El uso de OUTER APPLY garantiza que se devuelva fila aunque no existan datos del reporte o del contacto de emergencia.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empresa/IPS; Centro de atención prestador; Paciente; Pagador/Entidad; Inconsistencia básica de admisión; Tipo de documento; Dirección del paciente; Municipio y departamento; Contacto de emergencia/acompañante; Persona que reporta la inconsistencia', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por reporte de inconsistencia consultado, combinando datos de empresa, prestador, paciente, pagador y contacto de emergencia.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPINCBAS = ''1'' → Marca con ''X'' la columna InconsistenciaTipo1 (inconsistencia tipo 1). else InconsistenciaTipo1 queda en NULL; si TIPINCBAS = ''2'' → Marca con ''X'' la columna InconsistenciaTipo2 (inconsistencia tipo 2). else InconsistenciaTipo2 queda en NULL; si E.IPTELMOVI <> '''' → Usa el teléfono móvil del paciente como TELEFONO. else Usa E.IPTELEFON (teléfono fijo) como TELEFONO.; si ADACOMPAN.RESPONSAB = 2 → Toma esa persona como contacto de emergencia (NombreEmergencia y TelefonoEmergencia).; si Admissions.PatientAddress.IsMain = 0 (TOP 1) → Devuelve esa dirección como DireccionAlternativa del paciente.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumento; dbo.Only_Patient_Address; dbo.patient_municipality; dbo.patient_municipalityCode', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU; dbo.ADINCBASD; dbo.SEGusuaru; dbo.ADCENATEN; dbo.INENTIDAD; dbo.INPACIENT; dbo.INMUNICIP; dbo.INDEPARTA; Admissions.PatientAddress; dbo.ADACOMPAN', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListNewInconsistencyReport';
-- GO
