-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-12
-- Description:	Obtener el cuadro de turno habilitado en un rango de fechas
-- =============================================
CREATE FUNCTION [Authorization].[GetAuthorizationSchedule]
(
	@CurrentDate AS DATE,
	@Days AS INT 
)
RETURNS @WorkingHours TABLE
(
	UserId INT,
	UserCode VARCHAR(20),
	WorkingDate DATE,
	InitialTime TIME,
	EndingTime TIME
)
AS
BEGIN
	
	/**************************************************** VARIABLES **************************************************/

	DECLARE @ZeroHour TIME = TIMEFROMPARTS(0,0,0,0,0),
			-----------------------------------------------------------------------------------------------------------
			@NoveltyRows INT = 1,
			@NoveltyId INT = 0

	DECLARE @NoveltyHours AS TABLE
	(
		Id INT IDENTITY(1,1),
		UserId INT,
		WorkingDate DATE,
		InitialTime TIME,
		EndingTime TIME
	)

	/****************************************************  TURNOS ****************************************************/

	-- Turnos Normales
	INSERT INTO @WorkingHours
		SELECT	aus.UserId,	aus.UserCode, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) WorkingDate, ausdh.InitialTime, IIF(ausdh.NextDay = 1, TIMEFROMPARTS(0, 0, 0, 0, 0), ausdh.EndingTime) EndingTime
		FROM [Authorization].AuthorizationSchedule aus
		JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
		JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
		WHERE ausdh.Type = 1 AND DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)

	-- Turnos Dia Siguiente que afectan un turno normal creado
	UPDATE wh
		SET wh.InitialTime = IIF((nd.InitialTime < wh.InitialTime AND wh.InitialTime <= nd.EndingTime), nd.InitialTime, wh.InitialTime),
			wh.EndingTime = IIF((nd.InitialTime <= wh.EndingTime AND ((wh.EndingTime <> @ZeroHour AND wh.EndingTime < nd.EndingTime) OR nd.EndingTime = @ZeroHour)), nd.EndingTime, wh.EndingTime)
	FROM @WorkingHours wh
	JOIN
	(
		SELECT	aus.UserId,	aus.UserCode, DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) WorkingDate, TIMEFROMPARTS(0, 0, 0, 0, 0) InitialTime, ausdh.EndingTime
		FROM [Authorization].AuthorizationSchedule aus
		JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
		JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
		WHERE ausdh.Type = 1 AND ausdh.NextDay = 1 AND DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
	) nd ON wh.UserId = nd.UserId AND wh.WorkingDate = nd.WorkingDate
		AND
		(
			(nd.InitialTime < wh.InitialTime AND wh.InitialTime <= nd.EndingTime)
			OR
			(nd.InitialTime <= wh.EndingTime AND ((wh.EndingTime <> @ZeroHour AND wh.EndingTime < nd.EndingTime) OR nd.EndingTime = @ZeroHour))
		)

	-- Turnos Dia Siguiente que no afectan un turno normal creado
	INSERT INTO @WorkingHours
		SELECT	nd.UserId, nd.UserCode, nd.WorkingDate, nd.InitialTime, nd.EndingTime
		FROM
		(
			SELECT	aus.UserId,	aus.UserCode, DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) WorkingDate, TIMEFROMPARTS(0, 0, 0, 0, 0) InitialTime, ausdh.EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 1 AND ausdh.NextDay = 1 AND DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
		) nd
		LEFT JOIN @WorkingHours wh ON wh.UserId = nd.UserId AND wh.WorkingDate = nd.WorkingDate
		AND
		(
			(wh.InitialTime <= nd.InitialTime AND ((nd.EndingTime <> @ZeroHour AND nd.EndingTime <= wh.EndingTime) OR wh.EndingTime = @ZeroHour))
		)
		WHERE wh.UserId IS NULL

	/**************************************************** EVENTOS ****************************************************/

	-- Eventos que afectan un turno normal creado
	UPDATE wh
		SET wh.InitialTime = IIF((eh.InitialTime < wh.InitialTime AND wh.InitialTime <= eh.EndingTime), eh.InitialTime, wh.InitialTime),
			wh.EndingTime = IIF((eh.InitialTime <= wh.EndingTime AND ((wh.EndingTime <> @ZeroHour AND wh.EndingTime < eh.EndingTime) OR eh.EndingTime = @ZeroHour)), eh.EndingTime, wh.EndingTime)
	FROM @WorkingHours wh
	JOIN
	(
		SELECT	aus.UserId,	aus.UserCode, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) WorkingDate, ausdh.InitialTime, IIF(ausdh.NextDay = 1, TIMEFROMPARTS(0, 0, 0, 0, 0), ausdh.EndingTime) EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 2 AND DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
		UNION ALL
			SELECT	aus.UserId,	aus.UserCode, DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) WorkingDate, TIMEFROMPARTS(0, 0, 0, 0, 0) InitialTime, ausdh.EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 2 AND ausdh.NextDay = 1 AND DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
	) eh ON wh.UserId = eh.UserId AND wh.WorkingDate = eh.WorkingDate
		AND
		(
			(eh.InitialTime < wh.InitialTime AND wh.InitialTime <= eh.EndingTime)
			OR
			(eh.InitialTime <= wh.EndingTime AND ((wh.EndingTime <> @ZeroHour AND wh.EndingTime < eh.EndingTime) OR eh.EndingTime = @ZeroHour))
		)

	-- Eventos que no afectan un turno normal creado
	INSERT INTO @WorkingHours
		SELECT	nd.UserId, nd.UserCode, nd.WorkingDate, nd.InitialTime, nd.EndingTime
		FROM
		(
			SELECT	aus.UserId,	aus.UserCode, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) WorkingDate, ausdh.InitialTime, IIF(ausdh.NextDay = 1, TIMEFROMPARTS(0, 0, 0, 0, 0), ausdh.EndingTime) EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 2 AND DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
		UNION ALL
			SELECT	aus.UserId,	aus.UserCode, DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) WorkingDate, TIMEFROMPARTS(0, 0, 0, 0, 0) InitialTime, ausdh.EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 2 AND ausdh.NextDay = 1 AND DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
		) nd
		LEFT JOIN @WorkingHours wh ON wh.UserId = nd.UserId AND wh.WorkingDate = nd.WorkingDate
		AND
		(
			(wh.InitialTime <= nd.InitialTime AND ((nd.EndingTime <> @ZeroHour AND nd.EndingTime <= wh.EndingTime) OR wh.EndingTime = @ZeroHour))
		)
		WHERE wh.UserId IS NULL

	/*************************************************** NOVEDADES ***************************************************/

	-- Inserto las novedades registradas en el rango
	INSERT INTO @NoveltyHours
		SELECT nh.UserId, nh.WorkingDate, nh.InitialTime, nh.EndingTime
		FROM
		(
			SELECT	aus.UserId,	aus.UserCode, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) WorkingDate, ausdh.InitialTime, IIF(ausdh.NextDay = 1, TIMEFROMPARTS(0, 0, 0, 0, 0), ausdh.EndingTime) EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 3 AND DATEFROMPARTS(aus.Year, aus.Month, ausd.Day) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
		UNION ALL
			SELECT	aus.UserId,	aus.UserCode, DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) WorkingDate, TIMEFROMPARTS(0, 0, 0, 0, 0) InitialTime, ausdh.EndingTime
			FROM [Authorization].AuthorizationSchedule aus
			JOIN [Authorization].AuthorizationScheduleDetail ausd ON aus.Id = ausd.AuthorizationScheduleId
			JOIN [Authorization].AuthorizationScheduleDetailHour ausdh ON ausd.Id = ausdh.AuthorizationScheduleDetailId
			WHERE ausdh.Type = 3 AND ausdh.NextDay = 1 AND DATEADD(DAY, 1, DATEFROMPARTS(aus.Year, aus.Month, ausd.Day)) BETWEEN @CurrentDate AND DATEADD(DAY, @Days, @CurrentDate)
		) nh
		ORDER BY nh.WorkingDate, nh.InitialTime

	-- Actualizo los turnos que tengan una novedad en sus extremos
	UPDATE wh
		SET wh.InitialTime = IIF((nh.InitialTime <= wh.InitialTime), nh.EndingTime, wh.InitialTime),
			wh.EndingTime = IIF(((wh.EndingTime <> @ZeroHour AND nh.EndingTime >= wh.EndingTime) OR nh.EndingTime = @ZeroHour), nh.InitialTime, wh.EndingTime)
	FROM @NoveltyHours nh
	JOIN @WorkingHours wh ON nh.UserId = wh.UserId AND nh.WorkingDate = wh.WorkingDate
	WHERE 
	(
		(nh.InitialTime <= wh.InitialTime)
		OR
		((wh.EndingTime <> @ZeroHour AND nh.EndingTime >= wh.EndingTime) OR nh.EndingTime = @ZeroHour)
	)

	-- Recorro las novedades contenidas en un turno
	WHILE @NoveltyRows > 0
	BEGIN
		SELECT TOP 1
			@NoveltyId = nh.Id
		FROM @NoveltyHours nh
		JOIN @WorkingHours wh ON nh.UserId = wh.UserId AND nh.WorkingDate = wh.WorkingDate
		WHERE nh.Id > @NoveltyId AND
		(
			(wh.InitialTime < nh.InitialTime AND ((nh.EndingTime <> @ZeroHour AND wh.EndingTime > nh.EndingTime) OR wh.EndingTime = @ZeroHour))
		)
		ORDER BY nh.Id

		SET @NoveltyRows = @@ROWCOUNT
		IF @NoveltyRows = 0
		BEGIN
			BREAK
		END

		/********************************** *************************************** **********************************/
		
		INSERT INTO @WorkingHours
			SELECT TOP 1
				wh.UserId, wh.UserCode, wh.WorkingDate, nh.EndingTime, wh.EndingTime
			FROM @NoveltyHours nh
			JOIN @WorkingHours wh ON nh.UserId = wh.UserId AND nh.WorkingDate = wh.WorkingDate
			WHERE nh.Id = @NoveltyId AND
			(
				(wh.InitialTime < nh.InitialTime AND ((nh.EndingTime <> @ZeroHour AND wh.EndingTime > nh.EndingTime) OR wh.EndingTime = @ZeroHour))
			)

		UPDATE wh
			SET wh.EndingTime = nh.InitialTime
		FROM @NoveltyHours nh
		JOIN @WorkingHours wh ON nh.UserId = wh.UserId AND nh.WorkingDate = wh.WorkingDate
		WHERE nh.Id = @NoveltyId AND
		(
			(wh.InitialTime < nh.InitialTime AND ((nh.EndingTime <> @ZeroHour AND wh.EndingTime > nh.EndingTime) OR wh.EndingTime = @ZeroHour))
		)
	END

	-- Elimino los turnos que esten inmersos en una novedad
	DELETE wh
	FROM @NoveltyHours nh
	JOIN @WorkingHours wh ON nh.UserId = wh.UserId AND nh.WorkingDate = wh.WorkingDate
	WHERE 
	(
		(nh.InitialTime <= wh.InitialTime AND ((wh.EndingTime <> @ZeroHour AND nh.EndingTime >= wh.EndingTime) OR nh.EndingTime = @ZeroHour))
	)

	/*************************************************** RESULTADO ***************************************************/

	RETURN
END