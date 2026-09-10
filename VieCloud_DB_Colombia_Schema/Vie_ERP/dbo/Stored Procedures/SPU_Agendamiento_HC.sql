

CREATE PROCEDURE [dbo].[SPU_Agendamiento_HC] 
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
SELECT HC.IDETIPHIS AS TipoHC, HC.NUMEFOLIO AS Folio, HC.IPCODPACI AS IdPaciente, HC.CODPROSAL AS CodProfesional, 
		PS.NOMMEDICO AS NombreMedicoTratante, HC.FECHISPAC AS FechaHC, HC.ESTAFOLIO AS EstadoFolio, 
		HC.CODESPTRA AS CodigoEspecialidad, ES.DESESPECI AS EspecialidadTratante
	FROM HCHISPACA AS HC INNER JOIN AG ON AG.IPCODPACI=HC.IPCODPACI AND CAST(AG.FECHORAIN AS date)=CAST(HC.FECHISPAC AS date)
		INNER JOIN DBO.INESPECIA AS ES ON HC.CODESPTRA=ES.CODESPECI
		INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL=HC.CODPROSAL;
RETURN 0
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que cruza las citas agendadas de un rango de fechas y una especialidad médica específica con las historias clínicas generadas el mismo día para cada paciente. Toma como parámetros una fecha inicial, una fecha final y un código de especialidad, filtrando citas no canceladas. Combina los datos de agendamiento (AGASICITA) con el catálogo de especialidades (INESPECIA), las actividades médicas (AGACTIMED) y el maestro de profesionales (INPROFSAL), para luego unirlos con las notas clínicas (HCHISPACA) correspondientes. Se utiliza para auditoría y seguimiento clínico-administrativo: permite verificar qué pacientes que tenían cita en una especialidad efectivamente tienen historia clínica registrada ese día, identificando cumplimiento de la atención y trazabilidad entre el agendamiento y la consulta documentada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_HC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_HC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Cruza las citas agendadas en una especialidad y rango de fechas con las historias clínicas registradas el mismo día y paciente, para verificar la atención efectiva.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el código de especialidad en INESPECIA; Las citas deben tener profesional, especialidad y actividad médica válidos en sus catálogos; Las historias clínicas deben tener especialidad tratante y profesional válidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se incluyen citas canceladas en el resultado; El emparejamiento cita-HC exige mismo paciente y misma fecha calendario (ignora hora); El rango de fechas es semiabierto: incluye FechaInicial y excluye FechaFinal; El filtro de especialidad aplica sobre la cita, no sobre la HC (la HC puede tener especialidad tratante distinta)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Agendamiento; Especialidad; Profesional de salud / Médico tratante; Actividad médica; Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Historia clínica; Folio de historia clínica; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT: Devuelve sólo registros de HCHISPACA cuyo paciente y fecha (a nivel día) coinciden con una cita no cancelada (CODESTCIT<>4) en la especialidad y rango solicitados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT = 4 (Cancelada) → La cita se excluye del cruce con historias clínicas else La cita se incluye traduciendo el estado a etiqueta (Asignada/Cumplida/Incumplida/PreAsignada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGASICITA; DBO.INESPECIA; DBO.AGACTIMED; dbo.INPROFSAL; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_HC';
-- GO
