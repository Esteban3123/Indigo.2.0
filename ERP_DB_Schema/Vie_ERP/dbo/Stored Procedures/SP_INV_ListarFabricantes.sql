
CREATE PROCEDURE [dbo].[SP_INV_ListarFabricantes]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT OID,IFRCODIGO AS 'CODIGO FABRICANTE',IFRNOMBRE AS 'NOMBRE FABRICANTE', RTRIM(IFRCODIGO)+' - '+RTRIM(IFRNOMBRE) AS 'FABRICANTE'
FROM DBO.INNFABRIC

END