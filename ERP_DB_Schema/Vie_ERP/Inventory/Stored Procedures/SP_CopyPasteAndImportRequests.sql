

-- =================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 02/07/2019
-- Description:	Procedimiento que se encarga de validar para el CopyPaste e importar las solicitudes de inventario
-- =================================================================================================================
CREATE PROCEDURE [Inventory].[SP_CopyPasteAndImportRequests]
	@Xml xml
AS
BEGIN
		
	--Tabla temporal para obtener la información del xml y retornar
	declare @TableXml table(ProductId int, ProductCode varchar(20), ProductDescription varchar(max), ConsumptionUnit varchar(max), Quantity varchar(20), 
	Observation varchar(300), CodeResult int, MessageResult varchar(max))
	
	Begin try	

		--Se obtienen los datos del xml
		insert into @TableXml
		select 
			0 as ProductId,
			t.x.value('ProductCode[1]','varchar(20)') as ProductCode,
			'---' as ProductDescription,
			'---' as ConsumptionUnit,
			t.x.value('Quantity[1]','varchar(20)') as Quantity,
			t.x.value('Observation[1]','varchar(300)') as Observation,
			000 as CodeResult,
			'Producto agregado correctamente' as MessageResult
		from @Xml.nodes('/InventoryRequest/InventoryRequestDetail') t(x)
		
		--Se valida que el producto exista
		update t set t.CodeResult = 999, t.MessageResult = 'El producto con código ' + t.ProductCode + ' no existe'
		from @TableXml t
		left join Inventory.InventoryProduct p on p.Code = t.ProductCode
		where p.Id is null
		
		--Se valida que la cantidad sea numérica
		update t set t.CodeResult = 999, t.MessageResult = 'La cantidad asociada al producto ' + t.ProductCode + ' no es numérico'
		from @TableXml t
		where t.CodeResult = 000 and ISNUMERIC(t.Quantity) = 0
		
		--Se actualiza la información del producto
		update t set t.ProductId = p.Id, t.ProductDescription = p.Code + ' - ' + p.Name
		from @TableXml t
		inner join Inventory.InventoryProduct p on p.Code = t.ProductCode
		
		--Se actualiza la unidad de consumo
		update t set t.ConsumptionUnit = pu.Code + ' - ' + pu.Name
		from @TableXml t
		inner join Inventory.InventoryProduct p on p.Code = t.ProductCode
		inner join Inventory.PackagingUnit pu on pu.Id = p.PackagingUnitId
		where t.CodeResult = 000
		
		--Se retorna la tabla con los resultados
		select * from @TableXml

	End try
	Begin Catch

		--Se retorna el error
		delete from @TableXml
		insert into @TableXml(CodeResult, MessageResult) values(888, ERROR_MESSAGE())
		select * from @TableXml

	End Catch

END