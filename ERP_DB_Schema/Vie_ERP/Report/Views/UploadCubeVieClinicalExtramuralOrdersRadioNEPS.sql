
--CREATE PROCEDURE  [dbo].[ODO_Ordenes_Extramurales_Radioterapia_NEPS]

--DECLARE	@ini_date DATE='2024-05-01';
--DECLARE @end_date DATE='2024-05-16';

--AS
--BEGIN
CREATE view [Report].[UploadCubeVieClinicalExtramuralOrdersRadioNEPS] as

SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	rrad.fechaconfir AS 'MES REPORTE',--[MesReporte]
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
		pac.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion]
		dxp.coddiagno AS [CIE_10],
		pron.codserips AS 'TIPO RADIOTERAPIA',--[TipoRadioterapia]
		rrad.numsesion AS 'NRO SESIONES ORDENADAS',--[NroSesionesOrdenadas]
		CASE orad.estado
			WHEN 1 THEN 'Seguimiento'
			WHEN 2 THEN 'Tratamiento activo'
			WHEN 3 THEN 'Tratamiento activo'
			WHEN 4 THEN 'Tratamiento activo'
			WHEN 5 THEN 'Finalizo tratamiento activo'
			WHEN 6 THEN 'Alta voluntaria / Fallecimiento / Abandono'
			WHEN 7 THEN 'Finalizo tratamiento activo'
			WHEN 8 THEN 'Tratamiento activo' END AS 'NOVEDAD',--[Novedad]
		CASE rrad.UBICACION
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
			'' AS 'OBSERVACIONES' ,--Observaciones
			orad.fechaplanea [FECHA BUSQUEDA],
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM dbo.hcradorden AS orad
INNER JOIN dbo.hcradesquemas AS rrad ON orad.id = rrad.idhcradorden 
INNER JOIN dbo.inpacient AS pac ON orad.ipcodpaci = pac.ipcodpaci 
INNER JOIN dbo.hcordpron AS pron ON orad.idhcordpron = pron.auto AND pron.manextpro = 1
INNER JOIN dbo.adingreso AS ing ON pron.ipcodpaci = ing.ipcodpaci AND pron.numingres = ing.numingres 
INNER JOIN dbo.indiagnop AS dxp ON ing.numingres = dxp.numingres AND dxp.coddiapri = 1
INNER JOIN contract.healthadministrator AS heal ON ing.genconentity = heal.id AND heal.id IN (61, 62, 63)
WHERE  YEAR(orad.fechaplanea )>=2022
--CAST(orad.fechaplanea AS DATE) BETWEEN @ini_date AND @end_date