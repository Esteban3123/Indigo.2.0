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