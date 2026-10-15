CREATE PROCEDURE [FixedAsset].[SP_ReportListSubAccount]
	@NumberAccountStart AS varchar(20),
	@NumberAccountEnd AS varchar(20),
	@CodeFixedAssetItemStart AS varchar(20),
	@CodeFixedAssetItemEnd AS varchar(20),
	@CodeFixedAssetItemTypeStart AS varchar(20),
	@CodeFixedAssetItemTypeEnd AS varchar(20),
	@LegalBook bit
AS
BEGIN 

	DECLARE @OfficialBook BIT,
			@TypeBook TINYINT

	SELECT	@OfficialBook = OfficialBook,
			@TypeBook = TypeBook
	FROM GeneralLedger.LegalBook 
	WHERE Id = @LegalBook

	-----------------------------------------  RESULTADOS -----------------------------------------

	SELECT	fapa.Id, 
			ISNULL(mah.Number, ma.Number) AS NumberAccount, 
			ISNULL(mah.Name, ma.Name) AS NameAccount, 
			fapa.Plate, 
			fai.Code,
			fai.Description, 
			fait.Code AS CodeType, 
			fait.Name AS NameType , 
			ISNULL(fapadb.HistoricalValue, IIF(@TypeBook = 1, fapa.HistoricalValue, fapa.FairValue)) HistoricalValue, 
			ISNULL(fapadb.Valorization, 0) AS Valorization, 
			ISNULL(fapadb.Devaluation, 0) AS Devaluation, 
			ISNULL(madh.Number, mad.Number) AS DeprecationAccount,
			ISNULL(madh.Name, mad.Name) AS DeprecationAccountName,
			ISNULL(fapadb.DepreciatedValue,0) AS DepreciatedValue, 
			ISNULL(fapadb.ResidualValue, IIF(@TypeBook = 1, fapa.HistoricalValue, fapa.FairValue)) AS ResidualValue
	FROM FixedAsset.FixedAssetPhysicalAsset fapa
	JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
	---------------------------------------------------------------------------
	JOIN GeneralLedger.MainAccounts ma ON fapa.MainAccountId = ma.Id
	JOIN GeneralLedger.MainAccounts mad ON mad.Id = CASE fapa.AdquisitionType
														WHEN 3 THEN faic.LoanLeasingAccountId
														WHEN 7 THEN faic.DepreciationLeasingAccountId
														WHEN 9 THEN faic.FinancialRentingAccountId
														ELSE faic.DepreciationAccountId
													END
	---------------------------------  HOMOLOGACION CUENTA ACTIVO ---------------------------------
	LEFT JOIN GeneralLedger.HomologationAccount ha ON @OfficialBook <> 1 AND ma.Id = ha.OfficialMainAccountId
	LEFT JOIN GeneralLedger.MainAccounts mah ON ha.MainAccountId = mah.Id AND mah.LegalBookId = @LegalBook
	--------------------------- HOMOLOGACION CUENTA DEPRECIACION ACTIVO ---------------------------
	LEFT JOIN GeneralLedger.HomologationAccount had ON @OfficialBook <> 1 AND mad.Id = had.OfficialMainAccountId
	LEFT JOIN GeneralLedger.MainAccounts madh ON had.MainAccountId = madh.Id AND madh.LegalBookId = @LegalBook
	---------------------------------------------------------------------------	
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId  AND fapadb.LegalBookId = @LegalBook
	WHERE fapa.HasOutput = 0 
		AND ISNULL(mah.Number, ma.Number) BETWEEN @NumberAccountStart AND @NumberAccountEnd 
		AND fai.Code BETWEEN @CodeFixedAssetItemStart AND @CodeFixedAssetItemEnd 
		AND fait.Code BETWEEN @CodeFixedAssetItemTypeStart AND @CodeFixedAssetItemTypeEnd 
END