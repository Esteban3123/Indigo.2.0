-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-07-26
-- Description:	Devuelve la Cuenta NIIF Reconociemiento de Ingresos Pendientes por facturar
-- =============================================
CREATE FUNCTION [Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId]
(
	@RecordType tinyint,
	@BillingConceptId int, -- Variable solo se llena si es un servicio
	@BillingConceptAccountingType tinyint, -- Variable solo se llena si es un servicio
	@BillingConceptEntityIncomeRecognitionPendingBillingMainAccountId int, -- Variable solo se llena si es un servicio
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
			select @IncomeMainAccountId = IncomeRecognitionMainAccountId from Billing.BillingConceptAccount where UnitType = @UnitTypeFilter and BillingConceptId = @BillingConceptId
		end
		else begin --1 - Cuenta Unica de Ingreso
			set @IncomeMainAccountId = @BillingConceptEntityIncomeRecognitionPendingBillingMainAccountId
		end
	end
	else begin -- Si es un producto
		set @IncomeMainAccountId = @ProductGroupIncomeAccountId
	end
	return @IncomeMainAccountId
END