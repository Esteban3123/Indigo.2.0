-- =============================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 17-01-2022
-- Description:	Procedimiento para el reporte del mes de depreciación
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportDepreciationMonth]
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
			   @InitialType = IIF(ISNULL(@InitialType, '') = '', '0', @InitialType),
			   @FinalType = IIF(ISNULL(@FinalType, '') = '', 'ZZZZZZZZZZ', @FinalType)

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
			ma.Number MainAccounNumber,
			ma.Name  MainAccountName,
			IIF(@LegalBookId = @OfficialLegalBookId, dma.Number, dha.Number) DepreciationAccounNumber,
			IIF(@LegalBookId = @OfficialLegalBookId, dma.Name, dha.Name) DepreciationAccountName,
			fapadb.HistoricalValue,			
			fapa.FinancialDiscount,
			ISNULL(fad.DepreciateValue, 0) DepreciateValue,						
			ISNULL(faib.DepreciatedDays + faib.DaysPendingDepreciate, 0) + ISNULL(fat.TransactionDays, 0) LifeTimeInDays,			
			ISNULL(fad.DepreciateDays, 0) DepreciateDays,
			ISNULL(fapadb.ResidualValue,0)ResidualValue,
			fapa.Observation
		FROM FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK)
		LEFT JOIN (
				SELECT fapadb.PhysicalAssetId, fapadb.HistoricalValue,fapadb.ResidualValue
				FROM FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb
				WHERE fapadb.LegalBookId = @LegalBookId
			) AS fapadb ON fapa.id = fapadb.PhysicalAssetId
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
		LEFT JOIN 
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
				IIF(faeidb.Id IS NULL, (faei.UnitValue + IIF(@IvaCost = 1, ROUND(faei.IvaValue / faei.Quantity, 0), 0)), 0) DepreciatedValue,
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
		JOIN 
		(
			SELECT 
				fadd.FixedAssetPhysicalAssetId, 
				SUM(fadd.DepreciatedDays) DepreciateDays,
				SUM(fadd.DepreciationValue) DepreciateValue
			FROM FixedAsset.FixedAssetDepreciation fad WITH (NOLOCK)	
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd WITH (NOLOCK) ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE --fad.Status = 2
				 fadd.LegalBookId = @LegalBookId
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
		WHERE 
			ma.Number BETWEEN @InitialMainAccount AND @FinalMainAccount
			AND dma.Number BETWEEN @InitialDepreciationAccount AND @FinalDepreciationAccount
			AND faic.Code BETWEEN @InitialCatalog AND @FinalCatalog
			AND (
					(@InitialItem IS NULL OR fai.Code >= @InitialItem) 
					AND (@FinalItem IS NULL OR fai.Code <= @FinalItem)
				)
			AND fait.Code BETWEEN @InitialType AND @FinalType
			AND ISNULL(fad.DepreciateValue, @IncludeDepreciated) > 0			
		ORDER BY fapa.Plate

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte mensual de depreciación de activos fijos para un período (mes y año) y libro contable seleccionados. Consolida información de cada bien físico —placa, serie, modelo, tipo de adquisición, valor histórico, valor residual, días depreciados y valor depreciado en el mes— cruzando los activos con su catálogo, ítem, tipo, responsable (tercero), ubicación y cuentas contables principales y de depreciación. Permite filtrar por rangos de cuenta contable, catálogo, ítem y tipo de activo, y distingue entre el libro oficial y libros alternativos (NIIF, fiscal, etc.), considerando si el IVA forma parte del costo del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDepreciationMonth';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDepreciationMonth';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte mensual de depreciación de activos fijos físicos para un libro legal y mes/año dados, mostrando valor histórico, depreciación acumulada, vida útil y cuentas contables homologadas.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un LegalBook marcado como OfficialBook = 1 para resolver la cuenta oficial.; Debe existir configuración FixedAsset.SettingFixedAssetByLegalBook para el libro oficial (de allí se toma IvaCost).; Los XML de criterios y filtros deben respetar el esquema /Data con los nodos esperados (OperatingUnitId, Year, Month, LegalBookId, etc.).; Deben existir registros de depreciación (FixedAssetDepreciation/Detail) para el año y mes solicitados, ya que el JOIN a depreciaciones es INNER.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'@ClosingDate se normaliza al último día del mes solicitado (DATEADD(DAY,-1, DATEADD(MONTH,1, primer día del mes))).; Los rangos de filtro vacíos/NULL se sustituyen por ''0'' (inicial) y ''ZZZZZZZZZZ'' (final), garantizando que no filtren.; Solo se consideran balances iniciales y entradas con Status = 2 (aprobados/oficializados) y fecha <= cierre de mes.; Solo se consideran transacciones (FixedAssetTransaction) con Status = 2 y fecha <= cierre de mes.; La homologación de cuentas se restringe a cuentas cuyo LegalBookId coincide con el libro solicitado.; La depreciación reportada corresponde exclusivamente al ClosingYear y ClosingMonth solicitados.; Los errores no propagan excepción; siempre se devuelven como result set con Code ''999''.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico (placa/serie); Depreciación mensual; Tipos de adquisición (Compra Directa, Comodato, Donación, Traspaso, Leasing Financiero, Comodato Tercerizado, Renting Financiero, Renting Operativo); Libro legal contable / libro oficial; Homologación de cuentas contables entre libros; Valor histórico, valor residual, Valor de salvamento (FinancialDiscount); valor depreciado; Saldo inicial de activos fijos; Entrada de activos fijos; Transacciones de valorización y devaluación; Vida útil en días (depreciados, pendientes, por transacciones); Costo con IVA (IvaCost); Responsable del activo (tercero) y ubicación; Cierre contable mensual', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Cuando la consulta se ejecuta correctamente, retorna el listado de activos físicos con sus datos de depreciación del mes/año, filtrados por rangos de cuenta principal, cuenta de depreciación, catálogo, ítem y tipo.; [RETURN_RESULT] ResultSet: Cuando ocurre una excepción en TRY, retorna una fila con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE() en lugar del reporte.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @LegalBookId = @OfficialLegalBookId → Se reportan los datos de la cuenta de depreciación directa (dma). else Se reporta la cuenta de depreciación homologada (dha) obtenida desde GeneralLedger.HomologationAccount para el libro solicitado.; si fapa.AdquisitionType = 3 (Comodato) → La cuenta de depreciación se toma de faic.LoanLeasingAccountId.; si fapa.AdquisitionType = 7 (Leasing Financiero) → La cuenta de depreciación se toma de faic.DepreciationLeasingAccountId.; si fapa.AdquisitionType = 9 (Renting Financiero) → La cuenta de depreciación se toma de faic.FinancialRentingAccountId. else Para los demás tipos de adquisición se usa faic.DepreciationAccountId.; si fapa.OutputRefund = 1 → Se etiqueta el estado como ''Devuelto''. else Se etiqueta como ''Activo'' (independiente de fapa.Status).; si @IvaCost = 1 (configuración del libro oficial) y el activo proviene de FixedAssetEntry sin detalle de libro → El valor depreciado inicial incluye el IVA prorrateado: UnitValue + ROUND(IvaValue/Quantity, 0). else Solo se considera el UnitValue.; si ISNULL(fad.DepreciateValue, @IncludeDepreciated) > 0 → Se incluye el activo en el reporte; si no hay depreciación del mes, depende del flag @IncludeDepreciated para incluirlo o excluirlo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; FixedAsset.SettingFixedAssetByLegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; FixedAsset.FixedAssetResponsible; Common.ThirdParty; FixedAsset.FixedAssetLocation; GeneralLedger.MainAccounts; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetInitialBalance; FixedAsset.FixedAssetInitialBalanceItem; FixedAsset.FixedAssetInitialBalanceItemDetailBook; FixedAsset.FixedAssetEntry; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetEntryItemDetailBook; FixedAsset.FixedAssetTransaction; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetTransactionDetailBook; FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDepreciationMonth';
-- GO
