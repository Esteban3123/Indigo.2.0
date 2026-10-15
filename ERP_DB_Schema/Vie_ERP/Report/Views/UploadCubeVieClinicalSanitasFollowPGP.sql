
CREATE view [Report].[UploadCubeVieClinicalSanitasFollowPGP] AS

    -- Insert statements for procedure here
	WITH servprestados AS 
	(
		SELECT
			'SI' AS [Facturado],
			ing.numingres,
			RTRIM(grp.code) + ' - ' + grp.name AS [GrupoAtencion],
			cdes.code,
			ca.depmuncod AS [CodMunicipioDANE],
			SUBSTRING(ca.codipssec, 0, 11) AS [CodHabilitacionIPS],
			SUBSTRING(ca.codipssec, 11, 12) AS [CodigoSede],
			CASE inv.patienttype WHEN 0 THEN '10' WHEN 1 THEN '05' ELSE '' END AS [CodPlan],
			CONVERT(CHAR(10), invd.servicedate, 103) AS [FechaAtencion],
			CONVERT(CHAR(5), invd.servicedate, 108) AS [HoraConsultaExamen],
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
				WHEN 15 THEN 'SI' END AS [TipoId],
			pac.ipcodpaci AS [NumeroId],
			pac.ipprinomb AS [PrimerNombre],
			pac.ipsegnomb AS [SegundoNombre],
			pac.ippriapel AS [PrimerApellido],
			pac.ipsegapel AS [SegundoApellido],
			CASE pac.ipsexopac WHEN 1 THEN 'MASCULINO' WHEN 2 THEN 'FEMENINO' END AS Sexo,
			CONVERT(CHAR(10), pac.ipfecnaci, 103) AS [FechaNacimiento],
			CASE ing.tipoingre WHEN 1 THEN 'A' WHEN 2 THEN 'H' END AS [Ambito], 
			COALESCE(cups.code, prod.codecum, prod.code) AS [CodServicioPrestado],
			CASE  
				WHEN (cups.servicetype IS NULL) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 1) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 2) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 3) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 4 AND ipsss.code IS NULL) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 5 AND ipsss.code IS NULL) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 6) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 7) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 8) THEN invd.invoicedquantity
				WHEN (cups.servicetype = 9) THEN invd.invoicedquantity
				WHEN ROW_NUMBER() OVER(PARTITION BY cups.code, inv.status, inv.invoicenumber, inv.admissionnumber, inv.patientcode, srv.orderdate, srvd.id ORDER BY inv.status, inv.invoicenumber) = 1 AND cups.servicetype = 4 AND ipsss.code IS NOT NULL THEN 1
				WHEN ROW_NUMBER() OVER(PARTITION BY cups.code, inv.status, inv.invoicenumber, inv.admissionnumber, inv.patientcode, srv.orderdate, srvd.id ORDER BY inv.status, inv.invoicenumber) > 1 AND cups.servicetype = 4 AND ipsss.code IS NOT NULL THEN 0
				WHEN ROW_NUMBER() OVER(PARTITION BY cups.code, inv.status, inv.invoicenumber, inv.admissionnumber, inv.patientcode, srv.orderdate, srvd.id ORDER BY inv.status, inv.invoicenumber) > 1 AND cups.servicetype = 5 AND ipsss.code IS NOT NULL THEN 0
				WHEN ROW_NUMBER() OVER(PARTITION BY cups.code, inv.status, inv.invoicenumber, inv.admissionnumber, inv.patientcode, srv.orderdate, srvd.id ORDER BY inv.status, inv.invoicenumber) = 1 AND cups.servicetype = 5 AND ipsss.code IS NOT NULL THEN 1  END AS [Cantidad],
			CONVERT(CHAR(10), ing.ifechaing, 103) AS [FechaIngreso],
			CONVERT(CHAR(10), inv.outputdate, 103) AS [FechaEgreso],
			DATEDIFF(DAY, ing.ifechaing, inv.outputdate) AS [DiasEstancia],
			RTRIM(esp.desespeci) AS [Especialidad],
			CASE WHEN cdes.name IS NOT NULL THEN cdes.name ELSE COALESCE(cups.description, prod.name) END AS [Procedimiento],
			CASE hegr.estpacegr 
				WHEN 1 THEN 'CA'
				WHEN 2 THEN 'CA' 
				WHEN 3 THEN 'FA' 
				WHEN 4 THEN 'R' 
				WHEN 5 THEN 'D' ELSE 'OT' END AS [TipoEgreso], -- CA: Casa - FA: Fallecido - R: Remision - D: Atención domiciliaria (PHD) - OT: Otros
			'EG' AS [Origen],
			'' AS [TipoDocumentoMedicoOrdena], 
			'' AS [NroIdentificacionMedicoOrdena],
			'' AS [NombreMedicoOrdena],
			'' AS [TipoDocumentoPrestadorOrdena],
			'' AS [NroIdentificacionPrestadorOrdena],
			NULL AS [TipoRecaudo],
			'' AS [CodRecaudo],
			'' AS [CostoCuotaModeradoraCopago],
			'' AS [NroFactura],
			'' AS [Observaciones],
			CAST(inv.invoicedate AS DATE) 'FECHA BUSQUEDA'
		FROM billing.invoice AS inv
		INNER JOIN dbo.adingreso AS ing ON inv.admissionnumber = ing.numingres  
		INNER JOIN dbo.adcenaten AS ca ON ing.codcenate = ca.codcenate 
		INNER JOIN billing.invoicedetail AS invd ON inv.id = invd.invoiceid 
		INNER JOIN billing.serviceorderdetail AS srvd ON invd.serviceorderdetailid = srvd.id
		LEFT JOIN billing.serviceorder AS srv ON srvd.serviceorderid = srv.id
		LEFT JOIN billing.serviceorderdetailsurgical AS srvs ON srvd.id = srvs.serviceorderdetailid AND srvs.onlymedicalfees = 0
		LEFT JOIN contract.ipsservice AS ipsss ON srvs.ipsserviceid = ipsss.id
		LEFT JOIN contract.cupsentity AS cups ON srvd.cupsentityid = cups.id
		LEFT JOIN contract.ipsservice AS ipss ON srvd.ipsserviceid = ipss.id
		LEFT JOIN contract.cupsentitycontractdescriptions AS ccd ON srvd.cupsentitycontractdescriptionid = ccd.id
		LEFT JOIN contract.contractdescriptions AS cdes ON ccd.contractdescriptionid = cdes.id
		LEFT JOIN inventory.inventoryproduct AS prod ON srvd.productid = prod.id 
		INNER JOIN dbo.inpacient AS pac ON inv.patientcode = pac.ipcodpaci
		LEFT JOIN dbo.inprofsal AS med ON srvd.performshealthprofessionalcode = med.codprosal 
		LEFT JOIN dbo.inespecia AS esp ON med.codespec1 = esp.codespeci
		LEFT JOIN dbo.hcregegre AS hegr ON inv.admissionnumber = hegr.numingres 
		LEFT JOIN contract.caregroup AS grp ON inv.caregroupid = grp.id
		WHERE inv.status IN (1) AND inv.documenttype = 5 AND inv.caregroupid IN (428,429,430,431) AND YEAR(inv.invoicedate)>=2023
		--CAST(inv.invoicedate AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 

		SELECT
			'NO' AS [Facturado],
			ing.numingres,
			RTRIM(grp.code) + ' - ' + grp.name AS [GrupoAtencion],
			cdes.code,
			ca.depmuncod AS [CodMunicipioDANE],
			SUBSTRING(ca.codipssec, 0, 11) AS [CodHabilitacionIPS],
			SUBSTRING(ca.codipssec, 11, 12) AS [CodigoSede],
			CASE pac.iptipopac WHEN 0 THEN '10' WHEN 1 THEN '05' ELSE '' END AS [CodPlan],
			CONVERT(CHAR(10), srvd.servicedate, 103) AS [FechaAtencion],
			CONVERT(CHAR(5), srvd.servicedate, 108) AS [HoraConsultaExamen],
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
				WHEN 15 THEN 'SI' END AS [TipoId],
			pac.ipcodpaci AS [NumeroId],
			pac.ipprinomb AS [PrimerNombre],
			pac.ipsegnomb AS [SegundoNombre],
			pac.ippriapel AS [PrimerApellido],
			pac.ipsegapel AS [SegundoApellido],
			CASE pac.ipsexopac WHEN 1 THEN 'MASCULINO' WHEN 2 THEN 'FEMENINO' END AS Sexo,
			CONVERT(CHAR(10), pac.ipfecnaci, 103) AS [FechaNacimiento],
			CASE ing.tipoingre WHEN 1 THEN 'A' WHEN 2 THEN 'H' END AS [Ambito], 
			COALESCE(cups.code, prod.codecum, prod.code) AS [CodServicioPrestado],
			ISNULL(srvq.invoicedquantity, srvd.invoicedquantity) AS [Cantidad],
			CONVERT(CHAR(10), ing.ifechaing, 103) AS [FechaIngreso],
			CONVERT(CHAR(10), IIF (ing.tipoingre = 1 AND cups.servicetype IN (1, 2, 3, 8) AND egr.fecaltpac IS NULL, DATEADD(MINUTE, 10, ing.ifechaing), egr.fecaltpac), 103) AS [FechaEgreso],
			DATEDIFF(DAY, ing.ifechaing, IIF (ing.tipoingre = 1 AND cups.servicetype IN (1, 2, 3, 8) AND egr.fecaltpac IS NULL, DATEADD(MINUTE, 10, ing.ifechaing), egr.fecaltpac)) AS [Días de estancia],
			RTRIM(esp.desespeci) AS [Especialidad],
			CASE WHEN cdes.name IS NOT NULL THEN cdes.name ELSE COALESCE(cups.description, prod.name) END AS [Procedimiento],
			CASE hegr.estpacegr 
				WHEN 1 THEN 'CA'
				WHEN 2 THEN 'CA' 
				WHEN 3 THEN 'FA' 
				WHEN 4 THEN 'R' 
				WHEN 5 THEN 'D' ELSE 'OT' END AS [TipoEgreso], -- CA: Casa - FA: Fallecido - R: Remision - D: Atención domiciliaria (PHD) - OT: Otros
			'EG' AS [Origen],
			'' AS [TipoDocumentoMedicoOrdena], 
			'' AS [NroIdentificacionMedicoOrdena],
			'' AS [NombreMedicoOrdena],
			'' AS [TipoDocumentoPrestadorOrdena],
			'' AS [NroIdentificacionPrestadorOrdena],
			NULL AS [TipoRecaudo],
			'' AS [CodRecaudo],
			'' AS [CostoCuotaModeradoraCopago],
			'' AS [NroFactura],
			'' AS [Observaciones],
			CAST(ing.ifechaing AS DATE) 'FECHA BUSQUEDA'
		FROM billing.revenuecontroldetail AS rcd
		INNER JOIN billing.revenuecontrol AS rcn ON rcd.revenuecontrolid = rcn.id
		INNER JOIN billing.serviceorderdetaildistribution AS sodd ON rcd.id = sodd.revenuecontroldetailid 
		INNER JOIN dbo.adingreso AS ing ON rcn.admissionnumber = ing.numingres AND ing.iestadoin NOT IN ('A', 'F', 'C') AND YEAR(ing.ifechaing)>=2023
		--CAST(ing.ifechaing AS DATE) BETWEEN @ini_date AND @end_date
		INNER JOIN dbo.adcenaten AS ca ON ing.codcenate = ca.codcenate 
		INNER JOIN billing.serviceorderdetail AS srvd ON sodd.serviceorderdetailid = srvd.id AND srvd.isdelete = 0
		LEFT JOIN billing.serviceorderdetailsurgical AS srvq ON srvd.id = srvq.serviceorderdetailid 
		LEFT JOIN dbo.hcregegre AS egr ON ing.numingres = egr.numingres
		LEFT JOIN contract.cupsentity AS cups ON srvd.cupsentityid = cups.id
		LEFT JOIN contract.ipsservice AS ipss ON srvd.ipsserviceid = ipss.id
		LEFT JOIN contract.cupsentitycontractdescriptions AS ccd ON srvd.cupsentitycontractdescriptionid = ccd.id
		LEFT JOIN contract.contractdescriptions AS cdes ON ccd.contractdescriptionid = cdes.id
		LEFT JOIN inventory.inventoryproduct AS prod ON srvd.productid = prod.id 
		INNER JOIN dbo.inpacient AS pac ON rcn.patientcode = pac.ipcodpaci
		LEFT JOIN dbo.inprofsal AS med ON srvd.performshealthprofessionalcode = med.codprosal 
		LEFT JOIN dbo.inespecia AS esp ON med.codespec1 = esp.codespeci
		LEFT JOIN dbo.hcregegre AS hegr ON rcn.admissionnumber = hegr.numingres 
		LEFT JOIN contract.caregroup AS grp ON rcd.caregroupid = grp.id
		WHERE rcd.status IN (1, 3) AND rcd.caregroupid IN (428,429,430,431) 
	),
	
	pacientes AS 
	(
		SELECT DISTINCT [TipoId], [NumeroId] FROM servprestados
	),
	
	diagnosticoscac AS 
	(
		SELECT 
			ROW_NUMBER() OVER(PARTITION BY dxcac.[5], dxcac.[6] ORDER BY dxcac.id DESC) AS rownumber, dxcac.[5], dxcac.[6], dxcac.[17], dxcac.[18] 
		FROM dbo.hconcopreg AS dxcac 
		INNER JOIN pacientes AS pac ON  dxcac.[6] = pac.[NumeroId]
	)

	SELECT distinct CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		srvp.[Facturado]  'FACTURADO',
		srvp.numingres 'NRO INGRESO',
		srvp.[GrupoAtencion] 'GRUPO ATENCION',
		grp.agrupadorNT 'AGRUPADOR',
		srvp.[CodMunicipioDANE] 'CODIGO MUNICIPIO DANE',
		srvp.[CodHabilitacionIPS] 'CODIGO HABILITACION IPS',
		srvp.[CodigoSede] 'CODIGO SEDE',
		srvp.[CodPlan] 'CODIGO PLAN',
		srvp.[FechaAtencion] 'FECHA ATENCION',
		srvp.[HoraConsultaExamen] 'HORA CONSULTA EXAMEN',
		srvp.[TipoId] 'TIPO ID',
		srvp.[NumeroId]'NUMERO ID' ,
		srvp.[PrimerNombre] 'PRIMER NOMBRE',
		srvp.[SegundoNombre] 'SEGUNDO NOMBRE',
		srvp.[PrimerApellido] 'PRIMER APELLIDO',
		srvp.[SegundoApellido] 'SEGUNDO APELLIDO',
		srvp.[Sexo] 'SEXO',
		srvp.[FechaNacimiento] 'FECHA NACIMIENTO',
		srvp.[Ambito] 'AMBITO', 
		srvp.[CodServicioPrestado] 'CODIGO SERVICIO PRESTADO',
		srvp.[Cantidad] 'CANTIDAD',
		srvp.[FechaIngreso] 'FECHA INGRESO',
		srvp.[FechaEgreso] 'FECHA EGRESO',
		srvp.[DiasEstancia] 'DIAS ESTANCIA',
		srvp.[Especialidad] 'ESPECIALIDAD',
		srvp.[Procedimiento] 'PROCEDIMIENTO',
		srvp.[TipoEgreso] 'TIPO EGRESO',
		srvp.[Origen] 'ORIGEN',
		dxcac.[17] 'CODIGO DIAGNOSTICO PRINCIPAL', 
		CONVERT(VARCHAR(10), dxcac.[18], 103) 'FECHA DIAGNOSTICO', 
		''  'CODIGO DIAGNOSTICO II',
		srvp.[TipoDocumentoMedicoOrdena] 'TIPO DOCUMENTO MEDICO ORDENA', 
		srvp.[NroIdentificacionMedicoOrdena] 'NRO IDENTIFICACION MEDICO ORDENA',
		srvp.[NombreMedicoOrdena] 'NOMBRE MEDICO ORDENA',
		srvp.[TipoDocumentoPrestadorOrdena] 'TIPO DOCUMENTO PRESTADOR ORDENA',
		srvp.[NroIdentificacionPrestadorOrdena] 'NRO IDENTIFICACION PRESTADOR ORDENA', 
		grp.valor 'COSTO SERVICIO',
		srvp.[TipoRecaudo] 'TIPO RECAUDO',
		srvp.[CodRecaudo] 'CODIGO RECAUDO',
		srvp.[CostoCuotaModeradoraCopago] 'COSTO CUOTA MODERADORA COPAGO',
		srvp.[NroFactura] 'NRO FACTURA',
		srvp.[Observaciones] 'OBSERVACIONES',
		srvp.[FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM servprestados AS srvp 
	LEFT JOIN diagnosticoscac AS dxcac ON srvp.[NumeroId] = dxcac.[6] AND srvp.[TipoId] = dxcac.[5] AND dxcac.rownumber = 1
	LEFT JOIN Report.Tableagrupadoressanitas AS grp ON srvp.[CodServicioPrestado] = grp.cups AND srvp.code = grp.codigodescripcionrelacionada