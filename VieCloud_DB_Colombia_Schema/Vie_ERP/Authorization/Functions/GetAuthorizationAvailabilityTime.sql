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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula la disponibilidad de tiempo de los autorizadores (auditores de autorizaciones) en un rango de fechas determinado por una fecha inicial y una cantidad de días. Para cada usuario autorizador y cada día laborable, obtiene los minutos totales de turno consultando el horario de trabajo registrado en GetAuthorizationSchedule, luego descuenta los minutos ya comprometidos con solicitudes de autorización según lo registrado en ViewAllottedTimeForRequests, y finalmente calcula los minutos disponibles reales por usuario. Se usa para saber qué capacidad libre tiene cada autorizador para recibir nuevas solicitudes de autorización de servicios médicos, apoyando la asignación y gestión de carga de trabajo en el proceso de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetAuthorizationAvailabilityTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetAuthorizationAvailabilityTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula, por usuario y día dentro de un rango, los minutos trabajados, los minutos ya asignados a solicitudes pendientes y el tiempo total disponible restante para autorizaciones.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el cuadro de turnos retornado por Authorization.GetAuthorizationSchedule para el rango (@CurrentDate, @Days); Los usuarios con tiempo pendiente deben tener registro en Authorization.ViewAllottedTimeForRequests; El parámetro de días define la ventana hacia adelante a considerar', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los minutos asignados a un día nunca exceden los minutos trabajados de ese día (AssignedMinutes ≤ WorkMinutes); AvailabilityMinutes por usuario equivale a la suma de (WorkMinutes - AssignedMinutes) de todos sus días en el rango; El AvailabilityMinutes se replica igual en todas las filas del mismo usuario (es total por usuario, no por día); El tiempo a asignar se distribuye en orden cronológico ascendente, llenando primero los días más tempranos; Si un turno termina a las 00:00, se interpreta que finaliza al día siguiente', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuadro de turnos; Autorización; Disponibilidad de tiempo; Asignación de solicitudes; Jornada laboral', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @WorkingHours: Inserta una fila por (UserCode, WorkingDate) con WorkMinutes = suma de DATEDIFF(MIN, InitialDateTime, EndingDateTime) sobre los turnos del día; [UPDATE] @WorkingHours: Distribuye cronológicamente el AssignedMinutes del usuario (tomado de ViewAllottedTimeForRequests.AssignedMinutes) entre sus días, asignando hasta WorkMinutes por día; [UPDATE] @WorkingHours: Setea AvailabilityMinutes = SUM(WorkMinutes - AssignedMinutes) por UserCode, replicado en todas sus filas', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EndingTime = ''00:00:00'' (medianoche) → Se suma 1 día a EndingDateTime para representar fin de turno al día siguiente else Se mantiene EndingDateTime en el mismo día; si @TimeForRequests > wh.WorkMinutes → Se asigna al día solo el total de WorkMinutes disponibles del turno else Se asigna el remanente @TimeForRequests al día; si @WorkingRows = 0 OR @TimeForRequests = 0 → Se interrumpe la distribución de minutos para el usuario actual else Continúa asignando minutos en el siguiente día disponible', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.GetAuthorizationSchedule', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.ViewAllottedTimeForRequests', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationAvailabilityTime';
GO
