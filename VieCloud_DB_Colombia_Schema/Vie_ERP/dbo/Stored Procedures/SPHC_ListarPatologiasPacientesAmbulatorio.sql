-- Stored Procedure

CREATE PROCEDURE [dbo].[SPHC_ListarPatologiasPacientesAmbulatorio]
(
@Estado int,
@CentroAtencion Char(10)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad, 
       A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado, A.NUMINGRES AS Ingreso, RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, 
	   b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo, b.IPTELMOVI as Movil ,b.CORELEPAC as Correo, H.NOMMEDICO  AS Medico, J.LABRECMUE AS TiempoMaximo,
	   J.UNITIEREC AS UnidadTiempo, 0 as Minutos,CAST('' as char) as Barra,LABENTRES as TiempoResultado,UNITIEENT as UnidadResultado,CAST('' as bit) as Marcar, a.CONCURRE as Concurrencia,e.UFUCODIGO ,a.OBSERVACI as ObservacionServicio,
	   A.CODSERIPS AS CodigoServicio, '' AS Cama, '' AS TipoAislamiento, b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo, b.IPTELMOVI as Movil ,b.CORELEPAC as Correo,
	   H.NOMMEDICO AS Medico,CAST('' as bit) as Marcar, a.CONCURRE as Concurrencia,CONVERT(bit,1) as RealizaInterfaz, B.CODENTIDA, A.CANSERIPS, RTRIM(B.IPPRINOMB) as IPPRINOMB, RTRIM(B.IPSEGNOMB) as IPSEGNOMB, RTRIM(B.IPPRIAPEL) as IPPRIAPEL, 
	   RTRIM(B.IPSEGAPEL) as IPSEGAPEL, RTRIM(B.IPTELEFON) as IPTELEFON, RTRIM(B.IPTELMOVI) as IPTELMOVI, A.AUTO as AUTOPATOL, A.CODPROSAL, B.IPTIPODOC, B.IPTIPOPAC, B.IPTIPOAFI, D.CODGOCUPS, B.IPFECNACI, B.IPSEXOPAC, 2 as Prioridad, 
	   '' as ObservacionServicio, '' as CODDIAGNO, A.CANSERIPS, CAST('' AS VARCHAR(50)) AS Edad,

	   iif(CM.code is not null, CM.Description,'No especificado') as 'MedioRecoleccion', 
	   iif(SN.code is not null, SN.Description,'No especificado') as 'Especimen',
	   iif(TE.code is not null, TE.Description,'No especificado') as 'Tecnica',

	   CASE A.Laterality WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' else 'No Aplica' END AS Lateralidad,
	   dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas', FECRECEPMUES AS 'FechaRecepcion'

FROM  dbo.AMBORDPAT AS A with(nolock) 
	   INNER JOIN dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
	   INNER JOIN dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS 
	   INNER JOIN dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
	   INNER JOIN dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
	   left outer join dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  
	   LEFT OUTER JOIN dbo.HCPARALELAB AS J with(nolock) ON A.CODSERIPS = J.CODSERIPS 
	   LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	   LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	   LEFT JOIN ClinicalParameters.DefinitionSamples AS CM with(nolock) On A.CollectionMedium = CM.Id 
	   LEFT JOIN ClinicalParameters.DefinitionSamples AS SN with(nolock) On A.Specimen = SN.Id 
       LEFT JOIN ClinicalParameters.DefinitionSamples AS TE with(nolock) On A.Technique = TE.Id 
WHERE A.ESTSERIPS=@Estado and A.CODCENATE=@CentroAtencion   AND A.FECORDMED > DATEADD(MONTH, -3, Common.GETDATE())  
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes médicas de patología (servicios o exámenes) solicitadas a pacientes en atención ambulatoria, filtrando por estado de la orden y centro de atención, y considerando únicamente los últimos 3 meses. Combina datos del paciente (nombre completo, documento, fecha de nacimiento, sexo, grupo sanguíneo, contacto y correo), el episodio de ingreso, la unidad funcional donde está ubicado, el profesional que generó la orden, el servicio solicitado con su descripción CUPS y concepto de contrato asociado, los parámetros de laboratorio (tiempos de recepción y entrega de resultados), el medio de recolección, espécimen y técnica de la muestra, la lateralidad del procedimiento, y alertas de factores de riesgo y escalas clínicas del paciente. Se utiliza en los módulos de laboratorio y patología ambulatoria para que el personal asistencial gestione y ejecute las órdenes pendientes o en proceso de un centro de atención específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes/patologías ambulatorias de pacientes de los últimos 3 meses para un centro de atención y estado dados, con datos clínicos, demográficos, de servicio, muestra, lateralidad y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso del paciente en ADINGRESO ligado a la orden ambulatoria.; Debe existir el servicio CUPS y la unidad funcional referenciados.; El centro de atención y el estado deben corresponder a valores válidos en AMBORDPAT.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes ambulatorias con fecha de orden dentro de los últimos 3 meses respecto a la fecha actual del sistema.; Solo se listan órdenes que coinciden exactamente con el estado y centro de atención solicitados.; Siempre se requiere existencia de paciente, servicio CUPS, ingreso y unidad funcional asociada (joins internos).; Cuando no hay catálogo asociado de medio, espécimen o técnica, se devuelve ''No especificado''.; RealizaInterfaz siempre se devuelve como 1 (true) y Prioridad siempre como 2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente ambulatorio; Orden médica / patología; Ingreso o atención; Unidad funcional; Profesional de la salud; Servicio CUPS; Parámetros de laboratorio (tiempo de recepción y entrega); Muestra clínica (medio de recolección, espécimen, técnica); Lateralidad; Alertas de factores de riesgo y escalas; Centro de atención; Estado del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AMBORDPAT: Cuando ESTSERIPS = @Estado y CODCENATE = @CentroAtencion y FECORDMED > (hoy - 3 meses), retorna el conjunto de órdenes ambulatorias enriquecido con paciente, ingreso, unidad, servicio, médico, parámetros de laboratorio, muestras y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Laterality = 1 → Lateralidad = ''Izquierda''; si Laterality = 2 → Lateralidad = ''Derecha''; si Laterality = 3 → Lateralidad = ''Bilateral''; si Laterality = 4 → Lateralidad = ''Multilateral'' else Lateralidad = ''No Aplica''; si CollectionMedium/Specimen/Technique con coincidencia en DefinitionSamples → Devuelve la descripción del catálogo else Devuelve ''No especificado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.RiskFactorAlert; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDPAT; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.HCPARALELAB; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; ClinicalParameters.DefinitionSamples', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesAmbulatorio';
-- GO
