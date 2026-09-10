
CREATE PROCEDURE [dbo].[SPRIS_ListarCabeceraInterfazSP]
AS
BEGIN
	SET NOCOUNT ON;
SELECT CODCONSEC, IPCODPACI, NOMPACIEN, IPPRIAPEL, IPSEGAPEL, IPFECNACI, IPSEXOPAC, TIPMODALI, CODSERIPS, DESSERIPS, FECHTURNO, HORATURNO, INESTADOT, PROCESADO, REPSERPAC, OBSSERPAC, FECNACDAT, EXAURGPAC, NOMSERPAC 
FROM dbo.RISTRACAB WITH(NOLOCK)
WHERE CODCONSEC=''
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de interfaz que lista los registros de la cabecera de turnos y citas agendadas (tabla RISTRACAB) para su integración con sistemas externos. Retorna los datos principales del turno: código del paciente (cédula), nombre completo, apellidos, fecha y sexo del paciente, el servicio solicitado (CUPS/IPS), fecha y hora del turno, estado del agendamiento y observaciones del servicio. En la práctica, el filtro por CODCONSEC vacío hace que devuelva un resultado vacío, por lo que su uso principal es como plantilla o prueba de la estructura de datos de interfaz RIPS/agendamiento. Sirve como punto de extracción de información de citas para reportería o intercambio con sistemas externos de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista cabeceras de turnos/citas de la interfaz RIS filtradas por un código consecutivo vacío, retornando los datos del paciente y del servicio agendado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla de cabeceras de la interfaz RIS debe existir y estar accesible para lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna cabeceras cuyo código consecutivo está vacío.; Lectura sin bloqueo (NOLOCK), por lo que puede incluir lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Turno/Cita; Servicio de salud; Interfaz RIS; Modalidad; Sexo; Fecha de nacimiento; Examen de urgencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.RISTRACAB: Devuelve registros donde CODCONSEC = '''' (cadena vacía), incluyendo datos demográficos del paciente, servicio, fecha/hora de turno, estado y observaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RISTRACAB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarCabeceraInterfazSP';
-- GO
