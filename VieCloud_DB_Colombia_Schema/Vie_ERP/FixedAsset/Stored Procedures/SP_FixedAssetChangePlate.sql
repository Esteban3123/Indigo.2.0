-- =================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 26/07/2017
-- Description:	Procedimiento que se encarga de cambiar la placa en los procesos en los cuales se llama 
-- =================================================================================
CREATE PROCEDURE [FixedAsset].[SP_FixedAssetChangePlate] 
	@Xml as xml,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Tabla temporal de ids de la tabla PhysicalAsset que vienen desde el xml
	declare @TableTemp table(Id int IDENTITY PRIMARY KEY, FixedAssetPhysicalAssetId int, OldPlate varchar(50), NewPlate varchar(50))

	--Tabla temporal que se devuelve y sirve para poder validar
	declare @TableMessage table(CodeMessage int, MessageReturn varchar(max))

	--begin transaction
	begin try
	
		--Se obtiene la informacion que viene del xml
		insert into @TableTemp
		select 
		t.x.value('FixedAssetPhysicalAssetId[1]','int') as FixedAssetPhysicalAssetId,
		t.x.value('OldPlate[1]','varchar(50)') as OldPlate,
		t.x.value('NewPlate[1]','varchar(50)') as NewPlate
		from @Xml.nodes('/TableXml') t(x)
		
		declare @FixedAssetPhysicalAssetId as int --Id del physical
		declare @OldPlate as varchar(50) --Placa vieja
		declare @NewPlate as varchar(50) --Placa nueva
		declare InfoItem Cursor For Select [FixedAssetPhysicalAssetId], [OldPlate], [NewPlate] From @TableTemp

		Open InfoItem

		Fetch Next From InfoItem Into @FixedAssetPhysicalAssetId, @OldPlate, @NewPlate

		While @@fetch_status = 0
		begin

			--Se valida que la nueva placa no exista en PhysicalAsset
			if (select COUNT(*) from FixedAsset.FixedAssetPhysicalAsset where Plate = @NewPlate) > 0
			begin
			
				--Se inserta en la tabla de mensajes
				insert into @TableMessage(CodeMessage, MessageReturn) values(999, 'La nueva placa ' + @NewPlate + ' ya existe en la BD')
					
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @FixedAssetPhysicalAssetId, @OldPlate, @NewPlate
				continue
			end

			--Se actualiza la placa en el saldo inicial
			UPDATE faibi 
				set Plate = @NewPlate 
			FROM FixedAsset.FixedAssetInitialBalance faib
			JOIN FixedAsset.FixedAssetInitialBalanceItem faibi ON faib.Id = faibi.FixedAssetInitialBalanceId
			where faib.Status = 2 AND faibi.Plate = @OldPlate

			--Se actualiza la placa en la remision de entrada
			update fareid
				set Plate = @NewPlate 
			FROM FixedAsset.FixedAssetRemissionEntrance fare
			JOIN FixedAsset.FixedAssetRemissionEntranceItem farei ON fare.Id = farei.RemissionEntranceId
			JOIN FixedAsset.FixedAssetRemissionEntranceItemDetail fareid ON farei.Id = fareid.RemissionEntranceItemId
			where fare.Status = 2 AND fareid.Plate = @OldPlate

			--Se actualiza la placa en el ingreso
			update faeid
				set Plate = @NewPlate 
			FROM FixedAsset.FixedAssetEntry fae
			JOIN FixedAsset.FixedAssetEntryItem faei ON fae.Id = faei.FixedAssetEntryId
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
			WHERE fae.Status = 2 AND faeid.Plate = @OldPlate

			--Se actualiza la placa en la tabla FixedAssetPhysicalAsset
			update FixedAsset.FixedAssetPhysicalAsset set Plate = @NewPlate where Id = @FixedAssetPhysicalAssetId

			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @FixedAssetPhysicalAssetId, @OldPlate, @NewPlate
			continue
		end

		close InfoItem
		deallocate InfoItem

		--Si la tabla de mensajes no se lleno con algun error
		if (select COUNT(*) from @TableMessage where CodeMessage = 999) = 0
		begin
			--Inserto un valor de ok en la tabla de mensajes
			insert into @TableMessage(CodeMessage, MessageReturn) values(0, 'OK')
		end
		
		--commit transaction
		select * from @TableMessage

	end try
	begin catch

		--Se limpia la tabla de mensajes
		delete from @TableMessage

		--Se inserta en la tabla de mensajes el error
		insert into @TableMessage(CodeMessage, MessageReturn) values(999, ERROR_MESSAGE())

		--rollback transaction
		select * from @TableMessage

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cambia o actualiza el número de placa de uno o varios activos fijos en todos los documentos donde esté registrada esa placa. Recibe un XML con la placa antigua, la placa nueva y el identificador del activo físico; valida que la nueva placa no exista previamente en el maestro de activos físicos (FixedAssetPhysicalAsset) y luego propaga el cambio en los saldos iniciales (FixedAssetInitialBalanceItem), las remisiones de entrada (FixedAssetRemissionEntranceItemDetail) y las entradas o adquisiciones de activos (FixedAssetEntryItemDetail), actualizando únicamente los documentos en estado confirmado (Status = 2). Retorna una tabla de mensajes indicando éxito o los errores encontrados por cada placa procesada, siendo útil para corrección de placas erradas sin perder la trazabilidad histórica del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_FixedAssetChangePlate';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_FixedAssetChangePlate';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Cambia la placa de identificación de activos fijos propagando el nuevo valor en el maestro y en documentos confirmados (saldos iniciales, remisiones de entrada e ingresos), validando previamente que la nueva placa no exista.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe traer nodos /TableXml con FixedAssetPhysicalAssetId, OldPlate y NewPlate.; Los documentos a actualizar (saldo inicial, remisión de entrada, entrada) deben estar en Status = 2 (confirmado) para que el cambio se propague.; La nueva placa no debe existir previamente en FixedAssetPhysicalAsset.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo propaga cambios de placa sobre documentos con Status = 2.; Nunca actualiza la placa si la nueva ya existe en el maestro de activos físicos.; Procesa cada placa del XML de forma independiente vía cursor; un error de duplicidad en una no detiene el resto.; Siempre retorna una tabla de mensajes (al menos una fila: OK, error de duplicidad o excepción capturada).; Ante excepción, descarta los mensajes acumulados y retorna únicamente el ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Placa de activo; Saldo inicial de activos fijos; Remisión de entrada; Entrada/ingreso de activos; Estado confirmado del documento', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableMessage: Cuando COUNT(*) en FixedAssetPhysicalAsset con Plate = NewPlate > 0, se inserta mensaje código 999 ''La nueva placa ... ya existe en la BD'' y se omite el cambio para esa placa.; [UPDATE] FixedAsset.FixedAssetInitialBalanceItem: Cuando el saldo inicial padre tiene Status = 2 y el item tiene Plate = OldPlate, se actualiza Plate = NewPlate.; [UPDATE] FixedAsset.FixedAssetRemissionEntranceItemDetail: Cuando la remisión de entrada padre tiene Status = 2 y el detalle tiene Plate = OldPlate, se actualiza Plate = NewPlate.; [UPDATE] FixedAsset.FixedAssetEntryItemDetail: Cuando la entrada padre tiene Status = 2 y el detalle tiene Plate = OldPlate, se actualiza Plate = NewPlate.; [UPDATE] FixedAsset.FixedAssetPhysicalAsset: Para el Id recibido se actualiza Plate = NewPlate en el maestro de activos físicos.; [INSERT] @TableMessage: Si no se registró ningún mensaje con CodeMessage = 999, se inserta (0, ''OK'').; [INSERT] @TableMessage: En CATCH se limpia la tabla de mensajes y se inserta (999, ERROR_MESSAGE()).; [RETURN_RESULT] @TableMessage: Al final (éxito o error) se retorna SELECT * FROM @TableMessage.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en FixedAssetPhysicalAsset con Plate = NewPlate → Registra mensaje 999 de placa duplicada y salta a la siguiente fila del cursor sin propagar el cambio. else Procede a actualizar la placa en saldos iniciales, remisiones de entrada, entradas y maestro físico.; si Existen mensajes con CodeMessage = 999 en @TableMessage → Retorna los mensajes de error sin agregar OK. else Inserta mensaje (0, ''OK'') antes de retornar.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetInitialBalance; FixedAsset.FixedAssetInitialBalanceItem; FixedAsset.FixedAssetRemissionEntrance; FixedAsset.FixedAssetRemissionEntranceItem; FixedAsset.FixedAssetRemissionEntranceItemDetail; FixedAsset.FixedAssetEntry; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetEntryItemDetail', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetChangePlate';
-- GO
