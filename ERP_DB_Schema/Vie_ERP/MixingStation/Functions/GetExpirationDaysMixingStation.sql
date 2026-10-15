CREATE FUNCTION [MixingStation].[GetExpirationDaysMixingStation]()
RETURNS INT
AS
BEGIN
    DECLARE @ExpirationDays INT;
    SELECT TOP 1 @ExpirationDays = ExpirationDays FROM MixingStation.MixingStationSetting;
    RETURN @ExpirationDays;
END;