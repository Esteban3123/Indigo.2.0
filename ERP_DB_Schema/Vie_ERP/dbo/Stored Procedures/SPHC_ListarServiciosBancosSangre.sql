-- Stored Procedure



-- =============================================
-- Author:		<Author,Julian Cardozo>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosBancosSangre]
(
@CentroAtencion Char(10),
@Estado int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ROW_NUMBER() OVER (ORDER BY A.FECORDMED DESC) AS NumeroFila, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(A.IPCODPACI) + ' - ' + RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
A.UFUCODIGO AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEHEM  as EstadoAlerta, A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, RTRIM(D.DESPRODUC) AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, A.CODPRODUC  AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
AS NombreDiagnostico, G.DESCCAMAS AS Cama, g.CODAISLAM AS TipoAislamiento,
b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
b.IPTELMOVI as Movil ,b.CORELEPAC as Correo, H.NOMMEDICO  AS Medico, 0 as Minutos,CAST('' as char) as Barra,CAST('' as bit) as Marcar,
a.CONCURRE as Concurrencia

FROM  dbo.HCORDHEMO  AS A with(nolock) INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON A.UFUCODIGO = C.UFUCODIGO INNER JOIN
 dbo.HCHEMPROD  AS D with(nolock) ON A.CODPRODUC = D.CODPRODUC  INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES LEFT OUTER JOIN
 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO 
 
 WHERE A.ESTSERIPS=@Estado and A.CODCENATE=@CentroAtencion
END