CREATE PROCEDURE [dbo].[SPCH_ListarTableroHistoriasUnidades]
(
@Paciente Varchar(25),
@Empresa Char(3)
)
AS
BEGIN
	SET NOCOUNT ON;

	/*  MEDILASER
		050 -> Base de datos Historica   -> (Registros en la tbla: HCHISPACA_HISTORICA segmentados por la columna Container)
		051 -> Base de datos Nueva azure Transaccional
		052 -> Base de datos Historica   -> (Registros en la tbla: HCHISPACA_HISTORICA segmentados por la columna Container)
		053 -> Base de datos Historica   -> (Registros en la tbla: HCHISPACA_HISTORICA segmentados por la columna Container)
	*/

	/*  JERSALUD
		025 -> Base de datos Historica, 04-07-2026 se debe renombrar la tabla HCHISPACA_HISTORICA025  Por HCHISPACA_HISTORICA
	*/

	SELECT 
		FECHISPAC AS 'FechaHistoria', A.IDMODELOHC AS 'MODELOHC',RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, NUMEFOLIO+NUMINGRES AS NUMEFOLIO, CONSFOLIO+NUMINGRES AS CONSFOLIO, 
		RTRIM(DATSUBJET) AS Subjetivo, RTRIM(DATOBJETI) AS Objetivo, RTRIM(DATPRONOS) AS Pronostico, RTRIM(DATTRATAM) AS Tratamiento, 
		NUMINGRES AS Ingreso,RTRIM(NUMEFOLIO) AS Folio, TIPHISPAC AS TipoHC, RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, 
		@Empresa AS Empresa, A.CODCENATE AS CentroAtencion, ESTAFOLIO AS Estado, CAST(ESTAFOLIO as char(1)) as est, 
		CASE 
			WHEN A.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' THEN 'Nota Evolución'
						WHEN 'E' THEN (case when StoryType = 2 then 'Valoración salud visual' else 'Evolución' end)
						WHEN 'O' THEN 'Otros modelos de apoyo'
						WHEN 'PT' THEN 'Partograma'
						WHEN 'NF' THEN 'Nota Farmaceutica'
						WHEN 'V' THEN 'Valoración de Seguimiento'
						WHEN 'F' THEN 'Consulta Preanestesia'	
						WHEN 'T' THEN 'Historia clinica de Control'
						WHEN 'S' THEN 'Servicio de apoyo'
						WHEN 'P' THEN 'Atención partos'
						WHEN 'B' THEN 'Recien Nacido'
						WHEN 'JM' THEN 'Junta Médica - Nota Evolución'
						ELSE 'Otro'
					END)
			WHEN A.IDMODELOHC IS NOT NULL THEN M.NOMBRE
		END AS DescripcionModeloHC, A.IdClinicalHistoryFormats,
		A.UFUCODIGO,
		A.CODCENATE

	FROM 
		dbo.HCHISPACA A with(nolock) 
			INNER JOIN dbo.INDIAGNOS B with(nolock) ON A.CODDIAGNO = B.CODDIAGNO 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON A.CODESPTRA = ES.CODESPECI
			LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
			LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = A.IdClinicalHistoryFormats
	WHERE 
		A.IPCODPACI = @Paciente 
		AND TIPHISPAC IN ('E','S','N','P', 'JM','NF' ) 
		AND CONSFOLIO NOT IN (SELECT NUMEFOLIO FROM dbo.HCHISPACA WHERE TIPHISPAC = 'I' AND IPCODPACI=@Paciente)


UNION 

	SELECT 
		FECHISPAC AS 'FechaHistoria', A.IDMODELOHC AS 'MODELOHC',RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, NUMEFOLIO+NUMINGRES AS NUMEFOLIO, CONSFOLIO+NUMINGRES AS CONSFOLIO, 
		RTRIM(DATSUBJET) AS Subjetivo, RTRIM(DATOBJETI) AS Objetivo, RTRIM(DATPRONOS) AS Pronostico, RTRIM(DATTRATAM) AS Tratamiento, 
		NUMINGRES AS Ingreso,RTRIM(NUMEFOLIO) AS Folio, TIPHISPAC AS TipoHC, RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, 
		Container AS Empresa, A.CODCENATE AS CentroAtencion, ESTAFOLIO AS Estado, CAST(ESTAFOLIO as char(1)) as est, 
		CASE 
			WHEN A.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' THEN 'Nota Evolución'
						WHEN 'E' THEN (case when StoryType = 2 then 'Valoración salud visual' else 'Evolución' end)
						WHEN 'O' THEN 'Otros modelos de apoyo'
						WHEN 'PT' THEN 'Partograma'
						WHEN 'NF' THEN 'Nota Farmaceutica'
						WHEN 'V' THEN 'Valoración de Seguimiento'
						WHEN 'F' THEN 'Consulta Preanestesia'	
						WHEN 'T' THEN 'Historia clinica de Control'
						WHEN 'S' THEN 'Servicio de apoyo'
						WHEN 'P' THEN 'Atención partos'
						WHEN 'B' THEN 'Recien Nacido'
						WHEN 'JM' THEN 'Junta Médica - Nota Evolución'
						ELSE 'Otro'
					END)
			WHEN A.IDMODELOHC IS NOT NULL THEN M.NOMBRE
		END AS DescripcionModeloHC, A.IdClinicalHistoryFormats,
		A.UFUCODIGO,
		A.CODCENATE

	FROM 
		dbo.HCHISPACA_HISTORICA  A with(nolock) 
			INNER JOIN dbo.INDIAGNOS B with(nolock) ON A.CODDIAGNO = B.CODDIAGNO 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON A.CODESPTRA = ES.CODESPECI
			LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
			LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = A.IdClinicalHistoryFormats
	WHERE 
		A.IPCODPACI = @Paciente 
		AND  TIPHISPAC IN ('E','S','N','P', 'JM','NF' ) 
		AND CONSFOLIO NOT IN (SELECT NUMEFOLIO FROM dbo.HCHISPACA_HISTORICA WHERE TIPHISPAC = 'I' AND IPCODPACI=@Paciente)

end

