-- Stored Procedure

-- =============================================
-- Author:        <Julian Andres Cardozo>
-- Create date: <05/08/2011>
-- Description:    <Listado de patologias>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPatologiasPacientes]
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
SELECT ROW_NUMBER() OVER (ORDER BY A.FECORDMED DESC) AS NumeroFila, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
       E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEPAT  as EstadoAlerta, A.NUMINGRES AS Ingreso, 
       A.NUMEFOLIO AS Folio, RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
       AS NombreDiagnostico, G.DESCCAMAS AS Cama, g.CODAISLAM AS TipoAislamiento, b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,b.IPTELMOVI as Movil,
	   b.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,CAST('' as bit) as Marcar, a.CONCURRE as Concurrencia,a.SERREAINT as RealizaInterfaz, B.CODENTIDA, A.CANSERIPS, RTRIM(B.IPPRINOMB) as IPPRINOMB, RTRIM(B.IPSEGNOMB) as IPSEGNOMB,
	   RTRIM(B.IPPRIAPEL) as IPPRIAPEL, RTRIM(B.IPSEGAPEL) as IPSEGAPEL, RTRIM(B.IPTELEFON) as IPTELEFON, RTRIM(B.IPTELMOVI) as IPTELMOVI, RTRIM(A.CODDIAGNO) as CODDIAGNO, A.AUTO as AUTOPATOL, A.CODPROSAL, B.IPTIPODOC, B.IPTIPOPAC,
	   B.IPTIPOAFI, D.CODGOCUPS, B.IPFECNACI, B.IPSEXOPAC, A.PRISERIPS AS Prioridad, 'Autorización' AS Autorizacion, CAST('' AS VARCHAR(50)) AS Edad,
	   
	   iif(CM.code is not null, CM.Description,'No especificado') as 'MedioRecoleccion', 
	   iif(SN.code is not null, SN.Description,'No especificado') as 'Especimen',
	   iif(TE.code is not null, TE.Description,'No especificado') as 'Tecnica',

	   CASE A.Laterality WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' else 'No Aplica' END AS Lateralidad,
	   CASE when UFUEGRMED is null then '1 - Paciente en la unidad' else '2 - Paciente con salida' END Grupo,Z.Color , CASE A.PRISERIPS WHEN 1 THEN 'Urgente' WHEN 2 THEN 'Rutina' END PrioridadName,
	   CASE A.PRISERIPS when 1 then 'Urgente' when 2 then 'Rutinario' end as PrioridadDescripcion,
	   dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas', FECRECEPMUES AS 'FechaRecepcion'
FROM  dbo.HCORDPATO   AS A with(nolock) 
       INNER JOIN dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
       INNER JOIN dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS 
       INNER JOIN dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
       INNER JOIN dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
       INNER JOIN dbo.INUNIFUNC AS C2 with(nolock) ON A.UFUCODIGO = C2.UFUCODIGO 
       LEFT OUTER JOIN  dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS 
       INNER JOIN  dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  
       LEFT OUTER JOIN dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO  
       LEFT JOIN dbo.CHTIPOSAISLAMIENTOS Z ON G.CODAISLAM = Z.Id 
       LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
       LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
	   LEFT JOIN ClinicalParameters.DefinitionSamples AS CM with(nolock) On A.CollectionMedium = CM.Id 
	   LEFT JOIN ClinicalParameters.DefinitionSamples AS SN with(nolock) On A.Specimen = SN.Id 
       LEFT JOIN ClinicalParameters.DefinitionSamples AS TE with(nolock) On A.Technique = TE.Id 
WHERE A.ESTSERIPS=@Estado and A.CODCENATE=@CentroAtencion  
	  AND A.FECORDMED>=DATEADD(MONTH,-3,common.GETDATE()) AND A.FECORDMED < common.GETDATE()
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de patología e imágenes diagnósticas solicitadas en los últimos 3 meses para un centro de atención y un estado de orden determinados. Integra datos del paciente (nombre completo, cédula, fecha de nacimiento, sexo, grupo sanguíneo, contacto), del ingreso hospitalario (número de ingreso, unidad funcional actual y solicitante, cama asignada, tipo de aislamiento), del servicio CUPS solicitado (código, descripción, observaciones, prioridad, lateralidad, medio de recolección, espécimen y técnica) y del profesional que generó la orden. También incorpora el diagnóstico CIE-10, alertas de factores de riesgo y escalas clínicas, y la descripción contractual del procedimiento. Se usa en los módulos de laboratorio e imágenes diagnósticas para que el personal de salud gestione, consulte y marque el estado de las solicitudes de patología pendientes o en proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPatologiasPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de patología de pacientes en un centro de atención y estado dado, registradas en los últimos 3 meses, con datos clínicos, demográficos, de ubicación, alertas y muestra.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El estado y el centro de atención deben existir en HCORDPATO; Las tablas maestras (paciente, ingreso, unidad funcional, profesional, CUPS) deben tener los registros referenciados por la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes de los últimos 3 meses respecto a common.GETDATE(); Se filtra estrictamente por estado de servicio y centro de atención; Las consultas se hacen con NOLOCK (lectura sucia permitida); Las descripciones de muestra/espécimen/técnica nunca son nulas en el resultado (default ''No especificado''); La lateralidad siempre tiene un valor textual (default ''No Aplica'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Patología; Paciente; Ingreso hospitalario; Unidad funcional; Cama y aislamiento; Diagnóstico (CIE); Profesional de salud; Servicio CUPS; Prioridad (Urgente/Rutina); Lateralidad; Medio de recolección, espécimen y técnica de muestra; Alertas de factores de riesgo y escalas; Autorización; Concurrencia / interfaz de servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPATO: Devuelve órdenes de patología cuyo ESTSERIPS=@Estado, CODCENATE=@CentroAtencion y FECORDMED está dentro de los últimos 3 meses (>=DATEADD(MONTH,-3,common.GETDATE()) y < common.GETDATE()), ordenadas por FECORDMED DESC con número de fila', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.Laterality IN (1,2,3,4) → Traduce a ''Izquierda'', ''Derecha'', ''Bilateral'' o ''Multilateral'' else ''No Aplica''; si UFUEGRMED IS NULL (sin egreso médico) → Grupo = ''1 - Paciente en la unidad'' else Grupo = ''2 - Paciente con salida''; si A.PRISERIPS = 1 → Prioridad = ''Urgente'' else Si PRISERIPS=2 entonces ''Rutina''/''Rutinario''; si CM.code/SN.code/TE.code IS NOT NULL → Muestra Description del medio de recolección, espécimen o técnica else ''No especificado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.RiskFactorAlert; common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPATO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.CHTIPOSAISLAMIENTOS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; ClinicalParameters.DefinitionSamples', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientes';
-- GO
