
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-20
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una orden de traslado de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveTransferOrder_Output]
    @TransferOrderXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@MessageResultAux VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	Declare @Deleted Varchar(7) = 'Deleted'
	--Se declaran las variables para obtener la cabecera
	DECLARE @Prefix VARCHAR(4),
			@OperatingUnitId INT,
			@DocumentDate DATETIME,
			@DeliveryDocumentDate DATETIME,
			@OrderType TINYINT,
			@DispatchTo TINYINT,
			@SourceWarehouseId INT,
			@TransitWarehouseId INT,
			@TargetWarehouseId INT,
			@TargetFunctionalUnitId INT,
			@AdjustmentConceptId INT,
			@ThirdPartyId INT,
			@Description VARCHAR(MAX),
			@Status TINYINT,
			------------------------------
			@IdForm INT = 1519,
			@DocumentTypeControl INT = 13,
			------------------------------
			@StockControl TINYINT,
			@TakeTransferOrderThirdParty TINYINT,
			@TransferOrderThirdPartyId INT,
			@JournalVoucherTypeId INT,
			@SourceCostCenterId INT,
			@TargetCostCenterId INT,
			@PermissionValidateQuantity INT = 0,
			@JournalVoucherId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Detail TABLE
	(
		TransferOrderDetailIdTmp INT,
		Id INT,
		TransferOrderId INT,
		InventoryRequestDetailId INT,
		InventoryRequestDetailOtherId INT,
		ProductId INT NOT NULL,
		InventoryQuantity INT NOT NULL,
		Quantity INT NOT NULL,
		Description VARCHAR(MAX),
		ChangeTracker VARCHAR(30)
	)

	--Tabla temporal de los subdetalles
	DECLARE @DetailBatchSerial TABLE
	(
		TransferOrderDetailIdTmp INT,
		Id INT,
		TransferOrderDetailId INT,
		PhysicalInventoryId INT,
		Quantity INT NOT NULL,
		ChangeTracker VARCHAR(30)
	)

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','int'),
				@Prefix = t.x.value('Prefix[1]','varchar(4)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@Code = t.x.value('Code[1]','varchar(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
				@DeliveryDocumentDate = [Common].[GETDATE](),
				@OrderType = t.x.value('OrderType[1]','tinyint'),
				@DispatchTo = t.x.value('DispatchTo[1]','tinyint'),
				@SourceWarehouseId = t.x.value('SourceWarehouseId[1]','int'),
				@TransitWarehouseId = t.x.value('TransitWarehouseId[1]','int'),
				@TargetWarehouseId = t.x.value('TargetWarehouseId[1]','int'),
				@TargetFunctionalUnitId = t.x.value('TargetFunctionalUnitId[1]','int'),
				@AdjustmentConceptId = t.x.value('AdjustmentConceptId[1]','int'),
				@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
				@Description = t.x.value('Description[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','tinyint')
		FROM @TransferOrderXml.nodes('/TransferOrder') t(x)

		IF @ThirdPartyId is NULL
		begin 
			select @ThirdPartyId=TransferOrderThirdPartyId 
			from Inventory.SettingInventory
			where OperatingUnitId = @OperatingUnitId
		END
		----------------------------------

		IF @OrderType = 3 AND @Status = 2
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Inventory.TransferOrder om WHERE om.Id = @Id AND om.Status = 4)
			BEGIN
				SELECT @Message = 'La Orden de Traslado se encuentra en estado: ' + IIF(om.Status = 1, 'Registrado', IIF(om.Status = 2, 'Entregado', 'Anulado'))
				FROM Inventory.TransferOrder om 
				WHERE om.Id = @Id

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'No existe la orden de traslado.')
				RETURN
			END
		END
		ELSE IF EXISTS (SELECT 1 FROM Inventory.TransferOrder om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Orden de Traslado se encuentra en estado: ' + IIF(om.Status = 2, 'Entregado', IIF(om.Status = 3, 'Anulado', 'En Transito'))
			FROM Inventory.TransferOrder om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[TransferOrder]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN

			SELECT	@StockControl = StockControl,
					@TakeTransferOrderThirdParty = TakeTransferOrderThirdParty,
					@TransferOrderThirdPartyId = TransferOrderThirdPartyId,
					@JournalVoucherTypeId = OrderDispatchJournalVoucherTypeId
			FROM Inventory.SettingInventory
			WHERE OperatingUnitId = @OperatingUnitId

			--Obtengo permiso 'Quitar Validación Cantidades Importadas' esta en 'Si' no se realiza la validación
			SELECT @PermissionValidateQuantity = 1
			FROM Security.[UserInt] u
			JOIN Security.PermissionUserInt pu ON u.Id = pu.IdUser
			WHERE u.UserCode = @CodeUser AND pu.IdForm = @IdForm AND pu.Action = '91'

			/***************************************** VALIDACIONES CABECERA *****************************************/

			-- Valido el Periodo del Documento
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Inventory.SettingInventory si
				WHERE si.OperatingUnitId = @OperatingUnitId
					AND si.Year = YEAR(IIF((@OrderType = 3 AND @Status = 2), @DeliveryDocumentDate, @DocumentDate))
					AND si.Month = MONTH(IIF((@OrderType = 3 AND @Status = 2), @DeliveryDocumentDate, @DocumentDate))
			)
			BEGIN
				SELECT @Message = 'El periodo actual de inventario no coincide con la fecha del documento, Periodo Actual de Inventario: ' + CONCAT(si.Year, '-', RIGHT('00' + CAST(si.Month AS VARCHAR), 2))
				FROM Inventory.SettingInventory si
				WHERE si.OperatingUnitId = @OperatingUnitId

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'No se ha creado una configuración para el modulo de inventarios.')
							PRINT @OperatingUnitId
							PRINT @OrderType
							PRINT @DeliveryDocumentDate
							PRINT @DocumentDate
				RETURN
			END
			
		

			-- Valido que se haya ingresado una descripcion
			IF @Description IS NULL
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Se debe ingresar una descripción.')
				RETURN
			END

			declare @countControlStore int, @SourceWarehouseConsignment Bit

			SELECT TOP 1 @SourceWarehouseConsignment = WarehouseConsignment FROM Inventory.Warehouse WITH(NOLOCK) WHERE Id = @SourceWarehouseId
			set @countControlStore = (SELECT Count(1) FROM Inventory.Warehouse WHERE Id In (@SourceWarehouseId, @TargetWarehouseId) AND Status = 1 AND ControlStore = 1)
			
			If @countControlStore <> 2 BEGIN

				-- Valido el almacen de origen				
				IF NOT EXISTS (SELECT 1 FROM Inventory.Warehouse 
					WHERE Id = @SourceWarehouseId AND Status = 1 
						AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0) Or @OrderType = 2)							
				BEGIN				
					SELECT @Message = 'El Almacén de Origen: ' + 
							IIF(Status = 0, CHAR(13) + CHAR(10) + 'Se encuentra inactivo', '') +
							IIF(VirtualStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén Virtual') +
							IIF(WarehouseConsignment = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de Consignación') +
							IIF(CustodyStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de Custodia') +
							IIF(TransitStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de tránsito') +
							IIF(ControlStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de control')
					FROM Inventory.Warehouse
					WHERE Id = @SourceWarehouseId

					SELECT @CodeResult = 999, 
							@MessageResult = ISNULL(@Message, 'El Almacén de Origen no existe.')
					RETURN
				END

				-- Valido el tipo de orden
				IF ISNULL(@OrderType, 0) NOT IN (1, 2, 3)
				BEGIN
					SELECT @CodeResult = 999, 
							@MessageResult = 'El tipo de Orden de Traslado no es válido.'
					RETURN
				END

				-- Valido la opción de despachar a
				IF (@OrderType IN (1, 3) AND ISNULL(@DispatchTo, 0) NOT IN (1)) OR (@OrderType = 2 AND ISNULL(@DispatchTo, 0) NOT IN (1, 2))
				BEGIN
					SELECT @CodeResult = 999, 
							@MessageResult = 'La opción Despachar a no es válida.'
					RETURN
				END

				--Valido el almacén de tránsito
				IF @OrderType = 3 AND NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @TransitWarehouseId AND Status = 1 AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 1))
				BEGIN
					SELECT @Message = 'El Almacén de Tránsito: ' +
							IIF(Status = 0, CHAR(13) + CHAR(10) + 'Se encuentra inactivo', '') +
							IIF(VirtualStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén Virtual') +
							IIF(WarehouseConsignment = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de Consignación') +
							IIF(CustodyStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de Custodia') +
							IIF(TransitStore = 1, '', CHAR(13) + CHAR(10) + Code + '-' + Name + ' no se encuentra parametrizado como tipo Almacén de tránsito')
					FROM Inventory.Warehouse
					WHERE Id = @TransitWarehouseId

					SELECT @CodeResult = 999, 
							@MessageResult = ISNULL(@Message, 'El Almacén de Tránsito no existe.')
					RETURN
				END

				--Valido el almacén de destino
				IF @DispatchTo = 1
				BEGIN
					IF NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @TargetWarehouseId AND Status = 1 AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0))
					BEGIN
						SELECT @Message = 'El Almacén de Destino: ' +
								IIF(Status = 0, CHAR(13) + CHAR(10) + 'Se encuentra inactivo', '') +
								IIF(VirtualStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén Virtual') +
								IIF(WarehouseConsignment = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de Consignación') +
								IIF(CustodyStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de Custodia') +
								IIF(TransitStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de tránsito') +
								IIF(ControlStore = 0, '', CHAR(13) + CHAR(10) + 'Es un Almacén de control')
						FROM Inventory.Warehouse
						WHERE Id = @TargetWarehouseId

						SELECT @CodeResult = 999, 
								@MessageResult = ISNULL(@Message, 'El Almacén de Destino no existe.')
						RETURN
					END

					IF @SourceWarehouseId = @TargetWarehouseId
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = 'El almacén de Origen no puede ser el mismo almacén de destino.'
						RETURN
					END
				END
			end

			--Valido el unidad funcional de destino
			IF @DispatchTo = 2 AND NOT EXISTS (SELECT 1 FROM Payroll.FunctionalUnit WHERE Id = @TargetFunctionalUnitId AND State = 1)
			BEGIN
				SELECT @Message = 'La Unidad Funcional de Destino: ' +
						IIF(State = 0, CHAR(13) + CHAR(10) + 'Se encuentra inactivo', '')
				FROM Payroll.FunctionalUnit
				WHERE Id = @TargetFunctionalUnitId

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'La Unidad Funcional de Destino no existe.')
				RETURN
			END

			--Valido el concepto
			IF NOT EXISTS (SELECT 1 from inventory.SettingInventory WHERE @OperatingUnitId = OperatingUnitId AND AssociateCostMainAccount = 2)
				BEGIN
				IF @OrderType = 2 AND NOT EXISTS (SELECT 1 FROM Inventory.AdjustmentConcept WHERE Id = @AdjustmentConceptId AND Status = 1)
				BEGIN
					SELECT @Message = 'El Concepto: ' +
							IIF(Status = 0, CHAR(13) + CHAR(10) + 'Se encuentra inactivo', '')
					FROM Inventory.AdjustmentConcept
					WHERE Id = @AdjustmentConceptId

					SELECT @CodeResult = 999, 
							@MessageResult = ISNULL(@Message, 'El Concepto no existe.')
					RETURN
				END

				--Valido el tercero
				IF @OrderType = 2 AND @TakeTransferOrderThirdParty = 1 AND @ThirdPartyId IS NULL AND NOT EXISTS 
				(
					SELECT 1 
					FROM Inventory.AdjustmentConcept ac 
					JOIN GeneralLedger.MainAccounts ma ON ac.AdjustmentAccountId = ma.Id 
					WHERE ac.Id = @AdjustmentConceptId AND ma.HandlesThirdParty = 1
				)
				BEGIN
					SELECT @CodeResult = 999, 
							@MessageResult = ISNULL(@Message, 'La cuenta contable del concepto maneja tercero y este no fue parametrizado.')
					RETURN
				END
			END

			/******************************** *************************************** ********************************/

			IF (@OrderType = 3 AND @Status = 2)
			BEGIN
				/********************************  VALIDACIONES CONFIRMACIÓN TRANSITO ********************************/

				-- Valido que el inventario físico tenga las cantidades suficientes
				IF EXISTS
				(
					SELECT 1 
					FROM Inventory.TransferOrderDetail tod
					JOIN Inventory.TransferOrderDetailBatchSerial todbs ON tod.Id = todbs.TransferOrderDetailId
					JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
					LEFT JOIN Inventory.PhysicalInventory phys ON phys.WarehouseId = @TransitWarehouseId AND phy.ProductId = phys.ProductId AND ISNULL(phy.BatchSerialId, 0) = ISNULL(phys.BatchSerialId, 0)
					WHERE tod.TransferOrderId = @Id AND todbs.Quantity > ISNULL(phys.Quantity, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ', ip.Name, ' (Lote: ', bs.BatchCode, '). Cantidad disponible: ', ISNULL(phys.Quantity, 0), ' - Cantidad Solicitada: ', todbs.Quantity)
							FROM Inventory.TransferOrderDetail tod
							JOIN Inventory.TransferOrderDetailBatchSerial todbs ON tod.Id = todbs.TransferOrderDetailId
							JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
							LEFT JOIN Inventory.PhysicalInventory phys ON phys.WarehouseId = @TransitWarehouseId AND phy.ProductId = phys.ProductId AND ISNULL(phy.BatchSerialId, 0) = ISNULL(phys.BatchSerialId, 0)
							LEFT JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
							LEFT JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
							WHERE tod.TransferOrderId = @Id AND todbs.Quantity > ISNULL(phys.Quantity, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'Los siguientes productos no tienen la cantidad suficiente: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END

				/**************************************** ACTUALIZAR CABECERA ****************************************/

				UPDATE [Inventory].[TransferOrder]
					SET [Status] = @Status,
						[ConfirmationUser] = @CodeUser,
						[ConfirmationDate] = [Common].[GETDATE]()
				WHERE Id = @Id

				/****************************** *************************************** ******************************/

				SET @SourceWarehouseId = @TransitWarehouseId
			END
			ELSE
			BEGIN
				/************************************ VALIDACIONES OTROS PROCESOS ************************************/
				
				--Se obtiene los subdetalles que vienen en el xml
				INSERT INTO @DetailBatchSerial
					SELECT	t.x.value('TransferOrderDetailIdTmp[1]','int'),
							t.x.value('Id[1]','int'),
							t.x.value('TransferOrderDetailId[1]','int'),
							t.x.value('PhysicalInventoryId[1]','int'),
							t.x.value('Quantity[1]','int'),
							t.x.value('ChangeTracker[1]','varchar(30)')
					FROM @TransferOrderXml.nodes('/TransferOrder/TransferOrderDetail/TransferOrderDetailBatchSerial') t(x)

				--se eliminan los subdetalles marcados para su eliminación
				DELETE todbs
				FROM Inventory.TransferOrderDetail tod
				JOIN Inventory.TransferOrderDetailBatchSerial todbs ON tod.Id = todbs.TransferOrderDetailId
				JOIN @DetailBatchSerial dbs ON todbs.Id = dbs.Id AND tod.Id = dbs.TransferOrderDetailId
				WHERE @Id = tod.TransferOrderId AND dbs.ChangeTracker = 'Deleted'

				DELETE dbs FROM @DetailBatchSerial dbs WHERE dbs.ChangeTracker = 'Deleted'

				--Se obtiene los detalles que vienen en el xml
				INSERT INTO @Detail
					SELECT	t.x.value('TransferOrderDetailIdTmp[1]','int'),
							t.x.value('Id[1]','int'),
							t.x.value('TransferOrderId[1]','int'),
							t.x.value('InventoryRequestDetailId[1]','int'),
							t.x.value('InventoryRequestDetailOtherId[1]','int'),
							t.x.value('ProductId[1]','int'),
							t.x.value('InventoryQuantity[1]','int'),
							t.x.value('Quantity[1]','int'),
							t.x.value('Description[1]','varchar(max)'),
							t.x.value('ChangeTracker[1]','varchar(30)')
					FROM @TransferOrderXml.nodes('/TransferOrder/TransferOrderDetail') t(x)

				--se eliminan los detalles marcados para su eliminación
				DELETE tod
				FROM Inventory.TransferOrderDetail tod
				JOIN @Detail d ON tod.Id = d.Id
				WHERE @Id = tod.TransferOrderId AND d.ChangeTracker = 'Deleted'

				DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'

				/***************************************  VALIDACIONES DETALLE ***************************************/

				--- Valido que existan detalles
				IF NOT EXISTS (SELECT 1 FROM @Detail)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'La Orden de Traslado no tiene detalles.'
					RETURN
				END
			
				-- Valido que los registros editados no hayan cambiado sus valores base
				IF EXISTS 
				(
					SELECT 1 
					FROM @Detail d
					LEFT JOIN Inventory.TransferOrderDetail tod ON d.Id = tod.Id
					WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(tod.TransferOrderId, 0) OR ISNULL(d.InventoryRequestDetailId, 0) <> ISNULL(tod.InventoryRequestDetailId, 0) OR ISNULL(d.ProductId, 0) <> ISNULL(tod.ProductId, 0))
				) 
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Los detalles de la Orden de Traslado han sido alterados.'
					RETURN
				END

				-- Valido que los registros editados no hayan cambiado sus valores base
				IF EXISTS 
				(
					SELECT 1 
					FROM @Detail d
					LEFT JOIN Inventory.TransferOrderDetail tod ON d.Id = tod.Id
					WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(tod.TransferOrderId, 0) OR ISNULL(d.InventoryRequestDetailOtherId, 0) <> ISNULL(tod.InventoryRequestDetailOtherId, 0) OR ISNULL(d.ProductId, 0) <> ISNULL(tod.ProductId, 0))
				) 
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Los detalles de la Orden de Traslado han sido alterados.'
					RETURN
				END

				-- Valido que no hayan quedado detalles sin procesar
				IF EXISTS 
				(
					SELECT 1 
					FROM Inventory.TransferOrderDetail tod
					LEFT JOIN @Detail d ON d.Id = tod.Id
					WHERE tod.TransferOrderId = @Id AND d.Id IS NULL
				) 
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existen detalles de la Orden de Traslado que no han sido procesados.'
					RETURN
				END

				-- Valido que no existan detalles duplicados
				IF EXISTS
				(
					SELECT 1 
					FROM @Detail d
					GROUP BY d.InventoryRequestDetailId, d.ProductId
					HAVING COUNT(1) > 1
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existen detalles duplicados (El mismo producto con la misma solicitud).'
					RETURN
				END

				-- Valido que no existan detalles duplicados
				IF EXISTS
				(
					SELECT 1 
					FROM @Detail d
					GROUP BY d.InventoryRequestDetailOtherId, d.ProductId
					HAVING COUNT(1) > 1
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existen detalles duplicados (El mismo producto con la misma solicitud).'
					RETURN
				END

				-- Valido que no existan detalles con cantidad inválida
				IF EXISTS
				(
					SELECT 1 
					FROM @Detail d
					WHERE d.Quantity <= 0
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existen detalles con cantidades inválidas.'
					RETURN
				END

				-- Valido la cantidad de los detalles corresponda con la de sus subdetalles
				IF EXISTS
				(
					SELECT 1 
					FROM @Detail d
					LEFT JOIN
					(
						SELECT TransferOrderDetailIdTmp, SUM(dbs.Quantity) Quantity
						FROM @DetailBatchSerial dbs
						GROUP BY dbs.TransferOrderDetailIdTmp
					) dbs ON d.TransferOrderDetailIdTmp = dbs.TransferOrderDetailIdTmp
					WHERE d.Quantity <> ISNULL(dbs.Quantity, 0)
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'La cantidad de los detalles no corresponde con la de su información relacionada.'
					RETURN
				END

				-- Valido que los productos del detalle tengan costo promedio mayor a 0(si el almacen es de control se excluye)
				if  NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @SourceWarehouseId AND ControlStore = 1)
				begin
					IF EXISTS
					(
						SELECT 1 
						FROM @Detail d
						LEFT JOIN Inventory.InventoryProduct ip ON ip.id = d.ProductId
						WHERE ISNULL(ip.ProductCost, 0) = 0 
					)
					BEGIN
				
						SELECT @CodeResult = 999, 
							   @MessageResult = 'Existen productos con costo promedio en 0'
						RETURN
					END
				END

		IF @PermissionValidateQuantity = 0
		BEGIN
			-- CTE para consolidar cantidades y evitar recalcular
			;WITH Detail_Quantities_CTE AS (
				SELECT InventoryRequestDetailId, SUM(Quantity) AS Quantity
				FROM @Detail
				GROUP BY InventoryRequestDetailId
			)
			-- Valido la cantidad no supere lo solicitado (InventoryRequestDetail)
			SELECT TOP 1 @Message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ', ip.Name, ' (Solicitud: ', ir.Code, '). Cantidad disponible: ', ird.OutstandingQuantity, ' - Cantidad Solicitada: ', d.Quantity)
				FROM Inventory.InventoryRequest ir
				JOIN Inventory.InventoryRequestDetail ird ON ir.Id = ird.InventoryRequestId
				JOIN Detail_Quantities_CTE d ON ird.Id = d.InventoryRequestDetailId
				JOIN Inventory.InventoryProduct ip ON ird.InventoryProductId = ip.Id
				WHERE ird.OutstandingQuantity < d.Quantity
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			FROM Inventory.InventoryRequestDetail ird
			JOIN Detail_Quantities_CTE d ON ird.Id = d.InventoryRequestDetailId
			WHERE ird.OutstandingQuantity < d.Quantity

			IF @Message IS NOT NULL
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los siguientes productos superan la cantidad pendiente de la solicitud: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			-- CTE para consolidar cantidades Other
			;WITH Detail_Quantities_Other_CTE AS (
				SELECT InventoryRequestDetailOtherId, SUM(Quantity) AS Quantity
				FROM @Detail
				GROUP BY InventoryRequestDetailOtherId
			)
			-- Valido la cantidad no supere lo solicitado (InventoryRequestDetailOther)
			SELECT TOP 1 @Message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ', ip.Name, ' (Solicitud: ', ir.Code, '). Cantidad disponible: ', irdo.OutstandingQuantity, ' - Cantidad Solicitada: ', d.Quantity)
				FROM Inventory.InventoryRequest ir
				JOIN Inventory.InventoryRequestDetailOther irdo ON ir.Id = irdo.InventoryRequestId
				JOIN Detail_Quantities_Other_CTE d ON irdo.Id = d.InventoryRequestDetailOtherId
				JOIN Inventory.InventoryProduct ip ON irdo.InventoryProductId = ip.Id
				WHERE irdo.OutstandingQuantity < d.Quantity
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			FROM Inventory.InventoryRequestDetailOther irdo
			JOIN Detail_Quantities_Other_CTE d ON irdo.Id = d.InventoryRequestDetailOtherId
			WHERE irdo.OutstandingQuantity < d.Quantity

			IF @Message IS NOT NULL
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los siguientes productos superan la cantidad pendiente de la solicitud: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END
		END

				/*************************************  VALIDACIONES SUB-DETALLE *************************************/

				--- Valido que existan detalles
				IF NOT EXISTS (SELECT 1 FROM @DetailBatchSerial)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'La Orden de Traslado no tiene información asociada al detalle.'
					RETURN
				END

				-- Valido que los registros editados no hayan cambiado sus valores base
				IF EXISTS 
				(
					SELECT 1 
					FROM @DetailBatchSerial dbs
					LEFT JOIN Inventory.TransferOrderDetailBatchSerial todbs ON dbs.Id = todbs.Id
					LEFT JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
					WHERE dbs.ChangeTracker <> 'Added' AND (@Id <> ISNULL(tod.TransferOrderId, 0) OR ISNULL(dbs.PhysicalInventoryId, 0) <> ISNULL(todbs.PhysicalInventoryId, 0))
				) 
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'La información asociada a los detalles de la Orden de Traslado han sido alterados.'
					RETURN
				END

				-- Valido que no hayan quedado subdetalles sin procesar
				IF EXISTS 
				(
					SELECT 1 
					FROM Inventory.TransferOrderDetail tod
					JOIN Inventory.TransferOrderDetailBatchSerial todbs ON tod.Id = todbs.TransferOrderDetailId
					LEFT JOIN @DetailBatchSerial dbs ON todbs.Id = dbs.Id
					WHERE tod.TransferOrderId = @Id AND dbs.Id IS NULL
				) 
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existe información asociada al detalle de la Orden de Traslado que no han sido procesados.'
					RETURN
				END

				-- Valido que no existan detalles duplicados
				IF EXISTS
				(
					SELECT 1 
					FROM @DetailBatchSerial dbs
					GROUP BY dbs.PhysicalInventoryId
					HAVING COUNT(1) > 1
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existe información asociada al detalle duplicada (El mismo inventario físico).'
					RETURN
				END

				-- Valido que no existan detalles sin cantidad
				IF EXISTS
				(
					SELECT 1 
					FROM @DetailBatchSerial dbs
					WHERE dbs.Quantity <= 0
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existe información asociada al detalle con cantidades inválidas.'
					RETURN
				END

				-- Valido que el inventario físico corresponda con el del producto
				IF EXISTS
				(
					SELECT 1 
					FROM @Detail d
					JOIN @DetailBatchSerial dbs ON d.TransferOrderDetailIdTmp = dbs.TransferOrderDetailIdTmp
					LEFT JOIN Inventory.PhysicalInventory phy ON dbs.PhysicalInventoryId = phy.Id
					WHERE ISNULL(@SourceWarehouseId, 0) <> ISNULL(phy.WarehouseId, 0) OR ISNULL(d.ProductId, 0) <> ISNULL(phy.ProductId, 0)
				)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Existe información asociada cuyo inventario físico no concuerda con la información del detalle.'
					RETURN
				END

				-- Valido que el inventario físico tenga las cantidades suficientes
				IF EXISTS
				(
					SELECT 1 
					FROM @DetailBatchSerial dbs
					JOIN Inventory.PhysicalInventory phy ON dbs.PhysicalInventoryId = phy.Id
					WHERE dbs.Quantity > phy.Quantity
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ', ip.Name, ' (Lote: ', bs.BatchCode, '). Cantidad disponible: ', phy.Quantity, ' - Cantidad Solicitada: ', dbs.Quantity)
							FROM @DetailBatchSerial dbs
							JOIN Inventory.PhysicalInventory phy ON dbs.PhysicalInventoryId = phy.Id
							JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
							LEFT JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
							WHERE dbs.Quantity > phy.Quantity
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'Los siguientes productos no tienen la cantidad suficiente: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END

				/**********************************  INSERTAR / ACTUALIZAR CABECERA **********************************/

				DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
				DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

				IF @Id = 0
				BEGIN
					--Si se esta insertando por primera vez se consulta la secuencia numerica
					IF @Code = '' 
					BEGIN
						--Si se esta insertando por primera vez se consulta la secuencia numerica
						DECLARE @IsManual BIT
				
						EXEC Common.SP_GetSequence 190, @IdForm, @OperatingUnitId, @Prefix, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

						IF @Code_Output <> 0
						BEGIN
							SELECT	@CodeResult = 999, 
									@MessageResult = REPLACE(@Message_Output, '{0}', 'Orden de Traslado')
							RETURN
						END
						--Se inserta la cabecera

						INSERT INTO [Inventory].[TransferOrder]
						(
							[Code],[OperatingUnitId],[DocumentDate],[OrderType],[DispatchTo],[Description],[SourceWarehouseId],
							[TransitWarehouseId],[TargetWarehouseId],[TargetFunctionalUnitId],[AdjustmentConceptId],[ThirdPartyId],
							[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
						)
						SELECT	@Code,@OperatingUnitId,@DocumentDate,@OrderType,@DispatchTo,@Description,@SourceWarehouseId,
								@TransitWarehouseId,@TargetWarehouseId,@TargetFunctionalUnitId,@AdjustmentConceptId,@ThirdPartyId,
								@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

						--Obtengo el id de la cabcera
						SET @Id = SCOPE_IDENTITY()
					END
				END
				ELSE --Si se esta actualizando
				BEGIN
					UPDATE [Inventory].[TransferOrder]
						SET [Code] = @Code,
							[OperatingUnitId] = @OperatingUnitId,
							[DocumentDate] = @DocumentDate,
							[OrderType] = @OrderType,
							[DispatchTo] = @DispatchTo,
							[Description] = @Description,
							[SourceWarehouseId] = @SourceWarehouseId,
							[TransitWarehouseId] = @TransitWarehouseId,
							[TargetWarehouseId] = @TargetWarehouseId,
							[TargetFunctionalUnitId] = @TargetFunctionalUnitId,
							[AdjustmentConceptId] = @AdjustmentConceptId,
							[ThirdPartyId] = @ThirdPartyId,
							[Status] = @Status,
							[ModificationUser] = @CodeUser,
							[ModificationDate] = [Common].[GETDATE](),
							[ConfirmationUser] = @ConfirmationUser,
							[ConfirmationDate] = @ConfirmationDate
					WHERE Id = @Id
				END

				/*********************************** INSERTAR / ACTUALIZAR DETALLE ***********************************/

				INSERT INTO Inventory.TransferOrderDetail
				(
					TransferOrderId, InventoryRequestDetailId,InventoryRequestDetailOtherId, ProductId, InventoryQuantity, Quantity, Value, Description
				)
				SELECT	@Id, d.InventoryRequestDetailId,d.InventoryRequestDetailOtherId, d.ProductId, d.InventoryQuantity, d.Quantity, ip.ProductCost, d.Description
				FROM @Detail d
				JOIN Inventory.InventoryProduct ip ON d.ProductId = ip.Id
				WHERE d.ChangeTracker = 'Added'

				UPDATE d
					SET d.Id = tod.Id
				FROM @Detail d
				JOIN Inventory.TransferOrderDetail tod ON d.ProductId = tod.ProductId AND ISNULL(d.InventoryRequestDetailId, 0) = ISNULL(tod.InventoryRequestDetailId, 0)
				WHERE tod.TransferOrderId = @Id AND d.ChangeTracker = 'Added'

				UPDATE tod
					SET tod.InventoryQuantity = d.InventoryQuantity,
						tod.Quantity = d.Quantity,
						tod.Value = ip.ProductCost,
						tod.Description = d.Description
				FROM @Detail d
				JOIN Inventory.TransferOrderDetail tod ON d.Id = tod.Id
				JOIN Inventory.InventoryProduct ip ON d.ProductId = ip.Id
				WHERE tod.TransferOrderId = @Id AND d.ChangeTracker <> 'Added'

				UPDATE dbs
					SET dbs.TransferOrderDetailId = d.Id
				FROM @Detail d
				JOIN @DetailBatchSerial dbs ON d.TransferOrderDetailIdTmp = dbs.TransferOrderDetailIdTmp

				/********************************* INSERTAR / ACTUALIZAR SUB-DETALLE *********************************/

				INSERT INTO Inventory.TransferOrderDetailBatchSerial
				(
					TransferOrderDetailId, PhysicalInventoryId, Quantity, OutstandingQuantity
				)
				SELECT	dbs.TransferOrderDetailId, dbs.PhysicalInventoryId, dbs.Quantity, dbs.Quantity
				FROM @DetailBatchSerial dbs
				WHERE dbs.ChangeTracker = 'Added'

				UPDATE todbs
					SET todbs.Quantity = dbs.Quantity,
						todbs.OutstandingQuantity = dbs.Quantity
				FROM @DetailBatchSerial dbs
				JOIN Inventory.TransferOrderDetailBatchSerial todbs ON dbs.Id = todbs.Id
				JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
				WHERE tod.TransferOrderId = @Id AND dbs.ChangeTracker <> 'Added'

				/****************************** *************************************** ******************************/

				SET @TargetWarehouseId = IIF(@OrderType = 3, @TransitWarehouseId, @TargetWarehouseId)
			END

			/*********************************************  CONFIRMACIÓN *********************************************/

			IF @Status IN (2, 4)
			BEGIN
				/**************************  ACTUALIZAR CANTIDAD DISPONIBLE DE LA SOLICITUD **************************/

				IF (@OrderType = 3 AND @Status = 2)
				BEGIN
					SET @DocumentDate = @DeliveryDocumentDate
				END
				ELSE
				BEGIN
					UPDATE ird
						SET ird.OutstandingQuantity -= d.Quantity
					FROM Inventory.InventoryRequestDetail ird
					JOIN
					(
						SELECT InventoryRequestDetailId, SUM(Quantity) Quantity
						FROM @Detail
						GROUP BY InventoryRequestDetailId
					) d ON ird.Id = d.InventoryRequestDetailId

					UPDATE irdo
						SET irdo.OutstandingQuantity -= d.Quantity
					FROM Inventory.InventoryRequestDetailOther irdo
					JOIN
					(
						SELECT InventoryRequestDetailOtherId, SUM(Quantity) Quantity
						FROM @Detail
						GROUP BY InventoryRequestDetailOtherId
					) d ON irdo.Id = d.InventoryRequestDetailOtherId
				END

				/***************************** MOVIMIENTO KARDEX - SALIDA ALMACEN ORIGEN *****************************/

				--Generamos el XML para consumir el SP encargado del movimiento del kardex
				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT Kardex.*
						FROM 
						( 
							SELECT	@DocumentDate DocumentDate,
									2 MovementType,
									@SourceWarehouseId WarehouseId,
									phy.ProductId,
									phy.BatchSerialId BatchSerialId,
									todbs.Quantity Quantity,
									tod.Value Value,
									0 AffectAverageCost
							FROM Inventory.TransferOrderDetail tod
							JOIN Inventory.TransferOrderDetailBatchSerial todbs ON tod.Id = todbs.TransferOrderDetailId
							JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
							WHERE tod.TransferOrderId = @Id
						) Kardex
						FOR XML AUTO,TYPE, ELEMENTS
					)
				)

				EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'TransferOrder', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT @CodeResult = 999, 
							@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex con el movimiento de salida del almacén de origen.')
					RETURN
				END

				If @SourceWarehouseConsignment = 1 Begin
					--- Actualizo las remisiones de inventario en consignación si las hubiera
					declare @RemissionXml xml = (
						select pd.Id AS EntityDetailId
							, @OperatingUnitId AS OperatingUnitId
							, @TargetFunctionalUnitId AS FunctionalUnitId
							, pd.ProductId
							, phy.BatchSerialId
							, 2 as MovementType
							, @SourceWarehouseId AS WarehouseId
							, bat.Quantity
							, ip.ProductCost as Value
						FROM @Detail pd
						INNER JOIN @DetailBatchSerial bat on pd.Id = bat.TransferOrderDetailId
						INNER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.Id = bat.PhysicalInventoryId						
						INNER JOIN Inventory.InventoryProduct ip With(Nolock) on ip.Id = pd.ProductId 
						WHERE pd.ChangeTracker <> @Deleted and bat.ChangeTracker <> @Deleted
						for xml path('Remission'), elements
					)
					
					IF @RemissionXml IS NOT NULL BEGIN
						DECLARE @MessageReturnRemission VARCHAR(MAX)
						EXEC [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission] 
							@RemissionXml, 
							@Id, 
							@Code, 
							'TransferOrder', 
							@CodeUser, 
							@MessageReturnRemission OUTPUT
						
						IF ISNULL(@MessageReturnRemission, '') <> '' BEGIN

							SELECT	@CodeResult = 999, 
									@MessageResult = @MessageReturnRemission
							RETURN
						END
					END
				End

				IF @OrderType IN (1, 3) AND @DispatchTo = 1
				BEGIN

					/************************** MOVIMIENTO KARDEX - ENTRADA ALMACEN DESTINO **************************/

					--Generamos el XML para consumir el SP encargado del movimiento del kardex
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT Kardex.*
							FROM 
							( 
								SELECT	@DocumentDate DocumentDate,
										1 MovementType,
										@TargetWarehouseId WarehouseId,
										phy.ProductId,
										phy.BatchSerialId BatchSerialId,
										todbs.Quantity Quantity,
										tod.Value Value,
										0 AffectAverageCost
								FROM Inventory.TransferOrderDetail tod
								JOIN Inventory.TransferOrderDetailBatchSerial todbs ON tod.Id = todbs.TransferOrderDetailId
								JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
								WHERE tod.TransferOrderId = @Id
							) Kardex
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'TransferOrder', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex con el movimiento de entrada del almacén de destino.')
						RETURN
					END
				END

				/******************************************* VALIDAR STOCK *******************************************/
				
				DECLARE @MessageResultAuxTable AS TABLE(Id INT IDENTITY(1,1),messageResultAuxTmp VARCHAR(MAX))
				IF @StockControl = 1
				BEGIN
					-- validamos stock por producto
					--SELECT @MessageResultAux = STUFF((
					INSERT INTO @MessageResultAuxTable
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El producto ', ip.Code, ' - ', ip.Name, ' alcanzó su stock ', IIF(ip.MinimumStock > ISNULL(phy.Quantity, 0), 'mínimo', 'máximo'))
						FROM Inventory.TransferOrderDetail tod
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						LEFT JOIN
						(
							SELECT ProductId, SUM(Quantity) Quantity
							FROM Inventory.PhysicalInventory
							GROUP BY ProductId
						) phy ON tod.ProductId = phy.ProductId
						WHERE tod.TransferOrderId = @Id AND (ip.MinimumStock > ISNULL(phy.Quantity, 0) OR ip.MaximumStock < ISNULL(phy.Quantity, 0))
						--XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				END
				ELSE IF @StockControl = 2
				BEGIN
					-- validamos stock por almacen
					--SELECT @MessageResultAux = STUFF((
					INSERT INTO @MessageResultAuxTable
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El producto ', ip.Code, ' - ', ip.Name, ' alcanzó su stock ', IIF(ip.MinimumStock > ISNULL(phy.Quantity, 0), 'mínimo', 'máximo'))
						FROM Inventory.TransferOrderDetail tod
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						LEFT JOIN
						(
							SELECT ProductId, SUM(Quantity) Quantity
							FROM Inventory.PhysicalInventory
							WHERE WarehouseId = @SourceWarehouseId
							GROUP BY ProductId
						) phy ON tod.ProductId = phy.ProductId
						WHERE tod.TransferOrderId = @Id AND (ip.MinimumStock > ISNULL(phy.Quantity, 0) OR ip.MaximumStock < ISNULL(phy.Quantity, 0))
						--FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
						
				END

				IF @OrderType IN (1, 3)
				BEGIN
					IF @StockControl = 2
					BEGIN
						-- validamos stock por almacen
						--SELECT @MessageResultAux = ISNULL(@MessageResultAux, '') + STUFF((
						INSERT INTO @MessageResultAuxTable
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El producto ', ip.Code, ' - ', ip.Name, ' alcanzó su stock ', IIF(ip.MinimumStock > ISNULL(phy.Quantity, 0), 'mínimo', 'máximo'))
							FROM Inventory.TransferOrderDetail tod
							JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
							LEFT JOIN
							(
								SELECT ProductId, SUM(Quantity) Quantity
								FROM Inventory.PhysicalInventory
								WHERE WarehouseId = @TargetWarehouseId
								GROUP BY ProductId
							) phy ON tod.ProductId = phy.ProductId
							WHERE tod.TransferOrderId = @Id AND (ip.MinimumStock > ISNULL(phy.Quantity, 0) OR ip.MaximumStock < ISNULL(phy.Quantity, 0))
							--FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					END
				END

				DECLARE @MessageDX AS TABLE(Id INT IDENTITY(1,1),messageResultAux VARCHAR(MAX))							
				INSERT INTO @MessageDX
				SELECT 
				STUFF(( SELECT Distinct(B.messageResultAuxTmp) 
				FROM @MessageResultAuxTable B
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@MessageResultAux = messageResultAux
			FROM @MessageDX
								
				/**************************************** MOVIMIENTO CONTABLE ****************************************/

				IF @Status = 2 
					-- Si es un almacen de control no debe contabilizar
					AND NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @SourceWarehouseId AND ControlStore = 1)
				BEGIN
				
					--Si se tiene seleccionada la opcion de 'grupo de inventario' en los parametros de inventario
					IF EXISTS (SELECT 1 from inventory.SettingInventory WHERE @OperatingUnitId = OperatingUnitId AND AssociateCostMainAccount = 2) 
						AND @OrderType = 2 AND @DispatchTo = 2 
					BEGIN

						SELECT	@SourceCostCenterId = w.CostCenterId
						FROM Inventory.TransferOrder tro
						JOIN Inventory.Warehouse w ON tro.SourceWarehouseId = w.Id
						WHERE tro.Id = @Id

						SELECT	@TargetCostCenterId = CostCenterId
						FROM Payroll.FunctionalUnit
						WHERE Id = @TargetFunctionalUnitId AND @DispatchTo = 2

						SELECT @SubXml = CONVERT
						(
							XML, 
							(
								SELECT *
								FROM 
								(
									SELECT	0 Id,
											0 Consecutive,
											@JournalVoucherTypeId IdJournalVoucher, 									
											@DocumentDate VoucherDate, 
											'False' Imported,
											2 Status,
											@Description Detail, 
											'TransferOrder' EntityName,
											@Code EntityCode,
											@Id EntityId,
											0 IsClosedYear
								) JournalVoucher
								JOIN
							( 
									SELECT 
											0 Id,
											0 IdAccounting,
											pgf.CostAccountId IdMainAccount,
											CASE WHEN ma.HandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END IdThirdParty, 
											CASE WHEN ma.HandlesCostCenter = 1 THEN @TargetCostCenterId ELSE NULL END IdCostCenter, 									
											tod.Quantity * tod.Value DebitValue, 
											0 CreditValue
									FROM Inventory.TransferOrderDetail tod
									JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
									JOIN Inventory.ProductGroup pg ON pg.Id = ip.ProductGroupId
									JOIN Inventory.ProductGroupFunctionalUnit pgf ON pgf.ProductGroupId = pg.Id
									JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pgf.CostAccountId
									WHERE tod.TransferOrderId = @Id	and pgf.FunctionalUnitId = @TargetFunctionalUnitId
								UNION ALL
									SELECT	0 Id,
											0 IdAccounting,
											ma.Id IdMainAccount,
											CASE WHEN ma.HandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END IdThirdParty, 
											CASE WHEN ma.HandlesCostCenter = 1 THEN @TargetCostCenterId ELSE NULL END IdCostCenter, 	
											0 DebitValue, 
											tod.Quantity * tod.Value CreditValue
									FROM Inventory.TransferOrderDetail tod
									JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
									JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
									LEFT JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
									LEFT JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
									WHERE tod.TransferOrderId = @Id	And @SourceWarehouseConsignment = 0	
								UNION ALL
									SELECT	0 Id,
											0 IdAccounting,
											ma.Id IdMainAccount,
											CASE WHEN ma.HandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END IdThirdParty, 
											CASE WHEN ma.HandlesCostCenter = 1 THEN @TargetCostCenterId ELSE NULL END IdCostCenter, 	
											0 DebitValue, 
											tod.Quantity * tod.Value CreditValue
									FROM Inventory.TransferOrderDetail tod
									JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
									JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
									LEFT JOIN GeneralLedger.MainAccounts ma ON pg.CounterpartCostConsignedInventoryId = ma.Id
									WHERE tod.TransferOrderId = @Id	And @SourceWarehouseConsignment = 1
								) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
								For xml AUTO,TYPE, ELEMENTS
							)
						)
					END
					ELSE BEGIN
						SELECT	@SourceCostCenterId = w.CostCenterId,
								@ThirdPartyId = IIF(@OrderType IN (1, 3) OR @TakeTransferOrderThirdParty <> 1, @TransferOrderThirdPartyId, @ThirdPartyId)
						FROM Inventory.TransferOrder tro
						JOIN Inventory.Warehouse w ON tro.SourceWarehouseId = w.Id
						WHERE tro.Id = @Id

						SELECT	@TargetCostCenterId = CostCenterId
						FROM Inventory.Warehouse
						WHERE Id = @TargetWarehouseId AND @DispatchTo = 1

						SELECT	@TargetCostCenterId = CostCenterId
						FROM Payroll.FunctionalUnit
						WHERE Id = @TargetFunctionalUnitId AND @DispatchTo = 2

						SELECT @SubXml = CONVERT
						(
							XML, 
							(
								SELECT *
								FROM 
								(
									SELECT	0 Id,
											0 Consecutive,
											@JournalVoucherTypeId IdJournalVoucher, 									
											@DocumentDate VoucherDate, 
											'False' Imported,
											2 Status,
											@Description Detail, 
											'TransferOrder' EntityName,
											@Code EntityCode,
											@Id EntityId,
											0 IsClosedYear,
											@OperatingUnitId OperatingUnitId 
								) JournalVoucher
								JOIN
								( 
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, @SourceCostCenterId, NULL) IdCostCenter, 									
												0 DebitValue, 
												tod.Quantity * tod.Value CreditValue
										FROM Inventory.TransferOrderDetail tod WITH(NOLOCK)
										JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON tod.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON ip.ProductGroupId = pg.Id
										LEFT JOIN Payments.AccountPayableConcepts apc WITH(NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
										LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON apc.IdAccount = ma.Id
										WHERE tod.TransferOrderId = @Id And @SourceWarehouseConsignment = 0
									UNION ALL
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, @SourceCostCenterId, NULL) IdCostCenter, 									
												0 DebitValue, 
												tod.Quantity * tod.Value CreditValue
										FROM Inventory.TransferOrderDetail tod WITH(NOLOCK)
										JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON tod.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON ip.ProductGroupId = pg.Id
										LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON pg.CounterpartCostConsignedInventoryId = ma.Id
										WHERE tod.TransferOrderId = @Id And @SourceWarehouseConsignment = 1
									UNION ALL
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, @TargetCostCenterId, NULL) IdCostCenter, 									
												tod.Quantity * tod.Value DebitValue, 
												0 CreditValue
										FROM Inventory.TransferOrderDetail tod WITH(NOLOCK) 
										JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON tod.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON ip.ProductGroupId = pg.Id
										LEFT JOIN Payments.AccountPayableConcepts apc WITH(NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
										LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON apc.IdAccount = ma.Id
										WHERE tod.TransferOrderId = @Id AND @OrderType IN (1, 3)
									UNION ALL
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, @TargetCostCenterId, NULL) IdCostCenter, 									
												tod.Quantity * tod.Value DebitValue, 
												0 CreditValue
										FROM Inventory.TransferOrderDetail tod WITH(NOLOCK)
										LEFT JOIN Inventory.AdjustmentConcept ac WITH(NOLOCK) ON @AdjustmentConceptId = ac.Id
										LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ac.AdjustmentAccountId = ma.Id
										WHERE tod.TransferOrderId = @Id AND @OrderType = 2 AND @SourceWarehouseConsignment = 0									
									UNION ALL
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, @TargetCostCenterId, NULL) IdCostCenter, 									
												tod.Quantity * tod.Value DebitValue, 
												0 CreditValue
										FROM Inventory.TransferOrderDetail tod (NOLOCK)
										JOIN Inventory.TransferOrder tor (NOLOCK) on tod.TransferOrderId = tor.Id
										JOIN Inventory.AdjustmentConcept ac (NOLOCK) on tor.AdjustmentConceptId = ac.Id
										JOIN GeneralLedger.MainAccounts ma (NOLOCK) ON ma.Id = ac.AdjustmentAccountId
										WHERE tod.TransferOrderId = @Id AND @OrderType = 2 AND @SourceWarehouseConsignment = 1
								) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
								For xml AUTO,TYPE, ELEMENTS
							)
						)
					END

					--Se consume el sp que guarda el movimiento contable
					insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodeUser 

					select 
					@Code_Output = rjv.code, 
					@Message_Output = rjv.MessageResult, 
					@JournalVoucherId = rjv.IdJournalVoucher
					from @resultJournalVoucher rjv
					
					IF ISNULL(@Code_Output, 999) <> 0 
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'Comprobante contable no generado')
						RETURN 
					END

					SELECT	@Message = 'Se guardó y confirmó el comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(20)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name
					FROM GeneralLedger.JournalVouchers jv
					LEFT JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
					WHERE jv.Id = @JournalVoucherId
				END
			END
		END

		IF @Status = 1 OR @Status = 4
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Inventory.InventoryControlDocument (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentTypeControl, @CodeUser, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y entregó la Orden de Traslado con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Orden de Traslado con código ', @Code)
				   WHEN 4 THEN CONCAT('Se creó en tránsito la Orden de Traslado con código ', @Code)
				   ELSE CONCAT('Se guardó la Orden de Traslado con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SaveTransferOrder_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea, actualiza, confirma o anula órdenes de traslado de inventario entre bodegas, unidades funcionales o terceros. Gestiona el ciclo completo de la orden de traslado: valida el período contable, controla el stock disponible en bodega origen mediante el inventario físico (PhysicalInventory), registra el movimiento contable asociado (comprobante de despacho) y actualiza el estado de la orden (Registrado, En Tránsito, Entregado, Anulado). Toma los parámetros de configuración del módulo de inventario (SettingInventory) para determinar el tercero receptor, tipo de comprobante contable y controles de stock por unidad operativa. Recibe los datos de cabecera y detalle de la orden en formato XML, y devuelve el identificador y código de la orden generada junto con el resultado de la operación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransferOrder_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransferOrder_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de traslado de inventario; Almacén origen / tránsito / destino; Almacén virtual, de consignación, de custodia, de tránsito y de control; Unidad funcional de destino; Concepto de ajuste de inventario; Tercero que maneja la cuenta contable; Inventario físico (lotes/seriales); Solicitud de inventario y cantidad pendiente (OutstandingQuantity); Costo promedio del producto; Stock mínimo / máximo; Kardex (movimientos de entrada/salida); Remisiones de inventario en consignación; Comprobante contable / Journal Voucher; Grupo de productos y cuentas contables (débito/crédito); Período de inventario (año/mes); Documento de control de inventario; Permiso ''Quitar Validación Cantidades Importadas''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 3 → Anula la orden: actualiza Status, ModificationUser/Date y AnnulmentUser/Date sin ejecutar validaciones de detalle ni movimientos else Procesa registro/actualización/confirmación con validaciones completas; si @OrderType = 3 AND @Status = 2 (confirmación de tránsito) → Valida solo cantidades en almacén de tránsito, actualiza cabecera con ConfirmationUser/Date y reemplaza @SourceWarehouseId por @TransitWarehouseId else Ejecuta flujo completo de validaciones de detalle/sub-detalle e inserta/actualiza cabecera, detalle y sub-detalle; si @Id = 0 AND @Code = '''' → Solicita secuencia numérica vía Common.SP_GetSequence (tipo 190, IdForm 1519) e inserta nueva cabecera en Inventory.TransferOrder else Actualiza la cabecera existente; si @Status IN (2,4) → Ejecuta movimiento de kardex de salida del almacén origen y, si @OrderType IN (1,3) AND @DispatchTo=1, también el movimiento de entrada al destino; resta OutstandingQuantity en InventoryRequestDetail e InventoryRequestDetailOther salvo en confirmación de tránsito else No genera movimientos de kardex ni contables; si @SourceWarehouseConsignment = 1 (almacén origen es de consignación) → Invoca SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission para actualizar las remisiones de inventario en consignación else Omite la actualización de remisiones de consignación; si @Status = 2 AND el almacén origen NO es ControlStore → Genera comprobante contable vía GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; si SettingInventory.AssociateCostMainAccount=2, OrderType=2 y DispatchTo=2 usa cuentas de ProductGroupFunctionalUnit, sino usa cuentas de ProductGroup/AccountPayableConcepts/AdjustmentConcept else No registra comprobante contable; si @StockControl = 1 → Valida stock mínimo/máximo por producto sumando PhysicalInventory de todas las bodegas else Si @StockControl = 2, valida stock por bodega origen (y destino si OrderType IN (1,3)); si @PermissionValidateQuantity = 0 (usuario sin permiso 91 sobre IdForm 1519) → Valida que la cantidad de los detalles no supere OutstandingQuantity de InventoryRequestDetail e InventoryRequestDetailOther else Omite la validación de cantidad pendiente de la solicitud; si @Status IN (1,4) → Si no existe registro en InventoryControlDocument para (DocumentType=13, DocumentNumber=@Code) lo inserta else Elimina el registro de InventoryControlDocument para ese documento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Common.GETDATE; Inventory.SP_SavePhysicalInventoryKardex_Output; Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.SettingInventory; Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial; Inventory.Warehouse; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.BatchSerial; Inventory.InventoryRequest; Inventory.InventoryRequestDetail; Inventory.InventoryRequestDetailOther; Inventory.AdjustmentConcept; Inventory.ProductGroup; Inventory.ProductGroupFunctionalUnit; Inventory.InventoryControlDocument; Payroll.FunctionalUnit; Security.UserInt; Security.PermissionUserInt; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; Payments.AccountPayableConcepts', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder_Output';
-- GO
