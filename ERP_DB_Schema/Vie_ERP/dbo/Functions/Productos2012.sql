

CREATE FUNCTION [dbo].[Productos2012] (@Registro as nvarchar(100))
RETURNS nvarchar (60)
AS
BEGIN

declare @Producto nvarchar(60)

SELECT top 1 @Producto = IPRCODIGO
FROM        IHRINDDGH
WHERE CODPRODUC = @REGISTRO


RETURN @Producto

END