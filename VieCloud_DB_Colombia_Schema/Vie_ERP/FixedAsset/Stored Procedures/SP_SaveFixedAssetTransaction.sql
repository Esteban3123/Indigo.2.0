-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 11/05/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar las Transacciones
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_SaveFixedAssetTransaction] 
    @FixedAssetTransactionXml as Xml,
	@ListDeleteFixedAssetTransactionDetailBookXml as Xml,
	@ListDeleteFixedAssetTransactionDetailXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetTransactionDetailBook
	declare @ListDeleteFixedAssetTransactionDetailBookTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetTransactionDetail
	declare @ListDeleteFixedAssetTransactionDetailTemp table(Id int)
	--Se declaran las variables para obtener la cabecera
	declare @Id int, @OperatingUnitId int, @Code varchar(20), @DocumentDate date, @GenerateAccountPayable bit, @ThirdPartyId int, @SupplierId int, 
	@SupplierDistributionLineId int, @SupplierTypeId int, @DayPeriod int, @CreditMainAccountId int, @CostCenterId int, @InvoiceNumber varchar(100),
	@InvoiceDate date, @Observation varchar(1000), @Value numeric(20,4), @ValueDiscount numeric(20,4), @ValueTax numeric(20,4), @WithholdingTax numeric(20,4),
	@WithholdingICA numeric(20,4), @RetentionSource numeric(20,4), @RetentionOther numeric(20,4), @DeductionOther numeric(20,4), @TotalValue numeric(20,4), @Status tinyint,@CurrencyId INT , @TaxRegistration tinyint
	
	--Tabla temporal de FixedAssetTransactionDetail
	declare @FixedAssetTransactionDetail table(Id int, FixedAssetTransactionId int, TransactionClass tinyint, PhysicalAssetId int, PhysicalAssetPartsId int,
	TransactionType tinyint, ValorizationType tinyint, AffectDepreciation bit, Value numeric(20,4), [LifeTime] int, UnitLifeTime tinyint, IvaPercentage numeric(5,2), IvaValue numeric(20,4), IVAId INT,
	DiscountPercentage numeric(5,2), DiscountValue numeric(20,4), TotalValue numeric(20,4), RTFPercentage numeric(5,2), RTFValue numeric(20,4), AssetMainAccountId int,
	Detail varchar(1000) , TempId int)
	
	--Tabla temporal de FixedAssetTransactionDetailBook
	declare @FixedAssetTransactionDetailBook table(Id int, FixedAssetTransactionDetailId int, LegalBookId int, Value numeric(20,4), [LifeTime] int, 
	UnitLifeTime tinyint, ParentId int, TempId int)

	Begin try

		--Se obtiene FixedAssetTransaction
		select 
		@Id = t.x.value('Id[1]','int'),
		@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@DocumentDate = t.x.value('DocumentDate[1]','date'),
		@GenerateAccountPayable = t.x.value('GenerateAccountPayable[1]','bit'),
		@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
		@SupplierId =  case when t.x.value('SupplierId[1]','int') = 0 then null else t.x.value('SupplierId[1]','int') end,
		@SupplierDistributionLineId = case when t.x.value('SupplierDistributionLineId[1]','int') = 0 then null else t.x.value('SupplierDistributionLineId[1]','int') end,
		@SupplierTypeId = case when t.x.value('SupplierTypeId[1]','int') = 0 then null else t.x.value('SupplierTypeId[1]','int') end,
		@DayPeriod = t.x.value('DayPeriod[1]','int'),
		@CreditMainAccountId = t.x.value('CreditMainAccountId[1]','int'),
		@CostCenterId = case t.x.value('CostCenterId[1]','int') when 0 then null else t.x.value('CostCenterId[1]','int') end,
		@InvoiceNumber = case t.x.value('InvoiceNumber[1]','varchar(100)') when '0' then null else t.x.value('InvoiceNumber[1]','varchar(100)') end,
		@InvoiceDate =  t.x.value('InvoiceDate[1]','date'),		
		@Observation = dbo.DecodeXmlToText(t.x.value('Observation[1]','varchar(max)')),
		@Value = t.x.value('Value[1]','numeric(20,4)'),
		@ValueDiscount = t.x.value('ValueDiscount[1]','numeric(20,4)'),
		@ValueTax = t.x.value('ValueTax[1]','numeric(20,4)'),
		@WithholdingTax = t.x.value('WithholdingTax[1]','numeric(20,4)'),
		@WithholdingICA = t.x.value('WithholdingICA[1]','numeric(20,4)'),
		@RetentionSource = t.x.value('RetentionSource[1]','numeric(20,4)'),
		@RetentionOther = t.x.value('RetentionOther[1]','numeric(20,4)'),
		@DeductionOther = t.x.value('DeductionOther[1]','numeric(20,4)'),
		@TotalValue = t.x.value('TotalValue[1]','numeric(20,4)'),
		@Status = t.x.value('Status[1]','tinyint'),
		@CurrencyId = t.x.value('CurrencyId[1]','INT'),
		@TaxRegistration = t.x.value('TaxRegistration[1]', 'tinyint')
		from @FixedAssetTransactionXml.nodes('/FixedAssetTransaction') t(x)
		
		IF @InvoiceDate = '9999-12-31' BEGIN
			SET @InvoiceDate = null
		END
		
		if @Status <> 3 --Si no se esta anulando
		Begin

		
			--Se obtiene FixedAssetTransactionDetail
			insert into @FixedAssetTransactionDetail
				(Id, FixedAssetTransactionId, TransactionClass, PhysicalAssetId, PhysicalAssetPartsId, TransactionType, ValorizationType, AffectDepreciation, Value, LifeTime, 
				UnitLifeTime, IVAId, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue, TotalValue, RTFPercentage, RTFValue, AssetMainAccountId, Detail, TempId)
			select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('FixedAssetTransactionId[1]','int') as FixedAssetTransactionId,
			t.x.value('TransactionClass[1]','tinyint') as TransactionClass,
			case when t.x.value('PhysicalAssetId[1]','int') = 0 then null else t.x.value('PhysicalAssetId[1]','int') end as PhysicalAssetId,
			case when t.x.value('PhysicalAssetPartsId[1]','int') = 0 then null else t.x.value('PhysicalAssetPartsId[1]','int') end as PhysicalAssetPartsId,
			t.x.value('TransactionType[1]','tinyint') as TransactionType,
			t.x.value('ValorizationType[1]','tinyint') as ValorizationType,
			t.x.value('AffectDepreciation[1]','bit') as AffectDepreciation,
			t.x.value('Value[1]','numeric(20,4)') as Value,
			t.x.value('LifeTime[1]','int') as [LifeTime],
			t.x.value('UnitLifeTime[1]','tinyint') as UnitLifeTime,
			case when t.x.value('IVAId[1]','int') = 0 then null else t.x.value('IVAId[1]','int') end as IVAId,
			t.x.value('IvaPercentage[1]','numeric(5, 2)') as IvaPercentage,
			t.x.value('IvaValue[1]','numeric(20, 4)') as IvaValue,
			t.x.value('DiscountPercentage[1]','numeric(5, 2)') as DiscountPercentage,
			t.x.value('DiscountValue[1]','numeric(20, 4)') as DiscountValue,
			t.x.value('TotalValue[1]','numeric(20, 4)') as TotalValue,
			t.x.value('RTFPercentage[1]','numeric(5, 2)') as RTFPercentage,
			t.x.value('RTFValue[1]','numeric(20, 4)') as RTFValue,
			t.x.value('AssetMainAccountId[1]','int') as AssetMainAccountId,
			dbo.DecodeXmlToText(t.x.value('Detail[1]','varchar(max)')) as Detail,
			t.x.value('TempId[1]','int') as TempId
			from @FixedAssetTransactionXml.nodes('/FixedAssetTransaction/FixedAssetTransactionDetail') t(x)

			--Se obtiene FixedAssetEntryItemDetail
			insert into @FixedAssetTransactionDetailBook
			select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('FixedAssetTransactionDetailId[1]','int') as FixedAssetTransactionDetailId,
			t.x.value('LegalBookId[1]','int') as LegalBookId,
			t.x.value('Value[1]','numeric(20,4)') as Value,
			t.x.value('LifeTime[1]','int') as [LifeTime],
			t.x.value('UnitLifeTime[1]','tinyint') as UnitLifeTime,
			t.x.value('ParentId[1]','int') as ParentId,
			t.x.value('TempId[1]','int') as TempId
			from @FixedAssetTransactionXml.nodes('/FixedAssetTransaction/FixedAssetTransactionDetail/FixedAssetTransactionDetailBook') t(x)
		
			--Se obtienen los ids de los xml de los listados de eliminación

			--Se obtiene los detalles del xml(FixedAssetTransactionDetailBook)
			insert into @ListDeleteFixedAssetTransactionDetailBookTemp
			select 
			t.x.value('Id[1]','int') as Id
			from @ListDeleteFixedAssetTransactionDetailBookXml.nodes('/ListDeleteFixedAssetTransactionDetailBook') t(x)

			--Se obtiene los detalles del xml(FixedAssetEntryItemDetailPart)
			insert into @ListDeleteFixedAssetTransactionDetailTemp
			select 
			t.x.value('Id[1]','int') as Id
			from @ListDeleteFixedAssetTransactionDetailXml.nodes('/ListDeleteFixedAssetTransactionDetail') t(x)

			
				
			--Se eliminan los registros en cascada

			--Eliminación [FixedAssetTransactionDetailBook]
			delete [FixedAsset].[FixedAssetTransactionDetailBook] where Id in (select Id from @ListDeleteFixedAssetTransactionDetailBookTemp where Id > 0)

			--Eliminación [FixedAssetTransactionDetail]
			delete [FixedAsset].[FixedAssetTransactionDetail] where Id in (select Id from @ListDeleteFixedAssetTransactionDetailTemp where Id > 0)

		
		End

		

		declare @ConfirmUser as varchar(20) = case when @Status <> 2 then null else @CodeUser end
		declare @ConfirmDate as datetime = case when @Status <> 2 then null else [Common].[GETDATE]() end

		if @Id = 0 and @Status <> 3 --Si se esta insertando por primera vez se consulta la secuencia numerica y no se esta eliminando
		Begin
		   --- Obtenemos la secuencia numerica
		   if @Code = '' begin
				IF
						 (
							 SELECT COUNT(*)
							 FROM FixedAsset.FixedAssetSequence
							 WHERE IdForm = 1121
						 ) = 0
							 BEGIN
								 SELECT '999' AS CodeMessage,
										'No existe secuencia numerica para el formulario de transacciones' AS Message,
										0 AS Id,
										CAST(3 AS TINYINT) AS [Status];
								 RETURN;
							 END;
						 DECLARE @idSequenceDetail INT;
						 DECLARE @pattern VARCHAR(300);
						 DECLARE @NextS BIGINT;
						 DECLARE @Scope VARCHAR(5);
						 DECLARE @IdSequence INT;
						 SELECT @IdSequence = Id,
								@Scope = Scope
						 FROM FixedAsset.FixedAssetSequence
						 WHERE IdForm = 1121;

						IF @Scope = 'O'				 
							 BEGIN --- Secuencia por Prefijo
						print 'O'

					
								 SELECT @pattern = cs.Pattern,
										@idSequenceDetail = psd.Id,
										@NextS = psd.[Next]
								 FROM FixedAsset.FixedAssetSequenceDetail psd
									  INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
								 WHERE psd.IdSequenseFixedAssetC = @IdSequence;		

						 END;
						 ELSE
							 BEGIN -- Secuencia por Unidad operativa
						print 'OU'
					

								 SELECT @pattern = cs.Pattern,
										@idSequenceDetail = psd.Id,
										@NextS = psd.[Next]
								 FROM FixedAsset.FixedAssetSequenceDetail psd
									  INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
								 WHERE psd.IdSequenseFixedAssetC = @IdSequence
									   AND IdOperatingUnit = @OperatingUnitId;

						END;

				if (@idSequenceDetail is null)
				Begin
					select 999 as CodeMessage, 'Secuencia no encontrada en transacciones' as Message, '' as Code, 0 as Id
					return
				End

				select @Code = dbo.GetSequence('',@pattern,@NextS)  

				 IF @Code = '__ERROR_MAXVALUE__'
							 BEGIN
								 SELECT 999 AS CodeMessage,
										'La secuencia alcanzo su valor maximo' AS Message,
										0 AS Id,
										CAST(3 AS TINYINT) AS [Status];
								 RETURN;
							 END;

				update FixedAsset.FixedAssetSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		   end
                     
					 
			IF (Select COUNT(Id) FROM [FixedAsset].[FixedAssetTransaction] WHERE Code= @Code) > 0
				begin			
					SELECT 999 AS CodeMessage, 'Ya existen registros con esta secuencia' AS Message, '' as Code, 0 as Id; --Ya existen registros con esta secuencia
				RETURN;
			END

			--Se inserta la cabecera
			INSERT INTO [FixedAsset].[FixedAssetTransaction]
           (OperatingUnitId,[Code], DocumentDate, GenerateAccountPayable, ThirdPartyId, SupplierId, SupplierDistributionLineId, SupplierTypeId, DayPeriod
		   ,CreditMainAccountId, CostCenterId, InvoiceNumber, InvoiceDate, Observation, Value, ValueDiscount, ValueTax, WithholdingTax, WithholdingICA
		   ,RetentionSource, RetentionOther, DeductionOther, TotalValue, [Status], CreationUser, CreationDate, CurrencyId,TaxRegistration)
			VALUES
           (@OperatingUnitId, @Code, @DocumentDate, @GenerateAccountPayable, @ThirdPartyId, @SupplierId, @SupplierDistributionLineId, @SupplierTypeId, @DayPeriod,
		   @CreditMainAccountId, @CostCenterId, @InvoiceNumber, @InvoiceDate, @Observation, @Value, @ValueDiscount, @ValueTax, @WithholdingTax, @WithholdingICA,
		   @RetentionSource, @RetentionOther, @DeductionOther, @TotalValue, @Status, @CodeUser, [Common].[GETDATE](), @CurrencyId,@TaxRegistration)
		   
		   --Obtengo el id de la cabcera
			set @Id = SCOPE_IDENTITY()
		End
		Else --Si se esta actualizando
		Begin

			declare @AnnulmentUser as varchar(20) = case when @Status <> 3 then null else @CodeUser end
			declare @AnnulmentDate as datetime = case when @Status <> 3 then null else [Common].[GETDATE]() end

			--Se actualiza la cabecera
			UPDATE [FixedAsset].[FixedAssetTransaction] set
           OperatingUnitId = @OperatingUnitId, [Code] = @Code, DocumentDate = @DocumentDate, GenerateAccountPayable = @GenerateAccountPayable, ThirdPartyId = @ThirdPartyId, 
		   [SupplierId] = @SupplierId,[SupplierDistributionLineId] = @SupplierDistributionLineId, [SupplierTypeId] = @SupplierTypeId, DayPeriod = @DayPeriod,
		   CreditMainAccountId = @CreditMainAccountId, CostCenterId = @CostCenterId, [InvoiceNumber] = @InvoiceNumber, [InvoiceDate] = @InvoiceDate, Observation = @Observation,
		   [Value] = @Value , [ValueDiscount] = @ValueDiscount, [ValueTax] = @ValueTax, [WithholdingTax] = @WithholdingTax, [WithholdingICA] = @WithholdingICA, 
		   [RetentionSource] = @RetentionSource, [RetentionOther] = @RetentionOther, [DeductionOther] = @DeductionOther, [TotalValue] = @TotalValue, [Status] = @Status,
		   [ModificationUser] = @CodeUser, [ModificationDate] = [Common].[GETDATE](), AnnulmentUser = @AnnulmentUser, AnnulmentDate = @AnnulmentDate, [ConfirmationUser] = @ConfirmUser, 
		   [ConfirmationDate] = @ConfirmDate, [CurrencyId] = @CurrencyId, [TaxRegistration] = @TaxRegistration
		   where Id = @Id
		End
		
		if @Status <> 3
		Begin		
			--Se inserta FixedAssetTransactionDetail
			Declare @EntityId int
			Declare @TempId int
			Declare InfoItem Cursor For Select Id, TempId From @FixedAssetTransactionDetail

			Open InfoItem
			Fetch Next From InfoItem Into @EntityId, @TempId
			While @@fetch_status = 0
			Begin
				print '@Id --> ' + convert(varchar(5),@Id)
				if @EntityId = 0 --Si se esta insertando el registro
				Begin
					--Se inserta el registro
					insert into [FixedAsset].FixedAssetTransactionDetail
				   (FixedAssetTransactionId, TransactionClass, PhysicalAssetId, PhysicalAssetPartsId, TransactionType, ValorizationType, AffectDepreciation, value, [LifeTime],
				   UnitLifeTime, IVAId, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue, TotalValue, RTFPercentage, RTFValue, AssetMainAccountId, Detail)
				   select @Id, TransactionClass, PhysicalAssetId, PhysicalAssetPartsId, TransactionType, ValorizationType, AffectDepreciation, Value, [LifeTime],
				   UnitLifeTime, IVAId, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue, TotalValue, RTFPercentage, RTFValue, AssetMainAccountId, Detail
				   from  @FixedAssetTransactionDetail  where Id = 0 and TempId = @TempId
				   --Se actualiza el id en la tabla temporal @FixedAssetEntryItem
				   update @FixedAssetTransactionDetail set Id = SCOPE_IDENTITY() where TempId = @TempId
				   --Se actualiza el id de la relacion en la tabla temporal @FixedAssetEntryItemDetail
				   update @FixedAssetTransactionDetailBook set FixedAssetTransactionDetailId = SCOPE_IDENTITY() where ParentId = @TempId and Id = 0
				End
				Else --Si se esta actualizando
				Begin
					--Se actualiza el registro
					update faei set faei.FixedAssetTransactionId = faeiTemp.FixedAssetTransactionId, faei.TransactionClass = faeiTemp.TransactionClass, faei.PhysicalAssetId = faeiTemp.PhysicalAssetId,
					faei.PhysicalAssetPartsId = faeiTemp.PhysicalAssetPartsId, faei.TransactionType = faeiTemp.TransactionType, faei.ValorizationType = faeiTemp.ValorizationType, faei.AffectDepreciation = faeiTemp.AffectDepreciation,
					faei.Value = faeiTemp.Value, faei.[LifeTime] = faeiTemp.[LifeTime], faei.UnitLifeTime = faeiTemp.UnitLifeTime, faei.IVAId = faeiTemp.IVAId, faei.IvaPercentage = faeiTemp.IvaPercentage,
					faei.IvaValue = faeiTemp.IvaValue, faei.DiscountPercentage = faeiTemp.DiscountPercentage, faei.DiscountValue = faeiTemp.DiscountValue, 
					faei.TotalValue = faeiTemp.TotalValue, faei.RTFPercentage = faeiTemp.RTFPercentage, faei.RTFValue = faeiTemp.RTFValue, 
					faei.AssetMainAccountId = faeiTemp.AssetMainAccountId, faei.Detail = faeiTemp.Detail
					from [FixedAsset].FixedAssetTransactionDetail faei
					inner join @FixedAssetTransactionDetail faeiTemp on faeiTemp.Id = faei.Id
					where faei.Id = @EntityId
				End
			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @EntityId, @TempId
				continue

			End
			Close InfoItem
			Deallocate InfoItem

			--Se inserta FixedAssetTransactionDetailBook
			Declare @EntityDetailId int
			Declare @TempDetailId int
			Declare InfoItem Cursor For Select Id, TempId From @FixedAssetTransactionDetailBook

			Open InfoItem
			Fetch Next From InfoItem Into @EntityDetailId, @TempDetailId
			While @@fetch_status = 0
			Begin
			
				if @EntityId = 0 --Si se esta insertando el registro
				Begin
					--Se inserta el registro
					insert into [FixedAsset].FixedAssetTransactionDetailBook
				   (FixedAssetTransactionDetailId, LegalBookId, Value, [LifeTime], UnitLifeTime)
				   select 
				   FixedAssetTransactionDetailId, 
				   LegalBookId, 
				   ISNULL([Common].[CurrencyConverterByModule] (Temp.Value, @CurrencyId, lb.OfficialCurrencyId,NULL, NULL, @DocumentDate),Temp.Value) Value,
				   [LifeTime], 
				   UnitLifeTime
				   from  @FixedAssetTransactionDetailBook Temp  
				   JOIN GeneralLedger.LegalBook lb ON lb.Id = temp.LegalBookId
				   WHERE temp.Id = 0 and temp.TempId = @TempDetailId

				   --Se actualiza el id en la tabla temporal @FixedAssetEntryItem
				   update @FixedAssetTransactionDetailBook set Id = SCOPE_IDENTITY() where TempId = @TempDetailId
				End
				Else --Si se esta actualizando
				Begin
					--Se actualiza el registro
					update faei set faei.FixedAssetTransactionDetailId = faeiTemp.FixedAssetTransactionDetailId, faei.LegalBookId = faeiTemp.LegalBookId, faei.Value = faeiTemp.Value,
					faei.[LifeTime] = faeiTemp.[LifeTime], faei.UnitLifeTime = faeiTemp.UnitLifeTime
					from [FixedAsset].FixedAssetTransactionDetailBook faei
					inner join @FixedAssetTransactionDetailBook faeiTemp on faeiTemp.Id = faei.Id
					where faei.Id = @EntityDetailId
				End
			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @EntityDetailId, @TempDetailId
				continue

			End
			Close InfoItem
			Deallocate InfoItem

		End
			
		if @Status = 2 BEGIN
			
			DECLARE @PhysicalAssetId as Int = 0
			DECLARE @PhysicalAssetPartsId as Int = 0
			DECLARE @TransactionDetailId as Int = 0
			DECLARE @TransactionClass int = 0
			DECLARE @TransactionType int = 0
			DECLARE @DetailTotalValue as numeric(20,4)

			declare C_Transaction cursor for	
			
			SELECT Id, TransactionClass, TransactionType, PhysicalAssetId, PhysicalAssetPartsId, TotalValue FROM FixedAsset.FixedAssetTransactionDetail where FixedAssetTransactionId = @Id

			
			open C_Transaction
				fetch next from C_Transaction into @TransactionDetailId, @TransactionClass, @TransactionType, @PhysicalAssetId, @PhysicalAssetPartsId, @DetailTotalValue 
				while @@FETCH_STATUS = 0 begin

				-- DESVALORIZACIÓN
				IF @TransactionType = 2 BEGIN

					DECLARE @OriginalDevaluation as numeric(20,4) = 0
					DECLARE @OriginalAdjustedValue as numeric(20,4) = 0

					-- Si es por ACTIVO
					if @TransactionClass = 1 and  @PhysicalAssetId IS NOT NULL BEGIN
						
						SELECT @OriginalDevaluation = Devaluation, @OriginalAdjustedValue = AdjustedValue
						from FixedAsset.FixedAssetPhysicalAssetDetailBook where PhysicalAssetId = @PhysicalAssetId
						
						UPDATE FixedAsset.FixedAssetPhysicalAssetDetailBook 
							SET Devaluation = (@OriginalDevaluation + @DetailTotalValue), 
							 AdjustedValue = (@OriginalAdjustedValue - @DetailTotalValue)
						WHERE PhysicalAssetId = @PhysicalAssetId
					END
					
					-- Si es por PARTES
					iF @TransactionClass = 2 and @PhysicalAssetPartsId IS NOT NULL BEGIN

						SELECT @OriginalDevaluation = Devaluation, @OriginalAdjustedValue = AdjustedValue
						from FixedAsset.FixedAssetPhysicalAssetPartsDetailBook where PhysicalAssetPartsId = @PhysicalAssetPartsId
						
						UPDATE FixedAsset.FixedAssetPhysicalAssetPartsDetailBook 
							SET Devaluation = (@OriginalDevaluation + @DetailTotalValue), 
							 AdjustedValue = (@OriginalAdjustedValue - @DetailTotalValue)
						WHERE PhysicalAssetPartsId = @PhysicalAssetPartsId
					END

				END

				-- VALORIZACION
				IF @TransactionType = 1 BEGIN
					DECLARE @AffectDepreciation bit = 0
					DECLARE @OriginalValorization as numeric(20,4) = 0
					DECLARE @OriginalTransactionValue as numeric(20,4) = 0
					DECLARE @OriginalResidualValue as numeric(20,4) = 0
					DECLARE @OriginalAdjustedValueValorization as numeric(20,4) = 0
					DECLARE @OriginalValorizationHistoricalValue as numeric(18,2) = 0

					SELECT @AffectDepreciation = AffectDepreciation FROM FixedAsset.FixedAssetTransactionDetail where Id = @TransactionDetailId

					-- No Afecta Depreciación 
					IF @AffectDepreciation = 0 BEGIN
						-- Si es por ACTIVO
						if @TransactionClass = 1 and  @PhysicalAssetId IS NOT NULL BEGIN
						
							SELECT @OriginalValorization = Valorization, @OriginalAdjustedValueValorization = AdjustedValue , @OriginalValorizationHistoricalValue = HistoricalValue
							from FixedAsset.FixedAssetPhysicalAssetDetailBook where PhysicalAssetId = @PhysicalAssetId
						
							UPDATE FixedAsset.FixedAssetPhysicalAssetDetailBook 
								SET Valorization = (@OriginalValorization + @DetailTotalValue), 
								 AdjustedValue = (@OriginalAdjustedValueValorization + @DetailTotalValue),
								HistoricalValue = CASE 
									WHEN (@OriginalValorizationHistoricalValue + @DetailTotalValue) = ((DepreciatedValue + ResidualValue) - TransactionValue)
									THEN (@OriginalValorizationHistoricalValue + @DetailTotalValue)
									ELSE HistoricalValue  -- Mantiene el valor actual
								END
							WHERE PhysicalAssetId = @PhysicalAssetId
						END

						-- Si es por PARTES

						iF @TransactionClass = 2 and @PhysicalAssetPartsId IS NOT NULL BEGIN
						
							SELECT @OriginalValorization = Valorization, @OriginalAdjustedValueValorization = AdjustedValue  
							from FixedAsset.FixedAssetPhysicalAssetPartsDetailBook where PhysicalAssetPartsId = @PhysicalAssetPartsId
						
							UPDATE FixedAsset.FixedAssetPhysicalAssetPartsDetailBook 
								SET Valorization = (@OriginalValorization + @DetailTotalValue), 
								 AdjustedValue = (@OriginalAdjustedValueValorization + @DetailTotalValue)
							WHERE PhysicalAssetPartsId = @PhysicalAssetPartsId
						END

					-- SI AFECTA Depreciación
					END ELSE BEGIN
						
						-- DEBO REALIZAR EL AJUSTE DE ACUERDO AL DETALLE DE LIBROS DE LA TRANSACCIÓN

						declare @TransactionDetailBookId int
						Declare InfoPart Cursor For Select Id From FixedAsset.FixedAssetTransactionDetailBook where FixedAssetTransactionDetailId = @TransactionDetailId

						Open InfoPart
						Fetch Next From InfoPart Into @TransactionDetailBookId
						While @@fetch_status = 0
						Begin

							DECLARE @ValueDetailBook as numeric(20,4) = 0
							DECLARE @LifeUtilBook as int = 0
							DECLARE @UnitLifeUtilBook as int = 0
							DECLARE @LegalBookId as int = 0
							DECLARE @DaysValorization as int = 0
							DECLARE @OriginalValorizationDays as int = 0
							DECLARE @OriginalDaysPendingDepreciate as int = 0
							DECLARE @OriginalHistoricalValue  as decimal(18,2) = 0

							--Se agregan los valores correspondientes de acuerdo a los parametros establecidos
							SELECT 
								@ValueDetailBook = 
										CASE WHEN sfab.IvaCost = 0 AND @TaxRegistration = 2 --IVA al costo es NO y es IVA Descontable
											THEN (fd.Value - fd.DiscountValue)
											ELSE (fd.Value - fd.DiscountValue + fd.IvaValue)
										END,
								@LifeUtilBook = fdb.[LifeTime], 
								@UnitLifeUtilBook = fdb.UnitLifeTime, 
								@LegalBookId = fdb.LegalBookId 
							FROM FixedAsset.FixedAssetTransactionDetailBook fdb
							JOIN FixedAsset.FixedAssetTransactionDetail fd ON fd.Id = fdb.FixedAssetTransactionDetailId
							JOIN GeneralLedger.LegalBook lb ON lb.Id = fdb.LegalBookId
							JOIN FixedAsset.SettingFixedAsset sfa ON sfa.OperatingUnitId = @OperatingUnitId
							JOIN FixedAsset.SettingFixedAssetByLegalBook sfab ON sfa.Id = sfab.SettingFixedAssetId AND lb.Id = sfab.LegalBookId
							WHERE fdb.Id = @TransactionDetailBookId

							-- Si es por ACTIVO
							if @TransactionClass = 1 and  @PhysicalAssetId IS NOT NULL BEGIN
							
								if @ValueDetailBook > 0 OR (@LifeUtilBook > 0 AND @UnitLifeUtilBook > 0) BEGIN
									if @LegalBookId > 0 BEGIN

										SELECT @OriginalValorization = Valorization, @OriginalAdjustedValueValorization = AdjustedValue, @OriginalTransactionValue = TransactionValue,
										 @OriginalResidualValue = ResidualValue, @OriginalValorizationDays = ValorizationDays, @OriginalDaysPendingDepreciate = DaysPendingDepreciate, @OriginalHistoricalValue = HistoricalValue
										from FixedAsset.FixedAssetPhysicalAssetDetailBook where PhysicalAssetId = @PhysicalAssetId AND LegalBookId = @LegalBookId

										-- Tiene Valorización también con Días, debo convertirlo todo a DIAS
										IF @LifeUtilBook > 0 AND @UnitLifeUtilBook > 0 BEGIN
											-- AÑO
											if @UnitLifeUtilBook = 1 BEGIN
												SET @DaysValorization = @LifeUtilBook * 360
											-- MES
											END ELSE IF @UnitLifeUtilBook = 2 BEGIN
												SET @DaysValorization = @LifeUtilBook * 30
											-- DÍA 
											END ELSE IF @UnitLifeUtilBook = 3 BEGIN
												SET @DaysValorization = @LifeUtilBook
											END
										END ELSE BEGIN
											SET @DaysValorization = 0
										END

										UPDATE FixedAsset.FixedAssetPhysicalAssetDetailBook
										SET Valorization = @OriginalValorization + @ValueDetailBook,
										AdjustedValue = @OriginalAdjustedValueValorization + @ValueDetailBook,
										TransactionValue = @OriginalTransactionValue + @ValueDetailBook,
										ResidualValue = @OriginalResidualValue + @ValueDetailBook,
										ValorizationDays = @OriginalValorizationDays + @DaysValorization,
										DaysPendingDepreciate = @OriginalDaysPendingDepreciate + @DaysValorization,
										HistoricalValue =  CASE 
															WHEN (@OriginalHistoricalValue + @ValueDetailBook) <=  @OriginalHistoricalValue 
															THEN @OriginalHistoricalValue + @ValueDetailBook
															ELSE HistoricalValue -- No se actualiza
														 END
										where LegalBookId = @LegalBookId AND PhysicalAssetId = @PhysicalAssetId
									END
								END

								-- Si es por PARTES
								iF @TransactionClass = 2 and @PhysicalAssetPartsId IS NOT NULL BEGIN
							
									if @ValueDetailBook > 0 OR (@LifeUtilBook > 0 AND @UnitLifeUtilBook > 0) BEGIN
										if @LegalBookId > 0 BEGIN

											SELECT @OriginalValorization = Valorization, @OriginalAdjustedValueValorization = AdjustedValue, @OriginalTransactionValue = TransactionValue,
											 @OriginalResidualValue = ResidualValue, @OriginalValorizationDays = ValorizationDays, @OriginalDaysPendingDepreciate = DaysPendingDepreciate
											from FixedAsset.FixedAssetPhysicalAssetPartsDetailBook where PhysicalAssetPartsId = @PhysicalAssetPartsId AND LegalBookId = @LegalBookId

											-- Tiene Valorización también con Días, debo convertirlo todo a DIAS
											IF @LifeUtilBook > 0 AND @UnitLifeUtilBook > 0 BEGIN
												-- AÑO
												if @UnitLifeUtilBook = 1 BEGIN
													SET @DaysValorization = @LifeUtilBook * 360
												-- MES
												END ELSE IF @UnitLifeUtilBook = 2 BEGIN
													SET @DaysValorization = @LifeUtilBook * 30
												-- DÍA 
												END ELSE IF @UnitLifeUtilBook = 3 BEGIN
													SET @DaysValorization = @LifeUtilBook
												END
											END ELSE BEGIN
												SET @DaysValorization = 0
											END

											UPDATE FixedAsset.FixedAssetPhysicalAssetPartsDetailBook
											SET Valorization = @OriginalValorization + @ValueDetailBook,
											AdjustedValue = @OriginalAdjustedValueValorization + @ValueDetailBook,
											TransactionValue = @OriginalTransactionValue + @ValueDetailBook,
											ResidualValue = @OriginalResidualValue + @ValueDetailBook,
											ValorizationDays = @OriginalValorizationDays + @DaysValorization,
											DaysPendingDepreciate = @OriginalDaysPendingDepreciate + @DaysValorization
											where LegalBookId = @LegalBookId AND PhysicalAssetPartsId = @PhysicalAssetPartsId

											-- Actualizo en el Padre de la Parte el Valor Residual de las Partes
											DECLARE @PhysicalAssetParentId as Int = 0

											SELECT @PhysicalAssetParentId = PhysicalAssetId FROM FixedAsset.FixedAssetPhysicalAssetParts where Id = @PhysicalAssetPartsId

											IF @PhysicalAssetParentId > 0 BEGIN
												DECLARE @OriginalResidualValuePart as numeric(20,4) = 0
												SELECT ResidualValuePart = @OriginalResidualValuePart 
												FROM FixedAsset.FixedAssetPhysicalAssetDetailBook 
												where PhysicalAssetId = @PhysicalAssetParentId and LegalBookId = @LegalBookId

												UPDATE FixedAsset.FixedAssetPhysicalAssetDetailBook 
												SET ResidualValuePart = @OriginalResidualValuePart + @ValueDetailBook
												where PhysicalAssetId = @PhysicalAssetParentId and LegalBookId = @LegalBookId
											END

										END
									END
								END
							END

							--Se pasa a la siguiente posicion del cursor
							Fetch Next From InfoPart Into @TransactionDetailBookId
							continue
						End
						Close InfoPart
						Deallocate InfoPart					

						
					END

				END
	
				fetch next from C_Transaction into @TransactionDetailId, @TransactionClass, @TransactionType, @PhysicalAssetId, @PhysicalAssetPartsId, @DetailTotalValue
				end -- fin while de C_Fondos
			close C_Transaction 
			deallocate C_Transaction
		END

	select 0 as CodeMessage, 'Se guardó correctamente' as Message, @Code as Code, @Id as Id
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, '' as Code, 0 as Id
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o actualiza transacciones de activos fijos (compras, valorizaciones, bajas, ajustes) recibidas en formato XML. Gestiona la cabecera de la transacción (unidad operativa, fecha, proveedor, factura, retenciones, IVA, descuentos, valor total y moneda) junto con sus líneas de detalle por activo físico o componente, y los libros contables asociados a cada línea. También procesa las eliminaciones de detalles y libros enviadas como listas XML separadas, y registra la operación con el usuario que la ejecuta. Es el punto central de persistencia del módulo de activos fijos para todas las transacciones que afectan el valor, la vida útil y la depreciación de los bienes de la organización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetTransaction';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetTransaction';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Transacción de activo fijo; Valorización; Desvalorización; Depreciación; Vida útil; Libro legal/contable; Secuencia/consecutivo por formulario; Unidad operativa; Proveedor; Tercero; IVA descontable / IVA al costo; Retención en la fuente; Retención ICA; Anulación; Confirmación; Valor residual; Valor histórico; Conversión de moneda', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 3 (anulación) → Omite la carga/eliminación de detalles y la inserción/actualización de detalles; sólo actualiza la cabecera registrando AnnulmentUser y AnnulmentDate else Procesa eliminación, inserción/actualización de detalles y libros; si Id = 0 y Status <> 3 → Inserta cabecera nueva; si Code está vacío genera consecutivo desde FixedAssetSequence/FixedAssetSequenceDetail else Actualiza cabecera existente; si Code vacío y no existe registro en FixedAssetSequence con IdForm = 1121 → Devuelve CodeMessage 999 con mensaje ''No existe secuencia numerica para el formulario de transacciones'' y termina; si Scope de la secuencia = ''O'' → Obtiene patrón y siguiente número por prefijo (sin filtrar por unidad operativa) else Obtiene patrón y siguiente número filtrando por IdOperatingUnit = OperatingUnitId; si GetSequence retorna ''__ERROR_MAXVALUE__'' → Devuelve CodeMessage 999 ''La secuencia alcanzo su valor maximo'' y termina; si Existe ya un FixedAssetTransaction con el mismo Code → Devuelve CodeMessage 999 ''Ya existen registros con esta secuencia'' y termina sin insertar; si Status = 2 (confirmación) → Setea ConfirmationUser/ConfirmationDate y recorre los detalles para aplicar valorización/desvalorización a los libros del activo o de las partes; si TransactionType = 2 (Desvaluación) y TransactionClass = 1 con PhysicalAssetId no nulo → Suma TotalValue a Devaluation y resta a AdjustedValue en FixedAssetPhysicalAssetDetailBook del activo; si TransactionType = 2 y TransactionClass = 2 con PhysicalAssetPartsId no nulo → Suma TotalValue a Devaluation y resta a AdjustedValue en FixedAssetPhysicalAssetPartsDetailBook de la parte; si TransactionType = 1 (Valorización) y AffectDepreciation = 0 → Suma TotalValue a Valorization y AdjustedValue del libro del activo o parte; en activos actualiza HistoricalValue sólo si (HistoricalValue+valor) = ((DepreciatedValue+ResidualValue)-TransactionValue) else Si AffectDepreciation = 1 recorre cada FixedAssetTransactionDetailBook y ajusta el libro correspondiente recalculando días; si AffectDepreciation = 1 e IvaCost = 0 en SettingFixedAssetByLegalBook y TaxRegistration = 2 (IVA descontable) → Toma como valor del libro (Value - DiscountValue) sin sumar IvaValue else Toma como valor del libro (Value - DiscountValue + IvaValue); si UnitLifeTime = 1/2/3 con LifeTime > 0 → Convierte vida útil a días: año*360, mes*30, día tal cual else DaysValorization = 0; si TransactionClass = 2 con valorización que afecta depreciación y la parte tiene PhysicalAssetId padre → Suma el valor del libro al ResidualValuePart del activo padre en FixedAssetPhysicalAssetDetailBook para el mismo LegalBookId; si InvoiceDate = ''9999-12-31'' → Se normaliza a NULL antes de persistir', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.DecodeXmlToText; dbo.GetSequence; Common.CurrencyConverterByModule', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetSequence; FixedAsset.FixedAssetSequenceDetail; Common.Sequense; FixedAsset.FixedAssetTransaction; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetTransactionDetailBook; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetPhysicalAssetPartsDetailBook; FixedAsset.FixedAssetPhysicalAssetParts; FixedAsset.SettingFixedAsset; FixedAsset.SettingFixedAssetByLegalBook', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetTransaction';
-- GO
