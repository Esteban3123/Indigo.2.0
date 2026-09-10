CREATE PROCEDURE [Inventory].[SP_UpdateControlQuantityReplacement]
	@wId INT,						-- WarehouseId
	@pId INT,						-- ProductId
	@bsId INT,						-- BatchSerialId
	@bsrq INT,						-- ReplacementQuantity
	@bsrq_control INT OUTPUT
AS
BEGIN	
	BEGIN TRY

/******************** DECLARACION DE VARIABLES *************************/
		
		--Variables para recorrer los movimientos de la remision recibidas
		DECLARE 
			--Variables para recorrer los detalles de la remisiones de inventario en consignacion
			@in_cirdbsId INT,
			@in_cirdbsQuantityUsed INT,
			@in_cirdbsReplacementQuantity INT,
			@tmp_cirdbsReplacementQuantity INT
			--------------------------------------------------------------------------			

		SET @bsrq_control = 0

/************************  DISPENSACIONES ********************************/

		DECLARE quantity_replacement_cursor CURSOR FAST_FORWARD FOR 		
			SELECT cirdbs.Id, cirdbs.UsedQuantity, cirdbs.ReplacementQuantity
			FROM Inventory.ConsignmentInventoryRemission cir
			INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird
				ON cir.Id = cird.ConsignmentInventoryRemissionId
			INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
				ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId			
			WHERE cir.Status = 2 AND
				cir.WarehouseId = @wId AND
				cird.ProductId = @pId AND 
				ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(@bsId, 0) AND
				(cirdbs.UsedQuantity > cirdbs.ReplacementQuantity)

		OPEN quantity_replacement_cursor  
			FETCH NEXT FROM quantity_replacement_cursor INTO @in_cirdbsId, @in_cirdbsQuantityUsed, @in_cirdbsReplacementQuantity

			WHILE @@FETCH_STATUS = 0  
			BEGIN
				IF NOT @bsrq > 0 BEGIN
					BREAK
				END

				SELECT @tmp_cirdbsReplacementQuantity = @in_cirdbsQuantityUsed - @in_cirdbsReplacementQuantity
				SELECT @tmp_cirdbsReplacementQuantity = IIF(@bsrq > @tmp_cirdbsReplacementQuantity, @tmp_cirdbsReplacementQuantity, @bsrq)
					
				----Actualizo el detalle correcto
				UPDATE cirdbs
					SET cirdbs.ReplacementQuantity = cirdbs.ReplacementQuantity + @tmp_cirdbsReplacementQuantity
				FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
				WHERE cirdbs.Id = @in_cirdbsId

				SELECT @bsrq = @bsrq - @tmp_cirdbsReplacementQuantity
				SELECT @bsrq_control = @bsrq_control + @tmp_cirdbsReplacementQuantity
				FETCH NEXT FROM quantity_replacement_cursor INTO @in_cirdbsId, @in_cirdbsQuantityUsed, @in_cirdbsReplacementQuantity
			END

		CLOSE quantity_replacement_cursor
		DEALLOCATE quantity_replacement_cursor

--/*************************** END ***************************************/
	END TRY
	BEGIN CATCH
		CLOSE quantity_replacement_cursor
		DEALLOCATE quantity_replacement_cursor

		PRINT 'ERROR (Dispensacion): ' + ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(20))
		SET @bsrq_control = -1
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que actualiza la cantidad de reposición de un producto en consignación para un lote o serial específico dentro de una bodega determinada. Recorre las remisiones de consignación activas (estado 2) buscando registros donde las unidades usadas superen las ya repuestas, y distribuye la cantidad de reposición indicada entre esos registros hasta agotarla o cubrir el déficit. Devuelve como parámetro de salida el total de unidades efectivamente imputadas como reposición, o -1 si ocurre un error. Se usa en el proceso de legalización o reposición de inventario en consignación para mantener el control de cuántas unidades consumidas han sido formalmente repuestas al proveedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateControlQuantityReplacement';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Distribuye una cantidad de reposición sobre los detalles de lotes/seriales de remisiones en consignación activas, incrementando ReplacementQuantity hasta cubrir lo usado pendiente o agotar la cantidad solicitada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir remisiones de inventario en consignación con Status = 2 (confirmadas/activas) para la bodega y producto indicados.; Debe existir al menos un detalle de lote/serial donde UsedQuantity > ReplacementQuantity (saldo pendiente por reponer).; El BatchSerialId debe coincidir (tratando NULL como 0) con el del detalle a actualizar.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan remisiones con Status = 2.; Nunca se incrementa ReplacementQuantity por encima de UsedQuantity en un detalle (se limita por UsedQuantity - ReplacementQuantity).; La suma total aplicada (@bsrq_control) nunca excede la cantidad de reposición solicitada inicialmente.; El emparejamiento por lote/serial trata NULL y 0 como equivalentes (ISNULL(...,0)).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario en consignación; Remisión de inventario; Reposición de mercancía; Lote/Serial; Bodega/Almacén; Dispensación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.ConsignmentInventoryRemissionDetailBatchSerial: Por cada detalle de lote/serial con UsedQuantity > ReplacementQuantity, suma a ReplacementQuantity el menor entre (UsedQuantity - ReplacementQuantity) y la cantidad de reposición restante (@bsrq), iterando hasta agotarla.; [RETURN_RESULT] @bsrq_control: Devuelve el total efectivamente aplicado a reposición; si ocurre una excepción en el TRY, retorna -1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT @bsrq > 0 dentro del cursor → BREAK: termina la distribución porque ya no queda cantidad por reponer.; si @bsrq > (UsedQuantity - ReplacementQuantity) del detalle actual → Aplica solo el saldo pendiente del detalle (UsedQuantity - ReplacementQuantity). else Aplica @bsrq completo al detalle actual.; si Excepción capturada en BEGIN CATCH → Cierra y libera el cursor, imprime el error y fija @bsrq_control = -1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateControlQuantityReplacement';
-- GO
