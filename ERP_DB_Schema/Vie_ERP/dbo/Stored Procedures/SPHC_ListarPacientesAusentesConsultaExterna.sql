
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentesConsultaExterna]
@Profesional Char(20),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,
IPCODPACI AS Paciente,RTRIM(IPNOMCOMP) AS NombreCompleto,CODCENATE AS CentroAtencion,UFUCODIGO AS UnidadFuncional,CASE CONESTADO WHEN '2' THEN 'Consulta Externa' END AS Estado ,
FECPRILLA AS PrimerLlamado,FECSEGLLA AS SegundoLlamado,FECTERLLA AS TercerLlamado,OBVAUSENT AS Observacion,FECAUSENT AS FechaAusente,PROAUSENT,NOMMEDICO AS  ProfesionalAusenta
FROM ADCONCOEX a
INNER JOIN INPROFSAL c on a.PROAUSENT=c.CODPROSAL 
WHERE 
CONESTADO='2' AND PROAUSENT=@Profesional AND FECAUSENT BETWEEN @FechaInicial AND @FechaFinal 
  
END