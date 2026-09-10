-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-08-27
-- Description:	Procedimiento que se encarga de el copyPaste de los detalles de la base del elemento de costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ImportDetailsToCostDistributionSecondaryBase] 
	@DistributionType As TINYINT,
	@MeasurementUnit AS TINYINT,
	@XmlListDistributionSecondaryBaseDetail AS XML,
	@XmlData AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado ya cargado que viene en el xml
	DECLARE @TableXmlDistributionSecondaryBaseDetail TABLE
	(
		ProductionCenterId INT, 
		Quantity NUMERIC(18,4) DEFAULT (0)
	)

	--Tabla para almacenar los items a importar que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		Position INT, 
		StatusField BIT, 
		MessageField VARCHAR(MAX),
		ProductionCenterCode VARCHAR(200), 
		ProductionCenterId INT, 
		Quantity NUMERIC(18,4) DEFAULT (0) 
	)
	
	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField BIT, 
		MessageField VARCHAR(MAX), 
		-- DETAIL --
		ProductionCenterCodeName VARCHAR(200), 
		ProductionCenterId INT, 
		Quantity NUMERIC(18,4) DEFAULT (0)
	)

	BEGIN TRY

		INSERT INTO @TableXmlDistributionSecondaryBaseDetail (ProductionCenterId, Quantity)
			SELECT 
				t.x.value('ProductionCenterId[1]','int') as ProductionCenterId,
				t.x.value('Quantity[1]','decimal(18,4)') as Quantity
			FROM @XmlListDistributionSecondaryBaseDetail.nodes('/ListDistributionSecondaryBaseDetail/CostDistributionSecondaryBaseDetail') t(x)

		INSERT INTO @TableXmlObject (Position, StatusField, MessageField, ProductionCenterCode, Quantity)
			SELECT 
				t.x.value('Position[1]','int') as Position,
				t.x.value('StatusField[1]','bit') as StatusField,
				t.x.value('MessageField[1]','varchar(max)') as MessageField,
				t.x.value('ProductionCenterCode[1]','varchar(200)') as ProductionCenterCode,
				t.x.value('Quantity[1]','decimal(18,4)') as Quantity
			FROM @XmlData.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/

		UPDATE t
			SET t.StatusField = IIF(cpc.Id IS NULL, 0, t.StatusField),
				t.MessageField = IIF(cpc.Id IS NULL, 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' no tiene un Centro de Producción Operativo Válido', t.MessageField),
				t.ProductionCenterId = cpc.Id,
				t.ProductionCenterCode = CONCAT(cpc.Code, ' - ', cpc.Name)
		FROM @TableXmlObject t
		LEFT JOIN Cost.CostProductionCenter cpc ON t.ProductionCenterCode = cpc.Code AND cpc.CenterType = 1
		WHERE t.StatusField = 1

		UPDATE t
			SET t.StatusField = 0,
				t.MessageField = 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' no tiene un ' + IIF(@MeasurementUnit = 1, '% Distribución', 'Valor') + ' Válido'
		FROM @TableXmlObject t
		WHERE t.StatusField = 1
			AND @DistributionType = 2
			AND t.Quantity <= 0

		UPDATE t
			SET t.StatusField = 0,
				t.MessageField = 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' ya se encuentra agregado'
		FROM @TableXmlObject t
		JOIN @TableXmlDistributionSecondaryBaseDetail tdbd ON t.ProductionCenterId = tdbd.ProductionCenterId
		WHERE t.StatusField = 1

		UPDATE t
			SET t.StatusField = 0,
				t.MessageField = 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' se encuentra duplicado en la lista copiada'
		FROM @TableXmlObject t
		JOIN @TableXmlObject tdbd ON t.ProductionCenterId = tdbd.ProductionCenterId
		WHERE t.StatusField = 1
			AND CAST(t.Position AS VARCHAR(5)) <> tdbd.Position

		/************************************* INSERTAR ERRORES *************************************/

		INSERT INTO @TableResult (StatusField, MessageField)
			SELECT StatusField, MessageField
			FROM @TableXmlObject
			WHERE StatusField = 0

		/************************************* INSERTAR VALIDOS *************************************/
		
		INSERT INTO @TableResult 
			(
				StatusField, MessageField, 
				ProductionCenterCodeName, ProductionCenterId, 
				Quantity
			)
			SELECT 
				StatusField, '' MessageField,
				ProductionCenterCode, ProductionCenterId,
				IIF(@DistributionType = 2, Quantity, 0) Quantity
			FROM @TableXmlObject
			WHERE StatusField = 1
				
	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (StatusField, MessageField)
		VALUES (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de importación masiva de detalles hacia la base secundaria de distribución de costos, utilizado para copiar y pegar renglones de centros de producción desde una fuente XML hacia una base de distribución secundaria de elementos de costo. Recibe dos parámetros de configuración (tipo de distribución y unidad de medida), una lista XML con los detalles ya existentes en la base y un XML con los nuevos registros a importar; valida cada registro contra la tabla de centros de producción operativos (CostProductionCenter), verifica que los valores o porcentajes de distribución sean correctos, detecta duplicados tanto en la lista copiada como en los detalles ya cargados, y retorna un resultado por fila indicando si fue aceptado o rechazado con su respectivo mensaje de error. Forma parte del módulo de costos y sirve para garantizar la integridad de la distribución secundaria de costos entre centros de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara los detalles importados (copy-paste) para la base secundaria de distribución de costos, verificando centros de producción operativos, cantidades y duplicidades, y retorna el resultado fila a fila.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Recibir un XML con la lista de detalles ya cargados y otro XML con las filas a importar en el formato esperado (/Data/Row y /ListDistributionSecondaryBaseDetail/CostDistributionSecondaryBaseDetail); El código del centro de producción debe corresponder a un registro existente en Cost.CostProductionCenter con CenterType = 1; Los parámetros DistributionType y MeasurementUnit deben venir informados para definir el tipo de validación de cantidad y el texto del mensaje', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aceptan centros de producción cuyo CenterType = 1 (operativos); No se permiten ProductionCenterId duplicados entre las filas importadas; No se permiten ProductionCenterId que ya estén presentes en el detalle ya cargado; Cuando DistributionType ≠ 2, la cantidad de salida siempre se fuerza a 0; Cuando DistributionType = 2, la cantidad debe ser estrictamente mayor a cero; El procedimiento no realiza cambios persistentes; solo retorna un set de resultados; Cualquier excepción se captura y se retorna como una fila con StatusField=0 y el mensaje de error con la línea', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción operativo; Distribución secundaria de costos; Base de distribución; Unidad de medida (porcentaje/valor); Importación masiva (copy-paste) de detalles', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Retorna una tabla con todas las filas inválidas (StatusField=0 con su mensaje) y luego las filas válidas (StatusField=1) con código-nombre del centro, su Id y la cantidad ajustada según DistributionType; [RETURN_RESULT] ResultSet: Si ocurre una excepción en el TRY, retorna una única fila con StatusField=0 y el mensaje de error junto al número de línea (ERROR_MESSAGE + ERROR_LINE)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Centro de producción no existe o no es de tipo operativo (CenterType=1) → Marca el registro como inválido con mensaje indicando que no tiene un Centro de Producción Operativo Válido; si DistributionType = 2 y Quantity <= 0 → Marca el registro como inválido indicando que el % Distribución (si MeasurementUnit=1) o Valor no es válido; si El ProductionCenterId del registro ya existe en la lista de detalles previamente cargada → Marca el registro como inválido con mensaje ''ya se encuentra agregado''; si Existen dos filas con el mismo ProductionCenterId pero distinta Position dentro del XML importado → Marca el registro como inválido con mensaje ''se encuentra duplicado en la lista copiada''; si DistributionType = 2 al armar la salida válida → Conserva la Quantity capturada else Asigna Quantity = 0', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionSecondaryBase';
-- GO
