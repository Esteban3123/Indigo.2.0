
--CREATE PROCEDURE [dbo].[ODO_Mezclas_Liquidos]
	-- Add the parameters for the stored procedure here
--	@inidate DATE,
--	@enddate DATE

CREATE view [Report].[UploadCubeVieClinicalMedicalLiquidMixing] AS

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
			WHEN 12 THEN 'PE' 
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		pac.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		pac.ipnomcomp AS 'NOMBRE',--[Nombre],
		ce.name AS 'ENTIDAD ADMINSTRADORA',--[EntidadAdministradora],
		gp.name AS 'GRUPO ATENCION',--[GrupoAtencion],
		ing.numingres AS 'NRO INGRESO',--[NroIngreso],
		CONVERT(VARCHAR(10), ing.ifechaing, 103) AS 'FECHA INGRESO',--[FechaIngreso],
		RTRIM(ca.nomcenate) AS 'CENTRO ATENCION',--[CentroAtencion], 
		RTRIM(uf.ufudescri) AS 'UNIAD FUNCIONAL',--[UnidadFuncional], 
		CASE mez.cabestado 
			WHEN 1 THEN 'Aplicado' 
			WHEN 2 THEN 'Completado' 
			WHEN 3 THEN 'Descartado/Suspendido' 
			WHEN 4 THEN 'Sin Aplicar/pendiente' END AS 'ESTADO',--[Estado],
		med.nommedico AS 'PROFESIONAL',--[Profesional],
		CONVERT(VARCHAR(10), mez.fecaplmed, 103) AS 'FECHA APLICACION',--[FechaAplicacion],
		mez.nommezcla AS 'MEZCLA LIQUIDO',-- [MezclaLiquidos],
		mez.indaplmed AS 'ADMINISTRACION',--[Administracion],
		atc.code AS 'CODIGO PRODUCTO',--[CodigoProducto],
		RTRIM(atc.name) AS 'NOMBRE PRODUCTO',--[NombreProducto],
		mezd.cantiutil AS 'CANTIDAD',--[Cantidad],
		CONVERT(VARCHAR(5), mez.fecaplmed, 108) AS 'HORA',--[Hora]
		CAST(mez.fecaplmed AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hchojmezc AS mez
	INNER JOIN dbo.hchojmezd AS mezd ON mez.consecuti = mezd.consecuti
	INNER JOIN inventory.atc AS atc ON mezd.codproduc = atc.code
	INNER JOIN dbo.inprofsal AS med ON mez.codproapl = med.codprosal 
	INNER JOIN dbo.adcenaten AS ca ON mez.codcenate = ca.codcenate
	INNER JOIN dbo.inunifunc AS uf ON mez.ufucodigo = uf.ufucodigo
	INNER JOIN dbo.inpacient AS pac ON mez.ipcodpaci = pac.ipcodpaci
	INNER JOIN dbo.adingreso AS ing ON mez.numingres = ing.numingres
	INNER JOIN contract.healthadministrator AS ce ON ing.genconentity = ce.id
	INNER JOIN contract.caregroup AS gp ON ing.gencaregroup = gp.id
	WHERE CAST(mez.fecaplmed AS DATE)>='2024-01-01'
	--CAST(mez.fecaplmed AS DATE) BETWEEN @inidate AND @enddate