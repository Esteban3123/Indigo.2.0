

--DECLARE @ini_date AS DATETIME='2024-05-01';
--DECLARE	@end_date AS DATETIME='2024-05-31';

CREATE view [Report].[UploadCubeVieClinicalServicesPerformed] AS

WITH radioterapias AS 
	(
	--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
	--	Script para obtener el detalle de todas las radioterapias realizadas filtrando por EPS		
		SELECT
			'Radioterapia' AS [servicio],
			ord.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			ror.codserips AS [codigoCUPS],
			cit.fecregsis AS [fecprogramacionactividad],
			dos.fecharegistro AS [fecrealizacionactividad],
			1 AS [cantidad],
			res.numsesion AS [ciclos], 
			'' AS [cicloactual],
			ord.codcenate, 
			ord.ufucodigo,
			NULL AS [esquema],
			ROW_NUMBER() OVER(PARTITION BY res.id + res.id  ORDER BY dos.fecharegistro ASC) AS [dia],
			ord.fecordmed,
			CAST(dos.fecharegistro AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcraddosis AS dos
		INNER JOIN dbo.hcradesquemas res ON  dos.idhcradesquemas = res.id 
		INNER JOIN dbo.hcradorden ror ON res.idhcradorden = ror.id
		INNER JOIN dbo.hcordpron AS ord ON ror.idhcordpron = ord.auto
		INNER JOIN dbo.incupsips AS sip ON ror.codserips = sip.codserips AND (sip.desserips LIKE '%TELETER%' OR sip.desserips LIKE '%RADIOCIRUGIA%') 
		LEFT JOIN dbo.agasicita AS cit ON dos.idcita = cit.codautonu AND cit.codestcit = '1'
		INNER JOIN dbo.inpacient AS pac ON ror.ipcodpaci = pac.ipcodpaci 
		INNER JOIN dbo.adingreso AS ing ON ord.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		where year(dos.fecharegistro)>=2024 and month(dos.fecharegistro)>=3
	),

	CTE_braquiterapias AS
	(
	--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
	--	Script para obtener el detalle de todas las braquiterapias realizadas filtrando por EPS
		SELECT
			'Braquiterapia' AS [servicio],
			ord.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			ror.codserips AS [codigoCUPS],
			cit.fecregsis AS [fecprogramacionactividad],
			bra.fecharegistro AS [fecrealizacionactividad],
			1 AS  [cantidad],
			0 AS [ciclos], 
			0 AS [cicloactual],
			ord.codcenate, 
			ord.ufucodigo,
			NULL AS [esquema],
			ROW_NUMBER() OVER(PARTITION BY bra.id + bra.id  ORDER BY bra.fecharegistro ASC) AS [dia],
			ord.fecordmed,
			CAST(bra.fecharegistro AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcradorden ror 
		INNER JOIN dbo.hcordpron AS ord ON ror.idhcordpron = ord.auto
		INNER JOIN dbo.hcraddosisbraqui AS bra ON ror.id = bra.idhcradorden 
		LEFT JOIN dbo.agasicita AS cit ON bra.idcita = cit.codautonu AND cit.codestcit = '1'
		INNER JOIN dbo.inpacient AS pac ON ror.ipcodpaci = pac.ipcodpaci
		INNER JOIN dbo.adingreso AS ing ON ord.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		where year(bra.fecharegistro)>=2024 and month(bra.fecharegistro)>=3
	),

CTE_atenciones AS 
	(
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Atenciones Consulta Externa
		SELECT
			'Consulta Externa' AS [servicio],
			urg.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			act.codserips AS [codigoCUPS],
			cit.fecregsis AS [fecprogramacionactividad],
			urg.feciniate AS [fecrealizacionactividad],
			1 AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			urg.codcenate, 
			urg.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(urg.fechinihi AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcurging1 AS urg
		INNER JOIN dbo.inpacient AS pac ON urg.ipcodpaci = pac.ipcodpaci
		INNER JOIN dbo.hchispaca AS hc ON urg.numingres = hc.numingres AND urg.numefolio = hc.numefolio AND hc.genconext = 1 AND hc.tiphispac = 'I'
		INNER JOIN dbo.adingreso AS ing ON urg.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		LEFT JOIN (SELECT numingres, numconcit, MAX(ipfechaco) AS ipfechaco FROM dbo.adconcoex WHERE conestado = 3 GROUP BY numingres, numconcit) AS cs ON urg.numingres = cs.numingres
		LEFT JOIN dbo.agasicita AS cit ON cs.numconcit = cit.codautonu
		LEFT JOIN dbo.agactimed AS act ON cit.codactmed = act.codactmed
		WHERE year(urg.fechinihi)>=2024 and month(urg.fechinihi)>=3	--CAST(urg.fechinihi AS DATE) BETWEEN @ini_date AND @end_date

	UNION 

		SELECT
			'Consulta Externa' AS [servicio],
			urg.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			act.codserips AS [codigoCUPS],
			cit.fecregsis AS [fecprogramacionactividad],
			urg.feciniate AS [fecrealizacionactividad],
			1 AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			urg.codcenate, 
			urg.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(urg.fechinihi AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcnotevo1 AS urg
		INNER JOIN dbo.inpacient AS pac ON urg.ipcodpaci = pac.ipcodpaci
		INNER JOIN dbo.hchispaca AS hc ON urg.numingres = hc.numingres AND urg.numefolio = hc.numefolio AND hc.genconext = 1 AND hc.tiphispac = 'I'
		INNER JOIN dbo.adingreso AS ing ON urg.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		LEFT JOIN (SELECT numingres, numconcit, MAX(ipfechaco) AS ipfechaco FROM dbo.adconcoex WHERE conestado = 3 GROUP BY numingres, numconcit) AS cs ON urg.numingres = cs.numingres
		LEFT JOIN dbo.agasicita AS cit ON cs.numconcit = cit.codautonu
		LEFT JOIN dbo.agactimed AS act ON cit.codactmed = act.codactmed
		WHERE year(urg.fechinihi)>=2024 and month(urg.fechinihi)>=3 --CAST(urg.fechinihi AS DATE) BETWEEN @ini_date AND @end_date

	UNION ALL
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Procedimientos Quirurgicos - Procedimientos no Quirurgicos
		SELECT
			'Procedimientos Quirurgicos/No Quirurgicos' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			pqx.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			pqx.fechorini AS [fecrealizacionactividad],
			1 AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			pqx.codcenate, 
			pqx.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(pqx.fechorini AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcqxinfor AS pqx 
		INNER JOIN dbo.inpacient AS pac ON pqx.ipcodpaci = pac.ipcodpaci
		INNER JOIN dbo.adingreso AS ing ON pqx.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE  year(pqx.fechorini)>=2024 and month(pqx.fechorini)>=3  --CAST(pqx.fechorini AS DATE) BETWEEN @ini_date AND @end_date

	UNION ALL
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Laboratorios
		SELECT 
			'Laboratorios' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			lab.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			lab.fecrecmue AS [fecrealizacionactividad],
			lab.canserips AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			lab.codcenate, 
			lab.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(lab.fecrecmue AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcordlabo AS lab
		INNER JOIN dbo.inpacient AS pac ON lab.ipcodpaci = pac.ipcodpaci
		INNER JOIN contract.cupsentity AS cups ON lab.codserips = cups.code
		INNER JOIN dbo.adingreso AS ing ON lab.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE  year(lab.fecrecmue)>=2024 and month(lab.fecrecmue)>=3 AND lab.estserips NOT IN('6', '7')	--CAST(lab.fecrecmue AS DATE) BETWEEN @ini_date AND @end_date AND lab.estserips NOT IN('6', '7') 

	UNION ALL
		--	Laboratorios Ambulatorios 
		SELECT 
			'Laboratorios' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			lab.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			lab.fecrecmue AS [fecrealizacionactividad],
			lab.canserips AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			lab.codcenate, 
			lab.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(lab.fecrecmue AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.ambordlab AS lab
		INNER JOIN dbo.inpacient AS pac ON lab.ipcodpaci = pac.ipcodpaci
		INNER JOIN contract.cupsentity AS cups ON lab.codserips = cups.code
		INNER JOIN dbo.adingreso AS ing ON lab.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE  year(lab.fecrecmue)>=2024 and month(lab.fecrecmue)>=3  AND lab.estserips NOT IN('6', '7')  --CAST(lab.fecrecmue AS DATE) BETWEEN @ini_date AND @end_date AND lab.estserips NOT IN('6', '7') 

	UNION ALL
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Patologias
		SELECT
			'Patologias' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			pat.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			pat.fecrecexa AS [fecrealizacionactividad],
			pat.canserips AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			pat.codcenate, 
			pat.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(pat.fecrecexa AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcordpato AS pat
		INNER JOIN dbo.inpacient AS pac ON pat.ipcodpaci = pac.ipcodpaci
		INNER JOIN contract.cupsentity AS cups ON pat.codserips = cups.code
		INNER JOIN dbo.adingreso AS ing ON pat.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE year(pat.fecrecexa)>=2024 and month(pat.fecrecexa)>=3 AND pat.estserips NOT IN('6', '7')  --CAST(pat.fecrecexa AS DATE) BETWEEN @ini_date AND @end_date AND pat.estserips NOT IN('6', '7') 

		UNION ALL
		--	Patologias Ambulatorios 
		SELECT 
			'Patologias' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			pat.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			pat.fecrecexa AS [fecrealizacionactividad],
			pat.canserips AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			pat.codcenate, 
			pat.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(pat.fecrecexa AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.ambordpat AS pat
		INNER JOIN dbo.inpacient AS pac ON pat.ipcodpaci = pac.ipcodpaci
		INNER JOIN contract.cupsentity AS cups ON pat.codserips = cups.code
		INNER JOIN dbo.adingreso AS ing ON pat.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE year(pat.fecrecexa)>=2024 and month(pat.fecrecexa)>=3 AND pat.estserips NOT IN('6', '7') --CAST(pat.fecrecexa AS DATE) BETWEEN @ini_date AND @end_date AND pat.estserips NOT IN('6', '7') 

	UNION ALL
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Imagenes
		SELECT 
			'Imagenes' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			img.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			img.fecrecexa AS [fecrealizacionactividad],
			img.canserips AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			img.codcenate, 
			img.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(img.fecrecexa AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hcordimag AS img
		INNER JOIN dbo.inpacient AS pac ON img.ipcodpaci = pac.ipcodpaci
		INNER JOIN contract.cupsentity AS cups ON img.codserips = cups.code
		INNER JOIN dbo.adingreso AS ing ON img.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE  year(img.fecrecexa)>=2024 and month(img.fecrecexa)>=3 AND img.estserips NOT IN('6', '7') --CAST(img.fecrecexa AS DATE) BETWEEN @ini_date AND @end_date AND img.estserips NOT IN('6', '7') 

		UNION ALL

		--	Imagenes Ambulatorias
		SELECT 
			'Imagenes' AS [servicio],
			ing.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			img.codserips AS [codigoCUPS],
			NULL AS [fecprogramacionactividad],
			img.fecrecexa AS [fecrealizacionactividad],
			img.canserips AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			img.codcenate, 
			img.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(img.fecrecexa AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.ambordima AS img
		INNER JOIN dbo.inpacient AS pac ON img.ipcodpaci = pac.ipcodpaci
		INNER JOIN contract.cupsentity AS cups ON img.codserips = cups.code
		INNER JOIN dbo.adingreso AS ing ON img.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE  year(img.fecrecexa)>=2024 and month(img.fecrecexa)>=3 AND img.estserips NOT IN('6', '7') --CAST(img.fecrecexa AS DATE) BETWEEN @ini_date AND @end_date AND img.estserips NOT IN('6', '7') 

	UNION ALL 
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Quimioterapia 
		SELECT 
			'Quimioterapia' AS [servicio],
			qtx.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			qxm.codproduc AS [CodigoCUM],
			cit.fecregsis AS [fecprogramacionactividad],
			qxd.fechaadministracion AS [fecrealizacionactividad],
			qxm.cantidad AS  [cantidad],
			qtx.ciclos, 
			qtx.cicloactual,
			qxc.codcenate, 
			qxc.ufucodigo,
			RTRIM(sch.description) AS [esquema],
			qxd.dia,
			qxc.fecharegistro AS [fecordmed],
			CAST(qxd.fechaadministracion AS DATE) AS 'FECHA BUSQUEDA'
		FROM ehr.hcordciclos AS qxc
		INNER JOIN ehr.hcordquimio AS qtx ON qxc.idhcordquimio = qtx.id
		INNER JOIN dbo.inpacient AS pac ON qtx.ipcodpaci = pac.ipcodpaci 
		INNER JOIN ehr.hcordciclosd AS qxd ON qxc.idhcordquimio = qxd.idhcordquimio AND qxc.id = qxd.idhcordciclos --AND CAST(qxd.fechaadministracion AS DATE) BETWEEN @ini_date AND @end_date
		INNER JOIN ehr.hcordmedicam AS qxm ON qxd.idhcordquimio = qxm.idhcordquimio AND qxd.ciclo = qxm.ciclo AND qxd.dia = qxm.dia 
		INNER JOIN dbo.agasicita AS cit ON cit.idhcordciclosd = qxd.id AND cit.codestcit = '1'
		INNER JOIN dbo.adingreso AS ing ON qtx.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		INNER JOIN ehr.schemes AS sch ON qtx.schemesid = sch.id
		where year(qxd.fechaadministracion)>=2024 and month(qxd.fechaadministracion)>=3

	UNION ALL 
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Radioterapia 
		SELECT * FROM radioterapias --WHERE CAST(fecrealizacionactividad AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Braquiterapia 
		SELECT * FROM CTE_braquiterapias --WHERE CAST(fecrealizacionactividad AS DATE) BETWEEN @ini_date AND @end_date 

	UNION ALL 
		--	____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
		--	Medicamentos Hospitalizacion/UCI 
		SELECT 
			'Medicamentos Hospitalizacion/UCI' AS [servicio],
			med.numingres,
			RTRIM(pac.ipcodpaci) AS [ipcodpaci],
			med.codproduc AS [CodigoCUM],
			med.fecinitra AS [fecprogramacionactividad],
			med.fecaplmed AS [fecfealizacionactividad],
			med.candescon AS [cantidad],
			0 AS [ciclos],
			0 AS [cicloactual],
			med.codcenate, 
			med.ufucodigo,
			NULL AS [esquema],
			NULL AS [dia],
			NULL AS [fecordmed],
			CAST(med.fecaplmed AS DATE) AS 'FECHA BUSQUEDA'
		FROM dbo.hchojamed AS med
		INNER JOIN dbo.inpacient AS pac ON med.ipcodpaci = pac.ipcodpaci
		INNER JOIN dbo.adingreso AS ing ON med.numingres = ing.numingres --AND ing.genconentity IN (SELECT CAST(TRIM(value) AS INT) AS id FROM STRING_SPLIT(@id_eps, ','))
		WHERE med.medestado = '2' AND med.idhcordquimio IS NULL
		AND year(med.fecaplmed)>=2024 and month(med.fecaplmed)>=3  -- CAST(med.fecaplmed AS DATE) BETWEEN @ini_date AND @end_date
),

CTE_ingresosordenes AS 
	(

		SELECT DISTINCT numingres FROM CTE_atenciones

	),
	
CTE_diagnosticos AS 
	(

		SELECT 
			ing.numingres,
			dx.ipcodpaci,
			ROW_NUMBER() OVER(PARTITION BY ing.numingres ORDER BY dx.coddiapri DESC) AS numrow,
			dx.coddiagno,
			dx.coddiapri,
			CASE dx.tipdiagno 
				WHEN 'I' THEN 'Impresion Diagnostica'
				WHEN 'C' THEN 'Confirmado Nuevo'
				WHEN 'R' THEN 'Confirmado Repetido' END AS [tipdiagno]
		FROM dbo.indiagnop dx
		INNER JOIN CTE_ingresosordenes AS ing ON dx.numingres = ing.numingres
	)

SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
        RTRIM(pac.ipnomcomp) AS 'NOMBRE PACIENTE',--[NomPaciente],
		pac.ipfecnaci AS 'FECHA NACIMIENTO',--[FecNacimiento],
		mun.munnombre AS 'MUNICIPIO',--[Municipio],
		dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
		RTRIM(ent.code) AS 'CODIGO EPS',--[CodigoEPS],
		RTRIM(ent.name) AS 'NOMBRE EPS',--[NomEPS],
		CASE ent.entitytype
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
			WHEN 12 THEN 'Otros' END AS 'REGIMEN EPS',-- [RegimenEPS],
		RTRIM(grp.code) AS 'CODIGO GRUPO ATENCION',--[CodGrupAtencion],
		RTRIM(grp.name) AS 'NOMBRE GRUPO ATENCION',--[NomGrupoAtencion],
		CASE grp.liquidationtype
			WHEN 1 THEN 'Pago por Servicios'
			WHEN 2 THEN 'Capitacion'
			WHEN 3 THEN 'Factura Global'
			WHEN 4 THEN 'Capitacion Global'
			WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],
		cat.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
		ufu.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		atn.fecordmed AS 'FECHA ORDEN',--[FecOrden],
		atn.esquema AS 'ESQUEMA',--[Esquema],
		atn.dia AS 'DIA',--[Dia],
		atn.servicio 'TIPO SERVICIO',--[TipoServicio],
		CASE 
			WHEN LEFT(cat.codcenate, 2) = '11' THEN 'PEREIRA'
			WHEN LEFT(cat.codcenate, 2) = '12' THEN 'MANIZALES'
			WHEN LEFT(cat.codcenate, 2) = '13' THEN 'ARMENIA'
			WHEN LEFT(cat.codcenate, 2) = '14' THEN 'CARTAGO' END AS 'SUCURSAL',--[Sucursal],
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
		atn.ipcodpaci AS 'NRO IDENTIFICACION',--[NumIdentificacion],
		est.descripcion AS 'ESTADIO ACTUAL',--[EstadioActual],
		CASE adx.idadgrupocancerc 
		WHEN 1 THEN 'Mama'
		WHEN 2 THEN 'Pulmon'
		WHEN 3 THEN 'Gástrico'
		WHEN 4 THEN 'Colorrectal'
		WHEN 5 THEN 'Cáncer anal'
		WHEN 6 THEN 'Próstata'
		WHEN 7 THEN 'Cérvix'
		WHEN 8 THEN 'Melanoma'
		WHEN 9 THEN 'Leucemias'
		WHEN 10 THEN 'Linfomas (Hodgkin - No Hodgkin)' 
		WHEN 11 THEN 'Otros' ELSE NULL END AS 'TIPO CANCER',--[TipoCancer],
		dxp.coddiagno AS 'CIE10',--[CIE10],
		dxp.tipdiagno AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
		ISNULL(RTRIM(cups.code), RTRIM(atn.codigoCUPS)) AS 'CODIGO SERVICIO',--[CodigoServicio],
		ISNULL(RTRIM(cups.description), RTRIM(atc.name)) AS 'NOMBRE SERVICIO',--[NomServicio],
		atn.fecprogramacionactividad AS 'FECHA PROGRAMACION',--[FecProgramacion],
		atn.fecrealizacionactividad AS 'FECHA REALIZACION',--[FecRealizacion],
		atn.cantidad AS 'CANTIDAD',--[Cantidad],
		atn.ciclos AS 'NRO CICLOS ORDENADOS',--[NroCiclosOrdenados],
		atn.cicloactual AS 'CICLO ACTUAL',--[CicloActual],
		NULL AS 'NOVEDAD',--[Novedad],
		'801000713-9' AS 'NIT IPS',--[NITIPS],
		'ONCOLOGOS DEL OCCIDENTE' AS 'NOMBRE IPS',--[NomIPS],
		CASE 
			WHEN cac.[126] = 1 THEN 'Pseudoprogresión (aplica solo para inmunoterapia)' 
			WHEN cac.[126] = 2 THEN 'Progresión o recaída'
			WHEN cac.[126] = 3 THEN 'Respuesta parcial'
			WHEN cac.[126] = 4 THEN 'Respuesta completa'
			WHEN cac.[126] = 5 THEN 'Enfermedad estable'
			WHEN cac.[126] = 6 THEN 'Abandono del tratamiento o alta voluntaria'
			WHEN cac.[126] = 7 THEN 'Paciente en seguimiento por antecedente de cáncer'
			WHEN cac.[126] = 8 THEN 'Pendiente iniciar el tratamiento luego del diagnóstico (fue definido por especialista o aún esta pendiente por valoración oncológica inicial, en la cual se defina el tratamiento)'
			WHEN cac.[126] = 97 THEN 'Paciente en seguimiento por antecedente de cáncer'
			WHEN cac.[126] = 98 THEN 'No aplicable en este periodo, aún bajo tratamiento inicial'
			WHEN cac.[126] = 99 THEN 'No aplicable en este periodo, aún bajo tratamiento de recaída'
			WHEN cac.[126] = 55 THEN 'Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorial durante el periodo de reporte'
			END AS 'RESULTADO',--[Resultado],
		CASE 
			WHEN cac.[40] = 1 THEN 'Curación' 
			WHEN cac.[40] = 2 THEN 'Paliación (intención paliativa) exclusivamente' 
			WHEN cac.[40] = 55 THEN 'Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorial durante el periodo de reporte'
			WHEN cac.[40] = 99 THEN 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos' 
			END AS 'INTENCION TRATAMIENTO',--[IntencionTratamiento],
		CAST(cac.[24] AS VARCHAR(10)) AS 'FECHA PATOLOGIA',--[FechaPatologia]
		atn.[FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM CTE_atenciones AS atn
	INNER JOIN dbo.adingreso AS ing ON atn.numingres = ing.numingres
	INNER JOIN dbo.inpacient AS pac ON atn.ipcodpaci = pac.ipcodpaci
	INNER JOIN contract.healthadministrator AS ent ON ing.genconentity = ent.id
	INNER JOIN contract.caregroup AS grp ON ing.gencaregroup = grp.id
	INNER JOIN dbo.adcenaten AS cat WITH (NOLOCK) ON atn.codcenate = cat.codcenate 
	INNER JOIN dbo.inunifunc AS ufu WITH (NOLOCK) ON atn.ufucodigo = ufu.ufucodigo
	LEFT JOIN CTE_diagnosticos AS dxp ON atn.numingres = dxp.numingres AND dxp.numrow = 1
	LEFT JOIN dbo.adgrupocancerdiagnd AS adx ON dxp.ipcodpaci = adx.coddiagno
	LEFT JOIN dbo.hconcopreg AS cac ON atn.ipcodpaci = cac.[6] AND dxp.coddiagno = cac.[17]
	LEFT JOIN contract.cupsentity AS cups ON atn.codigoCUPS = cups.code
	LEFT JOIN inventory.atc AS atc ON atn.codigoCUPS = atc.code
	LEFT JOIN report.TableEstadios AS est ON cac.[29] = est.id
	LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
	LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
	LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a la carga de un cubo analítico que consolida, desde marzo de 2024 en adelante, todos los servicios clínicos realizados a pacientes: radioterapias, braquiterapias, consultas externas, procedimientos quirúrgicos y no quirúrgicos, laboratorios (hospitalarios y ambulatorios) y patologías. Cada registro incluye el código CUPS, fechas de programación y realización, cantidad, ciclos y unidad funcional, permitiendo análisis multidimensional de prestaciones por tipo de servicio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por servicio realizado el detalle clínico-administrativo (consulta externa, procedimientos, laboratorios, patologías, imágenes, quimioterapia, radioterapia, braquiterapia y medicamentos hospitalarios) para alimentar un cubo de servicios prestados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente (inpacient), el ingreso (adingreso), la EPS (contract.healthadministrator) y el grupo de atención (contract.caregroup) deben existir, ya que se unen con INNER JOIN.; Para servicios con CUPS (laboratorios, patologías, imágenes) el código debe existir en contract.cupsentity (INNER JOIN).; Para radioterapia, el servicio debe estar tipificado en incupsips con descripción que contenga ''TELETER'' o ''RADIOCIRUGIA''.; Para quimioterapia debe existir esquema en ehr.schemes y cita asociada (agasicita.idhcordciclosd) con codestcit=''1''.; La fecha de realización debe ser >= marzo de 2024 (year>=2024 AND month>=3) en cada origen.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan actividades con fecha de realización a partir de marzo de 2024 (year>=2024 AND month>=3) en todos los orígenes.; En laboratorios, patologías e imágenes nunca se incluyen registros con estserips 6 o 7.; Los medicamentos asociados a quimioterapia (idhcordquimio NOT NULL) se excluyen de la rama ''Medicamentos Hospitalización/UCI'' para evitar duplicidad con la rama de Quimioterapia.; Cada fila lleva NIT IPS fijo ''801000713-9'' y nombre IPS ''ONCOLOGOS DEL OCCIDENTE''.; ID_COMPANY corresponde al nombre de la base de datos (DB_NAME) truncado a 9 caracteres.; La marca de tiempo de actualización (ULT_ACTUAL) se calcula con GETDATE() convertido a ''Pakistan Standard Time''.; Para cada ingreso se conserva un único diagnóstico principal (numrow=1).; El código de servicio mostrado prioriza cupsentity.code; si no existe se usa el codigoCUPS original del origen; el nombre prioriza cupsentity.description y, en su defecto, inventory.atc.name (medicamentos).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalServicesPerformed: Devuelve un dataset unificado de actividades clínicas a partir de 11 orígenes (radio, braqui, consulta externa hcurging1/hcnotevo1, procedimientos, laboratorios hosp/amb, patologías hosp/amb, imágenes hosp/amb, quimioterapia y medicamentos hospitalización) filtradas desde marzo/2024.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si incupsips.desserips LIKE ''%TELETER%'' OR LIKE ''%RADIOCIRUGIA%'' → El servicio se clasifica como ''Radioterapia'' en la CTE radioterapias else Queda fuera de la rama de radioterapia; si agasicita.codestcit = ''1'' → Se toma la cita como cita programada válida (LEFT JOIN para radio/braqui; INNER JOIN obligatorio en quimioterapia) else fecprogramacionactividad queda NULL para radio/braqui; se excluye registro en quimioterapia; si hchispaca.genconext = 1 AND tiphispac = ''I'' (consulta externa) → La nota/atención se considera Consulta Externa else No se incluye en CTE_atenciones de consulta externa; si adconcoex.conestado = 3 → Se usa la última ipfechaco para enlazar la cita de consulta externa; si estserips NOT IN (''6'',''7'') (laboratorios, patologías, imágenes) → Se incluye el servicio como realizado else Se excluye (estados 6 y 7 considerados anulados/no facturables); si hchojamed.medestado=''2'' AND idhcordquimio IS NULL → Se considera medicamento de Hospitalización/UCI aplicado y no asociado a quimioterapia else Se excluye del reporte; si LEFT(adcenaten.codcenate,2) IN (''11'',''12'',''13'',''14'') → Se asigna sucursal Pereira/Manizales/Armenia/Cartago respectivamente else Sucursal queda NULL; si CASE sobre entitytype, liquidationtype, iptipodoc, idadgrupocancerc, cac.[126], cac.[40] → Se traducen los códigos a etiquetas de negocio (régimen EPS, tipo liquidación, tipo identificación, tipo de cáncer, resultado clínico, intención de tratamiento) else NULL si no coincide ningún valor mapeado; si ROW_NUMBER() PARTITION BY numingres ORDER BY coddiapri DESC; numrow=1 → Se selecciona un único diagnóstico principal por ingreso (el de mayor coddiapri)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalServicesPerformed';
GO
