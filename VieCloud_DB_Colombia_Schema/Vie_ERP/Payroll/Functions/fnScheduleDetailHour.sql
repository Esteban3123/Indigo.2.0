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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de nómina que, dado un rango de horas trabajadas (fecha/hora inicio y fin), el grupo de nómina del empleado y el total de horas, desglosa el turno en tramos horarios clasificados según la jornada ordinaria configurada para ese grupo (hora de inicio y fin del día ordinario, obtenidos de los parámetros de nómina). Detecta si la fecha corresponde a un día festivo o domingo consultando el calendario de festivos, y por cada tramo generado indica si es horario nocturno, si cruza al día siguiente y si aplica un concepto de liquidación especial (festivo/dominical). Su propósito es alimentar el detalle de horas de turno para que el proceso de liquidación de nómina pueda distinguir horas ordinarias, horas nocturnas y horas en días festivos o dominicales, aplicando el recargo correspondiente a cada concepto salarial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'fnScheduleDetailHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'fnScheduleDetailHour';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Descompone un intervalo horario laboral en tramos según la jornada ordinaria del grupo de nómina, marcando si cruza al día siguiente y si corresponde a domingo/festivo para aplicar el concepto de liquidación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El @IdGroup debe existir en Payroll.Group y tener un PayrollParameterId válido en Payroll.PayrollParameter, de lo contrario los tiempos ordinarios quedan NULL.; Payroll.PayrollParameter debe tener configurados InitialTimeOrdinaryDay y EndTimeOrdinaryDay para el grupo.; Common.Holiday debe contener los festivos del calendario para que la marca de día festivo opere correctamente.; Se asume @InitialDate <= @EndDate y que ambos están en el mismo día o consecutivos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los tramos insertados se devuelven con Event=1 y Approved=0 (pendientes de aprobación).; El indicador AppliedLiquidationConcept refleja si la jornada cae en festivo o domingo (señalando que aplica concepto de liquidación especial).; NextDay sólo se marca en 1 cuando el tramo cruza el inicio de la jornada ordinaria (caso madrugada hacia el día siguiente).; La jornada ordinaria se determina dinámicamente desde PayrollParameter del Group recibido, no es fija.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Jornada ordinaria de nómina; Recargo nocturno; Día festivo; Domingo; Concepto de liquidación; Grupo de nómina; Parámetros de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ScheduleDetailHour: Cuando el intervalo está totalmente dentro de la jornada ordinaria (@InitialDate>=@InitialDateOrdinary AND @EndDate<=@EndDateOrdinary), inserta un tramo único con NextDay=0, Event=1, Approved=0 y AppliedLiquidationConcept=@HolidayDay.; [INSERT] @ScheduleDetailHour: Cuando la jornada se prolonga después del fin ordinario (@EndDate>@EndDateOrdinary) inserta dos tramos: uno hasta EndTimeOrdinaryDay y otro desde EndTimeOrdinaryDay hasta @EndDate, calculando horas con DATEDIFF.; [INSERT] @ScheduleDetailHour: Cuando ambos extremos son posteriores al fin de jornada (@InitialDate>=@EndDateOrdinary AND @EndDate>=@EndDateOrdinary), inserta un único tramo nocturno con TotalHours recibidos.; [INSERT] @ScheduleDetailHour: Cuando ambos extremos son anteriores al inicio de jornada (@InitialDate<@InitialDateOrdinary AND @EndDate<=@InitialDateOrdinary), inserta un único tramo madrugada con TotalHours recibidos.; [INSERT] @ScheduleDetailHour: Cuando el intervalo cruza el inicio de jornada (@InitialDate<@InitialDateOrdinary AND @EndDate>@InitialDateOrdinary), inserta dos tramos: madrugada (NextDay=0) y tramo posterior con NextDay=1 indicando cruce al día siguiente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DAY(@InitialDate) = DAY(@EndDate) (mismo día) y la fecha es festivo o domingo (DATEPART(WEEKDAY)=7) → Marca @HolidayDay = 1 para que el registro insertado tenga AppliedLiquidationConcept=1 else Si las fechas abarcan días distintos, basta con que la fecha inicial o la final coincidan con un festivo en Common.Holiday para marcar @HolidayDay=1; si @InitialDate >= @InitialDateOrdinary AND @EndDate <= @EndDateOrdinary → Inserta un único tramo (jornada normal) con TotalHours recibidos y NextDay=0; si @InitialDate >= @InitialDateOrdinary AND @EndDate > @EndDateOrdinary → Divide la jornada en dos tramos: hasta EndTimeOrdinaryDay (jornada ordinaria) y desde EndTimeOrdinaryDay hasta @EndDate (recargo nocturno tras las 9 pm); si @InitialDate >= @EndDateOrdinary AND @EndDate >= @EndDateOrdinary → Inserta un único tramo posterior al fin de la jornada ordinaria con TotalHours recibidos; si @InitialDate < @InitialDateOrdinary AND @EndDate <= @InitialDateOrdinary → Inserta un único tramo previo al inicio de la jornada ordinaria con TotalHours recibidos; si @InitialDate < @InitialDateOrdinary AND @EndDate > @InitialDateOrdinary → Divide la jornada en dos tramos: antes de InitialDateOrdinary y desde InitialDateOrdinary hasta @EndDate marcando NextDay=1 en el segundo (cruza al día siguiente)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Group; Payroll.PayrollParameter; Common.Holiday', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnScheduleDetailHour';
GO
