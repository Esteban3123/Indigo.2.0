
-- =============================================
-- Author:      Cristhian Salazar
-- Create Date: 2023-12-14
-- Description: Funcion para determinar si el servicio no es facturable
-- =============================================
CREATE FUNCTION [Billing].[ServicioNoFacturable]
(
    @CareGroupId int,
	@CupsEntityId int,
	@ProductId int
)
RETURNS int
AS
BEGIN
	declare @isBilling int = 1;


 --   select (*) 
	--from [Contract].[CareGroupBillingItemsRestriction] ci
	--inner join [Contract].[BillingItemsRestriction] br on br.Id = ci.BillingItemsRestrictionId
	--inner join [Contract].BillingItemsRestrictionDetail rd on rd.BillingItemsRestrictionId = br.Id
	--where ci.CareGroupId = @CareGroupId

	--select * from [Contract].[BillingItemsRestriction]
	--select * from [Contract].[BillingItemsRestrictionDetail]
	--select * from [Contract].[BillingItemsRestrictionDetailCondition]

	return @isBilling

END