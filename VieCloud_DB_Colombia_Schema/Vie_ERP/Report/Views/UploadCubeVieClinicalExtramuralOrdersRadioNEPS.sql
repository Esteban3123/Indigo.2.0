
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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes extramurales de radioterapia (desde 2022) para pacientes afiliados a entidades NEPS específicas, con datos demográficos, diagnóstico, novedad clínica y objetivo del tratamiento, para carga al cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de radioterapia debe tener al menos un esquema asociado (hcradesquemas); El paciente de la orden debe existir en inpacient; El pronóstico vinculado debe corresponder a manejo extramural (manextpro = 1); Debe existir ingreso administrativo coincidente con paciente y número de ingreso del pronóstico; El ingreso debe tener diagnóstico principal registrado (coddiapri = 1); La entidad administradora del ingreso debe pertenecer al conjunto NEPS con id 61, 62 o 63; La fecha planeada de la orden debe ser de año 2022 o posterior', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes con fecha planeada desde 2022 en adelante; Solo se incluyen pronósticos marcados como manejo extramural (manextpro = 1); Solo se considera el diagnóstico principal del ingreso (coddiapri = 1); El universo se restringe a tres administradoras de salud (id 61, 62, 63); El campo OBSERVACIONES se entrega siempre vacío; La marca de última actualización se calcula con la hora actual convertida a ''Pakistan Standard Time''; El ID_COMPANY corresponde al nombre de la base de datos en ejecución, truncado a 9 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radioterapia extramural; Órdenes oncológicas; Esquemas de radioterapia; Sesiones ordenadas; Diagnóstico CIE-10; Tipo de identificación del paciente; Novedad clínica del tratamiento; Objetivo terapéutico (neoadyuvancia, adyuvancia, curativo, paliativo, recaídas, metastásico); Administradora de salud (NEPS); Pronóstico de manejo extramural; Ingreso asistencial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalExtramuralOrdersRadioNEPS: Cuando se cumplen los joins y YEAR(orad.fechaplanea) >= 2022 y heal.id IN (61,62,63) y pron.manextpro = 1 y dxp.coddiapri = 1, se retorna una fila por esquema de radioterapia con tipificación de identificación, novedad y objetivo según catálogos numéricos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc en 1..12 → Mapea a códigos de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE) else NULL; si orad.estado en {1} → Novedad = ''Seguimiento'' else Estados 2,3,4,8 → ''Tratamiento activo''; 5,7 → ''Finalizo tratamiento activo''; 6 → ''Alta voluntaria / Fallecimiento / Abandono''; si rrad.UBICACION según catálogo (1..13, 55, 98) → Determina objetivo del tratamiento (neoadyuvancia, curativo, adyuvancia, paliativo, recaídas, metastásico, aseguramiento o no aplica) else NULL si valor fuera del catálogo; si heal.id IN (61, 62, 63) → Solo se incluyen ingresos cuya entidad administradora sea una de las tres NEPS objetivo else Se excluye', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcradorden; dbo.hcradesquemas; dbo.inpacient; dbo.hcordpron; dbo.adingreso; dbo.indiagnop; contract.healthadministrator', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersRadioNEPS';
GO
