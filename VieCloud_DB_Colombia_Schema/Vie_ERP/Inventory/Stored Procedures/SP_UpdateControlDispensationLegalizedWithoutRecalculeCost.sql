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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que actualiza el control de dispensaciones farmacéuticas legalizadas en inventarios de consignación, sin recalcular el costo unitario de los productos. Se ejecuta cuando una dispensación de medicamentos ocurre en una bodega diferente a la bodega origen de la remisión en consignación, corrigiendo las cantidades pendientes de legalización, cantidades usadas, legalidades y reposiciones en los registros de control de remisiones (ConsignmentInventoryRemissionDetailControl y ConsignmentInventoryRemissionDetailBatchSerial), así como en los comprobantes de entrada al inventario (EntranceVoucherDetail). Recorre mediante cursor todas las dispensaciones farmacéuticas vinculadas a remisiones en consignación cuyas cantidades de control no coinciden con las pendientes de legalización, y reconcilia los movimientos entre la remisión de origen y la bodega donde se realizó la dispensación real, reasignando reposiciones a otras remisiones cuando es necesario mediante el subprocedimiento SP_UpdateControlQuantityReplacement. Retorna un parámetro de salida (@status_control) que indica el resultado de la ejecución, útil para auditoría y control de inventario de medicamentos en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reasigna dispensaciones farmacéuticas legalizadas contra una remisión de consignación equivocada hacia la remisión correcta del almacén dispensador, recolocando cantidades usadas/legalizadas/pendientes sin recalcular costos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en ConsignmentInventoryRemissionDetailControl con EntityName=''PharmaceuticalDispensing'' cuyo control referencie un detalle de dispensación cuyo WarehouseId difiera del WarehouseId de la remisión de consignación origen.; El control debe tener Quantity distinto de QuantityPendingLegalization (es decir, ya hubo legalización parcial o total).; Debe existir un EntranceVoucherDetail vinculado al ConsignmentInventoryRemissionDetailBatchSerial con UnitValue y Quantity coincidentes con el detalle de la remisión.; Debe existir otra remisión de consignación con Status=2, mismo producto, mismo lote/serial y WarehouseId igual al almacén de la dispensación, con OutstandingQuantity > 0 suficiente para cubrir la cantidad.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa controles cuya EntityName=''PharmaceuticalDispensing'' y donde el almacén de la remisión origen difiere del almacén de la dispensación.; La remisión destino se selecciona con Status=2, mismo producto, mismo lote/serial (tratando NULL como 0) y prioriza la de mayor OutstandingQuantity, luego la más antigua por RemissionDate y menor Id.; El traslado de cantidades es conservativo: lo que se resta de la remisión errada (Used/Legalized) se suma exactamente a la correcta, y a la inversa para OutstandingQuantity.; No recalcula costos del inventario (por diseño, según el nombre y ausencia de operaciones de costeo).; Ante excepción, libera el cursor y retorna @status_control = -1, sin propagar el error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Remisión de inventario en consignación; Lote/Serial; Legalización de consignación; Reposición (replacement) de unidades; Comprobante de entrada; Almacén/bodega', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.ConsignmentInventoryRemissionDetailBatchSerial: En la remisión errada (cirdbs.Id=@cirdbsId): resta @q a UsedQuantity y LegalizedQuantity, suma @q a OutstandingQuantity y fija ReplacementQuantity ajustada por reposiciones previas.; [UPDATE] Inventory.ConsignmentInventoryRemissionDetailBatchSerial: En la remisión correcta encontrada (cirdbs.Id=@in_cirdbsId): suma @q a UsedQuantity y LegalizedQuantity y resta @q a OutstandingQuantity.; [UPDATE] Inventory.ConsignmentInventoryRemissionDetailControl: Reapunta el control de dispensación al detalle de la remisión correcta (ConsignmentInventoryRemissionDetailId = @in_cirdId) cuando se encuentra remisión válida en el almacén dispensador.; [UPDATE] Inventory.EntranceVoucher: Cambia el WarehouseId del comprobante de entrada asociado al del almacén de la dispensación (@wdId) para alinearlo con la remisión correcta.; [RETURN_RESULT] @status_control: Devuelve por OUTPUT el conteo de filas reasignadas; -1 si ocurre una excepción capturada en el CATCH.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @q > ISNULL(@in_cirdbsOutstandingQuantity, 0) — la cantidad a mover excede el saldo pendiente de la remisión candidata → Imprime aviso ''No se dispone de las cantidades suficientes…'' y rompe el cursor (BREAK), abortando el proceso para los registros restantes. else Continúa con la reasignación de cantidades y actualización del control.; si @bsrq > (@bsuq - @q) — la cantidad reemplazada supera el nuevo usado tras descontar @q → Llama a Inventory.SP_UpdateControlQuantityReplacement para intentar trasladar la reposición sobrante a otra remisión; si retorna >0 reduce ReplacementQuantity en esa magnitud, en caso contrario imprime ''Error al actualizar las cantidades reemplazadas''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_UpdateControlQuantityReplacement', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetailControl; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PhysicalInventory; Inventory.EntranceVoucherDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlDispensationLegalizedWithoutRecalculeCost';
-- GO
