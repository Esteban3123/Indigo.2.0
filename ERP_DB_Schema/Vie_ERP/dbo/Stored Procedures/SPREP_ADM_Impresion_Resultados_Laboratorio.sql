-- Stored Procedure



CREATE PROCEDURE [dbo].[SPREP_ADM_Impresion_Resultados_Laboratorio]
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
SELECT IPCODPACI, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, FECORDMED, CODSERIPS
FROM AMBORDLAB with(nolock)
WHERE CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional
UNION SELECT IPCODPACI, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, FECORDMED, CODSERIPS
FROM HCORDLABO with(nolock)
WHERE CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional


END