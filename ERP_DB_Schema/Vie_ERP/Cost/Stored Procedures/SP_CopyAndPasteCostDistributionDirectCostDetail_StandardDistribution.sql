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