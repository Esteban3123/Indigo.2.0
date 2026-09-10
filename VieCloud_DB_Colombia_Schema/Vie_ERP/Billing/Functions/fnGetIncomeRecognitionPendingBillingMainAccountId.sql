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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina la cuenta contable NIIF de reconocimiento de ingresos pendientes por facturar, según si el registro corresponde a un servicio o a un producto. Para servicios, evalúa el tipo de contabilización del concepto de facturación: si usa cuenta única de ingreso, retorna la cuenta configurada directamente en la entidad; si usa cuenta por tipo de unidad, consulta la tabla BillingConceptAccount (que relaciona conceptos de facturación con cuentas contables) filtrando por el tipo de unidad resuelto mediante la función fnGetUnitType. Para productos, retorna la cuenta de ingresos del grupo de producto. Esta función es clave en los procesos de cierre contable y reconocimiento de ingresos bajo NIIF para ítems aún no facturados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve la cuenta contable NIIF de reconocimiento de ingresos pendientes por facturar, diferenciando entre servicios (con cuenta única o por tipo de unidad) y productos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Para servicios con contabilización por tipo de unidad debe existir un registro en Billing.BillingConceptAccount que coincida con el concepto y el tipo de unidad resuelto; El tipo de unidad debe ser resoluble por Billing.fnGetUnitType cuando aplica', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El tipo de registro 1 corresponde a servicio; cualquier otro valor se trata como producto; El AccountingType 2 corresponde a ''Cuenta por Tipo de Unidad''; otros valores implican ''Cuenta Única de Ingreso''; Para servicios con cuenta única no se consulta BillingConceptAccount; se usa directamente el parámetro entidad; Para productos no se evalúa el tipo de contabilización ni el tipo de unidad', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento de ingresos NIIF; Ingresos pendientes por facturar; Concepto de facturación; Tipo de unidad; Cuenta contable de ingresos; Grupo de producto; Servicio vs producto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si @RecordType=1 (servicio) y @BillingConceptAccountingType=2, retorna IncomeRecognitionMainAccountId desde Billing.BillingConceptAccount filtrando por UnitType y BillingConceptId; [RETURN_RESULT] : Si @RecordType=1 (servicio) y @BillingConceptAccountingType<>2 (cuenta única), retorna @BillingConceptEntityIncomeRecognitionPendingBillingMainAccountId; [RETURN_RESULT] : Si @RecordType<>1 (producto), retorna @ProductGroupIncomeAccountId', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RecordType = 1 (servicio) → Evalúa el tipo de contabilización del concepto de facturación else Trata el ítem como producto y usa la cuenta de ingresos del grupo de producto; si @BillingConceptAccountingType = 2 (Cuenta por Tipo de Unidad) → Resuelve el UnitType vía fnGetUnitType y consulta BillingConceptAccount else Usa la cuenta única de ingreso entidad recibida como parámetro', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.fnGetUnitType', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingConceptAccount', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetIncomeRecognitionPendingBillingMainAccountId';
GO
