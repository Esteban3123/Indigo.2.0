CREATE PROCEDURE [Inventory].[SP_UpdateControlDispensationWithoutLegalized]
	@status_control INT OUTPUT
AS
BEGIN	
	BEGIN TRY

/******************** DECLARACION DE VARIABLES *************************/
		
		--Variables para recorrer los movimientos de la remision recibidas
		DECLARE 
			@cirdbsId INT,					-- ConsignmentInventoryRemissionDetailBatchSerialId
			@cirdcId INT,					-- ConsignmentInventoryRemissionDetailControlId
			@cirdId INT,					-- ConsignmentInventoryRemissionDetailId
			@pdId INT,						-- PharmaceuticalDispensingId
			@pddId INT,						-- PharmaceuticalDispensingDetailId
			@wId INT,						-- WarehouseId for the ConsignmentInventoryRemission
			@wdId INT,						-- WarehouseId for the PharmaceuticalDispensingDetail
			@pId INT,						-- ProductId
			@bsId INT,						-- BatchSerialId			
			@q INT,							-- Quantity for the ConsignmentInventoryRemissionDetailControl
			@tmp_q INT,						-- Temporal Quantity for the ConsignmentInventoryRemissionDetailControl in the cursor
			@qpl INT,						-- Quantity Pending for Legalization the ConsignmentInventoryRemissionDetailControl
			@tmp_qpl INT,					-- Temporal Quantity Pending for Legalization the ConsignmentInventoryRemissionDetailControl
			@bsq INT,						-- Quantity for the ConsignmentInventoryRemissionDetailBatchSerial
			@bsoq INT,						-- Outstanding Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bsuq INT,						-- Used Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bslq INT,						-- Legalized Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bsrq INT,						-- Replacement Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@tmp_bsrq INT,					-- Temporal Replacement Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			--------------------------------------------------------------------------
			--Variables para recorrer los detalles de la remisiones de inventario en consignacion
			@in_cirId INT,
			@in_cirdId INT,
			@in_cirdbsId INT,
			@in_cirdcId INT,
			@in_cirdbsOutstandingQuantity INT,
			@in_cirdbsUsedQuantity INT,
			@in_QuantityUsed INT,
			@in_QuantityPendingLegalization INT,
			@tmp_QuantityPendingLegalization INT,
			--------------------------------------------------------------------------
			@substatus INT,					-- Control ejecucion procedimientos internos
			@subreplacement INT					-- Control ejecucion procedimientos replacement

		SET @status_control = 0

/************************  DISPENSACIONES ********************************/

		DECLARE pharmaceutical_dispensing_cursor CURSOR FAST_FORWARD FOR 		
			SELECT
				cirdbs.Id, cirdc.Id, cird.Id, pdd.PharmaceuticalDispensingId, pdd.Id, 
				cir.WarehouseId, pdd.WarehouseId, pdd.ProductId, ip.BatchSerialId, 
				cirdc.Quantity, cirdc.QuantityPendingLegalization, cirdbs.Quantity, cirdbs.OutstandingQuantity, cirdbs.UsedQuantity, cirdbs.LegalizedQuantity, cirdbs.ReplacementQuantity
			FROM Inventory.ConsignmentInventoryRemission cir 
			JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
			JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
			JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc ON cird.Id = cirdc.ConsignmentInventoryRemissionDetailId
			JOIN Inventory.PharmaceuticalDispensingDetail pdd ON cirdc.EntityDetailId = pdd.Id
			JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
			JOIN Inventory.PhysicalInventory ip ON pddbs.PhysicalInventoryId = ip.Id
			WHERE cirdc.EntityName = 'PharmaceuticalDispensing' AND
				cir.WarehouseId <> pdd.WarehouseId AND
				(
					cirdc.Quantity = cirdc.QuantityPendingLegalization
				)

		OPEN pharmaceutical_dispensing_cursor  
			FETCH NEXT FROM pharmaceutical_dispensing_cursor INTO @cirdbsId, @cirdcId, @cirdId, @pdId, @pddId, 
				@wId, @wdId, @pId, @bsId, 
				@q, @qpl, @bsq, @bsoq, @bsuq, @bslq, @bsrq

			WHILE @@FETCH_STATUS = 0  
			BEGIN
				SET @tmp_q = @q
				SET @tmp_qpl = @q

				DECLARE remission_detail_cursor CURSOR FAST_FORWARD FOR 		
					SELECT cir.Id, cird.Id, cirdbs.Id, cirdbs.OutstandingQuantity, cirdbs.UsedQuantity
						FROM Inventory.ConsignmentInventoryRemission cir
						INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
							ON cir.Id = cird.ConsignmentInventoryRemissionId
						INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
							ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
						INNER JOIN Inventory.Warehouse w
							ON cir.WarehouseId = w.Id
						WHERE cir.Status = 2 AND
							cir.WarehouseId = @wdId AND
							cird.ProductId = @pId AND 
							ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@bsId, 0) AND
							cirdbs.OutstandingQuantity > 0
						ORDER BY cir.RemissionDate, cir.Id

				OPEN remission_detail_cursor  
					FETCH NEXT FROM remission_detail_cursor INTO @in_cirId, @in_cirdId, @in_cirdbsId, @in_cirdbsOutstandingQuantity, @in_cirdbsUsedQuantity

					WHILE @@FETCH_STATUS = 0  
					BEGIN
						IF NOT @tmp_q > 0 BEGIN
							BREAK
						END

						SELECT @in_QuantityUsed = IIF(@tmp_q > @in_cirdbsOutstandingQuantity, @in_cirdbsOutstandingQuantity, @tmp_q)
						SELECT @tmp_QuantityPendingLegalization = IIF(@tmp_qpl > @in_QuantityUsed, @in_QuantityUsed, @tmp_qpl)

						--PRINT 'In use: ' + CAST(@in_QuantityUsed AS VARCHAR(20)) + ', Pending Legalization: ' + CAST(@tmp_QuantityPendingLegalization AS VARCHAR(20))
					
						----Actualizo el detalle correcto
						UPDATE cirdbs
							SET cirdbs.UsedQuantity = cirdbs.UsedQuantity + @in_QuantityUsed,						
								cirdbs.OutstandingQuantity = cirdbs.OutstandingQuantity - @in_QuantityUsed
						FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
						WHERE cirdbs.Id = @in_cirdbsId

						INSERT INTO Inventory.ConsignmentInventoryRemissionDetailControl
								  (ConsignmentInventoryRemissionDetailId, BatchSerialId, MovementType, Quantity,			QuantityPendingLegalization,		Value, EntityId, EntityCode, EntityName, EntityDetailId, OperatingUnitId, FunctionalUnitId, CreationUser, CreationDate)
							SELECT @in_cirdId, BatchSerialId, MovementType, @in_QuantityUsed,	@tmp_QuantityPendingLegalization,	Value, EntityId, EntityCode, EntityName, EntityDetailId, OperatingUnitId, FunctionalUnitId, CreationUser, CreationDate
							FROM Inventory.ConsignmentInventoryRemissionDetailControl
							WHERE Id = @cirdcId

						SELECT @tmp_q = @tmp_q - @in_QuantityUsed
						SELECT @tmp_qpl = @tmp_qpl - @tmp_QuantityPendingLegalization
						FETCH NEXT FROM remission_detail_cursor INTO @in_cirId, @in_cirdId, @in_cirdbsId, @in_cirdbsOutstandingQuantity, @in_cirdbsUsedQuantity
					END

				CLOSE remission_detail_cursor
				DEALLOCATE remission_detail_cursor

				SET @bsuq = @bsuq - (@q - @tmp_q)
				SET @bsoq = @bsoq + (@q - @tmp_q)

				--Si se ha realizado reposiciones que incluyen las cantidades
				IF (@bsrq > @bsuq) BEGIN
					--Errores por analizar, se repusieron las unidades
					--¿Es posible enviar esa reposicion a otra remision?

					SET @subreplacement = 0
					SET @tmp_bsrq = @bsrq - @bsuq
					--Buscamos otra remision con el mismo producto, lote y el mismo almacen donde poder actualizar las cantidades repuestas
					EXEC [Inventory].[SP_UpdateControlQuantityReplacement] @wId, @pId, @bsId, @tmp_bsrq, @subreplacement OUT
					--Si hubo cantidades reemplazadas actualizamos la cantidad
					IF @subreplacement > 0 BEGIN
						SET @bsrq = @bsrq - @subreplacement
					END
					ELSE BEGIN
						PRINT 'Error al actualizar las cantidades reemplazadas'
					END
				END

				----Actualizo el detalle incorrecto
				UPDATE Inventory.ConsignmentInventoryRemissionDetailBatchSerial
					SET UsedQuantity = @bsuq,
						OutstandingQuantity = @bsoq,
						ReplacementQuantity = @bsrq
				WHERE Id = @cirdbsId

				IF @tmp_q = 0 AND @tmp_qpl = 0 BEGIN
					SET @status_control = @status_control + 1
					DELETE FROM Inventory.ConsignmentInventoryRemissionDetailControl WHERE Id = @cirdcId
				END
				ELSE BEGIN
					PRINT 'Algo quedo pendiente'
					UPDATE cirdc 
						SET Quantity = @tmp_q,
							QuantityPendingLegalization = @tmp_qpl
					FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
					WHERE cirdc.Id = @cirdcId
				END
				
				pharmaceutical_dispensing_cursor_fetch:
				FETCH NEXT FROM pharmaceutical_dispensing_cursor INTO @cirdbsId, @cirdcId, @cirdId, @pdId, @pddId, 
					@wId, @wdId, @pId, @bsId, 
					@q, @qpl, @bsq, @bsoq, @bsuq, @bslq, @bsrq
			END
		CLOSE pharmaceutical_dispensing_cursor
		DEALLOCATE pharmaceutical_dispensing_cursor

--/*************************** END ***************************************/
	END TRY
	BEGIN CATCH
		CLOSE pharmaceutical_dispensing_cursor
		DEALLOCATE pharmaceutical_dispensing_cursor

		PRINT 'ERROR (Dispensacion): ' + ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(20))
		SET @status_control = -1
	END CATCH
END