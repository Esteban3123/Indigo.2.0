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

	   Case A.Laterality WHEN 0 THEN 'No aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' ELSE 'No especificado' END AS Lateralidad,
	   dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'

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
WHERE A.ESTSERIPS=@Estado and A.CODCENATE=@CentroAtencion   
 
END