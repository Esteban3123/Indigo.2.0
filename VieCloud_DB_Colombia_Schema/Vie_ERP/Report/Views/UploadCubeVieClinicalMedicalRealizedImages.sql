
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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida órdenes de imágenes diagnósticas (hospitalarias y ambulatorias) realizadas, procesadas o interpretadas desde 2023 con datos de paciente, EPS, médicos, diagnósticos y CUPS para alimentar el cubo de reportes clínicos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener estserips en (2,3,4): Estudio Realizado, Imagen Procesada o Estudio Interpretado.; La fecha de recepción del examen (fecrecexa) debe ser >= 2023-01-01.; El paciente debe existir en inpacient y el ingreso en adingreso (joins INNER).; El ingreso debe tener entidad administradora de salud y grupo de atención válidos en contract.healthadministrator y contract.caregroup.; El centro de atención y la unidad funcional referenciados deben existir.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes en estados 2,3,4 (estudios efectivamente ejecutados o interpretados); órdenes solicitadas, anuladas, remitidas o extramurales quedan excluidas.; Solo se incluyen órdenes con fecrecexa desde 2023-01-01 en adelante.; Las órdenes provenientes de ambordima siempre se clasifican como AMBULATORIO.; Por cada combinación numingres+numefolio se selecciona un único diagnóstico secundario (el más antiguo por fecdiagno).; Diagnóstico principal se identifica por coddiapri=1 y secundario por coddiapri=0.; La diferencia en días se calcula entre la fecha de orden médica y la fecha de transcripción.; ULT_ACTUAL se reporta en zona horaria ''Pakistan Standard Time''.; ID_COMPANY corresponde al nombre de la base de datos actual truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de imágenes diagnósticas; Ingreso hospitalario/ambulatorio; EPS / entidad administradora de salud; Régimen de afiliación; Grupo de atención; Tipo de liquidación contractual (PGP, capitación); Centro de atención; Unidad funcional; Médico ordenador; Médico que realiza; Estado de la orden de servicio; Diagnóstico principal y secundario (CIE); Clasificación TNM oncológica; Procedimiento CUPS; Ámbito de prestación (ambulatorio/hospitalario)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalRealizedImages: Devuelve un set distinct uniendo hcordimag y ambordima filtrado por estserips IN (2,3,4) y fecrecexa>=''2023-01-01'', enriquecido con paciente, ubicación, EPS, médicos y diagnósticos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si origen de la orden: hcordimag vs ambordima → hcordimag aporta tipoorden según manextpro (1=AMBULATORIO, 0=HOSPITALARIO) y conserva numefolio else ambordima fija tipoorden=''AMBULATORIO'' y numefolio=''''; si indiagnoh.coddiapri = 1 → se toma como diagnóstico principal del folio else si coddiapri = 0 entra al CTE dxsecundario y se selecciona el primero por fecdiagno (ROW_NUMBER=1) como diagnóstico secundario; si iptipodoc del paciente → se mapea a etiqueta de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si entitytype de la EPS → se traduce a régimen (Contributivo, Subsidiado, ARL, Medicina Prepagada, IPS, Régimen Especial, Fosyga, etc.); si liquidationtype del grupo de atención → se traduce a tipo de liquidación (Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP); si estserips de la orden → se traduce a estado (Solicitado, Estudio Realizado, Imagen Procesada, Estudio Interpretado, Remitido, Anulado, Extramural); si tipdiagno del diagnóstico principal → I=Impresión Diagnóstica, C=Confirmado Nuevo, R=Confirmado Repetido', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcordimag; dbo.ambordima; dbo.indiagnoh; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.indiagnos; contract.cupsentity', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRealizedImages';
GO
