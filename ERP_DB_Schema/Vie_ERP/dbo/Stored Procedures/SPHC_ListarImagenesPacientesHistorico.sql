

/***********************************************************************************************
-- ================================================================================================================================
-- Author:		rafael Patiño
-- Create date: 17/10/2019
-- Description:	Listar historico de imagenes
***********************************************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesHistorico]
(
@CentroAtencion Char(10),
@Paciente varchar(25)
)
AS
BEGIN
SET NOCOUNT ON;


SELECT A.AUTO,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, RTRIM(C.UFUDESCRI) AS DescripcionUnidad,RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, RTRIM(D .DESSERIPS) AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
AS NombreDiagnostico, G.DESCCAMAS AS Cama,G.CODAISLAM AS TipoAislamiento,
B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,A.SERREAINT as RealizaInterfaz, RTRIM(CA.NOMCENATE) as CentroAtencion, 
A.FECHASUGE , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDAD',
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad, 
CASE A.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' ELSE 'Otro' END AS PRIORIDAD,
CASE A.ESTSERIPS when 1 then 'Solicitado ' when 2 then 'Estudio Realizado ' when 3 then 'Imagen Procesada' when 4 then 'Estudio Interpretado' when 5 then 'Remitido' when 6 then 'Anulado' when 7 then 'Extramural' END as ESTADO
FROM  dbo.HCORDIMAG  AS A with(nolock) INNER JOIN
 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
 dbo.INUNIFUNC AS C2 with(nolock) ON A.UFUCODIGO = C2.UFUCODIGO LEFT OUTER JOIN 
 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO INNER JOIN
 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA 
 WHERE A.ESTSERIPS in(3,4) 
 AND A.IPCODPACI=@Paciente 
 AND A.ESTTRASER='1' 
 AND A.TIENEGRABACION = 1
 order by A.FECORDMED desc

END