
CREATE PROCEDURE [dbo].[SPREP_CH_ListarEcografias]
(
@NumeroFolio nChar(10),
@CodigoPaciente Varchar(25)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', NUMSEMULT AS 'SEMANAS EN ULTIMA ECOGRAFIA', FECHISPAC AS 'FECHA DE HISTORIA', NUMSEMHIS AS 'SEMANAS ACTUALES'
FROM HCANTECOG WITH(NOLOCK)
--where IDETPHIS???

WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente

END