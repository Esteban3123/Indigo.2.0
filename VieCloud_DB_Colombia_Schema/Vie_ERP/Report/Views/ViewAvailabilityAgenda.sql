
CREATE VIEW [Report].[ViewAvailabilityAgenda]
AS
     WITH CTE_AGENDA
          AS (SELECT CAST(DB_NAME() AS VARCHAR(9)) AS [ID_COMPANY], 
                     MAX(A.CODAUTONU) CODAUTONU, 
                     C.NOMCENATE 'CENTRO DE ATENCION', 
                     CON.DESCRICON AS 'CONSULTORIO', 
                     B.DESESPECI 'ESPECIALIDAD', 
                     E.NOMMEDICO 'PROFESIONAL', 
                     CONVERT(VARCHAR, MIN(FECHORAIN), 23) 'FECHA INICIAL DE AGENDA', 
                     CONVERT(VARCHAR, MIN(FECHORAIN), 20) 'FECHA HORA INICIAL DE AGENDA', 
                     CONVERT(VARCHAR, MAX(FECHORAFI), 23) 'FECHA FINAL DE AGENDA', 
                     CONVERT(VARCHAR, MAX(FECHORAFI), 20) 'FECHA HORA FINAL DE AGENDA', 
                     SUM(DATEDIFF(HOUR, A.FECHORAIN, A.FECHORAFI)) AS 'HORAS DIA', 
                     SUM(ISNULL(GB.HORAS, 0)) 'HORAS BLOQUEO', 
                     SUM(ISNULL(G.CITAS, 0)) 'NRO DE CITAS DIAS', 
                     SUM(ISNULL(GC.CITASCUMPLIDAS, 0)) 'CITAS CUMPLIDAS', 
                     SUM(ISNULL(GI.CITASINCUMPLIDAS, 0)) 'CITAS INCUMPLIDAS', 
                     CAST(A.FECHORAIN AS DATE) 'FECHA BUSQUEDA'
              FROM AGAGEMEDC AS A
                   JOIN INPROFSAL AS E WITH(NOLOCK) ON A.CODPROSAL = E.CODPROSAL
                   JOIN INESPECIA AS B WITH(NOLOCK) ON A.CODESPECI = B.CODESPECI
                   JOIN ADCENATEN AS C WITH(NOLOCK) ON A.CODCENATE = C.CODCENATE
                   JOIN AGCONSULT AS CON WITH(NOLOCK) ON A.CODIGOCON = CON.CODIGOCON
                                                         AND C.CODCENATE = CON.CODCENATE
                   LEFT JOIN
              (
                  SELECT IDAGENDA, 
                         SUM(DATEDIFF(HOUR, FECHAINIBLOQUEO, FECHAFINBLOQUEO)) 'HORAS'
                  FROM AGBLOQUEOPARCIAL
                  WHERE ESTADO = 1
                  GROUP BY IDAGENDA
              ) AS GB ON GB.IDAGENDA = A.CODAUTONU
                   LEFT JOIN
              (
                  SELECT COUNT(IDAGENDA) AS CITAS, 
                         IDAGENDA
                  FROM AGASICITA WITH(NOLOCK)
                  WHERE TIPSOLICITU = 1
                        AND CODESTCIT <> 4
                  GROUP BY IDAGENDA
              ) AS G ON G.IDAGENDA = A.CODAUTONU
                   LEFT JOIN
              (
                  SELECT COUNT(IDAGENDA) AS CITASASIGNADAS, 
                         IDAGENDA
                  FROM AGASICITA WITH(NOLOCK)
                  WHERE TIPSOLICITU = 1
                        AND CODESTCIT = 0
                  GROUP BY IDAGENDA
              ) AS GA ON GA.IDAGENDA = A.CODAUTONU
                   LEFT JOIN
              (
                  SELECT COUNT(IDAGENDA) AS CITASCUMPLIDAS, 
                         IDAGENDA
                  FROM AGASICITA WITH(NOLOCK)
                  WHERE TIPSOLICITU = 1
                        AND CODESTCIT = 1
                  GROUP BY IDAGENDA
              ) AS GC ON GC.IDAGENDA = A.CODAUTONU
                   LEFT JOIN
              (
                  SELECT COUNT(IDAGENDA) AS CITASINCUMPLIDAS, 
                         IDAGENDA
                  FROM AGASICITA WITH(NOLOCK)
                  WHERE TIPSOLICITU = 1
                        AND CODESTCIT = 2
                  GROUP BY IDAGENDA
              ) AS GI ON GI.IDAGENDA = A.CODAUTONU
              GROUP BY C.NOMCENATE, 
                       CON.DESCRICON, 
                       B.DESESPECI, 
                       E.NOMMEDICO, 
                       YEAR(FECHORAIN), 
                       MONTH(FECHORAIN), 
                       DAY(FECHORAIN), 
                       YEAR(FECHORAFI), 
                       MONTH(FECHORAFI), 
                       DAY(FECHORAFI), 
                       CAST(A.FECHORAIN AS DATE))
          SELECT *,
		         YEAR([FECHA BUSQUEDA]) AS 'AÑO FECHA BUSQUEDA', 
				 MONTH([FECHA BUSQUEDA]) AS 'MES AÑO FECHA BUSQUEDA',
				 CASE MONTH([FECHA BUSQUEDA]) 
				        WHEN 1 THEN 'ENERO'
						WHEN 2 THEN 'FEBRERO'
						WHEN 3 THEN 'MARZO'
						WHEN 4 THEN 'ABRIL'
						WHEN 5 THEN 'MAYO'
						WHEN 6 THEN 'JUNIO'
						WHEN 7 THEN 'JULIO'
						WHEN 8 THEN 'AGOSTO'
						WHEN 9 THEN 'SEPTIEMBRE'
						WHEN 10 THEN 'OCTUBRE'
						WHEN 11 THEN 'NOVIEMBRE'
						WHEN 12 THEN 'DICIEMBRE'
						END AS 'MES NOMBRE FECHA BUSQUEDA', 
				  DAY([FECHA BUSQUEDA]) AS 'DIA FECHA BUSQUEDA',
		         CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
          FROM CTE_AGENDA;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida la disponibilidad diaria de agendas médicas por centro de atención, consultorio, especialidad y profesional. Agrega horas totales de agenda, horas bloqueadas por bloqueos parciales activos, y conteo de citas según estado (cumplidas, incumplidas y totales excluyendo canceladas). Expone dimensiones de fecha descompuestas en año, mes y día —con nombre de mes en español— para facilitar el consumo en herramientas de BI o reportería de ocupación y cumplimiento de agendas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la disponibilidad diaria de agendas médicas por centro, consultorio, especialidad y profesional, totalizando horas programadas, horas bloqueadas y conteos de citas asignadas/cumplidas/incumplidas para reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas AGAGEMEDC, INPROFSAL, INESPECIA, ADCENATEN y AGCONSULT deben tener integridad referencial sobre CODPROSAL, CODESPECI, CODCENATE y CODIGOCON.; Los códigos de estado de cita (CODESTCIT) y de tipo de solicitud (TIPSOLICITU) deben respetar el dominio: TIPSOLICITU=1 representa la solicitud relevante; CODESTCIT 0=asignada, 1=cumplida, 2=incumplida, 4=excluida del total.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se materializa como el nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; ULT_ACTUAL se calcula como GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; Las horas de la agenda y de bloqueo se miden en horas enteras vía DATEDIFF(HOUR,...).; Las citas excluidas del total general son las que tienen CODESTCIT = 4.; Solo se consideran citas con TIPSOLICITU = 1 en todos los conteos.; Las uniones a métricas (bloqueos, citas) se hacen por IDAGENDA = CODAUTONU, vinculando agenda madre con sus eventos.; El JOIN AGCONSULT exige coincidencia simultánea de CODIGOCON y CODCENATE, garantizando que el consultorio pertenezca al centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Centro de atención; Consultorio; Especialidad; Profesional de la salud; Citas asignadas; Citas cumplidas; Citas incumplidas; Bloqueo parcial de agenda; Disponibilidad horaria', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewAvailabilityAgenda: Devuelve una fila por combinación de centro, consultorio, especialidad, profesional y fecha (CAST(FECHORAIN AS DATE)) con métricas agregadas de la agenda.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGBLOQUEOPARCIAL.ESTADO = 1 → Se computan las horas de bloqueo parcial (DATEDIFF HOUR entre FECHAINIBLOQUEO y FECHAFINBLOQUEO) y se suman en ''HORAS BLOQUEO''. else Bloqueos con otro estado no se contabilizan.; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT <> 4 → Se cuentan como ''NRO DE CITAS DIAS''.; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT = 1 → Se cuentan como ''CITAS CUMPLIDAS''.; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT = 2 → Se cuentan como ''CITAS INCUMPLIDAS''.; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT = 0 → Se calcula subconsulta de ''CITASASIGNADAS'' (aunque no se proyecta en el SELECT final).; si MONTH([FECHA BUSQUEDA]) entre 1 y 12 → Se traduce el número del mes a su nombre en español (''ENERO''…''DICIEMBRE'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCENATEN; dbo.AGCONSULT; dbo.AGBLOQUEOPARCIAL; dbo.AGASICITA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAvailabilityAgenda';
GO
