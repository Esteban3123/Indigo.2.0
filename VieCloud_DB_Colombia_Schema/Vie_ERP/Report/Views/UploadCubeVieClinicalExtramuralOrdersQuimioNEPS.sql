CREATE view [Report].[UploadCubeVieClinicalExtramuralOrdersQuimioNEPS] as

WITH CTE_Tableordenciclo
AS
(
 SELECT DISTINCT
	cqx.fecharegistro
	,cqx.idhcordquimio
	,cqx.schemesid
	,cqx.ciclo
	,mqx.dia
FROM ehr.hcordciclos AS cqx 
INNER JOIN ehr.hcordmedicam AS mqx ON cqx.schemesid = mqx.schemesid AND cqx.idhcordquimio = mqx.idhcordquimio AND cqx.ciclo = mqx.ciclo
WHERE year(cqx.fecharegistro)>=2022
)

SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	 cqx.fecharegistro AS 'MES REPORTE',--[MesReporte]
	 CASE pac.iptipodoc 
		WHEN 1 THEN 'CC'
		WHEN 2 THEN 'CE'
		WHEN 3 THEN 'TI'
		WHEN 4 THEN 'RC'
		WHEN 5 THEN 'PA'
		WHEN 6 THEN 'AS'
		WHEN 7 THEN 'MS'
		WHEN 8 THEN 'NU'
		WHEN 9 THEN 'CN'
		WHEN 10 THEN 'CD'
		WHEN 11 THEN 'SC' 
		WHEN 12 THEN 'PE' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion]
		pac.ipcodpaci AS 'NRO IDENTIFICACION',-- [NroIdentificacion]
		dxp.coddiagno AS [CIE-10],
		obj.description AS 'TIPO QUIMIOTERAPIA',--[TipoQuimioterapia] -- Pendiente
		COUNT(cqx.dia) AS 'NRO FASES ORDENADAS',--[NroFasesOrdenadas] -- Pendiente
		CASE oqx.estado
			WHEN 2 THEN 'Tratamiento activo'
			WHEN 3 THEN 'Finalizo tratamiento activo'END AS 'NOVEDAD',--[Novedad]
		CASE oqx.[48]
			WHEN 1 THEN 'Neoadyuvancia (manejo  inicial prequirUrgico)'
			WHEN 2 THEN 'Tratamiento inicial curativo'
			WHEN 3 THEN 'Adyuvancia (manejo inicial postquirurgico)'
			WHEN 4 THEN 'Manejo paliativo'
			WHEN 5 THEN 'Tratamiento curativo de recaidas'
			WHEN 6 THEN 'Manejo paliativo'
			WHEN 7 THEN 'Tratamiento curativo de recaidas'
			WHEN 8 THEN 'Manejo paliativo'
			WHEN 9 THEN 'Tratamiento curativo de recaidas'
			WHEN 10 THEN 'Manejo paliativo'
			WHEN 11 THEN 'Manejo de recaida'
			WHEN 12 THEN 'Manejo de enfermedad metastasica'
			WHEN 13 THEN 'Manejo paliativo (Sin majejo de caida y enfermedad metastasica)'
			WHEN 55 THEN 'Persona con aseguramiento (Regimen subsidiado o contributivo y que no son PPNA)'
			WHEN 98 THEN 'No aplica' END AS 'OBJETIVO QUIMIOTERAPIA ACTUAL',--[ObjetivoQuimioterapiaActual]
			'' AS 'OBSERVACIONES',--Observaciones
			cqx.fecharegistro [FECHA BUSQUEDA],
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM CTE_Tableordenciclo AS cqx
INNER JOIN ehr.hcordquimio AS oqx ON cqx.schemesid = oqx.schemesid AND cqx.idhcordquimio = oqx.id AND oqx.manejoexterno = 1
INNER JOIN ehr.schemes AS sch ON oqx.schemesid = sch.id 
INNER JOIN Report.Tableobjquimio AS obj ON sch.serviceips = obj.serviceips
INNER JOIN dbo.inpacient AS pac ON oqx.ipcodpaci = pac.ipcodpaci
INNER JOIN dbo.adingreso AS ing ON oqx.ipcodpaci = ing.ipcodpaci AND oqx.numingres = ing.numingres 
INNER JOIN contract.healthadministrator AS heal ON ing.genconentity = heal.id AND heal.id IN (61, 62, 63)
INNER JOIN dbo.indiagnop AS dxp ON ing.numingres = dxp.numingres AND dxp.coddiapri = 1
WHERE oqx.estado IN (2, 3) 
GROUP BY cqx.fecharegistro, pac.iptipodoc, pac.ipcodpaci, dxp.coddiagno, obj.description, oqx.estado, oqx.[48]

UNION

/*
 * Esquemas en estado 4, cuyo motivo es 3 - 5
*/
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	 cqx.fecharegistro AS [Mes de Reporte]
	,CASE pac.iptipodoc 
		WHEN 1 THEN 'CC'
		WHEN 2 THEN 'CE'
		WHEN 3 THEN 'TI'
		WHEN 4 THEN 'RC'
		WHEN 5 THEN 'PA'
		WHEN 6 THEN 'AS'
		WHEN 7 THEN 'MS'
		WHEN 8 THEN 'NU'
		WHEN 9 THEN 'CN'
		WHEN 10 THEN 'CD'
		WHEN 11 THEN 'SC' 
		WHEN 12 THEN 'PE' END AS [Tipo Identificacion]
		,pac.ipcodpaci AS [N° Identificacion]
		,dxp.coddiagno AS [CIE-10]
		,obj.description AS [Tipo de Quimioterapia] -- Pendiente
		,COUNT(cqx.dia) AS [N° Fases Ordenadas] -- Pendiente
		,CASE oqx.motivofinalizar
			WHEN 3 THEN 'Fallecimiento'
			WHEN 5 THEN 'Alta Voluntaria' END AS [Novedad]
		,CASE oqx.[48]
			WHEN 1 THEN 'Neoadyuvancia (manejo  inicial prequirUrgico)'
			WHEN 2 THEN 'Tratamiento inicial curativo'
			WHEN 3 THEN 'Adyuvancia (manejo inicial postquirurgico)'
			WHEN 4 THEN 'Manejo paliativo'
			WHEN 5 THEN 'Tratamiento curativo de recaidas'
			WHEN 6 THEN 'Manejo paliativo'
			WHEN 7 THEN 'Tratamiento curativo de recaidas'
			WHEN 8 THEN 'Manejo paliativo'
			WHEN 9 THEN 'Tratamiento curativo de recaidas'
			WHEN 10 THEN 'Manejo paliativo'
			WHEN 11 THEN 'Manejo de recaida'
			WHEN 12 THEN 'Manejo de enfermedad metastasica'
			WHEN 13 THEN 'Manejo paliativo (Sin majejo de caida y enfermedad metastasica)'
			WHEN 55 THEN 'Persona con aseguramiento (Regimen subsidiado o contributivo y que no son PPNA)'
			WHEN 98 THEN 'No aplica' END AS [Objetivo de Quimioterapia Actual]
			,'' AS Observaciones,
			cqx.fecharegistro [FECHA BUSQUEDA],
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM CTE_Tableordenciclo AS cqx
INNER JOIN ehr.hcordquimio AS oqx ON cqx.schemesid = oqx.schemesid AND cqx.idhcordquimio = oqx.id  AND oqx.manejoexterno = 1
INNER JOIN ehr.schemes AS sch ON oqx.schemesid = sch.id 
INNER JOIN Report.Tableobjquimio AS obj ON sch.serviceips = obj.serviceips
INNER JOIN dbo.inpacient AS pac ON oqx.ipcodpaci = pac.ipcodpaci
INNER JOIN dbo.adingreso AS ing ON oqx.ipcodpaci = ing.ipcodpaci AND oqx.numingres = ing.numingres 
INNER JOIN contract.healthadministrator AS heal ON ing.genconentity = heal.id AND heal.id IN (61, 62, 63)
INNER JOIN dbo.indiagnop AS dxp ON ing.numingres = dxp.numingres AND dxp.coddiapri = 1
WHERE oqx.estado IN (4) AND oqx.motivofinalizar IN (3, 5) 
GROUP BY cqx.fecharegistro, pac.iptipodoc, pac.ipcodpaci, dxp.coddiagno, obj.description, oqx.motivofinalizar, oqx.[48];
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a la carga de un cubo analítico con órdenes extramurales de quimioterapia (manejo externo) para pacientes afiliados a tres entidades administradoras de salud específicas (IDs 61, 62, 63), registradas desde 2022. Consolida por paciente el diagnóstico primario CIE-10, el tipo y objetivo de quimioterapia, el número de fases ordenadas por ciclo y la novedad del esquema (tratamiento activo, finalizado, fallecimiento o alta voluntaria), uniendo dos conjuntos según el estado del esquema.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida para cubo de reporte NEPS las órdenes de quimioterapia extramural (manejo externo) de pacientes afiliados a aseguradoras 61/62/63, mostrando ciclos, diagnóstico CIE-10, objetivo y novedad del tratamiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de quimioterapia deben tener manejoexterno = 1 (manejo extramural).; El ingreso del paciente debe estar asociado a una entidad administradora de salud cuyo id sea 61, 62 o 63.; El diagnóstico considerado es solo el principal del ingreso (indiagnop.coddiapri = 1).; Solo se consideran ciclos con fecharegistro de año >= 2022.; Debe existir relación entre ehr.hcordciclos y ehr.hcordmedicam por schemesid, idhcordquimio y ciclo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY siempre se reporta como el nombre de la base de datos actual truncado a 9 caracteres.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El conteo ''NRO FASES ORDENADAS'' agrupa por paciente/diagnóstico/objetivo/estado y cuenta días de medicamento del ciclo.; Solo se incluyen pacientes cuyo ingreso pertenece a las EPS/administradoras con id 61, 62 o 63.; El campo Observaciones siempre se devuelve vacío.; Se excluyen órdenes con manejoexterno distinto de 1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia extramural; Ciclos de quimioterapia; Esquema terapéutico; Objetivo de quimioterapia (neoadyuvancia, adyuvancia, paliativo, curativo, recaída, metastásico); Diagnóstico principal CIE-10; Tipo de identificación del paciente; Administradora de salud (EPS); Novedad de tratamiento (activo, finalizado, fallecimiento, alta voluntaria); Reporte NEPS / cubo de reporte', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas combinadas (UNION) de órdenes activas/finalizadas (estado 2 o 3) y órdenes finalizadas por fallecimiento o alta voluntaria (estado 4 con motivofinalizar 3 o 5).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si oqx.estado IN (2,3) → Mapea Novedad: 2→''Tratamiento activo'', 3→''Finalizo tratamiento activo''. else Se evalúa la rama de estado 4.; si oqx.estado = 4 AND oqx.motivofinalizar IN (3,5) → Mapea Novedad: 3→''Fallecimiento'', 5→''Alta Voluntaria''.; si CASE pac.iptipodoc 1..12 → Traduce código de tipo de documento a sigla (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE).; si CASE oqx.[48] (1..13, 55, 98) → Traduce código de objetivo de quimioterapia a etiqueta clínica (Neoadyuvancia, Adyuvancia, Manejo paliativo, Tratamiento curativo, Manejo de recaída, Enfermedad metastásica, etc.).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ehr.hcordciclos; ehr.hcordmedicam; ehr.hcordquimio; ehr.schemes; Report.Tableobjquimio; dbo.inpacient; dbo.adingreso; contract.healthadministrator; dbo.indiagnop', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersQuimioNEPS';
GO
