
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografiasInternos]
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
SELECT  NUMEFOLIO AS 'NUMERO FOLIO',FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', NUMSEMULT AS 'SEMANAS EN ULTIMA ECOGRAFIA', FECHISPAC AS 'FECHA DE HISTORIA', NUMSEMHIS AS 'SEMANAS ACTUALES'
FROM HCANTECOI WITH(NOLOCK)

WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente AND NUMINGRES=@NumeroIngreso

END