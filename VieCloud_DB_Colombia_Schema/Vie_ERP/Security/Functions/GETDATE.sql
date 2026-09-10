-- =============================================
-- Author:      Alvaro Heliud Herrera Novoa
-- Create Date: 2020-08-24
-- Description: Funcion Getdate() para PAAS
-- =============================================
CREATE FUNCTION [Security].[GETDATE]()
RETURNS DATETIME
AS
BEGIN
    -- Declare the return variable here
    DECLARE @ResultVar DATETIME

    -- Add the T-SQL statements to compute the return value here
    SELECT @ResultVar = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'SA Pacific Standard Time' AS datetime)

    -- Return the result of the function
    RETURN @ResultVar
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que retorna la fecha y hora actual ajustada a la zona horaria de Colombia (SA Pacific Standard Time, UTC-5), equivalente a un GETDATE() localizado para el entorno de nube (PaaS) donde la hora del servidor puede estar en UTC. Se usa como reemplazo estándar de GETDATE() en todos los módulos del sistema para garantizar que fechas de atención, ingresos, órdenes médicas y registros clínicos queden grabadas con la hora local colombiana correcta, independientemente del servidor donde corra la base de datos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'FUNCTION', @level1name = N'GETDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'FUNCTION', @level1name = N'GETDATE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha y hora actual ajustada a la zona horaria ''SA Pacific Standard Time'' para uso uniforme en entornos PAAS.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha retornada siempre corresponde a la zona horaria ''SA Pacific Standard Time'' independientemente de la zona del servidor.; El resultado siempre es de tipo DATETIME (sin offset).', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna SYSDATETIMEOFFSET() convertido a la zona horaria ''SA Pacific Standard Time'' y casteado a DATETIME.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
