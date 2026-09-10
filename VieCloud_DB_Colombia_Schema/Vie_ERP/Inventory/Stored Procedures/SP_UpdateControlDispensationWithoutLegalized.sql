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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que corrige y reconstruye el control de dispensaciones farmacéuticas en consignación que quedaron sin legalizar, cuando la bodega de la remisión de consignación no coincide con la bodega donde se realizó la dispensación. Recorre los registros de control de remisiones de consignación (ConsignmentInventoryRemissionDetailControl) cuya cantidad total está completamente pendiente de legalización, y los reasigna a las remisiones correctas de la bodega de dispensación, actualizando las cantidades usadas, pendientes y disponibles en los lotes/seriales de cada remisión (ConsignmentInventoryRemissionDetailBatchSerial). Finalmente elimina los controles mal asignados e inserta nuevos registros de control vinculados a la remisión correcta, garantizando la trazabilidad y conciliación entre la dispensación de medicamentos y el inventario en consignación. Se usa como proceso de saneamiento de datos cuando existen inconsistencias de bodega entre las remisiones de consignación y las órdenes farmacéuticas dispensadas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reasigna controles de dispensación farmacéutica pendientes de legalización hacia las remisiones en consignación correctas cuando la dispensación se realizó desde un almacén distinto al de la remisión original, redistribuyendo cantidades por lote/serial.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un ConsignmentInventoryRemissionDetailControl con EntityName=''PharmaceuticalDispensing'' cuya Quantity sea igual a su QuantityPendingLegalization (control aún no legalizado).; El almacén de la remisión (cir.WarehouseId) debe ser distinto al almacén del detalle de dispensación (pdd.WarehouseId).; Para reasignar, deben existir remisiones en consignación con Status=2 (activas/legalizadas) en el almacén de la dispensación con el mismo producto, mismo lote/serial y OutstandingQuantity > 0.; El procedimiento [Inventory].[SP_UpdateControlQuantityReplacement] debe estar disponible para procesar las reposiciones.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa controles de dispensación farmacéutica cuya cantidad total aún coincide con la pendiente de legalización (no toca controles ya parcialmente legalizados).; Solo reasigna a remisiones con Status=2, mismo producto, mismo lote/serial (comparado con ISNULL=0) y OutstandingQuantity>0.; Las remisiones candidatas se consumen en orden FIFO por RemissionDate, Id.; La cantidad reasignada por iteración nunca excede el OutstandingQuantity disponible ni la cantidad pendiente del control.; QuantityPendingLegalization clonada nunca supera la cantidad efectivamente usada en la nueva línea de control.; @status_control cuenta la cantidad de controles totalmente reasignados; -1 indica error capturado.; Conservación de cantidad: lo que se descuenta de OutstandingQuantity en remisiones destino se compensa devolviéndolo a OutstandingQuantity y restando UsedQuantity en el detalle batch/serial origen.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Inventario en consignación; Remisión de inventario; Legalización de cantidades; Lote/Serial; Almacén/Bodega; Reposición (Replacement); Movimientos de control de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.ConsignmentInventoryRemissionDetailBatchSerial: Por cada remisión candidata encontrada en el almacén destino, incrementa UsedQuantity y disminuye OutstandingQuantity en la cantidad consumida (mínimo entre cantidad pendiente y OutstandingQuantity disponible).; [INSERT] Inventory.ConsignmentInventoryRemissionDetailControl: Por cada porción reasignada se clona el control original (mismos MovementType, Value, EntityId, EntityCode, EntityName, EntityDetailId, etc.) apuntando al ConsignmentInventoryRemissionDetailId de la remisión correcta, con la cantidad consumida y la cantidad pendiente de legalización calculadas.; [UPDATE] Inventory.ConsignmentInventoryRemissionDetailBatchSerial: Sobre el detalle batch/serial original (incorrecto) ajusta UsedQuantity y OutstandingQuantity revirtiendo la cantidad reasignada y, si aplica, recalcula ReplacementQuantity tras llamar SP_UpdateControlQuantityReplacement.; [DELETE] Inventory.ConsignmentInventoryRemissionDetailControl: Si tras la reasignación queda totalmente cubierto (@tmp_q=0 AND @tmp_qpl=0), elimina el control original y suma 1 a @status_control.; [UPDATE] Inventory.ConsignmentInventoryRemissionDetailControl: Si tras la reasignación queda saldo pendiente, actualiza el control original con las cantidades restantes (Quantity=@tmp_q, QuantityPendingLegalization=@tmp_qpl).; [RAISERROR] Inventory.ConsignmentInventoryRemissionDetailControl: Ante cualquier excepción cierra cursores e imprime ''ERROR (Dispensacion): ...'' y retorna @status_control=-1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @tmp_q no es mayor que 0 dentro del cursor de remisiones candidatas → BREAK del cursor interno: ya no hay cantidad pendiente por reasignar para ese control.; si @bsrq > @bsuq (la cantidad ya repuesta excede la cantidad usada tras reasignar) → Invoca SP_UpdateControlQuantityReplacement con la diferencia para reubicar la reposición; si retorna >0 reduce ReplacementQuantity, si no imprime error de actualización. else No se invoca el SP de reposición.; si @tmp_q = 0 AND @tmp_qpl = 0 (todo el control quedó reasignado) → DELETE del control original y se incrementa @status_control. else UPDATE del control original con las cantidades remanentes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_UpdateControlQuantityReplacement', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetailControl; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationWithoutLegalized';
-- GO
