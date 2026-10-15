-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-08-17
-- Description:	Procedimiento que se encarga de obtener el número de horas para cada unidad funcional donde trabajo el empleado según el cuadro de turno
-- =============================================

CREATE PROCEDURE [Payroll].[SP_GetUnitFunctionalHoursByScheduleOfEmployee]
	@EmployeeId INT,
	@Period VARCHAR(7),
	@DayInitial INT,
	@DayEnd INT,
	@UnitFunctionalHours XML OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	DECLARE @DaysString VARCHAR(2),
			@Query nvarchar(MAX)

	DECLARE @TableDays AS TABLE(
		Day INT,
		FunctionalUnitId INT,
		Hours INT
	)

	/************************************* CÁLCULO ************************************/

	WHILE @DayInitial <= @DayEnd
	BEGIN
		SELECT @DaysString = RIGHT(CONCAT('00', @DayInitial), 2)

		SET @Query = CONCAT(N'SELECT ', @DayInitial, ' Day, s.FunctionalUnitId, SUM(sdh.TotalNumberHours) Hour
							FROM Payroll.Schedule s
							JOIN Payroll.ScheduleDetail sd ON s.D', @DaysString, ' = sd.Id
							JOIN Payroll.ScheduleDetailHour sdh ON sd.Id = sdh.ScheduleDetailId
							WHERE s.EmployeeId = @EmployeeId AND s.Period = @Period
							GROUP BY s.FunctionalUnitId')

		INSERT INTO @TableDays
			execute sp_executesql @Query, N'@EmployeeId INT, @Period varchar(7)', @EmployeeId, @Period  

		SET @DayInitial = @DayInitial + 1
	END

	/************************************* RESULTADO ************************************/

	SELECT @UnitFunctionalHours = CONVERT(xml, 
		(
			SELECT FunctionalUnitId, SUM(Hours) Hours
			FROM @TableDays UnitFunctionalHours
			GROUP BY FunctionalUnitId
			For xml AUTO,TYPE, ELEMENTS
		)
	)
END