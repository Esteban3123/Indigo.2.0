CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosPacientesAmbulatorio]
(
	@Estado varchar(100),
	@CentroAtencion Char(500)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Hoy DATETIME = common.GETDATE()
	DECLARE @FechaInicio DATETIME = DATEADD(MONTH, -4, @Hoy)
	DECLARE @FechaFin DATETIME = @Hoy

	DECLARE @Estados TABLE (Value VARCHAR(50))
	INSERT INTO @Estados
	SELECT Value FROM dbo.splitstring(@Estado)
	DECLARE @Centros TABLE (Value VARCHAR(50))
	INSERT INTO @Centros
	SELECT Value FROM dbo.splitstring(@CentroAtencion)

 
    -- Insert statements for procedure here
 
	SELECT DISTINCT
		A.[AUTO], 
		A.FECORDMED AS 'FechaSolicitud', 
		A.FECRECMUE AS 'FechaRecoleccion',
		RTRIM(A.IPCODPACI) AS 'CodigoPaciente', 
		RTRIM(B.IPNOMCOMP) AS 'NombrePaciente', 
		B.IPFECNACI AS 'FechaNacimiento', 
		E.UFUACTPAC AS 'CodigoUnidad', 
		RTRIM(C.UFUDESCRI) AS 'DescripcionUnidad', 
		A.CODCENATE AS 'CodigoCentro',
		CASE A.ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Muestra recolectada' WHEN 3 THEN 'Pendiente interpretación' WHEN 4 THEN 'Examen interpretado' WHEN 5 THEN 'Estudio remitido' 
		WHEN 6 THEN 'Anulado' WHEN 7 THEN 'Extramural' WHEN 8 THEN 'Muestra recolectada parcialmente' WHEN 9 THEN 'Muestra no conforme' END AS 'Estado', 
		A.ESTALELAB AS 'EstadoAlerta', 
		A.NUMINGRES AS 'Ingreso', 

		M.IDMuestra,
		RTRIM(A.CODSERIPS) AS CodigoServicio,
		RTRIM(D.DESSERIPS) + '. ' + ISNULL(CD.name,'') AS DescripcionServicio, 
		ISNULL(CD.name,'') AS DescripcionRelacionada,
		dbo.edad(B.IPFECNACI, @Hoy) AS Edad,

		B.CORELEPAC, 
		b.IPDIRECCI AS 'Direccion', 
		b.IPTELEFON AS 'Telefono', 
		b.IPSEXOPAC  AS 'Sexo', 
		b.IPRHSANGR AS 'Rh', 
		b.IPGRUPSAN AS 'GrupoSanguineo', 
		b.IPTELMOVI AS 'Movil', 
		b.CORELEPAC AS 'Correo', 
		H.NOMMEDICO  AS 'Medico', 
		J.LABRECMUE AS 'TiempoMaximo', 
		J.UNITIEREC AS 'UnidadTiempo', 
		0 AS 'Minutos', 
		CAST('' AS CHAR) AS 'Barra', 
		LABENTRES AS 'TiempoResultado', 
		UNITIEENT AS 'UnidadResultado', 
		CAST('' AS BIT) AS 'Marcar', 
		a.CONCURRE AS 'Concurrencia', 
		E.UFUCODIGO, 
		A.CODPROSAL,
		IPTIPODOC, 
		B.IPPRINOMB, 
		B.IPSEGNOMB, 
		B.IPPRIAPEL, 
		B.IPSEGAPEL, 
		B.CODENTIDA, 
		B.IPTIPOPAC, 
		B.IPTIPOAFI, 
		B.IPTELEFON, 
		B.IPTELMOVI, 
		B.IPDIRECCI, 
		A.CANSERIPS, 
		D.CODGOCUPS, 

		A.OBSERVACI AS 'ObservacionServicio', 
		RTRIM(MUN.DEPMUNCOD) AS 'MunicipioCodigo',
		RTRIM(MUN.MUNNOMBRE) AS 'MunicipioNombre', 
		RTRIM(ENT.CODENTIDA) AS 'entidadpagadoraCodigo', 
		RTRIM(ENT.NOMENTIDA) AS 'entidadpagadoraNombre', 
		E.CODTIPPAC, 
		'' AS 'CodigoDiagnosticoSegundo', 
		'' AS 'NombreDiagnosticoSegundo', 
		'' AS 'CodigoDiagnosticoTercero', 
		'' AS 'NombreDiagnosticoTercero', 
		'' AS 'Prioridad', 
		RP.GESTACION, 
		A.NUMCONCIT, 
		RTRIM(CA.NOMCENATE) AS 'CentroAtencion', 
		RTRIM(G.DESCCAMAS) AS 'Cama', 
		CASE WHEN ce.FinancedResourceUPC = '1' THEN 'Si' ELSE 'No' END AS 'FinanciadoUPC',
		A.CODMOTIVOMUESTRANOCONFORME AS 'CodigoMotivoMuestraNoConforme',
		RTRIM(MA.DESMOTANU) AS 'DescripcionMotivoMuestraNoConforme',
		RTRIM(CenRem.DESCENLAB) AS 'CentroRemision'
		,[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,A.NUMINGRES,2) AS IconoEscalas
	FROM  
		dbo.AMBORDLAB AS A  WITH(NOLOCK) 

		INNER JOIN @Estados ES ON A.ESTSERIPS = ES.Value
		INNER JOIN @Centros CAf ON A.CODCENATE = CAf.Value

		INNER JOIN dbo.INPACIENT AS B  WITH(NOLOCK) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN dbo.INCUPSIPS AS D  WITH(NOLOCK) ON A.CODSERIPS = D .CODSERIPS 
		INNER JOIN CONTRACT.CUPSEntity ce ON A.CODSERIPS = ce.Code 
		INNER JOIN dbo.ADINGRESO AS E  WITH(NOLOCK) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN dbo.INENTIDAD AS ENT  WITH(NOLOCK) ON ENT.CODENTIDA = E.CODENTIDA 
		INNER JOIN dbo.INUNIFUNC AS C  WITH(NOLOCK) ON E.UFUACTPAC = C.UFUCODIGO 
		LEFT OUTER JOIN dbo.INPROFSAL AS H  WITH(NOLOCK) ON A.CODPROSAL =H.CODPROSAL  
		LEFT OUTER JOIN dbo.HCPARALELAB AS J  WITH(NOLOCK) ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE = @CentroAtencion 
		INNER JOIN INUBICACI UBI  WITH(NOLOCK) ON B.AUUBICACI = UBI.AUUBICACI 
		INNER JOIN INMUNICIP MUN  WITH(NOLOCK) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD 
		LEFT JOIN dbo.HCRIESGOSP AS RP  WITH(NOLOCK) ON RP.NUMINGRCES = A.NUMINGRES 
		

		LEFT JOIN (
			SELECT autolabor, MIN(CODCONCEC) AS IDMuestra
			FROM dbo.INTERDETA WITH(NOLOCK)
			WHERE ORDTIP = 'AMB'
			GROUP BY autolabor
		) M ON M.autolabor = A.AUTO

		
		LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.ContractDescriptions  CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
		INNER JOIN ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE 
		LEFT JOIN dbo.CHCAMASHO AS G WITH(NOLOCK)  ON E.CODCAMACT = G.CODICAMAS
		LEFT JOIN HCMOANULB MA WITH(NOLOCK) ON MA.CODMOTANU = A.CODMOTIVOMUESTRANOCONFORME
		LEFT JOIN HCLABREMI Rem WITH(NOLOCK) ON  rem.IPCODPACI = A.IPCODPACI AND rem.NUMINGRES = A.NUMINGRES AND rem.CODSERIPS = A.CODSERIPS   AND Rem.NUMEFOLIO IS NULL
	    LEFT JOIN HCCENLABU CenRem WITH(NOLOCK) ON CenRem.CODCENLAB = Rem.CODCENLAB 
	WHERE 
		A.FECORDMED >= @FechaInicio 
		AND A.FECORDMED < DATEADD(DAY, 1, @FechaFin)

	ORDER BY A.FECORDMED DESC
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de exámenes de laboratorio ambulatorio pendientes o en proceso para un centro de atención, combinando datos del paciente (cédula, nombre, edad, contacto, grupo sanguíneo), el ingreso o episodio de atención, el servicio CUPS solicitado, el médico solicitante, los tiempos máximos de recolección y entrega de resultados, la entidad pagadora (EPS/aseguradora), la unidad funcional donde se encuentra el paciente, la cama asignada, el municipio de residencia, el estado de la orden (solicitado, muestra recolectada, interpretado, anulado, etc.) y el centro de remisión externo si aplica. Se usa en el módulo de laboratorio ambulatorio para que el personal de toma de muestras y los laboratoristas gestionen la lista de trabajo del día, filtrada por estado de la orden y centro de atención, mostrando únicamente órdenes de los últimos 4 meses. Integra las tablas de órdenes de laboratorio (AMBORDLAB), pacientes (INPACIENT), ingresos (ADINGRESO), catálogo CUPS (INCUPSIPS y CUPSEntity), unidades funcionales (INUNIFUNC), profesionales de la salud (INPROFSAL), parámetros de tiempos de laboratorio (HCPARALELAB) y entidades pagadoras (INENTIDAD), entre otras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio ambulatorio de los últimos 4 meses, filtradas por estado(s) y centro(s) de atención, con datos del paciente, servicio, ingreso, entidad pagadora, muestra, tiempos y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los parámetros de estado y centro de atención deben venir como cadenas separadas (parseables por dbo.splitstring).; Debe existir la función common.GETDATE() para obtener la fecha actual del sistema.; Las funciones dbo.edad y dbo.RiskFactorAlert deben estar disponibles.; Cada orden debe tener paciente (INPACIENT), servicio CUPS (INCUPSIPS y CONTRACT.CUPSEntity), ingreso (ADINGRESO), entidad pagadora, unidad funcional, ubicación y municipio para aparecer en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes con fecha de orden médica dentro de una ventana móvil de los últimos 4 meses hasta hoy inclusive.; El filtrado por estado y centro de atención es obligatorio (INNER JOIN contra las listas parseadas).; Solo se incluyen detalles de muestra de tipo ambulatorio (ORDTIP=''AMB'') al calcular IDMuestra.; La edad se calcula respecto a la fecha actual del sistema (common.GETDATE).; Los campos de diagnóstico secundario/terciario y prioridad se devuelven siempre vacíos (no se calculan).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio ambulatorio; Estado de orden/examen; Muestra de laboratorio; Motivo de muestra no conforme; Centro de atención; Centro de remisión de laboratorio; Paciente; Ingreso/Admisión; Unidad funcional; Cama; Entidad pagadora; Municipio de ubicación del paciente; Profesional de la salud (médico solicitante); Servicio CUPS; Financiación UPC; Gestación / factor de riesgo; Tiempos máximos de recolección y entrega de resultados; Alertas/Iconos de riesgo y escalas; Edad del paciente; Concurrencia de la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve órdenes con FECORDMED >= hoy-4 meses y < hoy+1 día, cuyo ESTSERIPS esté en la lista de estados y CODCENATE esté en la lista de centros, ordenadas por FECORDMED DESC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 1..9 → Traduce el código de estado a etiqueta legible: 1 Solicitado, 2 Muestra recolectada, 3 Pendiente interpretación, 4 Examen interpretado, 5 Estudio remitido, 6 Anulado, 7 Extramural, 8 Muestra recolectada parcialmente, 9 Muestra no conforme.; si ce.FinancedResourceUPC = ''1'' → Marca el servicio como financiado por UPC (''Si'') else Marca como no financiado por UPC (''No''); si HCPARALELAB.CODCENATE = @CentroAtencion → Toma los tiempos máximos de recolección y entrega de resultados parametrizados para ese centro (LEFT JOIN, opcional).; si INTERDETA.ORDTIP = ''AMB'' → Calcula IDMuestra como el menor CODCONCEC de los detalles de interconsulta de tipo ambulatorio asociados a la orden.; si HCLABREMI.NUMEFOLIO IS NULL → Considera la remisión de laboratorio como vigente/no facturada para resolver el centro de remisión asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; common.GETDATE; dbo.edad; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDLAB; dbo.INPACIENT; dbo.INCUPSIPS; CONTRACT.CUPSEntity; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.HCPARALELAB; dbo.INUBICACI; dbo.INMUNICIP; dbo.HCRIESGOSP; dbo.INTERDETA; CONTRACT.CUPSEntityContractDescriptions; CONTRACT.ContractDescriptions; dbo.ADCENATEN; dbo.CHCAMASHO; dbo.HCMOANULB; dbo.HCLABREMI; dbo.HCCENLABU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesAmbulatorio';
-- GO
