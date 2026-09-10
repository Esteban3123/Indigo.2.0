-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 03-10-2017
-- Description:	Devuelve la categoria del tipo de unidad
-- =============================================
CREATE Function [Billing].[RoundValue]
(
	@valueToRound Decimal(18, 2),
	@roundLevel Int
)
Returns Decimal(18, 2)
As
Begin
	Declare @RoundedValue Decimal(18, 2) = @valueToRound
	
	If @roundLevel = 1
		Set @RoundedValue = Round(@RoundedValue, 0)		
	Else If @roundLevel = 10
		Set @RoundedValue = Round(@RoundedValue, -1)		
	Else If @roundLevel = 100
		Set @RoundedValue = Round(@RoundedValue, -2)		
	Else If @roundLevel = 1000
		Set @RoundedValue = Round(@RoundedValue, -3)		
	Else
		Set @RoundedValue = Round(@RoundedValue, 2)

	Return @RoundedValue
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de facturación que redondea un valor monetario según el nivel de redondeo indicado. Recibe un monto decimal y un nivel (1, 10, 100 o 1000) que determina a qué unidad se aproxima el valor: al peso, a la decena, a la centena o al millar respectivamente; si no se especifica un nivel reconocido, redondea a dos decimales. Se usa en el módulo de Billing para ajustar valores de cobro, tarifas o totales de factura según las reglas de aproximación definidas por la institución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'RoundValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'RoundValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Aplica un redondeo configurable (entero, decena, centena, millar o dos decimales por defecto) a un valor monetario según el nivel de aproximación indicado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El valor a redondear debe poder representarse como Decimal(18,2).; Para obtener un redondeo a entero, decena, centena o millar, el nivel debe ser exactamente 1, 10, 100 o 1000; cualquier otro entero activa el redondeo por defecto a 2 decimales.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es un Decimal(18,2), por lo que cualquier redondeo a entero/decena/centena/millar se almacena conservando dos decimales (con ceros).; El nivel de redondeo se interpreta como la potencia de 10 a la que se aproxima el valor (1, 10, 100, 1000); cualquier otro valor cae en el comportamiento por defecto de dos decimales.; La función es determinista: para los mismos parámetros siempre devuelve el mismo resultado y no tiene efectos colaterales.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Redondeo de valores monetarios; Facturación (Billing)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return Decimal(18,2)): Devuelve el valor redondeado según el nivel: 1→0 decimales, 10→decenas, 100→centenas, 1000→millares; en cualquier otro caso redondea a 2 decimales.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @roundLevel = 1 → Redondea al entero más cercano (ROUND(valor, 0)); si @roundLevel = 10 → Redondea a la decena más cercana (ROUND(valor, -1)); si @roundLevel = 100 → Redondea a la centena más cercana (ROUND(valor, -2)); si @roundLevel = 1000 → Redondea a la unidad de mil más cercana (ROUND(valor, -3)); si @roundLevel no coincide con 1, 10, 100 ni 1000 → Redondea a dos decimales (ROUND(valor, 2)) como comportamiento por defecto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RoundValue';
GO
