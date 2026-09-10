

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa el copiado y pegado masivo de la grilla de cubrimientos de procedimientos CUPS en contratos. Recibe un listado de filas en formato XML donde cada fila indica un código CUPS y su descripción de contrato asociada, y verifica que cada registro tenga la estructura correcta, que el código CUPS exista en el catálogo maestro (CUPSEntity), que si el CUPS maneja descripciones el campo correspondiente no esté vacío, y que el código de descripción de contrato exista en el catálogo de descripciones (ContractDescriptions) y esté correctamente vinculado al CUPS mediante la tabla de relación CUPSEntityContractDescriptions. Retorna el listado de filas con un estado de validación (éxito o error) y un mensaje descriptivo por cada registro, permitiendo al usuario identificar qué líneas del copy-paste son inválidas antes de persistir los cambios en la configuración contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Validar fila por fila un listado XML de cubrimientos (CUPS y descripción contractual) pegado en una rejilla, marcando estado y mensaje de error y completando los identificadores y descripciones cuando es válido.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos esperados (CountFields, StatusField, CUPSCode, ContractDescriptionCode, etc.); Las tablas de catálogo Contract.CUPSEntity, Contract.ContractDescriptions y Contract.CUPSEntityContractDescriptions deben estar pobladas para validar existencia y relación; La vista Contract.ViewListCupsEntityWithDescriptions debe estar disponible para determinar si un CUPS exige descripción', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada registro del XML se procesa de forma independiente; un registro inválido no aborta el procesamiento de los demás; Solo se aceptan registros con CountFields = 1 o 2; Un código CUPS solo es válido si existe en Contract.CUPSEntity; Si el CUPS maneja descripciones, el ContractDescriptionCode es obligatorio; El ContractDescriptionCode debe existir y además estar relacionado con el CUPS en CUPSEntityContractDescriptions; Los identificadores y nombres descriptivos solo se completan cuando el registro es válido; Ante excepción, se devuelve el contenido de la tabla temporal sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Descripciones de contrato; Cubrimientos contractuales; Homologación de códigos', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Se cargan en una tabla temporal todas las filas del XML recibido (/Data/Row); [UPDATE] @TableXmlObject: Cuando CountFields no es 1 ni 2 se establece StatusField=0 con mensaje indicando que el registro no tiene la estructura requerida; [UPDATE] @TableXmlObject: Cuando el CUPSCode no existe en Contract.CUPSEntity se establece StatusField=0 con mensaje ''El código CUPS ... no existe''; [UPDATE] @TableXmlObject: Cuando el CUPS exige descripción (existe en ViewListCupsEntityWithDescriptions) y ContractDescriptionCode es vacío/nulo se establece StatusField=0 con mensaje ''maneja descripción pero el campo está vacío''; [UPDATE] @TableXmlObject: Cuando ContractDescriptionCode no existe en Contract.ContractDescriptions se establece StatusField=0 con mensaje ''El código de la descripción ... no existe''; [UPDATE] @TableXmlObject: Cuando ContractDescriptionCode no está asociado al CUPS en CUPSEntityContractDescriptions se establece StatusField=0 con mensaje ''no está relacionado en el CUPS''; [UPDATE] @TableXmlObject: Cuando todas las validaciones son satisfactorias se establece StatusField=1 y se completan CUPSId, CUPSDescription, ContractDescriptionId, ContractDescriptionCodeName y CUPSEntityContractDescriptionId; [RETURN_RESULT] @TableXmlObject: Al finalizar (o ante excepción capturada) se retorna el contenido completo de la tabla temporal con estados y mensajes por registro', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CountFields distinto de 1 y 2 → Marca el registro como inválido (StatusField=0) con mensaje ''no tiene la estructura requerida'' y omite el resto de validaciones; si No existe un CUPS con el código indicado en Contract.CUPSEntity → Marca el registro como inválido con mensaje ''El código CUPS ... no existe'' y continúa con el siguiente; si El CUPS aparece en ViewListCupsEntityWithDescriptions y el ContractDescriptionCode viene vacío o nulo → Marca el registro como inválido con mensaje ''maneja descripción pero el campo está vacío''; si ContractDescriptionCode no vacío y no existe en Contract.ContractDescriptions → Marca el registro como inválido con mensaje ''El código de la descripción ... no existe''; si ContractDescriptionCode no vacío y no está vinculado al CUPS vía CUPSEntityContractDescriptions → Marca el registro como inválido con mensaje ''no está relacionado en el CUPS''; si Todas las validaciones pasan → Marca StatusField=1 y completa CUPSId, CUPSDescription (Code - Description), ContractDescriptionId, ContractDescriptionCodeName (Code - Name) y CUPSEntityContractDescriptionId', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntity; Contract.ViewListCupsEntityWithDescriptions; Contract.ContractDescriptions; Contract.CUPSEntityContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProcedureTemplate';
-- GO
