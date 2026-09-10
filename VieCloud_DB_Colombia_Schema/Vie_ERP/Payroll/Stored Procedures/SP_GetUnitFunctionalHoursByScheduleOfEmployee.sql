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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el total de horas trabajadas por un empleado en cada unidad funcional durante un rango de días de un período de nómina, basándose en el cuadro de turnos (horario). Recorre día a día el rango indicado, consulta el turno asignado al empleado en el cuadro de turnos (Payroll.Schedule y sus detalles) y acumula las horas por unidad funcional. Devuelve el resultado como XML con el total de horas por unidad funcional, para ser usado en el proceso de liquidación de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, en un XML de salida, el total de horas programadas por unidad funcional para un empleado dentro de un rango de días de un período, recorriendo las columnas día (D01..D31) del cuadro de turno.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El período debe corresponder al formato esperado VARCHAR(7) (p.ej. ''YYYY-MM'') usado en Payroll.Schedule.Period.; El rango @DayInitial..@DayEnd debe estar entre 1 y 31 y existir como columnas D01..D31 en Payroll.Schedule.; Debe existir un cuadro de turno (Schedule) para el empleado y período; de lo contrario no se acumulan horas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las horas se agregan (SUM) por FunctionalUnitId tanto a nivel diario como en el resultado final consolidado del rango.; El nombre de la columna día en Payroll.Schedule sigue el patrón fijo ''D'' + número de día con dos dígitos (D01..D31).; Solo se consideran registros del empleado y período indicados (s.EmployeeId = @EmployeeId AND s.Period = @Period).; El resultado se devuelve como XML con elementos (FOR XML AUTO, TYPE, ELEMENTS) agrupado por unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; cuadro de turno; unidad funcional; horas programadas; período de nómina; detalle de turno', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve por parámetro OUTPUT @UnitFunctionalHours un XML (FOR XML AUTO, TYPE, ELEMENTS) con FunctionalUnitId y la suma de Hours agrupada por unidad funcional sobre el rango de días procesado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si WHILE @DayInitial <= @DayEnd → Itera día a día construyendo dinámicamente la columna D01..D31 (sufijo de dos dígitos vía RIGHT(CONCAT(''00'',day),2)) para unir Schedule con ScheduleDetail por el día correspondiente y acumular horas por unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Schedule; Payroll.ScheduleDetail; Payroll.ScheduleDetailHour', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetUnitFunctionalHoursByScheduleOfEmployee';
-- GO
