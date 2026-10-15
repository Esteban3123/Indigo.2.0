
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CriteriosIngresoInternos]
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
          
SELECT RTRIM(DESTIPCRI) AS 'TIPOS DE CRITERIO'

FROM HCCRIINGI A WITH(NOLOCK)
INNER JOIN HCCRIUNID B WITH(NOLOCK) ON A.CODTIPCRI=B.CODTIPCRI
              
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END