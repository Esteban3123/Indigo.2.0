CREATE PROCEDURE [dbo].[SPCH_ListarTableroHistoriasEvoluciones]
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

	SELECT  RECORDANESTESIA,
		D.CODCENATE, FECHISPAC AS 'FechaHistoria',RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, NUMEFOLIO+NUMINGRES AS NUMEFOLIO, CONSFOLIO+NUMINGRES AS CONSFOLIO, 
		RTRIM(DATSUBJET) AS Subjetivo, RTRIM(DATOBJETI) AS Objetivo, RTRIM(DATPRONOS) AS Pronostico, RTRIM(DATTRATAM) AS Tratamiento,NUMINGRES AS Ingreso, 
		RTRIM(NUMEFOLIO) AS Folio,
		TIPHISPAC AS TipoHC,
		case TIPHISPAC WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'RA' else TIPHISPAC end ) when 'E' then (case when StoryType = 2 then 'SV' else TIPHISPAC end) ELSE TIPHISPAC  END AS TipoHCIcono,
		RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, @Empresa AS Empresa,ESTAFOLIO AS Estado, 
		CASE 
			WHEN A.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de Anestesia' when HCTELEFONICA = 1 then 'Indicaciones Telefónicas' when HCOTROSPROC = 1 then 'Nota Otros Procedimientos' when HCOTROSPROC = 2 then 'Evolución Otros Procedimientos' else 'Nota Evolución' end )
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
		END AS DescripcionModeloHC,A.IdClinicalHistoryFormats,
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
		A.IPCODPACI = @Paciente AND TIPHISPAC IN ('E','S','N','T','P','B','V', 'JM' ) 
		AND CONSFOLIO IN (SELECT NUMEFOLIO FROM dbo.HCHISPACA WHERE TIPHISPAC = 'I' AND IPCODPACI = @Paciente)

UNION

	SELECT RECORDANESTESIA,
		D.CODCENATE, A.FECINIREG AS 'FechaHistoria','PARTOGRAMA' AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, '0          ' + H.NUMINGRES  AS NUMEFOLIO, H.NUMEFOLIO + H.NUMINGRES AS CONSFOLIO, 
		'' AS Subjetivo, '' AS Objetivo, '' AS Pronostico, '' AS Tratamiento,A.NUMINGRES AS Ingreso,'' AS Folio, 'PT' AS TipoHC, 
		case TIPHISPAC WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'RA' else TIPHISPAC end ) when 'E' then (case when StoryType = 2 then 'SV' else TIPHISPAC end) ELSE TIPHISPAC  END AS TipoHCIcono,
		RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, @Empresa AS Empresa,1 AS Estado, 
		CASE 
			WHEN H.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN H.IDMODELOHC IS NULL THEN 
				(CASE H.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de Anestesia' when HCTELEFONICA = 1 then 'Indicaciones Telefónicas' when HCOTROSPROC = 1 then 'Nota Otros Procedimientos' when HCOTROSPROC = 2 then 'Evolución Otros Procedimientos' else 'Nota Evolución' end )
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
			WHEN H.IDMODELOHC IS NOT NULL THEN M.NOMBRE
		END AS DescripcionModeloHC,H.IdClinicalHistoryFormats,
		A.UFUCODIGO,
		A.CODCENATE

	FROM 
		dbo.HCPARTGRA AS A with(nolock) 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON C.CODESPEC1 = ES.CODESPECI 
			INNER JOIN dbo.HCHISPACA H with(nolock) ON A.NUMINGRES = H.NUMINGRES AND A.IPCODPACI = H.IPCODPACI AND H.TIPHISPAC = 'I'
			LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = H.IDMODELOHC
			LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = H.IdClinicalHistoryFormats
	WHERE
		A.IPCODPACI = @Paciente

UNION

	SELECT RECORDANESTESIA,
		D.CODCENATE, A.FECHISPAC AS 'FechaHistoria',RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion,A.NUMEFOLIO+A.NUMINGRES AS NUMEFOLIO,H.CONSFOLIO+H.NUMINGRES AS CONSFOLIO, 
		'' AS Subjetivo, '' AS Objetivo, '' AS Pronostico, '' AS Tratamiento,A.NUMINGRES AS Ingreso,RTRIM(A.NUMEFOLIO) AS Folio, 'NF' AS TipoHC, 
		'N' AS TipoHCIcono,
		RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, @Empresa AS Empresa,1 AS Estado, 
		'Nota Farmaceutica' AS DescripcionModeloHC,NULL as IdClinicalHistoryFormats,
		A.UFUCODIGO, A.CODCENATE
	FROM 
		dbo.HCNOSERFA AS A with(nolock) 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON C.CODESPEC1 = ES.CODESPECI 
			INNER JOIN dbo.HCHISPACA H with(nolock) ON A.NUMINGRES = H.NUMINGRES AND A.IPCODPACI = H.IPCODPACI AND A.NUMEFOLIO = H.NUMEFOLIO AND H.TIPHISPAC = 'NF' 
		INNER JOIN dbo.INDIAGNOS B with(nolock) ON H.CODDIAGNO = B.CODDIAGNO			
	WHERE 
		A.IPCODPACI = @Paciente


UNION  

		SELECT  RECORDANESTESIA,
		D.CODCENATE, FECHISPAC AS 'FechaHistoria',RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, NUMEFOLIO+NUMINGRES AS NUMEFOLIO, CONSFOLIO+NUMINGRES AS CONSFOLIO, 
		RTRIM(DATSUBJET) AS Subjetivo, RTRIM(DATOBJETI) AS Objetivo, RTRIM(DATPRONOS) AS Pronostico, RTRIM(DATTRATAM) AS Tratamiento,NUMINGRES AS Ingreso, 
		RTRIM(NUMEFOLIO) AS Folio,
		TIPHISPAC AS TipoHC,
		case TIPHISPAC WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'RA' else TIPHISPAC end ) when 'E' then (case when StoryType = 2 then 'SV' else TIPHISPAC end) ELSE TIPHISPAC  END AS TipoHCIcono,
		RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, Container AS Empresa,ESTAFOLIO AS Estado, 
		CASE 
			WHEN A.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de Anestesia' when HCTELEFONICA = 1 then 'Indicaciones Telefónicas' when HCOTROSPROC = 1 then 'Nota Otros Procedimientos' when HCOTROSPROC = 2 then 'Evolución Otros Procedimientos' else 'Nota Evolución' end )
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
		END AS DescripcionModeloHC,A.IdClinicalHistoryFormats,
		A.UFUCODIGO,
		A.CODCENATE
	FROM 
		dbo.HCHISPACA_HISTORICA A with(nolock)
			INNER JOIN dbo.INDIAGNOS B with(nolock) ON A.CODDIAGNO = B.CODDIAGNO 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON A.CODESPTRA = ES.CODESPECI 
			LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
			LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = A.IdClinicalHistoryFormats
	WHERE
		A.IPCODPACI = @Paciente AND TIPHISPAC IN ('E','S','N','T','P','B','V', 'JM' ) 
		AND CONSFOLIO IN (SELECT NUMEFOLIO FROM dbo.HCHISPACA_HISTORICA WHERE TIPHISPAC = 'I' AND IPCODPACI = @Paciente)

UNION

	SELECT RECORDANESTESIA,
		D.CODCENATE, A.FECINIREG AS 'FechaHistoria','PARTOGRAMA' AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, '0          ' + H.NUMINGRES  AS NUMEFOLIO, H.NUMEFOLIO + H.NUMINGRES AS CONSFOLIO, 
		'' AS Subjetivo, '' AS Objetivo, '' AS Pronostico, '' AS Tratamiento,A.NUMINGRES AS Ingreso,'' AS Folio, 'PT' AS TipoHC, 
		case TIPHISPAC WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'RA' else TIPHISPAC end ) when 'E' then (case when StoryType = 2 then 'SV' else TIPHISPAC end) ELSE TIPHISPAC  END AS TipoHCIcono,
		RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, a.Container AS Empresa,1 AS Estado, 
		CASE 
			WHEN H.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN H.IDMODELOHC IS NULL THEN 
				(CASE H.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de Anestesia' when HCTELEFONICA = 1 then 'Indicaciones Telefónicas' when HCOTROSPROC = 1 then 'Nota Otros Procedimientos' when HCOTROSPROC = 2 then 'Evolución Otros Procedimientos' else 'Nota Evolución' end )
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
			WHEN H.IDMODELOHC IS NOT NULL THEN M.NOMBRE
		END AS DescripcionModeloHC,H.IdClinicalHistoryFormats,
		A.UFUCODIGO,
		A.CODCENATE

	FROM 
		dbo.HCPARTGRA_HISTORICA  AS A with(nolock) 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON C.CODESPEC1 = ES.CODESPECI 
			INNER JOIN dbo.HCHISPACA_HISTORICA H with(nolock) ON A.NUMINGRES = H.NUMINGRES AND A.IPCODPACI = H.IPCODPACI AND H.TIPHISPAC = 'I'
			LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = H.IDMODELOHC
			LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = H.IdClinicalHistoryFormats
	WHERE
		A.IPCODPACI = @Paciente

UNION

	SELECT RECORDANESTESIA,
		D.CODCENATE, A.FECHISPAC AS 'FechaHistoria',RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion,A.NUMEFOLIO+A.NUMINGRES AS NUMEFOLIO,H.CONSFOLIO+H.NUMINGRES AS CONSFOLIO, 
		'' AS Subjetivo, '' AS Objetivo, '' AS Pronostico, '' AS Tratamiento,A.NUMINGRES AS Ingreso,RTRIM(A.NUMEFOLIO) AS Folio, 'NF' AS TipoHC, 
		'N' AS TipoHCIcono,
		RTRIM(DESESPECI) AS Especialidad , UFUTIPUNI AS TipoUnidad, a.Container AS Empresa,1 AS Estado, 
		'Nota Farmaceutica' AS DescripcionModeloHC,NULL as IdClinicalHistoryFormats,
		A.UFUCODIGO, A.CODCENATE
	FROM 
		dbo.HCNOSERFA_HISTORICA AS A with(nolock) 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON C.CODESPEC1 = ES.CODESPECI 
			INNER JOIN dbo.HCHISPACA_HISTORICA H with(nolock) ON A.NUMINGRES = H.NUMINGRES AND A.IPCODPACI = H.IPCODPACI AND A.NUMEFOLIO = H.NUMEFOLIO AND H.TIPHISPAC = 'NF' 
		INNER JOIN dbo.INDIAGNOS B with(nolock) ON H.CODDIAGNO = B.CODDIAGNO			
	WHERE 
		A.IPCODPACI = @Paciente

end

