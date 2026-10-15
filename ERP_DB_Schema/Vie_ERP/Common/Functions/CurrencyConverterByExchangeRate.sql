
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