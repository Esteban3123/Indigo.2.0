CREATE PROCEDURE [dbo].[SPU_Agendamiento_CitasMultiples] @FechaInicial DATE, 
                                                        @FechaFinal   DATE, 
                                                        @Especialidad CHAR(3)
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
                     END AS EstadoAgendamiento
              FROM DBO.AGASICITA AS CI
                   INNER JOIN DBO.INESPECIA AS ES ON CI.CODESPECI = ES.CODESPECI
                   INNER JOIN DBO.AGACTIMED AS AM ON CI.CODACTMED = AM.CODACTMED
                   INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL = CI.CODPROSAL
              WHERE CI.FECHORAIN >= @FechaInicial
                    AND CI.FECHORAIN < @FechaFinal
                    AND CI.CODESTCIT <> 4
                    AND CI.CODESPECI = @Especialidad)
          SELECT CP.*
          FROM CP
               INNER JOIN CP AS CP2 ON CP.IPCODPACI = CP2.IPCODPACI
                                       AND CP.EspecialidadAgendada = CP2.EspecialidadAgendada
                                       AND CP2.NumCitas = 2
          ORDER BY CP.IPCODPACI, 
                   CP.EspecialidadAgendada, 
                   CP.FECHORAIN;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta pacientes que tienen dos o más citas agendadas en la misma especialidad dentro de un rango de fechas dado, identificando posibles duplicaciones o controles de citas múltiples. Filtra las citas activas (excluye canceladas) de la tabla de agendamiento AGASICITA, cruza con el catálogo de especialidades INESPECIA, las actividades médicas AGACTIMED y el maestro de profesionales INPROFSAL para enriquecer el resultado con el nombre del médico, la especialidad, la actividad agendada y el estado de la cita. Recibe como parámetros la fecha inicial, fecha final y el código de especialidad a analizar. Es útil para auditoría de agendamiento, detección de citas duplicadas y control de sobre-utilización de servicios por paciente en una especialidad específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_CitasMultiples';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica pacientes con citas múltiples (dos o más) en una misma especialidad dentro de un rango de fechas, listando todas sus citas no canceladas con datos del médico, actividad y estado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe estar definido (FechaInicial <= FechaFinal); Debe existir el código de especialidad consultado en INESPECIA; Las citas deben tener profesional, especialidad y actividad médica relacionados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye citas canceladas (CODESTCIT = 4); Solo considera citas dentro del rango [FechaInicial, FechaFinal); Filtra exclusivamente por la especialidad recibida; La numeración de citas se hace por paciente y especialidad ordenada cronológicamente; Solo se reportan pacientes con multiplicidad de citas en la misma especialidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Especialidad; Profesional de la salud / Médico; Actividad médica; Estado de agendamiento (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Paciente; Autorización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente las citas de pacientes que tengan al menos 2 citas en la misma especialidad dentro del rango (filtro CP2.NumCitas = 2)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT = 0/1/2/3/4 → Traduce el código a etiqueta legible: Asignada, Cumplida, Incumplida, PreAsignada o Cancelada; si CI.CODESTCIT = 4 (Cancelada) → Excluye la cita del resultado else Incluye la cita en la numeración y resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGASICITA; DBO.INESPECIA; DBO.AGACTIMED; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_CitasMultiples';
-- GO
