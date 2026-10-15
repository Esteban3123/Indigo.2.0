-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-08-27
-- Description:	Procedimiento que se encarga de el copyPaste de los detalles de la base del elemento de costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ImportDetailsToCostDistributionBase] 
	@DistributionType As TINYINT,
	@MeasurementUnit AS TINYINT,
	@XmlListDistributionBaseDetail AS XML,
	@XmlData AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado ya cargado que viene en el xml
	DECLARE @TableXmlDistributionBaseDetail TABLE
	(
		ProductionCenterId INT, 
		MainAccountId INT, 
		CostCenterId INT, 
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
		MainAccountNumber VARCHAR(200), 
		MainAccountId INT, 
		HandlesCostCenter INT, 
		CostCenterCode VARCHAR(200), 
		CostCenterId INT, 
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
		MainAccountNumberName VARCHAR(200), 
		MainAccountId INT, 
		CostCenterCodeName VARCHAR(200), 
		CostCenterId INT, 
		Quantity NUMERIC(18,4) DEFAULT (0)
	)

	BEGIN TRY

		INSERT INTO @TableXmlDistributionBaseDetail (ProductionCenterId, MainAccountId, CostCenterId, Quantity)
			SELECT 
				t.x.value('ProductionCenterId[1]','int') as ProductionCenterId,
				t.x.value('MainAccountId[1]','int') as MainAccountId,
				t.x.value('CostCenterId[1]','int') as CostCenterId,
				t.x.value('Quantity[1]','decimal(18,4)') as Quantity
			FROM @XmlListDistributionBaseDetail.nodes('/ListDistributionBaseDetail/CostDistributionBaseDetail') t(x)

		INSERT INTO @TableXmlObject (Position, StatusField, MessageField, ProductionCenterCode, MainAccountNumber, CostCenterCode, Quantity)
			SELECT 
				t.x.value('Position[1]','int') as Position,
				t.x.value('StatusField[1]','bit') as StatusField,
				t.x.value('MessageField[1]','varchar(max)') as MessageField,
				t.x.value('ProductionCenterCode[1]','varchar(200)') as ProductionCenterCode,
				t.x.value('MainAccountNumber[1]','varchar(200)') as MainAccountNumber,
				t.x.value('CostCenterCode[1]','varchar(200)') as CostCenterCode,
				t.x.value('Quantity[1]','decimal(18,4)') as Quantity
			FROM @XmlData.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/

		UPDATE t
			SET t.StatusField = IIF(cpc.Id IS NULL, 0, t.StatusField),
				t.MessageField = IIF(cpc.Id IS NULL, 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' no tiene un Centro de Producción Válido', t.MessageField),
				t.ProductionCenterId = cpc.Id,
				t.ProductionCenterCode = CONCAT(cpc.Code, ' - ', cpc.Name)
		FROM @TableXmlObject t
		LEFT JOIN Cost.CostProductionCenter cpc ON t.ProductionCenterCode = cpc.Code		
		WHERE t.StatusField = 1

		UPDATE t
			SET t.StatusField = IIF(ma.Id IS NULL, 0, t.StatusField),
				t.MessageField = IIF(ma.Id IS NULL, 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' no tiene una Cuenta contable Válida', t.MessageField),
				t.MainAccountId = ma.Id,
				t.MainAccountNumber = CONCAT(ma.Number, ' - ', ma.Name),
				t.HandlesCostCenter = ma.HandlesCostCenter
		FROM @TableXmlObject t
		LEFT JOIN GeneralLedger.MainAccounts ma ON t.MainAccountNumber = ma.Number AND ma.AllowsMovement = 1
		WHERE t.StatusField = 1

		UPDATE t
			SET t.StatusField = IIF(cc.Id IS NULL, 0, t.StatusField),
				t.MessageField = IIF(cc.Id IS NULL, 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' no tiene un Centro de Costo Válido', t.MessageField),
				t.CostCenterId = cc.Id,
				t.CostCenterCode = CONCAT(cc.Code, ' - ', cc.Name)
		FROM @TableXmlObject t
		LEFT JOIN Payroll.CostCenter cc ON t.CostCenterCode = cc.Code
		WHERE t.StatusField = 1
			AND t.HandlesCostCenter = 1

		UPDATE t
			SET t.StatusField = 0,
				t.MessageField = 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' no tiene un Centro de Costo asociado al Centro de Producción'
		FROM @TableXmlObject t
		LEFT JOIN Cost.CostProductionCenterCostCenter cpccc ON t.ProductionCenterId = cpccc.ProductionCenterId AND t.CostCenterId = cpccc.CostCenterId
		WHERE t.StatusField = 1
			AND t.HandlesCostCenter = 1
			AND cpccc.Id IS NULL

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
		JOIN @TableXmlDistributionBaseDetail tdbd ON t.ProductionCenterId = tdbd.ProductionCenterId
			AND t.MainAccountId = tdbd.MainAccountId
			AND ISNULL(t.CostCenterId, 0) = ISNULL(tdbd.CostCenterId, 0)
		WHERE t.StatusField = 1

		UPDATE t
			SET t.StatusField = 0,
				t.MessageField = 'El registro ' + CAST(t.Position AS VARCHAR(5)) + ' se encuentra duplicado en la lista copiada'
		FROM @TableXmlObject t
		JOIN @TableXmlObject tdbd ON t.ProductionCenterId = tdbd.ProductionCenterId
			AND t.MainAccountId = tdbd.MainAccountId
			AND ISNULL(t.CostCenterId, 0) = ISNULL(tdbd.CostCenterId, 0)
		WHERE t.StatusField = 1
			AND CAST(t.Position AS VARCHAR(5)) <> tdbd.Position

		/************************************* INSERTAR ERRORES *************************************/

		INSERT INTO @TableResult 
			(
				StatusField, MessageField
			)
			SELECT StatusField, MessageField
			FROM @TableXmlObject
			WHERE StatusField = 0

		/************************************* INSERTAR VALIDOS *************************************/
		
		INSERT INTO @TableResult 
			(
				StatusField, MessageField, 
				ProductionCenterCodeName, ProductionCenterId, 
				MainAccountNumberName, MainAccountId, 
				CostCenterCodeName, CostCenterId, 
				Quantity
			)
			SELECT 
				StatusField, '' MessageField,
				ProductionCenterCode, ProductionCenterId,
				MainAccountNumber, MainAccountId,
				IIF(HandlesCostCenter = 1, CostCenterCode, NULL), IIF(HandlesCostCenter = 1, CostCenterId, NULL),
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