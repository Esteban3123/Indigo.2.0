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