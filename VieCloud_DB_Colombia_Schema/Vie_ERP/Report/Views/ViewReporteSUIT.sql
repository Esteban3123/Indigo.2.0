

CREATE VIEW [Report].[ViewReporteSUIT] as
SELECT DISTINCT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
AUD.CODUSUCONS 'ID USUARIO QUE CONSULTA',US.NOMUSUARI 'USUARIO CONSULTA',AUD.CODPACQCON 'ID PACIENTE CONSULTADO',
PAC.IPNOMCOMP 'NOMBRE PACIENTE', AUD.FECHCONSU 'FECHA DE CONSULTA',AUD.NOMMAQCONS 'EQUIPO GENERA CONSULTA',
ISNULL(AUD.FOLIOIN,'N/A') 'FOLIO CONSULTADO', AUD.IPMAQCONS 'DIR. IP EQUIPO',ISNULL(AUD.INGRESO,'N/A') 'INGRESO', 
ISNULL(CHC.Nombre,'SIN MOTIVO DE CONSULTA') 'MOTIVO DE CONSULTA',
1 as 'CANTIDAD',
CAST(AUD.FECHCONSU AS date) AS 'FECHA BUSQUEDA',
YEAR(AUD.FECHCONSU) AS 'AÑO FECHA BUSQUEDA', 
MONTH(AUD.FECHCONSU) AS 'MES AÑO FECHA BUSQUEDA', 
CASE MONTH(AUD.FECHCONSU) 
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
FORMAT(DAY(AUD.FECHCONSU), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(AUD.FECHCONSU), '00') ,' - ', 
	   CASE MONTH(AUD.FECHCONSU) 
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
		END) MES_LABEL_BUSQUEDA,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM HCAUDITORIA AUD
LEFT JOIN MOVCONSULHC CHC WITH(NOLOCK) ON CHC.Id=AUD.MOVCONSULHCID
LEFT JOIN INPACIENT PAC WITH(NOLOCK) ON PAC.IPCODPACI=AUD.CODPACQCON
LEFT JOIN SEGusuaru US WITH(NOLOCK) ON US.CODUSUARI=AUD.CODUSUCONS
WHERE AUD.CODPACQCON NOT IN ('000000000000000')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a auditoría de accesos al expediente clínico electrónico, conforme al sistema SUIT. Consolida por cada consulta registrada: el usuario que accedió, el paciente consultado, la fecha/hora, el equipo e IP de origen, el folio y el motivo de consulta. Enriquece las fechas con etiquetas de año, mes (nombre y número) y día para facilitar filtros en herramientas de BI, excluyendo registros con código de paciente nulo/vacío.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los eventos de auditoría de consultas a la historia clínica enriquecidos con datos del usuario, paciente y motivo, junto con dimensiones de fecha (año, mes numérico/nombre, día y etiqueta) para reportería tipo SUIT.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla HCAUDITORIA debe registrar los eventos de consulta de historia clínica con el paciente consultado, usuario, fecha y equipo; Debe existir la zona horaria ''Pakistan Standard Time'' registrada en el sistema para CONVERT/AT TIME ZONE', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye registros cuyo paciente consultado sea el código genérico ''000000000000000''; Cuando no hay motivo de consulta asociado se reporta el literal ''SIN MOTIVO DE CONSULTA''; Cuando FOLIOIN o INGRESO son nulos se reportan como ''N/A''; Cada fila de auditoría contribuye con CANTIDAD = 1 (apto para sumarización); ID_COMPANY se obtiene del nombre de la base de datos actual truncado a 9 caracteres; La marca de última actualización (ULT_ACTUAL) se calcula con la hora actual convertida a la zona horaria ''Pakistan Standard Time''; Se aplica DISTINCT para eliminar duplicados en el resultado del reporte', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'auditoría de consultas a historia clínica; paciente; usuario; motivo de consulta; folio; ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCAUDITORIA: Retorna registros de auditoría de consultas de HC excluyendo aquellos con CODPACQCON = ''000000000000000'', con LEFT JOIN a MOVCONSULHC, INPACIENT y SEGusuaru para enriquecer motivo, paciente y usuario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MONTH(AUD.FECHCONSU) entre 1 y 12 → Traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) para etiquetas de reporte', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCAUDITORIA; MOVCONSULHC; INPACIENT; SEGusuaru', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSUIT';
GO
