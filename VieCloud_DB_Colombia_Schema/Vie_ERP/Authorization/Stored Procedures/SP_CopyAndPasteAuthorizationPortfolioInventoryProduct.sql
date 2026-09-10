-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/05/2020
-- Description:	Procedimiento que se encarga del Copy & Paste de los productos al portafolio de autorización
-- =============================================
CREATE PROCEDURE [Authorization].[SP_CopyAndPasteAuthorizationPortfolioInventoryProduct] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), 
	AuthorizationGroupId int, AuthorizationGroupCode varchar(20), AuthorizationGroupCodeName varchar(max),
	InventoryProductId int, InventoryProductCode varchar(20), InventoryProductName varchar(max), InventoryProductCodeName varchar(max))

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
		t.x.value('InventoryProductId[1]','int') as InventoryProductId,
		t.x.value('InventoryProductCode[1]','varchar(20)') as InventoryProductCode,
		t.x.value('InventoryProductName[1]','varchar(max)') as InventoryProductName,
		t.x.value('InventoryProductCodeName[1]','varchar(max)') as InventoryProductCodeName
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		Declare @Position as int = 0
		--Se declara la variable para poder realizar las validaciones
		Declare @Count as int

		--Se declara un cursor y las variables que lleva el cursor
		declare @CountFields as int, @StatusField as int, @MessageField as varchar(100), @Id as int
		declare @AuthorizationGroupId int, @AuthorizationGroupCode varchar(20), @AuthorizationGroupCodeName varchar(max)
		declare @InventoryProductId int, @InventoryProductCode varchar(20), @InventoryProductName varchar(max), @InventoryProductCodeName varchar(max)
		
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], [Id], 
										   AuthorizationGroupId, AuthorizationGroupCode, AuthorizationGroupCodeName,
										   InventoryProductId, InventoryProductCode, InventoryProductName, InventoryProductCodeName
										   From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
		@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
		@InventoryProductId, @InventoryProductCode, @InventoryProductName, @InventoryProductCodeName

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 2
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
				@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
				@InventoryProductId, @InventoryProductCode, @InventoryProductName, @InventoryProductCodeName
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
				@InventoryProductId, @InventoryProductCode, @InventoryProductName, @InventoryProductCodeName
				continue
			end

			--Se obtiene la información del grupo
			select @AuthorizationGroupId = Id, @AuthorizationGroupCodeName = Code + ' - ' + Name from [Authorization].AuthorizationGroup where Code = @AuthorizationGroupCode

			--Se valida que el producto exista
			if not exists(select 1 from Inventory.InventoryProduct where Code = @InventoryProductCode)
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El producto con código ' + @InventoryProductCode + ' del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
				@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
				@InventoryProductId, @InventoryProductCode, @InventoryProductName, @InventoryProductCodeName
				continue
			end

			--Se obtiene la información del producto
			select @InventoryProductId = Id, @InventoryProductName = Name, @InventoryProductCodeName = Code + ' - ' + Name from Inventory.InventoryProduct where Code = @InventoryProductCode

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1,
			AuthorizationGroupId = @AuthorizationGroupId, AuthorizationGroupCodeName = @AuthorizationGroupCodeName,
			InventoryProductId = @InventoryProductId, InventoryProductName = @InventoryProductName, InventoryProductCodeName = @InventoryProductCodeName
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, 
			@AuthorizationGroupId, @AuthorizationGroupCode, @AuthorizationGroupCodeName,
			@InventoryProductId, @InventoryProductCode, @InventoryProductName, @InventoryProductCodeName
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar productos del inventario (medicamentos, insumos, dispositivos médicos) hacia el portafolio de un grupo de autorización, procesando un listado enviado en formato XML. Por cada ítem del XML valida que el grupo de autorización exista en el catálogo de grupos y que el producto exista en el inventario maestro; si alguna validación falla, marca el registro con error y un mensaje descriptivo de la posición. Devuelve el resultado del procesamiento fila a fila indicando si cada producto fue vinculado exitosamente al grupo de autorización o si presentó inconsistencias, siendo utilizado en la gestión de portafolios de autorización para carga masiva desde interfaz.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y homologa, fila por fila desde un XML, pares (grupo de autorización, producto de inventario) por su Code, devolviendo el listado enriquecido con IDs/nombres y un estado/mensaje por registro para soportar carga masiva tipo Copy & Paste.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlObject debe seguir el esquema /Data/Row con los nodos CountFields, StatusField, MessageField, AuthorizationGroupId/Code/CodeName e InventoryProductId/Code/Name/CodeName; Cada fila debe declarar CountFields=2 para considerarse estructuralmente válida; Los códigos AuthorizationGroupCode e InventoryProductCode deben existir previamente en Authorization.AuthorizationGroup e Inventory.InventoryProduct respectivamente', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila del XML se procesa de forma independiente: un error en una fila no detiene el procesamiento de las demás (uso de CONTINUE en el cursor); La estructura válida exige exactamente 2 campos por registro (CountFields=2); La resolución de IDs se hace por Code: AuthorizationGroup.Code y InventoryProduct.Code son las claves de homologación; El procedimiento NO realiza INSERT/UPDATE sobre tablas físicas; solo valida y devuelve un dataset enriquecido (variable de tabla); En caso de excepción capturada, se devuelve igualmente el contenido de la tabla temporal sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de autorización; Producto de inventario; Portafolio de autorización; Carga masiva (Copy & Paste); Homologación por código', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableXmlObject: Al finalizar el cursor (o ante excepción en el CATCH) retorna SELECT * FROM @TableXmlObject con StatusField y MessageField por fila', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CountFields <> 2 → Marca el registro con StatusField=0 y mensaje ''El registro N no tiene la estructura requerida'' y avanza al siguiente; si NOT EXISTS en Authorization.AuthorizationGroup con Code = @AuthorizationGroupCode → Marca el registro con StatusField=0 y mensaje indicando que el grupo de autorización no existe; si NOT EXISTS en Inventory.InventoryProduct con Code = @InventoryProductCode → Marca el registro con StatusField=0 y mensaje indicando que el producto no existe; si Todas las validaciones aprobadas → Actualiza el registro con StatusField=1 y completa Id/Name/CodeName resueltos del grupo y del producto', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationGroup; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioInventoryProduct';
-- GO
