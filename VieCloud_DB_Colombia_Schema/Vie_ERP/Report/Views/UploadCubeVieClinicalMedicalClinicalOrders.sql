

--CREATE PROCEDURE [dbo].[ODO_Ordenamientos_Medicos]

--DECLARE	@inidate DATETIME='2024-06-01';
--DECLARE	@enddate DATETIME='2024-06-02';

CREATE view [Report].[UploadCubeVieClinicalMedicalClinicalOrders] AS

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
		WHERE CAST(ordc.fecharegistro AS DATE)>='2024-05-01'
		--ordc.fecharegistro BETWEEN @inidate AND @enddate   
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
		WHERE CAST(lab.fecordmed AS DATE) >='2024-05-01'
		--lab.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE CAST(img.fecordmed AS DATE) >='2024-05-01'
		--img.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE CAST(pat.fecordmed AS DATE) >='2024-05-01'
		--pat.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE  CAST(intc.fecordmed AS DATE) >='2024-05-01'
		--intc.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE CAST(pron.fecordmed AS DATE) >='2024-05-01'
		--pron.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE CAST(proq.fecordmed AS DATE) >='2024-05-01'
		--proq.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE CAST(chp.fechispac AS DATE) >='2024-05-01'
		--chp.fechispac BETWEEN @inidate AND @enddate


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
		WHERE CAST(hem.fecordmed AS DATE)>='2024-05-01'
		--hem.fecordmed BETWEEN @inidate AND @enddate
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
		WHERE pres.idesquemaonc IS NULL AND  CAST(pres.fecinidos AS DATE)>='2024-05-01'
		--pres.fecinidos BETWEEN @inidate AND @enddate


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


	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		RTRIM(ca.codcenate) + ' - ' + RTRIM(ca.nomcenate) AS 'CENTRO DE ATENCION',--[Centro de Atención],
		RTRIM(uf.ufucodigo) + ' - ' + RTRIM(uf.ufudescri) AS 'UNIDAD FUNCIONAL',--[Unidad Funcional],
		RTRIM(med.codprosal) AS 'DOCUMENTO DEL MEDICO',--[Documento del Medico],
		RTRIM(med.nommedico) AS 'NOMBRE DEL MEDICO',--[Nombre del Medico],
		RTRIM(esp.desespeci) AS 'ESPECIALIDAD',--[Especialidad],
		CONVERT(VARCHAR(19), orden.fecordmed, 120) AS 'FECHA ORDEN/ATENCION',--[Fecha de Orden/Atención],
		orden.numingres AS 'NUMERO INGRESO',--[Numero de Ingreso],
		CONVERT(VARCHAR(19), ing.ifechaing, 120) AS 'FECHA INGRESO',--[Fecha de Ingreso],
		cen.[Cama] 'CAMA',--,
		cen.[Tipo de Estancia] 'TIPO ESTANCIA',--,
		orden.numefolio AS 'NUMERO DE FOLIO',--[Numero de Folio] ,
		orden.autonro AS ID,
		orden.cupscode AS  'CODIGO SERVICIO IPS',--[Código Servicio IPS],
		orden.cupsname AS 'NOMBRE SERVICIO IPS',--[Nombre Servicio IPS],
		orden.descode AS 'CODIGO DESCRIPCOIN RELACIONADA',--[Código Descripción Relacionada],
		orden.desname AS 'NOMBRE DESCRIPCION RELACIONADA',--[Nombre Descripción Relacionada],
		orden.canserips 'CANTIDAD ORDENADA',--[Cantidad Ordenada],
		orden.estserips AS 'ESTADO DE LA ORDEN',--[Estado de la Orden],
		orden.manextpro AS 'TIPO DE ORDENAMIENTO',--[Tipo de Ordenamiento],
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
		RTRIM(pac.ipsegnomb) AS 'SEGUNDO NOMBRE ',--[Segundo Nombre],
		RTRIM(pac.ippriapel) AS 'PRIMER APELLIDO',--[Primer Apellido],
		RTRIM(pac.ipsegapel) AS 'SEGUNDO APELLIDO',--[Segundo Apellido],
		CASE pac.ipsexopac
			WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS 'GENERO',--Genero,
		DATEDIFF(YEAR, pac.ipfecnaci, orden.fecordmed) AS 'EDAD',--Edad,
		RTRIM(mun.muncodigo + ' - ' + mun.munnombre ) AS 'MUNICIPIO',--[Municipio],
		RTRIM(dep.depcodigo + ' - ' +  dep.nomdepart) AS 'DEPARTAMENTO',--[Departamento],
		pac.ipdirecci AS 'DIRECCION DE RESIDENCIA',--[Dirección de Residencia],
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
			WHEN 8 THEN 'Desplazado No Asegurado' END AS 'REGIMEN',--Régimen,
		entc.code AS 'CODIGO EPS',--[Código EPS],
		entc.name 'NOMBRE EPS',--[Nombre EPS],
		grpc.code AS 'CODIGO GRUPO DE ATENCION',--[Código Grupo de Atención],
		grpc.name 'NOMBRE GRUPO DE ATENCION',--[Nombre Grupo de Atención],
		CASE his.idetiphis 
			WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio)
			WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio)
			WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio) ELSE '' END AS 'JUSTIFICACION CLINICA',--[Justificación Clínica],
		CAST(orden.fecordmed AS DATE) as 'FECHA BUSQUEDA',
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
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida las órdenes médicas clínicas (laboratorios, imágenes, patologías, interconsultas, procedimientos, hemocomponentes, medicamentos y esquemas de quimioterapia) con datos del paciente, médico, ingreso, diagnóstico y censo para alimentar un cubo de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha de la orden (fecordmed/fecinidos/fechispac) debe ser >= ''2024-05-01'' para ser incluida.; El paciente debe existir en inpacient, el médico en inprofsal y el ingreso en adingreso.; Para medicamentos no quimio, idesquemaonc debe ser NULL (excluye los asociados a esquema oncológico).; Los códigos de servicio deben existir en contract.cupsentity (INNER JOIN por code).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con fecha >= ''2024-05-01'' (filtro hardcoded).; Solo se considera el censo activo (regestado = 1) para asignar cama y tipo de estancia.; El diagnóstico mostrado es siempre el principal del ingreso (coddiapri = 1).; La fecha de última actualización se calcula con GETDATE() convertido a ''Pakistan Standard Time''.; ID_COMPANY se obtiene dinámicamente de DB_NAME() truncado a VARCHAR(9).; Las cantidades en hemocomponentes se calculan como COUNT(det.codserips) y en quimio como SUM(mede.cantidad).; Para órdenes de control se fija canserips=1, estserips=NULL, manextpro=''EXTRAMURAL''.; La edad se calcula como DATEDIFF(YEAR, fecha_nacimiento, fecha_orden).; El SELECT final usa DISTINCT para eliminar duplicados generados por los joins.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica; Laboratorio clínico; Imágenes diagnósticas; Patología; Interconsulta; Procedimiento quirúrgico; Procedimiento no quirúrgico; Hemocomponente; Medicamento; Esquema de quimioterapia / ciclo oncológico; Orden de control extramural; CUPS (Código Único de Procedimientos en Salud); Centro de atención; Unidad funcional; Especialidad médica; Censo hospitalario / cama / tipo de estancia; Ingreso / folio; Diagnóstico principal (CIE); Paciente (tipo documento, régimen, EPS, grupo de atención); Justificación clínica (urgencias / evolución / nota); Tipo de ordenamiento (Hospitalaria / Extramural)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalClinicalOrders: Devuelve un set unificado (UNION ALL) de órdenes desde 9 fuentes distintas, etiquetadas por [type]: 1=Servicio, 2=Producto, 3=Producto Quimioterapia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de la orden (laboratorio/imagen/patología/interconsulta/proc. no quirúrgico/proc. quirúrgico/control/hemocomponente/medicamento/quimio) → Cada CASE traduce estserips a etiquetas distintas según el tipo de orden (p.ej. labo: 1=Solicitado..8=Muestra Recolectada Parcialmente; imagen: 1..7; quirúrgico: 1..6 incluye ''Sala Programada'',''Cancelado'',''Programado no realizado''; interconsulta: 1..5; hemocomponente: 1..4).; si manextpro = 0 vs 1 → Se etiqueta como ''HOSPITALARIA'' (0) o ''EXTRAMURAL'' (1); las órdenes de control siempre se marcan ''EXTRAMURAL''.; si orden.[type] → Se traduce a ''Servicio'' (1), ''Producto'' (2) o ''Producto Quimioterapia'' (3).; si pres.idesquemaonc IS NULL → La prescripción se incluye como medicamento normal (type=2); en caso contrario se omite (queda solo en flujo de quimio).; si his.idetiphis = ''hcurging1'' / ''hcurgevo1'' / ''hcnotevo1'' → Se obtiene la justificación clínica (analisisp) desde la tabla correspondiente; en otro caso devuelve cadena vacía.; si iptipodoc (1..15) → Se mapea a los códigos de tipo de documento colombianos (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si iptipopac (1..8) → Se mapea al régimen de afiliación: Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado (Contributivo/Subsidiado/No Asegurado).; si ipsexopac → 1=Masculino, 2=Femenino.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ehr.hcordciclos; ehr.hcordmedicam; ehr.schemes; dbo.chregesta; dbo.chcamasho; dbo.chtipesta; dbo.hcordlabo; dbo.hcordimag; dbo.hcordpato; dbo.hcordinte; dbo.hcordpron; dbo.hcordproq; dbo.hchispaca; dbo.hcdescoex; dbo.hcorhemco; dbo.hcorhemser; dbo.hcprescra; contract.cupsentity; contract.cupsentitycontractdescriptions; contract.contractdescriptions; contract.healthadministrator; contract.caregroup; inventory.atc; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.inpacient; dbo.inubicaci; dbo.inmunicip (+7 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalClinicalOrders';
GO
