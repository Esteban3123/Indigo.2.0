
CREATE PROCEDURE [Admissions].[SP_ListMedicalRecordsToRegeneratePDF]
(
@Paciente Varchar(25),
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
		C.CODPROSAL, 
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
		RTRIM(f.CODDIAGNO) + ' - ' + RTRIM(NOMDIAGNO) AS 'DiagnosticoPrincipal', 
		RTRIM(DATSUBJET) AS Subjetivo, 
		RTRIM(DATOBJETI) AS Objetivo, 
		RTRIM(DATPRONOS) AS Pronostico, 
		RTRIM(DATTRATAM) AS Tratamiento, 
		'' AS Live, 
		A.GENCONEXT,
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
			LEFT JOIN dbo.INDIAGNOS f with(nolock) ON A.CODDIAGNO = f.CODDIAGNO 
	WHERE 
		A.IPCODPACI=@Paciente
		Order by CAST(NUMEFOLIO as int) Asc	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las historias clínicas registradas para un paciente específico, incluyendo datos del folio clínico, fecha, médico tratante, especialidad, unidad funcional, centro de atención, diagnóstico principal y contenido clínico (subjetivo, objetivo, pronóstico, tratamiento). Cruza la historia clínica (HCHISPACA) con los maestros de paciente, profesional de salud, centro de atención, unidad funcional, especialidad, modelo de HC y diagnóstico CIE-10 para armar una ficha completa de cada nota clínica. Su propósito principal es identificar qué historias clínicas ya tienen PDF generado y cuáles aún no, permitiendo seleccionarlas para regenerar o crear el documento PDF correspondiente. Se utiliza en el módulo de gestión documental de historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los folios de historia clínica de un paciente con su descripción de modelo y estado de generación de PDF, para identificar cuáles requieren regenerar el PDF.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente (@Paciente) debe existir como IPCODPACI en HCHISPACA/INPACIENT para retornar registros; Cada folio debe tener referencias válidas (INNER JOIN) a profesional de salud, centro de atención, unidad funcional y especialidad tratante; folios con esas claves nulas o inexistentes quedan excluidos; El modelo de HC (PRMODELOHC) y el diagnóstico (INDIAGNOS) son opcionales (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan folios clínicos pertenecientes al paciente solicitado (A.IPCODPACI=@Paciente); El resultado se ordena ascendentemente por NUMEFOLIO convertido a entero; La columna Seleccion siempre se devuelve como BIT vacío/0 para uso de marcado en UI; CodeDiagnostic se fija siempre como literal ''Z000'' independientemente del diagnóstico real; La columna Empresa se propaga tal cual desde el parámetro de entrada y no se valida contra catálogo; Cada folio se enriquece con su modelo de HC: si existe IDMODELOHC se usa el nombre del catálogo PRMODELOHC; si no, se deriva del tipo de historia (TIPHISPAC) y banderas asociadas; Un folio se considera ''sin PDF generado'' únicamente cuando ReportCreatedID es NULL', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; historia clínica; folio clínico; modelo de historia clínica; especialidad médica; unidad funcional; centro de atención; diagnóstico principal (CIE-10); nota de evolución; registro de anestesia; partograma; nota farmacéutica; junta médica; indicaciones telefónicas; PDF de historia clínica', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Devuelve un resultset con los folios de historia clínica del paciente filtrado por A.IPCODPACI=@Paciente, ordenado por NUMEFOLIO ascendente, indicando si el folio tiene o no PDF generado (TienePDF / GeneradoPDF según ReportCreatedID)', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IDMODELOHC IS NULL → Se determina la descripción del modelo de HC mediante un CASE sobre TIPHISPAC (I, N, E, O, PT, NF, V, F, T, S, P, B, JM) con sub-reglas según banderas HCTELEFONICA, RECORDANESTESIA, HCOTROSPROC, StoryType e IDETIPHIS else Se toma M.NOMBRE del modelo de HC parametrizado en PRMODELOHC; si TIPHISPAC=''I'' y RTRIM(IDETIPHIS)=''HCNOTEVO1'' → Se etiqueta como ''Nota Evolución - Ingreso'' else Si HCTELEFONICA=1 → ''Indicaciones Telefónicas - Ingreso''; en otro caso → ''Historia Clinica Ingreso''; si TIPHISPAC=''N'' con RECORDANESTESIA=1, HCTELEFONICA=1, HCOTROSPROC=1 ó HCOTROSPROC=2 → Se etiqueta como ''Registro de Anestesia'', ''Indicaciones para enfermería'', ''Nota Otros Procedimientos'' o ''Evolución Otros Procedimientos'' respectivamente else ''Nota Evolución''; si TIPHISPAC=''E'' y StoryType=2 → Se etiqueta como ''Valoración salud visual'' else ''Evolución''; si ReportCreatedID IS NULL → Se marca GeneradoPDF=''Reporte sin PDF'' y TienePDF=0 (candidato a regenerar PDF) else GeneradoPDF=''Reporte generado en PDF'' y TienePDF=1', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INESPECIA; dbo.PRMODELOHC; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ListMedicalRecordsToRegeneratePDF';
-- GO
