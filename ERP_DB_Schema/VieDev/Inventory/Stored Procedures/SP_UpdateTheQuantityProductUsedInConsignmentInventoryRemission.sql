-- ========================================================= --
/******* EJEMPLO DE LA ESTRUCTURA QUE DEBEN ENVIAR ***********/
--<Remission>
--    <EntityDetailId>2717</EntityDetailId>
--	  <OperatingUnitId>1</OperatingUnitId>
--    <FunctionalUnitId>5</FunctionalUnitId>
--    <WarehouseId>1435</WarehouseId>
--    <ProductId>6361</ProductId>
--    <BatchSerialId>12</BatchSerialId>
--    <MovementType>2</MovementType>
--    <Quantity>1</Quantity>
--    <Value>12057.00</Value>
--    <OriginId>271</OriginId>
--</Remission>
-- =============================================
-- Author:		Miguel Fonseca
-- Create date: 2017-12-18
-- Description:	Actualizar la cantidad usada del producto en el detalle de la remisión de inventario en consignación
-- =============================================
CREATE PROCEDURE [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission]
	@Remission xml,
	@EntityId int,
	@EntityCode varchar(20),
	@EntityName as varchar(100),
	@User varchar(20),
	@MessageReturn VARCHAR(MAX) OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;

/******************** DECLARACION DE VARIABLES *************************/
	
	--Tabla para almacenar todas los movimientos de la remision recibidas
	DECLARE @TableRemission TABLE (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		EntityDetailId INT,
		OperatingUnitId INT,
		FunctionalUnitId INT,
		WarehouseId INT,
		ProductId INT,
		BatchSerialId INT,
		MovementType TINYINT,
		Quantity INT,			
		Value DECIMAL(20,4),
		OriginId INT,
		InvoiceWithCostList BIT
	)

	--Variables para recorrer los movimientos de la remision recibidas
	DECLARE @EntityDetailId INT,
		@OperatingUnitId INT,
		@FunctionalUnitId INT,
		@WarehouseId INT,
		@ProductId INT,
		@BatchSerialId INT,
		@MovementType TINYINT,
		@Quantity INT,			
		@Value DECIMAL(20,4),
		@OriginId INT,
		@InvoiceWithCostList BIT,
		--Mensaje de error
		@MessageError VARCHAR(MAX)

	--Variables para recorrer los detalles de la remisiones de inventario en consignacion
	DECLARE @cirId INT,
		@cirdId INT,
		@cirdbsId INT,
		@cirdcId INT,
		@cirdbsOutstandingQuantity INT,
		@cirdbsUsedQuantity INT,
		@QuantityUsed INT,
		@QuantityPendingLegalization INT,
		@productStatus AS TINYINT,
		@OfficialCurrencyId as INTEGER

	--Tabla temporal en donde se almacenan los errores  y poder validar
	DECLARE @TableErrors TABLE (Id INT IDENTITY(1,1) PRIMARY KEY, MessageError VARCHAR(MAX))

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE (
			Id integer DEFAULT(0),
			Consecutive bigint DEFAULT(0),
			LegalBookId integer,
			IdJournalVoucher integer,
			VoucherDate DATETIME,
			Imported varchar(5),
			[Status] tinyint,
			Detail varchar(500),
			EntityCode  varchar(20),
			EntityId integer,
			EntityName varchar(250),
			IsClosedYear tinyint
		)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE (
			Id integer DEFAULT(0),
			IdAccounting integer DEFAULT(0),
			IdMainAccount integer,
			IdThirdParty integer,
			IdCostCenter integer,
			DebitValue decimal(20,4),
			CreditValue decimal(20,4),
			Detail varchar(500),
			IdRetention integer,
			RetentionRate decimal(5,3) DEFAULT(0),
			BaseValue decimal(18,2) DEFAULT(0),
			BillingValue decimal(18,2) DEFAULT(0)
		)

	--Tabla temporal para guardar el resultado del save del comprobante contable
	DECLARE @resultJournalVoucher TABLE (
			code varchar(20),
			MessageResult varchar(max),
			IdJournalVoucher integer
		)

	--Variable para obtener el xml
	declare @JournalVoucherXML as XML,
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Id del Libro, Id del tipo de comprobante contable
	DECLARE @LegalBookId INT,
			@JournalVoucherTypeId INT,
			@IVACost TINYINT

	-- Detalle de la cabecera
	DECLARE @detail VARCHAR(500)

	BEGIN TRY
		INSERT INTO @TableRemission (EntityDetailId, OperatingUnitId, FunctionalUnitId, WarehouseId, ProductId, BatchSerialId, MovementType, Quantity, Value, OriginId, InvoiceWithCostList)
			SELECT
				t.x.value('EntityDetailId[1]','int'),
				t.x.value('OperatingUnitId[1]','int'),
				t.x.value('FunctionalUnitId[1]','int'),
				t.x.value('WarehouseId[1]','int'),
				t.x.value('ProductId[1]','int'),
				CASE 
					WHEN NULLIF(LTRIM(RTRIM(t.x.value('(BatchSerialId/text())[1]','nvarchar(50)'))), '') IS NULL THEN NULL
					ELSE t.x.value('(BatchSerialId)[1]', 'int')
				END,
				t.x.value('MovementType[1]','tinyint'),								
				t.x.value('Quantity[1]','int'),				
				t.x.value('Value[1]','DECIMAL(20,4)'),
				t.x.value('OriginId[1]','int'),
				ISNULL(t.x.value('InvoiceWithCostList[1]','BIT'), 0) 
			FROM @Remission.nodes('/Remission') t(x)
	
/******************** VALIDACIONES *************************/
		--Verificamos que lleguen datos
		if (SELECT count(*) FROM @Remission.nodes('/Remission') t(x)) = 0 begin
			SELECT @MessageReturn = 'Error al actualizar la cantidad usada en la remisión de inventario en consignación'
			RETURN
		end

		--Verificamos que todos los movimientos sean del mismo tipo
		if (SELECT COUNT(DISTINCT MovementType) FROM @TableRemission) <> 1 begin
			SELECT @MessageReturn = 'Solo debe haber un tipo de movimiento dentro de los detalles'
			RETURN
		end

		--Verificamos que todos los movimientos sean de entrada o de salida
		IF (SELECT COUNT(*) FROM @TableRemission WHERE MovementType IN (1, 2)) = 0 BEGIN
			SELECT @MessageReturn = 'Los únicos movimientos validos son de entrada y de salida'
			RETURN
		END
		
		--Comprobamos que se haya ingresado la cantidad en la transaccion
		IF ((SELECT COUNT(*) FROM @TableRemission WHERE ISNULL(Quantity, 0) <= 0) > 0) BEGIN
			SELECT @MessageError = (SELECT p.Code + ', ' FROM @TableRemission r INNER JOIN Inventory.InventoryProduct p ON r.ProductId = p.Id WHERE ISNULL(r.Quantity, 0) <= 0 for XML PATH(''))
			SELECT @MessageReturn = 'No se logro actualizar la cantidad usada en la remisión de inventario en consignación debido a que los siguientes Productos no tienen una cantidad válida asignada: ' + @MessageError
			RETURN
		END

		--El almacen debe ser tipo consignación
		IF (SELECT COUNT(*) FROM @TableRemission r LEFT JOIN Inventory.Warehouse w ON r.WarehouseId = w.Id WHERE ISNULL(w.WarehouseConsignment, 0) <> 1) > 0 BEGIN
			SELECT @MessageError = (SELECT w.Code + ' - ' + w.Name + ', ' FROM @TableRemission r LEFT JOIN Inventory.Warehouse w ON r.WarehouseId = w.Id WHERE ISNULL(w.WarehouseConsignment, 0) <> 1 for XML PATH(''))
			SELECT @MessageReturn = 'No se logro actualizar la cantidad usada en la remisión de inventario en consignación debido a que los siguientes almacenes no son de consignación: ' + @MessageError
			RETURN
		END

		--Verificamos que, si existen movimientos de entrada, sean de devolución de dispensación
		IF (SELECT COUNT(*) FROM @TableRemission r WHERE MovementType = 1 AND @EntityName <> 'PharmaceuticalDispensingDevolution' AND @EntityName <> 'TransferOrderDevolution' AND @EntityName <> 'BasicBilling') > 0 BEGIN
			SELECT @MessageReturn = 'No se logró actualizar la cantidad usada en la remisión de inventario en consignación debido a que se está realizando un movimiento no controlado por el sistema.'
			RETURN
		END

/****************************** DATOS NECESARIOS *******************************/

		--Permite saber si el iva va al costo
		SELECT @IVACost = si.TaxRegistration 
		FROM Inventory.SettingInventory  si
		JOIN @TableRemission tr ON si.OperatingUnitId = tr.OperatingUnitId

		SELECT top 1 @OfficialCurrencyId = cs.OfficialCurrencyId
		from GeneralLedger.CompanySettings cs WITH(NOLOCK)

/******************** CURSOR MOVIMIENTO DE LA REMISION *************************/

		DECLARE remission_cursor CURSOR FOR
			SELECT EntityDetailId, OperatingUnitId, FunctionalUnitId, ProductId, MovementType, WarehouseId, BatchSerialId, Quantity, Value, OriginId, InvoiceWithCostList FROM @TableRemission

		OPEN remission_cursor
  
			FETCH NEXT FROM remission_cursor
				INTO @EntityDetailId, @OperatingUnitId, @FunctionalUnitId, @ProductId, @MovementType, @WarehouseId, @BatchSerialId, @Quantity, @Value, @OriginId, @InvoiceWithCostList

			WHILE @@FETCH_STATUS = 0  
			BEGIN
							
				--Si es un movimiento de entrada
				IF @MovementType = 1 BEGIN
					--Comprobamos que se haya ingresado el documento del cual se desea retornar una cantidad
					IF (ISNULL(@OriginId, 0) = 0) BEGIN
						SELECT @MessageError = (select p.Code + ', ' from Inventory.InventoryProduct p where p.Id = @ProductId for XML PATH(''))

						INSERT INTO @TableErrors (MessageError)
							SELECT 'No se logro actualizar la cantidad usada en la remisión de inventario en consignación debido a que no se estableció el documento de origen del Producto: ' + @MessageError

						GOTO END_REMISSION_CURSOR
					END
					
					--Verificamos que se en realidad exista el documento del cual se desea retornar una cantidad
					IF ISNULL((SELECT COUNT(*)
							FROM Inventory.ConsignmentInventoryRemission cir
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
								ON cir.Id = cird.ConsignmentInventoryRemissionId
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdd
								ON cird.Id = cirdd.ConsignmentInventoryRemissionDetailId						
							WHERE cir.Status = 2 AND
								cir.WarehouseId = @WarehouseId AND
								cird.ProductId = @ProductId AND
								ISNULL(cirdd.BatchSerialId, 0) = ISNULL(@BatchSerialId, 0) AND
								cirdd.EntityId = @OriginId AND
								(cirdd.EntityName = 'PharmaceuticalDispensing' OR cirdd.EntityName = 'TransferOrder' OR cirdd.EntityName = 'BasicBilling' OR cirdd.EntityName = 'ConsignmentTransfer')
								), 0) = 0 BEGIN

						SELECT @MessageError = (select p.Code + ', ' from Inventory.InventoryProduct p where p.Id = @ProductId for XML PATH(''))

						INSERT INTO @TableErrors (MessageError)
							SELECT 'No se logro actualizar la cantidad usada en la remisión de inventario en consignación debido a que no se existe el documento de origen del siguiente Producto: ' + @MessageError
						
						GOTO END_REMISSION_CURSOR
					END
					
					--Ratificamos que el documento origen tenga cantidades pendientes por legalizar suficientes para realizar la devolución
					IF ISNULL((SELECT SUM(cirdc.QuantityPendingLegalization)
							FROM Inventory.ConsignmentInventoryRemission cir
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
								ON cir.Id = cird.ConsignmentInventoryRemissionId						
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
								ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId						
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc
								ON cird.Id = cirdc.ConsignmentInventoryRemissionDetailId
							WHERE cir.Status = 2 AND
								cir.WarehouseId = @WarehouseId AND
								cird.ProductId = @ProductId AND
								ISNULL(cirdc.BatchSerialId, 0) = ISNULL(@BatchSerialId, 0) AND
								cirdc.EntityId = @OriginId AND
								(cirdc.EntityName = 'PharmaceuticalDispensing' OR cirdc.EntityName = 'TransferOrder' OR cirdc.EntityName = 'BasicBilling' OR cirdc.EntityName = 'ConsignmentTransfer')
								), 0) < @Quantity BEGIN

						SELECT @MessageError = (select p.Code + ', ' from Inventory.InventoryProduct p where p.Id = @ProductId for XML PATH(''))

						INSERT INTO @TableErrors (MessageError)
							SELECT 'No se logro actualizar la cantidad usada en la remisión de inventario en consignación debido a que el siguiente Producto no cuenta con la cantidad suficiente-A: ' + @MessageError

						GOTO END_REMISSION_CURSOR
					END

					/******************** CURSOR DETALLE ENTRADA  *************************/

					DECLARE remission_detail_cursor CURSOR FOR
						SELECT cir.Id, cird.Id, cirdbs.Id, cirdc.Id, cirdbs.OutstandingQuantity, cirdbs.UsedQuantity, cirdc.QuantityPendingLegalization
							FROM Inventory.ConsignmentInventoryRemission cir
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
								ON cir.Id = cird.ConsignmentInventoryRemissionId
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
								ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc
								ON cird.Id = cirdc.ConsignmentInventoryRemissionDetailId
							INNER JOIN Inventory.Warehouse w
								ON cir.WarehouseId = w.Id
							WHERE cir.Status = 2 AND
								cir.WarehouseId = @WarehouseId AND
								cird.ProductId = @ProductId AND 
								ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@BatchSerialId, 0) AND
								cirdc.EntityId = @OriginId AND
								(cirdc.EntityName = 'PharmaceuticalDispensing' OR cirdc.EntityName = 'TransferOrder' OR cirdc.EntityName = 'BasicBilling' OR cirdc.EntityName = 'ConsignmentTransfer') AND
								cirdc.QuantityPendingLegalization > 0
							ORDER BY cir.RemissionDate, cir.Id, cirdc.Id

					OPEN remission_detail_cursor
  
						FETCH NEXT FROM remission_detail_cursor
							INTO @cirId, @cirdId, @cirdbsId, @cirdcId, @cirdbsOutstandingQuantity, @cirdbsUsedQuantity, @QuantityPendingLegalization

						WHILE @@FETCH_STATUS = 0  
						BEGIN
							IF NOT @Quantity > 0 BEGIN
								BREAK
							END

							SELECT @QuantityUsed = @QuantityPendingLegalization
							SELECT @QuantityUsed = IIF(@Quantity > @QuantityUsed, @QuantityUsed, @Quantity)

							--Actualizamos la cantidad pendiente de legalización del control de la dispensacion
							UPDATE cirdc 
								SET QuantityPendingLegalization = QuantityPendingLegalization - @QuantityUsed
							FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
							WHERE cirdc.Id = @cirdcId

							--Insertamos el control de la devolución de la dispensación
							INSERT INTO Inventory.ConsignmentInventoryRemissionDetailControl
									([ConsignmentInventoryRemissionDetailId], [BatchSerialId], [MovementType], [Quantity], [QuantityPendingLegalization], [Value], [EntityId], [EntityCode], [EntityName], [EntityDetailId], [OperatingUnitId], [FunctionalUnitId], [CreationUser], [CreationDate])
							VALUES	(@cirdId, @BatchSerialId, @MovementType, @QuantityUsed, 0, @Value, @EntityId, @EntityCode, @EntityName, @EntityDetailId, @OperatingUnitId, @FunctionalUnitId, @User, [Common].[GETDATE]())

							--Actualizamos las cantidades usadas y disponibles del lote
							UPDATE Inventory.ConsignmentInventoryRemissionDetailBatchSerial
								SET UsedQuantity = UsedQuantity - @QuantityUsed,
									OutstandingQuantity = OutstandingQuantity + @QuantityUsed
							WHERE Id = @cirdbsId

							--Determinamos el ProductStatus de la remision
							SET @productStatus = 3 -- Total
							IF ISNULL((SELECT COUNT(*)
										FROM Inventory.ConsignmentInventoryRemissionDetail cird
										JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
										WHERE cird.ConsignmentInventoryRemissionId = @cirId AND cirdbs.OutstandingQuantity > 0), 0) > 0 BEGIN
								SET @productStatus = 2 --Parcial
							END

							--Actualizamos el ProductStatus de la remision
							UPDATE Inventory.ConsignmentInventoryRemission
								SET ProductStatus = @productStatus
							WHERE Id = @cirId

							SELECT @Quantity = @Quantity - @QuantityUsed
							
							--Se insertan los detalles del comprobante contable (reversión de lo contabilizado en el uso de la remisión)
							SET @detail = 'Detalle generado luego de la devolución del uso de la remisión del inventario en consignación: '
							INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
								SELECT
									ma.Id, 
									IIF(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
									IIF(ma.Id = g.ConsignmentMerchandiseDebitAccountId, Common.CurrencyConverterByModule((@QuantityUsed * (CASE @IVACost WHEN 1 THEN ROUND(cird.UnitValue * (1 + cird.IvaPercentage / 100), 2) ELSE ROUND(cird.UnitValue, 2) END)),
																															cir.CurrencyId,@OfficialCurrencyId,@OperatingUnitId,@EntityName,cast(cir.RemissionDate as date)), 0),
									
									IIF(ma.Id = g.ConsignmentMerchandiseDebitAccountId, 0, Common.CurrencyConverterByModule((@QuantityUsed * (CASE @IVACost WHEN 1 THEN ROUND(cird.UnitValue * (1 + cird.IvaPercentage / 100), 2) ELSE ROUND(cird.UnitValue, 2) END)),
																																cir.CurrencyId,@OfficialCurrencyId,@OperatingUnitId,@EntityName,cast(cir.RemissionDate as date))),
									@detail + cir.Code
								FROM Inventory.ConsignmentInventoryRemission cir 
								JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
								JOIN Inventory.InventoryProduct p ON cird.ProductId = p.Id
								JOIN Inventory.ProductGroup g on g.Id = p.ProductGroupId
								JOIN GeneralLedger.MainAccounts ma on ma.Id = g.ConsignmentMerchandiseDebitAccountId OR ma.Id = g.ConsignmentMerchandiseCreditAccountId
								JOIN Common.Supplier s ON cir.SupplierId = s.Id
								WHERE cird.Id = @cirdId

							FETCH NEXT FROM remission_detail_cursor
								INTO @cirId, @cirdId, @cirdbsId, @cirdcId, @cirdbsOutstandingQuantity, @cirdbsUsedQuantity, @QuantityPendingLegalization
						END
					CLOSE remission_detail_cursor
					DEALLOCATE remission_detail_cursor

				END

				--Si es un movimiento de salida
				ELSE IF @MovementType = 2 BEGIN

					--Comprobamos que exista la cantidad suficiente para realizar la salida
					DECLARE @consulta as INT = 0 
					SELECT @consulta = SUM(cirdbs.OutstandingQuantity)
					FROM Inventory.ConsignmentInventoryRemission cir WITH(NOLOCK)
						INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird WITH(NOLOCK) ON cir.Id = cird.ConsignmentInventoryRemissionId
						INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs WITH(NOLOCK) ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
						INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON cir.WarehouseId = w.Id
					WHERE cir.Status = 2 AND
						cir.WarehouseId = @WarehouseId AND
						cird.ProductId = @ProductId AND 
						ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@BatchSerialId, 0) AND
						cirdbs.OutstandingQuantity > 0

					--Comprobamos que exista la cantidad suficiente para realizar la salida
					IF (isnull(@consulta, 0)) < @Quantity BEGIN

						SELECT @MessageError = (select p.Code + ', ' from Inventory.InventoryProduct p where p.Id = @ProductId for XML PATH(''))
					
						INSERT INTO @TableErrors (MessageError)
							SELECT 'No se logro actualizar la cantidad usada en la remisión de inventario en consignación debido a que el siguiente producto no cuenta con la cantidad suficiente-B: ' + @MessageError
						
						GOTO END_REMISSION_CURSOR
					END

				--Se afecta el Kardex solo si es un movimiento que proviene de un traslado en consignación
				IF @EntityName = 'ConsignmentTransfer' BEGIN
						SELECT @SubXml = CONVERT
							(
								XML, 
								(
									SELECT Kardex.*
									FROM 
									( 
										SELECT
											@ProductId ProductId,
											@MovementType MovementType,
											@WarehouseId WarehouseId,
											@BatchSerialId BatchSerialId,
											@Quantity Quantity,
											(select TOP 1 documentDate FROM Inventory.ConsignmentTransfer
											WHERE Id = @EntityId ) DocumentDate,
											@Value Value,
											0 AffectAverageCost
									) Kardex
									For XML AUTO,TYPE, ELEMENTS
								)
							)

						EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @EntityId, @EntityCode, 'ConsignmentTransfer', @User, 1, @Code_Output OUT, @Message_Output OUT

						IF @Code_Output <> 0
						BEGIN
							INSERT INTO @TableErrors 
								SELECT ISNULL(@Message_Output, 'No se pudo afectar el kardex')
							GOTO END_REMISSION_CURSOR
						END
					END

/******************** CURSOR DETALLE SALIDA  *************************/
				
					DECLARE remission_detail_cursor CURSOR FOR 		
						SELECT cir.Id, cird.Id, cirdbs.Id, cirdbs.OutstandingQuantity, cirdbs.UsedQuantity
							FROM Inventory.ConsignmentInventoryRemission cir
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
								ON cir.Id = cird.ConsignmentInventoryRemissionId
							INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
								ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
							INNER JOIN Inventory.Warehouse w
								ON cir.WarehouseId = w.Id
							WHERE cir.Status = 2 AND
								cir.WarehouseId = @WarehouseId AND
								cird.ProductId = @ProductId AND 
								ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@BatchSerialId, 0) AND
								cirdbs.OutstandingQuantity > 0
							ORDER BY cir.RemissionDate, cir.Id

					OPEN remission_detail_cursor
					
						FETCH NEXT FROM remission_detail_cursor
							INTO @cirId, @cirdId, @cirdbsId, @cirdbsOutstandingQuantity, @cirdbsUsedQuantity
						
						WHILE @@FETCH_STATUS = 0  
						BEGIN
						IF NOT @Quantity > 0 BEGIN
								BREAK
							END
							
							SELECT @QuantityUsed = IIF(@Quantity > @cirdbsOutstandingQuantity, @cirdbsOutstandingQuantity, @Quantity)											

							--Insertamos el control de la dispensación
							INSERT INTO Inventory.ConsignmentInventoryRemissionDetailControl
									([ConsignmentInventoryRemissionDetailId], [BatchSerialId], [MovementType], [Quantity], [QuantityPendingLegalization], [Value], [EntityId], [EntityCode], [EntityName], [EntityDetailId], [OperatingUnitId], [FunctionalUnitId], [CreationUser], [CreationDate])
							VALUES	(@cirdId, @BatchSerialId, @MovementType, @QuantityUsed, @QuantityUsed, ISNULL(@Value, 0), @EntityId, @EntityCode, @EntityName, @EntityDetailId, @OperatingUnitId, @FunctionalUnitId, @User, [Common].[GETDATE]())
							--select * from Inventory.ConsignmentInventoryRemissionDetailControl where ConsignmentInventoryRemissionDetailId = @cirdId
							--Actualizamos las cantidades usadas y disponibles del lote
							UPDATE Inventory.ConsignmentInventoryRemissionDetailBatchSerial
								SET UsedQuantity = UsedQuantity + @QuantityUsed,
									OutstandingQuantity = OutstandingQuantity - @QuantityUsed
							WHERE Id = @cirdbsId

							--Determinamos el ProductStatus de la remision
							SET @productStatus = 3 -- Total
							IF ISNULL((SELECT COUNT(*)
										FROM Inventory.ConsignmentInventoryRemissionDetail cird
										JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
										WHERE cird.ConsignmentInventoryRemissionId = @cirId AND cirdbs.OutstandingQuantity > 0), 0) > 0 BEGIN
								SET @productStatus = 2 --Parcial
							END

							--Actualizamos el ProductStatus de la remision
							UPDATE Inventory.ConsignmentInventoryRemission
								SET ProductStatus = @productStatus
							WHERE Id = @cirId

							SELECT @Quantity = @Quantity - @QuantityUsed

							--Se insertan los detalles del comprobante contable (reversión de lo contabilizado en la remisión)
							SET @detail = 'Detalle generado luego del uso de la remisión del inventario en consignación: '
							IF @InvoiceWithCostList <> 1 BEGIN
								INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
									SELECT
										ma.Id, 
										IIF(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
										IIF(ma.Id = g.ConsignmentMerchandiseDebitAccountId, 0, Common.CurrencyConverterByModule((@QuantityUsed * cird.UnitValue),
																																cir.CurrencyId,@OfficialCurrencyId,@OperatingUnitId,@EntityName,cast(cir.RemissionDate as date))),
									
										IIF(ma.Id = g.ConsignmentMerchandiseDebitAccountId, Common.CurrencyConverterByModule((@QuantityUsed * cird.UnitValue),
																															cir.CurrencyId,@OfficialCurrencyId,@OperatingUnitId,@EntityName,CAST(cir.RemissionDate as date)), 0),
										@detail + cir.Code
									FROM Inventory.ConsignmentInventoryRemission cir 
									JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
									JOIN Inventory.InventoryProduct p ON cird.ProductId = p.Id
									JOIN Inventory.ProductGroup g on g.Id = p.ProductGroupId
									JOIN GeneralLedger.MainAccounts ma on ma.Id = g.ConsignmentMerchandiseDebitAccountId OR ma.Id = g.ConsignmentMerchandiseCreditAccountId
									JOIN Common.Supplier s ON cir.SupplierId = s.Id
									WHERE cird.Id = @cirdId
								END
							ELSE
								BEGIN
									INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
										SELECT 
											ma.Id,
											IIF(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
											IIF(ma.Id = g.ConsignmentMerchandiseDebitAccountId, 0, ROUND(ccld.CostNew, 2)),
											IIF(ma.Id = g.ConsignmentMerchandiseDebitAccountId, ROUND(ccld.CostNew, 2), 0), 
											@detail + cir.Code
										FROM Inventory.ConsignmentInventoryRemission cir 
										JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
										JOIN Inventory.InventoryProduct p ON cird.ProductId = p.Id							
										JOIN Inventory.ProductGroup g on g.Id = p.ProductGroupId										
										JOIN GeneralLedger.MainAccounts ma on ma.Id = g.ConsignmentMerchandiseDebitAccountId OR ma.Id = g.ConsignmentMerchandiseCreditAccountId						
										JOIN Common.Supplier s ON cir.SupplierId = s.Id
										LEFT JOIN Billing.BasicBillingDetailItem bbdi with(Nolock) on bbdi.Id = @EntityDetailId
										LEFT JOIN Billing.BasicBillingDetail bbd ON bbd.Id = bbdi.BasicBillingDetailId
										LEFT JOIN Inventory.Warehouse wh with(Nolock) ON wh.Id = bbd.WarehouseId
										LEFT JOIN Inventory.ConsignmentCostList ccl with(Nolock) ON ccl.SupplierId = wh.SupplierId
										LEFT JOIN Inventory.ConsignmentCostListDetail ccld with(Nolock) ON ccld.ConsignmentCostListId = ccl.Id
										WHERE cird.Id = @cirdId AND ccld.ProductId = @ProductId AND ccl.OperatingUnitId = @OperatingUnitId
								END
							FETCH NEXT FROM remission_detail_cursor
								INTO @cirId, @cirdId, @cirdbsId, @cirdbsOutstandingQuantity, @cirdbsUsedQuantity
						END
						
					CLOSE remission_detail_cursor
					DEALLOCATE remission_detail_cursor

				END

				END_REMISSION_CURSOR:
				FETCH NEXT FROM remission_cursor
					INTO @EntityDetailId, @OperatingUnitId, @FunctionalUnitId, @ProductId, @MovementType, @WarehouseId, @BatchSerialId, @Quantity, @Value, @OriginId, @InvoiceWithCostList
			END
		CLOSE remission_cursor
		DEALLOCATE remission_cursor
	
		/** --------- VALIDO SI HUBO ERRORES RETORNO ERROR --------- **/
		IF (SELECT COUNT(*) FROM @TableErrors) > 0 BEGIN
			SELECT @MessageReturn = STUFF((SELECT N'' + MessageError + CHAR(13) + CHAR(10) FROM @TableErrors FOR xml path(N''), type).value(N'.[1]', N'NVARCHAR(MAX)'), 1,0, N'')
			RETURN
		END
		
		-- Obtenemos el id del comprobante contable
		IF (SELECT TOP 1 MovementType FROM @TableRemission) = 1 BEGIN
			SET @detail = 'Comprobante contable generado por la devolución del uso de la remisión del inventario en consignación'

			IF @EntityName = 'PharmaceuticalDispensingDevolution' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseDevolutionJournalVoucherTypeId
				FROM Inventory.PharmaceuticalDispensingDevolution pdd
				INNER JOIN Inventory.SettingInventory si ON pdd.OperatingUnitId = si.OperatingUnitId
				WHERE pdd.Id = @EntityId AND pdd.Code = @EntityCode
			END
			IF @EntityName = 'TransferOrderDevolution' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseDevolutionJournalVoucherTypeId
				FROM Inventory.TransferOrderDevolution pdd
				INNER JOIN Inventory.SettingInventory si ON pdd.OperatingUnitId = si.OperatingUnitId
				WHERE pdd.Id = @EntityId AND pdd.Code = @EntityCode
			END
			IF @EntityName = 'BasicBilling' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseDevolutionJournalVoucherTypeId
				FROM Billing.BasicBilling bb
				INNER JOIN Inventory.SettingInventory si ON bb.OperatingUnitId = si.OperatingUnitId
				where bb.Id = @EntityId AND bb.Code = @EntityCode
			END
		END
		ELSE BEGIN
			SET @detail = 'Comprobante contable generado por el uso de la remisión del inventario en consignación'

			IF @EntityName = 'DocumentInvoiceProductSales' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseJournalVoucherTypeId
				FROM Inventory.DocumentInvoiceProductSales dips
				INNER JOIN Inventory.SettingInventory si ON dips.OperatingUnitId = si.OperatingUnitId
				WHERE dips.Id = @EntityId AND dips.Code = @EntityCode
			END
			ELSE IF @EntityName = 'PharmaceuticalDispensing' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseJournalVoucherTypeId
				FROM Inventory.PharmaceuticalDispensing pd
				INNER JOIN Inventory.SettingInventory si ON pd.OperatingUnitId = si.OperatingUnitId
				WHERE pd.Id = @EntityId AND pd.Code = @EntityCode
			END
			ELSE IF @EntityName = 'TransferOrder' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseJournalVoucherTypeId
				FROM Inventory.TransferOrder tor
				INNER JOIN Inventory.SettingInventory si ON tor.OperatingUnitId = si.OperatingUnitId
				WHERE tor.Id = @EntityId AND tor.Code = @EntityCode
			END
			ELSE IF @EntityName ='BasicBilling' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseJournalVoucherTypeId
				FROM Billing.BasicBilling bb WITH(NOLOCK)
				INNER JOIN Inventory.SettingInventory si WITH(NOLOCK) on bb.OperatingUnitId = si.OperatingUnitId
				where bb.Id = @EntityId AND bb.Code = @EntityCode
			END
			ELSE IF @EntityName = 'ConsignmentTransfer' BEGIN
				SELECT @JournalVoucherTypeId = si.ConsignmentInventoryUseJournalVoucherTypeId
				FROM Inventory.ConsignmentTransfer pd
				INNER JOIN Inventory.SettingInventory si ON pd.OperatingUnitId = si.OperatingUnitId
				WHERE pd.Id = @EntityId AND pd.Code = @EntityCode
			END
		END

		-- Validamos que se haya establecido un tipo de comprobante contable
		IF @JournalVoucherTypeId IS NULL BEGIN
			SELECT @MessageReturn = 'No se ha establecido un comprobante contable para la contabilizacion de la reclasificacion de la remisión del inventario en consignación'
			RETURN
		END
		
		--Verificamos que configuracion de viebot
		IF (SELECT COUNT(*)
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = @EntityName + 'Reclassification' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = 'ConsignmentInventoryRemission' AND vb2.Id IS NULL) > 0 BEGIN

			INSERT INTO [GeneralLedger].[VieBot] (Form, HandlesHomologation, LegalBookId, Allow, CreationUser, CreationDate, ModificationUser, ModificationDate)
				SELECT @EntityName + 'Reclassification', vb.HandlesHomologation, vb.LegalBookId, vb.Allow, vb.CreationUser, vb.CreationDate, vb.ModificationUser, vb.ModificationDate
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = @EntityName + 'Reclassification' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = 'ConsignmentInventoryRemission' AND vb2.Id IS NULL
		END

		IF (SELECT COUNT(*)
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = 'ConsignmentInventoryRemission' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = @EntityName + 'Reclassification' AND vb2.Id IS NULL) > 0 BEGIN

			DELETE vb 
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = 'ConsignmentInventoryRemission' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				where vb.Form = @EntityName + 'Reclassification' AND vb2.Id IS NULL
		END
			
		--Se debe insertar en los libros en los cuales se registro la remision de inventario en consignación
		DECLARE viebot_detail_cursor CURSOR FOR 		
			SELECT DISTINCT LegalBookId 
			FROM GeneralLedger.VieBot vb
			inner join GeneralLedger.LegalBook lb on vb.LegalBookId = lb.Id
			WHERE Form = 'ConsignmentInventoryRemission' AND Allow = 1 and lb.OfficialBook = 1
			
		OPEN viebot_detail_cursor  
			FETCH NEXT FROM viebot_detail_cursor INTO @LegalBookId

			WHILE @@FETCH_STATUS = 0  
				
			BEGIN
				delete from @JournalVourcherTmp
				--Inserto la cabecera del comprobante contable
				INSERT INTO @JournalVourcherTmp (LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear)
					values (@LegalBookId, @JournalVoucherTypeId, [Common].[GETDATE](), 'False', 2, @detail, @EntityCode, @EntityId, @EntityName + 'Reclassification', 0)
				
				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML =  convert(xml, (select * 
						from @JournalVourcherTmp JournalVoucher 
						inner join @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

				--Se consume el sp que guarda el comprobante contable
				DELETE FROM @resultJournalVoucher
				INSERT INTO @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @User

				--Se valida que no hayan errores en el guardado del comprobante contable
				IF (SELECT code FROM @resultJournalVoucher) = '999' BEGIN
					INSERT INTO @TableErrors (MessageError)
						SELECT MessageResult FROM @resultJournalVoucher
				END

				FETCH NEXT FROM viebot_detail_cursor INTO @LegalBookId
			END

		CLOSE viebot_detail_cursor
		DEALLOCATE viebot_detail_cursor
		

		/** --------- VALIDO SI HUBO ERRORES RETORNO ERROR --------- **/
		IF (SELECT COUNT(*) FROM @TableErrors) > 0 BEGIN
			SELECT @MessageReturn = STUFF((SELECT N'' + MessageError + CHAR(13) + CHAR(10) FROM @TableErrors FOR xml path(N''), type).value(N'.[1]', N'NVARCHAR(MAX)'), 1,0, N'')			
			RETURN
		END
		
		RETURN

	END TRY
	BEGIN CATCH
		IF CURSOR_STATUS('global','remission_detail_cursor') >= -1
		BEGIN
			CLOSE remission_detail_cursor
			DEALLOCATE remission_detail_cursor
		END

		IF CURSOR_STATUS('global','viebot_detail_cursor') >= -1
		BEGIN
			CLOSE viebot_detail_cursor
			DEALLOCATE viebot_detail_cursor
		END

		SELECT  @MessageReturn = 'Ocurrio un error al actualizar la cantidad usada en la remisión de inventario en consignación: ' + ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(20))
	END CATCH
END