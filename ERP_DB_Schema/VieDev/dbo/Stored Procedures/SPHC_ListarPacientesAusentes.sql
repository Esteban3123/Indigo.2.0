
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentes]
@UnidadFuncional char(10),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT A.IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,
A.IPCODPACI AS Paciente,RTRIM(A.IPNOMCOMP) AS NombreCompleto,A.CODCENATE AS CentroAtencion,
A.UFUCODIGO AS UnidadFuncional,CASE A.CONESTADO WHEN '2' THEN 'Clasificacion Triage' WHEN '6' THEN 'Atencion Inicial Urgencias' END AS Estado ,
A.FECPRILLA AS FechaPrimerLlamadoT,A.FECSEGLLA AS FechaSegundoLlamadoT,A.FECTERLLA AS FechaTercerLlamadoT,
B.FECPRILLA AS FechaPrimerLlamadoA,B.FECSEGLLA AS FechaSegundoLlamadoA,B.FECTERLLA AS FechaTercerLlamadoA,
cast('' AS datetime) AS PrimerLlamado,cast('' AS datetime) AS SegundoLlamado,cast('' AS datetime) AS TercerLlamado,A.OBVAUSENT AS Observacion,A.FECAUSENT AS FechaAusente, C.NOMMEDICO AS  ProfesionalAusenta
FROM ADCONTURG A
LEFT OUTER JOIN ADTRIAGEU B ON A.CODCONCEC =B.CODCONCEC 
INNER JOIN INPROFSAL c on a.PROAUSENT=c.CODPROSAL 
WHERE CONESTADO IN ('2','6') AND A.FECTERLLA is not null AND A.UFUCODIGO= @UnidadFuncional 
AND A.FECAUSENT BETWEEN @FechaInicial AND @FechaFinal
END