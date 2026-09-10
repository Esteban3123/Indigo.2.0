

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentesConsultaExternaDetalle]
@Profesional Char(20),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

SELECT IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,FECAUSENT AS FechaAusente,rtrim(PROPRILLA)+' - '+rtrim(B.NOMMEDICO) AS PriLlamado ,
rtrim(PROSEGLLA)+' - '+rtrim(C.NOMMEDICO) AS SegLlamado,rtrim(PROTERLLA)+' - '+rtrim(D.NOMMEDICO) AS TerLlamado
FROM ADCONCOEX A
INNER JOIN INPROFSAL B ON A.PROPRILLA =B.CODPROSAL 
INNER JOIN INPROFSAL C ON A.PROSEGLLA =C.CODPROSAL
INNER JOIN INPROFSAL D ON A.PROTERLLA =D.CODPROSAL 
WHERE CONESTADO='2' AND A.PROAUSENT=@Profesional AND FECAUSENT BETWEEN @FechaInicial AND @FechaFinal 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de pacientes ausentes (inasistentes) en consulta externa para un profesional de la salud específico dentro de un rango de fechas. Para cada ausencia registrada, muestra la fecha de ausencia y los tres llamados realizados al paciente (primer, segundo y tercer llamado), resolviendo el nombre completo de cada profesional que realizó el llamado desde el maestro de profesionales. Filtra únicamente los registros con estado de ausencia confirmada (CONESTADO=''2'') y el profesional ausente indicado. Este reporte se usa para hacer seguimiento y auditoría de pacientes que no se presentaron a sus citas de consulta externa, detallando quién los llamó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle de pacientes ausentes en consulta externa para un profesional dado y rango de fechas, mostrando los profesionales llamados en primer, segundo y tercer turno.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben tener estado ''2'' (ausente/no asistencia); Los códigos de profesional de primer, segundo y tercer llamado deben existir en INPROFSAL (INNER JOIN obligatorios); El profesional ausente debe coincidir con el parámetro recibido; La fecha de ausencia debe estar dentro del rango indicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros con estado de cita = ''2''; La llave de salida combina código de paciente con la fecha de ausencia; Si alguno de los tres profesionales llamados no existe en INPROFSAL, la fila se excluye por el INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente ausente; Consulta externa; Profesional de salud; Llamados a consulta (primer, segundo, tercer llamado); Estado de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas de ADCONCOEX con CONESTADO=''2'', PROAUSENT=@Profesional y FECAUSENT entre @FechaInicial y @FechaFinal, concatenando código+nombre de los tres profesionales llamados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesConsultaExternaDetalle';
-- GO
