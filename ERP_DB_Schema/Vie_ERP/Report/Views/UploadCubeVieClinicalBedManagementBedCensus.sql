
CREATE view [Report].[UploadCubeVieClinicalBedManagementBedCensus] AS

WITH reservas AS 
	(

		SELECT * FROM dbo.chreserva WHERE fecreserv >= DATEADD(HH, -18, COMMON.GETDATE()) AND estadores NOT IN(3)

	), diagnosticos_odo AS 
	(

		SELECT 
			cen.numingres,
			ROW_NUMBER() OVER(PARTITION BY cen.numingres ORDER BY dxp.coddiapri DESC) AS numrow,
			dxp.coddiagno,
			RTRIM(dx.nomdiagno) AS nomdiagno, 
			dxp.coddiapri
		FROM dbo.indiagnop dxp
		INNER JOIN dbo.chregesta AS cen ON dxp.numingres = cen.numingres AND cen.regestado = 1
		INNER JOIN dbo.indiagnos AS dx ON dxp.coddiagno = dx.coddiagno 
	)

	SELECT
		COMMON.GETDATE() AS dateInsert,
		'8010007139' AS [CodUnidadNegocio],
		'ONCOLOGOS DEL OCCIDENTE' AS [NomUnidadNegocio],
		cat.codcenate AS [CodCentroAtencion],
		cat.nomcenate AS [NomCentroAtencion],
		ufu.ufucodigo AS [CodUniadFuncional],
		ufu.ufudescri AS [NomUnidadFuncional],
		tuf.tipounidadfuncional AS [TipoUnidadFuncional],
		UPPER(RTRIM(bed.desccamas)) AS [Cama],
		CASE bed.estadcama 
			WHEN 1 THEN 'Libre'
			WHEN 2 THEN 'Asignada'
			WHEN 3 THEN 'Inactiva'
			WHEN 4 THEN 'En Mantenimiento'
			WHEN 5 THEN 'En Aislamiento'
			WHEN 6 THEN 'Reservada sin Confirmar'
			WHEN 7 THEN 'Reservada Confirmada' END AS [EstadoCama],
		CASE rsv.estadores
			WHEN 1 THEN 'Reservada sin Confirmar'
			WHEN 2 THEN 'Reservada Confirmada' END AS [EstadoReserva],
		ing.numingres AS [NroIngreso],
		cen.feciniest AS [FecIniciaEstancia],
		DATEDIFF(DAY, cen.feciniest, COMMON.GETDATE()) AS [DiasEstancia],
		cte.destipest AS [TipoEstancia],
		egr.fecaltpac AS [FecAltaMedica],
		pac.ipcodpaci AS [NroDocumento],
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
			WHEN 15 THEN 'SI' END AS [TipoDocumento],
		RTRIM(pac.ipnomcomp) AS [NombrePaciente],
		DATEDIFF(YEAR, pac.ipfecnaci, COMMON.GETDATE()) AS [Edad],
		ha.code AS [CodEPS],
		RTRIM(ha.name) [NomEPS],
		cg.code AS [CodGrpAtencion],
		RTRIM(cg.name) AS [NomGrpAtencion],
		CASE ha.entitytype
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
			WHEN 12 THEN 'Otros' END AS [Regimen],
		dx.coddiagno AS [CodDiagnostico],
		dx.nomdiagno AS [NomDiagnostico],
		med.codprosal AS [CodMedico],
		RTRIM(med.nommedico) AS [NomMedico],
		RTRIM(esp.desespeci) AS [Especialidad]
	FROM dbo.chcamasho AS bed
	LEFT JOIN dbo.chregesta AS cen ON bed.codicamas = cen.codicamas AND cen.regestado = 1
	LEFT JOIN dbo.adingreso AS ing ON cen.numingres = ing.numingres
	LEFT JOIN dbo.adcenaten AS cat ON bed.codcenate = cat.codcenate 
	LEFT JOIN dbo.inunifunc AS ufu ON bed.ufucodigo = ufu.ufucodigo 
	LEFT JOIN dbo.chtipesta AS cte ON cen.codtipest = cte.codtipest 
	LEFT JOIN dbo.hcregegre AS egr ON cen.numingres = egr.numingres
	LEFT JOIN dbo.inpacient AS pac ON ing.ipcodpaci = pac.ipcodpaci
	LEFT JOIN contract.healthadministrator AS ha ON ing.genconentity = ha.id
	LEFT JOIN contract.caregroup AS cg  ON ing.gencaregroup = cg.id
	LEFT JOIN diagnosticos_odo AS dx ON cen.numingres = dx.numingres AND dx.numrow = 1
	LEFT JOIN dbo.inprofsal AS med ON cen.codprosal = med.codprosal 
	LEFT JOIN dbo.inespecia AS esp ON cen.codespeci = esp.codespeci
	LEFT JOIN Report.Table_TIPO_UNIDADES_FUNCIONALES AS tuf ON ufu.ufutipuni = tuf.id
	LEFT JOIN reservas AS rsv ON bed.codicamas = rsv.codicamas
	WHERE bed.codcenate NOT IN ('13032') AND bed.codicamas NOT IN (301, 302, 308, 309, 326, 327, 306, 307, 303, 304, 305)