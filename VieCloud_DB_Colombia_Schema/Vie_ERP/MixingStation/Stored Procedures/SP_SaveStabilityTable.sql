-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 04/11/2020
-- Description:	Procedimiento que se encarga de guardar tabla de estabilidad en central de mezclas
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_SaveStabilityTable]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables para guardar la cabecera
	declare @Id int, @Code varchar(20), @Name varchar(100), @StabilityDate datetime, @Observations varchar(300), @Status bit, @OperatingUnitId int

	--Tabla de detalles
	declare @StabilityTableDetail table(RowId int, Id int, StabilityTableId int, ATCId int, ProductId int, HourStabilityProduct int, UnitDoseTypeId int, AllowableDoses int, 
	Observations varchar(300), IsDelete bit)
	
	--Tabla de detalles de diluciones
	declare @StabilityTableDetailDilution table(RowId int, Id int, StabilityTableDetailId int, ATCId int, ConcentrationMaximum decimal(18,2), 
	ConcentrationMinimum decimal(18,2), PhotoProtection bit, InfusionTime int, BibliographicReference varchar(300), IsDelete bit, Container varchar(20), 
	HourStability int, StorageTemperatureId int)

	--Tabla de detalles de reconstituciones
	declare @StabilityTableDetailReconstitution table(RowId int, Id int, StabilityTableDetailId int, ATCId int, Volume decimal(5,2), 
	HourStability int, Observations varchar(1000), StorageTemperatureId int,IsDelete bit)

	Begin try
		
		--Se obtienen los datos para la cabecera
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(100)'),
			@StabilityDate = convert(date, t.x.value('StabilityDate[1]','varchar(20)'), 103),
			@Observations = IIF(t.x.value('Observations[1]','varchar(300)') = '', null, t.x.value('Observations[1]','varchar(300)')),
			@Status = t.x.value('Status[1]','bit'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int')
		from @Xml.nodes('/StabilityTable') t(x)
		
		--Se obtienen los detalles del xml
		insert into @StabilityTableDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('StabilityTableId[1]','int') as StabilityTableId,
			t.x.value('ATCId[1]','int') as ATCId,
			t.x.value('ProductId[1]','int') as ProductId,
			t.x.value('HourStabilityProduct[1]','int') as HourStabilityProduct,
			t.x.value('UnitDoseTypeId[1]','int') as UnitDoseTypeId,
			t.x.value('AllowableDoses[1]','int') as AllowableDoses,
			IIF(t.x.value('Observations[1]','varchar(300)') = '', null, t.x.value('Observations[1]','varchar(300)')) as Observations,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/StabilityTable/StabilityTableDetail') t(x)
		
		--Se obtienen los detalles de diluciones
		insert into @StabilityTableDetailDilution
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('StabilityTableDetailId[1]','int') as StabilityTableDetailId,
			t.x.value('ATCId[1]','int') as ATCId,
			REPLACE(t.x.value('ConcentrationMaximum[1]','varchar(20)'), ',', '.') as ConcentrationMaximum,
			REPLACE(t.x.value('ConcentrationMinimum[1]','varchar(20)'), ',', '.') as ConcentrationMinimum,
			t.x.value('PhotoProtection[1]','bit') as PhotoProtection,
			t.x.value('InfusionTime[1]','int') as InfusionTime,
			IIF(t.x.value('BibliographicReference[1]','varchar(300)') = '', null, t.x.value('BibliographicReference[1]','varchar(300)')) as BibliographicReference,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			IIF(t.x.value('Container[1]','varchar(20)') = '', null, t.x.value('Container[1]','varchar(20)')) as Container,
			t.x.value('HourStability[1]','int') as HourStability,
			t.x.value('StorageTemperatureId[1]','int') as StorageTemperatureId
		from @Xml.nodes('/StabilityTable/StabilityTableDetail/StabilityTableDetailDilution') t(x)

		--Se obtienen los detalles de PharmaceuticalDispensingDetail del xml
		insert into @StabilityTableDetailReconstitution
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('StabilityTableDetailId[1]','int') as StabilityTableDetailId,
			t.x.value('ATCId[1]','int') as ATCId,
			REPLACE(t.x.value('Volume[1]','varchar(20)'), ',', '.') as Volume,
			t.x.value('HourStability[1]','int') as HourStability,
			IIF(t.x.value('Observations[1]','varchar(1000)') = '', null, t.x.value('Observations[1]','varchar(1000)')) as Observations,
			t.x.value('StorageTemperatureId[1]','int') as StorageTemperatureId,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/StabilityTable/StabilityTableDetail/StabilityTableDetailReconstitution') t(x)

		--Se eliminan los detalles
		delete from MixingStation.StabilityTableDetailReconstitution where Id in (select Id from @StabilityTableDetailReconstitution where Id > 0 and IsDelete = 1)
		delete from @StabilityTableDetailReconstitution where Id > 0 and IsDelete = 1

		delete from MixingStation.StabilityTableDetailDilution where Id in (select Id from @StabilityTableDetailDilution where Id > 0 and IsDelete = 1)
		delete from @StabilityTableDetailDilution where Id > 0 and IsDelete = 1

		delete from MixingStation.StabilityTableDetailReconstitution where StabilityTableDetailId in (select Id from @StabilityTableDetail where Id > 0 and IsDelete = 1)
		delete from MixingStation.StabilityTableDetailDilution where StabilityTableDetailId in (select Id from @StabilityTableDetail where Id > 0 and IsDelete = 1)

		delete from MixingStation.StabilityTableDetail where Id in (select Id from @StabilityTableDetail where Id > 0 and IsDelete = 1)
		delete from @StabilityTableDetail where Id > 0 and IsDelete = 1

		--Se valida si hay algun medicamento agregado al detalle que ya existe en otro registro de estabilidad
		if exists(select 1
		from @StabilityTableDetail sdTemp
		inner join MixingStation.StabilityTableDetail sd on sd.ATCId = sdTemp.ATCId
		where sd.StabilityTableId <> @Id)
		begin
			declare @Message varchar(max) = ''
			select @Message = STUFF((
			select distinct CHAR(13) + CHAR(10) + 'El medicamento ' + a.Code + ' - ' + a.Name + ' ya se encuentra en la tabla de estabilidad ' + s.Code
			from @StabilityTableDetail sdTemp
			inner join MixingStation.StabilityTableDetail sd on sd.ATCId = sdTemp.ATCId
			inner join MixingStation.StabilityTable s on s.Id = sd.StabilityTableId
			inner join Inventory.ATC a on a.Id = sdTemp.ATCId
			where sd.StabilityTableId <> @Id
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			select 999 as CodeMessage, @Message as Message, 0 Id, '' Code
			return
		end

		--Si no viene el código se genera
		if @Code = '' or @Code is null
		begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from MixingStation.MixingStationSequence
			where IdForm = '2192'

			if @scope = 'O' --Si el ambito es por organización
			begin
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from MixingStation.MixingStationSequenceDetail bsd 
				inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2192'
				order by bsd.Next desc
			end
			else begin --Si el ambito es por unidad operativa
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from MixingStation.MixingStationSequenceDetail bsd 
				inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2192' and bsd.IdOperatingUnit = @OperatingUnitId
				order by bsd.Next desc
			end
					
			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la tabla de estabilidad' as Message, 0 Id, '' Code
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update MixingStation.MixingStationSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end

		if @Id = 0 or @Id is null --Se guarda la cabecera
		begin
			INSERT INTO [MixingStation].[StabilityTable]([Code], [Name], [StabilityDate], [Observations], [Status], [CreationUser], [CreationDate])
			VALUES(@Code, @Name, @StabilityDate, @Observations, @Status, @UserCode, [Common].[GETDATE]())

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			UPDATE [MixingStation].[StabilityTable] SET [Code] = @Code, [Name] = @Name, [StabilityDate] = @StabilityDate, [Observations] = @Observations, 
			[Status] = @Status, [ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		end
		
		--Variables para recorrer el detalle 
		declare @Rows int = 1, @RowId int = 0, @DetailId int, @StabilityTableId int, @ATCId int, @ProductId int, @UnitDoseTypeId int, @AllowableDoses int, 
		@ObservationsDetail varchar(300), @HourStabilityProduct int

		while @Rows > 0
		begin
			select top 1 @RowId = RowId, @DetailId = Id,
			@StabilityTableId = StabilityTableId, @ATCId = ATCId, @ProductId = ProductId, @UnitDoseTypeId = UnitDoseTypeId, @AllowableDoses = AllowableDoses, 
			@ObservationsDetail = Observations, @HourStabilityProduct = HourStabilityProduct
			from @StabilityTableDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId = 0 --Se inserta el detalle
			begin
				INSERT INTO [MixingStation].[StabilityTableDetail]([StabilityTableId], [ATCId], [ProductId], [UnitDoseTypeId], [AllowableDoses], [Observations], [HourStabilityProduct])
				VALUES(@Id, @ATCId, @ProductId, @UnitDoseTypeId, @AllowableDoses, @ObservationsDetail, @HourStabilityProduct)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				UPDATE [MixingStation].[StabilityTableDetail] SET [ATCId] = @ATCId, [ProductId] = @ProductId, [UnitDoseTypeId] = @UnitDoseTypeId, 
				[AllowableDoses] = @AllowableDoses, [Observations] = @ObservationsDetail, [HourStabilityProduct] = @HourStabilityProduct
				WHERE Id = @DetailId
			end

			--Se insertan las diluciones
			INSERT INTO [MixingStation].[StabilityTableDetailDilution]([StabilityTableDetailId], [ATCId], [ConcentrationMaximum], [ConcentrationMinimum], 
			[PhotoProtection], [InfusionTime], [BibliographicReference], Container, HourStability,StorageTemperatureId)
			select @DetailId, ATCId, ConcentrationMaximum, ConcentrationMinimum, PhotoProtection, InfusionTime, 
			BibliographicReference, Container, HourStability, StorageTemperatureId
			from @StabilityTableDetailDilution
			where Id = 0 and RowId = @RowId

			--Se actualizan las diluciones
			UPDATE d SET d.ATCId = dt.ATCId, d.ConcentrationMaximum = dt.ConcentrationMaximum, d.ConcentrationMinimum = dt.ConcentrationMinimum, 
			d.PhotoProtection = dt.PhotoProtection, d.InfusionTime = dt.InfusionTime, d.BibliographicReference = dt.BibliographicReference, 
			d.Container = dt.Container, d.HourStability = dt.HourStability, d.StorageTemperatureId = dt.StorageTemperatureId
			from @StabilityTableDetailDilution dt
			inner join MixingStation.StabilityTableDetailDilution d on d.Id = dt.Id
			where dt.Id > 0

			--Se insertan las reconstituciones
			INSERT INTO [MixingStation].[StabilityTableDetailReconstitution]([StabilityTableDetailId], [ATCId], [Volume],
			[HourStability], [Observations],[StorageTemperatureId])
			select @DetailId, ATCId, Volume,HourStability, Observations, StorageTemperatureId
			from @StabilityTableDetailReconstitution r
			where Id = 0 and RowId = @RowId

			--Se actualizan las reconstituciones
			UPDATE r SET r.ATCId = rt.ATCId, r.Volume = rt.Volume,
			r.HourStability = rt.HourStability,  r.Observations = rt.Observations, r.StorageTemperatureId = rt.StorageTemperatureId
			from @StabilityTableDetailReconstitution rt
			inner join MixingStation.StabilityTableDetailReconstitution r on r.Id = rt.Id
			where rt.Id > 0
		end

		--Se retorna el ok
		select 0 as CodeMessage, 'Se guardó correctamente la tabla de estabilidad' as Message, @Id Id, @Code Code
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' Code
		return

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de la central de mezclas que guarda o actualiza una tabla de estabilidad de medicamentos, recibiendo la información completa en formato XML. Gestiona la cabecera de la tabla (código, nombre, fecha de vigencia, estado, unidad operativa) junto con sus tres niveles de detalle: productos con código ATC y horas de estabilidad, diluciones (concentraciones máxima y mínima, tiempo de infusión, fotoprotección, temperatura de almacenamiento, referencia bibliográfica) y reconstituciones (volumen, horas de estabilidad, temperatura de almacenamiento). Aplica inserciones, actualizaciones y eliminaciones lógicas en las tablas StabilityTable, StabilityTableDetail, StabilityTableDetailDilution y StabilityTableDetailReconstitution, validando además que un mismo medicamento (ATC) no quede registrado en más de una tabla de estabilidad activa para la misma unidad operativa.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveStabilityTable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveStabilityTable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta o actualiza) una tabla de estabilidad de medicamentos de la central de mezclas con sus detalles, diluciones y reconstituciones a partir de un XML, validando unicidad del medicamento entre tablas y autogenerando el código mediante secuencia.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe respetar la estructura /StabilityTable con sus nodos hijos StabilityTableDetail, y dentro de cada uno StabilityTableDetailDilution y StabilityTableDetailReconstitution.; StabilityDate en el XML debe venir en formato dd/mm/yyyy (estilo 103) para conversión correcta.; Debe existir una configuración en MixingStation.MixingStationSequence para IdForm=''2192'' cuando se requiera autogenerar el código.; Cuando el Scope no sea ''O'', debe existir un MixingStationSequenceDetail para el @OperatingUnitId recibido.; Los ATCId, ProductId, UnitDoseTypeId, StorageTemperatureId referenciados en el XML deben existir en sus catálogos.; Los registros marcados con IsDelete=1 deben traer Id > 0 (existir previamente) para ser eliminados.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un mismo ATC (medicamento) no puede pertenecer a más de una tabla de estabilidad: si ya existe en otra StabilityTableId distinta, se bloquea el guardado.; El código de la tabla de estabilidad solo se autogenera cuando no se proporciona; si se autogenera, siempre se incrementa [Next] de MixingStationSequenceDetail en 1.; El ámbito de la secuencia depende del Scope configurado en MixingStationSequence para IdForm=''2192'' (''O'' = organización, otro = por unidad operativa).; Al eliminar un StabilityTableDetail (IsDelete=1) se eliminan en cascada sus diluciones y reconstituciones asociadas (StabilityTableDetailId).; Cadenas de Observations, BibliographicReference y Container vacías se almacenan como NULL.; Valores decimales (ConcentrationMaximum, ConcentrationMinimum, Volume) se normalizan reemplazando coma por punto antes de persistir.; StabilityDate se interpreta con formato 103 (dd/mm/yyyy) al convertir desde el XML.; Toda la operación se ejecuta dentro de TRY/CATCH; ante cualquier error se retorna CodeMessage=999 con ERROR_MESSAGE().; El procedimiento siempre retorna un resultset con columnas (CodeMessage, Message, Id, Code): 0 = éxito, 999 = error o validación fallida.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en MixingStation.StabilityTableDetail un ATCId del XML asociado a otra StabilityTableId distinta de @Id → Retorna CodeMessage=999 con un mensaje concatenado por cada medicamento ya presente en otra tabla de estabilidad y aborta el guardado else Continúa con la generación de código y persistencia; si @Code viene vacío o NULL → Genera el código mediante dbo.GetSequence usando la secuencia configurada en MixingStationSequence para IdForm=''2192'' e incrementa [Next] en MixingStationSequenceDetail else Usa el @Code recibido sin tocar la secuencia; si Scope de MixingStationSequence (IdForm=''2192'') = ''O'' → Selecciona la secuencia a nivel de organización (sin filtrar por unidad operativa), ordenada por Next desc else Selecciona la secuencia filtrando por bsd.IdOperatingUnit = @OperatingUnitId (ámbito por unidad operativa); si @idSequenceDetail IS NULL tras buscar la secuencia → Retorna CodeMessage=999 con mensaje ''Secuencia no encontrada para generar la tabla de estabilidad'' y aborta else Procede a generar el código y persistir la cabecera; si @Id = 0 o @Id IS NULL → INSERT en MixingStation.StabilityTable con CreationUser=@UserCode y CreationDate=Common.GETDATE(); toma SCOPE_IDENTITY() como nuevo @Id else UPDATE de la cabecera por Id=@Id seteando ModificationUser=@UserCode y ModificationDate=Common.GETDATE(); si Por cada fila del detalle, @DetailId = 0 → INSERT en StabilityTableDetail vinculado a la cabecera @Id; usa SCOPE_IDENTITY() como nuevo @DetailId para asociar diluciones/reconstituciones nuevas else UPDATE del detalle existente por Id=@DetailId; si Fila de dilución/reconstitución con Id = 0 y RowId del detalle actual → Se inserta como nueva asociada al @DetailId del detalle else Si Id > 0, se actualiza la fila existente por su Id; si Detalle/Dilución/Reconstitución con Id > 0 y IsDelete = 1 → Se elimina físicamente de la tabla persistente correspondiente y de la tabla variable antes de validar/insertar else Se conserva para ser insertado o actualizado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.StabilityTableDetail; MixingStation.StabilityTable; Inventory.ATC; MixingStation.MixingStationSequence; MixingStation.MixingStationSequenceDetail; Common.Sequense', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveStabilityTable';
-- GO
