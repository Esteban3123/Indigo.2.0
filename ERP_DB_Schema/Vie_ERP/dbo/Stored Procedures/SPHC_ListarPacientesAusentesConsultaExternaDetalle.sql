

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentesConsultaExternaDetalle]
@Profesional Char(20),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

SELECT IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,FECAUSENT AS FechaAusente,rtrim(PROPRILLA)+' - '+rtrim(B.NOMMEDICO) AS PriLlamado ,
rtrim(PROSEGLLA)+' - '+rtrim(C.NOMMEDICO) AS SegLlamado,rtrim(PROTERLLA)+' - '+rtrim(D.NOMMEDICO) AS TerLlamado
FROM ADCONCOEX A
INNER JOIN INPROFSAL B ON A.PROPRILLA =B.CODPROSAL 
INNER JOIN INPROFSAL C ON A.PROSEGLLA =C.CODPROSAL
INNER JOIN INPROFSAL D ON A.PROTERLLA =D.CODPROSAL 
WHERE CONESTADO='2' AND A.PROAUSENT=@Profesional AND FECAUSENT BETWEEN @FechaInicial AND @FechaFinal 

END