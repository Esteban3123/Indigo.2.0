/*******************************************************************************************************************
Nombre: [Report].[UploadCubeVieClinicalSchedulingSMSAppointments]
Tipo:Vista
Profesional:Ingeniero Andres Cabrera
Fecha:03-07-2024
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico:Amira Gil Meneses
Fecha:03-07-2024
Observaciones: Se ingresa el campo de Tipo de Cita a solicitud del Cliente.
--------------------------------------
Version 2
Persona que modifico:
Observación:
Fecha:
***********************************************************************************************************************************/

--CREATE PROCEDURE  [dbo].[ODO_Citas_Mensaje_de_Texto]

--DECLARE @ini_date DATETIME='2024-06-01';
--DECLARE @end_date DATETIME='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalSchedulingSMSAppointments] as 

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		TRIM(pac.ipprinomb) + ' ' + TRIM(pac.ipsegnomb) AS [Name]
		,TRIM(pac.ippriapel) + ' ' + TRIM(pac.ipsegapel) AS [LastName]
		,pac.iptelmovi AS [MobileNumber]
		,'' AS [Message]
		,UPPER(DATENAME(DW, cit.fechorain) + ' ' + CAST(DAY(cit.fechorain) AS CHAR(2)) + ' de ' + LOWER(DATENAME(MM, cit.fechorain)) + 
		CASE DATEPART(HH, cit.fechorain) 
			WHEN 7 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 7am' ELSE CONCAT(' a las 7:', DATEPART(MINUTE, cit.fechorain), 'am') END
			WHEN 8 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 8am' ELSE CONCAT(' a las 8:', DATEPART(MINUTE, cit.fechorain), 'am') END
			WHEN 9 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 9am' ELSE CONCAT(' a las 9:', DATEPART(MINUTE, cit.fechorain), 'am') END
			WHEN 10 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 10am' ELSE CONCAT(' a las 10:', DATEPART(MINUTE, cit.fechorain), 'am') END
			WHEN 11 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 11am' ELSE CONCAT(' a las 11:', DATEPART(MINUTE, cit.fechorain), 'am') END
			WHEN 12 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 12pm' ELSE CONCAT(' a las 12:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 13 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a la 1pm' ELSE CONCAT(' a la 1:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 14 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 2pm' ELSE CONCAT(' a las 2:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 15 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 3pm' ELSE CONCAT(' a las 3:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 16 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 4pm' ELSE CONCAT(' a las 4:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 17 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 5pm' ELSE CONCAT(' a las 5:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 18 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 6pm' ELSE CONCAT(' a las 6:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 19 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 7pm' ELSE CONCAT(' a las 7:', DATEPART(MINUTE, cit.fechorain), 'pm') END
			WHEN 20 THEN CASE DATEPART(MINUTE, cit.fechorain) WHEN 0 THEN ' a las 8pm' ELSE CONCAT(' a las 8:', DATEPART(MINUTE, cit.fechorain), 'pm') END END) AS [Custom1]
		,TRIM(cat.nomcenate) + ' ' + mun.munnombre  AS Custom2
		,CASE per.gender WHEN 1 THEN 'DOCTOR ' + TRIM(med.nommedico) WHEN 2 THEN 'DOCTORA ' + TRIM(med.nommedico) END AS Custom3
		,'' AS Custom4,
		CASE cit.TIPSOLICITU WHEN '1' THEN 'Cita Medica'
		                     WHEN '2' THEN 'Cita Apoyo Diagnostico'
							 WHEN '3' THEN 'Cita Tratamiento Especiales'
							 END AS 'Tipo de Cita',
	    CAST(cit.fechorain AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.agasicita AS cit
	INNER JOIN dbo.inpacient AS pac ON cit.ipcodpaci = pac.ipcodpaci
	INNER JOIN dbo.adcenaten AS cat ON cit.codcenate = cat.codcenate 
	INNER JOIN dbo.inmunicip AS mun ON cat.depmuncod = mun.depmuncod
	INNER JOIN dbo.inprofsal AS med ON cit.codprosal = med.codprosal
	INNER JOIN security.[userINT] AS usr ON med.codusuari = usr.usercode 
	INNER JOIN security.[personINT] AS per ON usr.idperson  = per.id
	WHERE cit.ipcodpaci NOT IN ('00000000', '0000', '0000000') AND cit.tipsolicitu = 1 AND cit.codestcit = 0
	--AND CAST(cit.fechorain AS DATE)>='2023-01-01'
	--CAST(cit.fechorain AS DATE) BETWEEN @ini_date AND @end_date

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado de pacientes con citas médicas activas y sus datos de contacto móvil para el envío de mensajes SMS recordatorios, formateando fecha/hora en lenguaje natural en español.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben tener un paciente válido (ipcodpaci distinto de ''00000000'', ''0000'', ''0000000'').; El tipo de solicitud de la cita debe ser ''1'' (cita médica).; El estado de la cita debe ser 0 (codestcit = 0, presumiblemente programada/activa).; El paciente, centro de atención, municipio, profesional de salud, usuario y persona deben existir en sus tablas para satisfacer los INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas médicas (tipsolicitu=1) en estado 0.; Se excluyen pacientes con identificadores genéricos/comodín (''00000000'',''0000'',''0000000'').; El identificador de compañía expuesto corresponde al nombre de la base de datos actual truncado a 9 caracteres.; La marca de última actualización se calcula convirtiendo la hora actual a la zona ''Pakistan Standard Time''.; El formato horario solo cubre el rango 07:00 a 20:59; fuera de él, el campo de mensaje queda NULL.; El texto del día y mes se genera en español mediante DATENAME y se devuelve en mayúsculas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Paciente; Profesional de salud / médico; Centro de atención; Municipio; Tipo de cita (médica, apoyo diagnóstico, tratamientos especiales); Mensaje de texto / SMS recordatorio; Género del profesional (Doctor/Doctora)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas distintas con datos del paciente, número móvil, mensaje formateado de día/hora de cita, centro de atención, municipio, profesional y tipo de cita, solo cuando tipsolicitu=1 y codestcit=0.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DATEPART(HH, cit.fechorain) entre 7 y 12 → Formatea la hora con sufijo ''am'' (1pm sólo a partir de las 13). else Para horas 13-20 formatea con sufijo ''pm''; horas fuera del rango 7-20 producen NULL en Custom1.; si DATEPART(MINUTE, cit.fechorain) = 0 → Muestra la hora sin minutos (ej. ''a las 8am''). else Concatena los minutos al texto (ej. ''a las 8:30am'').; si per.gender = 1 → Antepone ''DOCTOR '' al nombre del profesional. else Si gender = 2 antepone ''DOCTORA ''; otros valores producen NULL.; si cit.TIPSOLICITU IN (''1'',''2'',''3'') → Asigna etiqueta ''Cita Medica'', ''Cita Apoyo Diagnostico'' o ''Cita Tratamiento Especiales'' respectivamente. else Otros valores producen NULL en ''Tipo de Cita'' (aunque el WHERE filtra solo ''1'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.agasicita; dbo.inpacient; dbo.adcenaten; dbo.inmunicip; dbo.inprofsal; security.userINT; security.personINT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSMSAppointments';
GO
