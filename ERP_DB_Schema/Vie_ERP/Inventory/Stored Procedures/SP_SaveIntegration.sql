

-- ==============================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/05/2018
-- Description:	Procedimiento que se encarga de guardar en las tablas de integración con HEON
-- ==============================================================================================
CREATE PROCEDURE [Inventory].[SP_SaveIntegration] 
	@Xml as xml,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Se declaran las variables para obtener los datos de la cabecera
	declare @LogisticOperator int, @OfficeType int, @CareCenterCode varchar(20), @JsonSolicitud varchar(max), @JsonEntrega varchar(max), @JsonRespuestaHEON varchar(max),
	@EntityId int, @EntityCode varchar(20), @EntityName varchar(250), @Status tinyint

	--Tabla temporal para los detalles de la integración
	declare @TempControlIntegrationHeonDetail table(Id int IDENTITY PRIMARY KEY, MedicalOrderRecipe varchar(20), ProductCodeHeon varchar(20), TotalQuantity int, [Status] tinyint, [Message] varchar(max))

	--Tabla temporal que se devuelve y sirve para poder validar
	declare @TableMessage table(CodeMessage int, MessageReturn varchar(max))

	--Id de la cabecera
	declare @HeaderId int

	--begin transaction
	begin try
	
		--Se obtiene los datos de la cabecera
		select 
		@LogisticOperator = t.x.value('LogisticOperator[1]','int'),
		@OfficeType = t.x.value('OfficeType[1]','int'),
		@CareCenterCode = t.x.value('CareCenterCode[1]','varchar(20)'),
		@JsonSolicitud = t.x.value('JsonSolicitud[1]','varchar(max)'),
		@JsonEntrega = t.x.value('JsonEntrega[1]','varchar(max)'),
		@JsonRespuestaHEON = t.x.value('JsonRespuestaHeon[1]','varchar(max)'),
		@EntityId = t.x.value('EntityId[1]','int'),
		@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
		@EntityName = t.x.value('EntityName[1]','varchar(250)'),
		@Status = t.x.value('Status[1]','tinyint')
		from @Xml.nodes('/Header') t(x)
		
		print '@JsonRespuestaHEON: ' + @JsonRespuestaHEON

		--Se obtiene los datos de los detalles
		insert into @TempControlIntegrationHeonDetail
		select 
		t.x.value('MedicalOrderRecipe[1]','varchar(20)') as MedicalOrderRecipe,
		t.x.value('ProductCodeHeon[1]','varchar(20)') as ProductCodeHeon,
		t.x.value('TotalQuantity[1]','int') as TotalQuantity,
		t.x.value('Status[1]','tinyint') as Status,
		t.x.value('Message[1]','varchar(max)') as Message
		from @Xml.nodes('/Header/Details') t(x)
		
		--Se obtiene el id de la dispensación farmaceutica
		set @EntityId = (select Id from Inventory.PharmaceuticalDispensing where Code = @EntityCode)

		if @JsonRespuestaHEON = ''
		begin

			--Se guarda la cabecera de la tabla de control de integración
			insert into [Inventory].[ControlIntegrationHeon]
			([LogisticOperator], [OfficeType], [CareCenterCode], [JsonSolicitud], [JsonEntrega], [JsonRespuestaHEON], [EntityId], [EntityCode], [EntityName],
			[Status], [ConfirmationDate], [ConfirmationUser])
			values
			(@LogisticOperator, @OfficeType, @CareCenterCode, @JsonSolicitud, @JsonEntrega, @JsonRespuestaHEON, @EntityId, @EntityCode, @EntityName,
			@Status, [Common].[GETDATE](), @CodeUser)

			--Se obtiene el id generado
			set @HeaderId = SCOPE_IDENTITY()

			--Se insertan los detalles
			insert into [Inventory].[ControlIntegrationHeonDetail]
			([ControlIntegrationHeonId], [MedicalOrderRecipe], [ProductCodeHeon], [TotalQuantity], [Status], [Message])
			select @HeaderId, MedicalOrderRecipe, ProductCodeHeon, TotalQuantity, [Status], [Message]
			from @TempControlIntegrationHeonDetail

		end
		else begin

			--Se obtiene el id de la cabecera de la tabla de control
			set @HeaderId = (select Id from Inventory.ControlIntegrationHeon where EntityCode = @EntityCode)

			--Se actualiza el estado y la respuesta de HEON en la tabla de control
			update [Inventory].[ControlIntegrationHeon] set Status = @Status, JsonRespuestaHEON = @JsonRespuestaHEON where Id = @HeaderId

			--Se actualizan los detalles con la respuesta de HEON
			update cihd set cihd.Status = temp.Status, cihd.Message = temp.Message
			from Inventory.ControlIntegrationHeonDetail cihd
			inner join @TempControlIntegrationHeonDetail temp on temp.MedicalOrderRecipe = cihd.MedicalOrderRecipe
			where cihd.ControlIntegrationHeonId = @HeaderId

		end
		
		--Se inserta ok en la tabla return
		insert into @TableMessage(CodeMessage, MessageReturn) values(0, 'Se ha guardado correctamente en las tablas de control de integración')
		
		--Se devuelve la info
		select * from @TableMessage

	end try
	begin catch

		--Se limpia la tabla de mensajes
		delete from @TableMessage

		--Se inserta en la tabla de mensajes el error
		insert into @TableMessage(CodeMessage, MessageReturn) values(999, ERROR_MESSAGE())

		--Se devuelve la info
		select * from @TableMessage

	end catch

END