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