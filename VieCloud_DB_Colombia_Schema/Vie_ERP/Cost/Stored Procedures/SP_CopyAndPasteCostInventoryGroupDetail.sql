-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-03
-- Description:	Procedimiento que se encarga de el Copy & Paste de los productos desde el formulario Actividades de Costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CopyAndPasteCostInventoryGroupDetail] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		InventoryProductId INT,
		InventoryProductCode VARCHAR(500),
		InventoryProductName VARCHAR(500),
		MeasurementUnitId INT,
		MeasurementUnitCode VARCHAR(500),
		MeasurementUnitName VARCHAR(500),
		Quantity DECIMAL(24,6),
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(InventoryProductCode, Quantity)
			SELECT 
				t.x.value('InventoryProductCode[1]','varchar(500)') as InventoryProductCode,
				t.x.value('Quantity[1]','decimal(24,6)') as Quantity
			FROM @XmlObject.nodes('/Data/Row') t(x)

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Producto con Código ' + tXml.InventoryProductCode + ' no existe'
		FROM @TableXmlObject tXml
		LEFT JOIN Inventory.InventoryProduct ip ON tXml.InventoryProductCode = ip.Code
		WHERE ip.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'La Cantidad del Producto con Código ' + tXml.InventoryProductCode + ' no pueden ser igual o menor a 0'
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.Quantity <= 0

		UPDATE tXml
			SET tXml.StatusField = 0,
				tXml.InventoryProductId = ip.Id,
				tXml.InventoryProductCode = ip.Code,
				txml.InventoryProductName = ip.Name,
				tXml.MeasurementUnitId = imu.Id,
				tXml.MeasurementUnitCode = imu.Code,
				txml.MeasurementUnitName = imu.Name
		FROM @TableXmlObject tXml
		JOIN Inventory.InventoryProduct ip ON tXml.InventoryProductCode = ip.Code
		LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ip.MeasurementUnitId = imu.Id
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		InventoryProductId,
		InventoryProductCode,
		InventoryProductName,
		MeasurementUnitId,
		MeasurementUnitCode,
		MeasurementUnitName,
		Quantity,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de copiado y pegado masivo de productos de inventario en el formulario de Actividades de Costo. Recibe un listado de productos en formato XML (con código de producto y cantidad) y valida cada ítem: verifica que el código del producto exista en el catálogo maestro de inventario y que la cantidad sea mayor a cero. Para cada producto válido, resuelve y devuelve su identificador interno, nombre, unidad de medida (código y nombre) y cantidad; para los inválidos, retorna el mensaje de error correspondiente. Se utiliza para poblar de forma rápida los detalles de un grupo de costos de inventario copiando productos desde otro registro o plantilla.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Validar y enriquecer un listado de productos recibido vía XML para soportar la operación de copiar y pegar productos en el detalle de un grupo de costos de inventario, devolviendo resultados válidos o mensajes de error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar la estructura /Data/Row con nodos InventoryProductCode y Quantity; Debe existir el catálogo Inventory.InventoryProduct con códigos coincidentes para los productos a copiar', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven filas con StatusField distinto de 99 (procesadas: válidas con 0 o inválidas con 999); Un producto se considera válido únicamente si su Code existe en Inventory.InventoryProduct y su Quantity es estrictamente mayor a 0; Los registros válidos siempre se enriquecen con la unidad de medida asociada al producto vía MeasurementUnitId; Cualquier excepción capturada se reporta como una fila adicional con StatusField=999 incluyendo número de línea', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto de inventario; Unidad de medida; Grupo de costos de inventario; Copy & Paste de productos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableXmlObject: Devuelve el listado distinto de productos procesados (válidos con StatusField=0 e inválidos con StatusField=999), excluyendo los que quedaron en estado inicial 99; [RAISERROR] @TableXmlObject: Cuando ocurre una excepción en el TRY, inserta una fila con StatusField=999 y MessageField = ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Producto del XML no existe en Inventory.InventoryProduct (LEFT JOIN por Code y ip.Id IS NULL) → Marca StatusField=999 con mensaje ''El Producto con Código X no existe'' else Continúa validación de cantidad; si StatusField=99 AND Quantity <= 0 → Marca StatusField=999 con mensaje ''La Cantidad del Producto... no pueden ser igual o menor a 0'' else Continúa al enriquecimiento de datos; si StatusField=99 (registro aún válido tras validaciones) → Marca StatusField=0 y enriquece con Id/Code/Name del producto y de su unidad de medida', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostInventoryGroupDetail';
-- GO
