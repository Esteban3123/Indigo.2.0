
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Sol_ListarSolicitudes]
	(
	@CodUsuario char(20)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COMAUTON,COMFECHA,COMESTADO,COMOBSERV,COMPRIORI,CODUSUARI,(a.UFUCODIGO ) AS CodUni,(B.UFUDESCRI ) AS UFUNOM
	from dbo .SOLCOMPRA  A INNER JOIN dbo .INUNIFUNC AS B ON A.UFUCODIGO = B.UFUCODIGO
		WHERE CODUSUARI = @CodUsuario 
END