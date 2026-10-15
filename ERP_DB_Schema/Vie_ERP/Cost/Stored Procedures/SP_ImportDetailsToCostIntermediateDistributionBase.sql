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