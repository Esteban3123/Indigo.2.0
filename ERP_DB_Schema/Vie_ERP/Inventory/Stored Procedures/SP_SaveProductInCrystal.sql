






-- ==========================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 11/09/2017
-- Description:	Procedimiento que se encarga de guardar, actualizar productos en la BD de Crystal, cuando el producto sea de clase otro
-- ==========================================================================================================================
CREATE PROCEDURE [Inventory].[SP_SaveProductInCrystal] 
	@xml xml
AS
BEGIN
	
	--Id del producto de vie
	declare @ProductId int

	--Codigo del producto de vie, se utiliza para guardarlo en crystal
	declare @Code varchar(20)

	--Nombre del producto de vie, se utiliza para guardarlo en crystal
	declare @Name varchar(300)

	--Id del tipo de producto, con este tipo se realiza el guardado de la categoria en crystal
	declare @ProductTypeId int

	--Id de la unidad de medida, con esta unidad se realiza el guardado de la unidad en crystal
	declare @MeasurementUnitId int

	--Id del iva, con este iva se realiza el guardado del iva en crystal
	declare @IvaId int

	--Observaciones de vie para el producto en crystal
	declare @Observations varchar(300)

	--Estado de vie para el producto en crystal
	declare @Status bit

	--Contenedor de crystal, todavia no se va a utilizar
	declare @Container varchar(20)

	--Codigo que se asigna para poder validar la categoria
	declare @ProductTypeCode varchar(20)

	--Nombre del tipo de producto que sirve para guardar la categoria
	declare @ProductTypeName varchar(100)

	--Id de la categoria de crystal
	declare @CategoryIdCrystal int

	--Codigo que se asigna para validar la unidad de medida
	declare @MeasurementUnitCode varchar(20)

	--Nombre de la unidad de medida que sirve para la de crystal
	declare @MeasurementUnitName varchar(100)

	--Id de la unidad de medida de crystal
	declare @MeasurementUnitIdCrystal int

	--Codigo que se asigna para validar el iva
	declare @IvaCode varchar(20)

	--Nombre del iva que sirve para la de crystal
	declare @IvaName varchar(100)

	--Porcentaje iva que sirve para la de crystal
	declare @IvaPercentage numeric(5,2)

	--Id del iva de crystal
	declare @IvaIdCrystal int

	--Id creado con crystal
	declare @ProductIdCrystal int

	--begin transaction
	Begin try
	
		--Se obtienen los campos del xml y se asignan a los declarados
		select 
		@ProductId = t.x.value('ProductId[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@Name = t.x.value('Name[1]','varchar(300)'),
		@ProductTypeId = t.x.value('ProductTypeId[1]','int'),
		@MeasurementUnitId = t.x.value('MeasurementUnitId[1]','int'),
		@IvaId = t.x.value('IvaId[1]','int'),
		@Observations = t.x.value('Observations[1]','varchar(300)'),
		@Status = t.x.value('Status[1]','bit'),
		@Container = t.x.value('Container[1]','varchar(20)')
		from @xml.nodes('/Data') t(x)

		--Si el producto se va a crear
		if (@ProductId = 0)
		begin
			--Se valida si ya existe un producto con el código de Vie en Crystal
			if (select COUNT(*) from dbo.SOLPRODUC where PRODCODIGO = @Code) > 0
			begin
				--Se retorna el error
				select 999 as CodeMessage, 'El producto con código ' + @Code + ' ya existe en Crystal' as Message, 0 as ProductIdVie, 0 as ProductIdCrystal
			end
		end
		
		--Se obtiene el código del tipo de producto de vie para poder validar
		select @ProductTypeCode = Code, @ProductTypeName = [Name] from Inventory.ProductType where Id = @ProductTypeId

		--Se valida si existe una categoria en crystal con el codigo de tipo de producto, si no existe se crea la categoria en crystal
		if (select COUNT(*) from dbo.SOLCATEGO where CATCODIGO = @ProductTypeCode) = 0
		begin
			--Como no existe la categoria, se crea una nueva con los datos de tipo de producto
			insert into dbo.SOLCATEGO(CATCODIGO, CATDESCRI, CATESTADO) values(@ProductTypeCode, @ProductTypeName, 1)

			--Se obtiene el id generado
			set @CategoryIdCrystal = SCOPE_IDENTITY()
		end
		else --Si existe se obtiene el id
		begin
			select @CategoryIdCrystal = CATEAUTON from dbo.SOLCATEGO where CATCODIGO = @ProductTypeCode
		end

		--Se obtiene el código de la unidad de medida de vie para poder validar
		select @MeasurementUnitCode = Code, @MeasurementUnitName = [Name] from Inventory.InventoryMeasurementUnit where Id = @MeasurementUnitId

		--Se valida si existe una unidad de medida en crystal con el codigo de unidad de medida de vie, si no existe se crea la unidad de medida en crystal
		if (select COUNT(*) from dbo.SOLUNIMED where UNIMEDCOD = @MeasurementUnitCode) = 0
		begin
			--Como no existe la unidad de medida, se crea una nueva con los datos de vie
			insert into dbo.SOLUNIMED(UNIMEDCOD, UNIMEDDES) values(@MeasurementUnitCode, @MeasurementUnitName)

			--Se obtiene el id generado
			set @MeasurementUnitIdCrystal = SCOPE_IDENTITY()
		end
		else --Si existe se obtiene el id
		begin
			select @MeasurementUnitIdCrystal = UNIMEDAUT from dbo.SOLUNIMED where UNIMEDCOD = @MeasurementUnitCode
		end

		--Se obtiene el código del iva de vie para poder validar
		select @IvaCode = Code, @IvaName = [Name], @IvaPercentage = Percentage from GeneralLedger.GeneralLedgerIVA where Id = @IvaId

		--Se valida si existe un iva en crystal con el codigo de iva de vie, si no existe se crea el iva en crystal
		if (select COUNT(*) from dbo.SOLIVAPRO where SOCODIVA = @IvaCode) = 0
		begin
			--Como no existe el iva, se crea una nueva con los datos de vie
			insert into dbo.SOLIVAPRO(SOCODIVA, SODESCIVA, SOPORCIVA) values(@IvaCode, @IvaName, @IvaPercentage)

			--Se obtiene el id generado
			set @IvaIdCrystal = SCOPE_IDENTITY()
		end
		else --Si existe se obtiene el id
		begin
			select @IvaIdCrystal = SOAUTOIVA from dbo.SOLIVAPRO where SOCODIVA = @IvaCode
		end
		
		if (select COUNT(*) from dbo.SOLPRODUC where PRODCODIGO = @Code) > 0 --Si existe un producto con el codigo en crystal se actualiza
		begin
			--Se actualiza el producto en crystal
			update dbo.SOLPRODUC set PRODNOMBRE = @Name, PRODESTADO = @Status, PRODOBSER = @Observations, PRODCODCAT = @CategoryIdCrystal, 
			PRODUCIVA = @IvaIdCrystal, PROUNIMED = @MeasurementUnitIdCrystal
			where PRODCODIGO = @Code

			--Se obtiene el id generado del producto en crystal
			SELECT @ProductIdCrystal = PRODAUTON FROM dbo.SOLPRODUC WHERE PRODCODIGO = @Code
		end
		else --Si no existe se guarda
		begin
			--Se guarda el producto en crystal
			insert into dbo.SOLPRODUC(PRODCODIGO, PRODNOMBRE, PRODESTADO, PRODOBSER, PRODCODCAT, PRODUCIVA, PROUNIMED)
			values(@Code, @Name, @Status, @Observations, @CategoryIdCrystal, @IvaIdCrystal, @MeasurementUnitIdCrystal)

			--Se obtiene el id generado del producto en crystal
			set @ProductIdCrystal = SCOPE_IDENTITY()
		end

		--Se retornan los campos con el Ok
		select 0 as CodeMessage, 'El producto ' + @Code + ' tuvo el siguiente mensaje: Producto Guardado Correctamente ' as Message, @ProductId as ProductIdVie, @ProductIdCrystal as ProductIdCrystal
		
	end try
	begin catch

		--Se retorna el error
		select 999 as CodeMessage, 'El producto ' + @Code + ' tuvo el siguiente mensaje: ' + ERROR_MESSAGE() as Message, 0 as ProductIdVie, 0 as ProductIdCrystal

	end catch

END