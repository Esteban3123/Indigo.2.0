
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina si un servicio es facturable o no, según el grupo de atención (contrato/convenio), el código CUPS de la entidad y el producto asociado. Recibe tres parámetros de negocio: el grupo de atención (CareGroupId), el ítem de servicio CUPS (CupsEntityId) y el producto o portafolio (ProductId), y devuelve un indicador entero donde 1 significa que el servicio sí es facturable. Está diseñada para ser consultada durante el proceso de facturación con el fin de validar restricciones de facturación por contrato antes de generar un cobro. Actualmente retorna siempre 1 (facturable), lo que indica que la lógica de restricciones por contrato está pendiente de implementación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'ServicioNoFacturable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'ServicioNoFacturable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Función stub que pretende determinar si un servicio no es facturable según grupo de atención, CUPS y producto, pero actualmente solo retorna un valor fijo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'ServicioNoFacturable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado es constante = 1 sin importar los parámetros recibidos.; La lógica de validación contra tablas de restricciones está comentada y no se ejecuta.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'ServicioNoFacturable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'facturación; servicio facturable; grupo de atención (CareGroup); CUPS; producto; restricciones de ítems de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'ServicioNoFacturable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Siempre retorna 1 (valor fijo de @isBilling), sin evaluar ninguna restricción real.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'ServicioNoFacturable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'ServicioNoFacturable';
GO
