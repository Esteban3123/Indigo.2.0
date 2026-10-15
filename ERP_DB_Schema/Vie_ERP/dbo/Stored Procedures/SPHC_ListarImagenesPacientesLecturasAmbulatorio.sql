

/***********************************************************************************************
-- ================================================================================================================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- ================================================================================================================================
-- Modified By: Carlos Pinzon
-- Date: 2019.03.15
-- Description: Realizo modificacion a consulta para traer imagenes de pacientes lecturas ambulatorio filtrado por grupo Imagenologia
-- ================================================================================================================================
***********************************************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesLecturasAmbulatorio]
(
@Validado bit,
@Transcripcion bit,
@CentroAtencion Char(500),
@SubGrupo char(10),
@Profesional char(20)
)
AS
BEGIN
SET NOCOUNT ON;

IF @Profesional IS NULL

SELECT  AUTO, A.ESTTRASER,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(A.IPCODPACI)  as 'Identificación', 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,a.CANSERIPS  as Cantidad,A.OBSERVACI AS ObservacionServicio, RTRIM(CA.NOMCENATE) as CentroAtencion,
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION

FROM  dbo.AMBORDIMA  AS A with(nolock) INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
 dbo.INUNIFUNC AS C2 with(nolock) ON e.UFUACTPAC = C2.UFUCODIGO LEFT OUTER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL INNER JOIN
 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB INNER JOIN
 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA LEFT JOIN 
 contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
 contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
 WHERE A.ESTSERIPS  in(3,4) 
 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
 AND CS.IDRISGRIMAGE=@subgrupo  
 AND A.SERTRANSC=@Transcripcion 
 AND A.SERVALMED=@Validado 
 AND (ESTTRASER = '1' or ESTTRASER is null)
 AND A.TIENEGRABACION = 1
 ORDER by A.FECORDMED DESC

ELSE

SELECT  AUTO, A.ESTTRASER,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(A.IPCODPACI)  as 'Identificación', 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, A.CODSERIPS AS CodigoServicio,
B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,A.OBSERVACI AS ObservacionServicio, RTRIM(CA.NOMCENATE) as CentroAtencion,
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION

FROM  dbo.AMBORDIMA  AS A with(nolock) INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
 dbo.INUNIFUNC AS C2 with(nolock) ON e.UFUACTPAC = C2.UFUCODIGO LEFT OUTER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL INNER JOIN
 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB INNER JOIN
 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA LEFT JOIN 
 contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
 contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 

 WHERE A.ESTSERIPS in(3,4) 
 AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
 AND CS.IDRISGRIMAGE=@subgrupo  
 AND A.SERTRANSC=@Transcripcion 
 AND A.SERVALMED=@Validado 
 AND A.CODPROVAL=@Profesional  
 AND (ESTTRASER = '1' or ESTTRASER is null)
 AND A.TIENEGRABACION = 1
 ORDER by A.FECORDMED DESC

END