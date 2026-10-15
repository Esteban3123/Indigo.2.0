
CREATE PROCEDURE [dbo].[SPREP_CH_CamasVacias]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODICAMAS AS 'CODIGO CAMA', RTRIM(A.DESCCAMAS) AS 'DESCRIPCION CAMA'
FROM  CHCAMASHO A WITH(NOLOCK)

WHERE A.CODCENATE = @CentroAtencion AND A.UFUCODIGO IN (@UnidadFuncional) AND ESTADCAMA=1

END