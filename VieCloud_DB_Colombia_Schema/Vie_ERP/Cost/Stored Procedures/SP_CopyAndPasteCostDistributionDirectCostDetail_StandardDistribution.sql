-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-12-13
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la distribucion de los elementos del costo para el tipo de distribucion estandar
-- =============================================
CREATE   PROCEDURE [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution]
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
		[HandlesCostCenter] [bit],
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
			(ProductionCenterCodeName, MainAccountNumberName, CostCenterCodeName, MeasurementUnitCodeName, CountText, CostValueText)
			SELECT 
				t.x.value('Item0[1]','varchar(500)') as ProductionCenterCodeName,
				t.x.value('Item1[1]','varchar(500)') as MainAccountNumberName,
				t.x.value('Item2[1]','varchar(500)') as CostCenterCodeName,
				t.x.value('Item3[1]','varchar(500)') as MeasurementUnitCodeName,
				REPLACE(REPLACE(t.x.value('Item4[1]','varchar(500)'), '.', ''), ',', '.') as CountText,
				REPLACE(REPLACE(t.x.value('Item5[1]','varchar(500)'), '.', ''), ',', '.') as CostValueText
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
				tXml.HandlesThirdParty = ma.HandlesThirdParty,
				txml.HandlesCostCenter = ma.HandlesCostCenter
		FROM @TableXmlObject tXml
		LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tXml.MainAccountNumberName = ma.Number AND ma.LegalBookId = @OfficialLegalBookId
		WHERE tXml.StatusField = 99

		-------------------------- Centro de costo  --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(cac.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(cac.Id IS NULL, 'El centro de costo ' + tXml.CostCenterCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.CostCenterId = cac.Id,
				txml.CostCenterCodeName = CONCAT(cac.Code, ' - ', cac.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.CostCenter cac ON tXml.CostCenterCodeName = cac.Code
		WHERE tXml.StatusField = 99 AND txml.HandlesCostCenter = 1

		-------------------------- Unidad de medida --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(imu.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(imu.Id IS NULL, 'La unidad de medida ' + tXml.MeasurementUnitCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.MeasurementUnitId = imu.Id,
				txml.MeasurementUnitCodeName = CONCAT(imu.Code, ' - ', imu.Name),
				txml.AllowEditCostValue = imu.AllowEditCostValue,
				tXml.CostValue = imu.CostValue
		FROM @TableXmlObject tXml
		LEFT JOIN Inventory.InventoryMeasurementUnit imu ON tXml.MeasurementUnitCodeName = imu.Code
		WHERE tXml.StatusField = 99

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = CONCAT('La unidad de medida ', tXml.MeasurementUnitCodeName, ' no se encuentra parametrizada en el elemento del costo seleccionado')
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostDistributionBase cdb ON @GeneralExpenseId = cdb.GeneralExpenseId
		LEFT JOIN cost.CostDistributionBaseMeasurementUnit cdbmu ON  cdb.Id = cdbmu.DistributionBaseId
		WHERE tXml.StatusField = 99 AND cdbmu.Id IS NULL 

		-------------------------- Cantidad --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.CountText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.CountText AS DECIMAL(18, 2)) IS NULL, 'La cantidad ' + tXml.CountText+ ' no es válida', tXml.MessageField),
				------------------------------
				tXml.[Count] = TRY_CAST(tXml.CountText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

		-------------------------- Puntos de valor --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.CostValueText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.CostValueText AS DECIMAL(18, 2)) IS NULL, 'Los puntos de valor ' + tXml.CostValueText+ ' no es válido', tXml.MessageField),
				------------------------------
				tXml.CostValue = TRY_CAST(tXml.CostValueText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND ISNULL(tXml.AllowEditCostValue, 1) = 1

		-------------------------- Valida asociación de los detalles al elemento del costo -------------------------- 

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
				tXml.Value = 1
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que implementa la funcionalidad de Copiar y Pegar (Copy & Paste) para los detalles de distribución de costos directos bajo el esquema de distribución estándar. Recibe un XML con filas que contienen información de centros de producción, cuentas contables, centros de costo y unidades de medida, los cuales valida contra las tablas maestras de costos, contabilidad general y nómina antes de procesarlos. Durante la validación verifica que el centro de producción exista en Cost.CostProductionCenter, que la cuenta contable corresponda al libro oficial en GeneralLedger.MainAccounts y LegalBook, que el centro de costo sea válido y que la unidad de medida esté registrada. Devuelve el resultado de cada fila con un estado y mensaje indicando si fue procesada correctamente o si presentó errores, siendo utilizado en el módulo de costos para la carga masiva y reutilización de distribuciones de gastos directos entre períodos o centros.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y normaliza filas pegadas (Copy & Paste) de detalles de distribución estándar de un elemento de costo, resolviendo IDs (centro de producción, cuenta, centro de costo, unidad de medida) y devolviendo cada fila como válida o con su mensaje de error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Data con GeneralExpenseId y nodos /Data/Row con los campos Item0..Item5 (centro de producción, cuenta contable, centro de costo, unidad de medida, cantidad, valor); Debe existir un libro legal marcado como oficial (OfficialBook=1) en GeneralLedger.LegalBook; Debe existir una base de distribución (Cost.CostDistributionBase) asociada al GeneralExpenseId recibido para validar unidades de medida y detalles parametrizados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se valida y procesa contra cuentas contables del libro legal marcado como oficial (OfficialBook=1); El centro de costo solo se valida cuando la cuenta contable indica HandlesCostCenter=1; Las validaciones se ejecutan en cascada: solo se aplica la siguiente sobre filas con StatusField=99 (sin error previo); Los puntos de valor (CostValue) solo se editan/validan desde el XML cuando la unidad de medida permite edición (AllowEditCostValue=1); en caso contrario se hereda el CostValue de la unidad de medida; Los códigos numéricos en CountText/CostValueText se normalizan reemplazando ''.'' por '''' y '','' por ''.'' antes de convertir; El procedimiento no realiza escrituras persistentes: solo opera sobre una variable tabla y devuelve resultados; Las filas con StatusField=99 no se devuelven en el resultado final (solo válidas con 0 o erróneas con 999); Un error en tiempo de ejecución se captura y se devuelve como un registro con StatusField=999 y el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos estándar; Elemento del costo (Gasto general); Centro de producción; Cuenta contable / Plan de cuentas; Libro legal oficial; Centro de costo; Unidad de medida; Base de distribución de costos; Copy & Paste de detalles de distribución', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve solo las filas cuyo StatusField <> 99, es decir, las validadas exitosamente (StatusField=0, Value=1) o las que fallaron alguna validación (StatusField=999) con su mensaje correspondiente; [RAISERROR] RESULT_SET: Si ocurre una excepción dentro del TRY, inserta una fila con StatusField=999 y MessageField conteniendo ERROR_MESSAGE() y la línea del error, y la incluye en el resultado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El centro de producción (por código) no existe en Cost.CostProductionCenter → Marca el registro con StatusField=999 y mensaje indicando que el centro de producción no existe; si La cuenta contable (por número) no existe para el libro legal oficial (OfficialBook=1) → Marca StatusField=999 con mensaje ''La cuenta contable ... no existe''; si La cuenta contable obliga el manejo de centro de costo (HandlesCostCenter=1) y el centro de costo no existe en Payroll.CostCenter → Marca StatusField=999 con mensaje ''El centro de costo ... no existe''; si La unidad de medida no existe en Inventory.InventoryMeasurementUnit → Marca StatusField=999 con mensaje ''La unidad de medida ... no existe''; si La unidad de medida no está parametrizada en la base de distribución del elemento de costo (GeneralExpense) → Marca StatusField=999 con mensaje ''no se encuentra parametrizada en el elemento del costo seleccionado''; si La cantidad (CountText) no se puede convertir a DECIMAL(18,2) → Marca StatusField=999 con mensaje ''La cantidad ... no es válida''; si AllowEditCostValue=1 y CostValueText no convierte a DECIMAL(18,2) → Marca StatusField=999 con mensaje ''Los puntos de valor ... no es válido'' else Si AllowEditCostValue=0 conserva el CostValue tomado de la unidad de medida; si La combinación (centro de producción, cuenta contable, centro de costo) no está parametrizada como detalle de la base de distribución del elemento de costo → Marca StatusField=999 con mensaje de no parametrización en el elemento del costo; si Registro pasa todas las validaciones (StatusField sigue en 99) → Asigna StatusField=0 y Value=1 (registro válido)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Inventory.InventoryMeasurementUnit; Cost.CostDistributionBase; Cost.CostDistributionBaseMeasurementUnit; Cost.CostDistributionBaseDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution';
-- GO
