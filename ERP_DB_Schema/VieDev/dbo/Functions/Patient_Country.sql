-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Patient_Country] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Country varchar(300)

 SET @Country  = (select Rtrim(E.Name)
					from INPACIENT A WITH(NOLOCK) 
					inner join Common.Country E  WITH(NOLOCK) on A.IDPAIS = E.ID
					where A.IPCODPACI = @IPCODPACI
				  )

    RETURN @Country  
END