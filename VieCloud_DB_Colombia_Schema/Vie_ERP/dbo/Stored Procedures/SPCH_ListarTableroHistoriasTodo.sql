CREATE PROCEDURE [dbo].[SPCH_ListarTableroHistoriasTodo]
(
@Paciente Varchar(25),
@Empresa Char(3),
@Origen Char(1)
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

set @empresa = (select top 1 INDCODEMP  from INEMPRESu where EMPPRODUC = 1 )		
	SELECT 
		HCTELEFONICA,
		HCOTROSPROC,
		RECORDANESTESIA,
		A.IDMODELOHC AS 'MODELOHC',
		FECHISPAC AS 'FechaHistoria', 
		RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal', 
		RTRIM(C.NOMMEDICO) AS Medico, 
		C.CODPROSAL, 
		'' AS Live, 
		RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion,
		NUMEFOLIO+NUMINGRES AS NUMEFOLIO, 
		CONSFOLIO+NUMINGRES AS CONSFOLIO, 
		RTRIM(DATSUBJET) AS Subjetivo, 
		RTRIM(DATOBJETI) AS Objetivo, 
		RTRIM(DATPRONOS) AS Pronostico, 
		RTRIM(DATTRATAM) AS Tratamiento, 
		NUMINGRES AS Ingreso, 
		RTRIM(NUMEFOLIO) AS Folio, 
		RTRIM(DESESPECI) AS Especialidad, 
		UFUTIPUNI AS TipoUnidad, 
		@empresa AS Empresa, 
		ESTAFOLIO AS Estado, 
		A.CODCENATE AS CentroAtencion, 
		A.GENCONEXT,
	CASE 
		WHEN A.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
						WHEN 'I' THEN (case when RTRIM(A.IDETIPHIS) = 'HCNOTEVO1' then 'Nota Evolución - Ingreso' when HCTELEFONICA = 1 then 'Indicaciones Telefónicas - Ingreso' else 'Historia Clinica Ingreso' end ) 
						WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de Anestesia' when HCTELEFONICA = 1 then 'Indicaciones para enfermería' when HCOTROSPROC = 1 then 'Nota Otros Procedimientos' when HCOTROSPROC = 2 then 'Evolución Otros Procedimientos' else 'Nota Evolución' end )
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
						WHEN 'JM' THEN 'Junta Médica'
						ELSE 'Otro'
					END)
			WHEN A.IDMODELOHC IS NOT NULL THEN M.NOMBRE
		END AS DescripcionModeloHC,A.IdClinicalHistoryFormats,
	A.UFUCODIGO,
	A.CODCENATE
	FROM
		dbo.HCHISPACA A with(nolock) 
			LEFT JOIN dbo.INDIAGNOS B with(nolock) ON A.CODDIAGNO = B.CODDIAGNO 
			INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
			INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
			INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
			INNER JOIN dbo.INESPECIA ES with(nolock) ON A.CODESPTRA = ES.CODESPECI 
			LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
			LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = A.IdClinicalHistoryFormats
	WHERE 
			A.IPCODPACI=@Paciente 
			AND TIPHISPAC='I' 
			OR ((TIPHISPAC='N' OR TIPHISPAC='JM' ) 
			AND (JUNTAMEDICA = 1 OR HCTELEFONICA = 1 OR HCOTROSPROC IN (1,2) )
			AND A.IPCODPACI = @Paciente 
			AND (A.CONSFOLIO IS NULL OR A.CONSFOLIO = ''))		


--UNION 

		Select 
				HCTELEFONICA,
				HCOTROSPROC,
				RECORDANESTESIA,
				A.IDMODELOHC AS 'MODELOHC',
				FECHISPAC AS 'FechaHistoria', 
				RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal', 
				RTRIM(C.NOMMEDICO) AS Medico, 
				C.CODPROSAL, 
				'' AS Live, 
				RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion,
				NUMEFOLIO+NUMINGRES AS NUMEFOLIO, 
				CONSFOLIO+NUMINGRES AS CONSFOLIO, 
				RTRIM(DATSUBJET) AS Subjetivo, 
				RTRIM(DATOBJETI) AS Objetivo, 
				RTRIM(DATPRONOS) AS Pronostico, 
				RTRIM(DATTRATAM) AS Tratamiento, 
				NUMINGRES AS Ingreso, 
				RTRIM(NUMEFOLIO) AS Folio, 
				RTRIM(DESESPECI) AS Especialidad, 
				UFUTIPUNI AS TipoUnidad, 
				Container AS Empresa, 
				ESTAFOLIO AS Estado, 
				A.CODCENATE AS CentroAtencion, 
				A.GENCONEXT,
			CASE 
				WHEN A.IdClinicalHistoryFormats is not null THEN HistoryFormats.Name
					WHEN A.IDMODELOHC IS NULL THEN 
						(CASE A.TIPHISPAC
								WHEN 'I' THEN (case when RTRIM(A.IDETIPHIS) = 'HCNOTEVO1' then 'Nota Evolución - Ingreso' when HCTELEFONICA = 1 then 'Indicaciones Telefónicas - Ingreso' else 'Historia Clinica Ingreso' end ) 
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de Anestesia' when HCTELEFONICA = 1 then 'Indicaciones para enfermería' when HCOTROSPROC = 1 then 'Nota Otros Procedimientos' when HCOTROSPROC = 2 then 'Evolución Otros Procedimientos' else 'Nota Evolución' end )
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
								WHEN 'JM' THEN 'Junta Médica'
								ELSE 'Otro'
							END)
					WHEN A.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS DescripcionModeloHC,A.IdClinicalHistoryFormats,
			A.UFUCODIGO,
			A.CODCENATE
			FROM
				dbo.HCHISPACA_HISTORICA A with(nolock) 
					LEFT JOIN dbo.INDIAGNOS B with(nolock) ON A.CODDIAGNO = B.CODDIAGNO 
					INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
					INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
					INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
					INNER JOIN dbo.INESPECIA ES with(nolock) ON A.CODESPTRA = ES.CODESPECI 
					LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
					LEFT JOIN ClinicalParameters.ClinicalHistoryFormats HistoryFormats with(nolock) ON HistoryFormats.Id = A.IdClinicalHistoryFormats
			WHERE 
					A.IPCODPACI=@Paciente 
					AND TIPHISPAC='I' 
					OR ((TIPHISPAC='N' OR TIPHISPAC='JM' ) 
					AND (JUNTAMEDICA = 1 OR HCTELEFONICA = 1 OR HCOTROSPROC IN (1,2) )
					AND A.IPCODPACI = @Paciente 
					AND (A.CONSFOLIO IS NULL OR A.CONSFOLIO = ''))		

end

