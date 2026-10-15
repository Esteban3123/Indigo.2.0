
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentesDetalle] 
@UnidadFuncional char(10),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT A.IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,A.CONESTADO AS Estado,A.FECAUSENT AS FechaAusente,
rtrim(A.PROPRILLA)+' - '+rtrim(C.NOMMEDICO) AS ProfesionalPrimerLlamadoT,
rtrim(A.PROSEGLLA)+' - '+rtrim(D.NOMMEDICO) AS ProfesionalSegundoLlamadoT,
rtrim(A.PROTERLLA)+' - '+rtrim(E.NOMMEDICO) AS ProfesionalTercerLlamadoT,
rtrim(B.PROPRILLA)+' - '+rtrim(F.NOMMEDICO) AS ProfesionalPrimerLlamadoA,
rtrim(B.PROSEGLLA)+' - '+rtrim(G.NOMMEDICO) AS ProfesionalSegundoLlamadoA,
rtrim(B.PROTERLLA)+' - '+rtrim(H.NOMMEDICO) AS ProfesionalTercerLlamadoA,
cast('' AS char(150)) AS PriLlamado,cast('' AS char(150)) AS SegLlamado,cast('' AS char(150)) AS TerLlamado
FROM ADCONTURG A 
LEFT OUTER JOIN ADTRIAGEU B ON A.CODCONCEC =B.CODCONCEC
LEFT OUTER JOIN INPROFSAL C ON A.PROPRILLA= C.CODPROSAL
LEFT OUTER JOIN INPROFSAL D ON A.PROSEGLLA = D.CODPROSAL  
LEFT OUTER JOIN INPROFSAL E ON A.PROTERLLA = E.CODPROSAL 
LEFT OUTER JOIN INPROFSAL F ON B.PROPRILLA= F.CODPROSAL
LEFT OUTER JOIN INPROFSAL G ON B.PROSEGLLA = G.CODPROSAL  
LEFT OUTER JOIN INPROFSAL H ON B.PROTERLLA = H.CODPROSAL
WHERE 
A.CONESTADO IN ('2','6') AND A.UFUCODIGO= @UnidadFuncional AND A.FECAUSENT BETWEEN @FechaInicial AND @FechaFinal
END