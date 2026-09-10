

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación que valida y procesa un listado de usuarios enviado en formato XML para la operación de copiar y pegar en la grilla de Cruce de Anticipos contra Cuentas por Cobrar. Recorre cada fila del XML verificando que tenga la estructura correcta y que el código de usuario exista en el módulo de seguridad; si el usuario es válido, enriquece el registro con el nombre completo del usuario obtenido de las tablas Security.User y Security.Person. Devuelve el listado procesado con el estado de validación (éxito o error) y el mensaje correspondiente para cada fila, permitiendo que el formulario en pantalla refleje el resultado del pegado masivo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCategories';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCategories';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y homologa, fila por fila desde un XML, los códigos de usuario usados en la rejilla de Cruce Anticipo vs CxC, devolviendo el listado enriquecido con Id y nombre completo o con el mensaje de error correspondiente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe tener la estructura /Data/Row con nodos CountFields, StatusField, MessageField, UserCode, UserDescription, UserId; Security.User debe contener el UserCode a homologar; Security.Person debe estar relacionada con Security.User vía User.IdPerson = Person.Id para poder construir el Fullname', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila válida debe contener exactamente un campo (CountFields = 1) para considerarse estructuralmente correcta; La descripción del usuario enriquecida se construye como CONCAT(UserCode, '' - '', Person.Fullname); El procesamiento es por fila independiente: un error en una fila no impide procesar las demás; Siempre se devuelve el listado completo con su estado, incluso ante excepciones (en el CATCH también se hace SELECT del table variable)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce Anticipo vs CxC; Usuario; Persona; Pegado masivo (copy/paste) en rejilla', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableXmlObject: Al finalizar el cursor (o ante excepción en el CATCH) se retorna SELECT * FROM @TableXmlObject con el estado y mensaje de cada fila; [UPDATE] @TableXmlObject: Cuando CountFields <> 1, se actualiza la fila con StatusField=0 y mensaje ''El registro N no tiene la estructura requerida''; [UPDATE] @TableXmlObject: Cuando COUNT(*) FROM Security.User WHERE UserCode=@UserCode = 0, se actualiza la fila con StatusField=0 y mensaje ''El código del usuario del registro N no existe''; [UPDATE] @TableXmlObject: Cuando el usuario existe, se actualiza la fila con StatusField=1, UserId = User.Id y UserDescription = CONCAT(UserCode,'' - '',Person.Fullname)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CountFields <> 1 en la fila del XML → Marca StatusField=0 y MessageField=''El registro N no tiene la estructura requerida'' y salta a la siguiente fila else Continúa con validación de existencia del usuario; si No existe registro en Security.User con UserCode = valor recibido → Marca StatusField=0 y MessageField=''El código del usuario del registro N no existe'' y salta a la siguiente fila else Obtiene Id y Fullname desde Security.User+Security.Person y marca StatusField=1', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCategories';
-- GO
