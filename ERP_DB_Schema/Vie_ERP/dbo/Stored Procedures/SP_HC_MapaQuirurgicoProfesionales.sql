
-- =============================================
-- Author:        <Andres Felipe Bonilla Tocora>
-- Create date:   <14/12/2017>
-- Description:   <Mapa quirurgico>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_MapaQuirurgicoProfesionales]
(
    @fechaInicial DATETIME,
    @fechaFinal DATETIME,
	@codigoProfesional CHAR(20)
	--,@DesdeRegistro INT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    SELECT DISTINCT CONVERT(VARCHAR(20), A.FECHORAIN, 103) AS Fecha,
        CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAIN), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAIN), '00')) AS 'Hora Inicial',
        CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAFI), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAFI), '00')) AS 'Hora final',
        CONCAT(B.CODIGSALA, ' - ', B.DESCRIPSAL) AS Sala,
        STUFF((Select ', ' + ACT.DESACTMED AS [text()]
                From dbo.AGACTIMED ACT
                INNER JOIN AGAGEMEDD DET1 ON DET1.CODACTMED=ACT.CODACTMED
                Where A.CODAUTONU=DET1.CODAUTONU
                For XML PATH ('')), 1, 2,'') As Actividades
    FROM dbo.AGAGEMEDC A
        INNER JOIN AGAGEMEDD DET ON A.CODAUTONU=DET.CODAUTONU
        INNER JOIN dbo.AGENSALAC B
            ON A.AGENSALAC = B.CODCONCEC
        INNER JOIN dbo.INPROFSAL C
            ON A.CODPROSAL = C.CODPROSAL
    WHERE A.TIPAGEMED = '0'
          AND A.AGENSALAC IS NOT NULL
          AND A.FECHORAIN >= @fechaInicial
          AND A.FECHORAFI <= @fechaFinal
		  AND A.CODPROSAL = @codigoProfesional
    ORDER BY Fecha, 'Hora Inicial'

END;
-------------------------------------------------------------