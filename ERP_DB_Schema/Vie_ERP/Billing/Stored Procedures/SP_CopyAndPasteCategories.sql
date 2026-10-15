


-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de Cruce Anticipo vs CxC
-- =============================================
CREATE PROCEDURE [Billing].[SP_CopyAndPasteCategories] 
	@XmlObject as Xml
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY,CountFields int, StatusField int, 
								MessageField varchar(max), UserCode varchar(50), UserDescription varchar(max), UserId int)

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('UserCode[1]','varchar(50)') as UserCode,
		t.x.value('UserDescription[1]','varchar(max)') as UserDescription,
		t.x.value('UserId[1]','int') as UserId
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
		Declare @UserCode as varchar(50)
		Declare @UserDescription as varchar(max)
		Declare @UserId as int
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], 
										   [Id], [UserCode], [UserDescription], [UserId] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @UserCode, @UserDescription, @UserId

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @UserCode, @UserDescription, @UserId
				continue
			End		

			--Se valida que el código del usuario exista
			if (select count(*) from [Security].[User] where UserCode = @UserCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del usuario del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @UserCode, @UserDescription, @UserId
				continue
			End
			
			--Se consulta la tabla usuarios para obtener los datos necesarios
			select @UserId = u.Id, @UserDescription = CONCAT(u.UserCode, ' - ', p.Fullname) 
			from [Security].[User] u
			inner join [Security].[Person] p on u.IdPerson = p.Id
			where u.UserCode = @UserCode

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, UserDescription = @UserDescription, UserId = @UserId
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @UserCode, @UserDescription, @UserId
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