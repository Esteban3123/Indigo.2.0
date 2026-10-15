-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-03
-- Description:	Procedimiento que se encarga de el Copy & Paste de los activos fijos desde el formulario Actividades de Costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostActivityCopyAndPasteFixedAsset] 
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
		FixedAssetItemId INT,
		FixedAssetItemCode VARCHAR(500),
		FixedAssetItemName VARCHAR(500),
		Hours DECIMAL(24,6),
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(CostActivityStepId, FixedAssetItemCode, Hours)
			SELECT 
				t.x.value('CostActivityStepId[1]','int') as CostActivityStepId,
				t.x.value('FixedAssetItemCode[1]','varchar(500)') as FixedAssetItemCode,
				t.x.value('Hours[1]','decimal(24,6)') as Hours
			FROM @XmlObject.nodes('/Data/Row') t(x)

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Artículo con Código ' + tXml.FixedAssetItemCode + ' no existe'
		FROM @TableXmlObject tXml
		LEFT JOIN FixedAsset.FixedAssetItem fai ON tXml.FixedAssetItemCode = fai.Code
		WHERE fai.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'Las Horas del Artículo con Código ' + tXml.FixedAssetItemCode + ' no pueden ser igual o menor a 0'
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.Hours <= 0

		UPDATE tXml
			SET tXml.StatusField = 0,
				tXml.FixedAssetItemId = fai.Id,
				tXml.FixedAssetItemCode = fai.Code,
				txml.FixedAssetItemName = fai.Description
		FROM @TableXmlObject tXml
		JOIN FixedAsset.FixedAssetItem fai ON tXml.FixedAssetItemCode = fai.Code
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		CostActivityStepId,
		FixedAssetItemId,
		FixedAssetItemCode,
		FixedAssetItemName,
		Hours,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END