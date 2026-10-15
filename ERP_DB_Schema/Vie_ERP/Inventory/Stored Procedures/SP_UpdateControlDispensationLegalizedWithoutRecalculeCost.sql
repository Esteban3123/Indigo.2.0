CREATE PROCEDURE [Inventory].[SP_UpdateControlDispensationLegalizedWithoutRecalculeCost]
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
			@qpl INT,						-- Quantity Pending for Legalization the ConsignmentInventoryRemissionDetailControl
			@tmp_qpl INT,					-- Temporal Quantity Pending for Legalization the ConsignmentInventoryRemissionDetailControl
			@bsq INT,						-- Quantity for the ConsignmentInventoryRemissionDetailBatchSerial
			@bsoq INT,						-- Outstanding Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bsuq INT,						-- Used Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bslq INT,						-- Legalized Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bsrq INT,						-- Replacement Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@tmp_bsrq INT,					-- Temporal Replacement Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@evId INT,						-- EntranceVoucherId
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
				evd.EntranceVoucherId, cirdbs.Id, cirdc.Id, cird.Id, pdd.PharmaceuticalDispensingId, pdd.Id, 
				cir.WarehouseId, pdd.WarehouseId, pdd.ProductId, ip.BatchSerialId, 
				cirdc.Quantity, cirdc.QuantityPendingLegalization, cirdbs.Quantity, cirdbs.OutstandingQuantity, cirdbs.UsedQuantity, cirdbs.LegalizedQuantity, cirdbs.ReplacementQuantity
			FROM Inventory.ConsignmentInventoryRemission cir 
			JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
			JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
			JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc ON cird.Id = cirdc.ConsignmentInventoryRemissionDetailId
			JOIN Inventory.PharmaceuticalDispensingDetail pdd ON cirdc.EntityDetailId = pdd.Id
			JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
			JOIN Inventory.PhysicalInventory ip ON pddbs.PhysicalInventoryId = ip.Id
			JOIN Inventory.EntranceVoucherDetail evd ON evd.ConsignmentInventoryRemissionDetailBatchSerialId = cirdbs.Id
			WHERE cirdc.EntityName = 'PharmaceuticalDispensing' AND
				cir.WarehouseId <> pdd.WarehouseId AND
				(
					cirdc.Quantity <> cirdc.QuantityPendingLegalization
				) AND cird.UnitValue = evd.UnitValue AND
					cird.Quantity = evd.Quantity

		OPEN pharmaceutical_dispensing_cursor  
			FETCH NEXT FROM pharmaceutical_dispensing_cursor INTO @evId, @cirdbsId, @cirdcId, @cirdId, @pdId, @pddId, 
				@wId, @wdId, @pId, @bsId, 
				@q, @qpl, @bsq, @bsoq, @bsuq, @bslq, @bsrq

			WHILE @@FETCH_STATUS = 0  
			BEGIN
				SELECT TOP 1 @in_cirdbsId = cirdbs.Id, @in_cirdId = cird.Id, @in_cirdbsOutstandingQuantity = cirdbs.OutstandingQuantity
				FROM Inventory.ConsignmentInventoryRemission cir
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
					ON cir.Id = cird.ConsignmentInventoryRemissionId
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
					ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
				LEFT JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc 
					ON cird.Id = cirdc.ConsignmentInventoryRemissionDetailId
				WHERE cir.Status = 2 AND
					cir.WarehouseId = @wdId AND
					cird.ProductId = @pId AND 
					ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@bsId, 0) AND
					cirdbs.OutstandingQuantity > 0
				ORDER BY cirdbs.OutstandingQuantity DESC, cir.RemissionDate, cir.Id
				
				IF @q > ISNULL(@in_cirdbsOutstandingQuantity, 0) BEGIN
					PRINT 'No se dispone de las cantidades suficientes en una sola remision o registro de remision'
					BREAK
				END

				SET @bsuq = @bsuq - @q
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

				--Actualizo el detalle de las cantidades de la remision errada
				UPDATE cirdbs
					SET cirdbs.UsedQuantity = cirdbs.UsedQuantity - @q,						
						cirdbs.OutstandingQuantity = cirdbs.OutstandingQuantity + @q,
						cirdbs.LegalizedQuantity = cirdbs.LegalizedQuantity - @q,
						cirdbs.ReplacementQuantity = @bsrq
				FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
				WHERE cirdbs.Id = @cirdbsId
				
				----Actualizo el detalle correcto
				UPDATE cirdbs
					SET cirdbs.UsedQuantity = cirdbs.UsedQuantity + @q,						
						cirdbs.OutstandingQuantity = cirdbs.OutstandingQuantity - @q,
						cirdbs.LegalizedQuantity = cirdbs.LegalizedQuantity + @q
				FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
				WHERE cirdbs.Id = @in_cirdbsId

				----Actualizo el control con el detalle correcto
				UPDATE cirdc
					SET cirdc.ConsignmentInventoryRemissionDetailId = @in_cirdId
				FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
				WHERE cirdc.Id = @cirdcId

				----Actualizo el almacen del comprobante de entrada
				UPDATE ev
					SET ev.WarehouseId = @wdId
				FROM Inventory.EntranceVoucher ev
				WHERE ev.Id = @evId				
								
				SET @status_control = @status_control + 1
				FETCH NEXT FROM pharmaceutical_dispensing_cursor INTO @evId, @cirdbsId, @cirdcId, @cirdId, @pdId, @pddId, 
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