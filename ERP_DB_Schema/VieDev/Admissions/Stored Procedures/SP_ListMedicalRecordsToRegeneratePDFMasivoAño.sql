
CREATE PROCEDURE [Admissions].[SP_ListMedicalRecordsToRegeneratePDFMasivoAño]
(
@CodigoCentroAtencion Varchar(10),
@Año datetime,
@Cantidad int,
@Empresa Char(3)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP (@Cantidad)
		CAST('' AS BIT) AS Seleccion,
		Format(FECHISPAC, 'dd/MM/yyyy') AS 'FechaHistoria',
	    RTRIM(C.NOMMEDICO) AS Medico, 
		RTRIM(E.UFUDESCRI) AS UnidadFuncional,
		concat(rtrim(a.CODCENATE), '-',RTRIM(D.nomcenate)) as 'CentroAtencion',	
		RTRIM(DESESPECI) AS Especialidad, 
		RTRIM(E.UFUDESCRI) AS UnidadFuncional,
		concat(B.IPCODPACI,' - ', rtrim(b.IPNOMCOMP)) AS 'Paciente',
		rtrim(B.IPCODPACI) as IdentificacionPaciente,
		rtrim(b.IPNOMCOMP) AS NombrePaciente,
		RTRIM(C.CODPROSAL) AS CedulaMedico, 
		CAST(A.NUMEFOLIO AS INT) AS Folio,
		A.NUMINGRES AS Ingreso,  
		A.StoryType AS StoryType,  
		A.IDETIPHIS AS HistoryCode,  
		A.CODCENATE AS CodeHealthCareCenter,
		A.UFUCODIGO AS CodeFunctionalUnit,
		E.UFUTIPUNI AS TypeFunctionalUnit,
		A.CODESPTRA AS CodeTreatingSpecialty,
		A.GENCONEXT,
		A.ESTAFOLIO AS Estado,
		@empresa AS Empresa,
		a.INDICAPAC AS DestinationPatient,	
		b.IPSEXOPAC,
		'Z000' AS CodeDiagnostic,
		B.IPTIPODOC,
	CASE 
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
	END AS DescripcionModeloHC,
	iif(ReportCreatedID IS NULL, 'Reporte sin PDF', 'Reporte generado en PDF') AS GeneradoPDF,
	IIF(ReportCreatedID IS NULL, CAST(0 AS BIT), CAST(1 AS BIT)) AS TienePDF,
	M.CODIGO AS CodigoHCEspecializada
	FROM
		dbo.HCHISPACA A with(nolock) 
		INNER JOIN dbo.INPACIENT b with(nolock) ON b.IPCODPACI = a.IPCODPACI 
	    INNER JOIN dbo.INPROFSAL C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
		INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.codcenate 
		INNER JOIN dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
		INNER JOIN dbo.INESPECIA ES with(nolock) ON A.CODESPTRA = ES.CODESPECI 
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
	WHERE 
	--A.IPCODPACI = '1000077900' AND
	A.CODCENATE=@CodigoCentroAtencion 
	and A.ReportCreatedID is null
	and YEAR(FECHISPAC) = YEAR(@Año) --fecha indicada por jose reyes para jersalud 08-08-2024
	order by  CAST(A.NUMEFOLIO AS INT) asc 
END