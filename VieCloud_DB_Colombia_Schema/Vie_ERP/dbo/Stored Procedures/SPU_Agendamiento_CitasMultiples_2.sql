CREATE PROCEDURE [dbo].[SPU_Agendamiento_CitasMultiples_2] @FechaInicial DATETIME, 
                                                          @FechaFinal   DATETIME
AS
     WITH CP
          AS (SELECT ROW_NUMBER() OVER(PARTITION BY CI.IPCODPACI, 
                                                    CI.CODESPECI
                     ORDER BY CI.IPCODPACI, 
                              ES.DESESPECI, 
                              CI.FECHORAIN) AS NumCitas, 
                     CI.CODAUTONU, 
                     ES.DESESPECI AS EspecialidadAgendada, 
                     CI.IPCODPACI, 
                     CI.CODPROSAL, 
                     PS.NOMMEDICO AS NombreMedico, 
                     CI.FECHORAIN, 
                     AM.DESACTMED AS ActividadAgendada,
                     CASE CI.CODESTCIT
                         WHEN 0
                         THEN 'Asignada'
                         WHEN 1
                         THEN 'Cumplida'
                         WHEN 2
                         THEN 'Incumplida'
                         WHEN 3
                         THEN 'PreAsignada'
                         WHEN 4
                         THEN 'Cancelada'
                     END AS EstadoAgendamiento, 
                     P.IPNOMCOMP AS NombrePaciente
              FROM DBO.AGASICITA AS CI
                   INNER JOIN DBO.INESPECIA AS ES ON CI.CODESPECI = ES.CODESPECI
                   INNER JOIN DBO.AGACTIMED AS AM ON CI.CODACTMED = AM.CODACTMED
                   INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL = CI.CODPROSAL
                   INNER JOIN dbo.INPACIENT AS P ON CI.IPCODPACI = P.IPCODPACI
              WHERE CI.FECHORAIN >= @FechaInicial
                    AND CI.FECHORAIN < @FechaFinal
                    AND CI.CODESTCIT <> 4)
          SELECT CP.*
          FROM CP
               INNER JOIN CP AS CP2 ON CP.IPCODPACI = CP2.IPCODPACI
                                       AND CP.EspecialidadAgendada = CP2.EspecialidadAgendada
                                       AND CP2.NumCitas = 2
          ORDER BY CP.IPCODPACI, 
                   CP.EspecialidadAgendada, 
                   CP.FECHORAIN;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que identifica pacientes con al menos dos citas agendadas en la misma especialidad dentro de un rango de fechas, excluyendo citas canceladas. Combina datos de citas, especialidades, actividades médicas, profesionales de salud y pacientes para retornar todas las citas (hasta la segunda y más) de dichos pacientes, indicando estado del agendamiento (Asignada, Cumplida, Incumplida, PreAsignada). Sirve para detectar múltiples agendamientos por especialidad en un período.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica pacientes con múltiples citas (al menos dos) en una misma especialidad dentro de un rango de fechas, listando todas sus citas activas con datos de médico, especialidad, actividad y estado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un rango de fechas válido (FechaInicial y FechaFinal).; Las citas deben tener correspondencia en los maestros de especialidad, actividad médica, profesional de la salud y paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se consideran citas canceladas (CODESTCIT=4).; Solo se reportan pacientes con 2 o más citas en la misma especialidad dentro del rango.; El filtro de fechas es semiabierto: incluye FechaInicial y excluye FechaFinal.; La numeración de citas por paciente/especialidad se ordena cronológicamente por fecha/hora de inicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Especialidad; Profesional de la salud / Médico; Actividad médica; Estado de agendamiento (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente las citas de pacientes que, en la misma especialidad y rango de fechas, tienen al menos dos citas (existe una fila con NumCitas=2 en la misma combinación paciente+especialidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT IN (0,1,2,3) → La cita se incluye y se traduce su estado a ''Asignada'', ''Cumplida'', ''Incumplida'' o ''PreAsignada'' respectivamente. else Si CODESTCIT=4 (Cancelada) la cita se excluye del análisis.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGASICITA; DBO.INESPECIA; DBO.AGACTIMED; dbo.INPROFSAL; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples_2';
-- GO
