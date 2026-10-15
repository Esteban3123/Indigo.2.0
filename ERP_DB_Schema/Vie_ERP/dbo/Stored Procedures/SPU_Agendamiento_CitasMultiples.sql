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