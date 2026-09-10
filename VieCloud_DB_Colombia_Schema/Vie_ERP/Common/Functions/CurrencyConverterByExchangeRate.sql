
-- =============================================
-- Author:      Angi Camila Duran Vargas  
-- Create Date: 07/08/2023
-- Description: Convierte de una moneda a otra tomando en cuenta las tablas de exchange
-- =============================================
CREATE FUNCTION [Common].[CurrencyConverterByExchangeRate]
(
    @Value Decimal(21,5),
	@FromCurrencyId Int,
	@ToCurrencyConvertId int,
	@EntityName as varchar(100),
	@EntityId as int,
	@DateTrm as Date
)
RETURNS Decimal(21,5)
AS
BEGIN
   Declare @ConvertedValue Decimal(21, 5) = @Value,
			@OfficialCurrency int ,
			@TRMValue NUMERIC(20,5),
			@TRMReverse NUMERIC(20,5)

			--Si la moneda del valor que viene es igual a la que se va a convertir, No se hace Conversion y se retorna el valor
			-- o si el valor a convertir es 0
			IF (@FromCurrencyId = @ToCurrencyConvertId) OR @Value=0 BEGIN
				RETURN  @ConvertedValue
			END

			if @DateTrm is null
			begin
				set @DateTrm =cast(common.GETDATE() as DATE)
			end

			--se establece el Id de la moneda oficial del parametro de empresa
			SET @OfficialCurrency =(SELECT TOP 1 OfficialCurrencyId FROM GeneralLedger.CompanySettings)
		
		if @EntityName = 'AccountReceivable' BEGIN
			IF @OfficialCurrency = @FromCurrencyId BEGIN
					SELECT TOP 1 @TRMValue= Value , @TRMReverse= ValueReverse
					FROM Portfolio.AccountReceivableExchangeRate

				IF @TRMValue > @TRMReverse BEGIN
					RETURN  round(@ConvertedValue* (1/@TRMValue),5)
				END

				RETURN round(@ConvertedValue* @TRMReverse,5)
			END

			--se obtiene el valor del TRM de la moneda del valor a convertir
			SELECT TOP 1	@TRMValue =Value,
							@TRMReverse = ValueReverse 
			FROM Portfolio.AccountReceivableExchangeRate
			where AccountReceivableId = @EntityId
			--Se valida que el TRM no sea NULL o cero
			if @TRMValue IS NULL OR @TRMValue=0 OR @TRMReverse =NULL OR @TRMReverse=0 BEGIN
				RETURN NULL
			END

			--Se convierte el valor a la moneda oficial del sistema,
			--(Se multiplica si el valor esta en la moneda oficial (@TRMValue) o se divide si es el valor reverso(@TRMReverse) dependiedo quien sea mayor)			 
			IF @TRMValue >@TRMReverse BEGIN
				SET @ConvertedValue = @ConvertedValue* @TRMValue
			END
			ELSE BEGIN
				SET @ConvertedValue = @ConvertedValue/@TRMReverse
			END

			--Si la moneda a convertir es igual a la oficial retornamos el valor convertido
			IF @ToCurrencyConvertId = @OfficialCurrency BEGIN
				RETURN round(@ConvertedValue,5)
			END

			--Obtenemos el valor del TRM de la moneda a convertir
				SELECT TOP 1 @TRMValue =Value,
							@TRMReverse = ValueReverse 
				FROM Portfolio.AccountReceivableExchangeRate
				where AccountReceivableId = @EntityId
			--Se valida que el TRM no sea NULL o cero
			if @TRMValue IS NULL OR @TRMValue=0 OR @TRMReverse =NULL OR @TRMReverse=0 BEGIN
				RETURN NULL
			END
		END
		--Se realiza la conversion, (tener en cuenta que se 
		--DIVIDE si el valor esta en la moneda oficial (@TRMValue) o se MULTIPLICA si es el valor reverso(@TRMReverse) dependiedo quien sea mayor)
		IF @TRMValue >@TRMReverse BEGIN
			SET @ConvertedValue = @ConvertedValue/@TRMValue	
		END
		ELSE BEGIN
			SET @ConvertedValue = @ConvertedValue*@TRMReverse
		END

		Return round(@ConvertedValue,5)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un monto de una moneda origen a una moneda destino aplicando las tasas de cambio (TRM) registradas en el sistema. Utiliza los parámetros de empresa para identificar la moneda oficial y consulta la tabla de tasas de cambio de cuentas por cobrar (AccountReceivableExchangeRate) para obtener el valor de conversión y su inverso. Aplica la lógica de multiplicación o división según cuál de los dos valores (TRM directo o TRM inverso) es mayor, convirtiendo primero a la moneda oficial y luego a la moneda destino si es necesario. Se usa principalmente en la gestión de cartera y facturación en múltiples monedas (multimoneda), garantizando que los valores monetarios queden expresados en la divisa requerida según el contexto de negocio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverterByExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverterByExchangeRate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte un valor monetario entre dos monedas usando las tasas de cambio (TRM) registradas para una entidad (p. ej. cuenta por cobrar) y la moneda oficial de la empresa.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido.; Para EntityName=''AccountReceivable'', debe existir un registro en Portfolio.AccountReceivableExchangeRate asociado al EntityId con Value y ValueReverse no nulos ni cero.; Si no se provee fecha (@DateTrm), se asume la fecha actual obtenida de Common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre se redondea a 5 decimales cuando hay conversión.; Si origen y destino son iguales, no se realiza conversión.; La selección entre Value y ValueReverse depende de cuál sea mayor: el mayor se usa como divisor cuando se sale de la moneda oficial y como multiplicador al entrar a la oficial.; Sin TRM válido (NULL o cero), la función no devuelve un valor convertido sino NULL.; La moneda oficial se determina siempre desde GeneralLedger.CompanySettings (primer registro).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Moneda oficial; Tasa de cambio (TRM); Cuenta por cobrar (AccountReceivable); Conversión de moneda; Valor reverso de TRM', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si la moneda origen es igual a la moneda destino, o el valor es 0, retorna el mismo valor sin conversión.; [RETURN_RESULT] : Si el TRM (Value o ValueReverse) es NULL o cero para la entidad, retorna NULL.; [RETURN_RESULT] : Cuando la moneda origen es la oficial y EntityName=''AccountReceivable'': si Value > ValueReverse, retorna valor * (1/Value); de lo contrario, retorna valor * ValueReverse, redondeado a 5 decimales.; [RETURN_RESULT] : Cuando la moneda destino es la oficial: convierte multiplicando por Value si Value>ValueReverse, o dividiendo entre ValueReverse en caso contrario, y retorna redondeado a 5 decimales.; [RETURN_RESULT] : Conversión final hacia moneda no oficial: divide entre Value si Value>ValueReverse, o multiplica por ValueReverse en caso contrario, redondeado a 5 decimales.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @FromCurrencyId = @ToCurrencyConvertId OR @Value = 0 → Retorna el valor original sin conversión.; si @DateTrm IS NULL → Asigna la fecha actual del sistema (Common.GETDATE).; si @EntityName = ''AccountReceivable'' AND @OfficialCurrency = @FromCurrencyId → Toma TRM sin filtro de entidad y aplica conversión inversa según cuál valor (Value o ValueReverse) sea mayor.; si @EntityName = ''AccountReceivable'' AND moneda origen distinta a la oficial → Obtiene TRM filtrado por AccountReceivableId=@EntityId y convierte a moneda oficial.; si @TRMValue IS NULL OR =0 OR @TRMReverse IS NULL OR =0 → Retorna NULL. else Continúa con la conversión.; si @TRMValue > @TRMReverse (al convertir hacia oficial) → Multiplica el valor por @TRMValue. else Divide el valor entre @TRMReverse.; si @ToCurrencyConvertId = @OfficialCurrency → Retorna el valor ya convertido a la moneda oficial.; si @TRMValue > @TRMReverse (al convertir desde oficial a destino) → Divide el valor entre @TRMValue. else Multiplica el valor por @TRMReverse.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.AccountReceivableExchangeRate', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterByExchangeRate';
GO
