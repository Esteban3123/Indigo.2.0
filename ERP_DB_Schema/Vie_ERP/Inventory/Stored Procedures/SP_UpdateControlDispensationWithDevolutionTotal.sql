CREATE PROCEDURE [Inventory].[SP_UpdateControlDispensationWithDevolutionTotal]
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
			@pddevId INT,					-- PharmaceuticalDispensingDevolutionId
			@pdddevId INT,					-- PharmaceuticalDispensingDevolutionDetailId
			@wId INT,						-- WarehouseId for the ConsignmentInventoryRemission
			@wdId INT,						-- WarehouseId for the PharmaceuticalDispensingDetail
			@pId INT,						-- ProductId
			@bsId INT,						-- BatchSerialId			
			@q INT,							-- Quantity for the ConsignmentInventoryRemissionDetailControl
			@tmp_q INT,						-- Temporal Quantity for the ConsignmentInventoryRemissionDetailControl in the cursor
			@qpl INT,						-- Quantity Pending for Legalization the ConsignmentInventoryRemissionDetailControl
			@bsq INT,						-- Quantity for the ConsignmentInventoryRemissionDetailBatchSerial
			@bsoq INT,						-- Outstanding Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bsuq INT,						-- Used Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bslq INT,						-- Legalized Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@bsrq INT,						-- Replacement Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			@tmp_bsrq INT,					-- Temporal Replacement Quantity for ConsignmentInventoryRemissionDetailBatchSerial
			--------------------------------------------------------------------------
			--Variables para realizar la actualizacion
			@in_cirdId INT,			
			@in_cirdcId INT			-- ConsignmentInventoryRemissionDetailControlId for dispensing

		SET @status_control = 0

/************************  DISPENSACIONES ********************************/

		DECLARE pharmaceutical_dispensing_cursor CURSOR FAST_FORWARD FOR 		
			SELECT
				cirdbs.Id, cirdc.Id, cird.Id, pdd.PharmaceuticalDispensingId, pdd.Id, pdddev.PharmaceuticalDispensingDevolutionId, pdddev.Id, 
				cir.WarehouseId, pdd.WarehouseId, pdd.ProductId, ip.BatchSerialId, 
				cirdc.Quantity, cirdc.QuantityPendingLegalization, cirdbs.Quantity, cirdbs.OutstandingQuantity, cirdbs.UsedQuantity, cirdbs.LegalizedQuantity, cirdbs.ReplacementQuantity
			FROM Inventory.ConsignmentInventoryRemission cir
			JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
			JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
			JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc ON cird.Id = cirdc.ConsignmentInventoryRemissionDetailId			
			JOIN Inventory.PharmaceuticalDispensingDevolutionDetail pdddev ON cirdc.EntityId = pdddev.PharmaceuticalDispensingDevolutionId AND cirdc.EntityDetailId = pdddev.Id
			JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pdddev.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
			JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
			JOIN Inventory.PhysicalInventory ip ON pddbs.PhysicalInventoryId = ip.Id
			WHERE cirdc.EntityName = 'PharmaceuticalDispensingDevolution' AND
				cir.WarehouseId <> pdd.WarehouseId

		OPEN pharmaceutical_dispensing_cursor  
			FETCH NEXT FROM pharmaceutical_dispensing_cursor INTO @cirdbsId, @cirdcId, @cirdId, @pdId, @pddId, @pddevId, @pdddevId,
				@wId, @wdId, @pId, @bsId, 
				@q, @qpl, @bsq, @bsoq, @bsuq, @bslq, @bsrq

			WHILE @@FETCH_STATUS = 0  
			BEGIN				
				SET @in_cirdcId = NULL
				SET @tmp_q = NULL

				--Obtenemos la dispensación que fue devuelta
				SELECT @in_cirdcId = cirdc.Id, @tmp_q = Quantity
				FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc 
				WHERE cirdc.EntityId = @pdId AND 
					cirdc.EntityDetailId = @pddId AND
					cirdc.QuantityPendingLegalization = 0

				--Validamos que la cantidad devuelta se la misma dispensada
				IF ISNULL(@tmp_q, 0) <> @q BEGIN
					 PRINT 'No se devolvió todas las unidades'
					 BREAK
				END

				--Obtenemos cualquier ConsignmentInventoryRemissionDetail que cumpla con el almacen, producto y lote
				SELECT @in_cirdId = cird.Id
				FROM Inventory.ConsignmentInventoryRemission cir
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
				INNER JOIN Inventory.Warehouse w ON cir.WarehouseId = w.Id
				WHERE cir.Status = 2 AND
					cir.WarehouseId = @wdId AND
					cird.ProductId = @pId AND 
					ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@bsId, 0) 

				--Validamos que exista un registro cumpla con el almacen, producto y lote
				IF ISNULL(@in_cirdId, 0) = 0 BEGIN
					 PRINT 'No se realizo ninguna remision de entrada para el producto en el almacen dispensado'
					 BREAK
				END
				
				--Dispensacion
				INSERT INTO Inventory.ConsignmentInventoryRemissionDetailControl
						(ConsignmentInventoryRemissionDetailId, BatchSerialId, MovementType, Quantity, QuantityPendingLegalization, Value, EntityId, EntityCode, EntityName, EntityDetailId, OperatingUnitId, FunctionalUnitId, CreationUser, CreationDate)
					SELECT @in_cirdId, BatchSerialId, MovementType, Quantity,	QuantityPendingLegalization, Value, EntityId, EntityCode, EntityName, EntityDetailId, OperatingUnitId, FunctionalUnitId, CreationUser, CreationDate
					FROM Inventory.ConsignmentInventoryRemissionDetailControl
					WHERE Id = @in_cirdcId				

				--Devolucion
				INSERT INTO Inventory.ConsignmentInventoryRemissionDetailControl
						(ConsignmentInventoryRemissionDetailId, BatchSerialId, MovementType, Quantity, QuantityPendingLegalization, Value, EntityId, EntityCode, EntityName, EntityDetailId, OperatingUnitId, FunctionalUnitId, CreationUser, CreationDate)
					SELECT @in_cirdId, BatchSerialId, MovementType, Quantity,	QuantityPendingLegalization, Value, EntityId, EntityCode, EntityName, EntityDetailId, OperatingUnitId, FunctionalUnitId, CreationUser, CreationDate
					FROM Inventory.ConsignmentInventoryRemissionDetailControl
					WHERE Id = @cirdcId				

				--Eliminamos los detalles errados
				DELETE FROM Inventory.ConsignmentInventoryRemissionDetailControl WHERE Id = @in_cirdcId
				DELETE FROM Inventory.ConsignmentInventoryRemissionDetailControl WHERE Id = @cirdcId
				
				SET @status_control = @status_control + 1
				FETCH NEXT FROM pharmaceutical_dispensing_cursor INTO @cirdbsId, @cirdcId, @cirdId, @pdId, @pddId, @pddevId, @pdddevId,
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