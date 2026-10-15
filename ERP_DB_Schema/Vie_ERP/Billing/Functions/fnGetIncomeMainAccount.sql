-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 24-01-2016
-- Description:	Devuelve la cuenta contable de ingreso de un detalle de orden de servicio
-- =============================================
CREATE FUNCTION [Billing].[fnGetIncomeMainAccount]
(
	@RecordType tinyint,
	@CareGroupType tinyint,
	@BillingConceptId int, -- Variable solo se llena si es un servicio
	@BillingConceptAccountingType tinyint, -- Variable solo se llena si es un servicio
	@BillingConceptEntityIncomeAccountId int, -- Variable solo se llena si es un servicio
	@BillingConceptIndividualIncomeAccountId int, -- Variable solo se llena si es un servicio
	@ProductGroupIncomeAccountId int, -- Variable solo se solicita si es un producto
	@UnitType tinyint
)
RETURNS int
AS
BEGIN
	declare @IncomeMainAccountId int
	if @RecordType = 1 begin -- Si es un servicio
		if @BillingConceptAccountingType = 2 begin --2 - Cuenta por Tipo de Unidad
			declare @UnitTypeFilter int = (SELECT Billing.fnGetUnitType(@UnitType))			
			if @CareGroupType = 3 begin --Si es particular
				select @IncomeMainAccountId = IndividualIncomeAccountId from Billing.BillingConceptAccount where UnitType = @UnitTypeFilter and BillingConceptId = @BillingConceptId
			end
			else begin
				select @IncomeMainAccountId = EntityIncomeAccountId from Billing.BillingConceptAccount where UnitType = @UnitTypeFilter and BillingConceptId = @BillingConceptId
			end
		end
		else begin --1 - Cuenta Unica de Ingreso
			if @CareGroupType = 3 begin -- Si es particular
				set @IncomeMainAccountId = @BillingConceptIndividualIncomeAccountId
			end
			else begin 
				set @IncomeMainAccountId = @BillingConceptEntityIncomeAccountId
			end
		end
	end
	else begin -- Si es un producto
		set @IncomeMainAccountId = @ProductGroupIncomeAccountId
	end
	return @IncomeMainAccountId
END