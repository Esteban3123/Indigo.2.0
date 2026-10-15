

CREATE PROCEDURE [dbo].[SP_INV_ListarCodigosdeBarra]
(
@CodigoProducto varchar(20) 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT FECHCREAC AS 'FECHA CREACION',CODBARRAS AS 'CODIGO DE BARRAS', IPRCODIGO AS 'CODIGO DE PRODUCTO' 
	FROM   IHCODBARR
	WHERE IPRCODIGO=@CodigoProducto

END