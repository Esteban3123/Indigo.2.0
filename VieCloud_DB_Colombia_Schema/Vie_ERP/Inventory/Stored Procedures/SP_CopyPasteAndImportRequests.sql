

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida e importa solicitudes de inventario a partir de un XML con ítems de pedido (código de producto, cantidad y observación). Para cada ítem recibido, verifica que el producto exista en el catálogo maestro de inventario (medicamentos, insumos, dispositivos médicos) y que la cantidad sea un valor numérico; en caso de error devuelve un código y mensaje descriptivo por ítem. Cuando la validación es exitosa, enriquece cada línea con el identificador interno del producto, su descripción y la unidad de empaque/presentación correspondiente (caja, frasco, ampolla, etc.). Se usa en las funcionalidades de copiar-pegar e importación masiva de solicitudes de inventario, devolviendo al cliente un resultado detallado por cada producto procesado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteAndImportRequests';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteAndImportRequests';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece líneas de solicitudes de inventario provenientes de un XML (copiar-pegar o importación masiva), devolviendo por cada ítem el resultado de validación y datos del producto y su unidad de empaque.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /InventoryRequest/InventoryRequestDetail con nodos ProductCode, Quantity y Observation; Los códigos de producto enviados deben existir en Inventory.InventoryProduct para considerarse válidos; El producto debe tener asociado un PackagingUnitId válido en Inventory.PackagingUnit para enriquecer la unidad de consumo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Códigos de resultado: 000 = éxito, 999 = error de validación de negocio por ítem, 888 = excepción no controlada; Toda línea procesada exitosamente queda con ProductId>0, ProductDescription y ConsumptionUnit en formato ''Code - Name''; La validación de cantidad numérica solo se evalúa sobre líneas que aún están en estado exitoso (CodeResult=000); El enriquecimiento de unidad de consumo nunca se aplica sobre líneas marcadas con error; Ante excepción, la respuesta nunca contiene resultados parciales: se reemplaza por una única fila de error', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de inventario; Producto de inventario; Unidad de empaque/presentación; Importación masiva de solicitudes; Copiar-pegar de líneas de solicitud', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXml: Se inserta una fila por cada nodo InventoryRequestDetail del XML con CodeResult=000 y MessageResult=''Producto agregado correctamente'' como estado inicial; [UPDATE] @TableXml: Si el ProductCode no existe en Inventory.InventoryProduct (LEFT JOIN con p.Id IS NULL), se marca CodeResult=999 con mensaje ''El producto con código X no existe''; [UPDATE] @TableXml: Si la fila aún está en CodeResult=000 e ISNUMERIC(Quantity)=0, se marca CodeResult=999 con mensaje ''La cantidad asociada al producto X no es numérico''; [UPDATE] @TableXml: Para productos existentes se completa ProductId con InventoryProduct.Id y ProductDescription con ''Code - Name''; [UPDATE] @TableXml: Solo cuando CodeResult=000 se completa ConsumptionUnit con ''PackagingUnit.Code - PackagingUnit.Name'' a partir del PackagingUnitId del producto; [RETURN_RESULT] @TableXml: Se retorna SELECT * de la tabla temporal con las líneas validadas y enriquecidas; [DELETE] @TableXml: En el bloque CATCH se borra el contenido de la tabla temporal; [INSERT] @TableXml: En el bloque CATCH se inserta una sola fila con CodeResult=888 y MessageResult=ERROR_MESSAGE() y se retorna', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Producto no existe en Inventory.InventoryProduct (LEFT JOIN p.Id IS NULL) → Marca la línea con CodeResult=999 y mensaje de producto inexistente else Continúa el flujo de validaciones siguientes; si CodeResult=000 AND ISNUMERIC(Quantity)=0 → Marca la línea con CodeResult=999 y mensaje de cantidad no numérica else Mantiene CodeResult=000 y procede al enriquecimiento; si Ocurre una excepción en cualquier paso (TRY/CATCH) → Vacía la tabla temporal e inserta una única fila con CodeResult=888 y el mensaje de ERROR_MESSAGE() else Devuelve el resultado normal por ítem', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.PackagingUnit', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequests';
-- GO
