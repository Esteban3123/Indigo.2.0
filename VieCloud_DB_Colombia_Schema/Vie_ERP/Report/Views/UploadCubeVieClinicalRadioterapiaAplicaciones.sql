

CREATE view [Report].[UploadCubeVieClinicalRadioterapiaAplicaciones] AS

WITH ordenes
AS 
(

SELECT DISTINCT	ord.ipcodpaci,	ord.id,	sips.codserips, 	sips.desserips
FROM dbo.hcraddosis AS radd
INNER JOIN dbo.hcradesquemas AS rade WITH (NOLOCK) ON radd.idhcradesquemas = rade.id
INNER JOIN dbo.hcradorden ord WITH (NOLOCK) ON rade.idhcradorden = ord.id
INNER JOIN dbo.incupsips AS sips WITH (NOLOCK) ON ord.codserips = sips.codserips AND (sips.desserips LIKE '%TELETER%' OR sips.desserips LIKE '%RADIOCIRUGIA%') 
--WHERE CAST(radd.fecharegistro AS DATE) BETWEEN @ini_date AND @end_date

), detalleordenes AS
(

SELECT
	orden.id,orden.ipcodpaci,orden.codserips, orden.desserips,deto.codcenate,deto.fechaorden,deto.fechasimula,deto.fechaplanea,deto.estado,	deto.codprosal,	deto.coddiagno,
	rade.usuconfir,rade.dostotal,rade.dosisporsesion,rade.numsesion,rade.fechacrea,rade.numingres,radd.dosistumor1,radd.dosistumor2,radd.dosistumor3,radd.dosistumor4,
	radd.dosistumor5,radd.dosistumor6,radd.totaldosis,radd.observacion,radd.fecharegistro,radd.usuarioregistro,radd.codcenatedosis,
	ROW_NUMBER() OVER(PARTITION BY rade.id + rade.id  ORDER BY radd.fecharegistro ASC) AS nsesion,pron.numingres AS numingresord, pron.coddiagno AS coddiagnoord
FROM dbo.hcraddosis AS radd 
INNER JOIN dbo.hcradesquemas AS rade WITH (NOLOCK) ON radd.idhcradesquemas = rade.id 
INNER JOIN dbo.hcradorden AS deto WITH (NOLOCK) ON rade.idhcradorden = deto.id
INNER JOIN ordenes AS orden WITH (NOLOCK) ON  deto.id = orden.id
INNER JOIN dbo.hcordpron AS pron ON deto.idhcordpron = pron.auto
), primeraplicacion AS 
(

SELECT id, usuarioregistro, codcenatedosis FROM detalleordenes WHERE nsesion = 1

)

SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
	pac.ipcodpaci AS 'NRO DE IDENTIFICACION',-- [NroIdentificacion],
	RTRIM(pac.ipnomcomp) AS 'NOMBRE',-- [Nombre],
	CAST(pac.ipfecnaci  AS DATE) AS 'FECHA NACIMIENTO',-- [FechaNacimiento],
	mun.munnombre AS 'MUNICIPIO',--[Municipio],
	dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
	entc.code AS 'CODIGO EPS',--[CodEPS],
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
		WHEN 99 THEN 'Particulares'
		WHEN 12 THEN 'Otros' ELSE 'Otros' END AS 'REGIMEN',--[RegimenEPS],
	grpc.code AS 'CODIGO GRUPO ATENCION',--[CodGrupoAtencion],
	grpc.name 'GRUPO ATENCION',--[GrupoAtencion],
	CASE grpc.liquidationtype
		WHEN 1 THEN 'Pago por Servicios'
		WHEN 2 THEN 'Capitacion'
		WHEN 3 THEN 'Factura Global'
		WHEN 4 THEN 'Capitacion Global'
		WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],
	munca.munnombre AS 'CIUDAD',--[Ciudad],
	ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
	RTRIM(dx.coddiagno) + ' - ' + dx.nomdiagno AS 'DIAGNOSTICO',-- [Diagnostico],
	CASE dxp.tipdiagno 
		WHEN 'I' THEN 'Impresion Diagnostica' 
		WHEN 'C' THEN 'Confirmado Nuevo' 
		WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
	(SELECT CAST(MAX([23]) AS DATE) FROM dbo.hconcopreg WHERE [6] = dosis.ipcodpaci AND [17] = dosis.coddiagno) AS 'FECHA PATOLOGIA',--[FechaPatologia], 
	CAST(dosis.fechaorden AS DATE) AS 'FECHA ORDENAMIENTO',--[FechaOrdenamiento],
	prof.nommedico AS 'PROFESIONAL ORDENO',--[ProfesionalOrdeno], 
	esp.desespeci AS 'ESPECIALIDAD',--[Especialidad],
	CASE dosis.estado
		WHEN 1 THEN 'Solicitado' 
		WHEN 2 THEN 'Simulacion Programada (aplica solo para radioterapias)' 
		WHEN 3 THEN 'Planeacion Programada (aplica solo para radioterapias)' 
		WHEN 4 THEN 'Planeacion Confirmada (aplica solo para radioterapias)' 
		WHEN 5 THEN 'Finalizado' 
		WHEN 6 THEN 'Anulado' 
		WHEN 7 THEN 'Completado' 
		WHEN 8 THEN 'Braquiterapia Iniciada (cuando se ha aplicado la primer dosis desde dashboard de Braquiterapia)' END AS 'ESTADO',--[Estado],
	dosis.codserips AS 'CUPS',--CUPS,
	dosis.desserips AS 'DESCRIPCION CUPS',--[DescripcionCUPS],
	1 AS 'CANTIDAD',--Cantidad,
	ing.iautoriza AS 'NRO AUTORIZACION',--[NroAutorizacion],
	CAST(dosis.fechasimula AS DATE) AS 'FECHA SIMULACION',--[FechaSimulacion],
	CAST(dosis.fechaplanea AS DATE) AS 'FECHA PLANEACION',--[FechaPlaneacion],
	medp.nomusuari AS 'PROFESIONAL REALIZA PLANEACION',--[ProfesionalRealizaPlaneacion],
	CAST(dosis.fechacrea AS DATE) AS 'FECHA ESQUEMA',--[FechaEsquema],
	dosis.dostotal AS 'DOSIS TOTAL',--[DosisTotal],
	dosis.dosisporsesion AS 'DOSIS SESION',--[DosisSesion],
	dosis.numsesion AS 'NRO SESIONES',--[NroSesiones],
	dosis.nsesion AS 'NRO SESION',--[NroSesion],
	CAST(dosis.fecharegistro AS DATE) AS 'FECHA APLICACION',--[FechaAplicacion],
	dosis.observacion AS 'OBSERVACION',--[Observacion],
	usua.nomusuari AS 'PROFESIONAL APLICO',--[ProfesionalAplico],
	caap.nomcenate AS 'CENTRO ATENCION APLICO',--[CentroAtencionAplicacion],
	pusu.nomusuari AS 'PROFESIONAL APLICO PRIMERA DOSIS',--[ProfesionalAplicoPrimerDosis],
	capa.nomcenate AS 'CENTRO ATENCION PRIMER DOSIS',--[CentroAtencionPrimerDosis],
	dosis.dosistumor1 AS 'DOSIS TURNO UNO',--[DosisTumorUno],
	dosis.dosistumor2 AS 'DOSIS TURNO DOS',--[DosisTumorDos],
	dosis.dosistumor3 AS 'DOSIS TURNO TRES',--[DosisTumorTres],
	dosis.dosistumor4 AS 'DOSIS TURNO CUATRO',--[DosisTumorCuatro],
	dosis.dosistumor5 AS 'DOSIS TURNO CINCO',--[DosisTumorCinco],
	dosis.dosistumor6 AS 'DOSIS TURNO SEIS',--[DosisTumorSeis],
	dosis.totaldosis 'TOTAL DOSIS SESION',--[TotalDosisSesion]
	dosis.numingres 'INGRESO',
	CAST(dosis.fecharegistro AS DATE) AS 'FECHA BUSQUEDA',
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM detalleordenes AS dosis
INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON dosis.ipcodpaci = pac.ipcodpaci
LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo

INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON dosis.numingres = ing.numingres
INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
INNER JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dosis.coddiagno = dx.coddiagno
LEFT JOIN dbo.indiagnop AS dxp ON dosis.numingresord = dxp.numingres AND dosis.coddiagnoord = dxp.coddiagno

INNER JOIN dbo.inprofsal AS prof WITH (NOLOCK) ON dosis.codprosal = prof.codprosal
INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON prof.codespec1 = esp.codespeci

INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON dosis.codcenate = ca.codcenate 
INNER JOIN dbo.inmunicip AS munca WITH (NOLOCK) ON ca.depmuncod = munca.depmuncod
INNER JOIN .segusuaru AS usua WITH (NOLOCK) ON dosis.usuarioregistro = usua.codusuari 

INNER JOIN primeraplicacion AS pdos WITH (NOLOCK) ON dosis.id = pdos.id
LEFT JOIN .segusuaru AS pusu  WITH (NOLOCK) ON pdos.usuarioregistro = pusu.codusuari 
LEFT JOIN .segusuaru AS medp WITH (NOLOCK) ON dosis.usuconfir = medp.codusuari
INNER JOIN dbo.adcenaten AS caap WITH (NOLOCK) ON dosis.codcenatedosis = caap.codcenate 
INNER JOIN dbo.adcenaten AS capa WITH (NOLOCK) ON pdos.codcenatedosis = capa.codcenate 

--WHERE CAST(dosis.fecharegistro AS DATE) BETWEEN @ini_date AND @end_date
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting/carga de cubo OLAP que aplana el detalle de aplicaciones de radioterapia (teleterapia y radiocirugía) por sesión de dosis. Consolida datos del paciente, entidad aseguradora, régimen, diagnóstico, orden de radioterapia, esquema de dosis, fechas de simulación/planeación/aplicación, dosis por tumor y sesión, profesional que aplicó, y centros de atención. Identifica además la primera aplicación de cada esquema para comparar el profesional y centro de la primera dosis frente a cada sesión registrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, para la carga al cubo, el detalle de aplicaciones de radioterapia (teleterapia y radiocirugía) por paciente, esquema y sesión, enriquecido con datos demográficos, administrativos, clínicos y de centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de radioterapia deben estar vinculadas a un CUPS cuyo desserips contenga ''TELETER'' o ''RADIOCIRUGIA'' (incupsips).; Cada dosis (hcraddosis) debe pertenecer a un esquema (hcradesquemas) y a una orden (hcradorden) válida.; La orden debe tener pronóstico asociado (hcordpron) vía idhcordpron.; El ingreso (adingreso) debe existir y tener entidad de salud (healthadministrator) y grupo de atención (caregroup) asociados.; El profesional ordenante (inprofsal) debe tener especialidad principal (codespec1) registrada en inespecia.; Los centros de atención de la orden y de la dosis deben existir en adcenaten.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes de radioterapia cuyo CUPS describe TELETERAPIA o RADIOCIRUGIA (filtro fijo en CTE ''ordenes'').; El número de sesión (NRO SESION) se calcula cronológicamente por fecha de registro dentro del esquema.; La FECHA PATOLOGIA se obtiene como MAX([23]) en hconcopreg para el paciente y diagnóstico de la dosis.; La columna CANTIDAD siempre vale 1 (una sesión = una unidad).; ID_COMPANY se publica como los primeros 9 caracteres de DB_NAME().; ULT_ACTUAL se calcula como GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; El paciente y el ingreso son obligatorios (INNER JOIN con inpacient y adingreso); ubicación, municipio y departamento son opcionales (LEFT JOIN).; Profesional confirmador de planeación y profesional de primera aplicación pueden ser nulos (LEFT JOIN sobre segusuaru).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radioterapia; Teleterapia; Radiocirugía; Braquiterapia; Esquema de dosis; Sesión de radioterapia; Dosis por sesión / Dosis total; Orden médica; Diagnóstico (CIE); Tipo de diagnóstico (Impresión/Confirmado); Patología; CUPS; Autorización; Ingreso; EPS / Régimen de afiliación; Grupo de atención y tipo de liquidación (PGP, capitación, etc.); Centro de atención; Profesional de la salud y especialidad; Paciente y tipo de identificación; Simulación y planeación de radioterapia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalRadioterapiaAplicaciones: Devuelve una fila por sesión de dosis aplicada (hcraddosis) en órdenes cuyo CUPS describe TELETERAPIA o RADIOCIRUGIA, numerando las sesiones por esquema vía ROW_NUMBER() PARTITION BY rade.id ORDER BY radd.fecharegistro ASC.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si incupsips.desserips LIKE ''%TELETER%'' OR LIKE ''%RADIOCIRUGIA%'' → Se incluye la orden en el CTE ''ordenes'' y por tanto en el resultado. else La orden se descarta del cubo.; si detalleordenes.nsesion = 1 → Esa fila alimenta el CTE ''primeraplicacion'' aportando ''PROFESIONAL APLICO PRIMERA DOSIS'' y ''CENTRO ATENCION PRIMER DOSIS''.; si pac.iptipodoc IN (1..15) → Se traduce a código de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si entc.entitytype IN (1..12,99) → Se traduce al régimen (EPS Contributivo/Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de Tránsito, Fosyga, Particulares, Otros). else Se etiqueta como ''Otros''.; si grpc.liquidationtype IN (1..5) → Se mapea a tipo de liquidación (Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP).; si dxp.tipdiagno IN (''I'',''C'',''R'') → Se traduce a Impresión Diagnóstica, Confirmado Nuevo o Confirmado Repetido.; si dosis.estado IN (1..8) → Se etiqueta el estado de la orden (Solicitado, Simulación Programada, Planeación Programada, Planeación Confirmada, Finalizado, Anulado, Completado, Braquiterapia Iniciada).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcraddosis; dbo.hcradesquemas; dbo.hcradorden; dbo.incupsips; dbo.hcordpron; dbo.hconcopreg; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.indiagnos; dbo.indiagnop; dbo.inprofsal; dbo.inespecia; dbo.adcenaten; segusuaru', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalRadioterapiaAplicaciones';
GO
