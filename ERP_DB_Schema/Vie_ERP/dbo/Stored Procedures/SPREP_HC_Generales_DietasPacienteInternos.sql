
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DietasPacienteInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT A.CODTIPDIE AS 'CODIGO TIPO DIETA',RTRIM(B.DESTIPDIE) AS 'DESCRIPCION TIPO DE DIETA', A.OBSERVACI AS OBSERVACIONES

FROM HCREGDIEI A WITH(NOLOCK)
INNER JOIN CHTIPDIET B WITH(NOLOCK) ON A.CODTIPDIE=B.CODTIPDIE 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END