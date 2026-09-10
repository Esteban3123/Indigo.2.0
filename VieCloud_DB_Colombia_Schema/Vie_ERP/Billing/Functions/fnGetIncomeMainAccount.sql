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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina la cuenta contable principal de ingresos que corresponde a un ítem de facturación (servicio o producto) dentro de una orden de atención. Para servicios, evalúa si el concepto de facturación usa una cuenta única o una cuenta diferenciada por tipo de unidad funcional (hospitalización, urgencias, ambulatorio, etc.), y además distingue si el paciente es particular o pertenece a una entidad/aseguradora, retornando la cuenta de ingresos individual o de entidad según corresponda. Para productos (medicamentos, insumos), devuelve directamente la cuenta contable asociada al grupo de producto. Es utilizada en el proceso contable de facturación para garantizar que cada cargo quede registrado en la cuenta de ingresos correcta según el tipo de atención, el convenio y el concepto facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetIncomeMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetIncomeMainAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina la cuenta contable de ingreso aplicable a un detalle de orden de servicio, diferenciando entre servicios (con cuenta única o por tipo de unidad, e individual/entidad según atención particular) y productos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si @RecordType=1 (servicio), deben proveerse @BillingConceptId, @BillingConceptAccountingType y, según el caso, las cuentas individuales/entidad o el @UnitType.; Si @RecordType indica producto, debe proveerse @ProductGroupIncomeAccountId.; Para @BillingConceptAccountingType=2 debe existir una fila en Billing.BillingConceptAccount con el UnitType resuelto y el BillingConceptId; de lo contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para productos siempre se devuelve la cuenta de ingreso del grupo de producto, sin distinguir tipo de atención.; Para servicios, el tipo de atención particular (CareGroupType=3) siempre direcciona a la cuenta individual; cualquier otro tipo direcciona a la cuenta de entidad.; La resolución por tipo de unidad siempre pasa por la función Billing.fnGetUnitType antes de filtrar Billing.BillingConceptAccount.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cuenta contable de ingreso; concepto de facturación; orden de servicio; atención particular; tipo de unidad; grupo de producto; servicio vs producto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BillingConceptAccount: Cuando @RecordType=1 y @BillingConceptAccountingType=2, retorna IndividualIncomeAccountId si @CareGroupType=3 o EntityIncomeAccountId en otro caso, filtrando por UnitType=Billing.fnGetUnitType(@UnitType) y BillingConceptId=@BillingConceptId.; [RETURN_RESULT] (parámetro): Cuando @RecordType=1 y @BillingConceptAccountingType<>2, retorna @BillingConceptIndividualIncomeAccountId si @CareGroupType=3, o @BillingConceptEntityIncomeAccountId en otro caso.; [RETURN_RESULT] (parámetro): Cuando @RecordType<>1 (producto), retorna @ProductGroupIncomeAccountId.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RecordType = 1 (es un servicio) → Determina la cuenta según el tipo contable del concepto de facturación else Para productos (@RecordType<>1) retorna directamente @ProductGroupIncomeAccountId; si Servicio con @BillingConceptAccountingType = 2 (cuenta por tipo de unidad) → Consulta Billing.BillingConceptAccount filtrando por UnitType (resuelto vía Billing.fnGetUnitType) y BillingConceptId else Usa la cuenta única de ingreso recibida como parámetro (@BillingConceptIndividualIncomeAccountId o @BillingConceptEntityIncomeAccountId); si @CareGroupType = 3 (atención particular) → Retorna la cuenta de ingreso individual (IndividualIncomeAccountId / @BillingConceptIndividualIncomeAccountId) else Retorna la cuenta de ingreso de entidad (EntityIncomeAccountId / @BillingConceptEntityIncomeAccountId)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.fnGetUnitType', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingConceptAccount', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeMainAccount';
GO
