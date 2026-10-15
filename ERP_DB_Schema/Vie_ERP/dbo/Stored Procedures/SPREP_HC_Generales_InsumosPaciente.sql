

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_InsumosPaciente]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10),
@ManejoExterno bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT 
	RTRIM(B.CODPRODUC) AS 'CODIGO DEL PRODUCTO',
	CANPEDPRO AS 'CANTIDAD PEDIDA DEL PRODUCTO',
	RTRIM(DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',
	A.CODCENATE,
	A.UFUCODIGO,
	A.IPCODPACI, 
	RTRIM(B.JUSTIINSU) AS 'JUSTIFICACION', 
	B.MANEJOEXTRA
                   
FROM 
	HCSOLINSC A WITH(NOLOCK)
	INNER JOIN HCSOLINSD B WITH(NOLOCK) ON A.CODCONCEC = B.CODCONCEC 
	INNER JOIN IHLISTPRO C WITH(NOLOCK) ON B.CODPRODUC = C.CODPRODUC
              
WHERE
	A.IPCODPACI = @CodigoPaciente 
	AND A.NUMINGRES = @NumeroIngreso
	AND NUMEFOLIO = @NumeroFolio
	AND ( B.MANEJOEXTRA = @ManejoExterno OR ( @ManejoExterno = 0 AND  B.MANEJOEXTRA IS NULL ) )

END