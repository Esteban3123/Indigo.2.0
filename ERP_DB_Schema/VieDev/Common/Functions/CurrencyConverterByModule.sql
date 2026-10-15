
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