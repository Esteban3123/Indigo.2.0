
CREATE FUNCTION [FixedAsset].[fnCalculateDepreciateValue] 
(
	@DepreciationType TINYINT,							-- @DepreciationType = Tipo de depreciación
	@DepreciatedValue NUMERIC(20,4),					-- @DepreciatedValue = Valor actual depreciado
	@ResidualValue NUMERIC(20,4),						-- @ResidualValue = Valor actual pendiente de depreciar
	@PercentageRescue numeric(5,2),						-- @PercentageRescue = Porcentaje de rescate
	@DepreciateByTimeUse bit,							-- @DepreciateByTimeUse = Especifica si el activo se deprecia por tiempo de uso	
	@UseTime decimal(3,1),								-- @UseTime = Tiempo de uso		
	@DepreciatedDays INT,								-- @DepreciatedDays = Los días actuales depreciados
	@DaysPendingDepreciate INT,							-- @DaysPendingDepreciate = Los días actuales pendientes de depreciar	
	@DepreciateDays INT									-- @DepreciateDays = Los días que se van a depreciar del mes
)
RETURNS numeric(20,4)
AS
BEGIN
	DECLARE @HistoryValue NUMERIC(20,4),			-- @HistoryValue = Se suman los campos ResidualValue + DepreciatedValue de la tabla fixedAssetPhysicalAssetDetailBook
			@TotalDays INT,							-- @TotalDays = La sumatoria de los campos DaysPendingDepreciate + DepreciatedDays de la tabla fixedAssetPhysicalAssetDetailBook
			@DepreciateValue NUMERIC(20,4)			-- @DepreciateValue = Variable que retorna el valor depreciado calculado

	SELECT	@HistoryValue = @DepreciatedValue + @ResidualValue,
			@TotalDays = @DepreciatedDays + @DaysPendingDepreciate

	--Dependiendo del tipo de depreciación se realizan los cálculos
	IF @DepreciationType = 1 --Línea recta
	BEGIN
		--Se calcula el valor del día y se multiplica el valor del día por los días depreciados del mes
		SET @DepreciateValue = (@HistoryValue / @TotalDays) * @DepreciateDays
	END
	ELSE IF @DepreciationType = 2 --Suma de dígitos
	BEGIN
		--Se calcula los puntos y se divide los dias pendientes por depreciar
		DECLARE @partialValue1 NUMERIC(20,4)
		DECLARE @partialValue2 NUMERIC(20,4)
		SET @partialValue1 = (@TotalDays * (@TotalDays + 1) / 2)
		SET @partialValue2 = @DaysPendingDepreciate / @partialValue1
		SET @DepreciateValue =  @HistoryValue * @partialValue2
	END
	ELSE IF @DepreciationType = 3 --Reducción de saldos
	BEGIN
		IF @HistoryValue = 0 
		BEGIN
			RETURN  0  ---- Se retorna en 0 para cuando el FinantialDiscountDepreciation venga en 0
		END
		--Se calcula el valor del porcentaje de salvamento, se divide por el valor y se expone 1/los dias pendientes por depreciar
		declare @RescueValue numeric(20, 4) = @HistoryValue * (@PercentageRescue/100.0) 
		SET @DepreciateValue = (1 - POWER(((@RescueValue/@HistoryValue)), (1.0/(@TotalDays/30.0)))) * @ResidualValue
		
		if (@ResidualValue - @DepreciateValue) < @RescueValue begin
			set @DepreciateValue = IIF((@ResidualValue - @RescueValue) < 0, 0, (@ResidualValue - @RescueValue))
		end

		return @DepreciateValue
	END

	--Se valida si el articulo deprecia por tiempo de uso
	IF @DepreciateByTimeUse = 1
	BEGIN
		--Se calcula el porcentaje a que corresponde el tiempo de uso de la úbicación tomando como base 8 el 100%
			DECLARE @percentageUseTime DECIMAL(20,1)
		SET @percentageUseTime = (@UseTime * 100) / 8

		-- Limitar el porcentaje a 100% como máximo
		IF @percentageUseTime > 100 
		BEGIN
			SET @percentageUseTime = 100
		END

		--Se calcula el nuevo valor con respecto al porcentaje calculado
		SET @DepreciateValue = (@DepreciateValue * @percentageUseTime) / 100
	END

	--El valor depreciado no puede ser mayor al valor residual
	IF @DepreciateValue > @ResidualValue 
	BEGIN
		SET @DepreciateValue = @ResidualValue
	END

	--Al terminar el tiempo de vida del activo este se debe depreciar completamente
	IF @DepreciateDays >= @DaysPendingDepreciate 
	BEGIN
		SET @DepreciateValue = @ResidualValue
	END

	RETURN ROUND(@DepreciateValue, 2)
END