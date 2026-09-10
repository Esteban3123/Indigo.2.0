
/***********************************************************************************************
Modified By: Carlos Pinzon
Date: 2019.03.15
Description: Realizo modificacion a consulta para traer información de lista imagenes de pacientes filtrado por grupo Imagenologia
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesSinLecturas]
(
@CentroAtencion Char(10),
@SubGrupo char(10)
)
AS
BEGIN
SET NOCOUNT ON;

	SELECT
		AUTO,
		A.FECORDMED AS FechaSolicitud,
		RTRIM(A.IPCODPACI) AS CodigoPaciente, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		B.IPFECNACI AS FechaNacimiento, 
		E.UFUACTPAC AS CodigoUnidad, 
		RTRIM(C.UFUDESCRI) AS DescripcionUnidad, 
		RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, 
		A.CODCENATE AS CodigoCentro, 
		A.ESTSERIPS AS Estado, 
		A.ESTALEIMG AS EstadoAlerta, 
		A.NUMINGRES AS Ingreso, 
		A.NUMEFOLIO AS Folio, 
		RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio,
		A.OBSSERIPS AS ObservacionServicio,
		RTRIM(A.CODSERIPS) AS CodigoServicio,
		RTRIM(I.NOMDIAGNO) AS NombreDiagnostico, 
		G.DESCCAMAS AS Cama,
		G.CODAISLAM AS TipoAislamiento,
		B.IPDIRECCI AS Direccion,
		B.IPTELEFON AS Telefono,
		B.IPSEXOPAC AS Sexo,
		B.IPRHSANGR AS Rh,
		B.IPGRUPSAN AS GrupoSanguineo,
		B.IPTELMOVI AS Movil, 
		B.CORELEPAC AS Correo, 
		H.NOMMEDICO AS Medico,
		A.CONCURRE AS Concurrencia,
		A.SERREAINT AS RealizaInterfaz,
		A.CANSERIPS AS Cantidad,
		RTRIM(CA.NOMCENATE) AS CentroAtencion,
		A.FECHASUGE AS FechaSugerida,
		CASE A.LATERALIDAD 
			WHEN 0 THEN 'No Aplica' 
			WHEN 1 THEN 'Izquierda' 
			WHEN 2 THEN 'Derecha' 
			WHEN 3 THEN 'Ambos' 
		END AS 'LATERALIDAD',
		CAST('' AS Varchar(100)) AS Edad,
		RTRIM(K.NOMENTIDA) AS DescripcionEntidad, 
		CASE A.PRISERIPS 
			WHEN '1' THEN 'Urgente' 
			WHEN '2' THEN 'Rutinario' 
			ELSE 'Otro' 
		END AS PRIORIDAD,
		dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', 
		dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM
		dbo.HCORDIMAG AS A with(nolock) 
		INNER JOIN	dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN	dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D.CODSERIPS 
		INNER JOIN	dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN	dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
		INNER JOIN	dbo.INUNIFUNC AS C2 with(nolock) ON A.UFUCODIGO = C2.UFUCODIGO 
		LEFT OUTER JOIN  dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS 
		INNER JOIN 	dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL = H.CODPROSAL  
		LEFT OUTER JOIN	dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO  
		INNER JOIN	dbo.INCUPSSUB AS CS with(nolock) ON CS.CODGRUSUB = D.CODGRUSUB 
		INNER JOIN	dbo.ADCENATEN AS CA with(nolock) ON CA.CODCENATE = A.CODCENATE 
		INNER JOIN	dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA = E.CODENTIDA 
		LEFT JOIN  contract.CUPSEntityContractDescriptions AS CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions AS CD ON CD.Id = CDD.ContractDescriptionId
	 WHERE
		A.ESTSERIPS IN(3) 
		AND A.CODCENATE IN(SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
		AND CS.IDRISGRIMAGE = @subgrupo
		AND A.SERTRANSC = 0 
		AND A.SERVALMED = 0 
		AND A.ESTTRASER = '1' 
		and A.TIENEGRABACION = 0
	 ORDER BY A.FECORDMED DESC

 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, etc.) que han sido tomadas o ejecutadas pero aún no tienen lectura ni transcripción médica, filtrando por centro de atención y subgrupo de imagenología. Cruza las órdenes de imágenes (HCORDIMAG) con datos del paciente (INPACIENT), el ingreso activo (ADINGRESO), la unidad funcional actual y solicitante (INUNIFUNC), la cama asignada (CHCAMASHO), el médico solicitante (INPROFSAL), el diagnóstico CIE-10 (INDIAGNOS), el servicio CUPS (INCUPSIPS), la entidad aseguradora y el centro de atención (ADCENATEN). Devuelve información completa de cada estudio pendiente de lectura: paciente, ingreso, servicio solicitado, unidad, cama, prioridad (urgente o rutinario), lateralidad, alertas de factores de riesgo y escalas clínicas, útil para que el área de radiología o imagenología gestione la cola de estudios sin interpretar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas pendientes de lectura para uno o varios centros de atención y un subgrupo de imagenología, devolviendo datos del paciente, ingreso, servicio, médico, ubicación y alertas clínicas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.splitstring (lista delimitada de códigos); Debe existir el subgrupo de imagenología referenciado en INCUPSSUB.IDRISGRIMAGE; Las órdenes deben tener ingreso, paciente, servicio CUPS, unidad funcional solicitante y actual, profesional y centro de atención válidos para cumplir los INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes en estado 3 (pendientes) que aún no han sido transcritas (SERTRANSC=0) ni validadas por médico (SERVALMED=0); Solo se incluyen órdenes con estado de traslado de servicio igual a ''1''; Se excluyen órdenes que ya tienen grabación (TIENEGRABACION=0); Solo se incluyen órdenes cuyo subgrupo CUPS pertenezca al grupo de imagenología indicado; La descripción del servicio concatena la descripción CUPS con la descripción contractual cuando exista; Las alertas de factores de riesgo y escalas se calculan vía función dbo.RiskFactorAlert por paciente e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Imagenología; Lectura de imágenes; Paciente; Ingreso/admisión; Unidad funcional solicitante y actual; Cama y tipo de aislamiento; Diagnóstico (CIE); Profesional tratante; Entidad/aseguradora; Centro de atención; Lateralidad; Prioridad de servicio (urgente/rutinario); Alertas de factores de riesgo y escalas; Servicio CUPS/IPS; Concurrencia; Interfaz de servicios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Devuelve órdenes con ESTSERIPS=3, SERTRANSC=0, SERVALMED=0, ESTTRASER=''1'' y TIENEGRABACION=0, filtradas por centro de atención (lista) y subgrupo de imagenología (INCUPSSUB.IDRISGRIMAGE), ordenadas por FECORDMED DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.LATERALIDAD = 0/1/2/3 → Traduce a ''No Aplica'' / ''Izquierda'' / ''Derecha'' / ''Ambos'' respectivamente; si A.PRISERIPS = ''1'' o ''2'' → Clasifica la prioridad como ''Urgente'' o ''Rutinario'' else Cualquier otro valor se clasifica como ''Otro''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INCUPSSUB; dbo.ADCENATEN; dbo.INENTIDAD; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturas';
-- GO
