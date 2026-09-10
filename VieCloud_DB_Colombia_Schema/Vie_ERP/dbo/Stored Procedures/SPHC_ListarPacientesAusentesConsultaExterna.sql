
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentesConsultaExterna]
@Profesional Char(20),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,
IPCODPACI AS Paciente,RTRIM(IPNOMCOMP) AS NombreCompleto,CODCENATE AS CentroAtencion,UFUCODIGO AS UnidadFuncional,CASE CONESTADO WHEN '2' THEN 'Consulta Externa' END AS Estado ,
FECPRILLA AS PrimerLlamado,FECSEGLLA AS SegundoLlamado,FECTERLLA AS TercerLlamado,OBVAUSENT AS Observacion,FECAUSENT AS FechaAusente,PROAUSENT,NOMMEDICO AS  ProfesionalAusenta
FROM ADCONCOEX a
INNER JOIN INPROFSAL c on a.PROAUSENT=c.CODPROSAL 
WHERE 
CONESTADO='2' AND PROAUSENT=@Profesional AND FECAUSENT BETWEEN @FechaInicial AND @FechaFinal 
  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que no se presentaron (ausentes) a sus citas de Consulta Externa en un rango de fechas y para un profesional de salud específico. Consulta la tabla de citas de consulta externa (ADCONCOEX) filtrando por estado ''Consulta Externa'' y cruza con el maestro de profesionales (INPROFSAL) para obtener el nombre del médico o especialista que registró la ausencia. Devuelve datos clave como cédula del paciente, nombre completo, centro de atención, unidad funcional, fecha de ausencia, los tres intentos de llamado realizados, observaciones y el profesional que ausentó al paciente. Se usa para reportes de inasistencia, seguimiento de pacientes no contactados y gestión de agendas en consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes marcados como ausentes en consulta externa para un profesional dado dentro de un rango de fechas, incluyendo llamados, observación y datos del profesional que registró la ausencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El profesional consultado debe existir en INPROFSAL para que el INNER JOIN devuelva filas; Las fechas inicial y final deben definir un rango válido para FECAUSENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros cuyo estado de consulta sea ''2'' (Consulta Externa); Solo se incluyen ausencias asociadas a un profesional existente en el catálogo de profesionales de la salud; La llave de salida se construye concatenando código de paciente con la fecha de ausencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Consulta Externa; Ausencia a consulta; Profesional de la salud; Centro de atención; Unidad funcional; Llamados al paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADCONCOEX: Cuando CONESTADO=''2'' AND PROAUSENT=@Profesional AND FECAUSENT BETWEEN @FechaInicial AND @FechaFinal, se retorna el conjunto de pacientes ausentes con sus datos de llamados y profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CONESTADO = ''2'' → Se etiqueta el estado como ''Consulta Externa'' en la salida else No se asigna etiqueta de estado (NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExterna';
-- GO
