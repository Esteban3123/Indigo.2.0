

CREATE PROCEDURE [dbo].[SPU_Agendamiento_ProcedimientosMenores] 
    @FechaInicial date,
    @FechaFinal date,
	@Especialidad char(3)
AS
WITH AG
AS (
SELECT CI.CODAUTONU, CI.CODESPECI,ES.DESESPECI AS EspecialidadAgendada, CI.IPCODPACI, CI.CODPROSAL, 
		PS.NOMMEDICO AS NombreMedico,CI.FECHORAIN, AM.DESACTMED AS ActividadAgendada, 
		CASE CI.CODESTCIT
				when 0 then  'Asignada'  
				when 1 then  'Cumplida' 
				when 2 then  'Incumplida' 
				when 3 then  'PreAsignada'
				when 4 then  'Cancelada'
		END as EstadoAgendamiento
	FROM DBO.AGASICITA AS CI
		INNER JOIN DBO.INESPECIA AS ES ON CI.CODESPECI=ES.CODESPECI
		INNER JOIN DBO.AGACTIMED AS AM ON CI.CODACTMED=AM.CODACTMED
		INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL=CI.CODPROSAL
	WHERE CI.FECHORAIN >= @FechaInicial AND CI.FECHORAIN < @FechaFinal AND CI.CODESPECI=@Especialidad AND CI.CODESTCIT<>4
)
SELECT PM.CODCONCEC, PM.IDETIPHIS AS TipoHC, PM.NUMEFOLIO AS Folio, PM.IPCODPACI AS IdPaciente, PM.CODPROSAL AS CodProfesional, 
		PS.NOMMEDICO AS NombreProfesional, PM.FECREAPRO AS FechaPM, PM.CODSERIPS, CUPS.DESSERIPS, PM.DESHALLAZ AS DescripcionProcedimientoMenor, 
		CUPS.DESCODCUPS, CUPS.CODGOCUPS, ES.DESESPECI AS EspecialidadTratante
	FROM HCINFPROM AS PM INNER JOIN AG ON PM.IPCODPACI=AG.IPCODPACI AND CAST(AG.FECHORAIN AS date)<=CAST(PM.FECREAPRO AS date)
		INNER JOIN INCUPSIPS AS CUPS ON PM.CODSERIPS=CUPS.CODSERIPS
		INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL=PM.CODPROSAL
		INNER JOIN DBO.INESPECIA AS ES ON PS.CODESPEC1=ES.CODESPECI;
RETURN 0
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de procedimientos menores realizados en consulta, filtrado por rango de fechas y especialidad médica. Combina las citas agendadas (excluyendo canceladas) con los hallazgos y procedimientos documentados en la historia clínica del paciente (HCINFPROM), cruzando por código de paciente y fecha para relacionar la cita con la atención efectiva. Para cada procedimiento menor registrado, muestra el código CUPS del servicio, la descripción del hallazgo o intervención, el profesional tratante, la especialidad y el folio de la historia clínica. Sirve para auditoría clínica, seguimiento de productividad por especialidad y verificación de procedimientos menores ejecutados versus agendados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los procedimientos menores realizados a pacientes que tuvieron citas agendadas (no canceladas) en una especialidad y rango de fechas dados, cruzando agenda con la historia clínica de procedimientos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas y el código de especialidad deben ser válidos y existir en INESPECIA.; Las citas deben tener profesional registrado en INPROFSAL y actividad médica en AGACTIMED.; Los procedimientos menores deben tener un código de servicio existente en INCUPSIPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye citas con estado Cancelada (CODESTCIT=4).; Solo cruza procedimientos menores cuya fecha de realización sea igual o posterior a la fecha-hora de la cita agendada del mismo paciente.; El filtro de fecha es semiabierto: incluye FechaInicial y excluye FechaFinal.; La especialidad reportada como ''Tratante'' corresponde a la especialidad principal (CODESPEC1) del profesional que realizó el procedimiento, no necesariamente a la especialidad de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agendamiento de citas; Especialidad médica; Profesional de salud; Procedimiento menor; Historia clínica; Códigos CUPS; Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Actividad médica; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve los procedimientos menores (HCINFPROM) cuya fecha de realización es igual o posterior a la fecha de la cita agendada del mismo paciente, filtrando por especialidad y rango de fechas y excluyendo citas canceladas (CODESTCIT<>4).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT in (0,1,2,3) → Traduce el código numérico a etiqueta textual (Asignada, Cumplida, Incumplida, PreAsignada) else El valor 4 (Cancelada) queda excluido por el WHERE, por lo que nunca aparece en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INESPECIA; dbo.AGACTIMED; dbo.INPROFSAL; dbo.HCINFPROM; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_ProcedimientosMenores';
-- GO
