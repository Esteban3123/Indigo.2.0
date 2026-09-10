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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de subcuentas contables para activos fijos físicos, mostrando por cada bien: la cuenta contable del activo y su cuenta de depreciación (con soporte de homologación de cuentas para libros no oficiales como NIIF o libros alternativos), el valor histórico o valor razonable según el tipo de libro contable, la valorización, desvalorización, valor depreciado y valor residual. Permite filtrar por rango de número de cuenta contable, código de ítem de activo fijo y tipo de ítem. Compone información de los activos físicos (FixedAssetPhysicalAsset), su clasificación por ítem y tipo (FixedAssetItem, FixedAssetItemType), el catálogo de cuentas de depreciación según el tipo de adquisición (compra, leasing, renting), y el detalle contable por libro legal (FixedAssetPhysicalAssetDetailBook), resolviendo la homologación de cuentas cuando el libro seleccionado no es el oficial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListSubAccount';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListSubAccount';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de activos fijos físicos vigentes con su sub-cuenta contable, cuenta de depreciación y valores (histórico, valorización, desvalorización, depreciado y residual) para un libro contable específico.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro contable indicado debe existir en GeneralLedger.LegalBook (se leen sus atributos OfficialBook y TypeBook).; Los activos deben tener asignada cuenta principal y catálogo con la cuenta contable correspondiente al tipo de adquisición.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se incluyen activos dados de baja (HasOutput = 1).; Si el libro no es oficial, las cuentas mostradas son siempre las homologadas al libro consultado.; La cuenta de depreciación depende exclusivamente del tipo de adquisición del activo según el CASE.; Valorization, Devaluation y DepreciatedValue se reportan como 0 cuando no hay detalle por libro.; Los valores monetarios dependen del TypeBook del libro: TypeBook=1 usa HistoricalValue, otros usan FairValue.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico; Placa de activo; Cuenta contable principal; Cuenta de depreciación; Homologación de cuentas; Libro contable (oficial/no oficial); Valor histórico; Valor razonable (FairValue); Valorización; Desvalorización; Valor depreciado; Valor residual; Tipo de adquisición (leasing, renting, préstamo); Catálogo de ítems de activo fijo', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.FixedAssetPhysicalAsset: Sólo retorna activos físicos donde HasOutput = 0 (sin baja/salida) y cuya cuenta (homologada o original), código de ítem y código de tipo estén dentro de los rangos parametrizados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AdquisitionType = 3 (préstamo/leasing) → La cuenta de depreciación se toma de FixedAssetItemCatalog.LoanLeasingAccountId else Se evalúan los demás casos del CASE; si AdquisitionType = 7 (leasing depreciación) → La cuenta se toma de DepreciationLeasingAccountId; si AdquisitionType = 9 (renting financiero) → La cuenta se toma de FinancialRentingAccountId; si AdquisitionType distinto de 3, 7, 9 → La cuenta se toma de DepreciationAccountId (cuenta de depreciación estándar); si TypeBook = 1 y no existe detalle por libro → Se reporta HistoricalValue y ResidualValue como HistoricalValue del activo else Se reporta como FairValue del activo; si OfficialBook <> 1 (libro no oficial) → Se aplica homologación de cuentas vía GeneralLedger.HomologationAccount, mostrando la cuenta homologada del libro solicitado else Se muestra la cuenta original (oficial) del activo y de su depreciación; si Existe registro en FixedAssetPhysicalAssetDetailBook para el libro → Se usan sus valores (HistoricalValue, Valorization, Devaluation, DepreciatedValue, ResidualValue) else Se aplican valores por defecto del activo y ceros en valorización/desvalorización/depreciado', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetItemCatalog; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount; FixedAsset.FixedAssetPhysicalAssetDetailBook', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListSubAccount';
-- GO
