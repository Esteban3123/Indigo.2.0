

--DROP VIEW [dbo].[vwTrazabilidadCitasEnEspera]

CREATE VIEW [dbo].[vwTrazabilidadCitasEnEspera]
AS
SELECT MONTH(CE.FECREGSIS) AS Mes, CONCAT(DATENAME(MONTH,CE.FECREGSIS),'/',YEAR(CE.FECREGSIS)) AS MesDeRegistro, CONVERT(smalldatetime,CE.FECREGSIS,103) AS FechaRegistro,
                    PA.IPNOMCOMP AS Paciente, CE.IPCODPACI AS Identificacion, EN.NOMENTIDA AS Entidad, 
                    ES.DESESPECI AS Especialidad, AGAC.DESACTMED AS Actividad, 
					CASE WHEN (TRY_CAST(REPLACE(REPLACE((SUBSTRING(CE.OBSERVACI, CHARINDEX(CHAR(10), CE.OBSERVACI), 12)),CHAR(10),''),CHAR(13),'') AS datetime)) IS NULL THEN NULL
						WHEN TRY_CAST(REPLACE(REPLACE((SUBSTRING(CE.OBSERVACI, CHARINDEX(CHAR(10), CE.OBSERVACI), 12)),CHAR(10),''),CHAR(13),'') AS datetime)<='17530101' THEN NULL
						WHEN TRY_CAST(REPLACE(REPLACE((SUBSTRING(CE.OBSERVACI, CHARINDEX(CHAR(10), CE.OBSERVACI), 12)),CHAR(10),''),CHAR(13),'') AS datetime)>='99991231' THEN NULL
						ELSE TRY_CAST(REPLACE(REPLACE((SUBSTRING(CE.OBSERVACI, CHARINDEX(CHAR(10), CE.OBSERVACI), 12)),CHAR(10),''),CHAR(13),'') AS datetime)
					END AS FechaVenceAutorizacion,
					CASE CE.ESTADO WHEN 1 THEN 'En Espera' WHEN 2 THEN 'Asignada' WHEN 3 THEN 'Cancelada' END AS EstadoCitaEnEspera,
					CASE WHEN CE.ESTADO=1 THEN DATEDIFF(DAY,CE.FECREGSIS,SYSDATETIME()) ELSE NULL END AS DiasEnEspera,
					RTrim(US.NOMUSUARI) AS Usuario, CE.IDCITA, CAST(EX.IPFECHCIT AS DATE) AS FechaCita, DATENAME(MONTH,EX.IPFECHCIT) AS MesCita,
					RTrim(US3.NOMUSUARI) AS UsuarioAgenda, DATENAME(MONTH,AGA.FECHORAIN) + '/' + CAST(YEAR(AGA.FECHORAIN) AS VARCHAR(4)) AS MesAgenda ,
					CASE WHEN CE.CODESPECI=EX.CODESPECI THEN 'SI' ELSE 'NO' END AS IgualEspecialidad, 
					DATEDIFF(DAY,CE.FECREGSIS,EX.IPFECHCIT) AS OportunidadDias,
					RTrim(US2.NOMUSUARI) AS UsuarioCancela, CE.FECHCANCELA AS FechaCancela, CC.DESCAUCAN, CE.OBSCAUCAN AS CausaCancelaCitaEspera
FROM   AGCITAESP AS CE INNER JOIN 
                    INPACIENT AS PA ON CE.IPCODPACI = PA.IPCODPACI INNER JOIN
					SEGusuaru AS US ON CE.CODUSUASI=US.CODUSUARI INNER JOIN 
                    INESPECIA AS ES ON CE.CODESPECI = ES.CODESPECI INNER JOIN 
                    INENTIDAD AS EN ON PA.CODENTIDA = EN.CODENTIDA INNER JOIN 
                    AGACTIMED AS AGAC ON CE.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN 
                    SEGusuaru AS US2 ON CE.CANCELUSU=US2.CODUSUARI LEFT OUTER JOIN
					ADCONCOED AS ED ON CE.IDCITA=ED.NUMCONCIT LEFT OUTER JOIN
					ADCONCOEX AS EX ON ED.CODCONCEC=EX.CODCONCEC LEFT OUTER JOIN 
					AGCAUCACE AS CC ON CE.CODCAUCAN=CC.CODCAUCAN LEFT OUTER JOIN
					AGASICITA AS AGA ON CE.IDCITA=AGA.CODAUTONU LEFT OUTER JOIN
					SEGusuaru AS US3 ON AGA.CODUSUASI=US3.CODUSUARI 
                WHERE CE.FECREGSIS>='20170501';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad completa del ciclo de vida de las citas en lista de espera desde mayo de 2017. Integra datos del paciente (cédula, nombre), su entidad aseguradora (EPS/pagador), la especialidad médica solicitada y la actividad agendable, junto con el estado actual de la cita en espera (En Espera, Asignada o Cancelada), los días que lleva esperando, la fecha de vencimiento de autorización extraída del campo de observaciones, y la causa y usuario de cancelación cuando aplique. Adicionalmente cruza con el agendamiento efectivo para mostrar la fecha real de cita asignada, el usuario que la agendó, el mes de agenda y la oportunidad en días entre el registro en espera y la cita obtenida, permitiendo medir tiempos de respuesta y cumplimiento de oportunidad de atención. Sirve como insumo para reportes operativos y de auditoría sobre gestión de listas de espera, seguimiento de autorizaciones y desempeño del proceso de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwTrazabilidadCitasEnEspera';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwTrazabilidadCitasEnEspera';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de trazabilidad de citas médicas en espera que consolida información del paciente, entidad, especialidad, estado, oportunidad y causas de cancelación, junto con la cita asignada posterior.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha de registro de la cita en espera debe ser mayor o igual a 2017-05-01 para ser incluida.; El paciente, usuario asignador, especialidad, entidad y actividad médica deben existir referenciados en sus tablas maestras (joins INNER).; La fecha de vencimiento de autorización se espera embebida en el campo de observaciones, después de un salto de línea, en formato datetime parseable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los estados válidos de cita en espera son 1=En Espera, 2=Asignada, 3=Cancelada; otros estados producen NULL en la descripción.; DiasEnEspera solo se computa para citas en estado ''En Espera''.; FechaVenceAutorizacion solo es válida si está dentro del rango (1753-01-01, 9999-12-31).; OportunidadDias mide los días entre el registro de la solicitud en espera y la fecha de la cita efectivamente asignada.; Solo se incluyen registros desde 2017-05-01 en adelante.; La relación con la cita asignada se obtiene vía ADCONCOED→ADCONCOEX y AGASICITA, pudiendo ser nula si la cita aún no ha sido asignada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita en espera; Paciente; Entidad (aseguradora/EPS); Especialidad médica; Actividad médica; Autorización (fecha de vencimiento); Estado de cita (En Espera/Asignada/Cancelada); Oportunidad de cita (días); Causa de cancelación; Asignación de cita; Trazabilidad de usuarios (asigna/cancela/agenda)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna conjunto de citas en espera registradas desde 2017-05-01 con su trazabilidad completa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Texto extraído de OBSERVACI (tras CHAR(10), 12 caracteres) no parsea a datetime, o es <= 1753-01-01, o >= 9999-12-31 → FechaVenceAutorizacion se reporta como NULL else Se usa el valor parseado como FechaVenceAutorizacion; si ESTADO = 1 → EstadoCitaEnEspera = ''En Espera'' y se calcula DiasEnEspera = DATEDIFF(DAY, FECREGSIS, hoy) else DiasEnEspera = NULL; si ESTADO = 2 → EstadoCitaEnEspera = ''Asignada''; si ESTADO = 3 → EstadoCitaEnEspera = ''Cancelada''; si CE.CODESPECI = EX.CODESPECI → IgualEspecialidad = ''SI'' (la cita asignada conserva la especialidad solicitada) else IgualEspecialidad = ''NO''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGCITAESP; dbo.INPACIENT; dbo.SEGusuaru; dbo.INESPECIA; dbo.INENTIDAD; dbo.AGACTIMED; dbo.ADCONCOED; dbo.ADCONCOEX; dbo.AGCAUCACE; dbo.AGASICITA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwTrazabilidadCitasEnEspera';
GO
