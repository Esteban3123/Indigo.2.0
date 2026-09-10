CREATE PROCEDURE [dbo].[SP_AGE_ListarPacientesCirugiasProgramadas]
(
	@FechaInicial datetime,
	@FechaFinal datetime,
	@Grupo varchar(max),
	@Identificacion varchar(25)
)
AS
BEGIN
	
	SET NOCOUNT ON;

	if @Identificacion = '' begin

       SELECT   
	   		RTRIM(A.IPCODPACI) AS IPCODPACI,
			concat(RTRIM(A.IPCODPACI),' - ',RTRIM(C.IPNOMCOMP)) AS 'PACIENTE',
			A.CODAUTONU AS 'IDQX',
			B.CODCONCEC AS 'IDSALA',
			RTRIM(B.DESCRIPSAL) AS 'QUIROFANO',
			A.FECHORAIN AS 'FECHA'
		FROM AGEPROGQX A with(nolock)
			INNER JOIN AGENSALAC B with(nolock) ON A.AGENSALAC = B.CODCONCEC 
			INNER JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON A.CODSERIPS = GRUPO_D.CODSERIPS AND ISNULL(GRUPO_D.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
			INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
		WHERE
			(A.FECHORAIN BETWEEN @FechaInicial AND @FechaFinal) 
			AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo)) AND A.CODESTPQX <> 6
		order by A.FECHORAIN desc

	end else begin
		
		 SELECT   
	   		RTRIM(A.IPCODPACI) AS IPCODPACI,
			concat(RTRIM(A.IPCODPACI),' - ',RTRIM(C.IPNOMCOMP)) AS 'PACIENTE',
			A.CODAUTONU AS 'IDQX',
			B.CODCONCEC AS 'IDSALA',
			RTRIM(B.DESCRIPSAL) AS 'QUIROFANO',
			A.FECHORAIN AS 'FECHA'
		FROM AGEPROGQX A with(nolock)
			INNER JOIN AGENSALAC B with(nolock) ON A.AGENSALAC = B.CODCONCEC 
			INNER JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON A.CODSERIPS = GRUPO_D.CODSERIPS AND ISNULL(GRUPO_D.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
			INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
		WHERE
			GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo)) 
			AND A.IPCODPACI = @Identificacion AND A.CODESTPQX <> 6
		order by A.FECHORAIN desc

	end

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con cirugías o procedimientos quirúrgicos programados, filtrando por rango de fechas y grupo de procedimientos (CUPS). Permite opcionalmente filtrar por la cédula o identificación del paciente; si no se indica, devuelve todos los pacientes con cirugías vigentes en el período. Compone información de la programación quirúrgica (AGEPROGQX), el quirófano o sala asignada (AGENSALAC), los datos del paciente (INPACIENT) y la clasificación del procedimiento por grupo clínico (HCGRUPINVC/HCGRUPINVD), excluyendo las cirugías canceladas o inactivadas. Se usa para visualizar en pantalla la agenda quirúrgica por sala, grupo de procedimientos y paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cirugías programadas (no canceladas) filtradas por grupo clínico, con datos de paciente y quirófano, ya sea por rango de fechas o por identificación específica del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El listado de grupos clínicos debe venir como cadena delimitada parseable por dbo.SplitString.; Si se entrega identificación de paciente vacía, deben venir fechas válidas para acotar el rango.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye cirugías con CODESTPQX = 6 (estado excluido, típicamente canceladas/inactivadas).; Solo retorna cirugías cuyo grupo clínico esté en la lista provista.; El emparejamiento entre programación y descripción de grupo respeta IDDESCRIPCIONRELACIONADA tratando NULL como 0.; Resultados siempre ordenados por fecha/hora de inicio descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cirugía programada; Quirófano/Sala; Grupo de procedimientos clínicos; Estado de cirugía; Agenda quirúrgica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGEPROGQX: Cuando la identificación es vacía, retorna cirugías cuyo FECHORAIN esté entre las fechas dadas, pertenezcan a alguno de los grupos clínicos indicados y CODESTPQX <> 6, ordenadas por FECHORAIN desc.; [RETURN_RESULT] AGEPROGQX: Cuando la identificación no es vacía, retorna cirugías del paciente indicado dentro de los grupos clínicos solicitados y con CODESTPQX <> 6, ordenadas por FECHORAIN desc (sin filtro de fechas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Identificación de paciente vacía → Filtra por rango de fechas (FECHORAIN entre inicial y final) sin restringir paciente. else Ignora el rango de fechas y filtra exclusivamente por la identificación del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.HCGRUPINVD; dbo.HCGRUPINVC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacientesCirugiasProgramadas';
-- GO
