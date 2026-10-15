-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 2020-11-29
-- Description:	Función para armar el ScheduleDetailHour
-- =============================================
CREATE FUNCTION [Payroll].[fnScheduleDetailHour]
(	
	@InitialDate DATETIME,
	@EndDate DATETIME,
	@IdGroup INT,
	@TotalHours INT
)
RETURNS @ScheduleDetailHour TABLE 
(
	DatetimeInitial DATETIME,
	DatetimeEnd DATETIME,
	TotalNumberHours INT, 
	NextDay BIT,
	[Event] BIT,
	Approved BIT,
	AppliedLiquidationConcept BIT
)
AS
BEGIN
	
	DECLARE @InitialTimeOrdinaryDay TIME
	DECLARE @EndTimeOrdinaryDay TIME
	DECLARE @IdPayrollParameter INT

	DECLARE @InitialDateOrdinary DATETIME
	DECLARE @EndDateOrdinary DATETIME

	DECLARE @HolidayDay BIT = 0

	SELECT @IdPayrollParameter = PayrollParameterId FROM Payroll.[Group] WHERE Id = @IdGroup

	SELECT @InitialTimeOrdinaryDay = InitialTimeOrdinaryDay, @EndTimeOrdinaryDay = EndTimeOrdinaryDay FROM Payroll.PayrollParameter WHERE Id = @IdPayrollParameter

	SET @InitialDateOrdinary = CONVERT(DATETIME, @InitialDate) + CONVERT(DATETIME, @InitialTimeOrdinaryDay)
	SET @EndDateOrdinary = CONVERT(DATETIME, @InitialDate) + CONVERT(DATETIME, @EndDateOrdinary)

	IF DAY(@InitialDate) = DAY(@EndDate) BEGIN
		-- Es el mismo día
		If (SELECT COUNT(*) FROM Common.Holiday where Holiday = @InitialDate) > 0 BEGIN
			SET @HolidayDay = 1
		END
		
		IF(SELECT DATEPART(WEEKDAY,@InitialDate)) = 7 BEGIN
			-- Es domingo
			SET @HolidayDay = 1
		END

	END ELSE BEGIN
		If (SELECT COUNT(*) FROM Common.Holiday where Holiday = @InitialDate) > 0 BEGIN
			SET @HolidayDay = 1
		END

		If (SELECT COUNT(*) FROM Common.Holiday where Holiday = @EndDate) > 0 BEGIN
			SET @HolidayDay = 1
		END

	END

	-- Inserto en la tabla de Horas y Conceptos
	DECLARE @CountInsert INT = 0

	IF @InitialDate >= @InitialDateOrdinary AND @EndDate <= @EndDateOrdinary BEGIN
		-- Día normal
		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@InitialDate, @EndDate, @TotalHours, 0, 1, 0, @HolidayDay)

	END

	IF @InitialDate >= @InitialDateOrdinary AND @EndDate > @EndDateOrdinary BEGIN
		-- Jornada noche que pasan las 9 pm
		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@InitialDate, @EndTimeOrdinaryDay, DATEDIFF(HOUR,@EndTimeOrdinaryDay,@InitialDate),0, 1, 0, @HolidayDay)

		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@EndTimeOrdinaryDay, @EndDate, DATEDIFF(HOUR,@EndDate,@EndTimeOrdinaryDay),0, 1, 0, @HolidayDay)
	END

	IF @InitialDate >= @EndDateOrdinary AND @EndDate >= @EndDateOrdinary BEGIN
		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@InitialDate, @EndDate, @TotalHours,0, 1, 0, @HolidayDay)
	END

	IF @InitialDate < @InitialDateOrdinary AND @EndDate <= @InitialDateOrdinary BEGIN
		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@InitialDate, @EndDate, @TotalHours,0, 1, 0, @HolidayDay)
	END

	IF @InitialDate < @InitialDateOrdinary AND @EndDate > @InitialDateOrdinary BEGIN
		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@InitialDate, @InitialDateOrdinary, DATEDIFF(HOUR,@InitialDateOrdinary,@InitialDate),0, 1, 0, @HolidayDay)

		INSERT INTO @ScheduleDetailHour(DatetimeInitial, DatetimeEnd, TotalNumberHours, NextDay, Event, Approved, AppliedLiquidationConcept)
		VALUES(@InitialDateOrdinary, @EndDate, DATEDIFF(HOUR,@EndDate,@InitialDateOrdinary),1, 1, 0, @HolidayDay)
	END


RETURN
END