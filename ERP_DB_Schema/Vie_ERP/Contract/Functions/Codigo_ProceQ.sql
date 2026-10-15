-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Contract].[Codigo_ProceQ]
(
	@Id int
)
RETURNS varchar (300)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @resultado1 varchar (300)

	-- Add the T-SQL statements to compute the return value here
	SELECT @resultado1 = s.Code 
	FROM Billing.ServiceOrderDetailSurgical oq 
	JOIN Contract.IPSService s on s.Id=oq.IPSServiceId
	WHERE oq.Id = @Id

	-- Return the result of the function
	RETURN (@resultado1)

END