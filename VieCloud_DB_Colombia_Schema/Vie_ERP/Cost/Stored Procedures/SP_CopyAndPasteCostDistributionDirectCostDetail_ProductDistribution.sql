-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-12-13
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la distribucion de los elementos del costo
-- =============================================
CREATE   PROCEDURE [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution]
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	DECLARE @GeneralExpenseId INT,
			@OfficialLegalBookId INT

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		[ProductionCenterId] [int],
		[ProductionCenterCodeName] [varchar](500),
		[MainAccountId] [int],
		[MainAccountNumberName] [varchar](500),
		[CostCenterId] [int],
		[CostCenterCodeName] [varchar](500),
		[MeasurementUnitId] [int],
		[MeasurementUnitCodeName] [varchar](500),
		[AllowEditCostValue] [bit],
		[ThirdPartyId] [int],
		[ThirdPartyNitName] [varchar](500),
		[HandlesThirdParty] [bit],
		[JobTitle] [varchar](50),
		[PositionId] [int],
		[PositionCodeName] [varchar](500),
		[HoursManpowerText] [varchar](50),
		[HoursManpower] [decimal](18, 2),
		[ManpowerHoursContractedText] [varchar](50),
		[ManpowerHoursContracted] [decimal](18, 2),
		[ProductId] [int],
		[ProductCodeName] [varchar](500),
		[Percentage] [numeric](18, 4),
		[CountText] [varchar](50),
		[Count] [numeric](18, 2),		
		[CostValueText] [varchar](50),
		[CostValue] [decimal](18, 2),
		[BaseValue] [decimal](18, 2),
		[IvaValue] [decimal](18, 2),
		[ValueText] [varchar](50),
		[Value] [numeric](18, 2),
		[Nature] [bit],
		[NatureText] [varchar](100),
		[EmployeeId] [int],
		[EmployeeNitName] [varchar](500),
		[Observation] [varchar](500),
		[RetentionId] [int],
		[BaseRetention] [numeric](18, 2),
		[InvoicedValue] [numeric](18, 2),
		[PayrollConcept] [varchar](500),
		[ProcessDate] [Datetime],
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

	/************************************* ASIGNACIÓN ************************************/
		SELECT 
			@GeneralExpenseId = t.x.value('GeneralExpenseId[1]','int')
		FROM @XmlObject.nodes('/Data') t(x)

		SELECT @OfficialLegalBookId = Id
		FROM GeneralLedger.LegalBook WITH (NOLOCK)
		WHERE OfficialBook = 1

		INSERT INTO @TableXmlObject
			(ProductionCenterCodeName, MainAccountNumberName, CostCenterCodeName, ProductCodeName, CountText, ValueText)
			SELECT 
				t.x.value('Item0[1]','varchar(500)') as ProductionCenterCodeName,
				t.x.value('Item1[1]','varchar(500)') as MainAccountNumberName,
				t.x.value('Item2[1]','varchar(500)') as CostCenterCodeName,
				t.x.value('Item3[1]','varchar(500)') as ProductCodeName,
				1 CountText,
				REPLACE(REPLACE(t.x.value('Item4[1]','varchar(500)'), '.', ''), ',', '.') as ValueText
			FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES ************************************/

		-------------------------- Centro de producción -------------------------- 

		UPDATE tXml
			SET tXml.StatusField = IIF(cpc.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(cpc.Id IS NULL, 'El centro de producción con Código ' + tXml.ProductionCenterCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.ProductionCenterId = cpc.Id,
				txml.ProductionCenterCodeName = CONCAT(cpc.Code, ' - ', cpc.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON tXml.ProductionCenterCodeName = cpc.Code
		WHERE tXml.StatusField = 99

		-------------------------- Cuenta contable -------------------------- 

		UPDATE tXml
			SET tXml.StatusField = IIF(ma.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(ma.Id IS NULL, 'La cuenta contable ' + tXml.MainAccountNumberName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.MainAccountId = ma.Id,
				txml.MainAccountNumberName = CONCAT(ma.Number, ' - ', ma.Name),
				tXml.HandlesThirdParty = ma.HandlesThirdParty
		FROM @TableXmlObject tXml
		LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tXml.MainAccountNumberName = ma.Number AND ma.LegalBookId = @OfficialLegalBookId
		WHERE tXml.StatusField = 99

		-------------------------- Centro de costo -------------------------- 

		UPDATE tXml
			SET tXml.StatusField = IIF(cac.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(cac.Id IS NULL, 'El centro de costo ' + tXml.CostCenterCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.CostCenterId = cac.Id,
				txml.CostCenterCodeName = CONCAT(cac.Code, ' - ', cac.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.CostCenter cac ON tXml.CostCenterCodeName = cac.Code
		WHERE tXml.StatusField = 99

		-------------------------- Producto -------------------------- 

		UPDATE tXml
			SET tXml.StatusField = IIF(iprod.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(iprod.Id IS NULL, 'El producto ' + tXml.ProductCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.ProductId = iprod.Id,
				txml.ProductCodeName = CONCAT(iprod.Code, ' - ', iprod.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Inventory.InventoryProduct iprod ON tXml.ProductCodeName = iprod.Code
		WHERE tXml.StatusField = 99

		-------------------------- Valor -------------------------- 

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.ValueText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.ValueText AS DECIMAL(18, 2)) IS NULL, 'El valor ' + tXml.ValueText+ ' no es válido', tXml.MessageField),
				------------------------------
				tXml.[Value] = TRY_CAST(tXml.ValueText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = CONCAT('El centro de producción ', tXml.ProductionCenterCodeName, ' con la cuenta contable ', tXml.MainAccountNumberName, ' y centro de costo ', tXml.CostCenterCodeName, ' no se encuentra parametrizado en el elemento del costo seleccionado')
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostDistributionBase cdb ON @GeneralExpenseId = cdb.GeneralExpenseId
		LEFT JOIN cost.CostDistributionBaseDetail cdbd 
			ON  cdb.Id = cdbd.DistributionBaseId
				AND tXml.ProductionCenterId = cdbd.ProductionCenterId
				AND tXml.MainAccountId = cdbd.MainAccountId
				AND ISNULL(tXml.CostCenterId, 0) = ISNULL(cdbd.CostCenterId, 0)
		WHERE tXml.StatusField = 99 AND cdbd.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 0,
				--tXml.MeasurementUnitId = 41,
				tXml.CostValue = tXml.Value,
				tXml.[Count] = 1
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		ProductionCenterId,
		ProductionCenterCodeName,
		MainAccountId,
		MainAccountNumberName,
		CostCenterId,
		CostCenterCodeName,
		MeasurementUnitId,
		MeasurementUnitCodeName,
		ThirdPartyId,
		ThirdPartyNitName,
		HandlesThirdParty,
		JobTitle,
		PositionId,
		PositionCodeName,
		HoursManpower,
		ManpowerHoursContracted,
		ProductId,
		ProductCodeName,
		IIF([Percentage] IS NULL, 0, [Percentage]) Percentage,
		[Count],
		CostValue,
		BaseValue,
		IvaValue,
		Value,
		Nature,
		NatureText,
		EmployeeId,
		EmployeeNitName,
		Observation,
		RetentionId,
		BaseRetention,
		InvoicedValue,
		PayrollConcept,
		ProcessDate,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar masivamente detalles de la distribución de costos directos por producto, a partir de un archivo XML con filas de datos. Valida que cada fila contenga un centro de producción, cuenta contable (del libro oficial), centro de costo, producto e importe válidos, marcando con error los registros que no existan en las tablas maestras correspondientes. Si todas las validaciones son correctas, inserta los detalles en la distribución de costos de la distribución del período. Se utiliza en el módulo de costeo para agilizar la carga masiva de distribuciones de costos directos asociados a productos, evitando el ingreso manual registro por registro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece filas de distribución de costos por producto pegadas desde un XML (copy & paste), resolviendo IDs por código y marcando errores fila por fila para devolver el resultado al cliente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Data con GeneralExpenseId y nodos /Data/Row con Item0..Item4 (centro de producción, cuenta contable, centro de costo, producto y valor); Debe existir un libro legal oficial (OfficialBook = 1) en GeneralLedger.LegalBook; El elemento del costo (GeneralExpenseId) debe tener parametrizada una base de distribución en Cost.CostDistributionBase con detalles en CostDistributionBaseDetail para la combinación centro de producción/cuenta/centro de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una fila con error (StatusField=999) no es modificada por validaciones posteriores; ISNULL del CostCenterId se compara contra ISNULL del CostCenterId del detalle (tratando NULL=0) al verificar parametrización; La cuenta contable se busca solo dentro del libro legal oficial (OfficialBook = 1); El resultado nunca incluye filas con StatusField = 99 (no procesadas); Para filas válidas, Count se fija en 1 y CostValue queda igual al Value parseado; Cualquier excepción en el flujo termina como una fila adicional de error (999) en lugar de propagar el error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos; Elemento del costo / gasto general; Centro de producción; Centro de costo; Cuenta contable (plan de cuentas / libro oficial); Producto de inventario; Manejo de terceros (HandlesThirdParty); Copy & Paste de detalles de distribución', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Por cada /Data/Row del XML se inserta una fila con los textos de centro de producción, cuenta, centro de costo, producto y valor (este último normalizado removiendo ''.'' y cambiando '','' por ''.''); [UPDATE] @TableXmlObject: Si el código del centro de producción no existe en Cost.CostProductionCenter → StatusField=999 y mensaje ''El centro de producción con Código X no existe''; si existe se asigna ProductionCenterId y se reformatea como ''Code - Name''; [UPDATE] @TableXmlObject: Si la cuenta contable no existe en GeneralLedger.MainAccounts para el libro oficial → StatusField=999 y mensaje ''La cuenta contable X no existe''; si existe se asigna MainAccountId, se reformatea y se copia HandlesThirdParty; [UPDATE] @TableXmlObject: Si el código del centro de costo no existe en Payroll.CostCenter → StatusField=999 y mensaje ''El centro de costo X no existe''; si existe se asigna CostCenterId y se reformatea como ''Code - Name''; [UPDATE] @TableXmlObject: Si el código del producto no existe en Inventory.InventoryProduct → StatusField=999 y mensaje ''El producto X no existe''; si existe se asigna ProductId y se reformatea como ''Code - Name''; [UPDATE] @TableXmlObject: Si TRY_CAST(ValueText AS DECIMAL(18,2)) es NULL → StatusField=999 y mensaje ''El valor X no es válido''; si es válido se asigna a Value; [UPDATE] @TableXmlObject: Si la combinación (ProductionCenterId, MainAccountId, CostCenterId) no existe en Cost.CostDistributionBaseDetail asociada al GeneralExpenseId vía Cost.CostDistributionBase → StatusField=999 con mensaje indicando que no está parametrizado en el elemento del costo seleccionado; [UPDATE] @TableXmlObject: Para las filas que pasan todas las validaciones (StatusField=99) se marca StatusField=0, se asigna CostValue = Value y Count = 1; [INSERT] @TableXmlObject: En el bloque CATCH se inserta una fila con StatusField=999 y MessageField que concatena ERROR_MESSAGE() y la línea del error; [RETURN_RESULT] RESULT: Devuelve SELECT DISTINCT de las filas validadas (excluyendo las que quedaron en StatusField=99) con sus IDs resueltos, valores y StatusField/MessageField para indicar éxito (0) o error (999)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cada validación se aplica solo cuando StatusField = 99 (fila aún sin error) → Se evalúa la regla y, si falla, se marca StatusField=999 deteniendo validaciones posteriores para esa fila else Las filas con StatusField=999 se omiten de las siguientes validaciones; si La combinación (ProductionCenter, MainAccount, CostCenter) no está en CostDistributionBaseDetail del GeneralExpenseId → Se marca como no parametrizada en el elemento del costo (StatusField=999); si StatusField sigue en 99 tras todas las validaciones → Se marca como válida (StatusField=0), CostValue=Value y Count=1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Inventory.InventoryProduct; Cost.CostDistributionBase; Cost.CostDistributionBaseDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution';
-- GO
