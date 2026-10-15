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

	   Case A.Laterality WHEN 0 THEN 'No aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' ELSE 'No especificado' END AS Lateralidad,
	   CASE when UFUEGRMED is null then '1 - Paciente en la unidad' else '2 - Paciente con salida' END Grupo,Z.Color , CASE A.PRISERIPS WHEN 1 THEN 'Urgente' WHEN 2 THEN 'Rutina' END PrioridadName,
	   CASE A.PRISERIPS when 1 then 'Urgente' when 2 then 'Rutinario' end as PrioridadDescripcion,
	   dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
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
 
END