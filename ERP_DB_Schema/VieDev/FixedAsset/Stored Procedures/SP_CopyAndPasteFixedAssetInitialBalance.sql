-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 04/04/2018
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de saldos iniciales de activos fijos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_CopyAndPasteFixedAssetInitialBalance] 
	@XmlObject as Xml
AS
BEGIN
	
	--Variable para obtener las descripciones de cada entidad que se valida
	declare @EntityDescription varchar(100)

	--Variable para obtener los ids de cada entidad que se valida
	declare @EntityId int

	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), 
								  ItemCode varchar(20), ItemDescription varchar(100), ItemId int,
								  Serie varchar(50), Plate varchar(50),
								  ResponsibleCode varchar(20), ResponsibleDescription varchar(100), ResponsibleId int,
								  HistoricalValue varchar(20), FairValue varchar(20),
								  TrademarkCode varchar(20), TrademarkDescription varchar(100), TrademarkId int,
								  Model varchar(100),
								  SupplierCode varchar(20), SupplierDescription varchar(100), SupplierId int,
								  LocationCode varchar(20), LocationDescription varchar(100), LocationId int,
								  PolicyCode varchar(20), PolicyDescription varchar(100), PolicyId int,
								  HandlesWarranty varchar(10), WarrantyExpirationDate varchar(20), AdquisitionDate varchar(20), AdquisitionType varchar(10), Depreciate varchar(10),
								  ValidMinorAmount varchar(10), StatusAssetCode varchar(20), StatusAssetDescription varchar(100), StatusAssetId int,
								  BookCode varchar(20), BookDescription varchar(100), BookId int,
								  [LifeTime] varchar(10), UnitLifeTime varchar(10), DepreciationType varchar(10), DepreciatedDays varchar(10), DepreciatedValue varchar(20),
								  HistoricalValueInBook varchar(20), Amortize varchar(10))

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
			t.x.value('CountFields[1]','int') as CountFields,
			t.x.value('StatusField[1]','int') as StatusField,
			t.x.value('MessageField[1]','varchar(100)') as MessageField,
			t.x.value('ItemCode[1]','varchar(20)') as ItemCode,
			t.x.value('ItemDescription[1]','varchar(100)') as ItemDescription,
			t.x.value('ItemId[1]','int') as ItemId,
			t.x.value('Serie[1]','varchar(50)') as Serie,
			t.x.value('Plate[1]','varchar(50)') as Plate,
			t.x.value('ResponsibleCode[1]','varchar(20)') as ResponsibleCode,
			t.x.value('ResponsibleDescription[1]','varchar(100)') as ResponsibleDescription,
			t.x.value('ResponsibleId[1]','int') as ResponsibleId,
			t.x.value('HistoricalValue[1]','varchar(20)') as HistoricalValue,
			t.x.value('FairValue[1]','varchar(20)') as FairValue,
			t.x.value('TrademarkCode[1]','varchar(20)') as TrademarkCode,
			t.x.value('TrademarkDescription[1]','varchar(100)') as TrademarkDescription,
			t.x.value('TrademarkId[1]','int') as TrademarkId,
			t.x.value('Model[1]','varchar(100)') as Model,
			t.x.value('SupplierCode[1]','varchar(20)') as SupplierCode,
			t.x.value('SupplierDescription[1]','varchar(100)') as SupplierDescription,
			t.x.value('SupplierId[1]','int') as SupplierId,
			t.x.value('LocationCode[1]','varchar(20)') as LocationCode,
			t.x.value('LocationDescription[1]','varchar(100)') as LocationDescription,
			t.x.value('LocationId[1]','int') as LocationId,
			t.x.value('PolicyCode[1]','varchar(20)') as PolicyCode,
			t.x.value('PolicyDescription[1]','varchar(100)') as PolicyDescription,
			t.x.value('PolicyId[1]','int') as PolicyId,
			t.x.value('HandlesWarranty[1]','varchar(10)') as HandlesWarranty,
			t.x.value('WarrantyExpirationDate[1]','varchar(20)') as WarrantyExpirationDate,
			t.x.value('AdquisitionDate[1]','varchar(20)') as AdquisitionDate,
			t.x.value('AdquisitionType[1]','varchar(10)') as AdquisitionType,
			t.x.value('Depreciate[1]','varchar(10)') as Depreciate,
			t.x.value('ValidMinorAmount[1]','varchar(10)') as ValidMinorAmount,
			t.x.value('StatusAssetCode[1]','varchar(20)') as StatusAssetCode,
			t.x.value('StatusAssetDescription[1]','varchar(100)') as StatusAssetDescription,
			t.x.value('StatusAssetId[1]','int') as StatusAssetId,
			t.x.value('BookCode[1]','varchar(20)') as BookCode,
			t.x.value('BookDescription[1]','varchar(100)') as BookDescription,
			t.x.value('BookId[1]','int') as BookId,
			t.x.value('LifeTime[1]','varchar(10)') as LifeTime,
			t.x.value('UnitLifeTime[1]','varchar(10)') as UnitLifeTime,
			t.x.value('DepreciationType[1]','varchar(10)') as DepreciationType,
			t.x.value('DepreciatedDays[1]','varchar(10)') as DepreciatedDays,
			t.x.value('DepreciatedValue[1]','varchar(20)') as DepreciatedValue,
			t.x.value('HistoricalValueInBook[1]','varchar(20)') as HistoricalValueInBook,
			t.x.value('Depreciate[1]','varchar(10)') as Amortize
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		Declare @Position as int = 0

		--Se declara la variable para poder realizar las validaciones
		Declare @Count as int

		--Se declara un cursor y las variables que lleva el cursor
		declare @Id as int
		declare @CountFields int
		declare @ItemCode varchar(20)
		declare @Plate varchar(50)
		declare @ResponsibleCode  varchar(20)
		declare @HistoricalValue  varchar(20)
		declare @FairValue varchar(20)
		declare @TrademarkCode varchar(20)
		declare @SupplierCode varchar(20)
		declare @LocationCode varchar(20)
		declare @PolicyCode varchar(20)
		declare @HandlesWarranty varchar(10)
		declare @WarrantyExpirationDate varchar(20)
		declare @AdquisitionDate varchar(20)
		declare @AdquisitionType varchar(10)
		declare @Depreciate varchar(10)
		declare @ValidMinorAmount varchar(10)
		declare @StatusAssetCode varchar(20)
		declare @BookCode varchar(20)
		declare @LifeTime varchar(10)
		declare @UnitLifeTime varchar(10)
		declare @DepreciationType varchar(10)
		declare @DepreciatedDays varchar(10)
		declare @DepreciatedValue varchar(20)
		declare @HistoricalValueInBook varchar(20)
		declare @Amortize VARCHAR(20)
		declare @ClassificationItem VARCHAR(20)

		--Se declara el select del cursor
		declare InfoItem Cursor For Select Id, CountFields, ItemCode, Plate, ResponsibleCode, HistoricalValue, FairValue, TrademarkCode, SupplierCode,
									LocationCode, PolicyCode, HandlesWarranty, WarrantyExpirationDate, AdquisitionDate, AdquisitionType, Depreciate, ValidMinorAmount, StatusAssetCode, BookCode,
									LifeTime, UnitLifeTime, DepreciationType, DepreciatedDays, DepreciatedValue, HistoricalValueInBook, Amortize
									From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @Id, @CountFields, @ItemCode, @Plate, @ResponsibleCode, @HistoricalValue, @FairValue, @TrademarkCode, @SupplierCode,
								@LocationCode, @PolicyCode, @HandlesWarranty, @WarrantyExpirationDate, @AdquisitionDate, @AdquisitionType, @Depreciate, @ValidMinorAmount, @StatusAssetCode, @BookCode,
								@LifeTime, @UnitLifeTime, @DepreciationType, @DepreciatedDays,  @DepreciatedValue, @HistoricalValueInBook, @Amortize

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			

			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 25
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3), @Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End		

			--Se valida que el código del articulo no este vacio
			if ISNULL(@ItemCode, '0') = '0' or @ItemCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del articulo del registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end
			
			--Se valida que el código del articulo exista
			if (select count(*) from FixedAsset.FixedAssetItem where Code = @ItemCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del articulo ' + @ItemCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores del articulo
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Description] from FixedAsset.FixedAssetItem where Code = @ItemCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set ItemId = @EntityId, ItemDescription = @EntityDescription where Id = @Id
			
			--Se valida que la placa no este vacía
			if ISNULL(@Plate, '0') = '0' or @Plate = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La placa del registro ' + convert(varchar(3), @Position) + ' está vacía'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el código del responsable no este vacio
			if ISNULL(@ResponsibleCode, '0') = '0' or @ResponsibleCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del responsable registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el responsable exista
			if (select count(*) from FixedAsset.FixedAssetResponsible where Code = @ResponsibleCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del responsable ' + @ResponsibleCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores del responsable
			select @EntityId = r.Id, @EntityDescription = r.Code + ' - ' +  t.[Name]
			from FixedAsset.FixedAssetResponsible r
			inner join Common.ThirdParty t on t.Id = r.ThirdPartyId
			where r.Code = @ResponsibleCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set ResponsibleId = @EntityId, ResponsibleDescription = @EntityDescription where Id = @Id

			--Se valida si el valor historico es numerico
			if ISNUMERIC(@HistoricalValue) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor histórico ' + @HistoricalValue + ' del registro ' + convert(varchar(3), @Position) + ' no es numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			IF @HistoricalValue = 0
			BEGIN
				UPDATE @TableXmlObject SET StatusField = 0, MessageField = 'El valor histórico del registro ' + CAST(@Position AS VARCHAR(3)) + ' tiene que tener un valor'
				WHERE Id = @Id
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			--Se valida si el valor razonable es numerico
			if ISNUMERIC(@FairValue) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor razonable ' + @FairValue + ' del registro ' + convert(varchar(3), @Position) + ' no es numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el código de la marca no este vacio
			if ISNULL(@TrademarkCode, '0') = '0' or @TrademarkCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la marca registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que la marca exista
			if (select count(*) from FixedAsset.FixedAssetTrademark where Code = @TrademarkCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la marca ' + @TrademarkCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores de la marca
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Name] from FixedAsset.FixedAssetTrademark where Code = @TrademarkCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set TrademarkId = @EntityId, TrademarkDescription = @EntityDescription where Id = @Id

			--Se valida que el código del proveedor no este vacio
			if ISNULL(@SupplierCode, '0') = '0' or @SupplierCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del proveedor registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el proveedor exista
			if (select count(*) from Common.Supplier where Code = @SupplierCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del proveedor ' + @SupplierCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores del proveedor
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Name] from Common.Supplier where Code = @SupplierCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set SupplierId = @EntityId, SupplierDescription = @EntityDescription where Id = @Id

			--Se valida que el código de la localizacion no este vacio
			if ISNULL(@LocationCode, '0') = '0' or @LocationCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la localización registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que la localizacion exista
			if (select count(*) from FixedAsset.FixedAssetLocation where Code = @LocationCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la localización ' + @LocationCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores de la localizacion
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Name] from FixedAsset.FixedAssetLocation where Code = @LocationCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set LocationId = @EntityId, LocationDescription = @EntityDescription where Id = @Id

			--Se valida que el código de la poliza no este vacio
			if ISNULL(@PolicyCode, '0') = '0' or @PolicyCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la poliza registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que la poliza exista
			if (select count(*) from FixedAsset.FixedAssetPolicy where Code = @PolicyCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la poliza ' + @PolicyCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores de la poliza
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Name] from FixedAsset.FixedAssetPolicy where Code = @PolicyCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set PolicyId = @EntityId, PolicyDescription = @EntityDescription where Id = @Id

			--Se valida que si maneja garantia es numerico
			if ISNUMERIC(@HandlesWarranty) = 0 or @HandlesWarranty > '1'
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El campo si maneja garantía del registro ' + convert(varchar(3), @Position) + ' tiene que ser 0 ó 1'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida si maneja garantía 
			if @HandlesWarranty = 1
			begin
				--Se valida que la fecha de expiracion sea una fecha
				if ISDATE(@WarrantyExpirationDate) = 0
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La fecha vencimiento garantía del registro ' + convert(varchar(3), @Position) + ' no tiene formato válido de fecha'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				end
			end
			
			--Se valida que la fecha de adquisición no este vacia
			if ISNULL(@AdquisitionDate, '0') = '0' or @AdquisitionDate = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La fecha adquisición del registro ' + convert(varchar(3), @Position) + ' está vacía'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end
			
			--Se valida que la fecha de adquisición sea una fecha
			if ISDATE(@AdquisitionDate) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La fecha adquisición del registro ' + convert(varchar(3), @Position) + ' no tiene formato válido de fecha'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end
			
			--Se valida que el tipo de adquisición este dentro del rango permitido
			if ISNUMERIC(@AdquisitionType) <> 1 or @AdquisitionType NOT IN ('1', '3', '4', '5', '6', '7', '8', '9', '10')
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El tipo de adquisición del registro ' + convert(varchar(3), @Position) + ' no está dentro del rango permitido(1, 3, 4, 5, 6, 7, 8, 9, 10)'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que deprecia es numerico
			if ISNUMERIC(@Depreciate) = 0 or @Depreciate > '1'
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El campo si deprecia/amortiza del registro ' + convert(varchar(3), @Position) + ' tiene que ser 0 ó 1'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida si amortiza cuando el articulo tiene la clasificacion es intagible (2)
			SELECT
				@Amortize = fai.Amortizes,
				@ClassificationItem = faic.Classification
			FROM
				FixedAsset.FixedAssetItem AS fai
				INNER JOIN FixedAsset.FixedAssetItemCatalog AS faic ON faic.Id = fai.ItemCatalogId
			WHERE
				fai.Code = @ItemCode

			-- Solo los articulos intangibles se marcan como amortizan no debe alterar los otros datos
			IF @Amortize = 1 and @ClassificationItem  = 2
			BEGIN 
				UPDATE
					@TableXmlObject
				SET
					Amortize = @Amortize,
					Depreciate = 0,
					ValidMinorAmount = NULL
				WHERE
					Id = @Id
				SET @Depreciate = 0
				SET @ValidMinorAmount = NULL
			END

			--Se valida si deprecia
			if @Depreciate = 1
			begin
				--Se valida que valida menor cuantía es numerico
				if ISNUMERIC(@ValidMinorAmount) = 0 or @ValidMinorAmount > '1'
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El campo si valida menor cuantía del registro ' + convert(varchar(3), @Position) + ' tiene que ser 0 ó 1'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				end
			end
			
			--Se valida que el código del estado no este vacio
			if ISNULL(@StatusAssetCode, '0') = '0' or @StatusAssetCode = ''
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del estado del registro ' + convert(varchar(3), @Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el estado exista
			if (select count(*) from FixedAsset.FixedAssetStatusAsset where Code = @StatusAssetCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del estado de activo ' + @StatusAssetCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores del estado
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Name] from FixedAsset.FixedAssetStatusAsset where Code = @StatusAssetCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set StatusAssetId = @EntityId, StatusAssetDescription = @EntityDescription where Id = @Id

			--Se valida que el libro exista
			if (select count(*) from GeneralLedger.LegalBook where Code = @BookCode) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del libro ' + @BookCode + ' del registro ' + convert(varchar(3), @Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se obtienen los valores del estado
			select @EntityId = Id, @EntityDescription = Code + ' - ' + [Name] from GeneralLedger.LegalBook where Code = @BookCode

			--Se actualiza la tabla principal con los datos obtenidos
			update @TableXmlObject set BookId = @EntityId, BookDescription = @EntityDescription where Id = @Id

			--Se valida que la vida util sea numérico
			if ISNUMERIC(@LifeTime) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La vida útil del registro ' + convert(varchar(3), @Position) + ' no es numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end
			
			--Se valida que la unidad de vida util este dentro del rango
			if ISNUMERIC(@UnitLifeTime) = 0 or @UnitLifeTime = 0 or @UnitLifeTime > 3
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La unidad vida útil del registro ' + convert(varchar(3), @Position) + ' no está dentro del rango permitido(1, 2, 3)'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el tipo de depreciacion esta dentro del rango
			if ISNUMERIC(@DepreciationType) = 0 or @DepreciationType = 0 or @DepreciationType > 4
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El tipo depreciación del registro ' + convert(varchar(3), @Position) + ' no está dentro del rango permitido(1, 2, 3, 4)'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que los dias depreciados sea numerico
			if ISNUMERIC(@DepreciatedDays) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'Los días depreciados del registro ' + convert(varchar(3), @Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que el valor depreciado sea un valor numerico
			if ISNUMERIC(@DepreciatedValue) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor depreciado del registro ' + convert(varchar(3), @Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida si el valor historico dentro del libro es numerico
			if ISNUMERIC(@HistoricalValueInBook) = 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor histórico ' + @HistoricalValueInBook + ' dentro del libro del registro ' + convert(varchar(3), @Position) + ' no es numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Se valida que la vida del activo sea mayor o igual a sus dias depreciados
			if (CASE CAST(@UnitLifeTime AS INT)
					WHEN 1 THEN CAST(@LifeTime AS INT) * 360 - CAST(@DepreciatedDays AS INT)
					WHEN 2 THEN CAST(@LifeTime AS INT) * 30 - CAST(@DepreciatedDays AS INT)
					WHEN 3 THEN CAST(@LifeTime AS INT) - CAST(@DepreciatedDays AS INT)
					ELSE 1
				END) < 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'Los días depreciados del registro ' + convert(varchar(3), @Position) + ' superan el tiempo de vida del activo'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			end

			--Si todo esta ok se actualiza la tabla para el ok
			update @TableXmlObject 
				set StatusField = 1, 
					MessageField = 'Registro validado correctamente'
			where Id = @Id

			NextFetch:
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @Id, @CountFields, @ItemCode, @Plate, @ResponsibleCode, @HistoricalValue, @FairValue, @TrademarkCode, @SupplierCode,
								@LocationCode, @PolicyCode, @HandlesWarranty, @WarrantyExpirationDate, @AdquisitionDate, @AdquisitionType, @Depreciate, @ValidMinorAmount, @StatusAssetCode, @BookCode,
								@LifeTime, @UnitLifeTime, @DepreciationType, @DepreciatedDays,  @DepreciatedValue, @HistoricalValueInBook, @Amortize
			continue

		End

		Close InfoItem
		Deallocate InfoItem

		
		--Se retorna la tabla
		select * from @TableXmlObject
		
	end try
	begin catch

		delete from @TableXmlObject
		insert into @TableXmlObject(StatusField, MessageField) values(0, ERROR_MESSAGE())
		select * from @TableXmlObject

	end catch

END