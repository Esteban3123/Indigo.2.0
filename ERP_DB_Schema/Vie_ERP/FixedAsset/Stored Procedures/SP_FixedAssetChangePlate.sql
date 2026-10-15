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