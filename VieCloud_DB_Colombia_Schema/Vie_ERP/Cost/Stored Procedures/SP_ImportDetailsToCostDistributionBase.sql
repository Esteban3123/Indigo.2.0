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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa y valida masivamente los detalles de la base de distribución de costos mediante datos enviados en formato XML. Recibe dos listados XML: uno con los registros ya existentes en la base de distribución y otro con los nuevos registros a importar (copiados desde otra fuente). Valida que cada registro tenga un Centro de Producción válido (contra Cost.CostProductionCenter), una Cuenta Contable válida que permita movimientos (contra GeneralLedger.MainAccounts), un Centro de Costo válido asociado al Centro de Producción cuando aplica (contra Payroll.CostCenter y Cost.CostProductionCenterCostCenter), y que no existan duplicados ni registros ya cargados. Retorna el resultado de cada fila con su estado (aprobado o rechazado) y el mensaje de error correspondiente, permitiendo al usuario identificar qué líneas pueden importarse a la base del elemento de costo y cuáles requieren corrección.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida e importa masivamente desde XML un listado de detalles para la base de distribución de un elemento de costo, devolviendo registros válidos e inválidos con sus mensajes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de datos debe seguir la estructura /Data/Row con Position, StatusField, MessageField, ProductionCenterCode, MainAccountNumber, CostCenterCode y Quantity.; El XML del listado existente debe seguir la estructura /ListDistributionBaseDetail/CostDistributionBaseDetail.; Las cuentas contables consideradas válidas deben tener AllowsMovement = 1.; Solo se validan los registros que ingresan con StatusField = 1.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los registros válidos no incluyen centro de costo si la cuenta contable no maneja centros de costo (HandlesCostCenter <> 1).; La cantidad solo se conserva cuando DistributionType = 2; en otros casos siempre se almacena 0.; No se permiten duplicados ni dentro del XML de entrada ni respecto al listado ya cargado (clave: ProductionCenterId + MainAccountId + CostCenterId).; Solo se aceptan cuentas contables que permiten movimiento (AllowsMovement=1).; Cualquier excepción produce una fila de error en el resultado en lugar de propagar el error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de Producción; Cuenta contable; Centro de Costo; Base de distribución de costos; Elemento de costo; Tipo de distribución; Unidad de medida (% Distribución / Valor)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Cuando un registro queda con StatusField = 0 (inválido), se inserta en el resultado solo con StatusField y MessageField de error.; [INSERT] @TableResult: Cuando StatusField = 1, se inserta el registro válido con sus códigos resueltos; si HandlesCostCenter <> 1 se omite el centro de costo (NULL); si DistributionType <> 2 la cantidad se fuerza a 0.; [INSERT] @TableResult: En CATCH se inserta un registro con StatusField=0 y MessageField = ERROR_MESSAGE() + línea del error.; [RETURN_RESULT] @TableResult: Al final se retorna SELECT * FROM @TableResult con los resultados consolidados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El ProductionCenterCode no existe en Cost.CostProductionCenter → Se marca StatusField=0 con mensaje ''no tiene un Centro de Producción Válido''; si El MainAccountNumber no existe en GeneralLedger.MainAccounts con AllowsMovement=1 → Se marca StatusField=0 con mensaje ''no tiene una Cuenta contable Válida''; si HandlesCostCenter = 1 y el CostCenterCode no existe en Payroll.CostCenter → Se marca StatusField=0 con mensaje ''no tiene un Centro de Costo Válido''; si HandlesCostCenter = 1 y no existe relación en Cost.CostProductionCenterCostCenter entre el centro de producción y el centro de costo → Se marca StatusField=0 con mensaje ''no tiene un Centro de Costo asociado al Centro de Producción''; si DistributionType = 2 y Quantity <= 0 → Se marca StatusField=0 con mensaje indicando ''% Distribución'' (si MeasurementUnit=1) o ''Valor'' inválido; si El registro coincide (ProductionCenterId, MainAccountId, CostCenterId) con un detalle ya cargado en la lista existente → Se marca StatusField=0 con mensaje ''ya se encuentra agregado''; si El registro está duplicado dentro del propio XML de entrada → Se marca StatusField=0 con mensaje ''se encuentra duplicado en la lista copiada''; si HandlesCostCenter = 1 al insertar válidos → Se conserva CostCenterCode/Id else Se asigna NULL al centro de costo; si DistributionType = 2 al insertar válidos → Se conserva la Quantity ingresada else Quantity se fuerza a 0', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Cost.CostProductionCenterCostCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportDetailsToCostDistributionBase';
-- GO
