
CREATE PROCEDURE [dbo].[SPHC_GenerarPromptDatosClinicosRelevantes]
(
@Paciente Varchar(25),
@Ingreso Varchar(20),
@Folio Varchar(10)
)
AS
BEGIN
    SET NOCOUNT ON

	declare @Fecha As DateTime = (SELECT FECHISPAC from dbo.HCHISPACA WHERE IPCODPACI = @Paciente AND NUMINGRES = @Ingreso AND NUMEFOLIO = @Folio)

	declare @FechaPre As DateTime
	SELECT @FechaPre = DATEADD(hour, -72, @Fecha);

	WITH CTE_DatosClinicosRelevantes AS (
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
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE END , ' - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Motivo de consulta: ', RTRIM(MOTCONSUL), CHAR(10), 'Enfemerdedad actual: ', rtrim(ENFACTUAL), CHAR(10), 'Análisis: ', rtrim(ANALISISP)) as Information
		from HCURGING1 A 
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		LEFT JOIN dbo.HCQXINFOR D with(nolock) ON A.IDETIPHIS=D.IDETIPHIS AND A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.NUMEFOLIO=D.NUMEFOLIO
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND B.TIPHISPAC = 'I' ORDER BY A.NUMEFOLIO ASC 

		union
		--Traemos historias desde las 72 horas anteriores a la fecha y hora del folio de la solicitud, hasta el momento actual
		select 
		A.FECINIATE As 'FechaHistoria',
				CASE 
					WHEN B.IDMODELOHC IS NULL THEN 
						(CASE B.TIPHISPAC
								WHEN 'I' THEN (
									case when StoryType IN (12, 13) then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Informe QX - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Hallazgo operativo: ', DESHALLOP) else concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Ingreso - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Motivo de consulta: ', RTRIM(MOTCONSUL), CHAR(10), 'Enfemerdedad actual: ', rtrim(ENFACTUAL), CHAR(10), 'Análisis: ', rtrim(ANALISISP)) end)
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de anestesia' when HCTELEFONICA = 1 then 'Indicaciones telefónicas' 
									when HCOTROSPROC = 1 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota otros procedimientos - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis y conclusión: ', ANALISISP) 
									when HCOTROSPROC = 2 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución otros procedimientos - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis y conclusión: ', ANALISISP) 
									when StoryType IN (12, 13) then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20), ' - Tipo historia: Informe QX - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Hallazgo operativo: ', DESHALLOP) else concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP) end )
								WHEN 'E' THEN (
									case when StoryType <> 2 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP) end)
								WHEN 'JM' THEN concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Junta médica - Nota evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP)
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS Information
		from HCURGING1 A 
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		LEFT JOIN dbo.HCQXINFOR D with(nolock) ON A.IDETIPHIS=D.IDETIPHIS AND A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.NUMEFOLIO=D.NUMEFOLIO
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND A.FECINIATE BETWEEN @FechaPre AND @Fecha
		AND (B.TIPHISPAC IN ('I','JM') OR (B.TIPHISPAC = 'N' AND (RECORDANESTESIA IS NULL OR RECORDANESTESIA = 0)) OR (B.TIPHISPAC = 'N' AND (HCTELEFONICA IS NULL OR HCTELEFONICA = 0)) OR (B.TIPHISPAC = 'E' AND StoryType <> 2))

		UNION
		
		select 
		A.FECINIATE As 'FechaHistoria',
				CASE 
					WHEN B.IDMODELOHC IS NULL THEN 
						(CASE B.TIPHISPAC
								WHEN 'I' THEN (case when StoryType IN (12, 13) then 'Informe QX' else 'Ingreso' end)
								WHEN 'N' Then (case when RECORDANESTESIA = 1 then 'Registro de anestesia' when HCTELEFONICA = 1 then 'Indicaciones telefónicas' when HCOTROSPROC = 1 then 'Nota otros procedimientos' when HCOTROSPROC = 2 then 'Evolución otros procedimientos' when StoryType IN (12, 13) then 'Informe QX' else 'Nota evolución' end )
								WHEN 'E' THEN (case when StoryType <> 2 then concat('Fecha historia: ', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP, CHAR(10), 'Subjetivo: ', SUBJETIVO) end)
								WHEN 'JM' THEN 'Junta médica - Nota evolución'
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS Information
		from HCURGEVO1 A
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND A.FECINIATE BETWEEN @FechaPre AND @Fecha
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
									when StoryType IN (12, 13) then concat('Fecha historia:', CONVERT(varchar(50), A.FECINIATE, 20), ' - Tipo historia: Informe QX - Unidad funcional: ',RTRIM(UFUDESCRI), CHAR(10), 'Hallazgo operativo: ', DESHALLOP) 
									else concat('Fecha historia:', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Nota evolución Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP) end )
								WHEN 'E' THEN (
									case when StoryType <> 2 then concat('Fecha historia :', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP) end)
								WHEN 'JM' THEN concat('Fecha historia :', CONVERT(varchar(50), A.FECINIATE, 20),  ' - Tipo historia: Junta médica - Nota evolución - Unidad funcional: ', RTRIM(UFUDESCRI), CHAR(10), 'Análisis: ', ANALISISP)
								ELSE 'Otro'
							END)
					WHEN B.IDMODELOHC IS NOT NULL THEN M.NOMBRE
				END AS Information
		from HCNOTEVO1 a 
		INNER JOIN dbo.INUNIFUNC C with(Nolock) ON A.UFUCODIGO=C.UFUCODIGO
		INNER JOIN dbo.HCHISPACA B with(Nolock) ON A.IDETIPHIS=B.IDETIPHIS AND A.IPCODPACI=B.IPCODPACI AND A.NUMINGRES=B.NUMINGRES AND A.NUMEFOLIO=B.NUMEFOLIO
		LEFT JOIN dbo.PRMODELOHC M with(nolock) ON M.ID = B.IDMODELOHC
		LEFT JOIN dbo.HCQXINFOR D with(nolock) ON A.IDETIPHIS=D.IDETIPHIS AND A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.NUMEFOLIO=D.NUMEFOLIO
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES= @Ingreso AND A.FECINIATE BETWEEN @FechaPre AND @Fecha
		AND (B.TIPHISPAC IN ('I','JM') OR (B.TIPHISPAC = 'N' AND ((RECORDANESTESIA IS NULL OR RECORDANESTESIA = 0)) AND (B.TIPHISPAC = 'N' AND (HCTELEFONICA IS NULL OR HCTELEFONICA = 0))) OR (B.TIPHISPAC = 'E' AND StoryType <> 2))
		)
		
		SELECT FechaHistoria, Information FROM CTE_DatosClinicosRelevantes ORDER BY FechaHistoria ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera un resumen narrativo de los datos clínicos relevantes de un paciente para un ingreso y folio específicos, orientado a alimentar un prompt de inteligencia artificial. Recopila la nota de ingreso inicial a la institución (primer folio de tipo ingreso), los informes quirúrgicos y todas las notas de evolución, juntas médicas y notas de procedimientos registradas en las 72 horas previas a la fecha del folio solicitado, extrayendo de cada historia el tipo de nota, la unidad funcional donde se atendió el paciente, el motivo de consulta, la enfermedad actual, el análisis clínico y los hallazgos operatorios en caso de cirugías. Combina información de la historia clínica general (HCHISPACA), el detalle clínico de urgencias y evolución (HCURGING1), el informe quirúrgico (HCQXINFOR), el catálogo de unidades funcionales (INUNIFUNC) y los modelos de plantilla de historia clínica (PRMODELOHC) para producir un texto estructurado y cronológico que sirva de contexto clínico al motor de IA. Recibe como parámetros la cédula o código del paciente, el número de ingreso y el número de folio de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un texto consolidado con la historia clínica de ingreso y todas las notas/evoluciones relevantes del paciente en una ventana de 72 horas previas al folio indicado, para ser usado como prompt de datos clínicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA que coincida con paciente, ingreso y folio indicados, ya que de allí se obtiene la fecha de referencia.; El paciente debe tener al menos una historia tipo ''I'' (Ingreso) asociada al ingreso para obtener el primer bloque del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ventana temporal de búsqueda siempre es exactamente 72 horas previas a la fecha del folio de referencia.; Los registros de tipo ''E'' (Evolución) con StoryType=2 nunca se incluyen como evolución textual.; Las notas tipo ''N'' que sean Registro de anestesia o Indicaciones telefónicas se excluyen del segundo, tercer y cuarto bloque por el filtro WHERE.; Cuando la historia tiene un IDMODELOHC, prevalece el nombre del modelo sobre la clasificación por TIPHISPAC.; El primer bloque siempre toma una sola historia (TOP 1) de tipo ''I'' ordenada por NUMEFOLIO ascendente.; El procedimiento solo lee datos; no realiza modificaciones en ninguna tabla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de historia clínica; Historia clínica de ingreso; Nota de evolución; Informe quirúrgico; Registro de anestesia; Indicaciones telefónicas; Junta médica; Unidad funcional; Motivo de consulta; Enfermedad actual; Análisis clínico; Hallazgo operatorio; Modelo de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna un conjunto con FechaHistoria e Information ordenado ascendentemente por FechaHistoria, uniendo: (1) primer registro tipo ''I'' del ingreso desde HCURGING1; (2) registros de HCURGING1 dentro de las 72h previas a la fecha del folio; (3) registros equivalentes desde HCURGEVO1; (4) registros equivalentes desde HCNOTEVO1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si B.IDMODELOHC IS NOT NULL → Se usa M.NOMBRE (nombre del modelo de HC) como descripción del tipo de historia. else Se clasifica el tipo de historia según B.TIPHISPAC (I, N, E, JM) y banderas auxiliares (StoryType, RECORDANESTESIA, HCTELEFONICA, HCOTROSPROC).; si TIPHISPAC=''I'' y StoryType IN (12,13) → El tipo se etiqueta como ''Informe QX'' e incluye ''Hallazgo operativo'' (DESHALLOP). else Se etiqueta como ''Ingreso'' e incluye motivo de consulta, enfermedad actual y análisis.; si TIPHISPAC=''N'' con RECORDANESTESIA=1 → Se etiqueta como ''Registro de anestesia''.; si TIPHISPAC=''N'' con HCTELEFONICA=1 → Se etiqueta como ''Indicaciones telefónicas''.; si TIPHISPAC=''N'' con HCOTROSPROC=1 → Se etiqueta como ''Nota otros procedimientos'' e incluye análisis y conclusión. else Si HCOTROSPROC=2, se etiqueta como ''Evolución otros procedimientos''.; si TIPHISPAC=''E'' y StoryType <> 2 → Se etiqueta como ''Evolución'' y se incluye Análisis (y Subjetivo en HCURGEVO1). else Las evoluciones con StoryType=2 quedan excluidas/no etiquetadas.; si TIPHISPAC=''JM'' → Se etiqueta como ''Junta médica - Nota evolución''.; si FECINIATE BETWEEN @FechaPre (Fecha-72h) AND @Fecha → Solo se incluyen historias dentro de la ventana de 72 horas previas a la fecha del folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCURGING1; dbo.HCURGEVO1; dbo.HCNOTEVO1; dbo.INUNIFUNC; dbo.PRMODELOHC; dbo.HCQXINFOR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_GenerarPromptDatosClinicosRelevantes';
-- GO
