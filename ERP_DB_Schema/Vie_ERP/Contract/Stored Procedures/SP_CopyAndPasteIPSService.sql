


-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de Cruce Anticipo vs CxC
-- =============================================
CREATE PROCEDURE [Contract].[SP_CopyAndPasteIPSService] 
	@XmlObject as Xml,
	@ServiceManual as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY,CountFields int, StatusField int, 
								MessageField varchar(max), IPSServiceCode varchar(20), IPSServiceDescription varchar(max), IPSServiceId int,
								Class tinyint, DefaultService bit, ServiceAmount varchar(18))

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('IPSServiceCode[1]','varchar(20)') as IPSServiceCode,
		t.x.value('IPSServiceDescription[1]','varchar(max)') as IPSServiceDescription,
		t.x.value('IPSServiceId[1]','int') as IPSServiceId,
		t.x.value('Class[1]','tinyint') as Class,
		t.x.value('DefaultService[1]','bit') as DefaultService,
		t.x.value('ServiceAmount[1]','varchar(18)') as ServiceAmount
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
		Declare @IPSServiceCode as varchar(20)
		Declare @IPSServiceDescription as varchar(max)
		Declare @IPSServiceId as int
		Declare @Class as tinyint
		Declare @DefaultService as bit
		Declare @ServiceAmount as varchar(18)
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], 
										   [Id], [IPSServiceCode], [IPSServiceDescription], [IPSServiceId], 
										   [Class], [DefaultService], [ServiceAmount] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se asigna el defaultService
			if @Position = 1
			Begin
				set @DefaultService = 1
			End
			Else
			Begin
				set @DefaultService = 0
			End

			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 2
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End		

			--Se valida que el código del servicio ips exista
			if (select count(*) 
			from Contract.IPSService 
			where Status = 1 and Presentation = 1 and ServiceManual = @ServiceManual and 
			ServiceClass <> 1 and ServiceClass <> 6 and Code = @IPSServiceCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del servicio ips del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End
			
			--Se valida que la cantidad(ServiceAmount) no este vacia
			if LEN(@ServiceAmount) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cantidad del registro ' + convert(varchar(3),@Position) + ' no tiene valor'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End

			--Se valida que la cantidad(ServiceAmount) sea numérico
			if ISNUMERIC(@ServiceAmount) <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cantidad del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End

			--Se consulta la tabla IPSService para obtener los datos necesarios
			select @IPSServiceId = Id, @IPSServiceDescription = CONCAT(Code, ' - ', Name), @Class = ServiceClass
			from Contract.IPSService 
			where Status = 1 and Presentation = 1 and ServiceManual = @ServiceManual and 
			ServiceClass <> 1 and ServiceClass <> 6 and Code = @IPSServiceCode

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, IPSServiceDescription = @IPSServiceDescription, 
			IPSServiceId = @IPSServiceId, Class = @Class, DefaultService = @DefaultService
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
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