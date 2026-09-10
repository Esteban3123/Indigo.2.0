-- =============================================
-- Author:      Giovanny Plazas
-- Modificated by : Angi Camila Duran Vargas 
-- Create Date: 27/12/2022
-- Description: Convierte de una moneda a otra tomando en cuenta una fecha de corte
-- =============================================
CREATE FUNCTION [Common].[CurrencyConverterWithDate]
(
    @Value Decimal(21,5),
	@FromCurrencyId Int,
	@ToCurrencyConvertId int,
	@Date date
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

			--Si no trae fecha, por defecto se toma la del dia
			if @Date IS NULL BEGIN
			   set @Date =  Common.GETDATE()
			end

			--se establece el Id de la moneda oficial del parametro de empresa
			SET @OfficialCurrency =(SELECT TOP 1 OfficialCurrencyId FROM GeneralLedger.CompanySettings)
		

			IF @OfficialCurrency = @FromCurrencyId BEGIN
					SELECT TOP 1 @TRMValue= Value , @TRMReverse= ValueOfficialToCurrency
					FROM Common.TRM 
					WHERE CurrencyId = @ToCurrencyConvertId AND OfficialCurrencyId = @OfficialCurrency and MeasurementDate = @Date
					ORDER BY MeasurementDate DESC

				IF @TRMValue > @TRMReverse BEGIN
					RETURN  round(@ConvertedValue* (1/@TRMValue),5)
				END

				RETURN round(@ConvertedValue* @TRMReverse,5)
			END

			--se obtiene el valor del TRM de la moneda del valor a convertir
			SELECT TOP 1	@TRMValue =Value,
							@TRMReverse = ValueOfficialToCurrency 
			FROM Common.TRM 
			WHERE CurrencyId = @FromCurrencyId AND OfficialCurrencyId = @OfficialCurrency and MeasurementDate = @Date
			ORDER BY MeasurementDate DESC
			
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
							@TRMReverse = ValueOfficialToCurrency 
				FROM Common.TRM 
				WHERE CurrencyId = @ToCurrencyConvertId AND OfficialCurrencyId = @OfficialCurrency and MeasurementDate = @Date
				ORDER BY MeasurementDate DESC

			--Se valida que el TRM no sea NULL o cero
			if @TRMValue IS NULL OR @TRMValue=0 OR @TRMReverse =NULL OR @TRMReverse=0 BEGIN
				RETURN NULL
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un monto de una moneda origen a una moneda destino tomando como referencia una fecha específica de corte. Utiliza la tabla de Tasas de Cambio (TRM) registradas en el sistema para esa fecha, junto con la moneda oficial configurada en los parámetros de la empresa (CompanySettings), para realizar la conversión en dos pasos cuando ninguna de las monedas involucradas es la oficial: primero convierte el valor a la moneda oficial y luego a la moneda destino. Es usada en procesos contables y financieros donde se requiere expresar valores en divisas extranjeras a su equivalente en otra moneda con precisión histórica según la fecha de la transacción.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverterWithDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverterWithDate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte un valor monetario entre dos monedas usando la TRM vigente en una fecha dada, apoyándose en la moneda oficial de la empresa como pivote.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings que defina la moneda oficial.; Debe existir una TRM en Common.TRM para la moneda involucrada, la moneda oficial y la fecha indicada.; Si no se provee fecha, se usa la fecha actual obtenida vía Common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda conversión usa la moneda oficial de la empresa como pivote intermedio.; La elección entre Value y ValueOfficialToCurrency depende de cuál sea mayor: el mayor define el sentido del factor (multiplicación vs división).; Cuando origen y destino coinciden no se consulta TRM.; El resultado siempre se redondea a 5 decimales (excepto en los retornos tempranos por igualdad o valor cero).; Solo se utiliza la TRM cuya MeasurementDate coincide exactamente con la fecha indicada (toma TOP 1 ordenando por MeasurementDate DESC).; Ante ausencia o invalidez de TRM la función retorna NULL en lugar de un valor estimado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conversión de moneda; TRM (Tasa Representativa del Mercado); Moneda oficial de la empresa; Fecha de corte de conversión', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Si moneda origen = moneda destino o el valor es 0, retorna el valor original sin conversión.; [RETURN_RESULT] N/A: Si el TRM consultado es NULL o 0 (en Value o ValueOfficialToCurrency), retorna NULL.; [RETURN_RESULT] N/A: Cuando la moneda origen es la oficial: si Value > ValueOfficialToCurrency multiplica por (1/Value); en caso contrario multiplica por ValueOfficialToCurrency, y retorna redondeado a 5 decimales.; [RETURN_RESULT] N/A: Cuando la moneda origen NO es la oficial: primero convierte a oficial multiplicando por Value si Value>ValueOfficialToCurrency, o dividiendo por ValueOfficialToCurrency en caso contrario.; [RETURN_RESULT] N/A: Si la moneda destino es la oficial, retorna el valor ya convertido a oficial redondeado a 5 decimales.; [RETURN_RESULT] N/A: Para convertir de oficial a moneda destino: si Value>ValueOfficialToCurrency divide por Value, en caso contrario multiplica por ValueOfficialToCurrency; retorna redondeado a 5 decimales.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Moneda origen = moneda destino, o valor a convertir = 0 → Retorna el valor original sin transformación; si Fecha de corte es NULL → Asigna la fecha actual del sistema (Common.GETDATE()); si Moneda oficial = moneda origen → Aplica conversión directa oficial→destino y termina; si TRM Value > ValueOfficialToCurrency → Usa Value como factor (multiplica al ir a oficial, divide al salir de oficial) else Usa ValueOfficialToCurrency como factor inverso (divide al ir a oficial, multiplica al salir); si TRM Value o ValueOfficialToCurrency es NULL o 0 → Retorna NULL; si Moneda destino = moneda oficial → Retorna el valor ya convertido a oficial sin segunda conversión', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.TRM', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverterWithDate';
GO
