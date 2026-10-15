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