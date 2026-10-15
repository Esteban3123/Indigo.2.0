

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