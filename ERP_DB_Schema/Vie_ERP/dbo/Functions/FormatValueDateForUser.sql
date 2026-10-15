



CREATE FUNCTION [dbo].[FormatValueDateForUser] 

(
@UserCode as varchar(20), 
@dateValue as datetime
)

RETURNS nvarchar (30)
AS
BEGIN



declare @dateValueFormat nvarchar(30)
declare @dateFormat int = 4
declare @UserId as int


--obtener el id del usuario
SELECT @UserId = Id FROM Security.[UserInt] WHERE UserCode = @UserCode

--obtener el formato de fecha seleccionado por el usuario 
--SELECT @dateFormat = dateformat FROM Security.[UserConfiguration] WHERE UserId = @UserId


-- Formatear la fecha según el dateFormat obtenido
SET @dateValueFormat = CASE 
    WHEN @dateFormat = 0 THEN FORMAT(@dateValue, 'dd/MM/yyyy HH:mm', 'es-ES')
    WHEN @dateFormat = 1 THEN FORMAT(@dateValue, 'MM/dd/yyyy HH:mm', 'es-ES')
    WHEN @dateFormat = 2 THEN FORMAT(@dateValue, 'yyyy/MM/dd HH:mm', 'es-ES')
    WHEN @dateFormat = 3 THEN FORMAT(@dateValue, 'dd/MMM/yyyy HH:mm', 'es-ES')
    WHEN @dateFormat = 4 THEN FORMAT(@dateValue, 'dd \de MMMM \de yyyy HH:mm','es-ES')
    ELSE FORMAT(@dateValue, 'dd/MM/yyyy HH:mm:ss', 'es-ES') -- Formato por defecto en caso de que dateFormat no coincida con ninguno de los anteriores
END


RETURN @dateValueFormat

END