-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-11-18
-- Description:	Procedimiento que se encarga de validar el Copy & Paste de la rejilla
-- =============================================
CREATE PROCEDURE [Billing].[SP_SetBasicBillingDetailFromFile]
	@AddressId AS INT,
	@WarehouseId AS INT,
	@XmlObject AS XML	
AS
BEGIN
	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		DetailType INT,
		ItemId INT,
		ItemCode VARCHAR(50),
		ItemDescription VARCHAR(200),
		PhysicalInventoryId INT,
		ItemBatchSerial VARCHAR(50),
		Quantity INT,
		Price DECIMAL(18,2),
		PercentageDiscount DECIMAL(5,2),
		PercentageIVA DECIMAL(5,2),
		RetentionIdTax INT,
		RetentionPercentageTax DECIMAL(6,3),
		RetentionBaseTax DECIMAL(18,2),
		RetentionIdICA INT,
		RetentionPercentageICA DECIMAL(6,3),
		RetentionBaseICA DECIMAL(18,2),

		CountFields INT,
		StatusField INT,
		MessageField VARCHAR(MAX)
	)

	--Variables
	DECLARE @CityId AS INT

	--Se obtiene la ciudad para con esta obtener el porcentaje de retencion de ICA 
	SELECT @CityId = a.CityId
	FROM Common.Address a
	WHERE a.Id = @AddressId

	BEGIN TRY
		INSERT INTO @TableXmlObject
			SELECT
				t.x.value('DetailType[1]','int') as DetailType,
				t.x.value('ItemId[1]','int') as ItemId,
				t.x.value('ItemCode[1]','varchar(50)') as ItemCode,
				t.x.value('ItemDescription[1]','varchar(200)') as ItemDescription,
				t.x.value('PhysicalInventoryId[1]','int') as PhysicalInventoryId,
				t.x.value('ItemBatchSerial[1]','varchar(50)') as ItemBatchSerial,
				t.x.value('Quantity[1]','int') as Quantity,
				t.x.value('Price[1]','decimal(18,2)') as Price,
				t.x.value('PercentageDiscount[1]','decimal(5,2)') as PercentageDiscount,
				t.x.value('PercentageIVA[1]','decimal(5,2)') as PercentageIVA,
				t.x.value('RetentionIdTax[1]','int') as RetentionIdTax,
				t.x.value('RetentionPercentageTax[1]','decimal(6,3)') as RetentionPercentageTax,
				t.x.value('RetentionBaseTax[1]','decimal(18,2)') as RetentionBaseTax,
				t.x.value('RetentionIdICA[1]','int') as RetentionIdICA,
				t.x.value('RetentionPercentageICA[1]','decimal(6,3)') as RetentionPercentageICA,
				t.x.value('RetentionBaseICA[1]','decimal(18,2)') as RetentionBaseICA,

				t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(100)') as MessageField
			FROM @XmlObject.nodes('/Data/Row') t(x)

		--Se declara un cursor y las variables que lleva el cursor
		DECLARE @Id INT,
				@DetailType INT,
				@ItemId INT,
				@ItemCode VARCHAR(50),
				@ItemDescription VARCHAR(200),
				@PhysicalInventoryId INT,
				@BatchSerialId INT,
				@ItemBatchSerial VARCHAR(50),
				@Quantity INT,
				@Price DECIMAL(18,2),
				@PercentageDiscount DECIMAL(5,2),
				@PercentageIVA DECIMAL(5,2),
				@RetentionIdTax INT,
				@RetentionPercentageTax DECIMAL(6,3),
				@RetentionBaseTax DECIMAL(18,2),
				@RetentionIdICA INT,
				@RetentionPercentageICA DECIMAL(6,3),
				@RetentionBaseICA DECIMAL(18,2),
				-------------------------------
				@CountFields INT,
				-------------------------------
				@HandlesBatch BIT

		--Se declara el select del cursor
		DECLARE InfoItem CURSOR FOR 
			SELECT Id, DetailType, ItemCode, ItemBatchSerial, Quantity, Price, PercentageDiscount, CountFields FROM @TableXmlObject

		OPEN InfoItem

		FETCH NEXT FROM InfoItem INTO @Id, @DetailType, @ItemCode, @ItemBatchSerial, @Quantity, @Price, @PercentageDiscount, @CountFields

		WHILE @@fetch_status = 0
		BEGIN		
			--Se valida que cada registro tenga la estructura requerida
			IF @CountFields <> 6
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'El registro ' + convert(VARCHAR(3), @Id) + ' no tiene la estructura requerida'
				WHERE Id = @Id			
				
				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Reiniciamos los valores del cursor
			SELECT @BatchSerialId = NULL,
				@PhysicalInventoryId = 0,
				@HandlesBatch = 0,
				@PercentageIVA = 0,
				@RetentionIdTax = NULL,
				@RetentionPercentageTax = 0,
				@RetentionBaseTax = 0,
				@RetentionIdICA = NULL,
				@RetentionPercentageICA = 0,
				@RetentionBaseICA = 0

			--Se valida que el tipo de detalle sea valido
			IF NOT(@DetailType > 0 AND @DetailType < 5)
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'El tipo de detalle del registro ' + convert(VARCHAR(3), @Id) + ' no es válido'
				WHERE Id = @Id			

				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Se valida que el código del registro no este vacio
			IF ISNULL(@ItemCode, '') = ''
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'El código del registro ' + convert(VARCHAR(3), @Id) + ' esta vacío'
				WHERE Id = @Id			

				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Se valida que el código del registro no este vacio
			IF NOT @Quantity > 0
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'La cantidad del registro ' + convert(VARCHAR(3), @Id) + ' debe ser mayor a 0'
				WHERE Id = @Id			

				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Se valida que el código del registro no este vacio
			IF NOT @Price > 0
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'El precio del registro ' + convert(VARCHAR(3), @Id) + ' debe ser mayor a 0'
				WHERE Id = @Id			

				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Se valida que el código del registro no este vacio
			IF NOT (@PercentageDiscount >= 0 AND @PercentageDiscount <= 100)
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'El porcentaje de descuento del registro ' + convert(VARCHAR(3), @Id) + ' debe estar entre 0 y 100'
				WHERE Id = @Id			

				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Validaciones dependiento el tipo de detalle
			IF @DetailType = 1
			BEGIN
				--Se valida que se haya parametrizado un almacen y que este exista
				IF NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @WarehouseId)
				BEGIN
					--Se actualiza los campos con el estado en false y el mensaje de error
					UPDATE @TableXmlObject 
						SET StatusField = 0, 
							MessageField = 'No se ha definido un almacen válido para el registro ' + convert(VARCHAR(3), @Id)
					WHERE Id = @Id			

					--Se pasa a la siguiente posicion del cursor
					GOTO NextFetch
				END

				--Se valida que el registro exista
				IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryProduct WHERE Code = @ItemCode)
				BEGIN
					--Se actualiza los campos con el estado en false y el mensaje de error
					UPDATE @TableXmlObject 
						SET StatusField = 0, 
							MessageField = 'El producto con código ' + @ItemCode + ' del registro ' + convert(VARCHAR(3), @Id) + ' no existe'
					WHERE Id = @Id			

					--Se pasa a la siguiente posicion del cursor
					GOTO NextFetch
				END

				--Se obtienen los valores del registro
				SELECT @ItemId = ip.Id, 
					@ItemDescription = ip.Code + ' - ' + ip.Name,
					@HandlesBatch = psg.HandlesBatch
				FROM Inventory.InventoryProduct ip 
				JOIN Inventory.ProductSubGroup psg ON ip.ProductSubGroupId = psg.Id
				WHERE ip.Code = @ItemCode
				
				--Se valida si el producto maneja lote
				IF @HandlesBatch = 1
				BEGIN
					--Se valida que el lote del registro no este vacio
					IF ISNULL(@ItemBatchSerial, '') = ''
					BEGIN
						--Se actualiza los campos con el estado en false y el mensaje de error
						UPDATE @TableXmlObject 
							SET StatusField = 0, 
								MessageField = 'El código del lote del registro ' + convert(VARCHAR(3), @Id) + ' esta vacío'
						WHERE Id = @Id

						--Se pasa a la siguiente posicion del cursor
						GOTO NextFetch
					END

					SELECT @BatchSerialId = bs.Id 
					FROM Inventory.BatchSerial bs
					WHERE bs.ProductId = @ItemId AND bs.BatchCode = @ItemBatchSerial

					--Se valida que el lote del registro exista
					IF ISNULL(@BatchSerialId, 0) = 0
					BEGIN
						--Se actualiza los campos con el estado en false y el mensaje de error
						UPDATE @TableXmlObject 
							SET StatusField = 0, 
								MessageField = 'El lote ' + @ItemBatchSerial + ' del producto con código ' + @ItemCode + ' del registro ' + convert(VARCHAR(3), @Id) + ' no existe'
						WHERE Id = @Id

						--Se pasa a la siguiente posicion del cursor
						GOTO NextFetch
					END
				END

				SELECT TOP 1 @PhysicalInventoryId = p.Id
				FROM Inventory.PhysicalInventory p
				WHERE p.WarehouseId = @WarehouseId AND 
					p.ProductId = @ItemId AND 
					ISNULL(p.BatchSerialId, 0) = ISNULL(@BatchSerialId, 0) AND 
					p.Quantity > @Quantity

				--Se valida que exista la cantidad suficiente
				IF ISNULL(@PhysicalInventoryId, 0) = 0
				BEGIN
					--Se actualiza los campos con el estado en false y el mensaje de error
					UPDATE @TableXmlObject 
						SET StatusField = 0, 
							MessageField = 'El producto con código ' + @ItemCode + ' y lote ' + @ItemBatchSerial + ' del registro ' + convert(VARCHAR(3), @Id) + ' no tiene la cantidad suficiente'
					WHERE Id = @Id

					--Se pasa a la siguiente posicion del cursor
					GOTO NextFetch
				END

				SELECT 
					@PercentageIVA = ISNULL(iva.Percentage, 0),
					@RetentionIdTax = tax.Id,
					@RetentionPercentageTax = ISNULL(tax.Rate, 0),
					@RetentionBaseTax = ISNULL(tax.MinBase, 0),
					@RetentionIdICA = ica.Id,
					@RetentionPercentageICA = ISNULL(ISNULL(rcc.Rate, ica.Rate), 0),
					@RetentionBaseICA = ISNULL(ica.MinBase, 0)
				FROM Inventory.InventoryProduct ip
				JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON ip.IVAId = iva.Id
				LEFT JOIN GeneralLedger.RetentionConcepts tax ON pg.ReteFuenteConceptId = tax.Id
				LEFT JOIN GeneralLedger.RetentionConcepts ica ON pg.WithholdingICAConceptId = ica.Id
				LEFT JOIN GeneralLedger.RetentionConceptByCity rcc ON ica.Id = rcc.RetentionConceptId AND rcc.CityId = @CityId
				WHERE ip.Id = @ItemId
			END
			ELSE IF @DetailType = 2
			BEGIN
				--Se valida que el registro exista
				IF NOT EXISTS (SELECT 1 FROM Billing.BillingConcept WHERE Code = @ItemCode)
				BEGIN
					--Se actualiza los campos con el estado en false y el mensaje de error
					UPDATE @TableXmlObject 
						SET StatusField = 0, 
							MessageField = 'El concepto con código ' + @ItemCode + ' del registro ' + convert(VARCHAR(3), @Id) + ' no existe'
					WHERE Id = @Id			

					--Se pasa a la siguiente posicion del cursor
					GOTO NextFetch
				END

				--Se obtienen los valores del registro
				SELECT @ItemId = bc.Id, 
					@ItemDescription = bc.Code + ' - ' + bc.Name,
					@PercentageIVA = ISNULL(iva.Percentage, 0),
					@RetentionIdTax = tax.Id,
					@RetentionPercentageTax = ISNULL(tax.Rate, 0),
					@RetentionBaseTax = ISNULL(tax.MinBase, 0),
					@RetentionIdICA = ica.Id,
					@RetentionPercentageICA = ISNULL(ISNULL(rcc.Rate, ica.Rate), 0),
					@RetentionBaseICA = ISNULL(ica.MinBase, 0)
				FROM Billing.BillingConcept bc 
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON bc.IVAId = iva.Id
				LEFT JOIN GeneralLedger.RetentionConcepts tax ON bc.WithholdingTaxConceptId = tax.Id
				LEFT JOIN GeneralLedger.RetentionConcepts ica ON bc.WithholdingICAConceptId = ica.Id
				LEFT JOIN GeneralLedger.RetentionConceptByCity rcc ON ica.Id = rcc.RetentionConceptId AND rcc.CityId = @CityId
				WHERE bc.Code = @ItemCode
			END
			ELSE IF @DetailType = 3
			BEGIN
				--Se valida que el registro exista
				IF NOT EXISTS (SELECT 1 FROM FixedAsset.FixedAssetPhysicalAsset fapa WHERE fapa.Plate = @ItemCode AND (fapa.HasOutput <> 0 OR fapa.AdquisitionType IN (3, 8, 10)))
				BEGIN
					--Se actualiza los campos con el estado en false y el mensaje de error
					UPDATE t 
						SET StatusField = 0, 
							MessageField = CASE fapa.AdquisitionType
								WHEN 3 THEN 'El Activo Fijo con Placa ' + fapa.Plate + ' es un comodato y no puede tener la opción de venta'
								WHEN 8 THEN 'El Activo Fijo con Placa ' + fapa.Plate + 'es un comodato tercerizado y no puede tener la opción de venta'
								WHEN 10 THEN 'El Activo Fijo con Placa ' + fapa.Plate + ' es un renting operativo y no puede tener la opción de venta'
								ELSE 'El activo con placa ' + @ItemCode + ' del registro ' + convert(VARCHAR(3), @Id) + ' no existe o no se encuentra activo'
							END
					FROM @TableXmlObject t
					LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Plate = t.ItemCode
					WHERE t.Id = @Id

					--Se pasa a la siguiente posicion del cursor
					GOTO NextFetch
				END

				--Se obtienen los valores del registro
				SELECT @ItemId = fapa.Id, 					
					@ItemDescription = fapa.Plate + ' - ' + fai.Code + ' - ' + fai.Description,
					@Quantity = 1,
					@PercentageIVA = ISNULL(iva.Percentage, 0),
					@RetentionIdTax = tax.Id,
					@RetentionPercentageTax = ISNULL(tax.Rate, 0),
					@RetentionBaseTax = ISNULL(tax.MinBase, 0),
					@RetentionIdICA = ica.Id,
					@RetentionPercentageICA = ISNULL(ica.Rate, 0),
					@RetentionBaseICA = ISNULL(ica.MinBase, 0)
				FROM FixedAsset.FixedAssetPhysicalAsset fapa
				JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
				JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON fai.IVAId = iva.Id
				LEFT JOIN GeneralLedger.RetentionConcepts tax ON faic.WithholdingTaxConceptId = tax.Id
				LEFT JOIN GeneralLedger.RetentionConcepts ica ON faic.WithholdingICAConceptId = ica.Id
				WHERE fapa.Plate = @ItemCode
			END
			ELSE IF @DetailType = 4
			BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				UPDATE @TableXmlObject 
					SET StatusField = 0, 
						MessageField = 'La parte con código ' + @ItemCode + ' del registro ' + convert(VARCHAR(3), @Id) + ' no se ha validado'
				WHERE Id = @Id			

				--Se pasa a la siguiente posicion del cursor
				GOTO NextFetch
			END

			--Se actualiza la tabla principal con los datos obtenidos
			UPDATE @TableXmlObject 
				SET ItemId = @ItemId, 
					ItemDescription = @ItemDescription,
					PhysicalInventoryId = IIF(@HandlesBatch = 1, @PhysicalInventoryId, 0),
					ItemBatchSerial = IIF(@HandlesBatch = 1, ItemBatchSerial, ''),
					Quantity = @Quantity,
					PercentageIVA = @PercentageIVA,
					RetentionIdTax = @RetentionIdTax,
					RetentionPercentageTax = @RetentionPercentageTax,
					RetentionBaseTax = @RetentionBaseTax,
					RetentionIdICA = @RetentionIdICA,
					RetentionPercentageICA = @RetentionPercentageICA,
					RetentionBaseICA = @RetentionBaseICA,
					StatusField = 1, 
					MessageField = 'Registro validado correctamente'
			WHERE Id = @Id
						
			--Se pasa a la siguiente posicion del cursor
			NextFetch:
			FETCH NEXT FROM InfoItem INTO @Id, @DetailType, @ItemCode, @ItemBatchSerial, @Quantity, @Price, @PercentageDiscount, @CountFields
		END

		CLOSE InfoItem
		DEALLOCATE InfoItem

		--Validamos si existen registros duplicados
		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
			GROUP BY t.DetailType, t.ItemCode, t.ItemBatchSerial
			HAVING COUNT(*) > 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El Registro se encuentra duplicado'
			FROM @TableXmlObject t
			JOIN
			(
				SELECT t.DetailType, t.ItemCode, t.ItemBatchSerial
				FROM @TableXmlObject t
				WHERE t.StatusField = 1
				GROUP BY t.DetailType, t.ItemCode, t.ItemBatchSerial
				HAVING COUNT(*) > 1
			) t2 ON t.DetailType = t2.DetailType AND t.ItemCode = t2.ItemCode AND t.ItemBatchSerial = t2.ItemBatchSerial
			WHERE t.StatusField = 1
		END
	END TRY
	BEGIN CATCH
		DELETE FROM @TableXmlObject
		INSERT INTO @TableXmlObject(StatusField, MessageField) values(0, ERROR_MESSAGE() + '. Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)))
	END CATCH

	--Se retorna la tabla
	SELECT * FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa el pegado masivo (copy & paste) de ítems de facturación cargados desde un archivo o grilla, recibidos en formato XML. Toma una dirección de residencia o contacto para determinar la ciudad y calcular retenciones de ICA, y una bodega de inventario para verificar existencias. Por cada ítem del XML valida la estructura del registro, el tipo de detalle de factura, el código del producto o servicio, la cantidad y el precio; si el producto maneja lotes (batch/serial), también verifica la existencia y vigencia del lote. Retorna el listado con el estado de validación (aprobado o rechazado) y el mensaje de error correspondiente a cada línea, siendo el punto de entrada para cargar detalles básicos de una factura o remisión de manera masiva.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SetBasicBillingDetailFromFile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SetBasicBillingDetailFromFile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida en bloque (copy & paste) los renglones de detalle de factura/remisión recibidos en XML, enriqueciendo cada línea con datos del producto, concepto o activo fijo y marcando estado/mensaje de validación por registro.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SetBasicBillingDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos esperados (DetailType, ItemCode, Quantity, Price, etc.); Cada fila debe declarar CountFields = 6 para considerarse estructuralmente válida; AddressId debe corresponder a una dirección existente en Common.Address para resolver la ciudad y aplicar retención de ICA por municipio; Para DetailType=1 (productos) el WarehouseId debe existir en Inventory.Warehouse; DetailType debe estar en el rango (0,5) exclusivo, es decir 1..4', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SetBasicBillingDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Se cargan todas las filas del XML (/Data/Row) a la tabla temporal para procesarlas una a una; [UPDATE] @TableXmlObject: Si CountFields <> 6 → StatusField=0 con mensaje ''no tiene la estructura requerida''; [UPDATE] @TableXmlObject: Si DetailType no está entre 1 y 4 → StatusField=0 con mensaje ''tipo de detalle ... no es válido''; [UPDATE] @TableXmlObject: Si ItemCode es NULL o vacío → StatusField=0 con mensaje ''código del registro ... esta vacío''; [UPDATE] @TableXmlObject: Si Quantity no es > 0 → StatusField=0 con mensaje ''cantidad ... debe ser mayor a 0''; [UPDATE] @TableXmlObject: Si Price no es > 0 → StatusField=0 con mensaje ''precio ... debe ser mayor a 0''; [UPDATE] @TableXmlObject: Si PercentageDiscount no está entre 0 y 100 → StatusField=0 con mensaje ''porcentaje de descuento ... debe estar entre 0 y 100''; [UPDATE] @TableXmlObject: DetailType=1: si el WarehouseId no existe en Inventory.Warehouse → StatusField=0 ''No se ha definido un almacen válido''; [UPDATE] @TableXmlObject: DetailType=1: si Inventory.InventoryProduct no contiene el ItemCode → StatusField=0 ''producto ... no existe''; [UPDATE] @TableXmlObject: DetailType=1 con producto que maneja lote (ProductSubGroup.HandlesBatch=1) y ItemBatchSerial vacío → StatusField=0 ''código del lote ... esta vacío''; [UPDATE] @TableXmlObject: DetailType=1 con producto que maneja lote y BatchSerial inexistente para (ProductId, BatchCode) → StatusField=0 ''lote ... no existe''; [UPDATE] @TableXmlObject: DetailType=1: si no existe PhysicalInventory para (Warehouse, Product, BatchSerial) con Quantity > cantidad solicitada → StatusField=0 ''no tiene la cantidad suficiente''; [UPDATE] @TableXmlObject: DetailType=2: si Billing.BillingConcept no contiene el ItemCode → StatusField=0 ''concepto ... no existe''; [UPDATE] @TableXmlObject: DetailType=3: si no existe FixedAssetPhysicalAsset con Plate=ItemCode y (HasOutput<>0 o AdquisitionType IN (3,8,10)) → StatusField=0 con mensaje específico: AdquisitionType=3 comodato, =8 comodato tercerizado, =10 renting operativo (no pueden venderse), o ''no existe o no se encuentra activo''; [UPDATE] @TableXmlObject: DetailType=4 (parte): siempre marca StatusField=0 con mensaje ''no se ha validado'' (validación no implementada); [UPDATE] @TableXmlObject: Si la fila pasa todas las validaciones → StatusField=1, MessageField=''Registro validado correctamente'' y se enriquece con ItemId, ItemDescription, IVA, retenciones (fuente e ICA) y PhysicalInventoryId/ItemBatchSerial sólo si HandlesBatch=1; [UPDATE] @TableXmlObject: Tras procesar todas las filas, las que estén en StatusField=1 y se repitan por (DetailType, ItemCode, ItemBatchSerial) son marcadas StatusField=0 con mensaje ''El Registro se encuentra duplicado''; [DELETE] @TableXmlObject: En CATCH se vacía la tabla temporal y se inserta un único registro con StatusField=0 y el ERROR_MESSAGE + línea; [RETURN_RESULT] @TableXmlObject: Devuelve SELECT * FROM @TableXmlObject con todas las filas validadas/rechazadas', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SetBasicBillingDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SetBasicBillingDetailFromFile';
-- GO
