CREATE FUNCTION [dbo].[fnCalculateDateFrecuencyCurrentPeriod] 
(
    @Year int,
    @Date datetime,
    @UnitFrequency int
)
RETURNS datetime
AS
BEGIN
    declare @month as int
    declare @day as int

    if @UnitFrequency = 5 begin
        return DATEFROMPARTS(@Year, 12, 30)
    end

    if @UnitFrequency = 6 begin
        if CEILING(MONTH(@Date)/6) = 0  begin
            return DATEFROMPARTS(@Year, 6, 30)
        end
        return DATEFROMPARTS(@Year, 12, 30)
    end

    if @UnitFrequency = 7 begin
        declare @trimestre as int = CEILING(MONTH(@Date)/4) + 1
            return DATEFROMPARTS(@Year, @trimestre * 3, 30)
    end

    return eomonth(@Date)
END