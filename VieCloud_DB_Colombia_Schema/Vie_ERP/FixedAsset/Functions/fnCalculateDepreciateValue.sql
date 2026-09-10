
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el valor a depreciar en un período para un activo fijo, según el método de depreciación configurado: línea recta (cuota fija diaria), suma de dígitos (cuota decreciente ponderada) o reducción de saldos (porcentaje sobre saldo pendiente con valor de rescate o salvamento). Recibe como entrada el valor histórico del activo (suma del valor ya depreciado y el saldo pendiente), los días totales de vida útil, los días que se van a depreciar en el mes, y parámetros adicionales como porcentaje de rescate y tiempo de uso diario; aplica un ajuste proporcional si el activo se deprecia por horas de uso respecto a una jornada de 8 horas base. Garantiza que el valor calculado no supere el saldo pendiente por depreciar y que al finalizar la vida útil el activo quede completamente depreciado. Es utilizada en el módulo de activos fijos para la generación de las cuotas mensuales de depreciación contable y financiera.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateDepreciateValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateDepreciateValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor de depreciación mensual de un activo fijo aplicando línea recta, suma de dígitos o reducción de saldos, ajustando por tiempo de uso y topes de saldo residual.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de depreciación debe ser 1 (línea recta), 2 (suma de dígitos) o 3 (reducción de saldos); La suma de días depreciados y días pendientes (TotalDays) debe ser mayor a 0 para evitar división por cero en línea recta y suma de dígitos; Para reducción de saldos, si el valor histórico es 0 se retorna 0 sin cálculo; El porcentaje de rescate debe expresarse como porcentaje (se divide entre 100); La jornada base de uso es de 8 horas para el cálculo proporcional por tiempo de uso', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor depreciado retornado nunca excede el valor residual pendiente; Al alcanzar o superar el final de la vida útil, el activo queda completamente depreciado (valor = ResidualValue); El porcentaje aplicado por tiempo de uso nunca supera el 100%; En reducción de saldos, el saldo después de depreciar nunca queda por debajo del valor de rescate; El resultado se redondea a 2 decimales (excepto en reducción de saldos que retorna sin redondeo explícito); La jornada base para cálculo de uso es de 8 horas equivalente al 100%', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Depreciación línea recta; Depreciación suma de dígitos; Depreciación reducción de saldos; Valor residual; Valor de rescate/salvamento; Tiempo de uso del activo; Vida útil; Cuota mensual de depreciación', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando DepreciationType=1 (línea recta), retorna (HistoryValue/TotalDays)*DepreciateDays redondeado a 2 decimales; [RETURN_RESULT] N/A: Cuando DepreciationType=2 (suma de dígitos), retorna HistoryValue * (DaysPendingDepreciate / (TotalDays*(TotalDays+1)/2)); [RETURN_RESULT] N/A: Cuando DepreciationType=3 (reducción de saldos) y HistoryValue=0, retorna 0; [RETURN_RESULT] N/A: Cuando DepreciationType=3, calcula (1 - POWER(RescueValue/HistoryValue, 1/(TotalDays/30)))*ResidualValue; si (ResidualValue - DepreciateValue) < RescueValue, ajusta a max(ResidualValue - RescueValue, 0); [RETURN_RESULT] N/A: Cuando DepreciateByTimeUse=1, ajusta el valor multiplicando por (UseTime*100/8)/100, topando el porcentaje en 100%; [RETURN_RESULT] N/A: Cuando DepreciateValue > ResidualValue, retorna ResidualValue; [RETURN_RESULT] N/A: Cuando DepreciateDays >= DaysPendingDepreciate, retorna ResidualValue (deprecia completamente al cierre de vida útil)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DepreciationType = 1 → Aplica método de línea recta: valor por día por días a depreciar else Evalúa los siguientes tipos; si @DepreciationType = 2 → Aplica método de suma de dígitos sobre días totales else Evalúa reducción de saldos; si @DepreciationType = 3 → Aplica reducción de saldos con porcentaje de rescate; retorna inmediatamente sin pasar por ajustes posteriores de tiempo de uso ni topes; si @DepreciationType = 3 AND @HistoryValue = 0 → Retorna 0 directamente; si @DepreciateByTimeUse = 1 → Aplica ajuste proporcional según tiempo de uso vs jornada base de 8 horas, topando en 100%; si @DepreciateValue > @ResidualValue → Limita el valor depreciado al saldo residual; si @DepreciateDays >= @DaysPendingDepreciate → Fuerza la depreciación total al saldo residual restante', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciateValue';
GO
