
-- =============================================
-- Author:		Michael Andres Soto
-- Create date: 2022-06-07
-- Description:	SP que retorna los ordenamientos medicos intramurales y extramurales.
-- =============================================
CREATE PROCEDURE [dbo].[ODO_Ordenamientos_Medicos]
	@inidate DATETIME,
	@enddate DATETIME
AS
BEGIN

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
		WHERE ordc.fecharegistro BETWEEN @inidate AND @enddate   
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
		WHERE lab.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE img.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE pat.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE intc.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE pron.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE proq.fecordmed BETWEEN @inidate AND @enddate

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
		WHERE chp.fechispac BETWEEN @inidate AND @enddate

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
		WHERE hem.fecordmed BETWEEN @inidate AND @enddate
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
		WHERE pres.idesquemaonc IS NULL AND pres.fecinidos BETWEEN @inidate AND @enddate

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

	SELECT 
		RTRIM(ca.codcenate) + ' - ' + RTRIM(ca.nomcenate) AS [Centro de Atención],
		RTRIM(uf.ufucodigo) + ' - ' + RTRIM(uf.ufudescri) AS [Unidad Funcional],
		RTRIM(med.codprosal) AS [Documento del Medico],
		RTRIM(med.nommedico) AS [Nombre del Medico],
		RTRIM(esp.desespeci) AS [Especialidad],
		CONVERT(VARCHAR(19), orden.fecordmed, 120) AS [Fecha de Orden/Atención],
		trim(orden.numingres) AS [Numero de Ingreso],
		CONVERT(VARCHAR(19), ing.ifechaing, 120) AS [Fecha de Ingreso],
		cen.[Cama],
		cen.[Tipo de Estancia],
		orden.numefolio AS [Numero de Folio],
		convert(int,orden.autonro) AS ID,
		orden.cupscode AS  [Código Servicio IPS],
		orden.cupsname AS [Nombre Servicio IPS],
		orden.descode AS [Código Descripción Relacionada],
		orden.desname AS [Nombre Descripción Relacionada],
		orden.canserips [Cantidad Ordenada],
		orden.estserips AS [Estado de la Orden],
		orden.manextpro AS [Tipo de Ordenamiento],
		orden.Esquema,
		orden.Ciclo,
		CASE orden.[type] 
			WHEN 1 THEN 'Servicio'
			WHEN 2 THEN 'Producto'
			WHEN 3 THEN 'Producto Quimioterapia' END AS [Tipo Código],
		dx.coddiagno AS [Código Diagnostico],
		RTRIM(dx.nomdiagno) AS [Nombre Diagnostico],
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
			WHEN 15 THEN 'SI' END AS [Tipo de Documento],
		RTRIM(pac.ipcodpaci) AS [Numero de Documento],
		RTRIM(pac.ipprinomb) AS [Primer Nombre],
		RTRIM(pac.ipsegnomb) AS [Segundo Nombre],
		RTRIM(pac.ippriapel) AS [Primer Apellido],
		RTRIM(pac.ipsegapel) AS [Segundo Apellido],
		CASE pac.ipsexopac
			WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS Genero,
		DATEDIFF(YEAR, pac.ipfecnaci, orden.fecordmed) AS Edad,
		RTRIM(mun.muncodigo + ' - ' + mun.munnombre ) AS [Municipio],
		RTRIM(dep.depcodigo + ' - ' +  dep.nomdepart) AS [Departamento],
		pac.ipdirecci AS [Dirección de Residencia],
		pac.iptelmovi  AS [Teléfono Principal],
		pac.iptelefon  [Teléfono Secundario],
		CASE pac.iptipopac 
			WHEN 1 THEN 'Contributivo'
			WHEN 2 THEN 'Subsidiado' 
			WHEN 3 THEN 'Vinculado'
			WHEN 4 THEN 'Particular' 
			WHEN 5 THEN 'Otro' 
			WHEN 6 THEN 'Desplazado Reg. Contributivo' 
			WHEN 7 THEN 'Desplazado Reg. Subsidiado' 
			WHEN 8 THEN 'Desplazado No Asegurado' END AS Régimen,
		entc.code AS [Código EPS],
		entc.name [Nombre EPS],
		grpc.code AS [Código Grupo de Atención],
		grpc.name [Nombre Grupo de Atención],
		CASE his.idetiphis 
			WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio)
			WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio)
			WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE numingres = orden.numingres AND numefolio = orden.numefolio) ELSE '' END AS [Justificación Clínica],
		'ODO' AS origen,  
		CONVERT(VARCHAR(19), GETDATE(), 120) AS createdat
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

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[ODO_Ordenamientos_Medicos] TO [usr_tramita]
    AS [dbo];
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consolida todos los ordenamientos médicos —laboratorios, imágenes, patologías, interconsultas, procedimientos quirúrgicos y no quirúrgicos, hemocomponentes, medicamentos convencionales y esquemas de quimioterapia— registrados en un rango de fechas dado. Unifica las órdenes en un único resultado, distinguiendo su modalidad de atención (intramural/extramural) y su estado de gestión mediante decodificación de valores numéricos. Adicionalmente incorpora información del censo de camas y datos del paciente, ingreso y diagnóstico para enriquecer el reporte final.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y retorna en una sola vista los ordenamientos médicos (intramurales y extramurales) generados en un rango de fechas, incluyendo servicios, productos y esquemas de quimioterapia con datos clínicos, demográficos y administrativos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requieren fechas de inicio y fin válidas para filtrar por fecha de orden/registro.; Las órdenes deben tener centro de atención, profesional y paciente registrados (joins INNER con adcenaten, inprofsal, inpacient, adingreso).; El profesional debe tener especialidad principal asociada (codespec1).; Los servicios deben existir como CUPS en contract.cupsentity.; Para esquemas de quimioterapia, debe existir relación entre hcordciclos y hcordmedicam por schemesid, idhcordquimio y ciclo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros se filtran por la ventana [@inidate, @enddate] sobre la fecha de orden/registro propia de cada fuente.; Los estados (estserips) se traducen de códigos numéricos a literales específicos por cada tipo de orden.; La edad del paciente se calcula como diferencia en años entre fecha de nacimiento y la fecha de la orden.; El campo origen siempre se marca como ''ODO''.; createdat refleja el momento de ejecución (GETDATE()).; Los medicamentos con idesquemaonc no nulo no se incluyen en la rama de medicamentos regulares para evitar duplicados con la rama de quimioterapia.; Las cantidades de hemocomponentes se agregan por COUNT de codserips y las de quimioterapia por SUM de cantidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ordenamientos médicos intramurales y extramurales; Laboratorios; Imágenes diagnósticas; Patologías; Interconsultas; Procedimientos quirúrgicos y no quirúrgicos; Hemocomponentes; Medicamentos; Esquemas de quimioterapia (ciclos); Órdenes de control; Censo hospitalario (camas y tipo de estancia); Diagnóstico principal (CIE); CUPS (códigos de servicios IPS); EPS / Administradora de salud; Grupo de atención; Régimen de afiliación (contributivo, subsidiado, vinculado, particular, desplazado); Tipo de documento del paciente; Justificación clínica (notas de urgencias/evolución); Especialidad médica; Centro de atención y unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna un único conjunto de resultados con ordenamientos médicos clasificados como Servicio (type=1), Producto (type=2) o Producto Quimioterapia (type=3) cuyas fechas estén entre @inidate y @enddate.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del ordenamiento (laboratorio, imagen, patología, interconsulta, procedimiento no quirúrgico, procedimiento quirúrgico, hemocomponente, control, medicamento, esquema oncológico) → Cada origen aporta su propio SELECT al UNION ALL con mapeo específico de estados (estserips) y su tabla fuente; si pres.idesquemaonc IS NULL en hcprescra → El medicamento se clasifica como prescripción regular (type=2) else Se excluye de la rama de medicamentos regulares; los esquemas oncológicos se traen vía CTE medicamentosquimo (type=3); si manextpro = 0 vs 1 → 0 → ''HOSPITALARIA''; 1 → ''EXTRAMURAL''; si Órdenes de control (hchispaca/hcdescoex) → Se marcan siempre como ''EXTRAMURAL'' independientemente de manextpro; si regestado = 1 en chregesta → Se incluye la cama y tipo de estancia del paciente en el censo activo else El paciente aparece sin información de cama/estancia (LEFT JOIN con censo); si his.idetiphis ∈ {hcurging1, hcurgevo1, hcnotevo1} → La justificación clínica se obtiene del campo analisisp de la tabla correspondiente al tipo de historia else La justificación clínica se devuelve como cadena vacía; si dxp.coddiapri = 1 → Se asocia el diagnóstico principal del ingreso al ordenamiento else No se asocia diagnóstico (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ehr.hcordciclos; ehr.hcordmedicam; ehr.schemes; dbo.chregesta; dbo.chcamasho; dbo.chtipesta; dbo.hcordlabo; dbo.hcordimag; dbo.hcordpato; dbo.hcordinte; dbo.hcordpron; dbo.hcordproq; dbo.hchispaca; dbo.hcdescoex; dbo.hcorhemco; dbo.hcorhemser; dbo.hcprescra; inventory.atc; contract.cupsentity; contract.cupsentitycontractdescriptions; contract.contractdescriptions; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso (+7 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos';
-- GO
