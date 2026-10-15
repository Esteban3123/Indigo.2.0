
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_InsumosPacienteInternos]
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
          
SELECT RTRIM(B.CODPRODUC) AS 'CODIGO DEL PRODUCTO',CANPEDPRO AS 'CANTIDAD PEDIDA DEL PRODUCTO',RTRIM(DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
                   
FROM HCSOLINCI A   with(noLock)
INNER JOIN HCSOLINDI B  with(noLock) ON A.CODCONCEC=B.CODCONCEC 
INNER JOIN IHLISTPRO C  with(noLock) ON B.CODPRODUC=C.CODPRODUC
              
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END