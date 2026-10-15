
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