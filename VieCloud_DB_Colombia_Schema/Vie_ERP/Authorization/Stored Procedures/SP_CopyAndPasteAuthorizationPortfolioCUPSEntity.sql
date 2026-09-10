-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/05/2020
-- Description:	Procedimiento que se encarga del Copy & Paste de los servicios al portafolio de autorización
-- =============================================
CREATE PROCEDURE [Authorization].[SP_CopyAndPasteAuthorizationPortfolioCUPSEntity] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), 
	AuthorizationGroupId int, AuthorizationGroupCode varchar(20), AuthorizationGroupCodeName varchar(max),
	CUPSEntityId int, CUPSEntityCode varchar(20), CUPSEntityName varchar(200), CUPSEntityCodeName varchar(max),
	ContractDescriptionId int, ContractDescriptionCode varchar(20), ContractDescriptionCodeName varchar(max))

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('AuthorizationGroupId[1]','int') as AuthorizationGroupId,
		t.x.value('AuthorizationGroupCode[1]','varchar(20)') as AuthorizationGroupCode,
		t.x.value('AuthorizationGroupCodeName[1]','varchar(max)') as AuthorizationGroupCodeName,
		t.x.value('CUPSEntityId[1]','int') as CUPSEntityId,
		t.x.value('CUPSEntityCode[1]','varchar(20)') as CUPSEntityCode,
		t.x.value('CUPSEntityName[1]','varchar(200)') as CUPSEntityName,
		t.x.value('CUPSEntityCodeName[1]','varchar(max)') as CUPSEntityCodeName,
		t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
		t.x.value('ContractDescriptionCode[1]','varchar(20)') as ContractDescriptionCode,
		t.x.value('ContractDescriptionCodeName[1]','varchar(max)') as ContractDescriptionCodeName
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		Declare @Position as int = 0
		--Se declara la variable para poder realizar las validaciones
		Declare @Count as int

		--Se declara un cursor y las variables que lleva el cursor
		declare @CountFields as int, @StatusField as int, @MessageField as varchar(100), @Id as int
		declare @AuthorizationGroupId int, @AuthorizationGroupCode varchar(20), @AuthorizationGroupCodeName varchar(max)
		declare @CUPSEntityId int, @CUPSEntityCode varchar(20), @CUPSEntityName varchar(200), @CUPSEntityCodeName varchar(max)
		declare @ContractDescriptionId int, @ContractDescriptionCode varchar(20), @ContractDescriptionCodeName varchar(max)
		
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], [Id], 
										   AuthorizationGroupId, AuthorizationGroupCode, AuthorizationGroupCodeName,
										   CUPSEntityId, CUPSEntityCode, CUPSEntityName, CUPSEntityCodeName,
										   ContractDescriptionId, ContractDescriptionCode, ContractDescriptionCodeName
										   From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
		@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
		@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
		@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 2 and @CountFields <> 3
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
				@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
				@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
				@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName
				continue
			End		

			--Se valida que el grupo exista
			if not exists(select 1 from [Authorization].AuthorizationGroup where Code = @AuthorizationGroupCode)
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El grupo de autorización con código ' + @AuthorizationGroupCode + ' del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
				@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
				@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
				@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName
				continue
			end

			--Se obtiene la información del grupo
			select @AuthorizationGroupId = Id, @AuthorizationGroupCodeName = Code + ' - ' + Name from [Authorization].AuthorizationGroup where Code = @AuthorizationGroupCode

			--Se valida que el cups exista
			if not exists(select 1 from Contract.CUPSEntity where Code = @CUPSEntityCode)
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El CUPS con código ' + @CUPSEntityCode + ' del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
				@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
				@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
				@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName
				continue
			end

			--Se obtiene la información del cups
			select @CUPSEntityId = Id, @CUPSEntityName = Description, @CUPSEntityCodeName = Code + ' - ' + Description from Contract.CUPSEntity where Code = @CUPSEntityCode

			--Se valida que el código de la descripción exista
			if @ContractDescriptionCode <> ''
			begin
				if not exists(select 1 from Contract.ContractDescriptions where Code = @ContractDescriptionCode)
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La descripción con código ' + @ContractDescriptionCode + ' del registro ' + convert(varchar(3),@Position) + ' no existe'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
					@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
					@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
					@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName
					continue
				end

				--Se valida que el código de la descripción este asociado al cups
				if not exists(select 1
				from Contract.CUPSEntity ce
				inner join Contract.CUPSEntityContractDescriptions cecd on cecd.CUPSEntityId = ce.Id
				inner join Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
				where ce.Code = @CUPSEntityCode and cd.Code = @ContractDescriptionCode)
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La descripción con código ' + @ContractDescriptionCode + ' del registro ' + convert(varchar(3),@Position) + ' no está asociada al CUPS ' + @CUPSEntityCode
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
					@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
					@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
					@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName
					continue
				end

				--Se obtiene la información de la descripción
				select @ContractDescriptionId = Id, @ContractDescriptionCodeName = Code + ' - ' + Name from Contract.ContractDescriptions where Code = @ContractDescriptionCode
			end

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, 
			AuthorizationGroupId = @AuthorizationGroupId, AuthorizationGroupCodeName = @AuthorizationGroupCodeName,
			CUPSEntityId = @CUPSEntityId, CUPSEntityName = @CUPSEntityName, CUPSEntityCodeName = @CUPSEntityCodeName,
			ContractDescriptionId = @ContractDescriptionId, ContractDescriptionCodeName = @ContractDescriptionCodeName
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
			@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
			@CUPSEntityId, @CUPSEntityCode, @CUPSEntityName, @CUPSEntityCodeName,
			@ContractDescriptionId, @ContractDescriptionCode, @ContractDescriptionCodeName
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar servicios CUPS (procedimientos/exámenes) al portafolio de un grupo de autorización, recibiendo los datos en formato XML con múltiples registros. Para cada servicio recibido, valida que exista el grupo de autorización, el código CUPS y opcionalmente la descripción de contrato asociada, reportando errores por registro si alguna de estas entidades no existe. Compone la relación entre grupos de autorización (AuthorizationGroup), servicios CUPS del catálogo (CUPSEntity) y descripciones de contrato (ContractDescriptions/CUPSEntityContractDescriptions), insertando o actualizando los vínculos en el portafolio de autorización. Se usa para cargas masivas o duplicación de configuraciones de servicios autorizables entre grupos, facilitando la administración del portafolio de servicios habilitados para autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece (resolviendo IDs y etiquetas) un lote XML de servicios CUPS para pegar en el portafolio de un grupo de autorización, marcando estado y mensaje por fila.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener estructura /Data/Row con los campos esperados (CountFields, AuthorizationGroupCode, CUPSEntityCode, ContractDescriptionCode, etc.); Cada fila debe declarar CountFields en {2,3} para considerarse estructuralmente válida; Los códigos referenciados deben existir previamente en Authorization.AuthorizationGroup, Contract.CUPSEntity y, si aplica, Contract.ContractDescriptions; Si se envía ContractDescriptionCode no vacío, debe existir vínculo en Contract.CUPSEntityContractDescriptions con el CUPS indicado', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El procesamiento es por fila vía cursor; un error de validación en una fila no detiene el procesamiento de las demás; Una fila inválida nunca recibe StatusField=1; solo se enriquece con IDs/CodeName cuando pasa todas las validaciones; El procedimiento no inserta ni modifica datos en tablas físicas; solo opera sobre la variable de tabla y devuelve el resultado; Los mensajes de error siempre incluyen el número de posición (1-based) del registro dentro del lote; La existencia y resolución del CUPS y del grupo siempre se valida; la de ContractDescription solo si su código no está vacío', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de autorización; Servicio/Procedimiento CUPS; Descripción de contrato; Portafolio de autorización; Copy & Paste de servicios', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Por cada nodo /Data/Row del XML se inserta una fila con los valores tipados extraídos; [UPDATE] @TableXmlObject: Si CountFields no es 2 ni 3 → StatusField=0 y MessageField=''El registro N no tiene la estructura requerida''; [UPDATE] @TableXmlObject: Si no existe AuthorizationGroup con Code=@AuthorizationGroupCode → StatusField=0 y mensaje ''El grupo de autorización con código X del registro N no existe''; [UPDATE] @TableXmlObject: Si no existe CUPSEntity con Code=@CUPSEntityCode → StatusField=0 y mensaje ''El CUPS con código X del registro N no existe''; [UPDATE] @TableXmlObject: Si ContractDescriptionCode<>'''' y no existe en Contract.ContractDescriptions → StatusField=0 y mensaje ''La descripción con código X del registro N no existe''; [UPDATE] @TableXmlObject: Si ContractDescriptionCode<>'''' y no hay relación en CUPSEntityContractDescriptions entre el CUPS y la descripción → StatusField=0 y mensaje ''La descripción con código X del registro N no está asociada al CUPS Y''; [UPDATE] @TableXmlObject: Si todas las validaciones pasan → StatusField=1 y se completan AuthorizationGroupId/CodeName, CUPSEntityId/Name/CodeName y ContractDescriptionId/CodeName resueltos desde los catálogos; [RETURN_RESULT] @TableXmlObject: Al finalizar (try o catch) se retorna SELECT * FROM @TableXmlObject con el resultado de validación de cada fila', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CountFields <> 2 AND @CountFields <> 3 → Marca la fila como inválida por estructura y avanza al siguiente registro del cursor else Continúa con las validaciones de existencia y asociación; si @ContractDescriptionCode <> '''' → Ejecuta validación de existencia de la descripción y de su asociación al CUPS vía CUPSEntityContractDescriptions else Omite las validaciones relacionadas con ContractDescription; si Bloque CATCH (excepción durante el procesamiento) → Retorna el contenido actual de @TableXmlObject sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationGroup; Contract.CUPSEntity; Contract.ContractDescriptions; Contract.CUPSEntityContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCUPSEntity';
-- GO
