CREATE FUNCTION [MixingStation].[GetExpirationDaysMixingStation]()
RETURNS INT
AS
BEGIN
    DECLARE @ExpirationDays INT;
    SELECT TOP 1 @ExpirationDays = ExpirationDays FROM MixingStation.MixingStationSetting;
    RETURN @ExpirationDays;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retorna la cantidad de días de vencimiento configurada para la estación de mezcla (mixing station). Consulta el parámetro ''ExpirationDays'' de la tabla de configuración de la estación de mezcla y devuelve un único valor entero. Se usa para determinar el tiempo de vida útil o caducidad de las mezclas o fórmulas preparadas, aplicable en la gestión de inventario y control de calidad de preparaciones farmacéuticas o nutricionales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetExpirationDaysMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetExpirationDaysMixingStation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el número de días de vigencia/expiración configurado para la estación de mezcla, usado como parámetro operativo en la preparación de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExpirationDaysMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en MixingStation.MixingStationSetting; de lo contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExpirationDaysMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se considera un único valor de configuración: SELECT TOP 1 sin ORDER BY toma un registro arbitrario de la tabla de configuración; No modifica datos; función de solo lectura', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExpirationDaysMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estación de mezcla; días de expiración; configuración de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExpirationDaysMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.MixingStationSetting', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExpirationDaysMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetExpirationDaysMixingStation';
GO
