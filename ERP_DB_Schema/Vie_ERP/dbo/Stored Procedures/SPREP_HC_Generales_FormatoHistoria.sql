
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_FormatoHistoria]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@TipoHistoria Char(1)

)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODTIPHIS as 'CODIGO TIPO DE HISTORIA' 
FROM HCUNITHIS A with(nolock)
INNER JOIN HCHISTORI B with(nolock) ON A.CODTIPHIS=B.CODTIPHIS 
 WHERE A.CODCENATE=@CentroAtencion AND A.UFUCODIGO=@UnidadFuncional AND B.FORTIPHIS=@TipoHistoria
END