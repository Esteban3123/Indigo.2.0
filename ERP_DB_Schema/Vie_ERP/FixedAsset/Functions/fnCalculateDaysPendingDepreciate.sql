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

	SET @DaysPendingDepreciate =
    CASE 
        WHEN @Depreciation30Days = 1 AND @UnitLifeTime = 1 THEN @LifeTime * 360
        WHEN @Depreciation30Days = 1 AND @UnitLifeTime = 2 THEN @LifeTime * 30
        ELSE DATEDIFF(DAY, @AdquisitionDate, @DateAdd)

END
	RETURN @DaysPendingDepreciate
END 