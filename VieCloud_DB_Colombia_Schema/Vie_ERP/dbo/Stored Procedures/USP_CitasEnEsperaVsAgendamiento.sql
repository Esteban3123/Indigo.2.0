

CREATE PROCEDURE [dbo].[USP_CitasEnEsperaVsAgendamiento]
AS
	SELECT CE.IPCODPACI AS IdPaciente, PA.IPNOMCOMP AS  NombrePaciente, EN.NOMENTIDA AS EntidadPcte,
			CE.FECREGSIS AS FechaRegCitasEnEspera, CAST(CE.FECHACITA AS DATETIME) AS FechaDeseadaCitasEnEspera, 
			CI.FECHORAIN AS FechaAsignadaAgendamiento, ES.DESESPECI AS Especialidad, AM.DESACTMED AS ActividadCitasEnEspera, 
			AM2.DESACTMED AS ActividadAgendamiento, CE.CODPROSAL AS ProfesionalSolicitado, CI.CODPROSAL AS ProfesionalAsignado, 
			IIF(CE.ESTADO=1,'En espera', IIF(CE.ESTADO=2,'Asignada',IIF(CE.ESTADO=3, 'Cancelada', 'Otro'))) AS EstadoCitaEnEspera, 
			CE.OBSERVACI AS ObservacionesCitasEnEspera, 
			IIF(CI.CODESTCIT='0','Asignada', IIF(CI.CODESTCIT='1','Cumplida',IIF(CI.CODESTCIT='2', 'Incumplida', IIF(CI.CODESTCIT='3', 'PreAsignada', 
			IIF(CI.CODESTCIT='4', 'Cita Cancelada', 'Otro'))))) AS EstadoAgendamiento, CI.OBSCAUCAN AS ObservacionCancelacionAgendamiento, 
			US.NOMUSUARI AS UsuarioCitasEnEspera 
    FROM   AGCITAESP AS CE INNER JOIN SEGusuaru AS US ON CE.CODUSUASI = US.CODUSUARI INNER JOIN 
			INESPECIA AS ES ON CE.CODESPECI=ES.CODESPECI INNER JOIN 
			AGASICITA AS CI ON CE.IPCODPACI=CI.IPCODPACI AND CE.CODESPECI =CI.CODESPECI AND CI.FECREGSIS >=  CE.FECREGSIS INNER JOIN 
			AGACTIMED AS AM ON CE.CODACTMED=AM.CODACTMED INNER JOIN 
			AGACTIMED AS AM2 ON CI.CODACTMED=AM2.CODACTMED INNER JOIN 
			INPACIENT AS PA ON CE.IPCODPACI=PA.IPCODPACI INNER JOIN 
			INENTIDAD AS EN ON PA.CODENTIDA=EN.CODENTIDA 
	WHERE CE.ESTADO=1 
	ORDER BY CE.CODAUTONU
RETURN 0
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte comparativo entre las citas en lista de espera y las citas ya agendadas para los mismos pacientes. Cruza la lista de espera (AGCITAESP) con el agendamiento real (AGASICITA) por paciente y especialidad, mostrando para cada solicitud pendiente (estado ''En espera''): datos del paciente y su entidad/EPS, fecha en que solicitó la cita, fecha deseada, fecha en que fue finalmente asignada, especialidad, actividad médica solicitada vs. la asignada, profesional solicitado vs. asignado, estado de la cita en espera y estado del agendamiento resultante, junto con observaciones y el usuario que registró la solicitud. Permite a coordinadores de agendamiento y auditoría verificar si las citas en espera fueron atendidas oportunamente y con la especialidad y actividad correcta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Compara las solicitudes de citas en estado "En espera" contra las citas efectivamente agendadas posteriormente, mostrando datos del paciente, especialidad, profesionales y estados de ambos procesos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en AGCITAESP con ESTADO=1 (En espera); Para cada cita en espera debe existir un agendamiento en AGASICITA del mismo paciente y especialidad con FECREGSIS posterior o igual; Las tablas maestras (usuarios, especialidades, actividades médicas, pacientes, entidades) deben tener integridad referencial con los códigos usados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas en espera vigentes (ESTADO=1), excluyendo asignadas y canceladas en origen; El agendamiento emparejado siempre es posterior o simultáneo en registro a la solicitud en espera (FECREGSIS de agendamiento >= FECREGSIS de cita en espera); El emparejamiento entre solicitud y agendamiento se hace por paciente + especialidad; El procedimiento no modifica datos, solo retorna un conjunto de resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita en espera; Agendamiento de cita; Especialidad médica; Actividad médica; Profesional solicitado; Profesional asignado; Entidad del paciente; Estado de cita (En espera, Asignada, Cancelada, Cumplida, Incumplida, PreAsignada); Cancelación de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGCITAESP: Cuando AGCITAESP.ESTADO=1 y existe en AGASICITA un agendamiento del mismo paciente y especialidad con FECREGSIS >= FECREGSIS de la cita en espera, se retorna la fila comparativa ordenada por CODAUTONU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGCITAESP.ESTADO = 1 → Se etiqueta como ''En espera''; si AGCITAESP.ESTADO = 2 → Se etiqueta como ''Asignada''; si AGCITAESP.ESTADO = 3 → Se etiqueta como ''Cancelada''; si AGCITAESP.ESTADO no es 1, 2 ni 3 → Se etiqueta como ''Otro''; si AGASICITA.CODESTCIT = ''0'' → Se etiqueta el agendamiento como ''Asignada''; si AGASICITA.CODESTCIT = ''1'' → Se etiqueta el agendamiento como ''Cumplida''; si AGASICITA.CODESTCIT = ''2'' → Se etiqueta el agendamiento como ''Incumplida''; si AGASICITA.CODESTCIT = ''3'' → Se etiqueta el agendamiento como ''PreAsignada''; si AGASICITA.CODESTCIT = ''4'' → Se etiqueta el agendamiento como ''Cita Cancelada''; si AGASICITA.CODESTCIT no es ''0'',''1'',''2'',''3'' ni ''4'' → Se etiqueta como ''Otro''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGCITAESP; dbo.SEGusuaru; dbo.INESPECIA; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_CitasEnEsperaVsAgendamiento';
-- GO
