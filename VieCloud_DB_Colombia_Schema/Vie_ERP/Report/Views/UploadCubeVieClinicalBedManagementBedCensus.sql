
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de aplanado para carga a cubo OLAP que consolida el censo de camas de la institución "Oncólogos del Occidente". Combina el estado actual de cada cama (libre, asignada, en mantenimiento, aislamiento, reservada, etc.) con datos de la estancia activa: paciente, días de hospitalización, EPS, régimen, grupo de atención, diagnóstico principal y médico tratante. Incluye reservas de las últimas 18 horas y excluye un centro de atención y camas específicas mediante filtros fijos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida un censo de camas hospitalarias enriquecido con información de ocupación, paciente, diagnóstico principal, médico tratante, EPS y reservas vigentes, para cargue a cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las camas deben pertenecer a un centro de atención distinto de ''13032''; Las camas con código 301-309, 326, 327 (rangos específicos) son excluidas del censo; Para considerar registro de estancia activo se requiere chregesta.regestado = 1; Para considerar diagnóstico principal del ingreso se requiere chregesta.regestado = 1 y se toma el de mayor coddiapri (ROW_NUMBER ORDER BY coddiapri DESC = 1); Las reservas se filtran a fecreserv >= hace 18 horas y estadores distinto de 3 (cancelada/anulada)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La unidad de negocio se rotula fija como ''8010007139 - ONCOLOGOS DEL OCCIDENTE''; Solo se considera el diagnóstico principal con mayor coddiapri por ingreso (numrow=1); Los días de estancia se calculan desde feciniest hasta la fecha actual del sistema (COMMON.GETDATE); La edad se calcula en años calendario entre ipfecnaci y la fecha actual; Se excluyen sistemáticamente las camas del centro ''13032'' y un set fijo de códigos de cama; Las reservas consideradas vigentes son aquellas dentro de las últimas 18 horas y no en estado 3', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Censo de camas; Estancia; Ingreso; Egreso/Alta médica; Reserva de cama; Paciente; Diagnóstico principal; Médico tratante; Especialidad; EPS / Administradora de salud; Régimen de afiliación; Centro de atención; Unidad funcional; Grupo de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalBedManagementBedCensus: Devuelve una fila por cama (chcamasho) no excluida, con su estado, ocupación actual, datos del paciente ingresado, diagnóstico principal, médico, especialidad, EPS, grupo de atención y reserva vigente si existe.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si bed.estadcama IN (1..7) → Mapea a etiqueta textual: 1=Libre, 2=Asignada, 3=Inactiva, 4=En Mantenimiento, 5=En Aislamiento, 6=Reservada sin Confirmar, 7=Reservada Confirmada; si rsv.estadores IN (1,2) → Mapea estado de reserva: 1=Reservada sin Confirmar, 2=Reservada Confirmada; si pac.iptipodoc IN (1..15) → Mapea el tipo de documento a códigos estándar colombianos (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si ha.entitytype IN (1..12) → Mapea el régimen del pagador: EPS Contributivo/Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de Tránsito, Fosyga, Otros', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.chreserva; dbo.indiagnop; dbo.chregesta; dbo.indiagnos; dbo.chcamasho; dbo.adingreso; dbo.adcenaten; dbo.inunifunc; dbo.chtipesta; dbo.hcregegre; dbo.inpacient; contract.healthadministrator; contract.caregroup; dbo.inprofsal; dbo.inespecia; Report.Table_TIPO_UNIDADES_FUNCIONALES', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementBedCensus';
GO
