
CREATE  PROCEDURE [dbo].[SPCH_PacienteHistoriasMobile]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT RTRIM(C.DESESPECI) as Especialidad,rtrim(b.NOMMEDICO) as Profesional,rtrim(a.CODPROSAL) as CodigoProfesional,a.FECHINIHI as FechaHistoria,CONVERT(varchar,a.FECINIATE, 0) as 'FechaHistoriaFormateada',a.ANALISISP as Analisis, 'Historia Ingreso' as Tipo, TRIM(A.NUMEFOLIO) AS Folio
	FROM HCURGING1 A INNER JOIN
	INPROFSAL B ON A.CODPROSAL=B.CODPROSAL INNER JOIN
	INESPECIA C ON A.CODESPTRA=C.CODESPECI
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso
	UNION ALL

		SELECT RTRIM(C.DESESPECI) as Especialidad,rtrim(b.NOMMEDICO) as Profesional,rtrim(a.CODPROSAL) as CodigoProfesional,a.FECINIATE as FechaHistoria,CONVERT(varchar,a.FECINIATE, 0) as 'FechaHistoriaFormateada',a.ANALISISP as Analisis, 'Historia Evolucion' as Tipo, TRIM(A.NUMEFOLIO) AS Folio
	FROM HCURGEVO1 A INNER JOIN
	INPROFSAL B ON A.CODPROSAL=B.CODPROSAL INNER JOIN
	INESPECIA C ON A.CODESPTRA=C.CODESPECI
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso

	UNION ALL

		SELECT RTRIM(C.DESESPECI) as Especialidad,rtrim(b.NOMMEDICO) as Profesional,rtrim(a.CODPROSAL) as CodigoProfesional,a.FECINIATE as FechaHistoria,CONVERT(varchar,a.FECINIATE, 0) as 'FechaHistoriaFormateada' ,a.ANALISISP as Analisis, 'Historia Nota Rapida' as Tipo, TRIM(A.NUMEFOLIO) AS Folio
	FROM HCNOTEVO1 A INNER JOIN
	INPROFSAL B ON A.CODPROSAL=B.CODPROSAL INNER JOIN
	INESPECIA C ON A.CODESPTRA=C.CODESPECI
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso

	
	
	
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para aplicación móvil que obtiene el historial clínico completo de un paciente en urgencias dado su código de paciente (cédula) y número de ingreso. Consolida en un único resultado tres tipos de registros: la historia inicial de urgencias (HCURGING1), las notas de evolución clínica (HCURGEVO1) y las notas rápidas (HCNOTEVO1), enriqueciendo cada registro con el nombre del profesional de la salud tratante (INPROFSAL) y la descripción de la especialidad médica (INESPECIA). Devuelve por cada registro la especialidad, el profesional, la fecha de atención, el análisis clínico y el tipo de historia (Historia Ingreso, Historia Evolución, Historia Nota Rápida), permitiendo visualizar la línea de tiempo clínica del paciente durante su paso por urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_PacienteHistoriasMobile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las historias clínicas de un paciente en un ingreso específico (ingreso de urgencias, evoluciones y notas rápidas) para visualización en aplicación móvil.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir en al menos una de las tablas de historias clínicas; Los códigos de profesional y especialidad referenciados deben existir en INPROFSAL e INESPECIA para que aparezcan en el resultado (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las consultas filtran simultáneamente por paciente e ingreso; Cada fila del resultado se clasifica con un tipo de historia fijo: ''Historia Ingreso'', ''Historia Evolucion'' o ''Historia Nota Rapida''; Solo se incluyen historias cuyo profesional y especialidad estén catalogados (INNER JOIN con INPROFSAL e INESPECIA); Para historias de ingreso se usa FECHINIHI como fecha de historia, mientras que para evoluciones y notas rápidas se usa FECINIATE; El UNION ALL preserva duplicados sin deduplicar entre los tres orígenes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Historia clínica de urgencias; Evolución clínica; Nota rápida; Especialidad médica; Profesional de salud; Análisis clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCURGING1: Devuelve registros etiquetados como ''Historia Ingreso'' filtrando por paciente e ingreso; [RETURN_RESULT] HCURGEVO1: Devuelve registros etiquetados como ''Historia Evolucion'' filtrando por paciente e ingreso; [RETURN_RESULT] HCNOTEVO1: Devuelve registros etiquetados como ''Historia Nota Rapida'' filtrando por paciente e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCURGING1; dbo.HCURGEVO1; dbo.HCNOTEVO1; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteHistoriasMobile';
-- GO
