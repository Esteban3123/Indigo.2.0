

--/****** Object:  StoredProcedure [dbo].[ODO_UN_Censo]    Script Date: 16/05/2024 04:17:14 p. m. ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

---- =============================================
---- Author:		<Author,,Name>
---- Create date: <Create Date,,>
---- Description:	<Description,,>
---- =============================================
--CREATE PROCEDURE [dbo].[ODO_UN_Censo]
--	-- Add the parameters for the stored procedure here

--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

CREATE PROCEDURE [Report].[SP_V2_CARGAR_CENSO_ODO]
AS

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

 INSERT INTO Report.Table_CENSO
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
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que carga el censo hospitalario del módulo de Odontología de "Oncólogos del Occidente" en la tabla `Report.Table_CENSO`. Consolida el estado actual de camas (libre, asignada, en mantenimiento, reservada, etc.), ingresos activos, datos demográficos del paciente, EPS/régimen, grupo de atención, diagnóstico principal más reciente y médico tratante. Excluye determinados centros de atención y camas específicas, y considera reservas de las últimas 18 horas para reflejar el estado de ocupación vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Carga un censo hospitalario consolidado de camas, ocupación, pacientes, diagnósticos y reservas para la unidad de negocio Oncólogos del Occidente, dejándolo en una tabla de reportería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las camas a reportar deben pertenecer a centros de atención distintos de ''13032''.; Se excluyen las camas con códigos 301..309 y 326, 327 (camas no consideradas en el censo).; Para tomar la estancia activa de un ingreso se exige chregesta.regestado = 1.; Para considerar una reserva vigente, fecreserv debe ser >= ahora - 18 horas y estadores distinto de 3 (cancelada/anulada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La unidad de negocio queda fija como ''8010007139'' / ''ONCOLOGOS DEL OCCIDENTE'' para todas las filas insertadas.; El campo dateInsert se establece con COMMON.GETDATE() al momento de la inserción.; Los días de estancia se calculan como DATEDIFF(DAY, feciniest, ahora).; La edad se calcula como DATEDIFF(YEAR, ipfecnaci, ahora).; Solo se considera un diagnóstico por ingreso (numrow = 1).; Reservas canceladas (estadores = 3) nunca se cruzan al censo.; Las camas del centro ''13032'' y las camas con id 301-309, 326, 327 nunca aparecen en el censo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'censo hospitalario; cama; estado de cama; reserva de cama; ingreso; estancia; egreso/alta médica; paciente; diagnóstico principal; EPS / responsable de pago; régimen de afiliación; grupo de atención; unidad funcional; centro de atención; médico tratante; especialidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Report.Table_CENSO: Inserta una fila por cada cama de chcamasho que no esté excluida (codcenate <> ''13032'' y codicamas no en lista excluida), enriquecida con datos de estancia activa (regestado=1), ingreso, paciente, EPS, grupo de atención, diagnóstico principal odontológico, médico, especialidad, tipo de unidad funcional y reserva vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si bed.estadcama IN (1..7) → Mapea a etiquetas de estado: 1=Libre, 2=Asignada, 3=Inactiva, 4=En Mantenimiento, 5=En Aislamiento, 6=Reservada sin Confirmar, 7=Reservada Confirmada.; si rsv.estadores IN (1,2) → Etiqueta reserva como ''Reservada sin Confirmar'' (1) o ''Reservada Confirmada'' (2); otros valores quedan NULL.; si pac.iptipodoc IN (1..15) → Traduce a códigos de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si ha.entitytype IN (1..12) → Clasifica el régimen del responsable de pago (EPS Contributivo/Subsidiado, ET, ARL, MP, IPS Pública/Privada, Régimen Especial, SOAT, Fosyga, Otros).; si ROW_NUMBER() PARTITION BY numingres ORDER BY coddiapri DESC = 1 → Selecciona un único diagnóstico por ingreso priorizando el de mayor coddiapri (diagnóstico principal).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.chreserva; dbo.indiagnop; dbo.chregesta; dbo.indiagnos; dbo.chcamasho; dbo.adingreso; dbo.adcenaten; dbo.inunifunc; dbo.chtipesta; dbo.hcregegre; dbo.inpacient; contract.healthadministrator; contract.caregroup; dbo.inprofsal; dbo.inespecia; Report.Table_TIPO_UNIDADES_FUNCIONALES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_UN_Censo';
-- GO
