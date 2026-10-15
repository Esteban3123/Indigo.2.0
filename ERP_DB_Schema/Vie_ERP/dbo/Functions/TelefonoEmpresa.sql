
CREATE function [dbo].[TelefonoEmpresa]()
RETURNS nvarchar (60)
AS
BEGIN
declare @TelefonoEmpresa nvarchar(60)

	SELECT TOP 1 @TelefonoEmpresa = INDTE1EMP FROM INEMPRESU 
	return @TelefonoEmpresa
END