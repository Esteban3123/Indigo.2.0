-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_SaveFixedAssetEntry] 
    @FixedAssetEntryXml AS XML,
	@ListDeleteFixedAssetEntryItemDetailPartBookXml AS XML,
	@ListDeleteFixedAssetEntryItemDetailPartXml AS XML,
	@ListDeleteFixedAssetEntryItemDetailBookXml AS XML,
	@ListDeleteFixedAssetEntryItemDetailXml AS XML,
	@ListDeleteFixedAssetEntryItemXml AS XML,
	@CodeUser AS VARCHAR(20)
	with recompile
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT, @Code VARCHAR(20), @EntryDate DATETIME, @EntryNumber VARCHAR(100), @AdquisitionType TINYINT, @SupplierId INT, 
		@SupplierDistributionLineId INT, @SupplierTypeId INT, @Description VARCHAR(1000), @GetLocationResponsible TINYINT, @LocationId INT,
		@ResponsibleId INT, @RoundService INT, @InvoiceNumber VARCHAR(100), @InvoiceDate DATETIME, @DayPeriod INT, @IcaPercentage numeric(5,3),
		@FreightValue DECIMAL(20,2), @FreightIVAPercentage numeric(5,2), @FreightIVAValue DECIMAL(20,2), @Value DECIMAL(20,2),
		@ValueDiscount DECIMAL(20,2), @ValueTax DECIMAL(20,2), @WithholdingTax DECIMAL(20,2), @WithholdingICA numeric(20,2), 
		@RetentionSource DECIMAL(20,2),@RetentionOther DECIMAL(20,2), @DeductionOther DECIMAL(20,2), @TotalValue DECIMAL(20,2), 
		@Status TINYINT, @CommitmentDetailId INT, @AccountPayableId INT, @CostCenterId INT, @OperatingUnitId INT, 
		@NumberContractLeasing VARCHAR(50), @InitialDateLeasing date, @EndDateLeasing date, @DocumentSupportId int, @CurrencyId INT, 
		@TaxRegistration INT,@EconomicActivityId INT
	
	--Tabla temporal de FixedAssetEntryItem
	DECLARE @FixedAssetEntryItem TABLE
	(
		Id INT, FixedAssetEntryId INT, RemissionSource TINYINT, SourceCode VARCHAR(20), PurchaseOrderItemId INT,RemissionEntranceItemId INT, ItemId INT, 
		IVAId INT, TrademarkId INT, Model VARCHAR(100), PolicyId INT, Quantity INT, OutstandingQuantity INT, UnitValue numeric(20,4), 
		SubTotalValue numeric(20,4), IvaPercentage numeric(5,2), IvaValue numeric(20,4), DiscountPercentage numeric(5,2), DiscountValue numeric(20,4), 
		TotalValue numeric(20,4), RTFPercentage numeric(5,2), RTFValue numeric(18,2),Observation VARCHAR(1000), TempId INT, Edited BIT
	)
	
	--Tabla temporal de FixedAssetEntryItemDetail
	DECLARE @FixedAssetEntryItemDetail TABLE
	(
		Id INT, FixedAssetEntryItemId INT, Plate VARCHAR(50), Serie VARCHAR(50), ReponsibleId INT, LocationId INT, AdquisitionDate date, Depreciate bit, 
		HandlesWarranty bit, WarrantyExpirationDate date, StatusAssetId INT, ParentId INT, TempId INT, Edited BIT,ValidSmallerAmount BIT, Amortize BIT
	)

	--Tabla temporal de FixedAssetEntryItemDetailBook
	DECLARE @FixedAssetEntryItemDetailBook TABLE
	(
		Id INT, FixedAssetEntryItemDetailId INT, LegalBookId INT, LifeTime INT, UnitLifeTime TINYINT, DepreciationType TINYINT, TotalProductionUnit numeric(18,0), 
		PercentageRescue numeric(5,2), HistoricalValue NUMERIC(20,4), DaysPendingDepreciate INT, ParentId INT, Edited BIT
	)

	--Tabla temporal de FixedAssetEntryItemDetailPart
	DECLARE @FixedAssetEntryItemDetailPart TABLE
	(
		Id INT, FixedAssetEntryItemDetailId INT, PartAccesoriesConsumiblesId INT, DepreciatePart bit,
		Value numeric(18,0), ParentId INT, TempId INT, Edited BIT
	)

	--Tabla temporal de FixedAssetEntryItemDetailPartBook
	DECLARE @FixedAssetEntryItemDetailPartBook TABLE
	(
		Id INT, FixedAssetEntryItemDetailPartId INT, LegalBookId INT, LifeTime INT, UnitLifeTime TINYINT,
		DepreciationType TINYINT, TotalProductionUnit numeric(18,0), PercentageRescue numeric(5,2), ParentId INT, Edited BIT
	)
	
	--Tabla en donde se almacena los compromisos que vienene del xml
	declare @FixedAssetEntryCommitment table(Id int, FixedAssetEntryId int, CommitmentDetailId int, Value numeric(20, 4), IsDelete bit)
	--valida el parametro
	Declare @Depreciation30Days INT  
	Begin try
		--Se obtiene datos de la cabecera

		select 
			@Id = t.x.value('Id[1]','INT'),
			@Code = t.x.value('Code[1]','VARCHAR(20)'),
			@EntryDate = t.x.value('EntryDate[1]','DATETIME'),
			@EntryNumber = t.x.value('EntryNumber[1]','VARCHAR(100)'),
			@AdquisitionType = t.x.value('AdquisitionType[1]','TINYINT'),
			@SupplierId = t.x.value('SupplierId[1]','INT'),
			@SupplierDistributionLineId = t.x.value('SupplierDistributionLineId[1]','INT'),
			@SupplierTypeId = t.x.value('SupplierTypeId[1]','INT'),
			@Description = t.x.value('Description[1]','VARCHAR(1000)'),
			@GetLocationResponsible = t.x.value('GetLocationResponsible[1]','TINYINT'),
			@LocationId = case t.x.value('LocationId[1]','INT') when 0 then null else t.x.value('LocationId[1]','INT') end,
			@ResponsibleId = case t.x.value('ResponsibleId[1]','INT') when 0 then null else t.x.value('ResponsibleId[1]','INT') end,
			@RoundService = t.x.value('RoundService[1]','INT'),
			@InvoiceNumber = t.x.value('InvoiceNumber[1]','VARCHAR(100)'),
			@InvoiceDate = t.x.value('InvoiceDate[1]','date'),
			@DayPeriod = t.x.value('DayPeriod[1]','INT'),
			@IcaPercentage = t.x.value('IcaPercentage[1]','numeric(5,3)'),
			@FreightValue = t.x.value('FreightValue[1]','DECIMAL(20,2)'),
			@FreightIVAPercentage = t.x.value('FreightIVAPercentage[1]','numeric(5,2)'),
			@FreightIVAValue = t.x.value('FreightIVAValue[1]','DECIMAL(20,2)'),
			@Value = t.x.value('Value[1]','DECIMAL(20,2)'),
			@ValueDiscount = t.x.value('ValueDiscount[1]','DECIMAL(20,2)'),
			@ValueTax = t.x.value('ValueTax[1]','DECIMAL(20,2)'),
			@WithholdingTax = t.x.value('WithholdingTax[1]','DECIMAL(20,2)'),
			@WithholdingICA = t.x.value('WithholdingICA[1]','DECIMAL(20,2)'),
			@RetentionSource = t.x.value('RetentionSource[1]','DECIMAL(20,2)'),
			@RetentionOther = t.x.value('RetentionOther[1]','DECIMAL(20,2)'),
			@DeductionOther = t.x.value('DeductionOther[1]','DECIMAL(20,2)'),
			@TotalValue = t.x.value('TotalValue[1]','DECIMAL(20,2)'),
			@Status = t.x.value('Status[1]','TINYINT'),
			@CommitmentDetailId = t.x.value('CommitmentDetailId[1]','INT'),
			@AccountPayableId = t.x.value('AccountPayableId[1]','INT'),
			@CostCenterId = t.x.value('CostCenterId[1]','INT'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
			@NumberContractLeasing = case t.x.value('NumberContractLeasing[1]','VARCHAR(50)') when '---' then null else t.x.value('NumberContractLeasing[1]','VARCHAR(50)') end,
			@InitialDateLeasing = NULLIF(COALESCE(TRY_CONVERT(date, NULLIF(LTRIM(RTRIM(t.x.value('InitialDateLeasing[1]','varchar(30)'))), ''), 23), TRY_CONVERT(date, NULLIF(LTRIM(RTRIM(t.x.value('InitialDateLeasing[1]','varchar(30)'))), ''), 103)), CAST('1900-01-01' AS date)),
			@EndDateLeasing = NULLIF(COALESCE(TRY_CONVERT(date, NULLIF(LTRIM(RTRIM(t.x.value('EndDateLeasing[1]','varchar(30)'))), ''), 23), TRY_CONVERT(date, NULLIF(LTRIM(RTRIM(t.x.value('EndDateLeasing[1]','varchar(30)'))), ''), 103)), CAST('1900-01-01' AS date)),
			@DocumentSupportId = t.x.value('DocumentSupportId[1]','INT'),
			@CurrencyId = t.x.value('CurrencyId[1]','INT'),
			@TaxRegistration = t.x.value('TaxRegistration[1]','INT'),
			@EconomicActivityId = IIf(t.x.value('EconomicActivityId[1]', 'INT') = 0, NULL, t.x.value('EconomicActivityId[1]', 'INT'))
		from @FixedAssetEntryXml.nodes('/FixedAssetEntry') t(x)
		
		DECLARE @OfficialLegalBookId INT,
				@SettingFixedAssetId INT,
				@IvaCost BIT = 0,
				@LowBidAmount NUMERIC(18,0) = 0,
				@TopMinorValue NUMERIC(18,0) = 0,
				@IVAIdPercentageZero INT,
				@OfficialCurrencyId as INT,
				------------------------------
				@errors VARCHAR(MAX)
print concat('Actividad Economica: ',@EconomicActivityId)

		--Id del libro oficial, esta variable es utilizada para asignar la cuenta cuando es renting financiero
		select @OfficialLegalBookId = Id from GeneralLedger.LegalBook where OfficialBook = 1

		--Buscamos si el parametro deprecia a 30 dias
		set @Depreciation30Days = (select Depreciation30Days FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId) 
		--Parámetros definidos para determinar si maneja iva al costo en el libro contable oficial y el valor de menor cuantía establecido
		SELECT @SettingFixedAssetId = sfa.Id,
			   @IvaCost = ISNULL(sfalb.IvaCost, 0), 
			   @LowBidAmount = sfa.LowBidAmount, 
			   @TopMinorValue = sfa.TopMinorValue,
			   @OfficialCurrencyId = sfa.CurrencyId
		FROM FixedAsset.SettingFixedAsset sfa
		LEFT JOIN
		(
			SELECT sfalb.SettingFixedAssetId, sfalb.IvaCost
			FROM FixedAsset.SettingFixedAssetByLegalBook sfalb
			WHERE sfalb.LegalBookId = @OfficialLegalBookId
		) sfalb ON sfa.Id = sfalb.SettingFixedAssetId
		WHERE sfa.OperatingUnitId = @OperatingUnitId
		

		--Id del iva cuando porcentage es 0
		SELECT @IVAIdPercentageZero = Id from GeneralLedger.GeneralLedgerIVA where Percentage = 0 AND Status = 1

		IF EXISTS (SELECT 1 FROM [FixedAsset].[FixedAssetEntry] fae WHERE fae.Id = @Id AND fae.Status <> 1)
		BEGIN
			SELECT 999 AS CodeMessage, 'El registro se encuentra en estado: ' + IIF(fae.Status = 2, 'Confirmado', 'Anulado') AS Message, '' AS Code, 0 AS Id 
			FROM [FixedAsset].[FixedAssetEntry] fae
			WHERE fae.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [FixedAsset].[FixedAssetEntry] 
				SET [Status] = @Status, 
					[ModificationUser] = @CodeUser, 
					[ModificationDate] = [Common].[GETDATE](), 
					AnnulmentUser = @CodeUser,
					AnnulmentDate = [Common].[GETDATE]()
		   where Id = @Id

		   select 0 AS CodeMessage, 'Se anuló correctamente' AS Message, @Code AS Code, @Id AS Id	
		   return
		END
		ELSE
		BEGIN
			/***************************************************** ELIMINACION ***********************************************/

			--Eliminación FixedAssetEntryItemDetailPartBook
			DELETE faeidpb
			FROM @ListDeleteFixedAssetEntryItemDetailPartBookXml.nodes('/ListDeleteFixedAssetEntryItemDetailPartBook') t(x)
			JOIN FixedAsset.FixedAssetEntryItemDetailPartBook faeidpb ON t.x.value('Id[1]','int') = faeidpb.Id
			JOIN FixedAsset.FixedAssetEntryItemDetailPart faeidp ON faeidpb.FixedAssetEntryItemDetailPartId = faeidp.Id
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faeidp.FixedAssetEntryItemDetailId = faeid.Id
			JOIN FixedAsset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			WHERE faei.FixedAssetEntryId = @Id

			--Eliminación FixedAssetEntryItemDetailPart
			DELETE faeidp
			FROM @ListDeleteFixedAssetEntryItemDetailPartXml.nodes('/ListDeleteFixedAssetEntryItemDetailPart') t(x)
			JOIN FixedAsset.FixedAssetEntryItemDetailPart faeidp ON t.x.value('Id[1]','int') = faeidp.Id
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faeidp.FixedAssetEntryItemDetailId = faeid.Id
			JOIN FixedAsset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			WHERE faei.FixedAssetEntryId = @Id

			--Eliminación FixedAssetEntryItemDetailBook
			DELETE faeidb
			FROM @ListDeleteFixedAssetEntryItemDetailBookXml.nodes('/ListDeleteFixedAssetEntryItemDetailBook') t(x)
			JOIN FixedAsset.FixedAssetEntryItemDetailBook faeidb ON t.x.value('Id[1]','int') = faeidb.Id
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faeidb.FixedAssetEntryItemDetailId = faeid.Id
			JOIN FixedAsset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			WHERE faei.FixedAssetEntryId = @Id

			--Eliminación FixedAssetEntryItemDetail
			DELETE faeid
			FROM @ListDeleteFixedAssetEntryItemDetailXml.nodes('/ListDeleteFixedAssetEntryItemDetail') t(x)
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON t.x.value('Id[1]','int') = faeid.Id
			JOIN FixedAsset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			WHERE faei.FixedAssetEntryId = @Id

			--Eliminación FixedAssetEntryItem
			DELETE faei
			FROM @ListDeleteFixedAssetEntryItemXml.nodes('/ListDeleteFixedAssetEntryItem') t(x)
			JOIN FixedAsset.FixedAssetEntryItem faei ON t.x.value('Id[1]','int') = faei.Id
			WHERE faei.FixedAssetEntryId = @Id

			--Eliminación FixedAssetEntryCommitment
			delete from FixedAsset.FixedAssetEntryCommitment where Id in (select t.x.value('Id[1]','int') 
																		  from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryCommitment') t(x)
																		  where t.x.value('Id[1]','int') > 0 and t.x.value('IsDelete[1]','bit') = 1)

			/************************************************ CARGAMOS DETALLES ************************************************/

			--Se obtiene FixedAssetEntryCommitment
			insert into @FixedAssetEntryCommitment
			select 
				t.x.value('Id[1]','int') as Id,
				t.x.value('FixedAssetEntryId[1]','int') as FixedAssetEntryId,
				t.x.value('CommitmentDetailId[1]','int') as CommitmentDetailId,
				REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
				t.x.value('IsDelete[1]','bit') as IsDelete
			from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryCommitment') t(x)
			where t.x.value('IsDelete[1]','bit') = 0

			--Se obtiene FixedAssetEntryItem
			insert into @FixedAssetEntryItem
				select 
					t.x.value('Id[1]','int') AS Id,
					@Id AS FixedAssetEntryId,
					t.x.value('RemissionSource[1]','tinyint') AS RemissionSource,
					case when LEN(t.x.value('SourceCode[1]','varchar(20)')) = 0 then null else t.x.value('SourceCode[1]','varchar(20)') end AS SourceCode,
					case when t.x.value('PurchaseOrderItemId[1]','int') = 0 then null else t.x.value('PurchaseOrderItemId[1]','int') end AS PurchaseOrderItemId,
					case when t.x.value('RemissionEntranceItemId[1]','int') = 0 then null else t.x.value('RemissionEntranceItemId[1]','int') end AS RemissionEntranceItemId,
					t.x.value('ItemId[1]','int') AS ItemId,
					CASE @AdquisitionType
						WHEN 4 THEN @IVAIdPercentageZero
						ELSE
							case 
								when t.x.value('IVAId[1]','int') = 0 then null 
								else t.x.value('IVAId[1]','int') 
							END
					END AS IVAId,
					t.x.value('TrademarkId[1]','int') AS TrademarkId,
					t.x.value('Model[1]','varchar(100)') AS Model,
					t.x.value('PolicyId[1]','int') AS PolicyId,
					t.x.value('Quantity[1]','int') AS Quantity,
					t.x.value('OutstandingQuantity[1]','int') AS OutstandingQuantity,
					t.x.value('UnitValue[1]','numeric(20,4)') AS UnitValue,
					t.x.value('SubTotalValue[1]','numeric(20,4)') AS SubTotalValue,
					t.x.value('IvaPercentage[1]','numeric(5,2)') AS IvaPercentage,
					t.x.value('IvaValue[1]','numeric(20,4)') AS IvaValue,
					t.x.value('DiscountPercentage[1]','numeric(5,2)') AS DiscountPercentage,
					t.x.value('DiscountValue[1]','numeric(20,4)') AS DiscountValue,
					t.x.value('TotalValue[1]','numeric(20,4)') AS TotalValue,
					t.x.value('RTFPercentage[1]','numeric(5,2)') AS RTFPercentage,
					t.x.value('RTFValue[1]','numeric(18,2)') AS RTFValue,
					t.x.value('Observation[1]','varchar(1000)') AS Observation,
					t.x.value('TempId[1]','int') AS TempId,
					1 Edited
				from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryItem') t(x)
			
			insert into @FixedAssetEntryItem
				SELECT
					faei.Id,
					faei.FixedAssetEntryId,
					faei.RemissionSource,
					faei.SourceCode,
					faei.PurchaseOrderItemId,
					faei.RemissionEntranceItemId,
					faei.ItemId,
					faei.IVAId,
					faei.TrademarkId,
					faei.Model,
					faei.PolicyId,
					faei.Quantity,
					faei.OutstandingQuantity,
					faei.UnitValue,
					faei.SubTotalValue,
					faei.IvaPercentage,
					faei.IvaValue,
					faei.DiscountPercentage,
					faei.DiscountValue,
					faei.TotalValue,
					faei.RTFPercentage,
					faei.RTFValue,
					faei.Observation,
					faei.Id,
					0 Edited
				FROM FixedAsset.FixedAssetEntryItem faei
				LEFT JOIN @FixedAssetEntryItem d ON faei.Id = d.Id
				WHERE faei.FixedAssetEntryId = @Id AND d.Id IS NULL
		
			--Se obtiene FixedAssetEntryItemDetail
			insert into @FixedAssetEntryItemDetail
				select 
					t.x.value('Id[1]','int') AS Id,
					t.x.value('FixedAssetEntryItemId[1]','int') AS FixedAssetEntryItemId,
					t.x.value('Plate[1]','varchar(50)') AS Plate,
					t.x.value('Serie[1]','varchar(50)') AS Serie,
					t.x.value('ReponsibleId[1]','int') AS ReponsibleId,
					t.x.value('LocationId[1]','int') AS LocationId,
					t.x.value('AdquisitionDate[1]','date') AS AdquisitionDate,
					t.x.value('Depreciate[1]','bit') AS Depreciate,
					t.x.value('HandlesWarranty[1]','bit') AS HandlesWarranty,
					CASE
						WHEN NULLIF(LTRIM(RTRIM(t.x.value('WarrantyExpirationDate[1]','varchar(30)'))), '') IN ('0') THEN NULL
						ELSE COALESCE(
							TRY_CONVERT(date, t.x.value('WarrantyExpirationDate[1]','varchar(30)'), 23),
							TRY_CONVERT(date, t.x.value('WarrantyExpirationDate[1]','varchar(30)'), 103)
						)
					END AS WarrantyExpirationDate,
					t.x.value('StatusAssetId[1]','int') AS StatusAssetId,
					t.x.value('ParentId[1]','int') AS ParentId,
					t.x.value('TempId[1]','int') AS TempId,
					1 Edited,
					t.x.value('ValidSmallerAmount[1]','bit'),
					t.x.value('Amortize[1]', 'bit') AS Amortize
				from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryItem/FixedAssetEntryItemDetail') t(x)
			
			insert into @FixedAssetEntryItemDetail
				SELECT
					faeid.Id,
					faeid.FixedAssetEntryItemId,
					faeid.Plate,
					faeid.Serie,
					faeid.ReponsibleId,
					faeid.LocationId,
					faeid.AdquisitionDate,
					faeid.Depreciate,
					faeid.HandlesWarranty,
					faeid.WarrantyExpirationDate,
					faeid.StatusAssetId,
					faeid.FixedAssetEntryItemId,
					faeid.Id,
					0 Edited,
					faeid.ValidSmallerAmount,
					faeid.Amortize
				FROM FixedAsset.FixedAssetEntryItem faei
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				LEFT JOIN @FixedAssetEntryItemDetail d ON faeid.Id = d.Id
				WHERE faei.FixedAssetEntryId = @Id AND d.Id IS NULL
		
			--Se obtiene FixedAssetEntryItemDetailBook
			insert into @FixedAssetEntryItemDetailBook
				select 
					t.x.value('Id[1]','int') AS Id,
					t.x.value('FixedAssetEntryItemDetailId[1]','int') AS FixedAssetEntryItemDetailId,
					t.x.value('LegalBookId[1]','int') AS LegalBookId,
					t.x.value('LifeTime[1]','int') AS LifeTime,
					t.x.value('UnitLifeTime[1]','tinyint') AS UnitLifeTime,
					t.x.value('DepreciationType[1]','tinyint') AS DepreciationType,
					t.x.value('TotalProductionUnit[1]','numeric(18,0)') AS TotalProductionUnit,
					t.x.value('PercentageRescue[1]','numeric(5,2)') AS PercentageRescue,
					0 HistoricalValue,
					0 DaysPendingDepreciate,
					t.x.value('ParentId[1]','int') AS ParentId,
					1 Edited
				from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryItem/FixedAssetEntryItemDetail/FixedAssetEntryItemDetailBook') t(x)
			
			insert into @FixedAssetEntryItemDetailBook
				SELECT
					faeidb.Id,
					faeidb.FixedAssetEntryItemDetailId,
					faeidb.LegalBookId,
					faeidb.LifeTime,
					faeidb.UnitLifeTime,
					faeidb.DepreciationType,
					faeidb.TotalProductionUnit,
					faeidb.PercentageRescue,
					faeidb.HistoricalValue,
					faeidb.DaysPendingDepreciate,
					faeidb.FixedAssetEntryItemDetailId,
					0 Edited
				FROM FixedAsset.FixedAssetEntryItem faei
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetEntryItemDetailBook faeidb ON faeid.Id = faeidb.FixedAssetEntryItemDetailId
				LEFT JOIN @FixedAssetEntryItemDetailBook d ON faeidb.Id = d.Id
				WHERE faei.FixedAssetEntryId = @Id AND d.Id IS NULL

			--Se obtiene FixedAssetEntryItemDetailPart
			insert into @FixedAssetEntryItemDetailPart
				select 
					t.x.value('Id[1]','int') AS Id,
					t.x.value('FixedAssetEntryItemDetailId[1]','int') AS FixedAssetEntryItemDetailId,
					t.x.value('PartAccesoriesConsumiblesId[1]','int') AS PartAccesoriesConsumiblesId,
					t.x.value('DepreciatePart[1]','bit') AS DepreciatePart,
					t.x.value('Value[1]','numeric(18,0)') AS Value,
					t.x.value('ParentId[1]','int') AS ParentId,
					t.x.value('TempId[1]','int') AS TempId,
					1 Edited
				from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryItem/FixedAssetEntryItemDetail/FixedAssetEntryItemDetailPart') t(x)
			
			insert into @FixedAssetEntryItemDetailPart
				SELECT
					faeidp.Id,
					faeidp.FixedAssetEntryItemDetailId,
					faeidp.PartAccesoriesConsumiblesId,
					faeidp.DepreciatePart,
					faeidp.Value,
					faeidp.FixedAssetEntryItemDetailId,
					faeidp.Id,
					0 Edited
				FROM FixedAsset.FixedAssetEntryItem faei
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetEntryItemDetailPart faeidp ON faeid.Id = faeidp.FixedAssetEntryItemDetailId
				LEFT JOIN @FixedAssetEntryItemDetailBook d ON faeidp.Id = d.Id
				WHERE faei.FixedAssetEntryId = @Id AND d.Id IS NULL

			--Se obtiene FixedAssetEntryItemDetailPartBook
			insert into @FixedAssetEntryItemDetailPartBook
				select 
					t.x.value('Id[1]','int') AS Id,
					t.x.value('FixedAssetEntryItemDetailPartId[1]','int') AS FixedAssetEntryItemDetailPartId,
					t.x.value('LegalBookId[1]','int') AS LegalBookId,
					t.x.value('LifeTime[1]','int') AS LifeTime,
					t.x.value('UnitLifeTime[1]','tinyint') AS UnitLifeTime,
					t.x.value('DepreciationType[1]','tinyint') AS DepreciationType,
					t.x.value('TotalProductionUnit[1]','numeric(18,0)') AS TotalProductionUnit,
					t.x.value('PercentageRescue[1]','numeric(5,2)') AS PercentageRescue,
					t.x.value('ParentId[1]','int') AS ParentId,
					1 Edited
				from @FixedAssetEntryXml.nodes('/FixedAssetEntry/FixedAssetEntryItem/FixedAssetEntryItemDetail/FixedAssetEntryItemDetailPart/FixedAssetEntryItemDetailPartBook') t(x)
			
			insert into @FixedAssetEntryItemDetailPartBook
				SELECT
					faeidpb.Id,
					faeidpb.FixedAssetEntryItemDetailPartId,
					faeidpb.LegalBookId,
					faeidpb.LifeTime,
					faeidpb.UnitLifeTime,
					faeidpb.DepreciationType,
					faeidpb.TotalProductionUnit,
					faeidpb.PercentageRescue,
					faeidpb.FixedAssetEntryItemDetailPartId,
					0 Edited
				FROM FixedAsset.FixedAssetEntryItem faei
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetEntryItemDetailPart faeidp ON faeid.Id = faeidp.FixedAssetEntryItemDetailId
				JOIN FixedAsset.FixedAssetEntryItemDetailPartBook faeidpb ON faeidp.Id = faeidpb.FixedAssetEntryItemDetailPartId
				LEFT JOIN @FixedAssetEntryItemDetailBook d ON faeidpb.Id = d.Id
				WHERE faei.FixedAssetEntryId = @Id AND d.Id IS NULL

			/***************************************************** VALIDACIONES ***********************************************/

			if @AdquisitionType = 7 --Leasing financiero
			Begin
				--Se valida que el numero de contrato leasing no exista
				if (select count(*) from FixedAsset.FixedAssetEntry where NumberContractLeasing = @NumberContractLeasing and Id <> @Id) > 0
				Begin
					DECLARE @cNumber VARCHAR(50)
					DECLARE @cCode VARCHAR(20)
					select @cCode = Code, @cNumber = NumberContractLeasing from FixedAsset.FixedAssetEntry where NumberContractLeasing = @NumberContractLeasing and Code <> @Code
					select 999 AS CodeMessage, 'El número de contrato leasing ' + @cNumber + ' ya existe en el ingreso ' + @cCode AS Message, '' AS Code, 0 AS Id
					return
				End
			End

			IF NOT EXISTS (SELECT 1 FROM @FixedAssetEntryItem faei)
			BEGIN
				SELECT 999 AS CodeMessage, 'El documento no tiene detalles.' AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM @FixedAssetEntryItem faei
				JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
				GROUP BY faeid.Plate
				HAVING COUNT(*) > 1
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + faeid.Plate
						FROM @FixedAssetEntryItem faei
						JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
						GROUP BY faeid.Plate
						HAVING COUNT(*) > 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Las siguientes placas se encuentra duplicadas en el ingreso: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM @FixedAssetEntryItem faei
				JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
				WHERE faei.RemissionSource IN (1, 2)
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + faeid.Plate
						FROM @FixedAssetEntryItem faei
						JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
						JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
						WHERE faei.RemissionSource IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Las siguientes placas ya existen: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM FixedAsset.FixedAssetPurchaseOrderItem fapoi
				JOIN
				(
					SELECT 
						faei.PurchaseOrderItemId,
						COUNT(1) Quantity
					FROM @FixedAssetEntryItem faei
					JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
					WHERE faei.RemissionSource = 2
					GROUP BY faei.PurchaseOrderItemId
				) faei ON fapoi.Id = faei.PurchaseOrderItemId
				WHERE faei.Quantity > fapoi.OutstandingQuantity
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT(fai.Code,' - ',fai.Description)
						FROM FixedAsset.FixedAssetPurchaseOrderItem fapoi						
						JOIN
						(
							SELECT 
								faei.PurchaseOrderItemId,
								COUNT(1) Quantity
							FROM @FixedAssetEntryItem faei
							JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
							WHERE faei.RemissionSource = 2
							GROUP BY faei.PurchaseOrderItemId
						) faei ON fapoi.Id = faei.PurchaseOrderItemId
						JOIN FixedAsset.FixedAssetItem fai ON fapoi.ItemId = fai.Id
						WHERE faei.Quantity > fapoi.OutstandingQuantity
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Las cantidad de los siguientes artículos es mayor a la cantidad de la orden de compra: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM FixedAsset.FixedAssetRemissionEntranceItem farei
				JOIN
				(
					SELECT 
						faei.RemissionEntranceItemId,
						COUNT(1) Quantity
					FROM @FixedAssetEntryItem faei
					JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
					WHERE faei.RemissionSource = 3
					GROUP BY faei.RemissionEntranceItemId
				) faei ON farei.Id = faei.RemissionEntranceItemId
				WHERE faei.Quantity > farei.OutstandingQuantity
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT(fai.Code,' - ',fai.Description)
						FROM FixedAsset.FixedAssetRemissionEntranceItem farei
						JOIN
						(
							SELECT 
								faei.RemissionEntranceItemId,
								COUNT(1) Quantity
							FROM @FixedAssetEntryItem faei
							JOIN @FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
							WHERE faei.RemissionSource = 3
							GROUP BY faei.RemissionEntranceItemId
						) faei ON farei.Id = faei.RemissionEntranceItemId
						JOIN FixedAsset.FixedAssetItem fai ON farei.ItemId = fai.Id
						WHERE faei.Quantity > farei.OutstandingQuantity
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Las cantidad de los siguientes artículos es mayor a la cantidad de la remisión de entrada: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM @FixedAssetEntryItem faei
				LEFT JOIN
				(
					SELECT 
						faeid.FixedAssetEntryItemId, faeid.ParentId, COUNT(1) Quantity
					FROM @FixedAssetEntryItemDetail faeid
					GROUP BY faeid.FixedAssetEntryItemId, faeid.ParentId
				) faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
				WHERE faei.Quantity <> ISNULL(faeid.Quantity, 0)
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT(fai.Code,' - ',fai.Description)
						FROM @FixedAssetEntryItem faei
						LEFT JOIN
						(
							SELECT 
								faeid.FixedAssetEntryItemId, faeid.ParentId, COUNT(1) Quantity
							FROM @FixedAssetEntryItemDetail faeid
							GROUP BY faeid.FixedAssetEntryItemId, faeid.ParentId
						) faeid ON faei.Id = faeid.FixedAssetEntryItemId AND faei.TempId = faeid.ParentId
						JOIN FixedAsset.FixedAssetItem fai ON faei.ItemId = fai.Id
						WHERE faei.Quantity <> ISNULL(faeid.Quantity, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Las cantidad de los siguientes artículos no corresponde con la de sus detalles: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmUser AS VARCHAR(20) = case when @Status <> 2 then null else @CodeUser end
			DECLARE @ConfirmDate AS DATETIME = case when @Status <> 2 then null else [Common].[GETDATE]() end

			IF @Id = 0
			BEGIN
				IF @Code = '' 
				BEGIN
					--Consultamos si la secuencia es con O o OU
					DECLARE @IdForm VARCHAR(5) = '1116',							
							@pattern VARCHAR(300),
							@NextS INT,
							@idSequenceDetail INT

					-- Consultamos la secuencia numerica del formulario
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
					FROM FixedAsset.FixedAssetSequenceDetail bsd 
					JOIN FixedAsset.FixedAssetSequence bs ON bs.Id = bsd.IdSequenseFixedAssetC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm 
						AND 
						(
							(bs.Scope = 'O')
							OR
							(bs.Scope <> 'O' AND bsd.IdOperatingUnit = @OperatingUnitId)
						)

					if (@idSequenceDetail is null)
					Begin
						select 999 AS CodeMessage, 'Secuencia de Ingresos de Activos Fijos no encontrada' AS Message, '' AS Code, 0 AS Id
						return
					End

					SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
					UPDATE FixedAsset.FixedAssetSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

				end
			
				--Se inserta la cabecera
				INSERT INTO [FixedAsset].[FixedAssetEntry]
				(
					[Code],[EntryDate],[EntryNumber],[AdquisitionType],[SupplierId],[SupplierDistributionLineId],[SupplierTypeId],[Description]
					,[GetLocationResponsible],[LocationId],[ResponsibleId],[RoundService],[InvoiceNumber],[InvoiceDate],[DayPeriod],[IcaPercentage],[FreightValue]
					,[FreightIVAPercentage],[FreightIVAValue],[Value],[ValueDiscount],[ValueTax],[WithholdingTax],[WithholdingICA],[RetentionSource],[RetentionOther]
					,[DeductionOther],[TotalValue],[Status],[CreationUser],[CreationDate], [ConfirmationUser], [ConfirmationDate], [CostCenterId], [OperatingUnitId],
					[CommitmentDetailId], [AccountPayableId], NumberContractLeasing, InitialDateLeasing, EndDateLeasing, DocumentSupportId, CurrencyId, TaxRegistration,EconomicActivityId
				)
				SELECT
					@Code, @EntryDate, @EntryNumber, @AdquisitionType, @SupplierId, @SupplierDistributionLineId, @SupplierTypeId, @Description, 
					@GetLocationResponsible, @LocationId, @ResponsibleId, @RoundService, @InvoiceNumber, @InvoiceDate, @DayPeriod, @IcaPercentage, @FreightValue, 
					@FreightIVAPercentage, @FreightIVAValue, @Value, @ValueDiscount, @ValueTax, @WithholdingTax, @WithholdingICA, @RetentionSource, @RetentionOther, 
					@DeductionOther, @TotalValue, @Status, @CodeUser, [Common].[GETDATE](), @ConfirmUser, @ConfirmDate, @CostCenterId, @OperatingUnitId, 
					@CommitmentDetailId, @AccountPayableId, @NumberContractLeasing, @InitialDateLeasing, @EndDateLeasing, @DocumentSupportId, @CurrencyId, @TaxRegistration,@EconomicActivityId
		   
			   --Obtengo el id de la cabcera
				set @Id = SCOPE_IDENTITY()
			End
			Else --Si se esta actualizando
			Begin
				UPDATE [FixedAsset].[FixedAssetEntry] 
					set [Code] = @Code, [EntryDate] = @EntryDate, [EntryNumber] = @EntryNumber, [AdquisitionType] = @AdquisitionType, [SupplierId] = @SupplierId,
						[SupplierDistributionLineId] = @SupplierDistributionLineId, [SupplierTypeId] = @SupplierTypeId, [Description] = @Description,
						[GetLocationResponsible] = @GetLocationResponsible, [LocationId] = @LocationId, [ResponsibleId] = @ResponsibleId, [RoundService] = @RoundService, 
						[InvoiceNumber] = @InvoiceNumber, [InvoiceDate] = @InvoiceDate, [DayPeriod] = @DayPeriod, [IcaPercentage] = @IcaPercentage, 
						[FreightValue] = @FreightValue, [FreightIVAPercentage] = @FreightIVAPercentage, [FreightIVAValue] = @FreightIVAValue, [Value] = @Value, 
						[ValueDiscount] = @ValueDiscount, [ValueTax] = @ValueTax, [WithholdingTax] = @WithholdingTax, [WithholdingICA] = @WithholdingICA, 
						[RetentionSource] = @RetentionSource, [RetentionOther] = @RetentionOther, [DeductionOther] = @DeductionOther, [TotalValue] = @TotalValue, 
						[Status] = @Status, [ModificationUser] = @CodeUser, [ModificationDate] = [Common].[GETDATE](), [ConfirmationUser] = @ConfirmUser, [ConfirmationDate] = @ConfirmDate, 
						[CostCenterId] = @CostCenterId, OperatingUnitId = @OperatingUnitId, CommitmentDetailId = @CommitmentDetailId, AccountPayableId = @AccountPayableId, 
						NumberContractLeasing = @NumberContractLeasing, InitialDateLeasing = @InitialDateLeasing, EndDateLeasing = @EndDateLeasing, DocumentSupportId = @DocumentSupportId, TaxRegistration = @TaxRegistration,
						CurrencyId = @CurrencyId, EconomicActivityId=@EconomicActivityId
			   where Id = @Id
			END
			/*************************************************************************************/

			--Se insertan los compromisos
			insert into [FixedAsset].[FixedAssetEntryCommitment]([FixedAssetEntryId], [CommitmentDetailId], [Value])
			select @Id, temp.CommitmentDetailId, temp.Value
			from @FixedAssetEntryCommitment temp
			where temp.Id = 0 and temp.IsDelete = 0

			--Se actualizan los compromisos
			update ec set ec.Value = temp.Value
			from @FixedAssetEntryCommitment temp
			inner join FixedAsset.FixedAssetEntryCommitment ec on ec.Id = temp.Id
			where temp.Id > 0 and temp.IsDelete = 0

			--Se inserta FixedAssetEntryItem
			DECLARE @FixedAssetEntryItemRows INT = 1,
					@FixedAssetEntryItemId INT = -1,
					@FixedAssetEntryItemTempRows INT,
					@FixedAssetEntryItemTempId INT

			WHILE @FixedAssetEntryItemRows > 0
			BEGIN
				SELECT TOP 1
					@FixedAssetEntryItemId = faei.Id,
					@FixedAssetEntryItemTempRows = 1,
					@FixedAssetEntryItemTempId = 0
				FROM @FixedAssetEntryItem faei
				WHERE faei.Edited = 1
					AND faei.Id > @FixedAssetEntryItemId
				ORDER BY faei.Id

				--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
				SET @FixedAssetEntryItemRows = @@ROWCOUNT				

				IF @FixedAssetEntryItemRows = 0 
				BEGIN				
					BREAK
				END
				
				WHILE @FixedAssetEntryItemTempRows > 0
				BEGIN
					SELECT TOP 1
						@FixedAssetEntryItemTempId = faei.TempId
					FROM @FixedAssetEntryItem faei
					WHERE faei.Edited = 1
						AND faei.Id = @FixedAssetEntryItemId
						AND faei.TempId > @FixedAssetEntryItemTempId
					ORDER BY faei.TempId

					--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
					SET @FixedAssetEntryItemTempRows = @@ROWCOUNT
					IF @FixedAssetEntryItemTempRows = 0 
					BEGIN
						BREAK
					END

					if @FixedAssetEntryItemId = 0 --Si se esta insertando el registro
					Begin
						--Se inserta el registro
						insert into [FixedAsset].[FixedAssetEntryItem]
						(
							[FixedAssetEntryId], [RemissionSource], [SourceCode], [PurchaseOrderItemId], [RemissionEntranceItemId], [ItemId], [IVAId], [TrademarkId], [Model], 
							[PolicyId], [Quantity], [OutstandingQuantity], [UnitValue], [SubTotalValue], [IvaPercentage], [IvaValue], [DiscountPercentage], [DiscountValue], 
							[TotalValue], [RTFPercentage], [RTFValue],[Observation]
						)
						select 
							@Id, RemissionSource, SourceCode, PurchaseOrderItemId, RemissionEntranceItemId, ItemId, IVAId, TrademarkId, Model, 
							PolicyId, Quantity, OutstandingQuantity, UnitValue, SubTotalValue, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue, 
							TotalValue, IIF(@RetentionSource = 0, 0, RTFPercentage), IIF(@RetentionSource = 0, 0, RTFValue), Observation
						from @FixedAssetEntryItem where Id = 0 and TempId = @FixedAssetEntryItemTempId

						--Se actualiza el id en la tabla temporal @FixedAssetEntryItem
						update @FixedAssetEntryItem 
							set Id = SCOPE_IDENTITY(), 
								TempId = SCOPE_IDENTITY(),
								Edited = 0 
						where Id = 0 and TempId = @FixedAssetEntryItemTempId

						--Se actualiza el id de la relacion en la tabla temporal @FixedAssetEntryItemDetail
						update @FixedAssetEntryItemDetail 
							set FixedAssetEntryItemId = SCOPE_IDENTITY(),
								ParentId = SCOPE_IDENTITY()
						where FixedAssetEntryItemId = 0 AND ParentId = @FixedAssetEntryItemTempId
					End
					Else --Si se esta actualizando
					Begin
						update faei 
							set faei.FixedAssetEntryId = faeiTemp.FixedAssetEntryId, faei.RemissionSource = faeiTemp.RemissionSource, faei.SourceCode = faeiTemp.SourceCode,
								faei.PurchaseOrderItemId = faeiTemp.PurchaseOrderItemId, faei.RemissionEntranceItemId = faeiTemp.RemissionEntranceItemId, faei.ItemId = faeiTemp.ItemId,
								faei.IVAId = faeiTemp.IVAId, faei.TrademarkId = faeiTemp.TrademarkId, faei.Model = faeiTemp.Model, faei.PolicyId = faeiTemp.PolicyId,
								faei.Quantity = faeiTemp.Quantity, faei.OutstandingQuantity = faeiTemp.OutstandingQuantity, faei.UnitValue = faeiTemp.UnitValue, 
								faei.SubTotalValue = faeiTemp.SubTotalValue, faei.IvaPercentage = faeiTemp.IvaPercentage, faei.IvaValue = faeiTemp.IvaValue, 
								faei.DiscountPercentage = faeiTemp.DiscountPercentage, faei.DiscountValue = faeiTemp.DiscountValue, faei.TotalValue = faeiTemp.TotalValue, 
								faei.RTFPercentage = IIF(@RetentionSource = 0, 0, faeiTemp.RTFPercentage), faei.RTFValue = IIF(@RetentionSource = 0, 0, faeiTemp.RTFValue),faei.Observation =faeiTemp.Observation
						from [FixedAsset].[FixedAssetEntryItem] faei
						join @FixedAssetEntryItem faeiTemp on faeiTemp.Id = faei.Id
						where faei.Id = @FixedAssetEntryItemId
					End
				END
			END

			--Se inserta FixedAssetEntryItemDetail
			DECLARE @FixedAssetEntryItemDetailRows INT = 1,
					@FixedAssetEntryItemDetailId INT = -1,
					@FixedAssetEntryItemDetailTempRows INT,
					@FixedAssetEntryItemDetailTempId INT,
					-----------------------------------------
					@Plate VARCHAR(50),
					@Serie VARCHAR(50)

			WHILE @FixedAssetEntryItemDetailRows > 0
			BEGIN
				SELECT TOP 1
					@FixedAssetEntryItemDetailId = faeid.Id,
					@FixedAssetEntryItemDetailTempRows = 1,
					@FixedAssetEntryItemDetailTempId = 0
				FROM @FixedAssetEntryItemDetail faeid
				WHERE faeid.Edited = 1
					AND faeid.Id > @FixedAssetEntryItemDetailId
				ORDER BY faeid.Id

				--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
				SET @FixedAssetEntryItemDetailRows = @@ROWCOUNT

				IF @FixedAssetEntryItemDetailRows = 0 
				BEGIN
					BREAK
				END

				WHILE @FixedAssetEntryItemDetailTempRows > 0
				BEGIN
					SELECT TOP 1
						@FixedAssetEntryItemDetailTempId = faeid.TempId,
						@Plate = faeid.Plate,
						@Serie = faeid.Serie
					FROM @FixedAssetEntryItemDetail faeid
					WHERE faeid.Edited = 1
						AND faeid.Id = @FixedAssetEntryItemDetailId
						AND faeid.TempId > @FixedAssetEntryItemDetailTempId
					ORDER BY faeid.TempId

					--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
					SET @FixedAssetEntryItemDetailTempRows = @@ROWCOUNT
					IF @FixedAssetEntryItemDetailTempRows = 0 
					BEGIN
						BREAK
					END

					if @FixedAssetEntryItemDetailId = 0 --Si se esta insertando el registro
					Begin
						DECLARE @Sum INT = 1
						DECLARE @Number VARCHAR(50)
						DECLARE @PositionEnd INT

						if @Plate = 'INDP' + cast(year(GetDate()) AS VARCHAR(4)) + '#'
						Begin
							if (select COUNT(*) from FixedAsset.FixedAssetEntryItemDetail where Plate like 'INDP' + cast(year(GetDate()) AS VARCHAR(4)) + '#%') > 0
							Begin
								select top 1 @PositionEnd = LEN(Plate) from FixedAsset.FixedAssetEntryItemDetail where Plate like 'INDP' + cast(year(GetDate()) AS VARCHAR(4)) + '#%' order by Id desc 
								select top 1 @Number = SUBSTRING(Plate,10,@PositionEnd) from FixedAsset.FixedAssetEntryItemDetail where Plate like 'INDP' + cast(year(GetDate()) AS VARCHAR(4)) + '#%' order by Id desc 
								set @Sum = @Sum + CAST(@Number AS INT)
							End
							set @Plate = CONCAT(@Plate,cast(@Sum AS VARCHAR(6)))
						End

						set @Sum = 1

						if @Serie = 'INDS' + cast(year(GetDate()) AS VARCHAR(4)) + '#'
						Begin
							if (select COUNT(*) from FixedAsset.FixedAssetEntryItemDetail where Serie like 'INDS' + cast(year(GetDate()) AS VARCHAR(4)) + '#%') > 0
							Begin
								select top 1 @PositionEnd = LEN(Serie) from FixedAsset.FixedAssetEntryItemDetail where Serie like 'INDS' + cast(year(GetDate()) AS VARCHAR(4)) + '#%' order by Id desc 
								select top 1 @Number = SUBSTRING(Serie,10,@PositionEnd) from FixedAsset.FixedAssetEntryItemDetail where Serie like 'INDS' + cast(year(GetDate()) AS VARCHAR(4)) + '#%' order by Id desc 
								set @Sum = @Sum + CAST(@Number AS INT)
							End
							set @Serie = CONCAT(@Serie,cast(@Sum AS VARCHAR(6)))
						End

						--Se inserta el registro
						insert into [FixedAsset].[FixedAssetEntryItemDetail]
						(
							[FixedAssetEntryItemId], [Plate], [Serie], [ReponsibleId], [LocationId], [AdquisitionDate], [Depreciate], 
							[HandlesWarranty], [WarrantyExpirationDate], [StatusAssetId],[ValidSmallerAmount], [Amortize]
						)
						select 
							FixedAssetEntryItemId, @Plate, @Serie, ReponsibleId, LocationId, AdquisitionDate, Depreciate, 
							HandlesWarranty, WarrantyExpirationDate, StatusAssetId,ValidSmallerAmount, Amortize
						from @FixedAssetEntryItemDetail 
						where Id = 0 and TempId = @FixedAssetEntryItemDetailTempId

						--Se actualiza el id en la tabla temporal @FixedAssetEntryItemDetail
						update @FixedAssetEntryItemDetail set Id = SCOPE_IDENTITY(), Edited = 0 where Id = 0 and TempId = @FixedAssetEntryItemDetailTempId

						--Se actualiza el id de la relacion en la tabla temporal @FixedAssetEntryItemDetailBook
						update @FixedAssetEntryItemDetailBook set FixedAssetEntryItemDetailId = SCOPE_IDENTITY() where FixedAssetEntryItemDetailId = 0 AND ParentId = @FixedAssetEntryItemDetailTempId

						--Se actualiza el id de la relacion en la tabla temporal @FixedAssetEntryItemDetailPart
						update @FixedAssetEntryItemDetailPart set FixedAssetEntryItemDetailId = SCOPE_IDENTITY() where FixedAssetEntryItemDetailId = 0 AND ParentId = @FixedAssetEntryItemDetailTempId
					End
					Else --Si se esta actualizando
					Begin
						update faeid 
							set faeid.FixedAssetEntryItemId = faeidTemp.FixedAssetEntryItemId, faeid.Plate = faeidTemp.Plate, 
								faeid.Serie = faeidTemp.Serie, faeid.ReponsibleId = faeidTemp.ReponsibleId, faeid.LocationId = faeidTemp.LocationId, 
								faeid.AdquisitionDate = faeidTemp.AdquisitionDate, faeid.Depreciate = faeidTemp.Depreciate, 
								faeid.HandlesWarranty = faeidTemp.HandlesWarranty, faeid.WarrantyExpirationDate = faeidTemp.WarrantyExpirationDate, 
								faeid.StatusAssetId = faeidTemp.StatusAssetId,faeid.ValidSmallerAmount = faeidTemp.ValidSmallerAmount, faeid.Amortize = faeidTemp.Amortize
						from [FixedAsset].[FixedAssetEntryItemDetail] faeid
						join @FixedAssetEntryItemDetail faeidTemp on faeidTemp.Id = faeid.Id
						where faeid.Id = @FixedAssetEntryItemDetailId
					End
				END
			END

			--Se actualizan los libros del detalle(FixedAssetEntryItemDetailBook) si hay
			update faeidb 
				set faeidb.FixedAssetEntryItemDetailId = faeidbTemp.FixedAssetEntryItemDetailId, 
					faeidb.LegalBookId = faeidbTemp.LegalBookId, 
					faeidb.LifeTime = faeidbTemp.LifeTime, 
					faeidb.UnitLifeTime = faeidbTemp.UnitLifeTime, 
					faeidb.DepreciationType = faeidbTemp.DepreciationType, 
					faeidb.TotalProductionUnit = faeidbTemp.TotalProductionUnit, 
					faeidb.PercentageRescue = faeidbTemp.PercentageRescue,
					faeidb.HistoricalValue = ISNULL([Common].[CurrencyConverterByModule](faei.UnitValue + IIF(ISNULL(sfalb.IvaCost, 0) = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0),@CurrencyId,lb.OfficialCurrencyId, NULL, NULL, TRY_CONVERT(DATE,@EntryDate)),faei.UnitValue + IIF(ISNULL(sfalb.IvaCost, 0) = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)),
					faeidb.DaysPendingDepreciate = FixedAsset.fnCalculateDaysPendingDepreciate
												   (
													   faeid.AdquisitionDate,
													   faeidbTemp.LifeTime, 
													   faeidbTemp.UnitLifeTime, 
													   ISNULL(faeid.ValidSmallerAmount, 0), 
													   (faei.UnitValue + IIF(ISNULL(sfalb.IvaCost, 0) = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)),
													   @LowBidAmount,
													   @TopMinorValue,
													   @Depreciation30Days
												   )
			FROM [FixedAsset].[FixedAssetEntryItem] faei
			JOIN [FixedAsset].[FixedAssetEntryItemDetail] faeid ON faei.Id = faeid.FixedAssetEntryItemId			
			JOIN @FixedAssetEntryItemDetailBook faeidbTemp on faeid.Id = faeidbTemp.FixedAssetEntryItemDetailId
			JOIN [FixedAsset].[FixedAssetEntryItemDetailBook] faeidb ON faeidb.Id = faeidbTemp.Id
			JOIN GeneralLedger.LegalBook lb ON lb.Id = faeidbTemp.LegalBookId
			LEFT JOIN FixedAsset.SettingFixedAssetByLegalBook sfalb ON sfalb.SettingFixedAssetId = @SettingFixedAssetId AND sfalb.LegalBookId = faeidbTemp.LegalBookId
			where faeidbTemp.Id > 0

			--Se insertan los libros del detalle(FixedAssetEntryItemDetailBook) si hay
			insert into [FixedAsset].[FixedAssetEntryItemDetailBook] 
			(
				[FixedAssetEntryItemDetailId], [LegalBookId], [LifeTime], [UnitLifeTime], 
				[DepreciationType], [TotalProductionUnit], [PercentageRescue],
				HistoricalValue, DaysPendingDepreciate
			)
			select 
				faeidbTemp.FixedAssetEntryItemDetailId, faeidbTemp.LegalBookId, faeidbTemp.LifeTime, faeidbTemp.UnitLifeTime, 
				faeidbTemp.DepreciationType, faeidbTemp.TotalProductionUnit, faeidbTemp.PercentageRescue ,
				ISNULL([Common].[CurrencyConverterByModule](faei.UnitValue + IIF(ISNULL(sfalb.IvaCost, 0) = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0),@CurrencyId,lb.OfficialCurrencyId, NULL, NULL, TRY_CONVERT(DATE,@EntryDate)),faei.UnitValue + IIF(ISNULL(sfalb.IvaCost, 0) = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)),
				FixedAsset.fnCalculateDaysPendingDepreciate
				(
					faeid.AdquisitionDate,
					faeidbTemp.LifeTime, 
					faeidbTemp.UnitLifeTime, 
					ISNULL(faeid.ValidSmallerAmount, 0), 
					(faei.UnitValue + IIF(ISNULL(sfalb.IvaCost, 0) = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)),
					@LowBidAmount,
					@TopMinorValue,
					@Depreciation30Days
				)
			FROM [FixedAsset].[FixedAssetEntryItem] faei
			JOIN [FixedAsset].[FixedAssetEntryItemDetail] faeid ON faei.Id = faeid.FixedAssetEntryItemId
			JOIN @FixedAssetEntryItemDetailBook faeidbTemp on faeid.Id = faeidbTemp.FixedAssetEntryItemDetailId
			JOIN GeneralLedger.LegalBook lb ON lb.Id = faeidbTemp.LegalBookId
			LEFT JOIN FixedAsset.SettingFixedAssetByLegalBook sfalb ON sfalb.SettingFixedAssetId = @SettingFixedAssetId AND sfalb.LegalBookId = faeidbTemp.LegalBookId
			where faeidbTemp.Id = 0

			--Se inserta FixedAssetEntryItemDetailPart
			DECLARE @FixedAssetEntryItemDetailPartRows INT = 1,
					@FixedAssetEntryItemDetailPartId INT = -1,
					@FixedAssetEntryItemDetailPartTempRows INT,
					@FixedAssetEntryItemDetailPartTempId INT
			WHILE @FixedAssetEntryItemDetailPartRows > 0
			BEGIN
				SELECT TOP 1
					@FixedAssetEntryItemDetailPartId = faeidp.Id,
					@FixedAssetEntryItemDetailPartTempRows = 1,
					@FixedAssetEntryItemDetailPartTempId = 0
				FROM @FixedAssetEntryItemDetailPart faeidp
				WHERE faeidp.Edited = 1
					AND faeidp.Id > @FixedAssetEntryItemDetailPartId
				ORDER BY faeidp.Id

				--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
				SET @FixedAssetEntryItemDetailPartRows = @@ROWCOUNT
				IF @FixedAssetEntryItemDetailPartRows = 0 
				BEGIN
					BREAK
				END

				WHILE @FixedAssetEntryItemDetailPartTempRows > 0
				BEGIN
					SELECT TOP 1
						@FixedAssetEntryItemDetailPartTempId = faeidp.TempId
					FROM @FixedAssetEntryItemDetailPart faeidp
					WHERE faeidp.Edited = 1
						AND faeidp.Id = @FixedAssetEntryItemDetailPartId
						AND faeidp.TempId > @FixedAssetEntryItemDetailPartTempId
					ORDER BY faeidp.TempId

					--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
					SET @FixedAssetEntryItemDetailPartTempRows = @@ROWCOUNT
					IF @FixedAssetEntryItemDetailPartTempRows = 0 
					BEGIN
						BREAK
					END

					if @FixedAssetEntryItemDetailPartId = 0 --Si se esta insertando el registro
					Begin
						--Se inserta el registro
						insert into [FixedAsset].[FixedAssetEntryItemDetailPart]
						(
							[FixedAssetEntryItemDetailId], [PartAccesoriesConsumiblesId], [DepreciatePart], [Value]
						)
						select FixedAssetEntryItemDetailId, PartAccesoriesConsumiblesId, DepreciatePart, Value
						from @FixedAssetEntryItemDetailPart 
						where Id = 0 and TempId = @FixedAssetEntryItemDetailPartTempId

						--Se actualiza el id en la tabla temporal @FixedAssetEntryItemDetailPart
						update @FixedAssetEntryItemDetailPart set Id = SCOPE_IDENTITY(), Edited = 0 where Id = 0 and TempId = @FixedAssetEntryItemDetailPartTempId

						--Se actualiza el id de la relacion en la tabla temporal @FixedAssetEntryItemDetailPartBook
						update @FixedAssetEntryItemDetailPartBook set FixedAssetEntryItemDetailPartId = SCOPE_IDENTITY() where FixedAssetEntryItemDetailPartId = 0 AND ParentId = @FixedAssetEntryItemDetailPartTempId
					End
					Else --Si se esta actualizando
					Begin
						update faeidp 
							set faeidp.FixedAssetEntryItemDetailId = faeidpTemp.FixedAssetEntryItemDetailId, 
								faeidp.PartAccesoriesConsumiblesId = faeidpTemp.PartAccesoriesConsumiblesId, faeidp.DepreciatePart = faeidpTemp.DepreciatePart, 
								faeidp.Value = faeidpTemp.Value
						from [FixedAsset].[FixedAssetEntryItemDetailPart] faeidp
						inner join @FixedAssetEntryItemDetailPart faeidpTemp on faeidpTemp.Id = faeidp.Id
						where faeidp.Id = @FixedAssetEntryItemDetailPartId
					End
				END
			END
			--Se actualizan los libros del detalle(FixedAssetEntryItemDetailPartBook) si hay
			update faeidpb 
			set faeidpb.FixedAssetEntryItemDetailPartId = faeidpbTemp.FixedAssetEntryItemDetailPartId, 
				faeidpb.LegalBookId = faeidpbTemp.LegalBookId, 
				faeidpb.LifeTime = faeidpbTemp.LifeTime, 
				faeidpb.UnitLifeTime = faeidpbTemp.UnitLifeTime, 
				faeidpb.DepreciationType = faeidpbTemp.DepreciationType, 
				faeidpb.TotalProductionUnit = faeidpbTemp.TotalProductionUnit, faeidpb.PercentageRescue = faeidpbTemp.PercentageRescue
			from [FixedAsset].[FixedAssetEntryItemDetailPartBook] faeidpb
			join @FixedAssetEntryItemDetailPartBook faeidpbTemp on faeidpbTemp.Id = faeidpb.Id
			where faeidpbTemp.Id > 0

			--Se insertan los libros de la parte(FixedAssetEntryItemDetailPartBook) si hay
			insert into [FixedAsset].[FixedAssetEntryItemDetailPartBook] 
			(
				[FixedAssetEntryItemDetailPartId], [LegalBookId], [LifeTime], 
				[UnitLifeTime], [DepreciationType], [TotalProductionUnit], [PercentageRescue]
			)
			select 
				FixedAssetEntryItemDetailPartId, LegalBookId, LifeTime, 
				UnitLifeTime, DepreciationType, TotalProductionUnit, PercentageRescue
			from @FixedAssetEntryItemDetailPartBook 
			where Id = 0

			/*************************************************************************************/
			
			IF @Status = 2
			BEGIN

				DECLARE @FixedAssetPhysicalAssetId INT

				--Si es importado de una orden de compra reducimos las cantidades disponibles y aumentando la cantidades legalizadas
				UPDATE fapoi SET fapoi.OutstandingQuantity = fapoi.OutstandingQuantity - faei.Quantity , fapoi.CancelledQuantity += faei.Quantity
				FROM FixedAsset.FixedAssetPurchaseOrderItem fapoi						
				JOIN
				(
					SELECT 
						faei.PurchaseOrderItemId,
						COUNT(1) Quantity
					FROM FixedAsset.FixedAssetEntryItem faei								
					JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
					WHERE faei.FixedAssetEntryId = @Id
						AND faei.RemissionSource = 2
					GROUP BY faei.PurchaseOrderItemId
				) faei ON fapoi.Id = faei.PurchaseOrderItemId

				--Si es importado de una remision reducimos las cantidades disponibles
				UPDATE farei SET farei.OutstandingQuantity = farei.OutstandingQuantity - faei.Quantity
				FROM FixedAsset.FixedAssetRemissionEntranceItem farei
				JOIN
				(
					SELECT 
						faei.RemissionEntranceItemId,
						COUNT(1) Quantity
					FROM FixedAsset.FixedAssetEntryItem faei								
					JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
					WHERE faei.FixedAssetEntryId = @Id
						AND faei.RemissionSource = 3
					GROUP BY faei.RemissionEntranceItemId
				) faei ON farei.Id = faei.RemissionEntranceItemId

				--Si es importado de una remisión debo actualizar el activo
				UPDATE fapa
					SET fapa.HistoricalValue = faei.UnitValue + IIF(@IvaCost = 1,ROUND(faei.IvaValue / faei.Quantity, 0), 0)
				FROM FixedAsset.FixedAssetEntryItem faei								
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
				WHERE faei.FixedAssetEntryId = @Id
					AND faei.RemissionSource = 3

				--Si es importado de una remisión debo actualizar el detalle del libro
				UPDATE fapadb
					SET
						fapadb.DaysPendingDepreciate = faeidb.DaysPendingDepreciate,
						fapadb.ResidualValue = faeidb.HistoricalValue,
						fapadb.HistoricalValue = faeidb.HistoricalValue
				FROM FixedAsset.FixedAssetEntryItem faei								
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetEntryItemDetailBook faeidb ON faeid.Id = faeidb.FixedAssetEntryItemDetailId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND faeidb.LegalBookId = fapadb.LegalBookId
				WHERE faei.FixedAssetEntryId = @Id
					AND faei.RemissionSource = 3
					
				--Si proviene de una orden de compra o de otro lugar creamos el activo
				INSERT INTO [FixedAsset].[FixedAssetPhysicalAsset] 
				(
					[ItemId], [Serie], [Plate], [LocationId], [ResponsibleId], [SupplierId], [TrademarkId], [Model], 
					[PolicyId], [HandlesWarranty], [WarrantyExpirationDate], [AdquisitionDate], [Depreciate], [Observation], [StatusAssetId], [Status], 
					[NumberContractLeasing], [InitialDateLeasing], [EndDateLeasing], [AdquisitionType], [AdquisitionTypeReal], 
					[MainAccountId], 
					[HistoricalValue], [FairValue], [ApplyMinimunAmount], [Amortize]
				)
				SELECT 
					faei.ItemId, faeid.Serie, faeid.Plate, faeid.LocationId, faeid.ReponsibleId, @SupplierId, faei.TrademarkId, faei.Model, 
					faei.PolicyId, faeid.HandlesWarranty, faeid.WarrantyExpirationDate, faeid.AdquisitionDate, faeid.Depreciate, faei.Observation, faeid.StatusAssetId, 1, 
					@NumberContractLeasing, @InitialDateLeasing, @EndDateLeasing, @AdquisitionType, @AdquisitionType, 
					CASE @AdquisitionType 
						WHEN 3 THEN faic.DebitLoanAccountId 
						WHEN 7 THEN faic.IncomeLeasingAccountId 
						WHEN 9 THEN ISNULL(faicat.MainAccountId, faic.IncomeAccountId) 
						ELSE faic.IncomeAccountId 
					END, 
					ISNULL([Common].[CurrencyConverterByModule](faei.UnitValue + IIF(@IvaCost = 1,ROUND(faei.IvaValue / faei.Quantity, 0), 0),@CurrencyId,@OfficialCurrencyId,NULL,NULL,TRY_CONVERT(DATE,@EntryDate)),faei.UnitValue + IIF(@IvaCost = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)), --[HistoricalValue]
					ISNULL([Common].[CurrencyConverterByModule](faei.UnitValue + IIF(@IvaCost = 1,ROUND(faei.IvaValue / faei.Quantity, 0), 0),@CurrencyId,@OfficialCurrencyId,NULL,NULL,TRY_CONVERT(DATE,@EntryDate)),faei.UnitValue + IIF(@IvaCost = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)), --[FairValue]
					0,
					faeid.Amortize
				FROM FixedAsset.FixedAssetEntryItem faei								
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetItem fai ON fai.Id = faei.ItemId
				JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = fai.ItemCatalogId
				left join FixedAsset.FixedAssetItemCatalogAdquisitionType faicat on faicat.ItemCatalogId = faic.Id and faicat.LegalBookId = @OfficialLegalBookId
				WHERE faei.FixedAssetEntryId = @Id
					AND faei.RemissionSource IN (1, 2)

				SET @FixedAssetPhysicalAssetId = SCOPE_IDENTITY()

				--Si proviene de una orden de compra o de otro lugar creamos el activo si deprecia
				INSERT INTO [FixedAsset].[FixedAssetPhysicalAssetDetailBook] 
				(
					[PhysicalAssetId],[LegalBookId],[LifeTime],[UnitLifeTime],[DepreciationType],[TotalProductionUnit],
					[PercentageRescue],[Valorization],[Devaluation],[AdjustedValue],[TransactionValue],[DepreciatedValue],[DepreciatedValuePart],
					[ResidualValue],[ResidualValuePart], [HistoricalValue],[DaysPendingDepreciate],[DepreciatedDays], 
					[ApplyMinimunAmount]
				)
				SELECT
					fapa.Id, faeidb.LegalBookId, faeidb.LifeTime, faeidb.UnitLifeTime, faeidb.DepreciationType, faeidb.TotalProductionUnit,
					faeidb.PercentageRescue, 0, 0, 0, 0, 0, 0,
					faeidb.HistoricalValue, 0, faeidb.HistoricalValue, faeidb.DaysPendingDepreciate, 0,
                    IIF(faeid.ValidSmallerAmount = 0, 0, IIF(faeidb.HistoricalValue <= ISNULL(@TopMinorValue, 0), 1, 0))
				FROM FixedAsset.FixedAssetEntryItem faei								
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
				JOIN FixedAsset.FixedAssetEntryItemDetailBook faeidb ON faeid.Id = faeidb.FixedAssetEntryItemDetailId
				JOIN GeneralLedger.LegalBook lb ON lb.Id = faeidb.LegalBookId
				LEFT JOIN FixedAsset.SettingFixedAssetByLegalBook sfalb ON sfalb.SettingFixedAssetId = @SettingFixedAssetId AND faeidb.LegalBookId = sfalb.LegalBookId				
				WHERE faei.FixedAssetEntryId = @Id
					AND (faeid.Depreciate = 1 OR faeid.Amortize = 1)
					AND faei.RemissionSource IN (1, 2)
				GROUP BY fapa.Id, faeidb.LegalBookId, faeidb.LifeTime, faeidb.UnitLifeTime, faeidb.DepreciationType, faeidb.TotalProductionUnit,
					faeidb.PercentageRescue, faeidb.HistoricalValue, faeidb.HistoricalValue, faeidb.DaysPendingDepreciate, faeid.ValidSmallerAmount
                    

				--Se inserta en el kardex(KardexItem)
				INSERT INTO [FixedAsset].[FixedAssetKardexItem]
				(
					[MovementType],[PhysicalAssetId],[ResponsibleId],[LocationId],[DocumentDate],[EntityId],[EntityCode],[EntityName],[ImportedEntityId],
					[ImportedEntityCode],[ImportedEntityName],[AffectPhysical],[CreationUser],[CreationDate]
				)
				select 
					1, fapa.Id, faeid.ReponsibleId, faeid.LocationId, @EntryDate, @Id, @Code, '', null, 
					null, null, 0, @CodeUser, [Common].[GETDATE]()
				FROM FixedAsset.FixedAssetEntryItem faei								
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
				WHERE faei.FixedAssetEntryId = @Id
					AND faei.RemissionSource IN (1, 2)

				--Se inserta FixedAssetEntryItemDetailPart
				SELECT @FixedAssetEntryItemDetailPartRows = 1,
					   @FixedAssetEntryItemDetailPartId = 0

				WHILE @FixedAssetEntryItemDetailPartRows > 0
				BEGIN
					SELECT TOP 1
						--@FixedAssetEntryItemDetailPartTempId = faeidp.Id
						@FixedAssetEntryItemDetailPartId =  faeidp.Id
					FROM FixedAsset.FixedAssetEntryItem faei								
					JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
					JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
					JOIN FixedAsset.FixedAssetEntryItemDetailPart faeidp ON faeid.Id = faeidp.FixedAssetEntryItemDetailId
					WHERE faei.FixedAssetEntryId = @Id
						AND faei.RemissionSource IN (1, 2)
						AND faeidp.Id > @FixedAssetEntryItemDetailPartId
					ORDER BY faeidp.Id

					--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
					SET @FixedAssetEntryItemDetailPartRows = @@ROWCOUNT

					IF @FixedAssetEntryItemDetailPartRows = 0 
					BEGIN
						BREAK
					END

					INSERT INTO [FixedAsset].[FixedAssetPhysicalAssetParts] 
					(
						[PhysicalAssetId],[PartAccesoriesConsumiblesId],[DepreciatePart],[HistoricalValue]
					)
					select fapa.Id, faeidp.PartAccesoriesConsumiblesId, faeidp.DepreciatePart, faeidp.Value 
					FROM FixedAsset.FixedAssetEntryItem faei								
					JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON faei.Id = faeid.FixedAssetEntryItemId
					JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
					JOIN FixedAsset.FixedAssetEntryItemDetailPart faeidp ON faeid.Id = faeidp.FixedAssetEntryItemDetailId
					WHERE faei.FixedAssetEntryId = @Id
						AND faei.RemissionSource IN (1, 2)
						AND faeidp.Id = @FixedAssetEntryItemDetailPartId

					--Se declara el id del physicalAssetParts
					DECLARE @PhysicalAssetPartsId INT = SCOPE_IDENTITY()

					INSERT INTO [FixedAsset].[FixedAssetPhysicalAssetPartsDetailBook] 
					(
						[PhysicalAssetPartsId],[LegalBookId],[LifeTime],[UnitLifeTime],[ValorizationDays],[DaysPendingDepreciate],
						[DepreciatedDays],[DepreciationType],[TotalProductionUnit],[PercentageRescue],[Valorization],[Devaluation],[AdjustedValue],
						[TransactionValue],[DepreciatedValue],[ResidualValue]
					)
					select @PhysicalAssetPartsId, faeidpb.LegalBookId, faeidpb.LifeTime, faeidpb.UnitLifeTime, 0, 0, 
						0, faeidpb.DepreciationType, faeidpb.TotalProductionUnit, faeidpb.PercentageRescue, 0, 0, 0, 
						0, 0, 0
					from FixedAsset.FixedAssetEntryItemDetailPartBook faeidpb
					where faeidpb.FixedAssetEntryItemDetailPartId = @FixedAssetEntryItemDetailPartId
				END

				--Actualizamos ultimo costo del articulo al que ingresaron nuevos activos
				UPDATE fai
					SET fai.LastCostItem = fapa.HistoricalValue
				FROM FixedAsset.FixedAssetEntryItem faei
				JOIN FixedAsset.FixedAssetItem fai ON fai.Id = faei.ItemId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.ItemId = fai.Id
				WHERE faei.FixedAssetEntryId = @Id AND fapa.Id = @FixedAssetPhysicalAssetId
			END

			/*************************************************************************************/			

			select 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code AS Code, @Id AS Id		
		END
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra o actualiza una entrada (adquisición) de activos fijos en el sistema. Procesa un XML con la cabecera del documento de compra o incorporación —proveedor, factura, valores, descuentos, impuestos, retenciones, fletes e IVA— junto con el detalle de cada ítem, sus placas, series, responsables, ubicaciones y libros contables (libros legales de depreciación). Consulta la configuración de activos fijos para determinar si el IVA forma parte del costo, si aplica depreciación en 30 días, el monto mínimo de cuantía menor y la moneda base; además valida el porcentaje de IVA vigente contra el libro legal activo. Permite crear nuevos registros o actualizar los existentes, y gestiona la eliminación de sub-items de detalle enviados en listas XML separadas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetEntry';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetEntry';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea/actualiza/anula) una entrada de activos fijos con su jerarquía completa (ítems, detalles, libros, partes y libros de parte), aplicando validaciones y, al confirmar, materializa los activos físicos y su kárdex.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetEntry';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro no debe estar en estado distinto de 1 (En elaboración); si Status<>1 se rechaza con mensaje ''El registro se encuentra en estado: Confirmado/Anulado''.; Si el tipo de adquisición es 7 (Leasing financiero), el número de contrato leasing no puede existir en otra entrada distinta.; Debe existir al menos un FixedAssetEntryItem en el XML; de lo contrario se rechaza con ''El documento no tiene detalles.''.; Las placas dentro de la misma entrada no pueden estar duplicadas.; Cuando RemissionSource ∈ (1,2), las placas no deben existir previamente en FixedAssetPhysicalAsset.; Cuando RemissionSource=2, la cantidad por PurchaseOrderItem no puede exceder OutstandingQuantity de la orden de compra.; Cuando RemissionSource=3, la cantidad por RemissionEntranceItem no puede exceder OutstandingQuantity de la remisión de entrada.; La cantidad declarada en cada ítem debe coincidir con el conteo de sus detalles (Plate).; Debe existir secuencia configurada (FixedAssetSequence/Detail) para el formulario 1116 con scope ''O'' o por unidad operativa cuando se crea un nuevo registro sin Code.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetEntry';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetEntry';
-- GO
