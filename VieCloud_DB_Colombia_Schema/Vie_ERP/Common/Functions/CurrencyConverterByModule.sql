
-- =============================================
-- Author:		Giovanny Plazas L
-- Create date: 24/10/2022
-- Description:	Convierte de una moneda a otra (Retorna el Valor)
-- =============================================
CREATE Function [Common].[CurrencyConverterByModule]
(
	@Value Decimal(21,5),
	@FromCurrencyId Int,
	@ToCurrencyConvertId int,
	@OperativeUnitId int,
	@EntityName as varchar(50),
	@DateTrm as Date
)
Returns Decimal(21,5)
As
Begin 
	Declare @ConvertedValue Decimal(21, 5) = @Value

	if @FromCurrencyId = @ToCurrencyConvertId begin
		return	@ConvertedValue
	end

	if @DateTrm is null
	begin
		set @DateTrm =cast( common.GETDATE() as DATE)
	end

	if @EntityName = 'Invoice' and exists(	SELECT 1 
											from billing.SettingsBilling
											where HasCustomTRM =1) BEGIN
		Declare @OfficialCurrency int ,
				@TRMValue NUMERIC(20,5),
				@TRMReverse NUMERIC(20,5)

		set @OfficialCurrency = (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings)

			select top 1 @TRMValue =trm.Value, @TRMReverse = trm.ValueOfficialToCurrency
			FROM Billing.CustomTRM trm WITH(NOLOCK)
			where (trm.OperatingUnitId = @OperativeUnitId or @OperativeUnitId is null ) 
			and trm.CurrencyId = iif(@ToCurrencyConvertId = @OfficialCurrency,@FromCurrencyId,@ToCurrencyConvertId) and trm.OfficialCurrencyId= @OfficialCurrency AND
				@DateTrm >= trm.InitialMeasurementDate and @DateTrm <= trm.FinalMeasurementDate
			order by trm.Id DESC

			IF @TRMReverse IS NULL OR @TRMReverse =0 OR @TRMValue IS NULL OR @TRMValue =0
			BEGIN
				RETURN NULL
			END

			IF @ToCurrencyConvertId = @OfficialCurrency
			BEGIN
				RETURN ROUND( @ConvertedValue/IIF(@TRMReverse >=@TRMValue, @TRMReverse,(1/@TRMValue) ),5)
			END
			ELSE BEGIN
				RETURN ROUND(@ConvertedValue/IIF(@TRMValue >=@TRMReverse,@TRMValue,(1/@TRMReverse)),5)
			END
	end
	else begin
		--Branch_One:
	 --Logica para el cambio de moneda se translada a la siguiente funcion
		 SELECT  @ConvertedValue = [Common].[CurrencyConverterWithDate](@Value, @FromCurrencyId, @ToCurrencyConvertId,@DateTrm )
	end	

	RETURN @ConvertedValue
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un valor monetario de una moneda de origen a una moneda destino, aplicando la tasa de cambio (TRM) vigente para una fecha determinada. Cuando el módulo es ''Invoice'' (facturación) y la unidad operativa tiene configurada una TRM personalizada, consulta la tabla de tasas personalizadas (Billing.CustomTRM) para obtener el valor de conversión directa o inversa según la moneda oficial de la compañía. Si no aplica TRM personalizada, delega el cálculo a la función genérica Common.CurrencyConverterWithDate. Es utilizada principalmente en el proceso de facturación para expresar valores en moneda extranjera o local según la configuración contable de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverterByModule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverterByModule';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte un valor monetario entre dos monedas aplicando TRM personalizada cuando el módulo es facturación y está habilitada, o delegando a la conversión estándar por fecha en otro caso.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si la fecha de TRM no se suministra, se asume la fecha actual del sistema.; Para usar TRM personalizada debe existir registro en billing.SettingsBilling con HasCustomTRM=1.; Debe existir una moneda oficial configurada en GeneralLedger.CompanySettings.; Para conversión personalizada debe existir una TRM vigente en Billing.CustomTRM cuyo rango de fechas contenga la fecha indicada.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre se redondea a 5 decimales cuando se usa TRM personalizada.; Cuando origen y destino coinciden, no hay transformación del valor.; La conversión personalizada solo aplica al módulo de facturación (Invoice).; Selecciona la TRM más reciente vigente (ORDER BY Id DESC) dentro del rango de fechas.; Si no hay TRM válida en el flujo custom, la función no recurre a la conversión estándar: retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conversión de moneda; Tasa Representativa del Mercado (TRM); Moneda oficial; Facturación (Invoice); Unidad operativa; TRM personalizada', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si la moneda origen es igual a la destino, retorna el valor sin alterar.; [RETURN_RESULT] : Cuando entidad=''Invoice'' y SettingsBilling.HasCustomTRM=1, busca TRM en Billing.CustomTRM filtrando por unidad operativa (o todas si es null), moneda no oficial involucrada, moneda oficial y rango de fechas; retorna NULL si no se encuentra TRM o sus valores son 0/NULL.; [RETURN_RESULT] : Si destino = moneda oficial: retorna Valor / max(TRMReverse, 1/TRMValue) redondeado a 5 decimales.; [RETURN_RESULT] : Si destino ≠ moneda oficial dentro del flujo Invoice con TRM custom: retorna Valor / max(TRMValue, 1/TRMReverse) redondeado a 5 decimales.; [RETURN_RESULT] : Si no aplica el flujo Invoice con TRM custom, delega la conversión a Common.CurrencyConverterWithDate y retorna su resultado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Moneda origen = Moneda destino → Retorna el valor original sin conversión else Continúa con la lógica de conversión; si EntityName=''Invoice'' y existe configuración con HasCustomTRM=1 → Aplica conversión usando Billing.CustomTRM contra la moneda oficial de la empresa else Delega a Common.CurrencyConverterWithDate; si Moneda destino = Moneda oficial de la compañía → Divide el valor por max(TRMReverse, 1/TRMValue) else Divide el valor por max(TRMValue, 1/TRMReverse); si TRMValue o TRMReverse son NULL o 0 → Retorna NULL (no se puede convertir)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterWithDate; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'billing.SettingsBilling; GeneralLedger.CompanySettings; Billing.CustomTRM', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByModule';
GO
