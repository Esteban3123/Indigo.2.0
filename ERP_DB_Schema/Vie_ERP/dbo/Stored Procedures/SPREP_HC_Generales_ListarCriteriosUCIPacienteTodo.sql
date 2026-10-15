
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo]
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
 FROM HCCRIINGU A WITH(NOLOCK)
 INNER JOIN HCCRIUNID B ON A.CODTIPCRI=B.CODTIPCRI
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END