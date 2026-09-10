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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que actualiza el control de dispensación farmacéutica en consignación cuando se registra una devolución total de medicamentos dispensados desde una bodega diferente a la bodega de origen de la remisión en consignación. Recorre mediante cursor todos los movimientos de control vinculados a devoluciones de dispensación (PharmaceuticalDispensingDevolution) donde la bodega de la dispensación no coincide con la bodega de la remisión en consignación, valida que se hayan devuelto exactamente todas las unidades dispensadas y reasigna los registros de control (ConsignmentInventoryRemissionDetailControl) al detalle de remisión correspondiente al almacén donde realmente se dispensó el medicamento, corrigiendo así la trazabilidad de lotes y seriales entre ConsignmentInventoryRemissionDetailBatchSerial y el inventario físico (PhysicalInventory). Garantiza la coherencia del stock en consignación por bodega, producto y lote/serial luego de una devolución total, devolviendo un indicador de estado (@status_control) que informa si el proceso completó exitosamente o si se detectaron inconsistencias como devoluciones parciales o remisiones de entrada faltantes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reasigna los movimientos de control de inventario en consignación (dispensación y su devolución total) desde el detalle de remisión incorrecto hacia el detalle de remisión correspondiente a la bodega real de dispensación, cuando se devolvieron todas las unidades.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir devoluciones de dispensación (PharmaceuticalDispensingDevolutionDetail) referenciadas por registros de control con EntityName=''PharmaceuticalDispensingDevolution''; La bodega de la remisión de consignación debe ser distinta a la bodega de la dispensación; Debe existir una ConsignmentInventoryRemission con Status=2 para la bodega de dispensación, mismo producto y mismo lote/serial; La dispensación correspondiente debe tener un control con QuantityPendingLegalization=0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa controles cuyo EntityName sea ''PharmaceuticalDispensingDevolution''; Solo actúa cuando la bodega de la remisión difiere de la bodega de la dispensación (cir.WarehouseId <> pdd.WarehouseId); La remisión destino debe estar en Status=2 (legalizada/activa); La dispensación origen identificada debe tener QuantityPendingLegalization=0 (totalmente legalizada); La cantidad devuelta debe ser exactamente igual a la cantidad dispensada (devolución total); de lo contrario, no se reasigna; El emparejamiento entre bodegas requiere coincidencia exacta de producto y lote/serial (tratando NULL como 0); Por cada movimiento procesado exitosamente, status_control se incrementa en 1; si hay error global, queda en -1; No se modifican cantidades: los registros de control se duplican tal cual y luego se eliminan los originales (reasignación al ConsignmentInventoryRemissionDetail correcto)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de dispensación; Remisión de inventario en consignación; Lote/Serial; Bodega/Almacén; Legalización de inventario en consignación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.ConsignmentInventoryRemissionDetailControl: Cuando la cantidad devuelta coincide con la dispensada y existe una remisión (Status=2) que coincide en bodega/producto/lote, se inserta una copia del control de la dispensación original asociándolo al ConsignmentInventoryRemissionDetail correcto; [INSERT] Inventory.ConsignmentInventoryRemissionDetailControl: Bajo las mismas condiciones, se inserta una copia del control de la devolución asociándolo al ConsignmentInventoryRemissionDetail correcto; [DELETE] Inventory.ConsignmentInventoryRemissionDetailControl: Tras duplicar el control de dispensación, se elimina el registro original (Id = in_cirdcId) considerado errado; [DELETE] Inventory.ConsignmentInventoryRemissionDetailControl: Tras duplicar el control de devolución, se elimina el registro original de devolución (Id = cirdcId); [RETURN_RESULT] : Devuelve por OUTPUT el contador de movimientos reasignados, o -1 si ocurrió una excepción', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La cantidad devuelta (tmp_q de la dispensación con QuantityPendingLegalization=0) no coincide con la cantidad del control de devolución (q) → Se imprime ''No se devolvió todas las unidades'' y se interrumpe el ciclo (BREAK) sin reasignar movimientos else Continúa el proceso de búsqueda de remisión destino; si No existe ConsignmentInventoryRemissionDetail con Status=2 que coincida en bodega de dispensación, producto y lote/serial → Se imprime ''No se realizo ninguna remision de entrada para el producto en el almacen dispensado'' y se interrumpe el ciclo (BREAK) else Se reasignan los movimientos de control (dispensación y devolución) al detalle de remisión encontrado y se eliminan los originales; si Ocurre cualquier excepción dentro del TRY → Se cierra y libera el cursor, se imprime el error con línea y se asigna status_control = -1', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetailControl; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDetail; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithDevolutionTotal';
-- GO
