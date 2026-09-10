-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-03
-- Description:	Procedimiento que se encarga de el Copy & Paste de los productos desde el formulario Actividades de Costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostActivityCopyAndPasteInventory] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CostActivityStepId INT,
		CostInventoryGroupId INT,
		CostInventoryGroupCode VARCHAR(500),
		CostInventoryGroupName VARCHAR(500),
		MeasurementUnitCode VARCHAR(500),
		MeasurementUnitName VARCHAR(500),
		Quantity DECIMAL(24,6),
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(CostActivityStepId, CostInventoryGroupCode, Quantity)
			SELECT 
				t.x.value('CostActivityStepId[1]','int') as CostActivityStepId,
				t.x.value('CostInventoryGroupCode[1]','varchar(500)') as CostInventoryGroupCode,
				t.x.value('Quantity[1]','decimal(24,6)') as Quantity
			FROM @XmlObject.nodes('/Data/Row') t(x)

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Grupo de Productos con Código ' + tXml.CostInventoryGroupCode + ' no existe'
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostInventoryGroup ip ON tXml.CostInventoryGroupCode = ip.Code
		WHERE ip.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'La Cantidad del Grupo de Productos con Código ' + tXml.CostInventoryGroupCode + ' no pueden ser igual o menor a 0'
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.Quantity <= 0

		UPDATE tXml
			SET tXml.StatusField = 0,
				tXml.CostInventoryGroupId = ip.Id,
				tXml.CostInventoryGroupCode = ip.Code,
				txml.CostInventoryGroupName = ip.Name,
				tXml.MeasurementUnitCode = imu.Code,
				txml.MeasurementUnitName = imu.Name
		FROM @TableXmlObject tXml
		JOIN Cost.CostInventoryGroup ip ON tXml.CostInventoryGroupCode = ip.Code
		JOIN Inventory.InventoryMeasurementUnit imu ON ip.InventoryMeasurementUnitId = imu.Id
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		CostActivityStepId,
		CostInventoryGroupId,
		CostInventoryGroupCode,
		CostInventoryGroupName,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona la operación de copiar y pegar productos (grupos de inventario) desde el formulario de Actividades de Costo. Recibe un listado de ítems en formato XML con el paso de actividad, el código del grupo de productos y la cantidad asociada, luego valida que cada grupo de productos exista en el catálogo de grupos de inventario de costos y que la cantidad sea mayor a cero. Para cada ítem válido, resuelve y enriquece la información con el nombre del grupo, la unidad de medida (código y nombre) consultada desde el catálogo de unidades de medida de inventario, devolviendo el resultado con un estado de éxito o un mensaje de error por cada fila procesada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece un listado de grupos de productos pegados desde el formulario de actividades de costo, devolviendo cada fila con sus datos resueltos (grupo y unidad de medida) o con un mensaje de error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /Data/Row con CostActivityStepId, CostInventoryGroupCode y Quantity; Los códigos de grupo de inventario referenciados deben existir en Cost.CostInventoryGroup para poder validarse; Cada grupo de inventario válido debe tener una unidad de medida asociada en Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas se inicializan con StatusField=99 (pendiente) y solo cambian a 0 (válido) o 999 (error); Una fila marcada como error (999) no es reevaluada por validaciones posteriores; Se descartan del resultado las filas que sigan en estado pendiente (StatusField=99); Cualquier excepción se captura y se reporta como una fila de error con el mensaje y la línea del error; La cantidad debe ser estrictamente positiva; Cada grupo de inventario válido debe tener asociada una unidad de medida en Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Actividades de costo; Grupo de productos/inventario; Unidad de medida de inventario; Copy & Paste de productos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve el conjunto distinto de filas procesadas cuyo StatusField sea distinto de 99 (válidas con 0 o erróneas con 999), incluyendo datos resueltos del grupo y unidad de medida o mensaje de error; [RAISERROR] ?: Cuando ocurre una excepción en el TRY, se inserta una fila con StatusField=999 y MessageField con ERROR_MESSAGE() y ERROR_LINE() para reportarla en el resultado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El código del grupo de productos no existe en Cost.CostInventoryGroup → Marca la fila como error (StatusField=999) con mensaje indicando que el grupo no existe; si La cantidad es menor o igual a 0 y la fila aún no tiene error → Marca la fila como error (StatusField=999) con mensaje indicando que la cantidad no puede ser <= 0; si La fila pasó las validaciones (StatusField=99) → Resuelve y completa los datos del grupo de inventario y su unidad de medida, marcando StatusField=0 (válido)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostInventoryGroup; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteInventory';
-- GO
