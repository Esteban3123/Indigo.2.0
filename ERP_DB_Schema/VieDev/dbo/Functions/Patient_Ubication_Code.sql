-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Patient_Ubication_Code] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Ubication_code varchar(300)

   SET @Ubication_code = (SELECT RTRIM(B.AUUBICACI) 
				from Admissions.PatientAddress A 
				inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID 
				where ipcodpaci = @IPCODPACI and IsMain = 1)

    -- Return the result of the function
    RETURN @Ubication_code
END