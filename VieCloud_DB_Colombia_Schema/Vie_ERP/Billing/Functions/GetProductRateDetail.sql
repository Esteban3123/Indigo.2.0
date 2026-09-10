CREATE Function [Billing].[GetProductRateDetail]
(
	@CareGroupId Int,
	@ProductId Int,
	@ServiceDate DateTime
)
Returns @ProductRateDetail Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	
	Id Int,
	ProductRateId Int,
	ProductId Int,
	InitialDate DateTime,
	EndDate DateTime,
	SalesValue Decimal(18, 2),
	SalesValueWithSurcharge Decimal(18, 2),
	GrossValue Numeric(20,2),
	TaxValue numeric(20,2)
) 
As
Begin	
	Declare @Id Int,
		@ProductRateId Int,
		@ProductIdx Int,
		@InitialDate DateTime,
		@EndDate DateTime,
		@SalesValue Decimal(18, 2),
		@SalesValueWithSurcharge Decimal(18, 2),
		@SalePriceIncludeTax bit,
		@TaxValue numeric(20,2),
		@GrossValue numeric(20,2),
		@TaxPercentage numeric(20,2),
		@RateType Tinyint,
		@RatePercentage numeric(20,2),
		@ProductCost numeric(20,2),
		@FinalProductCost numeric(20,2),
		@PercentageBasedOn Tinyint

		Select top 1 @SalePriceIncludeTax = cs.SalePriceIncludeTax from GeneralLedger.CompanySettings cs WITH(NOLOCK)

		Select Top 1 @Id = prd.Id, @ProductRateId = prd.ProductRateId, @ProductIdx = ProductId, @InitialDate = InitialDate
			, @EndDate = EndDate, @SalesValue = SalesValue, @SalesValueWithSurcharge = SalesValueWithSurcharge,
			@TaxPercentage = iif(ip.TaxedProduct=1 And ip.LiquidateSalesTaxes=1,isnull(giva.Percentage,0),0), @RateType=prd.RateType,
			@ProductCost=ip.ProductCost, @FinalProductCost=ip.FinalProductCost, @PercentageBasedOn=prd.PercentageBasedOn,
			@RatePercentage= prd.Percentage
		From [Contract].CareGroup cg With(Nolock)
		Inner Join Inventory.ProductRate pr With(Nolock) On cg.ProductRateId = pr.Id
		Inner Join Inventory.ProductRateDetail prd With(Nolock) On prd.ProductRateId = pr.Id
		inner join Inventory.InventoryProduct ip WITH(NOLOCK) on ip.Id = prd.ProductId
		LEFT join GeneralLedger.GeneralLedgerIVA giva WITH(NOLOCK) on ip.IVAId=giva.Id
		Where cg.Id = @CareGroupId And prd.ProductId = @ProductId And (prd.InitialDate <= @ServiceDate And prd.EndDate >= @ServiceDate)
		Order By prd.Id
	
	If @Id Is Null Begin
		Declare @ProductCodeName Varchar(320) = (Select Concat(Code, ' - ', [Name]) From Inventory.InventoryProduct With(Nolock) Where Id = @ProductId)
		Declare @CareGroupCodeName Varchar(320) = (Select Concat(Code, ' - ', [Name]) From [Contract].CareGroup With(Nolock) Where Id = @CareGroupId)

		Insert Into @ProductRateDetail
		Values (0, 'El producto ' + @ProductCodeName + ' no se encuentra dentro de la tarifa para el grupo de atención ' + @CareGroupCodeName + ' y fecha ' + Cast(@ServiceDate As Varchar)
			, 0,0,0,GetDate(), GetDate(), 0, 0,0,0)
	End
	Else Begin

		IF @RateType =2 BEGIN
			
			SET @SalesValue = (CASE @PercentageBasedOn
								WHEN 1 THEN ROUND( @ProductCost * (@RatePercentage/100) + @ProductCost,2)
								WHEN 2 THEN ROUND( @FinalProductCost * (@RatePercentage/100) + @FinalProductCost,2)
								ELSE @SalesValue
								END)

		END
		
		SELECT	@GrossValue = GrossValue,
				@TaxValue = TaxValue,
				@SalesValue = SalesPrice
		from billing.SetValueSalesPrice(@SalePriceIncludeTax,@SalesValue,@TaxPercentage)

		Insert Into @ProductRateDetail
		Values (1, '', @Id, @ProductRateId, @ProductIdx, @InitialDate, @EndDate, @SalesValue, @SalesValueWithSurcharge,@GrossValue,@TaxValue)
	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el precio de venta vigente de un producto (medicamento o insumo) para un grupo de atención contractual y una fecha de servicio específica. Cruza la tarifa asignada al grupo de atención con el detalle de precios del producto y aplica el porcentaje de IVA correspondiente, calculando el valor bruto, el impuesto y el precio final. Si la tarifa es de tipo porcentual, recalcula el precio de venta en función del costo del producto. Retorna el precio con y sin recargo, el valor del impuesto y un indicador de éxito o mensaje de error si el producto no tiene tarifa vigente para ese grupo de atención en esa fecha; se usa en facturación y liquidación de servicios para determinar cuánto cobrar por un producto dentro de un contrato.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetProductRateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetProductRateDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el detalle de tarifa vigente de un producto para un grupo de atención en una fecha dada, calculando precio de venta, IVA y valor bruto, o devuelve mensaje de error si no existe tarifa aplicable.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un CareGroup con ProductRate asignado (cg.ProductRateId vinculado a Inventory.ProductRate).; Debe existir un ProductRateDetail vigente cuya InitialDate <= @ServiceDate <= EndDate para el producto y la tarifa.; Debe existir configuración en GeneralLedger.CompanySettings para determinar si el precio incluye IVA.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tarifa aplicable se determina por la vigencia: prd.InitialDate <= @ServiceDate <= prd.EndDate.; Si el producto no es gravado (TaxedProduct=0), el porcentaje de IVA aplicado es 0 sin importar el IVAId asignado.; Cuando RateType=2 (tarifa por porcentaje sobre costo), el SalesValue se recalcula dinámicamente y reemplaza al valor almacenado.; El cálculo final de GrossValue, TaxValue y SalesPrice se delega a billing.SetValueSalesPrice según la configuración SalePriceIncludeTax de la empresa.; Siempre retorna exactamente una fila en la tabla resultado: o de error (StatusResult=0) o de éxito (StatusResult=1).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'tarifa de producto; grupo de atención; vigencia de tarifa; precio de venta; recargo; IVA; producto gravado; costo del producto; costo final del producto; tarifa porcentual sobre costo; precio incluye impuesto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ProductRateDetail: Si no se encuentra tarifa vigente (@Id IS NULL), inserta fila con StatusResult=0 y mensaje indicando que el producto no está en la tarifa del grupo de atención y la fecha indicada.; [INSERT] @ProductRateDetail: Si se encuentra tarifa vigente, inserta fila con StatusResult=1 y los valores de Id, ProductRateId, fechas, SalesValue, SalesValueWithSurcharge, GrossValue y TaxValue calculados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Id IS NULL (no existe ProductRateDetail vigente para el producto/grupo/fecha) → Construye mensaje con código y nombre del producto y del grupo de atención, e inserta resultado de error (StatusResult=0). else Calcula SalesValue según RateType y PercentageBasedOn, invoca billing.SetValueSalesPrice e inserta resultado exitoso (StatusResult=1).; si @RateType = 2 AND @PercentageBasedOn = 1 → SalesValue = ROUND(ProductCost * (Percentage/100) + ProductCost, 2) — precio calculado sobre costo del producto.; si @RateType = 2 AND @PercentageBasedOn = 2 → SalesValue = ROUND(FinalProductCost * (Percentage/100) + FinalProductCost, 2) — precio calculado sobre costo final del producto.; si @RateType = 2 AND @PercentageBasedOn no es 1 ni 2 → Mantiene el SalesValue original de ProductRateDetail.; si ip.TaxedProduct = 1 → @TaxPercentage = ISNULL(giva.Percentage, 0). else @TaxPercentage = 0 (producto no gravado con IVA).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'billing.SetValueSalesPrice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Contract.CareGroup; Inventory.ProductRate; Inventory.ProductRateDetail; Inventory.InventoryProduct; GeneralLedger.GeneralLedgerIVA', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetProductRateDetail';
GO
