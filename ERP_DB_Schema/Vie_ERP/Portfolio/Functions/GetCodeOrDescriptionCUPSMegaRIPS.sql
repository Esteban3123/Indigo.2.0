-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Portfolio].[GetCodeOrDescriptionCUPSMegaRIPS]
(
	@ServiceOrderDetailSurgicalId int,
	@IsCode bit
)
RETURNS varchar (300)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result varchar (300)

	if @IsCode = 1 begin
		select  @result=s.Code from Billing.ServiceOrderDetailSurgical oq 
		inner join [Contract].IPSService s on s.Id=oq.IPSServiceId
		where oq.Id =@ServiceOrderDetailSurgicalId	
	end
	else begin
		select  @result = s.Name from Billing.ServiceOrderDetailSurgical oq 
		inner join [Contract].IPSService s on s.Id=oq.IPSServiceId
		where oq.Id =@ServiceOrderDetailSurgicalId
	end
	-- Return the result of the function
	RETURN @result
END