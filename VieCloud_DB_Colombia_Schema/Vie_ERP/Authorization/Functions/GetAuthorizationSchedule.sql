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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y devuelve los horarios de trabajo efectivos de los usuarios autorizadores dentro de un rango de fechas, a partir de una fecha de inicio (@CurrentDate) y una cantidad de días (@Days). Compone la información del cronograma de autorizaciones (mes, año, usuario) con el detalle de días y las franjas horarias (hora inicio, hora fin, tipo de turno), resolviendo correctamente los turnos que cruzan la medianoche al día siguiente. También aplica eventos especiales y novedades (permisos, incapacidades) que pueden ampliar, reducir o eliminar franjas de trabajo. Se utiliza para determinar en qué momento un auditor o autorizador médico está disponible para gestionar autorizaciones de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetAuthorizationSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetAuthorizationSchedule';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye, para un rango de fechas, el cuadro efectivo de turnos por usuario combinando turnos normales, eventos y novedades, ajustando solapamientos y partiendo turnos cuando una novedad queda contenida.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El cronograma de autorización debe estar registrado en AuthorizationSchedule con su detalle de días (AuthorizationScheduleDetail) y horas (AuthorizationScheduleDetailHour).; Las horas se clasifican por Type: 1 = turno normal, 2 = evento, 3 = novedad.; El flag NextDay=1 indica que la franja se extiende al día siguiente y se modela con InitialTime=00:00 ese día.; El rango efectivo procesado es [@CurrentDate, @CurrentDate + @Days].', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los turnos resultantes nunca solapan con una novedad: las novedades siempre prevalecen recortando, partiendo o eliminando el turno.; El valor 00:00 (@ZeroHour) se utiliza convencionalmente como marcador de medianoche/fin de día, no como inicio cero, en todas las comparaciones de fin de franja.; Las franjas que cruzan medianoche (NextDay=1) se materializan como dos registros: uno terminando a 00:00 en el día original y otro empezando a 00:00 en el día siguiente.; Los eventos y turnos normales se fusionan extendiendo extremos cuando solapan; solo se insertan como nuevos cuando no quedan contenidos por un turno existente.; Solo se procesan registros cuya fecha derivada (Year/Month/Day) esté dentro del rango [@CurrentDate, @CurrentDate + @Days].', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuadro de turnos; Autorización; Turno normal; Evento; Novedad; Jornada con cruce de medianoche (NextDay); Cronograma mensual de autorización', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @WorkingHours: Carga turnos normales (Type=1) cuyas fechas (Year/Month/Day) caen dentro del rango; si NextDay=1, EndingTime se trunca a 00:00.; [UPDATE] @WorkingHours: Para turnos normales con NextDay=1, extiende el InitialTime/EndingTime de un turno existente del día siguiente cuando los rangos solapan.; [INSERT] @WorkingHours: Inserta el remanente del turno NextDay=1 en el día siguiente cuando no existe un turno que ya lo contenga.; [UPDATE] @WorkingHours: Para eventos (Type=2, incluyendo continuación NextDay), expande Initial/EndingTime del turno existente cuando solapa con el evento.; [INSERT] @WorkingHours: Inserta eventos (Type=2) que no quedan contenidos por ningún turno existente del mismo usuario y fecha.; [INSERT] @NoveltyHours: Carga novedades (Type=3, incluyendo continuación NextDay) que caen en el rango, ordenadas por fecha y hora.; [UPDATE] @WorkingHours: Recorta los extremos del turno cuando una novedad cubre su inicio (InitialTime <= wh.InitialTime) o su fin (nh.EndingTime >= wh.EndingTime o EndingTime=00:00).; [INSERT] @WorkingHours: Cuando una novedad queda estrictamente contenida dentro de un turno, inserta un nuevo segmento desde nh.EndingTime hasta wh.EndingTime (parte el turno en dos).; [UPDATE] @WorkingHours: En el partido por novedad contenida, ajusta el segmento original poniendo wh.EndingTime = nh.InitialTime.; [DELETE] @WorkingHours: Elimina turnos cuyo rango quede totalmente cubierto por una novedad (nh.InitialTime <= wh.InitialTime AND nh.EndingTime >= wh.EndingTime, considerando 00:00 como fin de día).; [RETURN_RESULT] @WorkingHours: Retorna la tabla resultante con UserId, UserCode, WorkingDate, InitialTime y EndingTime efectivos tras aplicar turnos, eventos y novedades.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ausdh.Type = 1 → Trata el registro como turno normal y lo inserta o expande en @WorkingHours.; si ausdh.Type = 2 → Trata el registro como evento y lo fusiona con turnos existentes o lo inserta como nuevo bloque.; si ausdh.Type = 3 → Trata el registro como novedad: recorta extremos, parte turnos contenedores o elimina turnos totalmente cubiertos.; si ausdh.NextDay = 1 → Genera una franja adicional al día siguiente con InitialTime=00:00 y EndingTime original; en el día actual la franja termina en 00:00. else Conserva InitialTime y EndingTime tal como están definidos.; si EndingTime = 00:00 (@ZeroHour) → Se interpreta como ''fin de día/medianoche'' y se considera mayor que cualquier otra hora en las comparaciones de solapamiento.; si Existe novedad estrictamente contenida en un turno (wh.InitialTime < nh.InitialTime AND wh.EndingTime > nh.EndingTime) → Se entra al WHILE que parte el turno: inserta segmento posterior y acorta el original. else Se sale del bucle (BREAK) cuando @@ROWCOUNT = 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationSchedule; Authorization.AuthorizationScheduleDetail; Authorization.AuthorizationScheduleDetailHour', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetAuthorizationSchedule';
GO
