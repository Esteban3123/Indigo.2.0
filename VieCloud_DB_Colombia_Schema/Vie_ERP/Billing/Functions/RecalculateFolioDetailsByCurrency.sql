
CREATE Function [Billing].[RecalculateFolioDetailsByCurrency]
(
	@revenueControlDetailId INTEGER,
	@currencyId				INTEGER,
	@dateTRM				DATE,
	@OperativeUnitId		INTEGER
)
Returns @RecalculateFolioDetails Table
(
	StatusResult			Bit,
	MessageResult			Varchar(Max),
	
	RevenueControlDetailId	INTEGER,
	SodDistributionId		INTEGER,
	SubTotalSalesPrice		NUMERIC(20,2),
	GrandTotalDiscount		NUMERIC(20,2),
	UnitGrossValue			NUMERIC(20,2),
	TaxPercentage			NUMERIC(20,2),
	NetWorth				NUMERIC(20,2),
	NetUnitValue			NUMERIC(20,2),
	GrandTotalTaxes			NUMERIC(20,2),
	GrandTotalSalesPrice	NUMERIC(20,2),
	TotalSalesPrice			NUMERIC(20,2),
	InvoicedQuantity		INTEGER,
	IvaId						INTEGER,
	InvoiceThirdPartySalesValue NUMERIC(20,2),
	SubTotalPatientSalesPrice	NUMERIC(20,2),
	CurrencyId					INTEGER,
	CurrencyAbbreviation		Varchar(4),
	CurrencyName				Varchar(20)
) 
As
Begin	
	Declare @OfficialCurrencyId  integer,
			@LiquidateMasterAccount BIT
	/***********Tabla variable para recalcular los valores segun moneda********************/
	DECLARE @TempFolioDetailConverted as TABLE(	RevenueControlDetailId		INTEGER NOT NULL,
												SodDistributionId			INTEGER NOT NULL,
												SubTotalSalesPrice			NUMERIC(20,2) ,
												GrandTotalDiscount			NUMERIC(20,2) ,
												TaxPercentage				NUMERIC(20,2),
												IvaId						INTEGER,
												SubTotalPatientSalesPrice	NUMERIC(20,2),
												GrandTotalTaxes				NUMERIC(20,2),
												CurrencyId					INTEGER)
	
		If @revenueControlDetailId is null begin
			insert into @RecalculateFolioDetails (	StatusResult,
													MessageResult)
			values (0, 'Id del folio vacío')
		end

		set @OfficialCurrencyId = (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings WITH(NOLOCK))
		SET @LiquidateMasterAccount =(	SELECT top 1 ISNULL(LiquidateMasterAccount,0) 
										from Billing.SettingsBilling WITH(NOLOCK) 
										where @OperativeUnitId is null OR IdOperatingUnit=@OperativeUnitId
										order by LiquidateMasterAccount DESC);

		WITH cte_ValuesCol as (	SELECT (ssp.GrossValue + sodd.GrandTotalDiscount) as GrossValue,
										ssp.GrossValue as NetWorth,
										ssp.TaxValue,
										ssp.SalesPrice as GrandTotalSalesPrice,
										sodd.Id ServicerOrderDId
								FROM Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
								JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId=sod.Id
								LEFT JOIN GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK) on sod.IvaId=gli.Id
								CROSS APPLY Billing.SetValueSalesPrice(1,sodd.GrandTotalSalesPrice,ISNULL(gli.Percentage,0)) ssp
								where sodd.RevenueControlDetailId = @revenueControlDetailId and @LiquidateMasterAccount=0
								)

		INSERT INTO @TempFolioDetailConverted
		SELECT	rcd.Id as RevenueControlDetailId,
				sodd.Id as SodDistributionId,
				ROUND([Common].[CurrencyConverterByModule](ISNULL(cte.GrossValue,sodd.SubTotalSalesPrice),@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),2),
				ROUND([Common].[CurrencyConverterByModule](sodd.GrandTotalDiscount,@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),2),				
				IIF(ISNULL(cte.TaxValue,sodd.GrandTotalTaxes)>0,ISNULL(gli.Percentage,0),0) as TaxPercentage,
				IIF(ISNULL(cte.TaxValue,sodd.GrandTotalTaxes)>0,gli.Id,NULL) as IvaId,
				[Common].[CurrencyConverterByModule](iif(@LiquidateMasterAccount = 1,0,sodd.SubTotalPatientSalesPrice),@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),
				ROUND([Common].[CurrencyConverterByModule](ISNULL(cte.TaxValue,sodd.GrandTotalTaxes),@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),2),
				ISNULL(@CurrencyId,@OfficialCurrencyId)
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
		JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on sodd.RevenueControlDetailId=rcd.Id
		LEFT JOIN GeneralLedger.GeneralLedgerIVA gli with(NOLOCK) on sod.IvaId = gli.Id
		LEFT JOIN cte_ValuesCol cte on cte.ServicerOrderDId=sodd.Id
		WHERE RevenueControlDetailId = @revenueControlDetailId AND sod.IsDelete = 0

		INSERT INTO @RecalculateFolioDetails
		SELECT	iif(temp.SubTotalSalesPrice is null,0,1),
				iif(temp.SubTotalSalesPrice is null,'Valores no recalculados','Valores recalculados'),
				temp.RevenueControlDetailId,
				temp.SodDistributionId,
				temp.SubTotalSalesPrice,
				temp.GrandTotalDiscount,
				ROUND(temp.SubTotalSalesPrice/sodd.Quantity,2) AS UnitGrossValue,
				temp.TaxPercentage,
				(temp.SubTotalSalesPrice - temp.GrandTotalDiscount) AS NetWorth,
				ROUND((temp.SubTotalSalesPrice - temp.GrandTotalDiscount)/sodd.Quantity,2) AS NetUnitValue,
				-------------------------------------------------------------------------------------
				temp.GrandTotalTaxes AS GrandTotalTaxes,
				-------------------------------------------------------------------------------------
				(temp.SubTotalSalesPrice - temp.GrandTotalDiscount) + temp.GrandTotalTaxes AS GrandTotalSalesPrice,
				-------------------------------------------------------------------------------------
				((temp.SubTotalSalesPrice - temp.GrandTotalDiscount) + temp.GrandTotalTaxes) / sodd.Quantity AS TotalSalesPrice,
				-------------------------------------------------------------------------------------
				sodd.Quantity AS InvoicedQuantity,
				temp.IvaId,
				-------------------------------------------------------------------------------------
				(temp.SubTotalSalesPrice - temp.GrandTotalDiscount) + temp.GrandTotalTaxes - temp.SubTotalPatientSalesPrice AS InvoiceThirdPartySalesValue,
				-------------------------------------------------------------------------------------
				temp.SubTotalPatientSalesPrice,
				temp.CurrencyId,
				iso.CodeAbbreviation,
				iso.CurrencyName
		FROM @TempFolioDetailConverted temp
		INNER JOIN Billing.ServiceOrderDetailDistribution sodd with(NOLOCK) on  sodd.Id=temp.SodDistributionId
		INNER JOIN Common.Currency c with(NOLOCK) on temp.CurrencyId = c.Id
		INNER JOIN Common.ISO4217 iso WITH(NOLOCK) on c.ISO4217Id=iso.Id
		Return

End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que recalcula los valores monetarios de los ítems de un folio de facturación (detalle de control de ingresos) expresándolos en una moneda específica distinta a la moneda oficial de la empresa, aplicando la tasa de cambio TRM de una fecha determinada. Para cada distribución de orden de servicio asociada al folio indicado, convierte los valores brutos, descuentos, impuestos (IVA), precio neto, valor unitario, total a cargo del tercero pagador (EPS/aseguradora) y valor a cargo del paciente, utilizando el convertidor de moneda por módulo de facturación. Tiene en cuenta la configuración de liquidación por cuenta maestra de la unidad operativa para ajustar si el valor del paciente se incluye o se lleva a cero. Se usa principalmente en la presentación y liquidación de facturas en moneda extranjera, permitiendo mostrar los totales de servicios, procedimientos, medicamentos e insumos hospitalarios en la divisa requerida por el contrato o el pagador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'RecalculateFolioDetailsByCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'RecalculateFolioDetailsByCurrency';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recalcula los valores monetarios (subtotales, descuentos, impuestos, totales y participación del paciente/tercero) de los detalles de un folio de facturación convirtiéndolos a una moneda destino con la TRM de una fecha dada.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador del detalle del control de ingresos (folio) debe estar informado; si es nulo se retorna fila de error.; Debe existir registro en GeneralLedger.CompanySettings con la moneda oficial configurada.; El detalle debe tener distribuciones en Billing.ServiceOrderDetailDistribution con su correspondiente Billing.ServiceOrderDetail no eliminado (IsDelete=0).; La moneda destino debe estar registrada en Common.Currency y vinculada a Common.ISO4217.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La moneda destino siempre se resuelve a un valor no nulo (si no se especifica, se usa la moneda oficial de la empresa).; Cuando la cuenta maestra se liquida (@LiquidateMasterAccount=1), el valor del paciente convertido siempre es 0.; Solo se procesan distribuciones cuyo detalle de orden de servicio no esté marcado como eliminado (sod.IsDelete=0).; Los valores monetarios convertidos se redondean a 2 decimales (excepto SubTotalPatientSalesPrice que se devuelve sin redondeo explícito).; El IVA solo se aplica si el valor de impuestos calculado o existente es mayor a cero; en caso contrario el porcentaje se anula.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Folio de facturación; Control de ingresos; Distribución de orden de servicio; Conversión de moneda (TRM); Moneda oficial de la empresa; IVA; Liquidación de cuenta maestra; Valor del paciente vs tercero pagador; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @RecalculateFolioDetails: Cuando @revenueControlDetailId es NULL → inserta una fila con StatusResult=0 y MessageResult=''Id del folio vacío''.; [RETURN_RESULT] @RecalculateFolioDetails: Para cada distribución del folio (sod.IsDelete=0) retorna los valores recalculados y convertidos a la moneda destino, marcando StatusResult=1 (''Valores recalculados'') o 0 (''Valores no recalculados'') según si SubTotalSalesPrice fue calculado.; [RETURN_RESULT] @RecalculateFolioDetails: Calcula NetWorth = SubTotalSalesPrice - GrandTotalDiscount; GrandTotalSalesPrice = NetWorth + GrandTotalTaxes; UnitGrossValue, NetUnitValue y TotalSalesPrice se obtienen dividiendo entre sodd.Quantity; InvoiceThirdPartySalesValue = GrandTotalSalesPrice - SubTotalPatientSalesPrice.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @revenueControlDetailId IS NULL → Inserta fila de error ''Id del folio vacío'' con StatusResult=0. else Procede al recálculo y conversión de moneda de todos los detalles del folio.; si @LiquidateMasterAccount = 0 (parámetro de Billing.SettingsBilling para la unidad operativa) → Usa el CTE cte_ValuesCol que recalcula GrossValue, NetWorth, TaxValue y SalesPrice mediante Billing.SetValueSalesPrice a partir de GrandTotalSalesPrice y el porcentaje de IVA. else Cuando @LiquidateMasterAccount = 1: SubTotalPatientSalesPrice se fuerza a 0 (no se liquida valor del paciente) y se usan los valores originales de sodd (GrandTotalTaxes, SubTotalSalesPrice).; si ISNULL(cte.TaxValue, sodd.GrandTotalTaxes) > 0 → Asigna TaxPercentage = gli.Percentage e IvaId = gli.Id. else TaxPercentage=0 e IvaId=NULL.; si @CurrencyId IS NULL → Usa @OfficialCurrencyId (moneda oficial de la empresa) como moneda destino para la conversión. else Usa la moneda recibida en @CurrencyId.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterByModule; Billing.SetValueSalesPrice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Billing.SettingsBilling; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; GeneralLedger.GeneralLedgerIVA; Billing.RevenueControlDetail; Common.Currency; Common.ISO4217', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'RecalculateFolioDetailsByCurrency';
GO
