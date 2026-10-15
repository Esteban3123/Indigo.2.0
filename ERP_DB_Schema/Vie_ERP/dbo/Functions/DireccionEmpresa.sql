
CREATE function [dbo].[DireccionEmpresa]()
RETURNS nvarchar (60)
AS
BEGIN
declare @DireccionEmpresa nvarchar(60)

	SELECT TOP 1 @DireccionEmpresa = INDDIREMP FROM INEMPRESU 
	return @DireccionEmpresa
END