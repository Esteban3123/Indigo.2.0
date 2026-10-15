-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 22/12/2016
-- Description:	Procedimiento que se encarga de guardar en las tablas de FixedAssetPhysicalAsset cuando se va a confirmar el saldo inicial
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ConfirmFixedAssetInitialBalance] 
	@FixedAssetInitialBalanceId as int
AS
BEGIN
	
	--begin transaction
	begin try

		if (select count(*) from FixedAsset.FixedAssetInitialBalanceItem where FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId group by Plate having count(*) > 1) > 1 begin
			declare @MessagePlateDuplicate varchar(max)
			select @MessagePlateDuplicate=stuff((select N'; la placa ' + Plate + ' se encuentra duplicada'
			from FixedAsset.FixedAssetInitialBalanceItem 
			where FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId 
			group by Plate having count(*) > 1
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			select 999 as CodeMessage, @MessagePlateDuplicate as Message
			return
		end

		--Parámetros definidos para determinar si maneja iva al costo en el libro contable oficial y el valor de menor cuantía establecido
		DECLARE @OfficialLegalBookId INT,
				@SettingFixedAssetId INT,
				@CurrentYear INT,
				@LowBidAmount NUMERIC(18,0) = 0,
				@TopMinorValue NUMERIC(18,0) = 0

		--Id del libro oficial, esta variable es utilizada para asignar la cuenta cuando es renting financiero
		select @OfficialLegalBookId = Id from GeneralLedger.LegalBook where OfficialBook = 1

		SELECT TOP 1 @SettingFixedAssetId = sfa.Id, @CurrentYear = YEAR(sfa.ProcessDate), @LowBidAmount = sfa.LowBidAmount, @TopMinorValue = sfa.TopMinorValue
		FROM FixedAsset.SettingFixedAsset sfa
		ORDER BY sfa.ProcessDate DESC

		--Se inserta en la tabla de FixedAssetPhysicalAsset
		INSERT INTO [FixedAsset].[FixedAssetPhysicalAsset]
			(
				[ItemId], [Serie], [Plate], [LocationId], [ResponsibleId], [SupplierId], [TrademarkId], [Model],
				[PolicyId], [HandlesWarranty], [WarrantyExpirationDate], [AdquisitionDate], [Depreciate], [Observation], [StatusAssetId], [Status], 
				[AdquisitionType], [AdquisitionTypeReal],
				[MainAccountId], 
				[HistoricalValue], [FairValue],
				[ApplyMinimunAmount],[Amortize]
			)
			SELECT 
				ItemId, bi.Serie, bi.Plate, bi.LocationId, bi.ResponsibleId, bi.SupplierId, bi.TrademarkId, bi.Model, 
				bi.PolicyId, bi.HandlesWarranty, bi.WarrantyExpirationDate, bi.AdquisitionDate, bi.Depreciate, '', bi.StatusAssetId, 1, 
				bi.AdquisitionType, bi.AdquisitionType, 
				CASE bi.AdquisitionType 
					WHEN 3 THEN c.DebitLoanAccountId 
					WHEN 7 THEN c.IncomeLeasingAccountId 
					WHEN 9 THEN ISNULL(faicat.MainAccountId, c.IncomeAccountId) 
					ELSE c.IncomeAccountId 
				END,
				bi.HistoricalValue, bi.FairValue,
				CASE 
				    WHEN bi.ValidMinorAmount IS NULL OR bi.ValidMinorAmount = 0 THEN 0
				    WHEN bi.ValidMinorAmount = 1 AND 
				         YEAR(bi.AdquisitionDate) = @CurrentYear AND 
				         bi.Depreciate = 1 AND 
				         (bi.HistoricalValue) <= ISNULL(@TopMinorValue, 0) THEN 1
				    ELSE 0
				END,
				bi.Amortize
			FROM FixedAsset.FixedAssetInitialBalanceItem bi
			JOIN FixedAsset.FixedAssetItem i on bi.ItemId = i.Id
			JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
			LEFT JOIN FixedAsset.FixedAssetItemCatalogAdquisitionType faicat ON faicat.ItemCatalogId = c.Id and faicat.LegalBookId = @OfficialLegalBookId
			WHERE bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId

		--Se inserta en la tabla FixedAssetPhysicalAssetDetailBook
		INSERT INTO [FixedAsset].[FixedAssetPhysicalAssetDetailBook]
			(
				[PhysicalAssetId],[LegalBookId],[LifeTime],[UnitLifeTime],[DepreciationType],[TotalProductionUnit],
				[PercentageRescue],[Valorization],[Devaluation],[AdjustedValue],[TransactionValue],[DepreciatedValue],[DepreciatedValuePart],
				[ResidualValue],[ResidualValuePart],[HistoricalValue],
				[DaysPendingDepreciate],[DepreciatedDays],[ApplyMinimunAmount]
			)
			SELECT 
				pa.Id, bidb.LegalBookId, bidb.[LifeTime], bidb.UnitLifeTime, bidb.DepreciationType, bidb.TotalProductionUnit, 
				bidb.PercentageRescue, 0, 0, 0, 0, bidb.DepreciatedValue, bidb.DepreciatedValuePart, 
				bidb.ResidualValue, bidb.ResidualValuePart, bidb.HistoricalValue, 
				bidb.DaysPendingDepreciate, bidb.DepreciatedDays,
				CASE
					WHEN bi.ValidMinorAmount IS NULL OR bi.ValidMinorAmount = 0 THEN 0
					WHEN bi.ValidMinorAmount = 1 AND
						bidb.HistoricalValue <= ISNULL(@TopMinorValue, 0) THEN 1
					ELSE 0
				END
			FROM FixedAsset.FixedAssetInitialBalanceItem bi
			JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Plate = bi.Plate
			JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook bidb on bidb.FixedAssetInitialBalanceItemId = bi.Id
			LEFT JOIN FixedAsset.SettingFixedAssetByLegalBook sfalb ON sfalb.SettingFixedAssetId = @SettingFixedAssetId AND bidb.LegalBookId = sfalb.LegalBookId
			WHERE bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId 
				AND bi.Depreciate = 1
				AND pa.AdquisitionType <> 8 AND pa.AdquisitionType <> 10

		--Se inserta en la tabla FixedAssetPhysicalAssetParts
		insert into [FixedAsset].[FixedAssetPhysicalAssetParts]
		(PhysicalAssetId, PartAccesoriesConsumiblesId, DepreciatePart, HistoricalValue, HasOutput, OutputDate)
		select pa.Id, bidb.PartAccesoriesConsumablesId, bidb.DepreciatePart, bidb.HistoricalValue, 0, null
		from FixedAsset.FixedAssetInitialBalanceItem bi
		inner join FixedAsset.FixedAssetPhysicalAsset pa on pa.Plate = bi.Plate
		inner join FixedAsset.FixedAssetInitialBalanceItemParts bidb on bidb.FixedAssetInitialBalanceItemId = bi.Id
		where bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId

		--Se inserta en la tabla FixedAssetPhysicalAssetPartsDetailBook
		insert into [FixedAsset].[FixedAssetPhysicalAssetPartsDetailBook]
		(PhysicalAssetPartsId, LegalBookId, LifeTime, UnitLifeTime, ValorizationDays, DaysPendingDepreciate, DepreciatedDays, DepreciationType, TotalProductionUnit, PercentageRescue, Valorization,
		Devaluation, AdjustedValue, TransactionValue, DepreciatedValue, InflationAdjustmentValue, ResidualValue)
		select par.Id, boo.LegalBookId, boo.LifeTime, boo.UnitLifeTime, 0, boo.DaysPendingDepreciate, boo.DepreciatedDays, boo.DepreciationType, boo.TotalProductionUnit, boo.PercentageRescue,
		0, 0, 0, 0, boo.DepreciatedValue, 0, boo.ResidualValue
		from FixedAsset.FixedAssetInitialBalanceItem bi
		inner join FixedAsset.FixedAssetPhysicalAsset pa on pa.Plate = bi.Plate
		inner join FixedAsset.FixedAssetPhysicalAssetParts par on par.PhysicalAssetId = pa.Id
		inner join FixedAsset.FixedAssetInitialBalanceItemParts bidb on bidb.FixedAssetInitialBalanceItemId = bi.Id
		inner join FixedAsset.FixedAssetInitialBalanceItemPartsDetailBook boo on boo.FixedAssetInitialBalanceItemPartsId = bidb.Id
		where bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId AND pa.AdquisitionType <> 8 AND pa.AdquisitionType <> 10

		--commit transaction
		select 0 as CodeMessage, 'Se confirmó correctamente' as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END