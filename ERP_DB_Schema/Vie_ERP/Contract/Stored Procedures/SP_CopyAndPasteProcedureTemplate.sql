

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de cubrimientos
-- =============================================
CREATE PROCEDURE [Contract].[SP_CopyAndPasteProcedureTemplate] 
	@XmlObject as Xml
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY,CountFields int, StatusField int, 
								MessageField varchar(max), CUPSCode varchar(20), CUPSDescription varchar(max), CUPSId int, 
								ContractDescriptionCode varchar(20), ContractDescriptionId int, CUPSEntityContractDescriptionId int, ContractDescriptionCodeName varchar(200))

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('CUPSCode[1]','varchar(20)') as CUPSCode,
		t.x.value('CUPSDescription[1]','varchar(max)') as CUPSDescription,
		t.x.value('CUPSId[1]','int') as CUPSId,
		t.x.value('ContractDescriptionCode[1]','varchar(20)') as ContractDescriptionCode,
		t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
		t.x.value('CUPSEntityContractDescriptionId[1]','int') as CUPSEntityContractDescriptionId,
		t.x.value('ContractDescriptionCodeName[1]','varchar(200)') as ContractDescriptionCodeName
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		Declare @Position as int = 0
		--Se declara la variable para poder realizar las validaciones
		Declare @Count as int

		--Se declara un cursor y las variables que lleva el cursor
		Declare @CountFields as int
		Declare @StatusField as int
		Declare @MessageField as varchar(100)
		Declare @Id as int
		Declare @CUPSCode as varchar(20)
		Declare @CUPSDescription as varchar(max)
		Declare @CUPSId as int
		declare @ContractDescriptionCode varchar(20)
		declare @ContractDescriptionId as int
		declare @CUPSEntityContractDescriptionId as int
		declare @ContractDescriptionCodeName varchar(200)
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], 
										   [Id], [CUPSCode], [CUPSDescription], [CUPSId], 
										   [ContractDescriptionCode], [ContractDescriptionId], 
										   [CUPSEntityContractDescriptionId], [ContractDescriptionCodeName] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
		@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 1 and @CountFields <> 2 
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(10),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
				@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName
				continue
			End		

			--Se valida que el código del cups exista
			if (select count(*) from Contract.CUPSEntity where Code = @CUPSCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código CUPS del registro ' + convert(varchar(10),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
				@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName
				continue
			End
			
			--Se valida que si el cups tiene descripciones y el campo descripción viene vacío
			if exists(select 1 from Contract.ViewListCupsEntityWithDescriptions where CUPSEntityCode = @CUPSCode) and (@ContractDescriptionCode = '' or @ContractDescriptionCode is null)
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código CUPS del registro ' + convert(varchar(10),@Position) + ' maneja descripción pero el campo está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
				@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName
				continue
			end

			--Se valida que el código de la descripción exista
			if @ContractDescriptionCode <> ''
			begin
				if not exists(select 1 from Contract.ContractDescriptions where Code = @ContractDescriptionCode)
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El código de la descripción del registro ' + convert(varchar(10),@Position) + ' no existe'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
					@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName
					continue
				end

				--Se valida que el código de la descripción este asociado al cups
				if not exists(select 1
				from Contract.CUPSEntity ce
				inner join Contract.CUPSEntityContractDescriptions cecd on cecd.CUPSEntityId = ce.Id
				inner join Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
				where ce.Code = @CUPSCode and cd.Code = @ContractDescriptionCode)
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El código de la descripción del registro ' + convert(varchar(10),@Position) + ' no está relacionado en el CUPS'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
					@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName
					continue
				end

				select @ContractDescriptionId = cd.Id, @ContractDescriptionCodeName = cd.Code + ' - ' + cd.Name, @CUPSEntityContractDescriptionId = cecd.Id
				from Contract.CUPSEntity ce
				inner join Contract.CUPSEntityContractDescriptions cecd on cecd.CUPSEntityId = ce.Id
				inner join Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
				where ce.Code = @CUPSCode and cd.Code = @ContractDescriptionCode
			end

			--Se consulta la tabla CUPS para obtener los datos necesarios
			select @CUPSId = Id, @CUPSDescription = CONCAT(Code, ' - ', [Description]) from Contract.CUPSEntity where Code = @CUPSCode

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, CUPSDescription = @CUPSDescription, CUPSId = @CUPSId, ContractDescriptionId = @ContractDescriptionId,
			ContractDescriptionCodeName = @ContractDescriptionCodeName, CUPSEntityContractDescriptionId = @CUPSEntityContractDescriptionId
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CUPSCode, @CUPSDescription, @CUPSId, @ContractDescriptionCode, @ContractDescriptionId, 
			@CUPSEntityContractDescriptionId, @ContractDescriptionCodeName
			continue

		End

		Close InfoItem
		Deallocate InfoItem

		
		--Se retorna la tabla
		select * from @TableXmlObject
		
	end try
	begin catch

		--rollback transaction
		select * from @TableXmlObject

	end catch

END