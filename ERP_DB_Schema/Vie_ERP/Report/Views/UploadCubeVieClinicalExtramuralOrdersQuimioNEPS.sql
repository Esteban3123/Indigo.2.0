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