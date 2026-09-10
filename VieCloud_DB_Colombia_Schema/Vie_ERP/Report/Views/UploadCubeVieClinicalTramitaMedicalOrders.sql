


--CREATE PROCEDURE [dbo].[ODO_Ordenamientos_Medicos]
--DECLARE	@inidate DATETIME='2024-04-01';
--DECLARE	@enddate DATETIME='2024-04-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalTramitaMedicalOrders] as

	WITH medicamentosquimo 
	AS 
	(

		SELECT 
			ordc.id AS autonro,
			ordc.codcenate,
			ordc.ufucodigo,
			ordc.codprosal,
			ordc.fecharegistro AS fecordmed,
			ordc.numingres,
			ordc.numefolio,
			ordc.ipcodpaci,
			ordc.schemesid,
			mede.ciclo,
			mede.codproduc,
			SUM(mede.cantidad) AS canserips
		FROM ehr.hcordciclos AS ordc
		INNER JOIN ehr.hcordmedicam AS mede WITH (NOLOCK) ON ordc.schemesid = mede.schemesid AND ordc.idhcordquimio = mede.idhcordquimio AND ordc.ciclo = mede.ciclo
		WHERE year(ordc.fecharegistro)>=2024  --BETWEEN @inidate AND @enddate   
		GROUP BY ordc.id, ordc.codcenate, ordc.ufucodigo, ordc.codprosal, ordc.fecharegistro, ordc.numingres, ordc.numefolio, ordc.ipcodpaci, ordc.schemesid, mede.ciclo, mede.codproduc

	), censo AS 
	(
	
		SELECT 
			cen.ipcodpaci, 
			cen.numingres,
			RTRIM(bed.desccamas) AS [Cama],
			RTRIM(tip.destipest) [Tipo de Estancia]
		FROM dbo.chregesta AS cen
		INNER JOIN dbo.chcamasho AS bed ON cen.codicamas = bed.codicamas 
		LEFT JOIN dbo.chtipesta AS tip ON cen.codtipest = tip.codtipest
		WHERE regestado = 1

	), ordmedicas AS 
	(

		-- ================================================
		-- Laboratorios
		-- ================================================
		SELECT 
			lab.auto AS autonro,
			lab.codcenate,
			lab.ufucodigo,
			lab.codprosal,
			lab.fecordmed,
			lab.numingres,
			lab.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			lab.canserips,
			CASE lab.estserips
				WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Muestra Recolectada'
				WHEN 3 THEN 'Resultado Entregado'
				WHEN 4 THEN 'Examen Interpretado'
				WHEN 5 THEN 'Remitido'
				WHEN 6 THEN 'Anulado'
				WHEN 7 THEN 'Extramural'
				WHEN 8 THEN 'Muestra Recolectada Parcialmente' END AS estserips,
			CASE lab.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			lab.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcordlabo AS lab
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON lab.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON lab.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(lab.fecordmed)>=2024 -- BETWEEN @inidate AND @enddate

		UNION ALL
		-- ================================================
		-- Imagenes
		-- ================================================
		SELECT 
			img.auto AS autonro,
			img.codcenate,
			img.ufucodigo,
			img.codprosal,
			img.fecordmed,
			img.numingres,
			img.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			img.canserips,
			CASE img.estserips
				WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Estudio Realizado'
				WHEN 3 THEN 'Imagen Procesada'
				WHEN 4 THEN 'Estudio Interpretado'
				WHEN 5 THEN 'Remitido'
				WHEN 6 THEN 'Anulado'
				WHEN 7 THEN 'Extramural' END AS estserips,
			CASE img.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			img.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcordimag AS img
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON img.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON img.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(img.fecordmed)>=2024 -- BETWEEN @inidate AND @enddate

		UNION ALL
		-- ================================================
		-- Patologias
		-- ================================================
		SELECT 
			pat.auto AS autonro,
			pat.codcenate,
			pat.ufucodigo,
			pat.codprosal,
			pat.fecordmed,
			pat.numingres,
			pat.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			pat.canserips,
			CASE pat.estserips
				WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Muestra Recolectada'
				WHEN 3 THEN 'Resultado Entregado'
				WHEN 4 THEN 'Examen Interpretado'
				WHEN 5 THEN 'Remitido'
				WHEN 6 THEN 'Anulado'
				WHEN 7 THEN 'Extramural' END AS estserips,
			CASE pat.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			pat.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcordpato AS pat
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON pat.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON pat.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(pat.fecordmed)>=2024 -- BETWEEN @inidate AND @enddate

		UNION ALL
		-- ================================================
		-- Interconsultas
		-- ================================================
		SELECT 
			intc.auto AS autonro,
			intc.codcenate,
			intc.ufucodigo,
			intc.codprosal,
			intc.fecordmed,
			intc.numingres,
			intc.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			intc.canserips,
			CASE intc.estserips
				WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Solicitud Enviada'
				WHEN 3 THEN 'Interconsulta Realizada'
				WHEN 4 THEN 'Extramural'
				WHEN 5 THEN 'Anulado' END AS estserips,
			CASE intc.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			intc.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcordinte AS intc
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON intc.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON intc.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(intc.fecordmed)>=2024 -- BETWEEN @inidate AND @enddate

		UNION ALL
		-- ================================================
		-- Procedimientos no Quirurgicos 
		-- ================================================
		SELECT 
			pron.auto AS autonro,
			pron.codcenate,
			pron.ufucodigo,
			pron.codprosal,
			pron.fecordmed,
			pron.numingres,
			pron.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			pron.canserips,
			CASE pron.estserips
				WHEN 1 THEN 'Ordenado'
				WHEN 2 THEN 'Completado'
				WHEN 3 THEN 'Interpretado'
				WHEN 4 THEN 'Sin Interfaz'
				WHEN 5 THEN 'Anulado' END AS estserips,
			CASE pron.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			pron.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcordpron AS pron
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON pron.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON pron.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(pron.fecordmed)>=2024 -- BETWEEN @inidate AND @enddate

		UNION ALL
		-- ================================================
		-- Procedimientos Quirurgicos 
		-- ================================================
		SELECT 
			proq.auto AS autonro,
			proq.codcenate,
			proq.ufucodigo,
			proq.codprosal,
			proq.fecordmed,
			proq.numingres,
			proq.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			proq.canserips,
			CASE proq.estserips
				WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Sala Programada'
				WHEN 3 THEN 'Cancelado'
				WHEN 4 THEN 'Resultado Revisado'
				WHEN 5 THEN 'Anulado'
				WHEN 6 THEN 'Programado no realizado' END AS estserips,
			CASE proq.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			proq.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcordproq AS proq
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON proq.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON proq.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(proq.fecordmed)>=2024 -- BETWEEN @inidate AND @enddate

		UNION ALL
		-- ================================================
		-- Ordenes de control 
		-- ================================================
		SELECT 
			hci.auto AS autonro,
			chp.codcenate,
			chp.ufucodigo,
			chp.codprosal,
			chp.fechispac AS fecordmed,
			chp.numingres,
			chp.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			NULL AS descode,
			NULL AS desname,
			1 AS canserips,
			NULL AS estserips,
			'EXTRAMURAL' AS manextpro,
			chp.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hchispaca AS chp
		INNER JOIN dbo.hcdescoex AS hci WITH (NOLOCK) ON chp.numingres = hci.numingres AND chp.numefolio = hci.numefolio
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON hci.codserips = cups.code
		WHERE year(chp.fechispac)>=2024 -- BETWEEN @inidate AND @enddate


		UNION ALL
		-- ================================================
		-- Hemocomponentes
		-- ================================================
		SELECT 
			hem.id AS autonro,
			hem.codcenate,
			NULL AS ufucodigo,
			NULL AS codprosal,
			hem.fecordmed,
			hem.numingres,
			hem.numefolio,
			cups.code AS cupscode,
			cups.description AS cupsname,
			[desc].code AS descode,
			[desc].name AS desname,
			COUNT(det.codserips) AS canserips,
			CASE det.estado
				WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Realizado'
				WHEN 3 THEN 'Anulado'
				WHEN 4 THEN 'Extramural' END AS estserips,
			CASE hem.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			hem.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			1 AS [type]
		FROM dbo.hcorhemco AS hem 
		INNER JOIN dbo.hcorhemser AS det WITH (NOLOCK) ON hem.id = det.hcorhemcoid 
		INNER JOIN contract.cupsentity AS cups WITH (NOLOCK) ON det.codserips = cups.code
		LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON det.iddescripcionrelacionada = descr.id
		LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
		WHERE year(hem.fecordmed)>= 2024-- BETWEEN @inidate AND @enddate
		GROUP BY hem.id, hem.codcenate, hem.fecordmed, hem.numingres, hem.numefolio, det.codserips, det.iddescripcionrelacionada, det.estado, hem.manextpro, hem.ipcodpaci,
		cups.code, cups.description, [desc].code, [desc].name

		UNION ALL
		-- ================================================
		-- Medicamentos 
		-- ================================================
		SELECT 
			pres.id AS autonro,
			pres.codcenate,
			pres.ufucodigo,
			pres.codprosal,
			pres.fecinidos AS fecordmed,
			pres.numingres,
			pres.numefolio,
			prod.code AS cupscode,
			prod.name AS cupsname,
			NULL AS descode,
			NULL AS desname,
			pres.canpedpro AS canserips,
			NULL AS estserips,
			CASE pres.manextpro 
				WHEN 0 THEN 'HOSPITALARIA'
				WHEN 1 THEN 'EXTRAMURAL' END AS manextpro,
			pres.ipcodpaci,
			NULL AS Esquema,
			0 AS Ciclo,
			2 AS [type]
		FROM dbo.hcprescra AS pres
		INNER JOIN inventory.atc AS prod ON pres.codproduc = prod.code
		WHERE pres.idesquemaonc IS NULL AND year(pres.fecinidos)>=2024 -- BETWEEN @inidate AND @enddate


		UNION ALL
		-- ================================================
		-- Medicamentos Esquemas de Quimioterapia
		-- ================================================
		SELECT 
			medq.autonro,
			medq.codcenate,
			medq.ufucodigo,
			medq.codprosal,
			medq.fecordmed,
			medq.numingres,
			medq.numefolio,
			prod.code AS cupscode,
			prod.name AS cupsname,
			NULL AS descode,
			NULL AS desname,
			medq.canserips,
			NULL AS estserips,
			NULL AS manextpro,
			medq.ipcodpaci,
			esq.code + ' - ' + esq.[description] AS Esquema,
			medq.ciclo AS Ciclo,
			3 AS [type]
		FROM medicamentosquimo AS medq
		INNER JOIN inventory.atc AS prod WITH (NOLOCK) ON medq.codproduc = prod.code
		INNER JOIN ehr.schemes AS esq WITH (NOLOCK) ON medq.schemesid = esq.id 
	
	), tiphistoria AS (


		SELECT DISTINCT
			hc.numingres,
			hc.numefolio,
			hc.idetiphis
		FROM ordmedicas AS ord
		INNER JOIN dbo.hchispaca AS hc ON ord.numingres = hc.numingres AND ord.numefolio = hc.numefolio 

	)


	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		RTRIM(ca.codcenate) + ' - ' + RTRIM(ca.nomcenate) AS 'CENTRO DE ATENCION',--[Centro de Atención],
		RTRIM(uf.ufucodigo) + ' - ' + RTRIM(uf.ufudescri) AS 'UNIDAD FUNCIONAL',--[Unidad Funcional],
		RTRIM(med.codprosal) AS 'DOCUMENTO DEL MEDICO',--[Documento del Medico],
		RTRIM(med.nommedico) AS 'NOMBRE DEL MEDICO',--[Nombre del Medico],
		RTRIM(esp.desespeci) AS 'ESPECIALIDAD',--[Especialidad],
		CONVERT(VARCHAR(19), orden.fecordmed, 120) AS 'FECHA ORDEN/ATENCION',--[Fecha de Orden/Atención],
		orden.numingres AS 'NRO INGRESOS',--[Numero de Ingreso],
		CONVERT(VARCHAR(19), ing.ifechaing, 120) AS 'FECHA INGRESO',--[Fecha de Ingreso],
		cen.[Cama] 'CAMA',--,
		cen.[Tipo de Estancia] 'TIPO ESTANCIA',--,
		orden.numefolio AS 'NUMERO FOLIO',--[Numero de Folio],
		orden.autonro AS 'ID',--ID,
		orden.cupscode AS  'CODIGO SERVCIOS IPS',--[Código Servicio IPS],
		orden.cupsname AS 'NOMBRE SERVICIO IPS',--[Nombre Servicio IPS],
		orden.descode AS 'CODIGO DESCRIPCION RELACIONADA',--[Código Descripción Relacionada],
		orden.desname AS 'NOMBRE DESCRIPCION RELACIONADA',--[Nombre Descripción Relacionada],
		orden.canserips 'CANTIDAD ORDENADA',--[Cantidad Ordenada],
		orden.estserips AS 'ESTADO DE LA ORDEN',--[Estado de la Orden],
		orden.manextpro AS 'TIPO ORDENAMIENTO',--[Tipo de Ordenamiento],
		orden.Esquema 'ESQUEMA',--,
		orden.Ciclo 'CICLO',--,
		CASE orden.[type] 
			WHEN 1 THEN 'Servicio'
			WHEN 2 THEN 'Producto'
			WHEN 3 THEN 'Producto Quimioterapia' END AS 'TIPO CODIGO',--[Tipo Código],
		dx.coddiagno AS 'CODIGO DIAGNOSTICO',--[Código Diagnostico],
		RTRIM(dx.nomdiagno) AS 'NOMBRE DIAGNOSTICO',--[Nombre Diagnostico],
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
			WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO',--[Tipo de Documento],
		RTRIM(pac.ipcodpaci) AS 'NUMERO DOCUMENTO',--[Numero de Documento],
		RTRIM(pac.ipprinomb) AS 'PRIMER NOMBRE',--[Primer Nombre],
		RTRIM(pac.ipsegnomb) AS 'SEGUNDO NOMBRE',--[Segundo Nombre],
		RTRIM(pac.ippriapel) AS 'PRIMER APELLIDO',--[Primer Apellido],
		RTRIM(pac.ipsegapel) AS 'SEGUNDO APELLIDO',--[Segundo Apellido],
		CASE pac.ipsexopac
			WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS 'GENERO',--Genero,
		DATEDIFF(YEAR, pac.ipfecnaci, orden.fecordmed) AS 'EDAD',--Edad,
		RTRIM(mun.muncodigo + ' - ' + mun.munnombre ) AS 'MUNICIPIO',--[Municipio],
		RTRIM(dep.depcodigo + ' - ' +  dep.nomdepart) AS 'DEPARTAMENTO',--[Departamento],
		pac.ipdirecci AS 'DIRECCION RESIDENCIA',--[Dirección de Residencia],
		pac.iptelmovi  AS 'TELEFONO PRINCIPAL',--[Teléfono Principal],
		pac.iptelefon  'TELEFONO SECUNDARIO',--[Teléfono Secundario],
		CASE pac.iptipopac 
			WHEN 1 THEN 'Contributivo'
			WHEN 2 THEN 'Subsidiado' 
			WHEN 3 THEN 'Vinculado'
			WHEN 4 THEN 'Particular' 
			WHEN 5 THEN 'Otro' 
			WHEN 6 THEN 'Desplazado Reg. Contributivo' 
			WHEN 7 THEN 'Desplazado Reg. Subsidiado' 
			WHEN 8 THEN 'Desplazado No Asegurado' 
			WHEN 99 THEN 'Particular' else 'Otro' END AS 'REGIMEN',--Régimen,
		entc.code AS 'CODIGO EPS',--[Código EPS],
		entc.name 'NOMBRE EPS',--[Nombre EPS],
		grpc.code AS 'CODIGO GRUPO ATENCION',--[Código Grupo de Atención],
		grpc.name 'NOMBRE GRUPO ATENCION',--[Nombre Grupo de Atención],
		CASE his.idetiphis 
			WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio)
			WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio)
			WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio) ELSE '' END AS 'JUSTIFICACION CLINICA',--[Justificación Clínica],
		'ODO' AS 'ORIGEN',--origen,  
		CAST(orden.fecordmed AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM ordmedicas AS orden
	INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON orden.codcenate = ca.codcenate
	LEFT JOIN dbo.inunifunc AS uf WITH (NOLOCK) ON orden.ufucodigo = uf.ufucodigo
	INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON orden.codprosal = med.codprosal
	INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON med.codespec1 = esp.codespeci 
	INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON orden.ipcodpaci = pac.ipcodpaci 
	LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
	LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
	LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
	INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON orden.numingres = ing.numingres
	INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
	INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
	LEFT JOIN dbo.indiagnop AS dxp WITH (NOLOCK) ON orden.numingres = dxp.numingres AND dxp.coddiapri = 1 --AND dxp.tipdiagno <> 'R' 
	LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dxp.coddiagno = dx.coddiagno
	INNER JOIN tiphistoria AS his ON orden.numingres = his.numingres AND orden.numefolio = his.numefolio
	LEFT JOIN censo AS cen ON orden.ipcodpaci = cen.ipcodpaci AND orden.numingres = cen.numingres

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista las órdenes médicas (laboratorios, imágenes, patología, interconsultas, procedimientos quirúrgicos y no quirúrgicos, órdenes de control, hemocomponentes, medicamentos y esquemas de quimioterapia) generadas desde 2024, enriquecidas con datos del paciente, ingreso, médico, diagnóstico, ubicación y censo, para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de órdenes deben tener registros con year(fecordmed) >= 2024 (o year(fechispac)/year(fecinidos) >= 2024 según el origen).; Los códigos de servicio (codserips) de cada orden deben existir en contract.cupsentity para no perderse por el INNER JOIN.; Cada orden debe tener un ingreso (adingreso), centro de atención (adcenaten), profesional (inprofsal con especialidad inespecia), paciente (inpacient), entidad EPS (healthadministrator) y grupo de atención (caregroup) válidos; de lo contrario se excluye.; Debe existir al menos un registro en hchispaca para el par (numingres, numefolio) de la orden (INNER JOIN con tiphistoria).; Para medicamentos no oncológicos: pres.idesquemaonc IS NULL.; Para censo (cama/tipo estancia): chregesta.regestado = 1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila pertenece a una orden con fecha cuyo año es >= 2024.; La columna ID_COMPANY corresponde al nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; ULT_ACTUAL siempre se calcula como GETDATE() convertido a ''Pakistan Standard Time''.; ORIGEN siempre es la constante ''ODO''.; Edad se calcula en años completos entre ipfecnaci y la fecha de la orden.; Las órdenes de control siempre se reportan como EXTRAMURAL con cantidad 1.; Los medicamentos de esquemas de quimioterapia agrupan cantidad por (orden, esquema, ciclo, producto).; Los hemocomponentes agrupan canserips como COUNT de servicios del detalle por orden.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica; Laboratorio clínico; Imágenes diagnósticas; Patología; Interconsulta; Procedimiento quirúrgico; Procedimiento no quirúrgico; Orden de control / extramural; Hemocomponentes; Prescripción de medicamentos; Esquema de quimioterapia / ciclo; Paciente; Ingreso hospitalario; Censo (cama, tipo de estancia); Diagnóstico principal (CIE); Especialidad médica; Centro de atención / Unidad funcional; Régimen de afiliación (Contributivo, Subsidiado, etc.); EPS / Administradora de salud; Grupo de atención; CUPS; Justificación clínica (historia de urgencias / evolución); Modalidad hospitalaria vs extramural', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalTramitaMedicalOrders: Devuelve el conjunto unificado de órdenes médicas filtradas por year(fecha) >= 2024, etiquetando el tipo (1=Servicio, 2=Producto, 3=Producto Quimioterapia) y traduciendo códigos de estado y modalidad (HOSPITALARIA/EXTRAMURAL).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si orden.[type] = 1 / 2 / 3 → Se etiqueta como ''Servicio'', ''Producto'' o ''Producto Quimioterapia'' respectivamente.; si his.idetiphis = ''hcurging1'' | ''hcurgevo1'' | ''hcnotevo1'' → Se obtiene la JUSTIFICACION CLINICA (analisisp) desde la tabla correspondiente al tipo de historia. else Se devuelve cadena vacía como justificación clínica.; si pres.idesquemaonc IS NULL (en hcprescra) → El medicamento se incluye como ordenamiento tipo 2 (Producto no quimioterapia).; si Origen = Órdenes de control (hchispaca/hcdescoex) → Se fuerza manextpro = ''EXTRAMURAL'', canserips = 1 y descode/desname/estserips = NULL.; si estserips según origen (lab/img/pat/intc/pron/proq/hem) → Se mapea a literales distintos por dominio (p.ej. ''Solicitado'',''Muestra Recolectada'',''Sala Programada'',''Cancelado'', etc.).; si manextpro = 0 / 1 → Se traduce a ''HOSPITALARIA'' / ''EXTRAMURAL''.; si iptipopac IN (1..8,99) o ELSE → Se mapea régimen (Contributivo, Subsidiado, Vinculado, Particular, Desplazados, etc.); cualquier otro valor → ''Otro''.; si chregesta.regestado = 1 → Solo se considera el registro de censo activo para asociar cama y tipo de estancia.; si dxp.coddiapri = 1 → Solo se asocia el diagnóstico marcado como principal del ingreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ehr.hcordciclos; ehr.hcordmedicam; ehr.schemes; dbo.chregesta; dbo.chcamasho; dbo.chtipesta; dbo.hcordlabo; dbo.hcordimag; dbo.hcordpato; dbo.hcordinte; dbo.hcordpron; dbo.hcordproq; dbo.hchispaca; dbo.hcdescoex; dbo.hcorhemco; dbo.hcorhemser; dbo.hcprescra; contract.cupsentity; contract.cupsentitycontractdescriptions; contract.contractdescriptions; contract.healthadministrator; contract.caregroup; inventory.atc; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.inpacient; dbo.inubicaci; dbo.inmunicip (+7 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalTramitaMedicalOrders';
GO
