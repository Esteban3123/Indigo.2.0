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