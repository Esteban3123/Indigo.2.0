-- =============================================
-- Author:		Diego A. Roldan Lozano
-- Create date: 2025-07-02
-- Description:	Procedimiento que se encarga de el copyPaste de los detalles de la base del elemento de costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ImportDetailsToCostIntermediateDistributionBase] 
	@DistributionType As TINYINT,
	@MeasurementUnit AS TINYINT,
	@XmlListIntermediateDistributionBaseDetail AS XML,
	@XmlData AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado ya cargado que viene en el xml
	DECLARE @TableXmlIntermediateDistributionBaseDetail TABLE
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

		INSERT INTO @TableXmlIntermediateDistributionBaseDetail (ProductionCenterId, Quantity)
			SELECT 
				t.x.value('ProductionCenterId[1]','int') as ProductionCenterId,
				t.x.value('Quantity[1]','decimal(18,4)') as Quantity
			FROM @XmlListIntermediateDistributionBaseDetail.nodes('/ListIntermediateDistributionBaseDetail/CostIntermediateDistributionBaseDetail') t(x)

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
		JOIN @TableXmlIntermediateDistributionBaseDetail tdbd ON t.ProductionCenterId = tdbd.ProductionCenterId
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa y valida masivamente los detalles de la base de distribución intermedia de costos, a partir de datos enviados en formato XML. Recibe una lista de centros de producción con sus cantidades o porcentajes de distribución, valida que cada centro sea operativo y exista en el catálogo de centros de producción (Cost.CostProductionCenter), y verifica que no haya duplicados ni registros ya cargados previamente. Retorna un listado de resultados indicando qué registros son válidos (listos para ser insertados en la base intermedia de distribución) y cuáles fallaron, con el mensaje de error correspondiente. Se usa en el módulo de costos hospitalarios para apoyar la operación de copiar y pegar detalles entre bases de distribución intermedia, controlando el tipo de distribución (fija o porcentual) y la unidad de medida aplicable.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Validar e importar (copiar/pegar) un listado de centros de producción con cantidades para alimentar el detalle de la base de distribución intermedia de un elemento de costo, devolviendo registros válidos y errores.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de la lista ya cargada debe seguir la estructura /ListIntermediateDistributionBaseDetail/CostIntermediateDistributionBaseDetail con ProductionCenterId y Quantity; El XML de datos a importar debe seguir la estructura /Data/Row con Position, StatusField, MessageField, ProductionCenterCode y Quantity; Los registros candidatos a validar deben venir con StatusField = 1 desde el XML de entrada; Debe existir el catálogo Cost.CostProductionCenter con centros marcados como CenterType = 1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran centros de producción con CenterType = 1 (operativos) como válidos; Los registros que ya existen en la base previamente cargada no se importan nuevamente; No se permiten códigos de centro de producción duplicados en una misma carga; Cuando el tipo de distribución no es 2, la cantidad importada se fuerza a 0; Cuando el tipo de distribución es 2, la cantidad debe ser estrictamente mayor a 0; Los errores se retornan en la misma tabla resultado junto con los registros válidos; Los errores capturados en CATCH se devuelven como un registro con StatusField=0 incluyendo el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de Producción Operativo; Distribución de costos; Base de distribución intermedia de costos; Porcentaje de distribución; Unidad de medida', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve una tabla con todos los registros: los inválidos (StatusField=0) con su mensaje de error y los válidos (StatusField=1) con código-nombre del centro, Id y cantidad ajustada según DistributionType; [RETURN_RESULT] @TableResult: Si ocurre una excepción, devuelve una única fila con StatusField=0 y el mensaje de error junto con la línea donde ocurrió', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Centro de producción no existe con CenterType=1 para el código ingresado → Marca el registro como inválido con mensaje de Centro de Producción Operativo no válido; si DistributionType = 2 y Quantity <= 0 → Marca el registro como inválido indicando ''% Distribución'' o ''Valor'' no válido según MeasurementUnit; si ProductionCenterId del registro ya existe en la lista previamente cargada → Marca el registro como inválido por estar ya agregado; si ProductionCenterId duplicado dentro de la misma lista copiada (distinta Position) → Marca el registro como duplicado en la lista copiada; si DistributionType = 2 al insertar registros válidos → Conserva la Quantity del XML else Asigna Quantity = 0; si MeasurementUnit = 1 al construir mensaje de cantidad inválida → Indica ''% Distribución'' else Indica ''Valor''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostIntermediateDistributionBase';
-- GO
