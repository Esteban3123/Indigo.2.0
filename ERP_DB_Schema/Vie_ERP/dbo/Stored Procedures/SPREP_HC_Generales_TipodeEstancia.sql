

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_TipodeEstancia]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT RTRIM(DESTIPEST) AS 'DESCRIPCION TIPO DE ESTANCIA',
A.JUSCAMANT 'JUSTIFICACION'           
FROM HCREGEST A with(noLock)
INNER JOIN CHTIPESTA B with(noLock) ON A.CODTIPEST=B.CODTIPEST
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END