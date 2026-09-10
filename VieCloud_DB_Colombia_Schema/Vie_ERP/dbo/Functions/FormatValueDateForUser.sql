

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que formatea una fecha y hora según las preferencias de visualización configuradas por el usuario del sistema. Recibe el código de usuario y un valor de fecha, consulta el identificador interno del usuario en el módulo de seguridad, y devuelve la fecha convertida a texto en el formato preferido (por ejemplo: ''15 de marzo de 2024 10:30''). Actualmente aplica el formato largo en español (''dd de MMMM de yyyy HH:mm'') como valor predeterminado. Se usa en toda la plataforma para mostrar fechas —como fechas de ingreso, órdenes médicas o atenciones— de forma legible y consistente para cada operador o profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FormatValueDateForUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FormatValueDateForUser';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una fecha formateada como cadena según la preferencia de formato configurada para el usuario, usando la cultura es-ES.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de usuario debe existir en Security.UserInt para resolver su Id, aunque dicho Id no afecta el resultado final mientras la lectura de UserConfiguration esté comentada; La fecha de entrada debe ser un datetime válido para poder formatearse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna la fecha formateada en cultura es-ES; Actualmente el formato aplicado es siempre el 4 (''dd de MMMM de yyyy HH:mm'') porque la lectura de la configuración del usuario está comentada; Nunca retorna NULL si la fecha de entrada es válida (CASE cubre todos los valores con ELSE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario; Configuración de formato de fecha por usuario; Localización es-ES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna nvarchar(30) con la fecha formateada según el valor de @dateFormat (fijo en 4 por estar comentada la lectura de Security.UserConfiguration), aplicando ELSE ''dd/MM/yyyy HH:mm:ss'' si no coincide con 0-4', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si dateFormat = 0 → Formato dd/MM/yyyy HH:mm en es-ES; si dateFormat = 1 → Formato MM/dd/yyyy HH:mm en es-ES; si dateFormat = 2 → Formato yyyy/MM/dd HH:mm en es-ES; si dateFormat = 3 → Formato dd/MMM/yyyy HH:mm en es-ES; si dateFormat = 4 → Formato ''dd de MMMM de yyyy HH:mm'' en es-ES else Formato por defecto dd/MM/yyyy HH:mm:ss en es-ES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.UserInt', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatValueDateForUser';
GO
