-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-03
-- Description:	Procedimiento que se encarga de el Copy & Paste de los centros de producción desde el formulario Actividades de Costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostActivityCopyAndPasteProductionCenter] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CostProductionCenterId INT,
		CostProductionCenterCode VARCHAR(500),
		CostProductionCenterName VARCHAR(500),
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(CostProductionCenterCode)
			SELECT 
				t.x.value('CostProductionCenterCode[1]','varchar(500)') as CostProductionCenterCode
			FROM @XmlObject.nodes('/Data/Row') t(x)

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Centro de Producción con Código ' + tXml.CostProductionCenterCode + ' no existe'
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostProductionCenter cpc ON tXml.CostProductionCenterCode = cpc.Code
		WHERE cpc.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Centro de Producción con Código ' + tXml.CostProductionCenterCode + ' es de tipo ' + 
					CASE cpc.CenterType
						WHEN 2 THEN 'Administrativo'
						WHEN 3 THEN 'Logístico'
						ELSE 'N/A'
					END
		FROM @TableXmlObject tXml
		JOIN Cost.CostProductionCenter cpc ON tXml.CostProductionCenterCode = cpc.Code
		WHERE tXml.StatusField = 99 AND cpc.CenterType <> 1

		UPDATE tXml
			SET tXml.StatusField = 0,
				tXml.CostProductionCenterId = cpc.Id,
				tXml.CostProductionCenterCode = cpc.Code,
				txml.CostProductionCenterName = cpc.Name
		FROM @TableXmlObject tXml
		JOIN Cost.CostProductionCenter cpc ON tXml.CostProductionCenterCode = cpc.Code
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		CostProductionCenterId,
		CostProductionCenterCode,
		CostProductionCenterName,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END