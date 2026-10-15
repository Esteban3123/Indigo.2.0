
--CREATE PROCEDURE  [dbo].[ODO_Procedimientos_imagenologia_realizados]

--DECLARE	@ini_date DATE='2024-06-01';
--DECLARE @end_date DATE='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalMedicalRealizedImages] AS

WITH imgordenes AS
(
SELECT 
	ordi.ipcodpaci,
	ordi.codcenate,
	ordi.ufucodigo AS ufservicio,
	CASE ordi.manextpro WHEN 1 THEN 'AMBULATORIO' WHEN 0 THEN 'HOSPITALARIO' END AS tipoorden,
	ordi.codprosal AS medordena,
	ordi.fecordmed,
	ordi.estserips,
	ordi.medrealec AS medrealiza,
	ordi.fecrecexa,
	ordi.fectraser,
	ordi.fecvalser,
	ordi.fechlect,
	ordi.codserips,
	ordi.canserips,
	ordi.numingres,
	ordi.numefolio
FROM dbo.hcordimag AS ordi
WHERE ordi.estserips IN (2, 3, 4) AND CAST(ordi.fecrecexa AS DATE)>='2023-01-01'
--CAST(ordi.fecrecexa AS DATE) BETWEEN @ini_date AND @end_date

UNION ALL

SELECT 
	ambi.ipcodpaci,
	ambi.codcenate,
	ambi.ufucodigo AS ufservicio,
	'AMBULATORIO' AS tipoorden,
	ambi.codprosal AS medordena,
	ambi.fecordmed,
	ambi.estserips,
	ambi.medrealec AS medrealiza,
	ambi.fecrecexa,
	ambi.fectraser,
	ambi.fecvalser,
	ambi.fechlect,
	ambi.codserips,
	ambi.canserips,
	ambi.numingres,
	'' AS numefolio
FROM dbo.ambordima AS ambi
WHERE ambi.estserips IN (2, 3, 4) AND CAST(ambi.fecrecexa AS DATE)>='2023-01-01'
--CAST(ambi.fecrecexa AS DATE) BETWEEN @ini_date AND @end_date
),

dxsecundario AS
(
	SELECT
		img.numingres,
		img.numefolio,
		dxs.coddiagno,
		ROW_NUMBER() OVER (PARTITION BY img.numingres + img.numefolio ORDER BY dxs.fecdiagno) AS number
	FROM imgordenes AS img 
	INNER JOIN dbo.indiagnoh AS dxs WITH (NOLOCK) ON img.numingres = dxs.numingres AND img.numefolio = dxs.numefolio AND dxs.coddiapri = 0
)



SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
	pac.ipcodpaci AS 'NUMERO IDENTIFICACION',--[NumeroIdentificacion],
	RTRIM(pac.ipnomcomp) AS 'NOMBRE',--[Nombre],
	CASE pac.ipsexopac WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS 'SEXO',--[Sexo],
	pac.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
	mun.munnombre AS 'MUNICIPIO',--[Municipio],
	dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
	entc.code AS 'CODIGO EPS',--[CodigoEPS],
	entc.name 'NOMBRE EPS',--[NombreEPS],
	CASE entc.entitytype
		WHEN 1 THEN 'EPS Contributivo'
		WHEN 2 THEN 'EPS Subsidiado'
		WHEN 3 THEN 'ET Vinculados Municipios'
		WHEN 4 THEN 'ET Vinculados Departamentos'
		WHEN 5 THEN 'ARL Riesgos Laborales'
		WHEN 6 THEN 'MP Medicina Prepagada'
		WHEN 7 THEN 'IPS Privada'
		WHEN 8 THEN 'IPS Publica'
		WHEN 9 THEN 'Regimen Especial'
		WHEN 10 THEN 'Accidentes de transito'
		WHEN 11 THEN 'Fosyga'
		WHEN 12 THEN 'Otros' END AS 'REGIMEN EPS',--[RegimenEPS],
	grpc.code AS 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
	grpc.name 'NOMBRE GRUPO ATENCION',--[NombreGrupoAtencion],
	CASE grpc.liquidationtype
		WHEN 1 THEN 'Pago por Servicios'
		WHEN 2 THEN 'Capitacion'
		WHEN 3 THEN 'Factura Global'
		WHEN 4 THEN 'Capitacion Global'
		WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],
	ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
	uf.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
	medo.nommedico AS 'MEDICO ORDENA',--[MedicoOrdena],
	imgord.fecordmed AS 'FECHA ORDENAMIENTO',--[FechaOrdenamiento],
	CASE imgord.estserips 
		WHEN 1 THEN 'Solicitado'
		WHEN 2 THEN 'Estudio Realizado'
		WHEN 3 THEN 'Imagen Procesada'
		WHEN 4 THEN 'Estudio Interpretado'
		WHEN 5 THEN 'Remitido'
		WHEN 6 THEN 'Anulado'
		WHEN 7 THEN 'Extramural' END AS 'ESTADO ORDEN',--[EstadoOrden],
	imgord.fecrecexa AS 'FECHA TOMA IMAGEN',--[FechaTomaImagen],
	medr.nommedico AS 'MEDICO REALIZA',--[MedicoRealiza],
	imgord.tipoorden AS 'AMBITO PRESTACION',--[AmbitoPrestacion],
	imgord.fectraser AS 'FECHA TRASCRIPCION',--[FechaTrascripcion],
	imgord.fecvalser AS  'FECHA VALIDACION TRASCRIPCION',--[FechaValidacionTrascripcion],
	imgord.fechlect AS 'FECHA LECTURA',--[FechaLectura],
	DATEDIFF(DAY, imgord.fecordmed, imgord.fectraser) AS 'DIFERENCIA DIAS',--[DiferenciaDias],
	ing.numingres AS 'NRO INGRESO',--[NroIngreso],
	ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
	dx1.coddiagno AS 'CODIGO DIAGNOSTICO PRINCIPAL',--[CodigoDiagnosticoPrincipal],
	dx1.nomdiagno AS 'NOMBRE DIAGNOSTICO PRINCIPAL',--[NombreDiagnosticoPrincipal],
	CASE dxp.tipdiagno 
		WHEN 'I' THEN 'Impresion Diagnostica' 
		WHEN 'C' THEN 'Confirmado Nuevo' 
		WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
	dxp.t1 + dxp.n1 + dxp.m1 AS TNM,
	dx2.coddiagno AS 'CODIGO DIAGNOSTICO SECUNDARIO',--[CodigoDiagnosticoSecundario],
	dx2.nomdiagno AS 'NOMBRE DIAGNOSTICO SECUNDARIO',--[NombreDiagnosticoSecundario],
	cups.code AS 'CODIGO CUPS',--[CodigoCUPS],
	cups.[description] AS 'DESCRIPCION CUPS',--[DescripcionCUPS],
	imgord.canserips AS 'NUMERO ESTUDIO',--[NumeroEstudios],
	CAST(imgord.fecrecexa AS DATE) [FECHA BUSQUEDA],
    CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM imgordenes AS imgord 
INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON imgord.ipcodpaci = pac.ipcodpaci
LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON imgord.numingres = ing.numingres
INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON imgord.codcenate = ca.codcenate 
INNER JOIN dbo.inunifunc AS uf WITH (NOLOCK) ON imgord.ufservicio = uf.ufucodigo 
LEFT JOIN dbo.inprofsal AS medo WITH (NOLOCK) ON imgord.medordena = medo.codprosal 
LEFT JOIN dbo.inprofsal AS medr WITH (NOLOCK) ON imgord.medrealiza = medr.codprosal 
LEFT JOIN dbo.indiagnoh AS dxp WITH (NOLOCK) ON  imgord.numingres = dxp.numingres AND imgord.numefolio = dxp.numefolio AND coddiapri = 1
LEFT JOIN dbo.indiagnos AS dx1 WITH (NOLOCK) ON dxp.coddiagno = dx1.coddiagno
LEFT JOIN dxsecundario AS dxs WITH (NOLOCK) ON imgord.numingres = dxs.numingres AND imgord.numefolio = dxs.numefolio AND dxs.number = 1
LEFT JOIN dbo.indiagnos AS dx2 WITH (NOLOCK) ON dxs.coddiagno = dx2.coddiagno
LEFT JOIN contract.cupsentity AS cups WITH (NOLOCK) ON imgord.codserips = cups.code