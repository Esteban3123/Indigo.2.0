
CREATE Function [Billing].[SetValueSalesPrice]
(
	@FlagTaxInclude As BIT,
	@SalesPrice As Decimal(20,2),
	@TaxPercent As Decimal(10,2)
)
Returns @SetValueSalesPrice Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	
	GrossValue decimal(20,2),
	TaxValue decimal(20,2),
	SalesPrice decimal(20,2)
) 
As
Begin	
	set @SalesPrice= ISNULL(@SalesPrice,0)
	set @TaxPercent = ISNULL(@TaxPercent,0)

	declare @_grossValue decimal(20,2),
			@_taxValue decimal(20,2),
			@_salesPrice decimal(20,2)

	if @FlagTaxInclude=1 begin
		set @_salesPrice = @SalesPrice
		set @_grossValue =ROUND(@SalesPrice/((@TaxPercent/100) + 1),2)
		set @_taxValue =  iif(@TaxPercent=0,0,ROUND(@SalesPrice - (@SalesPrice/((@TaxPercent/100) + 1)),2))
	end
	else begin
		set @_grossValue = @SalesPrice
		set @_taxValue = ROUND(@SalesPrice * (@TaxPercent/100),2)
		set @_salesPrice = @SalesPrice + ROUND((@SalesPrice * (@TaxPercent/100)),2)
	end

	insert INTO @SetValueSalesPrice VALUES (1, 'Calculó existosamente', @_grossValue,@_taxValue,@_salesPrice)

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de facturación que calcula los componentes de precio de venta de un servicio o producto, descomponiendo el valor en precio base (sin impuesto) y valor del impuesto (IVA u otro tributo), según el porcentaje de impuesto indicado. Recibe como entrada el precio de venta, el porcentaje de impuesto y una bandera que indica si el impuesto ya está incluido en el precio o si debe sumarse aparte. Retorna el valor bruto o base gravable, el valor del impuesto calculado y el precio de venta final, permitiendo que el módulo de facturación desagregue correctamente los componentes tributarios de cada ítem facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'SetValueSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'SetValueSalesPrice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Descompone un precio de venta en valor bruto, valor de impuesto y precio final, según si el precio recibido ya incluye o no el impuesto indicado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@SalesPrice y @TaxPercent pueden venir nulos; se reemplazan por 0.; @FlagTaxInclude debe indicar si el precio de venta recibido ya contiene el impuesto (1) o no (0).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores nulos de precio o porcentaje de impuesto se normalizan a 0 antes de cualquier cálculo.; Todos los montos calculados se redondean a 2 decimales.; La función siempre devuelve exactamente una fila con StatusResult=1 y MessageResult=''Calculó existosamente'' (no implementa rama de error).; Se cumple la identidad SalesPrice ≈ GrossValue + TaxValue en ambos modos de cálculo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Precio de venta; Impuesto (tax); Valor bruto; Precio con/sin impuesto incluido', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @SetValueSalesPrice: Siempre inserta una única fila con StatusResult=1, MessageResult=''Calculó existosamente'' y los tres montos calculados (GrossValue, TaxValue, SalesPrice).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @FlagTaxInclude = 1 (el precio ya incluye impuesto) → SalesPrice se mantiene; GrossValue = ROUND(SalesPrice / ((TaxPercent/100)+1), 2); TaxValue = SalesPrice - GrossValue (0 si TaxPercent=0) else SalesPrice no incluye impuesto: GrossValue = SalesPrice; TaxValue = ROUND(SalesPrice * TaxPercent/100, 2); SalesPrice final = SalesPrice + TaxValue; si @TaxPercent = 0 y @FlagTaxInclude = 1 → TaxValue se fuerza a 0 mediante IIF, evitando cálculo redundante', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'SetValueSalesPrice';
GO
