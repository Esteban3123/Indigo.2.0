-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-13
-- Description:	Obtener el cuadro de turno habilitado en un rango de fechas con asignaciones
-- =============================================
CREATE FUNCTION [Authorization].[GetAuthorizationAvailabilityTime]
(
	@CurrentDate AS DATE,
	@Days AS INT
)
RETURNS @WorkingHours TABLE
(
	UserCode VARCHAR(20),
	WorkingDate DATE,
	WorkMinutes INT,
	AssignedMinutes INT,
	AvailabilityMinutes INT
)
AS
BEGIN

	/**************************************************** VARIABLES **************************************************/

	DECLARE @ZeroHour TIME = TIMEFROMPARTS(0,0,0,0,0),			
			-----------------------------------------------------------------------------------------------------------
			@UserRows INT = 1,
			@UserCode VARCHAR(20) = '',
			@TimeForRequests INT,
			-----------------------------------------------------------------------------------------------------------
			@WorkingRows INT = 1,
			@WorkingDate DATE,
			@AssignedMinutes INT
	
	/****************************************************  TURNOS ****************************************************/

	INSERT INTO @WorkingHours
		SELECT UserCode, WorkingDate, SUM(DATEDIFF(MINUTE, gas.InitialDateTime, gas.EndingDateTime)) WorkMinutes, 0 AssignedMinutes, 0 AvailabilityMinutes
		FROM
		(
			SELECT	UserCode, WorkingDate, 
					CAST(WorkingDate AS datetime) + CAST(InitialTime AS datetime) InitialDateTime,
					DATEADD(DAY, IIF(EndingTime = @ZeroHour, 1, 0), CAST(WorkingDate AS datetime) + CAST(EndingTime AS datetime)) EndingDateTime
			FROM [Authorization].GetAuthorizationSchedule(@CurrentDate, @Days)	
		) gas
		GROUP BY UserCode, WorkingDate
		ORDER BY gas.UserCode, WorkingDate

	/*********************************************  ASIGNACIÓN DE TIEMPO *********************************************/

	-- Recorro los usuarios
	WHILE @UserRows > 0
	BEGIN
		SELECT TOP 1
			@UserCode = wh.UserCode,
			@TimeForRequests = 0,
			@WorkingRows = 1,
			@WorkingDate = DATEFROMPARTS(1, 1, 1)
		FROM @WorkingHours wh
		WHERE wh.UserCode > @UserCode
		ORDER BY wh.UserCode

		SET @UserRows = @@ROWCOUNT
		IF @UserRows = 0
		BEGIN
			BREAK
		END

		/********************************** *************************************** **********************************/

		-- Tiempo asignado en minutos
		SELECT @TimeForRequests = v.AssignedMinutes
		FROM [Authorization].[ViewAllottedTimeForRequests] v
		WHERE v.AssignUserCode = @UserCode

		/********************************** *************************************** **********************************/

		-- Recorro los dias y empiezo a asignarle el tiempo
		WHILE @WorkingRows > 0
		BEGIN
			SELECT TOP 1
				@WorkingDate = wh.WorkingDate,
				@AssignedMinutes = IIF(@TimeForRequests > wh.WorkMinutes, wh.WorkMinutes, @TimeForRequests)
			FROM @WorkingHours wh
			WHERE wh.UserCode = @UserCode
				AND wh.WorkingDate > @WorkingDate
			ORDER BY wh.WorkingDate

			SET @WorkingRows = @@ROWCOUNT
			IF @WorkingRows = 0 OR @TimeForRequests = 0
			BEGIN
				BREAK
			END

			/******************************** *************************************** ********************************/

			--Se establece la cantidad asignada en el turno
			UPDATE wh
				SET wh.AssignedMinutes = @AssignedMinutes
			FROM @WorkingHours wh
			WHERE wh.UserCode = @UserCode
				AND wh.WorkingDate = @WorkingDate

			--Se actualiza la cantidad pendiente por asignar
			SET @TimeForRequests = @TimeForRequests - @AssignedMinutes
		END
	END

	/**************************************  DETERMINAR TOTAL TIEMPO DISPONIBLE **************************************/

	UPDATE wh
		SET wh.AvailabilityMinutes = wht.AvailabilityMinutes
	FROM @WorkingHours wh
	JOIN
	(
		SELECT wh.UserCode, SUM(wh.WorkMinutes - wh.AssignedMinutes) AvailabilityMinutes
		FROM @WorkingHours wh
		GROUP BY wh.UserCode
	) wht ON wh.UserCode = wht.UserCode

	/*************************************************** RESULTADO ***************************************************/

	RETURN
END