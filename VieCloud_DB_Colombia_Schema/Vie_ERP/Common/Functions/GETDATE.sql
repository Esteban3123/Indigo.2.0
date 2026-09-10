-- User Defined Function

-- =============================================
-- Author:      Alvaro Heliud Herrera Novoa
-- Create Date: 2020-08-24
-- Description: Funcion Getdate() para PAAS
-- =============================================
CREATE FUNCTION [Common].[GETDATE]()
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que retorna la fecha y hora actual ajustada a la zona horaria de Colombia (SA Pacific Standard Time, UTC-5), equivalente a un GETDATE() localizado para el entorno de la plataforma PAAS. Se usa en toda la aplicación para registrar fechas y horas de eventos clínicos, administrativos y de auditoría con la hora local correcta, evitando inconsistencias por zona horaria del servidor. Reemplaza el uso directo de GETDATE() o SYSDATETIME() en stored procedures, triggers y vistas que necesiten la hora colombiana confiable.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GETDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GETDATE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha y hora actual ajustada a la zona horaria ''SA Pacific Standard Time'' para uso uniforme en entornos PaaS.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha retornada siempre está expresada en la zona horaria ''SA Pacific Standard Time''; El valor retornado nunca incluye el offset de zona horaria (se descarta al castear a DATETIME)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Retorna SYSDATETIMEOFFSET() convertido a la zona ''SA Pacific Standard Time'' y casteado a DATETIME', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
