-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-26
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una devolución de una orden de traslado de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveTransferOrderDevolution_Output]
    @TransferOrderDevolutionXml AS XML,
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

	--Se declaran las variables para obtener la cabecera
	DECLARE @Prefix VARCHAR(4),
			@OperatingUnitId INT,
			@DocumentDate DATETIME,
			@TransferOrderId INT,
			@Description VARCHAR(MAX),
			@Status TINYINT,
			------------------------------
			@IdForm INT = 332,
			@DocumentTypeControl INT = 14,
			------------------------------
			@OrderType TINYINT,
			@DispatchTo TINYINT,
			@SourceWarehouseId INT,
			@TargetWarehouseId INT,
			@TargetFunctionalUnitId INT,
			@AdjustmentConceptId INT,
			@ThirdPartyId INT,
			------------------------------
			@StockControl TINYINT,
			@TakeTransferOrderDevolutionThirdParty TINYINT,
			@TransferOrderDevolutionThirdPartyId INT,
			@JournalVoucherTypeId INT,
			@SourceCostCenterId INT,
			@TargetCostCenterId INT,
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
		Id INT,
		TransferOrderDevolutionId INT,
		TransferOrderDetailBatchSerialId INT,
		Quantity INT NOT NULL,
		ChangeTracker VARCHAR(30)
	)
	--Tabla temporal de cabecera de comprobantes
	DECLARE @JournalVoucher TABLE
	(
		Id INT,
		Consecutive INT,
		IdJournalVoucher INT,
		VoucherDate datetime,
		Imported bit,
		Status Tinyint,
		Detail VARCHAR(MAX),
		EntityName VARCHAR(250),
		EntityCode VARCHAR(20),
		EntityId int,
		IsClosedYear Tinyint
	)

	--Tabla temporal de detalles de comprobantes
	DECLARE @JournalVoucherDetails TABLE
	(
		Id INT,
		IdAccounting INT,
		IdMainAccount INT,
		IdThirdParty  int,
		IdCostCenter int,
		DebitValue Decimal(21,5),
		CreditValue Decimal(21,5)
	)

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','int'),
				@Prefix = t.x.value('Prefix[1]','varchar(4)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@Code = t.x.value('Code[1]','varchar(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]','datetime'),				
				@TransferOrderId = t.x.value('TransferOrderId[1]','int'),
				@Description = t.x.value('Description[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','tinyint')
		FROM @TransferOrderDevolutionXml.nodes('/TransferOrderDevolution') t(x)

		IF EXISTS (SELECT 1 FROM Inventory.TransferOrderDevolution om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Devolución de Orden de Traslado se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', IIF(om.Status = 3, 'Anulado', ''))
			FROM Inventory.TransferOrderDevolution om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[TransferOrderDevolution]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN

			SELECT	@OrderType = OrderType,
					@DispatchTo = DispatchTo,
					@SourceWarehouseId = SourceWarehouseId,
					@TargetWarehouseId = TargetWarehouseId,
					@TargetFunctionalUnitId = TargetFunctionalUnitId,
					@AdjustmentConceptId = AdjustmentConceptId,
					@ThirdPartyId = ThirdPartyId
			FROM Inventory.TransferOrder
			WHERE Id = @TransferOrderId

			SELECT	@StockControl = StockControl,
					@TakeTransferOrderDevolutionThirdParty = TakeTransferOrderThirdParty,
					@TransferOrderDevolutionThirdPartyId = TransferOrderThirdPartyId,
					@JournalVoucherTypeId = OrderDispatchReturnJournalVoucherTypeId
			FROM Inventory.SettingInventory
			WHERE OperatingUnitId = @OperatingUnitId

			/***************************************** VALIDACIONES CABECERA *****************************************/

			DECLARE @SourceWarehouseConsignment BIT
			SELECT TOP 1 @SourceWarehouseConsignment = WarehouseConsignment, @Prefix = Prefix FROM Inventory.Warehouse WHERE Id = @SourceWarehouseId

			-- Valido el Periodo del Documento
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Inventory.SettingInventory si
				WHERE si.OperatingUnitId = @OperatingUnitId
					AND si.Year = YEAR(@DocumentDate)
					AND si.Month = MONTH(@DocumentDate)
			)
			BEGIN
				SELECT @Message = 'El periodo actual de inventario no coincide con la fecha del documento, Periodo Actual de Inventario: ' + CONCAT(si.Year, '-', RIGHT('00' + CAST(si.Month AS VARCHAR), 2))
				FROM Inventory.SettingInventory si
				WHERE si.OperatingUnitId = @OperatingUnitId

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'No se ha creado una configuración para el modulo de inventarios.')
				RETURN
			END

			-- Valido el estado de la orden de traslado
			IF NOT EXISTS (SELECT 1 FROM Inventory.TransferOrder WHERE Id = @TransferOrderId AND Status = 2)
			BEGIN
				SELECT @Message = 'La orden de traslado se encuentra en estado: ' +
						CASE Status
							WHEN 1 THEN 'Registrado'
							WHEN 3 THEN 'Anulado'
							WHEN 4 THEN 'En Transito'
						END
				FROM Inventory.TransferOrder
				WHERE Id = @TransferOrderId

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'La orden de traslado no existe.')
				RETURN
			END

			/***************************************** ********************* *****************************************/

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
				SELECT	t.x.value('Id[1]','int'),
						t.x.value('TransferOrderDevolutionId[1]','int'),
						t.x.value('TransferOrderDetailBatchSerialId[1]','int'),
						t.x.value('Quantity[1]','int'),
						t.x.value('ChangeTracker[1]','varchar(30)')
				FROM @TransferOrderDevolutionXml.nodes('/TransferOrderDevolution/TransferOrderDevolutionDetail') t(x)

			--se eliminan los detalles marcados para su eliminación
			DELETE todd
			FROM Inventory.TransferOrderDevolutionDetail todd
			JOIN @Detail d ON todd.Id = d.Id
			WHERE @Id = todd.TransferOrderDevolutionId AND d.ChangeTracker = 'Deleted'

			DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'

			/*****************************************  VALIDACIONES DETALLE *****************************************/

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Detail)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'La Devolución de la Orden de Traslado no tiene detalles.'
				RETURN
			END
			
			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				LEFT JOIN Inventory.TransferOrderDevolutionDetail todd ON d.Id = todd.Id
				WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(todd.TransferOrderDevolutionId, 0) OR ISNULL(d.TransferOrderDetailBatchSerialId, 0) <> ISNULL(todd.TransferOrderDetailBatchSerialId, 0))
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los detalles de la Devolución de la Orden de Traslado han sido alterados.'
				RETURN
			END

			-- Valido que no hayan quedado detalles sin procesar
			IF EXISTS 
			(
				SELECT 1 
				FROM Inventory.TransferOrderDevolutionDetail todd
				LEFT JOIN @Detail d ON d.Id = todd.Id
				WHERE todd.TransferOrderDevolutionId = @Id AND d.Id IS NULL
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Existen detalles de la Devolución de la Orden de Traslado que no han sido procesados.'
				RETURN
			END

			-- Valido que no existan detalles duplicados
			IF EXISTS
			(
				SELECT 1 
				FROM @Detail d
				GROUP BY d.TransferOrderDetailBatchSerialId
				HAVING COUNT(1) > 1
			)
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Existen detalles duplicados (El mismo detalle de la orden de traslado).'
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

			-- Valido que la orden tenga las cantidades suficientes a devolver
			IF EXISTS
			(
				SELECT 1 
				FROM @Detail d
				JOIN Inventory.TransferOrderDetailBatchSerial todbs ON d.TransferOrderDetailBatchSerialId = todbs.Id
				WHERE d.Quantity > todbs.OutstandingQuantity
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ', ip.Name, ' (Lote: ', bs.BatchCode, '). Cantidad disponible: ', todbs.OutstandingQuantity, ' - Cantidad Devuelta: ', d.Quantity)
						FROM @Detail d
						JOIN Inventory.TransferOrderDetailBatchSerial todbs ON d.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
						JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
						LEFT JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
						WHERE d.Quantity > todbs.OutstandingQuantity
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
						@MessageResult = 'Los siguientes productos superan la cantidad disponible de la orden de traslado: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END
			
			-- Si la orden de traslado fue de tipo traslado / traslado en transito
			IF @OrderType IN (1, 3)
			BEGIN
				-- Valido que el inventario físico tenga las cantidades suficientes
				IF EXISTS
				(
					SELECT 1 
					FROM @Detail d
					JOIN Inventory.TransferOrderDetailBatchSerial todbs ON d.TransferOrderDetailBatchSerialId = todbs.Id
					JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
					LEFT JOIN Inventory.PhysicalInventory phyd ON @TargetWarehouseId = phyd.WarehouseId AND phy.ProductId = phyd.ProductId AND ISNULL(phy.BatchSerialId, 0) = ISNULL(phyd.BatchSerialId, 0)
					WHERE d.Quantity > ISNULL(phyd.Quantity, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ', ip.Name, ' (Lote: ', bs.BatchCode, '). Cantidad disponible: ', ISNULL(phyd.Quantity, 0), ' - Cantidad Solicitada: ', d.Quantity)
							FROM @Detail d
							JOIN Inventory.TransferOrderDetailBatchSerial todbs ON d.TransferOrderDetailBatchSerialId = todbs.Id
							JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
							LEFT JOIN Inventory.PhysicalInventory phyd ON @TargetWarehouseId = phyd.WarehouseId AND phy.ProductId = phyd.ProductId AND ISNULL(phy.BatchSerialId, 0) = ISNULL(phyd.BatchSerialId, 0)
							LEFT JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
							LEFT JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
							WHERE d.Quantity > ISNULL(phyd.Quantity, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
							@MessageResult = 'Los siguientes productos no tienen la cantidad suficiente: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
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
								@MessageResult = REPLACE(@Message_Output, '{0}', 'Devolución de la Orden de Traslado')
						RETURN
					END

					--Se inserta la cabecera
					INSERT INTO [Inventory].[TransferOrderDevolution]
					(
						[Code],[OperatingUnitId],[DocumentDate],[TransferOrderId],[Description],
						[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
					)
					SELECT	@Code,@OperatingUnitId,@DocumentDate,@TransferOrderId,@Description,
							@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Inventory].[TransferOrderDevolution]
					SET [Code] = @Code,
						[OperatingUnitId] = @OperatingUnitId,
						[DocumentDate] = @DocumentDate,
						[TransferOrderId] = @TransferOrderId,
						[Description] = @Description,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/*********************************** INSERTAR / ACTUALIZAR DETALLE ***********************************/
			
			INSERT INTO Inventory.TransferOrderDevolutionDetail
			(
				TransferOrderDevolutionId, TransferOrderDetailBatchSerialId, Quantity
			)
			SELECT	@Id, d.TransferOrderDetailBatchSerialId, d.Quantity
			FROM @Detail d
			WHERE d.ChangeTracker = 'Added'

			UPDATE todd
				SET todd.Quantity = d.Quantity
			FROM @Detail d
			JOIN Inventory.TransferOrderDevolutionDetail todd ON d.Id = todd.Id
			WHERE todd.TransferOrderDevolutionId = @Id AND d.ChangeTracker <> 'Added'

			/*********************************************  CONFIRMACIÓN *********************************************/

			IF @Status = 2
			BEGIN
				/**********************  ACTUALIZAR CANTIDAD DISPONIBLE DE LA ORDEN DE TRASLADO **********************/

				UPDATE todbs
					SET todbs.OutstandingQuantity -= todd.Quantity
				FROM Inventory.TransferOrderDevolutionDetail todd
				JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
				WHERE todd.TransferOrderDevolutionId = @Id

				/**************************  ACTUALIZAR CANTIDAD DISPONIBLE DE LA SOLICITUD **************************/

				UPDATE ird
					SET ird.OutstandingQuantity += d.Quantity
				FROM Inventory.InventoryRequestDetail ird
				JOIN
				(
					SELECT tod.InventoryRequestDetailId, SUM(todd.Quantity) Quantity
					FROM Inventory.TransferOrderDevolutionDetail todd
					JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
					JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
					WHERE todd.TransferOrderDevolutionId = @Id
					GROUP BY tod.InventoryRequestDetailId
				) d ON ird.Id = d.InventoryRequestDetailId

				IF @OrderType IN (1, 3) AND @DispatchTo = 1
				BEGIN
					
					/**************************  MOVIMIENTO KARDEX - SALIDA ALMACEN DESTINO **************************/

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
										@TargetWarehouseId WarehouseId,
										phy.ProductId,
										phy.BatchSerialId BatchSerialId,
										todd.Quantity,
										ip.ProductCost Value,
										0 AffectAverageCost
								FROM Inventory.TransferOrderDevolutionDetail todd
								JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
								JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
								JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
								WHERE todd.TransferOrderDevolutionId = @Id
							) Kardex
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'TransferOrderDevolution', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex con el movimiento de salida del almacén de destino.')
						RETURN
					END
				END

				/****************************  MOVIMIENTO KARDEX - ENTRADA ALMACEN ORIGEN ****************************/

				--Generamos el XML para consumir el SP encargado del movimiento del kardex
				
				IF @OrderType  IN(1,3)
				BEGIN
				
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT Kardex.*
							FROM 
							( 
								SELECT	@DocumentDate DocumentDate,
										1 MovementType,
										@SourceWarehouseId WarehouseId,
										phy.ProductId,
										phy.BatchSerialId BatchSerialId,
										todd.Quantity,
										ip.ProductCost Value,
										0 AffectAverageCost
								FROM Inventory.TransferOrderDevolutionDetail todd
								JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
								JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
								JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
								WHERE todd.TransferOrderDevolutionId = @Id
							) Kardex
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)
				
					EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'TransferOrderDevolution', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex con el movimiento de entrada del almacén de origen.')
						RETURN
					END
				END
				ELSE
				BEGIN
				----PARA CUANDO ES TIPO CONSUMO SE DEBE TOMAR EL VALOR CON EL QUE FUE HECHO LA ORDEN DE TRANSLADO
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT Kardex.*
							FROM 
							( 
								SELECT	@DocumentDate DocumentDate,
										1 MovementType,
										@SourceWarehouseId WarehouseId,
										phy.ProductId,
										phy.BatchSerialId BatchSerialId,
										todd.Quantity,
										tood.Value Value,
										0 AffectAverageCost
								FROM Inventory.TransferOrderDevolutionDetail todd
								JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
								join Inventory.TransferOrderDetail tood on tood.Id = todbs.TransferOrderDetailId
								JOIN Inventory.PhysicalInventory phy ON todbs.PhysicalInventoryId = phy.Id
								JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
								WHERE todd.TransferOrderDevolutionId = @Id
							) Kardex
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)
				
					EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'TransferOrderDevolution', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex con el movimiento de entrada del almacén de origen.')
						RETURN
					END
				END		
				If @SourceWarehouseConsignment = 1 Begin
					--- Actualizo las remisiones de inventario en consignación si las hubiera
					declare @RemissionXml xml = (
						Select td.Id As EntityDetailId, 
							@OperatingUnitId As OperatingUnitId, 
							@TargetFunctionalUnitId AS FunctionalUnitId, 
							pdd.ProductId As ProductId, 
							ph.BatchSerialId, 
							1 As MovementType, 
							@SourceWarehouseId As WarehouseId, 
							td.Quantity, 
							pdd.Value, 
							pdd.TransferOrderId As OriginId
						From Inventory.TransferOrderDevolutionDetail As td With(Nolock)
						Inner Join Inventory.TransferOrderDetailBatchSerial pddbs With(Nolock) On pddbs.Id = td.TransferOrderDetailBatchSerialId 
						Inner Join Inventory.TransferOrderDetail pdd With(Nolock) On pdd.Id = pddbs.TransferOrderDetailId
						Inner join Inventory.TransferOrder pd with(nolock) ON pdd.TransferOrderId = pd.Id
						Inner Join Inventory.PhysicalInventory ph With(Nolock) On ph.Id = pddbs.PhysicalInventoryId
						Inner Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = pdd.ProductId
						Where td.TransferOrderDevolutionId = @Id
						For Xml Path('Remission'), Elements
					)
				
					IF @RemissionXml Is Not Null BEGIN
						DECLARE @MessageReturnRemission AS VARCHAR(MAX)
						EXEC [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission] @RemissionXml, @Id, 
							@Code, 'TransferOrderDevolution', @CodeUser, @MessageReturnRemission OUTPUT
						IF (@MessageReturnRemission IS NOT NULL AND @MessageReturnRemission <> '') BEGIN
							
							SELECT	@CodeResult = 999, 
								@MessageResult = @MessageReturnRemission
							RETURN 
						END
					END
				End
				
				/******************************************* VALIDAR STOCK *******************************************/

				IF @StockControl = 1
				BEGIN
					-- validamos stock por producto
					SELECT @MessageResultAux = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El producto ', ip.Code, ' - ', ip.Name, ' alcanzó su stock ', IIF(ip.MinimumStock > ISNULL(phy.Quantity, 0), 'mínimo', 'máximo'))
						FROM Inventory.TransferOrderDevolutionDetail todd
						JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						LEFT JOIN
						(
							SELECT ProductId, SUM(Quantity) Quantity
							FROM Inventory.PhysicalInventory
							GROUP BY ProductId
						) phy ON tod.ProductId = phy.ProductId
						WHERE todd.TransferOrderDevolutionId = @Id AND (ip.MinimumStock > ISNULL(phy.Quantity, 0) OR ip.MaximumStock < ISNULL(phy.Quantity, 0))
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				END
				ELSE IF @StockControl = 2
				BEGIN
					-- validamos stock por almacen
					SELECT @MessageResultAux = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El producto ', ip.Code, ' - ', ip.Name, ' alcanzó su stock ', IIF(ip.MinimumStock > ISNULL(phy.Quantity, 0), 'mínimo', 'máximo'))
						FROM Inventory.TransferOrderDevolutionDetail todd
						JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						LEFT JOIN
						(
							SELECT ProductId, SUM(Quantity) Quantity
							FROM Inventory.PhysicalInventory
							WHERE WarehouseId = @SourceWarehouseId
							GROUP BY ProductId
						) phy ON tod.ProductId = phy.ProductId
						WHERE todd.TransferOrderDevolutionId = @Id AND (ip.MinimumStock > ISNULL(phy.Quantity, 0) OR ip.MaximumStock < ISNULL(phy.Quantity, 0))
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				END

				IF @OrderType IN (1, 3)
				BEGIN
					IF @StockControl = 2
					BEGIN
						-- validamos stock por almacen
						SELECT @MessageResultAux = ISNULL(@MessageResultAux, '') + STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El producto ', ip.Code, ' - ', ip.Name, ' alcanzó su stock ', IIF(ip.MinimumStock > ISNULL(phy.Quantity, 0), 'mínimo', 'máximo'))
							FROM Inventory.TransferOrderDevolutionDetail todd
							JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
							JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
							JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
							LEFT JOIN
							(
								SELECT ProductId, SUM(Quantity) Quantity
								FROM Inventory.PhysicalInventory
								WHERE WarehouseId = @TargetWarehouseId
								GROUP BY ProductId
							) phy ON tod.ProductId = phy.ProductId
							WHERE todd.TransferOrderDevolutionId = @Id AND (ip.MinimumStock > ISNULL(phy.Quantity, 0) OR ip.MaximumStock < ISNULL(phy.Quantity, 0))
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					END
				END
								
				/**************************************** MOVIMIENTO CONTABLE ****************************************/
				
				IF @Status = 2
					-- Si es un almacen de control no debe contabilizar
					AND NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @SourceWarehouseId AND ControlStore = 1)
				BEGIN
				--Si se tiene seleccionada la opcion de 'grupo de inventario' en los parametros de inventario
					IF  EXISTS (SELECT 1 from inventory.SettingInventory WHERE @OperatingUnitId = OperatingUnitId AND AssociateCostMainAccount = 2) 
						AND @OrderType = 2 AND @DispatchTo = 2 
					BEGIN
						SELECT	@SourceCostCenterId = w.CostCenterId
						FROM Inventory.TransferOrder tro
						JOIN Inventory.Warehouse w ON tro.SourceWarehouseId = w.Id
						WHERE tro.Id = @Id

						SELECT	@TargetCostCenterId = CostCenterId
						FROM Payroll.FunctionalUnit
						WHERE Id = @TargetFunctionalUnitId AND @DispatchTo = 2

						insert into @JournalVoucher (Id, Consecutive, IdJournalVoucher, VoucherDate, Imported, Status, Detail, EntityName, EntityCode, EntityId, IsClosedYear)
						SELECT	0 Id,
								0 Consecutive,
								@JournalVoucherTypeId IdJournalVoucher, 									
								@DocumentDate VoucherDate, 
								'False' Imported,
								2 Status,
								@Description Detail, 
								'TransferOrderDevolution' EntityName,
								@Code EntityCode,
								@Id EntityId,
								0 IsClosedYear

					insert into @JournalVoucherDetails (Id, IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
					SELECT 
							0 Id,
							0 IdAccounting,
							pgf.CostAccountId IdMainAccount,
							CASE WHEN ma.HandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END IdThirdParty,
							CASE WHEN ma.HandlesCostCenter = 1 THEN @TargetCostCenterId ELSE NULL END IdCostCenter, 									
							0 DebitValue, 
							todd.Quantity * tood.Value CreditValue 
					from  Inventory.TransferOrderDevolution tod
					join Inventory.TransferOrderDevolutionDetail todd on todd.TransferOrderDevolutionId = tod.Id
					join Inventory.TransferOrderDetailBatchSerial todbs on todbs.Id = todd.TransferOrderDetailBatchSerialId
					join Inventory.TransferOrderDetail tood on tood.Id = todbs.TransferOrderDetailId
					join Inventory.TransferOrder too on too.Id = tood.TransferOrderId
					JOIN Inventory.InventoryProduct ip ON tood.ProductId = ip.Id
					JOIN Inventory.ProductGroup pg ON pg.Id = ip.ProductGroupId
					JOIN Inventory.ProductGroupFunctionalUnit pgf ON pgf.ProductGroupId = pg.Id
					JOIN GeneralLedger.MainAccounts ma ON pgf.CostAccountId = ma.Id 
					WHERE tod.TransferOrderId =@TransferOrderId and pgf.FunctionalUnitId = @TargetFunctionalUnitId
					UNION ALL
					SELECT	0 Id,
							0 IdAccounting,
							ma.Id IdMainAccount,
							CASE WHEN ma.HandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END IdThirdParty,
							CASE WHEN ma.HandlesCostCenter = 1 THEN @TargetCostCenterId ELSE NULL END IdCostCenter, 	
							todd.Quantity * tood.Value DebitValue, 
							0 CreditValue 
					FROM Inventory.TransferOrderDevolution tod
					join Inventory.TransferOrderDevolutionDetail todd on todd.TransferOrderDevolutionId = tod.Id
					join Inventory.TransferOrderDetailBatchSerial todbs on todbs.Id = todd.TransferOrderDetailBatchSerialId
					join Inventory.TransferOrderDetail tood on tood.Id = todbs.TransferOrderDetailId
					JOIN Inventory.InventoryProduct ip ON tood.ProductId = ip.Id
					JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
					LEFT JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
					LEFT JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
					WHERE tod.TransferOrderId = @TransferOrderId
										
						SELECT @SubXml = CONVERT
						(
							XML, 
							( 
								select * from @JournalVoucher JournalVoucher
								join @JournalVoucherDetails JournalVoucherDetail on JournalVoucherDetail.IdAccounting = JournalVoucher.Id																				   			
								For xml AUTO,TYPE, ELEMENTS
							)
						)
					END
					ELSE BEGIN
					
					SELECT	@SourceCostCenterId = w.CostCenterId,
							@ThirdPartyId = IIF(@OrderType IN (1, 3) OR @TakeTransferOrderDevolutionThirdParty <> 1, @TransferOrderDevolutionThirdPartyId, @ThirdPartyId)
					FROM Inventory.Warehouse w
					WHERE w.Id = @SourceWarehouseId

					SELECT	@TargetCostCenterId = CostCenterId
					FROM Inventory.Warehouse
					WHERE Id = @TargetWarehouseId AND @DispatchTo = 1

					SELECT	@TargetCostCenterId = CostCenterId
					FROM Payroll.FunctionalUnit
					WHERE Id = @TargetFunctionalUnitId AND @DispatchTo = 2

					insert into @JournalVoucher (Id, Consecutive, IdJournalVoucher, VoucherDate, Imported, Status, Detail, EntityName, EntityCode, EntityId, IsClosedYear)
					SELECT	0 Id,
							0 Consecutive,
							@JournalVoucherTypeId IdJournalVoucher, 									
							@DocumentDate VoucherDate, 
							'False' Imported,
							2 Status,
							@Description Detail, 
							'TransferOrderDevolution' EntityName,
							@Code EntityCode,
							@Id EntityId,
							0 IsClosedYear

					insert into @JournalVoucherDetails (Id, IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
					SELECT	0 Id,
							0 IdAccounting,
							ma.Id IdMainAccount,
							IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
							IIF(ma.HandlesCostCenter = 1, @SourceCostCenterId, NULL) IdCostCenter, 									
							ROUND(todd.Quantity * ip.ProductCost, 2) DebitValue, 
							0 CreditValue
					FROM Inventory.TransferOrderDevolutionDetail todd
					JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
					JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
					JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
					JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
					LEFT JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
					LEFT JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
					WHERE todd.TransferOrderDevolutionId = @Id And @SourceWarehouseConsignment = 0
					UNION ALL
						SELECT	0 Id,
								0 IdAccounting,
								ma.Id IdMainAccount,
								IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
								IIF(ma.HandlesCostCenter = 1, @SourceCostCenterId, NULL) IdCostCenter, 									
								0 DebitValue, 
								tod.Quantity * tod.Value CreditValue
						FROM Inventory.TransferOrderDevolutionDetail todd
						JOIN Inventory.TransferOrderDetailBatchSerial todbs With(Nolock) ON todd.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.TransferOrderDetail tod With(Nolock) ON todbs.TransferOrderDetailId = tod.Id
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
						LEFT JOIN GeneralLedger.MainAccounts ma ON pg.CounterpartCostConsignedInventoryId = ma.Id
						WHERE tod.TransferOrderId = @TransferOrderId And @SourceWarehouseConsignment = 1
					UNION ALL
						SELECT	0 Id,
								0 IdAccounting,
								ma.Id IdMainAccount,
								IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
								IIF(ma.HandlesCostCenter = 1, @TargetCostCenterId, NULL) IdCostCenter, 									
								0 DebitValue, 
								ROUND(todd.Quantity * ip.ProductCost, 2) CreditValue
						FROM Inventory.TransferOrderDevolutionDetail todd
						JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
						LEFT JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
						LEFT JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
						WHERE todd.TransferOrderDevolutionId = @Id AND @OrderType IN (1, 3)
					UNION ALL
						SELECT	0 Id,
								0 IdAccounting,
								ma.Id IdMainAccount,
								IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
								IIF(ma.HandlesCostCenter = 1, @TargetCostCenterId, NULL) IdCostCenter, 									
								0 DebitValue, 
								ROUND(todd.Quantity * ip.ProductCost, 2) CreditValue
						FROM Inventory.TransferOrderDevolutionDetail todd
						JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todd.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.TransferOrderDetail tod ON todbs.TransferOrderDetailId = tod.Id
						JOIN Inventory.InventoryProduct ip ON tod.ProductId = ip.Id
						LEFT JOIN Inventory.AdjustmentConcept ac ON @AdjustmentConceptId = ac.Id
						LEFT JOIN GeneralLedger.MainAccounts ma ON ac.AdjustmentAccountId = ma.Id
						WHERE todd.TransferOrderDevolutionId = @Id AND @OrderType = 2 AND @SourceWarehouseConsignment = 0
					UNION ALL
						SELECT	0 Id,
								0 IdAccounting,
								ma.Id IdMainAccount,
								IIF(ma.HandlesThirdParty = 1, @ThirdPartyId, NULL) IdThirdParty, 
								IIF(ma.HandlesCostCenter = 1, @TargetCostCenterId, NULL) IdCostCenter, 									
								0 DebitValue, 
								ROUND(todd.Quantity * tod.Value, 2) CreditValue
						FROM Inventory.TransferOrderDevolutionDetail todd With(Nolock)
						JOIN Inventory.TransferOrderDetailBatchSerial todbs With(Nolock) ON todd.TransferOrderDetailBatchSerialId = todbs.Id
						JOIN Inventory.TransferOrderDetail tod With(Nolock) ON todbs.TransferOrderDetailId = tod.Id
						JOIN Inventory.TransferOrder tor (NOLOCK) on tod.TransferOrderId = tor.Id									
						JOIN Inventory.AdjustmentConcept ac (NOLOCK) on tor.AdjustmentConceptId = ac.Id
						JOIN GeneralLedger.MainAccounts ma (NOLOCK) ON ma.Id = ac.AdjustmentAccountId
						WHERE todd.TransferOrderDevolutionId = @Id AND @OrderType = 2 AND @SourceWarehouseConsignment = 1

					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							select * from @JournalVoucher JournalVoucher
							join @JournalVoucherDetails JournalVoucherDetail on JournalVoucherDetail.IdAccounting = JournalVoucher.Id							
							For xml AUTO,TYPE, ELEMENTS
						)
					)
					END

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

					SELECT @Message = 'Se guardó y confirmó el comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(20)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name
					FROM GeneralLedger.JournalVouchers jv
					join GeneralLedger.JournalVoucherDetails jvd on jv.id = jvd.IdAccounting
					LEFT JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
					WHERE jv.Id = @JournalVoucherId
				END
			END
		END

		IF @Status = 1
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Devolución de la Orden de Traslado con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Devolución de la Orden de Traslado con código ', @Code)
				   ELSE CONCAT('Se guardó la Devolución de la Orden de Traslado con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SaveTransferOrderDevolution_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END