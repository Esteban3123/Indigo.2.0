

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 19/05/2020
-- Description:	Procedimiento que se encarga de guardar, actualizar una asignación de turnos
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveAuthorizationSchedule] 
    @Xml AS xml
AS
BEGIN
	SET NOCOUNT ON

	--Variables para obtener la cabecera del xml
	declare @Id int, @AuthorizationScheduleTemplateId int, @Month int, @Year int, @UserId int, @UserCode varchar(50), @Status bit
	
	--Tabla de dias
	declare @AuthorizationScheduleDetail table(RowId int, Id int, AuthorizationScheduleId int, Schedule tinyint, Day int, NumberHour numeric(5,2), Status bit)

	--Tabla de horas
	declare @AuthorizationScheduleDetailHour table(RowId int, Id int, AuthorizationScheduleDetailId int, NextDay bit, InitialTime time(0), EndingTime time(0), NumberHour numeric(5,2),
	Type tinyint, NoveltyType tinyint, Status bit)

	--Tabla en donde se almacena si ya se ha registrado un turno normal
	declare @TurnError table(MessageReturn varchar(100))

	begin try	

		--Se obtienen los datos de la cabecera del xml
		select 
			@Id = t.x.value('Id[1]','int'),
			@AuthorizationScheduleTemplateId = t.x.value('AuthorizationScheduleTemplateId[1]','int'),
			@Month = t.x.value('Month[1]','int'),			
			@Year = t.x.value('Year[1]','int'),		
			@UserId = t.x.value('UserId[1]','int'),		
			@UserCode = t.x.value('UserCode[1]','varchar(50)'),
			@Status = t.x.value('Status[1]','bit')
		from @Xml.nodes('/AuthorizationSchedule') t(x)

		--Se obtienen los datos de los días
		insert into @AuthorizationScheduleDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationScheduleId[1]','int') as AuthorizationScheduleId,
			t.x.value('Schedule[1]','tinyint') as Schedule,
			t.x.value('Day[1]','int') as Day,
			REPLACE(t.x.value('NumberHour[1]','varchar(20)'), ',', '.') as NumberHour,	
			t.x.value('Status[1]','bit') as Status
		from @Xml.nodes('/AuthorizationSchedule/AuthorizationScheduleDetail') t(x)

		--Se obtienen los datos de las horas por día
		insert into @AuthorizationScheduleDetailHour
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationScheduleDetailId[1]','int') as AuthorizationScheduleDetailId,
			t.x.value('NextDay[1]','bit') as NextDay,
			t.x.value('InitialTime[1]','varchar(20)') as InitialTime,
			t.x.value('EndingTime[1]','varchar(20)') as EndingTime,
			REPLACE(t.x.value('NumberHour[1]','varchar(20)'), ',', '.') as NumberHour,	
			t.x.value('Type[1]','tinyint') as Type,
			IIF(t.x.value('NoveltyType[1]','varchar(20)') = '', null, t.x.value('NoveltyType[1]','varchar(20)')) as NoveltyType,
			t.x.value('Status[1]','bit') as Status
		from @Xml.nodes('/AuthorizationSchedule/AuthorizationScheduleDetail/AuthorizationScheduleDetailHour') t(x)
		
		--Se inserta la cabecera
		if @Id is null or @Id = 0
		begin
			INSERT INTO [Authorization].[AuthorizationSchedule]([AuthorizationScheduleTemplateId], [Month], [Year], [UserId], [UserCode], [Status])
			VALUES(@AuthorizationScheduleTemplateId, @Month, @Year, @UserId, @UserCode, @Status)

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			UPDATE [Authorization].[AuthorizationSchedule] SET AuthorizationScheduleTemplateId = @AuthorizationScheduleTemplateId, Month = @Month, Year = @Year, UserId = @UserId,
			UserCode = @UserCode, Status = @Status
			WHERE Id = @Id
		end

		declare @Rows int = 1, @RowId int = 0,
		@DetailId int, @AuthorizationScheduleId int, @Schedule tinyint, @Day int, @NumberHour numeric(5,2), @DetailStatus bit

		while @Rows > 0
		begin
			select top 1 @RowId = RowId, @DetailId = Id, @AuthorizationScheduleId = AuthorizationScheduleId, @Schedule = Schedule, @Day = Day, @NumberHour = NumberHour, @DetailStatus = Status
			from @AuthorizationScheduleDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId is null or @DetailId = 0 --Se inserta el detalle
			begin
				INSERT INTO [Authorization].[AuthorizationScheduleDetail]([AuthorizationScheduleId], [Schedule], 
				[Day], [NumberHour], [Status]) 
				VALUES(@Id, @Schedule, @Day, @NumberHour, @DetailStatus)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				update sd set sd.AuthorizationScheduleId = @AuthorizationScheduleId, sd.Schedule = @Schedule, sd.Day = @Day, sd.NumberHour = @NumberHour, sd.Status = @Status
				from [Authorization].AuthorizationScheduleDetail sd
				where sd.Id = @DetailId
			end

			--Se valida que no se vuelva a registrar un turno normal en el día que se esta recorriendo
			if exists(select 1 from @AuthorizationScheduleDetailHour where RowId = @RowId and Id = 0 and [Type] = 1) and
			exists(select 1 from [Authorization].AuthorizationScheduleDetailHour where AuthorizationScheduleDetailId = @DetailId and [Type] = 1)
			begin
				insert into @TurnError(MessageReturn) values('El día ' + CAST(@Day as varchar(2)) + ' ya tiene un turno asignado')
				continue
			end

			--Se insertan las horas de los días
			INSERT INTO [Authorization].[AuthorizationScheduleDetailHour]([AuthorizationScheduleDetailId], [NextDay], [InitialTime], [EndingTime], [NumberHour], [Status], Type, NoveltyType)
			select @DetailId, sdh.NextDay, sdh.InitialTime, sdh.EndingTime, sdh.NumberHour, sdh.Status, sdh.Type, sdh.NoveltyType
			from @AuthorizationScheduleDetailHour sdh
			where sdh.Id = 0 and sdh.RowId = @RowId

			--Se actualizan las horas de los días
			update sdh set sdh.AuthorizationScheduleDetailId = t.AuthorizationScheduleDetailId, sdh.NextDay = t.NextDay, sdh.InitialTime = t.InitialTime, sdh.EndingTime = t.EndingTime, 
			sdh.NumberHour = t.NumberHour, sdh.Status = t.Status, sdh.Type = t.Type, sdh.NoveltyType = t.NoveltyType
			from @AuthorizationScheduleDetailHour t
			inner join [Authorization].AuthorizationScheduleDetailHour sdh on sdh.Id = t.Id
			where t.Id > 0 and t.RowId = @RowId

			--Se actualiza el schedule en el detalle siempre y cuando no haya un horario normal es decir que solo tenga un evento o una novedad
			if not exists(select 1 from [Authorization].AuthorizationScheduleDetailHour where AuthorizationScheduleDetailId = @DetailId and [Type] = 1)
			begin
				--Si no existe un horario normal se obtiene el primero que encuentre para actualizar el detalle del día(Campo Schedule) con evento o novedad
				declare @Type tinyint = (select top 1 [Type] from [Authorization].AuthorizationScheduleDetailHour where AuthorizationScheduleDetailId = @DetailId and [Type] <> 1)
				
				--Actualizamos el detalle
				update [Authorization].AuthorizationScheduleDetail set Schedule = IIF(@Type = 2, 7, 8) where Id = @DetailId
			end
						
			--Se obtiene la sumatoria de horas de turnos normales y eventos
			declare @SumTurnAndEvent decimal(5, 2) = (select SUM(NumberHour) from [Authorization].AuthorizationScheduleDetailHour where AuthorizationScheduleDetailId = @DetailId and ([Type] = 1 or [Type] = 2))

			--Se obtiene la sumatoria de horas de novedades
			declare @SumNovelty decimal(5, 2) = (select SUM(NumberHour) from [Authorization].AuthorizationScheduleDetailHour where AuthorizationScheduleDetailId = @DetailId and [Type] = 3)

			--Se actualiza la sumatoria de horas en el día
			update [Authorization].AuthorizationScheduleDetail set NumberHour = ISNULL(@SumTurnAndEvent, 0) - ISNULL(@SumNovelty, 0) where Id = @DetailId
		end	

		if exists(select 1 from @TurnError)
		begin
			declare @Message varchar(max) = ''
			select @Message = STUFF((
						SELECT CHAR(13) + CHAR(10) + MessageReturn
						FROM @TurnError
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			select 999 AS CodeResult, @Message AS MessageResult
			return
		end

		select 0 AS CodeResult, 'Se guardó correctamente' AS MessageResult
		return
	end try
	begin catch
		select 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
		return
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza el cronograma mensual de turnos para la autorización de servicios médicos. Recibe un XML jerárquico con tres niveles: la cabecera del cronograma (mes, año, plantilla y usuario responsable), el detalle de días con sus bloques horarios, y las franjas de hora específicas (hora inicio, hora fin, tipo de turno y novedad) de cada día. Inserta o actualiza según corresponda en las tablas AuthorizationSchedule, AuthorizationScheduleDetail y AuthorizationScheduleDetailHour, validando que no se registre un turno normal duplicado para el mismo día dentro del período seleccionado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAuthorizationSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAuthorizationSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta o actualiza) un cronograma mensual de asignación de turnos, junto con sus días y horas, validando que no se duplique un turno normal por día y recalculando totales de horas y tipo de jornada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener la estructura /AuthorizationSchedule con nodos hijos AuthorizationScheduleDetail y AuthorizationScheduleDetailHour; Cada hora del XML debe traer un Type (1=turno normal, 2=evento, 3=novedad) coherente con la lógica de cálculo; Los detalles y horas a actualizar deben referenciar Ids existentes (Id>0) en las tablas Authorization.AuthorizationScheduleDetail / AuthorizationScheduleDetailHour; El RowId enlaza un detalle del XML con sus horas correspondientes', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'NumberHour del detalle se recalcula como SUM(horas Type=1 o 2) - SUM(horas Type=3), tratando NULL como 0; Schedule del detalle se fija en 7 cuando el día solo tiene eventos (Type=2) y en 8 cuando solo tiene novedades (Type=3); no se altera si existe al menos un turno normal (Type=1); No se permite registrar más de un turno normal (Type=1) por día/detalle: si ya existe en BD, no se insertan las horas nuevas y se reporta error; Los valores numéricos NumberHour provenientes del XML reemplazan '','' por ''.'' antes de convertirse; NoveltyType vacío en el XML se persiste como NULL; Cualquier excepción se captura y retorna como CodeResult=999 con el mensaje de error y línea', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Asignación de turnos; Cronograma de autorización; Turno normal; Evento; Novedad; Jornada/horario', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Authorization.AuthorizationSchedule: Cuando @Id IS NULL o = 0, inserta nueva cabecera del cronograma y usa SCOPE_IDENTITY como Id para los detalles posteriores; [UPDATE] Authorization.AuthorizationSchedule: Cuando @Id > 0, actualiza la cabecera existente (template, mes, año, usuario, código y estado) WHERE Id = @Id; [INSERT] Authorization.AuthorizationScheduleDetail: Por cada fila del XML con Id nulo o 0, inserta un detalle de día asociado al Id de cabecera; [UPDATE] Authorization.AuthorizationScheduleDetail: Por cada fila del XML con Id > 0, actualiza el detalle del día (Schedule, Day, NumberHour, Status) WHERE Id = @DetailId; [UPDATE] Authorization.AuthorizationScheduleDetail: Si el detalle no tiene horas con Type=1, actualiza Schedule = 7 cuando el primer Type hallado es 2 (evento), de lo contrario 8 (novedad); [UPDATE] Authorization.AuthorizationScheduleDetail: Recalcula NumberHour del detalle como ISNULL(SUM horas Type IN(1,2),0) - ISNULL(SUM horas Type=3,0) WHERE Id = @DetailId; [INSERT] Authorization.AuthorizationScheduleDetailHour: Inserta las horas del XML con Id=0 para el RowId actual, ligadas al @DetailId, salvo cuando se detectó duplicidad de turno normal (Type=1) para ese día; [UPDATE] Authorization.AuthorizationScheduleDetailHour: Para horas del XML con Id>0, actualiza tiempos, duración, tipo, novedad y estado JOIN por Id; [RETURN_RESULT] @TurnError: Si existen mensajes en @TurnError, retorna CodeResult=999 y MessageResult con los mensajes concatenados por CRLF; en caso normal retorna CodeResult=0 y ''Se guardó correctamente''; ante excepción retorna 999 con ERROR_MESSAGE() + línea', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Id IS NULL OR @Id = 0 (cabecera nueva) → INSERT en AuthorizationSchedule y se toma SCOPE_IDENTITY como Id else UPDATE de la cabecera existente por Id; si Detalle con Id IS NULL OR Id = 0 → INSERT en AuthorizationScheduleDetail y se obtiene DetailId vía SCOPE_IDENTITY else UPDATE del detalle existente por Id; si Existe en el XML una hora con Type=1 (turno normal) para el día Y ya existe en BD otro registro con Type=1 para ese mismo AuthorizationScheduleDetailId → Se agrega mensaje de error ''El día X ya tiene un turno asignado'' a @TurnError y se omite la inserción/actualización de horas para esa fila (CONTINUE) else Se procede a insertar/actualizar las horas del día; si No existe ninguna hora con Type=1 (turno normal) para el detalle → Se actualiza Schedule del detalle: 7 si el primer Type encontrado es 2 (evento), 8 en caso contrario (novedad) else No se modifica el campo Schedule del detalle; si Al finalizar, EXISTS registros en @TurnError → Retorna CodeResult=999 con la concatenación de mensajes separados por CRLF else Retorna CodeResult=0 con mensaje ''Se guardó correctamente''', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationSchedule; Authorization.AuthorizationScheduleDetail; Authorization.AuthorizationScheduleDetailHour', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationSchedule';
-- GO
