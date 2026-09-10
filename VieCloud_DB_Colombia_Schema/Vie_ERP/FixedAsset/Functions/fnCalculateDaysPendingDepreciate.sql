CREATE FUNCTION [FixedAsset].[fnCalculateDaysPendingDepreciate] 
(
	@AdquisitionDate AS DATE,			--Fecha en la que se adquirió el Activo Fijo
	@LifeTime AS INT,					--Tiempo de Vida, depende de la unidad de tiempo de vida
	@UnitLifeTime AS TINYINT,			--Unidad de tiempor de Vida 1 Años, 2 Meses, 3 Dias
	@ApplyMinimunAmount AS BIT,			--Especifica si aplica minima cuantía, y dependiendo de ello verifica si esta dentro de los topes
	@HistoricalValue AS DECIMAL(20,4),	--Valor Histórico registrado en el libro del Activo Fijo
	@LowBidAmount AS DECIMAL(18,0),		--Valor mínimo de menor cuantía, si esta por debajo deprecia en el mes actual
	@TopMinorValue AS DECIMAL(18,0),	--Valor tope de menor cuantía, si cumple la condición se deprecia en el actual periodo fiscal
	@Depreciation30Days INT				--Variable para mirar si se depreica a 30 dias desde paramaetros de activos fijos
)
RETURNS int
AS
BEGIN
	--Variable que retorna los dias pendientes por depreciar
	DECLARE @DaysPendingDepreciate INT

	-- Variable cálculo de la diferencia total en días
	DECLARE @TotalDays INT;
	--Fecha con la cual se calculará la diferencia de días a depreciar
	DECLARE @DateAdd DATE
	
	IF @ApplyMinimunAmount = 1
	BEGIN
		IF @LowBidAmount > @HistoricalValue
		BEGIN
			SELECT @DateAdd = DATEADD(MONTH, DATEDIFF(MONTH, -1, @AdquisitionDate), 0)
		END
		ELSE IF @TopMinorValue >= @HistoricalValue
		BEGIN
			SELECT @DateAdd = DATEADD(YEAR, DATEDIFF(YEAR, -1, @AdquisitionDate), 0)
		END		
	END
	
	IF @DateAdd IS NULL
	BEGIN

		
		--Se asigna la fecha con el aumento dependiendo de la unidad de vida util
		SET @DateAdd = CASE @UnitLifeTime 
							WHEN 1 THEN DATEADD(YEAR, @LifeTime, @AdquisitionDate)
							WHEN 2 THEN DATEADD(MONTH,@LifeTime, @AdquisitionDate) 
							WHEN 3 THEN DATEADD(DAY,  @LifeTime, @AdquisitionDate) 
						END
	END

	--Se obtiene la diferencia en días de la fecha de adquisición @AdquisitionDate y la fecha temporal @DateAdd
	IF @Depreciation30Days = 1 BEGIN --SE VALIDA SI DEPREICA A 30 DIAS
		IF @UnitLifeTime = 1 OR @UnitLifeTime = 2 BEGIN
			SET @TotalDays = DATEDIFF(DAY, @AdquisitionDate, @DateAdd);
			SET @DaysPendingDepreciate = CAST((@TotalDays * 360) / 365 AS INT) --AJUSTAMOS LOS DIAS A 360 DIAS
			
		END
		ELSE BEGIN
			SET @DaysPendingDepreciate = DATEDIFF(DAY, @AdquisitionDate, @DateAdd)
		END
	END
	ELSE BEGIN
			SET @DaysPendingDepreciate = DATEDIFF(DAY, @AdquisitionDate, @DateAdd)
	END
	RETURN @DaysPendingDepreciate
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula los días pendientes por depreciar de un activo fijo, a partir de su fecha de adquisición y su vida útil (expresada en años, meses o días). Considera reglas de menor cuantía: si el valor histórico del activo está por debajo del tope mínimo se deprecia en el mes actual, y si está dentro del tope de menor cuantía se deprecia en el año fiscal actual. Adicionalmente, si el parámetro de depreciación a 30 días está activo, ajusta el cálculo de días reales a una base de 360 días por año (convención contable). Esta función es usada en el módulo de Activos Fijos para determinar cuántos días le restan de depreciación a cada bien según su vida útil y las políticas contables configuradas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateDaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateDaysPendingDepreciate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula los días pendientes por depreciar de un activo fijo según su vida útil, fecha de adquisición y reglas de menor cuantía, ajustando opcionalmente a base de 360 días.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad de vida útil debe ser 1 (años), 2 (meses) o 3 (días); de lo contrario el cálculo por vida útil queda indefinido; Si se aplica mínima cuantía, deben suministrarse los topes de menor cuantía y valor histórico para comparar', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las reglas de menor cuantía tienen prioridad sobre el cálculo por vida útil cuando están activas y se cumple alguna condición de tope; Cuando la depreciación es a 30 días, sólo se ajusta a base 360 si la unidad de vida útil es años o meses; en días se conserva la diferencia real; El resultado siempre es un entero', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo Fijo; Depreciación; Vida útil; Valor histórico; Menor cuantía; Periodo fiscal; Fecha de adquisición; Depreciación a 30 días / base 360', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve los días pendientes por depreciar como entero', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Aplica mínima cuantía y el valor mínimo de menor cuantía es mayor que el valor histórico → La fecha de cálculo se fija al primer día del mes siguiente a la fecha de adquisición (deprecia en el mes actual); si Aplica mínima cuantía y el tope de menor cuantía es mayor o igual al valor histórico → La fecha de cálculo se fija al primer día del año siguiente a la fecha de adquisición (deprecia en el periodo fiscal actual); si No se determinó fecha por reglas de menor cuantía → Se calcula la fecha sumando la vida útil a la fecha de adquisición según la unidad: años (1), meses (2) o días (3); si Depreciación a 30 días activa y la unidad de vida útil es años o meses → Se ajustan los días totales multiplicando por 360 y dividiendo entre 365 (base 360 días) else Se devuelve la diferencia exacta de días entre la fecha de adquisición y la fecha calculada', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDaysPendingDepreciate';
GO
