CREATE PROCEDURE [Admissions].[SP_ListMedicalRecordsToRegeneratePDFMasivo]
(
@CodigoCentroAtencion Varchar(10),
@Empresa Char(3)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
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
	A.CODCENATE=@CodigoCentroAtencion 
	and A.ReportCreatedID is null
	and FECHISPAC < '31-12-2024' --fecha indicada por jose reyes para jersalud 08-08-2024
	order by  CAST(A.NUMEFOLIO AS INT) asc 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las historias clínicas de un centro de atención que aún no tienen PDF generado y cuya fecha es anterior al 31 de diciembre de 2024, para permitir la regeneración masiva de sus documentos PDF. Integra datos del folio clínico (HCHISPACA) con información del paciente (nombre y cédula), el profesional de salud tratante, la unidad funcional, el centro de atención y la especialidad. Para cada historia clínica devuelve la descripción del tipo de historia según su modelo o plantilla (consulta, nota de evolución, partograma, anestesia, etc.), el número de ingreso, el folio y un indicador de si el PDF ya fue generado o está pendiente. Se usa en procesos administrativos y de gestión documental para identificar y regenerar en lote los reportes PDF de historias clínicas sin documento asociado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista masivamente las historias clínicas de un centro de atención cuyo PDF aún no fue generado y con fecha anterior al 31-12-2024, para regenerar/exportar sus reportes en PDF.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención (@CodigoCentroAtencion) debe existir en HCHISPACA y en ADcenaten para producir filas.; Cada historia clínica debe tener paciente, profesional, unidad funcional y especialidad válidos (INNER JOIN obligatorio); de lo contrario se excluye.; El modelo de HC en PRMODELOHC es opcional (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se listan historias clínicas cuyo PDF aún no ha sido generado (ReportCreatedID IS NULL).; Sólo se incluyen folios con FECHISPAC anterior al 31-12-2024 (corte fijo en código).; El listado se restringe al centro de atención recibido por parámetro (A.CODCENATE = @CodigoCentroAtencion).; Los registros se ordenan ascendentemente por número de folio convertido a entero.; El diagnóstico se fuerza al código fijo ''Z000'' independientemente del registro real.; La empresa devuelta corresponde literalmente al parámetro recibido, no se valida contra catálogo.; Si existe modelo de HC (IDMODELOHC), su nombre prevalece sobre la clasificación derivada de TIPHISPAC.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Folio clínico; Centro de atención; Unidad funcional; Especialidad médica; Profesional de la salud; Paciente; Modelo de historia clínica; Reporte PDF de HC; Nota de evolución; Registro de anestesia; Partograma; Junta médica; Consulta preanestesia; Recién nacido; Atención de partos', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Devuelve un resultset de historias clínicas pendientes de PDF filtradas por CODCENATE=@CodigoCentroAtencion, ReportCreatedID IS NULL y FECHISPAC < ''31-12-2024'', ordenadas por NUMEFOLIO ascendente.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IDMODELOHC IS NULL → Determina la descripción del tipo de historia clínica con base en A.TIPHISPAC y banderas auxiliares (HCTELEFONICA, RECORDANESTESIA, HCOTROSPROC, StoryType, IDETIPHIS) else Toma la descripción desde PRMODELOHC.NOMBRE asociada al modelo configurado; si TIPHISPAC=''I'' y RTRIM(IDETIPHIS)=''HCNOTEVO1'' → Etiqueta ''Nota Evolución - Ingreso'' else Si HCTELEFONICA=1 → ''Indicaciones Telefónicas - Ingreso''; en otro caso ''Historia Clinica Ingreso''; si TIPHISPAC=''N'' con RECORDANESTESIA=1 / HCTELEFONICA=1 / HCOTROSPROC=1 / HCOTROSPROC=2 → Asigna respectivamente ''Registro de Anestesia'', ''Indicaciones para enfermería'', ''Nota Otros Procedimientos'', ''Evolución Otros Procedimientos'' else ''Nota Evolución''; si TIPHISPAC=''E'' y StoryType=2 → Etiqueta ''Valoración salud visual'' else ''Evolución''; si ReportCreatedID IS NULL → Marca GeneradoPDF=''Reporte sin PDF'' y TienePDF=0 (siendo además criterio obligatorio del WHERE para incluir el folio) else Marca ''Reporte generado en PDF'' y TienePDF=1', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INESPECIA; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivo';
-- GO
