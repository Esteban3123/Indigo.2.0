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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la operación de copiar y pegar activos fijos dentro del formulario de Actividades de Costo. Recibe un listado de ítems en formato XML, cada uno con el paso de actividad de costo, el código del activo fijo y las horas asignadas. Valida que cada activo fijo exista en el catálogo de activos fijos y que las horas indicadas sean mayores a cero, devolviendo el resultado de cada fila con su estado (éxito o error) y mensaje descriptivo. Permite registrar masivamente la asignación de activos fijos a pasos de actividades de costo, facilitando la gestión de costos por uso de bienes de capital.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece un listado XML de activos fijos pegados en el formulario de Actividades de Costo, devolviendo cada fila con su estado de validación e identificadores resueltos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe respetar la estructura /Data/Row con nodos CostActivityStepId, FixedAssetItemCode y Hours; Los códigos de activo fijo deben existir previamente en FixedAsset.FixedAssetItem', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las filas inician en StatusField=99 y sólo se devuelven cuando pasaron a 0 (válida) o 999 (error); Las horas registradas para un activo fijo deben ser estrictamente mayores a 0; Sólo se aceptan códigos de activo fijo presentes en el catálogo maestro; Las validaciones son excluyentes: un error previo (999) impide que se sobrescriba con validaciones posteriores; El procedimiento no modifica tablas persistentes; sólo valida y devuelve resultados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Actividad de costo; Paso de actividad de costo; Horas de uso de activo fijo; Copy & Paste de activos fijos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Por cada nodo /Data/Row del XML se inserta una fila con StatusField inicial 99 (pendiente de validar); [UPDATE] @TableXmlObject: Cuando el código no existe en FixedAsset.FixedAssetItem, se marca StatusField=999 con mensaje ''El Artículo con Código X no existe''; [UPDATE] @TableXmlObject: Cuando StatusField=99 y Hours<=0, se marca StatusField=999 con mensaje indicando que las horas no pueden ser iguales o menores a 0; [UPDATE] @TableXmlObject: Cuando StatusField=99 (sin errores previos) y el código existe, se marca StatusField=0 y se completan Id, Code y Description del activo fijo; [INSERT] @TableXmlObject: Si ocurre una excepción en el TRY, se inserta una fila con StatusField=999 y MessageField con el mensaje y línea del error; [RETURN_RESULT] @TableXmlObject: Se devuelven sólo las filas con StatusField<>99, es decir, las que finalizaron como válidas (0) o con error (999)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El código de activo fijo no existe en el catálogo maestro → Marca la fila como error (999) con mensaje de inexistencia; si La fila aún está pendiente (StatusField=99) y Hours<=0 → Marca la fila como error (999) por horas inválidas; si La fila aún está pendiente (StatusField=99) y el código existe → Marca la fila como válida (0) y enriquece con Id, Code y Description del activo fijo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetItem', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteFixedAsset';
-- GO
