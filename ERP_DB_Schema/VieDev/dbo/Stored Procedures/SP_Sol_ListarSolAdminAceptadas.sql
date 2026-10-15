

-- sp para listar las solicitudes autorizadas del usuario administrador logueado

CREATE PROCEDURE [dbo].[SP_Sol_ListarSolAdminAceptadas] 
@CodUser as char (20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT A.FECHAUTORI,A.CODUSUARI,A.COMAUTO ,
B.NOMUSUARI ,
C.CODUSUARI ,C.COMFECHA ,
D.NOMUSUARI AS USERSOLI
FROM SOL_AUTORI AS A 
INNER JOIN SEGusuaru  AS B ON A.CODUSUARI = B.CODUSUARI 
INNER JOIN SOLCOMPRA  AS C ON A.COMAUTO = C.COMAUTON 
INNER JOIN SEGusuaru  AS D ON C.CODUSUARI = D.CODUSUARI 
WHERE A.CODUSUARI = @CodUser 
END