
CREATE PROCEDURE [dbo].[SPHC_GenerarDatosClinicosRelevantesHCOP]
(
@Paciente Varchar(25),
@Ingreso Varchar(20),
@Folio Varchar(10)
)
AS
BEGIN
    SET NOCOUNT ON

	DECLARE @Fecha As DateTime = (SELECT FECHISPAC from dbo.HCHISPACA WHERE IPCODPACI = @Paciente AND NUMINGRES = @Ingreso AND NUMEFOLIO = @Folio);

	WITH CTE_DatosClinicosRelevantesHCOP AS (
		--Traemos folio del ingreso a la institución
		select 
		top 1 
		A.FECINIATE As 'FechaHistoria',
		concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: ', CASE 
					WHEN B.IDMODELOHC IS NULL THEN 
						(CASE B.TIPHISPAC
								WHEN 'I' THEN (case when StoryType IN (12, 13) then 'Informe QX' else 'Ingreso' end)
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de anestesia' when HCTELEFONICA = 1 then 'Indicaciones telefónicas' when HCOTROSPROC = 1 then 'Nota otros procedimientos' when HCOTROSPROC = 2 then 'Evolución otros procedimientos' when StoryType IN (12, 13) then 'Informe QX' else 'Nota evolución' end )
								WHEN 'E' THEN (case when StoryType <> 2 then 'Evolución' end)
								WHEN 'JM' THEN 'Junta médica - Nota evolución'
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE END , ' - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Motivo de consulta: ', RTRIM(MOTCONSUL), CHAR(10), 'Enfermedad actual: ', rtrim(ENFACTUAL),
					CHAR(10), 'Objetivo: ', CONCAT([dbo].[SignosVitalesPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO, 0), ' - ', [dbo].[ExamenFisicoPorFolioConcatenados](@Paciente, @Ingreso, A.NUMEFOLIO, 0)),
					CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO),
					CHAR(10), 'Análisis: ', rtrim(ANALISISP),					
					CHAR(10), 'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)),
					CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)
					) as Information
		from HCURGING1 A 
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		INNER JOIN dbo.INESPECIA E with(Nolock) ON B.CODESPTRA=E.CODESPECI
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		LEFT JOIN dbo.HCQXINFOR D with(nolock) ON A.IDETIPHIS=D.IDETIPHIS AND A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.NUMEFOLIO=D.NUMEFOLIO
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND B.TIPHISPAC = 'I' ORDER BY A.NUMEFOLIO ASC 
	
UNION
		--Traemos historias desde la fecha y hora del folio de la solicitud, hasta el momento actual
		select 
		A.FECINIATE As 'FechaHistoria',
				CASE 
					WHEN B.IDMODELOHC IS NULL THEN 
						(CASE B.TIPHISPAC
								WHEN 'I' THEN (
									case when StoryType IN (12, 13) then
										concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Informe QX - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Hallazgo operativo: ', DESHALLOP) 
									else 
										concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Ingreso - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Motivo de consulta: ', RTRIM(MOTCONSUL), CHAR(10), 'Enfermedad actual: ', rtrim(ENFACTUAL),
										CHAR(10), 'Objetivo: ', CONCAT([dbo].[SignosVitalesPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO, 0), [dbo].[ExamenFisicoPorFolioConcatenados](@Paciente, @Ingreso, A.NUMEFOLIO, 0)),
										CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', rtrim(ANALISISP), CHAR(10),
										'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) end)
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de anestesia' when HCTELEFONICA = 1 then 'Indicaciones telefónicas' 
									when HCOTROSPROC = 1 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota otros procedimientos - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis y conclusión: ', ANALISISP, CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) 
									when HCOTROSPROC = 2 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución otros procedimientos - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis y conclusión: ', ANALISISP, CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) 
									when StoryType IN (12, 13) then 
											concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20), ' - Tipo historia: Informe QX - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Hallazgo operativo: ', DESHALLOP, CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) 
										else 
											concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota evolución - Unidad funcional: ', RTRIM(UFUDESCRI), 
											CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', RTRIM(ANALISISP),
											CHAR(10), 'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) end)
								WHEN 'E' THEN (
									case when StoryType <> 2 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 
									'Subjetivo: ', RTRIM(B.DATSUBJET), CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', ANALISISP, CHAR(10),
									'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) end)
								WHEN 'JM' THEN concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Junta médica - Nota evolución - Unidad funcional: ', RTRIM(UFUDESCRI), 
									CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', RTRIM(ANALISISP), 
									CHAR(10), 'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10))
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS Information
		from HCURGING1 A 
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		INNER JOIN dbo.INESPECIA E with(Nolock) ON B.CODESPTRA=E.CODESPECI
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		LEFT JOIN dbo.HCQXINFOR D with(nolock) ON A.IDETIPHIS=D.IDETIPHIS AND A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.NUMEFOLIO=D.NUMEFOLIO
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND A.FECINIATE > @Fecha
		AND (B.TIPHISPAC IN ('I','JM') OR (B.TIPHISPAC = 'N' AND (RECORDANESTESIA IS NULL OR RECORDANESTESIA = 0)) OR (B.TIPHISPAC = 'N' AND (HCTELEFONICA IS NULL OR HCTELEFONICA = 0)) OR (B.TIPHISPAC = 'E' AND StoryType <> 2))

UNION
		
		select 
		A.FECINIATE As 'FechaHistoria',
				CASE 
					WHEN B.IDMODELOHC IS NULL THEN 
						(CASE B.TIPHISPAC
								WHEN 'I' THEN (case when StoryType IN (12, 13) then 'Informe QX' else 'Ingreso' end)
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de anestesia' when HCTELEFONICA = 1 then 'Indicaciones telefónicas' when HCOTROSPROC = 1 then 'Nota otros procedimientos' when HCOTROSPROC = 2 then 'Evolución otros procedimientos' when StoryType IN (12, 13) then 'Informe QX' else 'Nota evolución' end )
								WHEN 'E' THEN (
									case when StoryType <> 2 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10),
									'Subjetivo: ', SUBJETIVO, CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', ANALISISP, CHAR(10), 
									'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)),
									CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) 
									end)
								WHEN 'JM' THEN 'Junta médica - Nota evolución'
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS Information
		from HCURGEVO1 A
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		INNER JOIN dbo.INESPECIA E with(Nolock) ON B.CODESPTRA=E.CODESPECI
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND A.FECINIATE > @Fecha
		AND (B.TIPHISPAC IN ('I','JM') OR (B.TIPHISPAC = 'N' AND (RECORDANESTESIA IS NULL OR RECORDANESTESIA = 0)) OR (B.TIPHISPAC = 'N' AND (HCTELEFONICA IS NULL OR HCTELEFONICA = 0)) OR (B.TIPHISPAC = 'E' AND StoryType <> 2))

UNION
		
		select 
		A.FECINIATE As 'FechaHistoria',
				CASE 
					WHEN B.IDMODELOHC IS NULL THEN 
						(CASE B.TIPHISPAC
								WHEN 'I' THEN (case when StoryType IN (12, 13) then 'Informe QX' else 'Ingreso' end)
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de anestesia' when HCTELEFONICA = 1 then 'Indicaciones telefónicas' 
									when HCOTROSPROC = 1 then concat('Fecha historia :', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota otros procedimientos - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis y conclusión: ', ANALISISP) 
									when HCOTROSPROC = 2 then concat('Fecha historia :', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución otros procedimientos - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis y conclusión: ', ANALISISP) 
									when StoryType IN (12, 13) then 
										concat('Fecha historia:', CONVERT(varchar(50), A.FECINIATE, 20), ' - Tipo historia: Informe QX - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Hallazgo operativo: ', DESHALLOP) 
									else concat('Fecha historia:', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota evolución Unidad funcional: ', RTRIM(UFUDESCRI), 
									CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', RTRIM(ANALISISP),
									CHAR(10), 'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) end)
								WHEN 'E' THEN (
									case when StoryType <> 2 then concat('Fecha historia :', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 
									'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', ANALISISP,
									CHAR(10), 'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10)) end)
								WHEN 'JM' THEN concat('Fecha historia :', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Junta médica - Nota evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10),
									CHAR(10), 'Diagnósticos: ', [dbo].[DiagnosticoPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), CHAR(10), 'Análisis: ', ANALISISP,
									CHAR(10), 'Tratamiento instaurado: ', CONCAT([dbo].[MedicamentosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO), ' - ', [dbo].[MezclasLiquidosPorFolioConcatenados] (@Paciente, @Ingreso, A.NUMEFOLIO)), CHAR(10), 'Especialidad: ', RTRIM(E.DESESPECI), CHAR(10), CHAR(10))
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS Information
		from HCNOTEVO1 a 
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		INNER JOIN dbo.INESPECIA E with(Nolock) ON B.CODESPTRA=E.CODESPECI
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		LEFT JOIN dbo.HCQXINFOR D with(nolock) ON A.IDETIPHIS=D.IDETIPHIS AND A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.NUMEFOLIO=D.NUMEFOLIO
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND A.FECINIATE > @Fecha
		AND (B.TIPHISPAC IN ('I','JM') OR (B.TIPHISPAC = 'N' AND ((RECORDANESTESIA IS NULL OR RECORDANESTESIA = 0)) AND (B.TIPHISPAC = 'N' AND (HCTELEFONICA IS NULL OR HCTELEFONICA = 0))) OR (B.TIPHISPAC = 'E' AND StoryType <> 2))
		
	)

	SELECT FechaHistoria, Information FROM CTE_DatosClinicosRelevantesHCOP ORDER BY FechaHistoria ASC
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera un resumen narrativo consolidado de los datos clínicos relevantes de la historia clínica de un paciente hospitalizado (HCOP), dado su código de paciente, número de ingreso y folio. Recorre y concatena en texto legible los distintos tipos de folio clínico registrados durante el ingreso: nota de ingreso, notas de evolución, informes quirúrgicos, notas de procedimientos, juntas médicas y evoluciones de urgencias, incluyendo para cada uno la fecha, tipo de historia, unidad funcional, motivo de consulta, enfermedad actual, signos vitales, examen físico, diagnósticos (CIE-10), análisis clínico, tratamiento instaurado (medicamentos y mezclas), especialidad médica y pronóstico. Integra información de las tablas de historia clínica (HCHISPACA, HCURGING1, HCQXINFOR), catálogos de unidades funcionales (INUNIFUNC), especialidades médicas (INESPECIA) y plantillas de HC (PRMODELOHC), y llama a funciones auxiliares para obtener signos vitales, examen físico, diagnósticos, medicamentos y mezclas concatenados por folio. Su propósito principal es alimentar la visualización clínica resumida del expediente del paciente para soporte a la decisión médica, asistentes clínicos o generación de resúmenes de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un resumen cronológico de los datos clínicos relevantes (historia inicial y evoluciones posteriores) de un paciente para un ingreso específico, formateado como texto para una historia clínica de origen prequirúrgico/HCOP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA con la combinación paciente/ingreso/folio recibida para poder obtener la fecha base (FECHISPAC); en caso contrario el filtro temporal queda sin valor.; El paciente y el ingreso deben tener historias clínicas asociadas en HCURGING1, HCURGEVO1 o HCNOTEVO1 vinculadas a HCHISPACA.; Las historias deben tener especialidad tratante (CODESPTRA) válida en INESPECIA y unidad funcional válida en INUNIFUNC, dado que los joins son INNER.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve la primera historia de ingreso (TOP 1, TIPHISPAC=''I'') del paciente/ingreso, ordenada por NUMEFOLIO ascendente.; Las historias posteriores incluidas son estrictamente posteriores (>) a la fecha del folio base, no iguales.; Se excluyen siempre del bloque de evoluciones las notas tipo ''N'' marcadas como registro de anestesia o telefónicas, y las evoluciones ''E'' con StoryType=2.; El texto de la historia siempre contiene fecha, tipo, unidad funcional y especialidad cuando aplica.; Cuando la historia tiene IDMODELOHC, prevalece el nombre del modelo sobre el formato derivado de TIPHISPAC.; El resultado final está siempre ordenado cronológicamente ascendente por FechaHistoria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Ingreso del paciente; Folio de historia; Nota de evolución; Junta médica; Informe quirúrgico (Informe QX); Hallazgo operativo; Registro de anestesia; Indicaciones telefónicas; Nota/Evolución de otros procedimientos; Motivo de consulta; Enfermedad actual; Signos vitales; Examen físico; Diagnósticos; Análisis; Tratamiento (medicamentos y mezclas/líquidos); Unidad funcional; Especialidad médica; Modelo de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] CTE_DatosClinicosRelevantesHCOP: Devuelve un resultset con FechaHistoria e Information ordenado ascendente por FechaHistoria, combinando la historia de ingreso (TIPHISPAC=''I'') previa al folio y todas las historias posteriores a la fecha del folio (FECINIATE > @Fecha).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si B.IDMODELOHC IS NOT NULL → Se usa el nombre del modelo (PRMODELOHC.NOMBRE) como descripción de la historia. else Se evalúa TIPHISPAC para determinar el formato del texto.; si TIPHISPAC = ''I'' y StoryType IN (12,13) → Se etiqueta la historia como ''Informe QX'' e incluye el hallazgo operativo (DESHALLOP). else Se etiqueta como ''Ingreso'' e incluye motivo de consulta, enfermedad actual, signos vitales, examen físico, diagnósticos, análisis y tratamiento.; si TIPHISPAC = ''N'' y RECORDANESTESIA = 1 → Se etiqueta como ''Registro de anestesia''.; si TIPHISPAC = ''N'' y HCTELEFONICA = 1 → Se etiqueta como ''Indicaciones telefónicas''.; si TIPHISPAC = ''N'' y HCOTROSPROC = 1 → Se etiqueta como ''Nota otros procedimientos'' con análisis y conclusión.; si TIPHISPAC = ''N'' y HCOTROSPROC = 2 → Se etiqueta como ''Evolución otros procedimientos'' con análisis y conclusión.; si TIPHISPAC = ''N'' y StoryType IN (12,13) → Se etiqueta como ''Informe QX'' con hallazgo operativo. else Se etiqueta como ''Nota evolución'' con diagnósticos, análisis y tratamiento.; si TIPHISPAC = ''E'' y StoryType <> 2 → Se etiqueta como ''Evolución'' con subjetivo, diagnósticos, análisis y tratamiento. else Se omite/no se genera contenido (caso StoryType = 2 queda vacío).; si TIPHISPAC = ''JM'' → Se etiqueta como ''Junta médica - Nota evolución'' con diagnósticos, análisis y tratamiento.; si Filtro de evoluciones (TIPHISPAC IN (''I'',''JM'') o N sin RECORDANESTESIA/HCTELEFONICA o E con StoryType<>2) y FECINIATE > @Fecha → Se incluye la historia en el resultado posterior al folio base. else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SignosVitalesPorFolioConcatenados; dbo.ExamenFisicoPorFolioConcatenados; dbo.DiagnosticoPorFolioConcatenados; dbo.MedicamentosPorFolioConcatenados; dbo.MezclasLiquidosPorFolioConcatenados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCURGING1; dbo.HCURGEVO1; dbo.HCNOTEVO1; dbo.INUNIFUNC; dbo.INESPECIA; dbo.PRMODELOHC; dbo.HCQXINFOR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarDatosClinicosRelevantesHCOP';
-- GO
