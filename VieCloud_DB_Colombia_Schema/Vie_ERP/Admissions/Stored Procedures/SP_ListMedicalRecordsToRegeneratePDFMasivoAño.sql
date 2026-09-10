
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las historias clínicas de un centro de atención y año específicos que aún no tienen PDF generado, con el fin de permitir la regeneración masiva de documentos PDF de historia clínica electrónica. Combina información de la historia clínica (HCHISPACA) con datos del paciente (nombre, cédula, tipo de documento), del profesional de salud tratante (nombre y código del médico), del centro de atención, la unidad funcional y la especialidad médica. Devuelve un número limitado de registros (controlado por el parámetro @Cantidad) ordenados por folio, incluyendo el tipo y descripción del modelo de HC (consulta, evolución, nota farmacéutica, partograma, recién nacido, junta médica, entre otros), el estado del folio y si el reporte ya fue generado en PDF o no. Se usa para gestión documental y auditoría de historias clínicas pendientes de PDF en un año calendario determinado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas de un centro de atención y año específicos que aún no tienen PDF generado, para identificar candidatas a regeneración masiva del reporte clínico en PDF.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención (@CodigoCentroAtencion) debe existir y tener registros en HCHISPACA.; @Año debe ser una fecha válida; solo se considera su componente YEAR.; @Cantidad debe ser un entero positivo para limitar el TOP.; NUMEFOLIO debe ser convertible a INT para el ordenamiento.; Las claves foráneas (IPCODPACI, CODPROSAL, CODCENATE, UFUCODIGO, CODESPTRA) deben tener correspondencia en sus catálogos por usar INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan historias clínicas cuyo ReportCreatedID es NULL (sin PDF generado).; El listado se restringe al centro de atención indicado y al año (YEAR(FECHISPAC)=YEAR(@Año)).; Los resultados se limitan al TOP @Cantidad y se ordenan ascendentemente por NUMEFOLIO (numérico).; El campo Seleccion siempre se inicializa como BIT vacío (false) en cada fila.; CodeDiagnostic se asigna fijo como ''Z000''.; El campo Empresa devuelto siempre es el parámetro @Empresa, no proviene de los datos.; Cuando existe modelo de HC (IDMODELOHC no nulo), prevalece el nombre del modelo sobre la clasificación derivada de TIPHISPAC.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'historia clínica; folio clínico; paciente; profesional de salud (médico); centro de atención; unidad funcional; especialidad médica; modelo de historia clínica; PDF de reporte clínico; anestesia; partograma; junta médica; atención de partos; recién nacido', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve hasta @Cantidad filas con datos descriptivos de la historia clínica (paciente, médico, especialidad, unidad funcional, modelo HC, estado PDF) filtradas por CODCENATE=@CodigoCentroAtencion, ReportCreatedID IS NULL y YEAR(FECHISPAC)=YEAR(@Año), ordenadas por NUMEFOLIO ascendente.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IDMODELOHC IS NULL → Se deriva la descripción del tipo de historia clínica a partir de A.TIPHISPAC y banderas auxiliares (HCTELEFONICA, RECORDANESTESIA, HCOTROSPROC, StoryType, IDETIPHIS) else Se toma el nombre del modelo (M.NOMBRE) desde PRMODELOHC; si TIPHISPAC=''I'' con IDETIPHIS=''HCNOTEVO1'' → Descripción = ''Nota Evolución - Ingreso'' else Si HCTELEFONICA=1 → ''Indicaciones Telefónicas - Ingreso''; en otro caso ''Historia Clinica Ingreso''; si TIPHISPAC=''N'' → RECORDANESTESIA=1→''Registro de Anestesia''; HCTELEFONICA=1→''Indicaciones para enfermería''; HCOTROSPROC=1→''Nota Otros Procedimientos''; HCOTROSPROC=2→''Evolución Otros Procedimientos''; else ''Nota Evolución''; si TIPHISPAC=''E'' → StoryType=2 → ''Valoración salud visual''; else ''Evolución''; si ReportCreatedID IS NULL → Se marca como ''Reporte sin PDF'' y TienePDF=0 (registro elegible para regeneración) else No es seleccionado por el filtro WHERE', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INESPECIA; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDFMasivoAño';
-- GO
