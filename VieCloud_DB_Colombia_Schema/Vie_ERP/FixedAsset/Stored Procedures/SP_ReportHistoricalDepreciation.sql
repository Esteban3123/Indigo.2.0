-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-17
-- Description:	Procedimiento para el reporte histórico de depreciación
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportHistoricalDepreciation]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --
			@OperatingUnitId INT,
			@Year INT,
			@Month INT,
			@ClosingDate DATE,
			@LegalBookId INT,
			@OfficialLegalBookId INT,
			@IncludeDepreciated BIT,
			@AdquisitionType VARCHAR(MAX),
			-- FILTROS --
			@InitialMainAccount VARCHAR(MAX),
			@FinalMainAccount VARCHAR(MAX),
			@InitialDepreciationAccount VARCHAR(MAX),
			@FinalDepreciationAccount VARCHAR(MAX),
			@InitialCatalog VARCHAR(MAX),
			@FinalCatalog VARCHAR(MAX),
			@InitialItem VARCHAR(MAX),
			@FinalItem VARCHAR(MAX),
			@InitialType VARCHAR(MAX),
			@FinalType VARCHAR(MAX),
			-- FILTROS --
			@IvaCost BIT = 0

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Year = t.x.value('Year[1]','int'),
			@Month = t.x.value('Month[1]','int'),
			@LegalBookId = t.x.value('LegalBookId[1]','int'),
			@IncludeDepreciated = t.x.value('IncludeDepreciated[1]','bit'),
			@AdquisitionType = t.x.value('AdquisitionType[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
			@InitialMainAccount = t.x.value('InitialMainAccount[1]','varchar(max)'),
			@FinalMainAccount = t.x.value('FinalMainAccount[1]','varchar(max)'),
			@InitialDepreciationAccount = t.x.value('InitialDepreciationAccount[1]','varchar(max)'),
			@FinalDepreciationAccount = t.x.value('FinalDepreciationAccount[1]','varchar(max)'),
			@InitialCatalog = t.x.value('InitialCatalog[1]','varchar(max)'),
			@FinalCatalog = t.x.value('FinalCatalog[1]','varchar(max)'),
			@InitialItem = t.x.value('InitialItem[1]','varchar(max)'),
			@FinalItem = t.x.value('FinalItem[1]','varchar(max)'),
			@InitialType = t.x.value('InitialType[1]','varchar(max)'),
			@FinalType = t.x.value('FinalType[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		SELECT @ClosingDate = '01/' + RIGHT('0' + CAST(@Month AS VARCHAR(20)), 2) + '/' + CAST(@Year AS VARCHAR(20)),
			   @InitialMainAccount = IIF(ISNULL(@InitialMainAccount, '') = '', '0', @InitialMainAccount),
			   @FinalMainAccount = IIF(ISNULL(@FinalMainAccount, '') = '', 'ZZZZZZZZZZ', @FinalMainAccount),
			   @InitialDepreciationAccount = IIF(ISNULL(@InitialDepreciationAccount, '') = '', '0', @InitialDepreciationAccount),
			   @FinalDepreciationAccount = IIF(ISNULL(@FinalDepreciationAccount, '') = '', 'ZZZZZZZZZZ', @FinalDepreciationAccount),
			   @InitialCatalog = IIF(ISNULL(@InitialCatalog, '') = '', '0', @InitialCatalog),
			   @FinalCatalog = IIF(ISNULL(@FinalCatalog, '') = '', 'ZZZZZZZZZZ', @FinalCatalog),
			   @InitialItem = IIF(ISNULL(@InitialItem, '') = '', '0', @InitialItem),
			   @FinalItem = IIF(ISNULL(@FinalItem, '') = '', 'ZZZZZZZZZZ', @FinalItem),
			   @InitialType = IIF(ISNULL(@InitialType, '') = '', '0', @InitialType),
			   @FinalType = IIF(ISNULL(@FinalType, '') = '', 'ZZZZZZZZZZ', @FinalType)

		SELECT CAST(Data AS INT) Data 
		INTO #Table_AdquisitionType
		FROM dbo.Split(@AdquisitionType, ',')

		SELECT 
			@ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @ClosingDate)),
			@OfficialLegalBookId = lb.Id
		FROM GeneralLedger.LegalBook lb
		WHERE lb.OfficialBook = 1

		SELECT @IvaCost = sfalb.IvaCost
		FROM FixedAsset.SettingFixedAssetByLegalBook sfalb 
		WHERE sfalb.LegalBookId = @OfficialLegalBookId

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fapa.AdquisitionType,
			fapa.AdquisitionDate,
			CASE fapa.AdquisitionType
				WHEN 1 THEN 'Compra Directa'
				WHEN 3 THEN 'Comodato'
				WHEN 4 THEN 'Donacion'
				WHEN 5 THEN 'Traspaso de Bienes'
				WHEN 6 THEN 'Otro Concepto'
				WHEN 7 THEN 'Leasing Financiero'
				WHEN 8 THEN 'Comodato Tercerizado'
				WHEN 9 THEN 'Renting Financiero'
				WHEN 10 THEN 'Renting Operativo'
			END AdquisitionTypeName,
			fapa.Status,
			fapa.HasOutput,
			IIF(fapa.OutputRefund = 1, 'Devuelto', IIF(fapa.Status = 1, 'Activo', 'Activo')) StatusName,
			faic.Code ItemCatalogCode,
			faic.Description ItemCatalogDescription,
			fai.Code ItemCode,
			fai.Description ItemDescription,
			fait.Code ItemTypeCode,
			fait.Name ItemTypeDescription,
			tp.Nit ResponsibleCode,
			tp.Name ResponsibleName,
			fal.Code LocationCode,
			fal.Name LocationName,

			IIF(@LegalBookId = @OfficialLegalBookId, ma.Number, ha.Number) MainAccounNumber,
			IIF(@LegalBookId = @OfficialLegalBookId, ma.Name, ha.Name) MainAccountName,
			IIF(@LegalBookId = @OfficialLegalBookId, dma.Number, dha.Number) DepreciationAccounNumber,
			IIF(@LegalBookId = @OfficialLegalBookId, dma.Name, dha.Name) DepreciationAccountName,

			ISNULL(faib.DepreciatedValue + faib.ResidualValue, 0) - ISNULL(fat.DevaluationValue, 0) HistoricalValueBook,
			fapa.HistoricalValue,
			ISNULL(fat.ValorizationValue, 0) ValorizationValue,
			ISNULL(fat.DevaluationValue, 0) DevaluationValue,
			ISNULL(faib.DepreciatedValue, 0) + ISNULL(fadh.DepreciatedValue, 0) AccumulatedDepreciation,
			ISNULL(fad.DepreciateValue, 0) DepreciateValue,
			ISNULL(faib.DepreciatedDays + faib.DaysPendingDepreciate, 0) + ISNULL(fat.TransactionDays, 0) LifeTimeInDays,		
			ISNULL(fat.TransactionDays, 0) AdjustedDays,
			ISNULL(faib.DepreciatedDays, 0) + ISNULL(fadh.DepreciatedDays, 0) DepreciatedDays,
			ISNULL(fad.DepreciateDays, 0) DepreciateDays,
			ISNULL(fapadb.ResidualValue,0)ResidualValue,
			fapa.Observation
		FROM FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK)
		LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
		JOIN #Table_AdquisitionType adt ON fapa.AdquisitionType = adt.Data
		JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
		JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK) ON fai.ItemCatalogId = faic.Id
		JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON fapa.ResponsibleId = far.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON far.ThirdPartyId = tp.Id
		JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON fapa.MainAccountId = ma.Id
		JOIN GeneralLedger.MainAccounts dma WITH (NOLOCK) ON CASE fapa.AdquisitionType 
							WHEN 3 then faic.LoanLeasingAccountId
							WHEN 7 then faic.DepreciationLeasingAccountId 
							WHEN 9 then faic.FinancialRentingAccountId
							ELSE faic.DepreciationAccountId 
						END = dma.Id
		JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
		JOIN 
		(
			SELECT 
				faibi.Plate, 
				faibidb.DepreciatedDays,
				faibidb.DaysPendingDepreciate,
				ISNULL(faibidb.DepreciatedValue, faibi.HistoricalValue) DepreciatedValue,
				ISNULL(faibidb.ResidualValue, 0) ResidualValue
			FROM FixedAsset.FixedAssetInitialBalance faib WITH (NOLOCK)
			JOIN FixedAsset.FixedAssetInitialBalanceItem faibi WITH (NOLOCK) ON faib.id = faibi.FixedAssetInitialBalanceId
			LEFT JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook faibidb WITH (NOLOCK) ON faibi.id = faibidb.FixedAssetInitialBalanceItemId AND faibidb.LegalBookId = @LegalBookId
			WHERE faib.Status = 2
				AND CAST(faib.DocumentDate AS DATE) <= @ClosingDate

			UNION ALL

			SELECT
				faeid.Plate,
				0 DepreciatedDays,
				faeidb.DaysPendingDepreciate,
				IIF(faeid.Depreciate = 0,0,IIF(faeidb.Id IS NULL, (faei.UnitValue + IIF(@IvaCost = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)), 0)) DepreciatedValue,
				ISNULL(faeidb.HistoricalValue, 0) ResidualValue
			FROM FixedAsset.FixedAssetEntry fae WITH (NOLOCK)
			JOIN FixedAsset.FixedAssetEntryItem faei WITH (NOLOCK) ON fae.Id = faei.FixedAssetEntryId
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid WITH (NOLOCK) ON faei.Id = faeid.FixedAssetEntryItemId
			LEFT JOIN FixedAsset.FixedAssetEntryItemDetailBook faeidb WITH (NOLOCK) ON faeid.Id = faeidb.FixedAssetEntryItemDetailId AND faeidb.LegalBookId = @LegalBookId
			WHERE fae.Status = 2
				AND CAST(fae.EntryDate AS DATE) <= @ClosingDate				
		) faib ON fapa.Plate = faib.Plate -- VALOR INICIAL
		LEFT JOIN
		(
			SELECT 
				fatd.PhysicalAssetId, 
				SUM(IIF(fatd.TransactionType = 1, fatdb.Value, 0)) ValorizationValue,
				SUM(IIF(fatd.TransactionType = 2, fatdb.Value, 0)) DevaluationValue,
				SUM(fatdb.LifeTime * fatdb.UnitLifeTime) TransactionDays			
			FROM FixedAsset.FixedAssetTransaction fat WITH (NOLOCK)	
			JOIN FixedAsset.FixedAssetTransactionDetail fatd WITH (NOLOCK) ON fat.Id = fatd.FixedAssetTransactionId
			JOIN FixedAsset.FixedAssetTransactionDetailBook fatdb WITH (NOLOCK) ON fatd.Id = fatdb.FixedAssetTransactionDetailId
			WHERE fat.Status = 2
				AND fatdb.LegalBookId = @LegalBookId
				AND CAST(fat.DocumentDate AS DATE) <= @ClosingDate
				AND fatd.PhysicalAssetId IS NOT NULL
			GROUP BY fatd.PhysicalAssetId
		) fat ON fapa.Id = fat.PhysicalAssetId -- VALOR TRANSACCIONES
		LEFT JOIN 
		(
			SELECT 
				fadd.FixedAssetPhysicalAssetId, 
				SUM(fadd.DepreciatedDays) DepreciatedDays,
				SUM(fadd.DepreciationValue) DepreciatedValue
			FROM FixedAsset.FixedAssetDepreciation fad WITH (NOLOCK)	
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd WITH (NOLOCK) ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE fad.Status = 2
				AND fadd.LegalBookId = @LegalBookId
				AND
				(
					fad.ClosingYear < @Year
					OR
					(fad.ClosingYear = @Year AND fad.ClosingMonth < @Month)
				)
			GROUP BY fadd.FixedAssetPhysicalAssetId
		) fadh ON fapa.Id = fadh.FixedAssetPhysicalAssetId -- DEPRECIACIONES ANTERIORES
		LEFT JOIN 
		(
			SELECT 
				fadd.FixedAssetPhysicalAssetId, 
				SUM(fadd.DepreciatedDays) DepreciateDays,
				SUM(fadd.DepreciationValue) DepreciateValue
			FROM FixedAsset.FixedAssetDepreciation fad WITH (NOLOCK)	
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd WITH (NOLOCK) ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE fad.Status = 2
				AND fadd.LegalBookId = @LegalBookId
				AND
				(
					(fad.ClosingYear = @Year AND fad.ClosingMonth = @Month)
				)
			GROUP BY fadd.FixedAssetPhysicalAssetId
		) fad ON fapa.Id = fad.FixedAssetPhysicalAssetId -- DEPRECIACIONES ACTUAL
		LEFT JOIN
		(
			SELECT ha.OfficialMainAccountid, ma.Id, ma.Number, ma.Name
			FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK)
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ha.MainAccountId = ma.Id
			WHERE ma.LegalBookId = @LegalBookId
		) ha ON ma.Id = ha.OfficialMainAccountId
		LEFT JOIN
		(
			SELECT ha.OfficialMainAccountid, ma.Id, ma.Number, ma.Name
			FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK)
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ha.MainAccountId = ma.Id
			WHERE ma.LegalBookId = @LegalBookId
		) dha ON dma.Id = dha.OfficialMainAccountId
		WHERE IIF(@LegalBookId = @OfficialLegalBookId, ma.Number, ha.Number) BETWEEN @InitialMainAccount AND @FinalMainAccount
			AND IIF(@LegalBookId = @OfficialLegalBookId, dma.Number, dha.Number) BETWEEN @InitialDepreciationAccount AND @FinalDepreciationAccount
			AND faic.Code BETWEEN @InitialCatalog AND @FinalCatalog
			AND fai.Code BETWEEN @InitialItem AND @FinalItem
			AND fait.Code BETWEEN @InitialType AND @FinalType
			AND ISNULL(fad.DepreciateValue, @IncludeDepreciated) > 0
			AND (fapa.OutputDate IS NULL OR fapa.OutputDate > @ClosingDate)
		ORDER BY fapa.Plate

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH

	IF OBJECT_ID('tempdb..#Table_AdquisitionType') IS NOT NULL DROP TABLE #Table_AdquisitionType
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte histórico de depreciación de activos fijos de la organización para un período contable específico (año y mes). Consolida información de los activos físicos (placa, serie, modelo, tipo de adquisición) con sus valores contables de depreciación acumulada, días depreciados, valor residual y valorización/devaluación, cruzando el libro contable oficial o alternativo seleccionado. Combina datos de catálogo de ítems, responsables del activo (con su tercero asociado), ubicación física y cuentas contables principales y de depreciación, aplicando filtros por rangos de cuenta, catálogo, ítem y tipo de activo. Se usa para reportería contable y de control patrimonial de activos fijos, cumplimiento de normas de depreciación y auditoría del balance de bienes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportHistoricalDepreciation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportHistoricalDepreciation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte histórico de depreciación de activos fijos a una fecha de cierre (último día del mes/año indicado), consolidando saldos iniciales, transacciones de valorización/devaluación y depreciaciones acumuladas y del periodo, por libro contable.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un libro contable marcado como oficial (OfficialBook = 1) en GeneralLedger.LegalBook.; Debe existir configuración FixedAsset.SettingFixedAssetByLegalBook para el libro oficial (define si IVA suma al costo).; El XML de criterios debe traer Year, Month, LegalBookId y AdquisitionType como lista separada por comas.; Si el LegalBookId solicitado no es el oficial, deben existir homologaciones en GeneralLedger.HomologationAccount entre cuentas oficiales y del libro destino.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran saldos iniciales (FixedAssetInitialBalance), entradas (FixedAssetEntry) y transacciones (FixedAssetTransaction) con Status=2 (aprobado/contabilizado).; Solo se consideran depreciaciones (FixedAssetDepreciation) con Status=2.; La fecha de cierre se ajusta siempre al último día del mes indicado por @Year/@Month.; Los activos dados de baja antes o en la fecha de cierre (OutputDate <= ClosingDate) se excluyen del reporte.; Los rangos de cuenta principal, cuenta de depreciación, catálogo, ítem y tipo aplican filtrado BETWEEN tras sustituir vacíos por ''0'' y ''ZZZZZZZZZZ''.; El valor histórico contable se calcula como (DepreciatedValue + ResidualValue) inicial menos DevaluationValue de transacciones.; La depreciación acumulada incluye depreciación inicial más depreciaciones de periodos anteriores al cierre.; El IVA se incorpora al costo solo si la configuración del libro oficial lo indica.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila por activo físico (FixedAssetPhysicalAsset) cuyo OutputDate sea NULL o posterior a la fecha de cierre, con saldos históricos, depreciación acumulada, depreciación del periodo, valorizaciones y devaluaciones.; [RETURN_RESULT] Resultset: En caso de excepción devuelve una fila con Code=''999'', el mensaje y la línea del error en lugar del reporte.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @LegalBookId = @OfficialLegalBookId → Usa directamente las cuentas principales (ma/dma) del activo y filtra rangos contra ma.Number/dma.Number. else Usa las cuentas homologadas (ha/dha) vía GeneralLedger.HomologationAccount para mostrar y filtrar números de cuenta del libro solicitado.; si fapa.AdquisitionType = 3 (Comodato) → La cuenta de depreciación se toma de faic.LoanLeasingAccountId. else Se evalúan otros tipos de adquisición.; si fapa.AdquisitionType = 7 (Leasing Financiero) → La cuenta de depreciación se toma de faic.DepreciationLeasingAccountId.; si fapa.AdquisitionType = 9 (Renting Financiero) → La cuenta de depreciación se toma de faic.FinancialRentingAccountId. else Para los demás tipos se usa faic.DepreciationAccountId.; si @IvaCost = 1 (configuración del libro oficial indica IVA al costo) → El valor inicial proveniente de entradas (FixedAssetEntry) suma ROUND(IvaValue/Quantity,0) al UnitValue. else Solo se considera UnitValue como valor depreciable inicial.; si faeid.Depreciate = 0 → El DepreciatedValue del item de entrada se toma como 0 (activo no depreciable). else Si no existe registro en FixedAssetEntryItemDetailBook se toma UnitValue (+ IVA según config); si existe, se toma 0 para no duplicar.; si fad.ClosingYear < @Year OR (ClosingYear = @Year AND ClosingMonth < @Month) → La depreciación se acumula como histórica (fadh). else Si ClosingYear=@Year y ClosingMonth=@Month se considera depreciación del periodo actual (fad).; si ISNULL(fad.DepreciateValue, @IncludeDepreciated) > 0 → El activo se incluye en el reporte; si no hay depreciación del periodo, sólo se incluye cuando @IncludeDepreciated=1. else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; FixedAsset.SettingFixedAssetByLegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; FixedAsset.FixedAssetResponsible; Common.ThirdParty; FixedAsset.FixedAssetLocation; GeneralLedger.MainAccounts; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetInitialBalance; FixedAsset.FixedAssetInitialBalanceItem; FixedAsset.FixedAssetInitialBalanceItemDetailBook; FixedAsset.FixedAssetEntry; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetEntryItemDetailBook; FixedAsset.FixedAssetTransaction; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetTransactionDetailBook; FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalDepreciation';
-- GO
