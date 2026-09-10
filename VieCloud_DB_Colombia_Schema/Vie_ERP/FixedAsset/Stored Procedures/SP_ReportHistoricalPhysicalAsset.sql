-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/11/2019
-- Description:	Procedimiento para el reporte histórico de activos fijos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportHistoricalPhysicalAsset]
	@xmlFilters as xml
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year INT, 
			@Month INT, 
			@LegalBookId INT, 
			@IncludedDepreciation INT, 
			@Status VARCHAR(MAX), 
			@AdquisitionTypes VARCHAR(MAX), 
			@Items VARCHAR(MAX), 
			@ItemTypes VARCHAR(MAX), 
			@Plates VARCHAR(MAX), 
			-------------
			@FilterByStatus BIT = 0,
			@FilterByAdquisitionTypes BIT = 0,
			@FilterByItems BIT = 0,
			@FilterByItemTypes BIT = 0,
			@FilterByPlates BIT = 0,
			@ClosingDate DATE,
			-------------
			@LegalBookCode VARCHAR(20),
			@LegalBookName VARCHAR(200),
			@OfficialBook BIT,
			@TypeBook TINYINT

	DECLARE @Table_Status AS TABLE(Id INT)
	DECLARE @Table_AdquisitionTypes AS TABLE(Id INT)
	DECLARE @Table_Items AS TABLE(Id INT)
	DECLARE @Table_ItemTypes AS TABLE(Id INT)
	DECLARE @Table_Plates AS TABLE(Id INT)

	--se crea tabla temporal para acumular la data
	CREATE TABLE #Table_Result 
	(
				LegalBookId INT, 
				LegalBookCode varchar(50), 
				LegalBookName varchar(MAX), 
				LegalBookDescription varchar(MAX),
				Plate varchar(50), 
				Serie varchar(50),
				Model  varchar(100),
				AdquisitionType bit, 
				AdquisitionDate datetime, 
				AdquisitionTypeDescription varchar(100),
				[Status]  bit, 
				OutputRefund  bit, 
				StatusDescription varchar(100), 
				Depreciate bit, 
				DepreciateDescription varchar(5), 				
				ItemCatalogId int, 
				ItemCatalogCode varchar(50), 
				ItemCatalogName varchar(MAX), 
				ItemCatalogDescription varchar(MAX),
				ItemId int, 
				ItemCode varchar(50), 
				ItemName varchar(MAX), 
				ItemDescription varchar(MAX),
				ItemTypeId int, 
				ItemTypeCode varchar(50), 
				ItemTypeName varchar(50), 
				ItemTypeDescription varchar(100),				
				ResponsibleId int, 
				ResponsibleCode varchar(50), 
				ResponsibleNit varchar(50), 
				ResponsibleDescription varchar(MAX), 
				LocationId int, 
				LocationCode varchar(50), 
				LocationName varchar(MAX), 
				LocationDescription varchar(MAX),

				MainAccountId int,
			    MainAccountNumber varchar(50),				
				MainAccountName varchar(100),
				MainAccountDescription varchar(MAX),

				DepreciationMainAccountId int,
				DepreciationMainAccountNumber varchar(50),
				DepreciationMainAccountName varchar(MAX),
				DepreciationMainAccountDescription varchar(MAX),
				
				HistoricalValue DECIMAL(20,4),
				ValorizationValue DECIMAL(20,4),
				DevaluationValue DECIMAL(20,4),
				TransactionValue DECIMAL(20,4), 
				DepreciatedDays INT,
				DepreciateValue DECIMAL(20,4), 
				AcumulatedDepreciation DECIMAL(20,4),
				FixedAssetPhysicalAssetId int
	)

	BEGIN TRY

		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = tp.x.value('Year[1]','int'),
				@Month = tp.x.value('Month[1]','int'),
				@LegalBookId = tp.x.value('LegalBookId[1]','int'),
				@IncludedDepreciation = tp.x.value('IncludedDepreciation[1]','int'),
				@Status = tp.x.value('StatusFilter[1]','varchar(max)'),				
				@AdquisitionTypes = tp.x.value('AdquisitionTypeFilter[1]','varchar(max)'),
				@Items = tp.x.value('ItemFilter[1]','varchar(max)'),
				@ItemTypes = tp.x.value('ItemTypeFilter[1]','varchar(max)'),
				@Plates = tp.x.value('PlateFilter[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') tp(x)

		IF ISNULL(@Status, '') <> ''
		BEGIN
			SET @FilterByStatus = 1

			INSERT INTO @Table_Status
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Status, ',')
		END

		IF ISNULL(@AdquisitionTypes, '') <> ''
		BEGIN
			SET @FilterByAdquisitionTypes = 1

			INSERT INTO @Table_AdquisitionTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@AdquisitionTypes, ',')
		END

		IF ISNULL(@Items, '') <> ''
		BEGIN
			SET @FilterByItems = 1

			INSERT INTO @Table_Items
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Items, ',')
		END

		IF ISNULL(@ItemTypes, '') <> ''
		BEGIN
			SET @FilterByItemTypes = 1

			INSERT INTO @Table_ItemTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ItemTypes, ',')
		END

		IF ISNULL(@Plates, '') <> ''
		BEGIN
			SET @FilterByPlates = 1

			INSERT INTO @Table_Plates
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Plates, ',')
		END
	
		SELECT @ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, DATEFROMPARTS(@Year, @Month, 1)))

		SELECT	@LegalBookCode = Code,
				@LegalBookName = Name,
				@OfficialBook = OfficialBook,
				@TypeBook = TypeBook
		FROM GeneralLedger.LegalBook 
		WHERE Id = @LegalBookId
		

		--   TABLAS INTERMEDIAS PARA OPTIMIZACIÓN
		CREATE TABLE #Temp_PhysicalAssets
		(
			PhysicalAssetId INT,
			Plate VARCHAR(50),
			Serie VARCHAR(50),
			Model VARCHAR(100),
			AdquisitionType TINYINT,
			AdquisitionDate DATE,
			Status BIT,
			OutputRefund BIT,
			Depreciate BIT,
			LocationId INT,
			ResponsibleId INT,
			MainAccountId INT,
			HistoricalValue DECIMAL(18,2),
			FairValue DECIMAL(18,2),
			ItemId INT,
			ItemCode VARCHAR(20),
			ItemDescription VARCHAR(300),
			ItemTypeId INT,
			ItemCatalogId INT,
			ItemCatalogCode VARCHAR(20),
			ItemCatalogDescription VARCHAR(MAX),
			ItemTypeCode VARCHAR(20),
			ItemTypeName VARCHAR(50),
			LocationCode VARCHAR(20),
			LocationName VARCHAR(200),
			ResponsibleCode VARCHAR(20),
			ResponsibleNit VARCHAR(25),
			ResponsibleName VARCHAR(300)
		);

		CREATE TABLE #Temp_MainAccounts
		(
			MainAccountId INT,
			MainAccountNumber VARCHAR(50),
			MainAccountName VARCHAR(300),
			LegalBookId INT
		);

		CREATE TABLE #Temp_DepreciationAccounts
		(
			DepreciationAccountId INT,
			DepreciationMainAccountId INT,
			DepreciationMainAccountNumber VARCHAR(50),
			DepreciationMainAccountName VARCHAR(300),
			PhysicalAssetId INT
		);

			
		CREATE TABLE #Temp_DepreciationData
		(
			PhysicalAssetId INT,
			InitialDepreciatedDays INT,
			InitialDepreciatedValue DECIMAL(18,2),
			PreviousDepreciatedDays INT,
			PreviousDepreciatedValue DECIMAL(18,2),
			CurrentDepreciatedDays INT,
			CurrentDepreciatedValue DECIMAL(18,2),
			ValorizationValue DECIMAL(18,2),
			DevaluationValue DECIMAL(18,2)
		);

			

	/************************************* NUEVO CÓDIGO OPTIMIZADO ***********************************/
		---- Se agrega sección para que se respete el estado del activo en la fecha de corte
		;with cte_FixedAsset as(select fapa.id, fapa.Plate,
                        fapa.Serie,
                        fapa.Model,
                        fapa.AdquisitionType,
                        fapa.AdquisitionDate,
                        fapa.OutputRefund,
                        fapa.Depreciate,
                        fapa.LocationId,
                        fapa.ResponsibleId,
                        fapa.MainAccountId,
                        fapa.HistoricalValue,
                        fapa.FairValue,
                        fapa.ItemId,
						fapa.HasOutput,
                        IIF(
							(fa.DocumentDate IS NOT NULL OR CAST(faed.DocumentDate AS DATE) IS NOT NULL),
								IIF(COALESCE(fa.DocumentDate, CAST(faed.DocumentDate AS DATE)) > @ClosingDate, 1,0), fapa.Status) AS Status
                FROM FixedAsset.FixedAssetPhysicalAsset fapa
                left join FixedAsset.FixedAssetActiveOutputDetail fad on fad.PhysicalAssetId =  fapa.id
                left join FixedAsset.FixedAssetActiveOutput fa on fa.id = fad.FixedAssetActiveOutputId and fa.Status = 2
                left join FixedAsset.FixedAssetEntryItemDetail faeid  on faeid.Plate = fapa.Plate
                left join FixedAsset.FixedAssetEntryDevolutionDetail faedd on faeid.Id = faedd.FixedAssetEntryItemDetailId
                left join FixedAsset.FixedAssetEntryDevolution faed on faed.Id = faedd.FixedAssetEntryDevolutionId and faed.Status = 2
				GROUP BY fapa.id, 
				fapa.Plate,
				fapa.Serie,
				fapa.Model,
				fapa.AdquisitionType,
				fapa.AdquisitionDate,
				fapa.OutputRefund,
				fapa.Depreciate,
				fapa.LocationId,
				fapa.ResponsibleId,
				fapa.MainAccountId,
				fapa.HistoricalValue,
				fapa.FairValue,
				fapa.ItemId,
				fa.DocumentDate,
				faed.DocumentDate,
				faeid.plate,
				fapa.Status,
				fapa.HasOutput
					)

		INSERT INTO #Temp_PhysicalAssets
		SELECT
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fapa.AdquisitionType,
			fapa.AdquisitionDate,
			fapa.Status,
			fapa.OutputRefund,
			fapa.Depreciate,
			fapa.LocationId,
			fapa.ResponsibleId,
			fapa.MainAccountId,
			fapa.HistoricalValue,
			fapa.FairValue,
			fai.Id AS ItemId,
			fai.Code AS ItemCode,
			fai.Description AS ItemDescription,
			fai.ItemTypeId,
			faic.Id AS ItemCatalogId,
			faic.Code AS ItemCatalogCode,
			faic.Description AS ItemCatalogDescription,
			fait.Code AS ItemTypeCode,
			fait.Name AS ItemTypeName,
			fal.Code AS LocationCode,
			fal.Name AS LocationName,
			far.Code AS ResponsibleCode,
			tp.Nit AS ResponsibleNit,
			tp.Name AS ResponsibleName
		FROM cte_FixedAsset fapa WITH(NOLOCK)
		JOIN FixedAsset.FixedAssetItem fai WITH(NOLOCK) ON fapa.ItemId = fai.Id
		JOIN FixedAsset.FixedAssetItemType fait WITH(NOLOCK) ON fai.ItemTypeId = fait.Id
		JOIN FixedAsset.FixedAssetItemCatalog faic WITH(NOLOCK) ON fai.ItemCatalogId = faic.Id
		JOIN FixedAsset.FixedAssetLocation fal WITH(NOLOCK) ON fapa.LocationId = fal.Id
		JOIN FixedAsset.FixedAssetResponsible far WITH(NOLOCK) ON fapa.ResponsibleId = far.Id
		JOIN Common.ThirdParty tp ON far.ThirdPartyId = tp.Id
		LEFT JOIN @Table_Status TS ON TS.Id = FAPA.Status OR (TS.Id = 2 AND fapa.OutputRefund = 1)
		WHERE fapa.AdquisitionDate <= @ClosingDate
		   AND (
		    @FilterByStatus = 0
		    OR (
		      @FilterByStatus = 1
		      AND (
		        (1 IN (SELECT Id FROM @Table_Status) AND fapa.Status = 1 AND ISNULL(fapa.OutputRefund, 0) = 0 AND fapa.HasOutput = 0)
		        OR (2 IN (SELECT Id FROM @Table_Status) AND fapa.OutputRefund = 1)
		        OR (0 IN (SELECT Id FROM @Table_Status) AND fapa.Status = 0 AND ISNULL(fapa.OutputRefund, 0) = 0)
		      )
		    )
		  )
		  AND (@FilterByAdquisitionTypes = 0 OR fapa.AdquisitionType IN (SELECT Id FROM @Table_AdquisitionTypes))
		  AND (@FilterByItems = 0 OR fapa.ItemId IN (SELECT Id FROM @Table_Items))
		  AND (@FilterByItemTypes = 0 OR fai.ItemTypeId IN (SELECT Id FROM @Table_ItemTypes));

		INSERT INTO #Temp_MainAccounts
		SELECT
			ma.Id AS MainAccountId,
			ma.Number AS MainAccountNumber,
			ma.Name AS MainAccountName,
			ma.LegalBookId
		FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
		WHERE ma.LegalBookId = @LegalBookId;

		INSERT INTO #Temp_DepreciationAccounts
		SELECT 
			CASE 
				WHEN fapa.AdquisitionType = 3 THEN faic.LoanLeasingAccountId
				WHEN fapa.AdquisitionType = 7 THEN faic.DepreciationLeasingAccountId
				WHEN fapa.AdquisitionType = 9 THEN faic.FinancialRentingAccountId
				ELSE faic.DepreciationAccountId
			END AS DepreciationAccountId,
			mad.Id AS DepreciationMainAccountId,
			mad.Number AS DepreciationMainAccountNumber,
			mad.Name AS DepreciationMainAccountName,
			fapa.Id AS PhysicalAssetId
		FROM FixedAsset.FixedAssetPhysicalAsset fapa WITH(NOLOCK)
		JOIN FixedAsset.FixedAssetItem fai  WITH(NOLOCK) ON fapa.ItemId = fai.Id
		JOIN FixedAsset.FixedAssetItemCatalog faic WITH(NOLOCK) ON fai.ItemCatalogId = faic.Id
		JOIN GeneralLedger.MainAccounts mad WITH(NOLOCK) ON mad.Id = 
			CASE 
				WHEN fapa.AdquisitionType = 3 THEN faic.LoanLeasingAccountId
				WHEN fapa.AdquisitionType = 7 THEN faic.DepreciationLeasingAccountId
				WHEN fapa.AdquisitionType = 9 THEN faic.FinancialRentingAccountId
				ELSE faic.DepreciationAccountId
			END;

	-- CTEs PARA DATOS DE DEPRECIACIÓN
		WITH 
			_MainAccount AS (
				SELECT ha.OfficialMainAccountId,
					   mah.LegalBookId, 
					   mah.Id as IdAccount, 
					   mah.Number,
					   mah.Name
				FROM GeneralLedger.HomologationAccount ha   WITH(NOLOCK)
				JOIN GeneralLedger.MainAccounts mah  WITH(NOLOCK) ON (@OfficialBook <> 1 AND mah.LegalBookId = @LegalBookId) and ha.MainAccountId = mah.Id AND mah.LegalBookId = @LegalBookId
				),
			_initialValueData as (
				SELECT 
					faibi.Plate, 
					faibidb.DepreciatedDays,
					faibidb.DaysPendingDepreciate,
					faibidb.DepreciatedValue,
					faibidb.ResidualValue
				FROM FixedAsset.FixedAssetInitialBalance faib WITH (NOLOCK)
				JOIN FixedAsset.FixedAssetInitialBalanceItem faibi WITH (NOLOCK) ON faib.id = faibi.FixedAssetInitialBalanceId
				LEFT JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook faibidb WITH (NOLOCK) ON faibi.id = faibidb.FixedAssetInitialBalanceItemId
				WHERE faib.Status = 2
					AND CAST(faib.DocumentDate AS DATE) <= @ClosingDate
					AND ISNULL(faibidb.LegalBookId,@LegalBookId) = @LegalBookId

				UNION ALL

				SELECT
					faeid.Plate,
					0 DepreciatedDays,
					faeidb.DaysPendingDepreciate,
					0 DepreciatedValue,
					faeidb.HistoricalValue ResidualValue
				FROM FixedAsset.FixedAssetEntry fae WITH (NOLOCK)
				JOIN FixedAsset.FixedAssetEntryItem faei WITH (NOLOCK) ON fae.Id = faei.FixedAssetEntryId
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid WITH (NOLOCK) ON faei.Id = faeid.FixedAssetEntryItemId
				LEFT JOIN FixedAsset.FixedAssetEntryItemDetailBook faeidb WITH (NOLOCK) ON faeid.Id = faeidb.FixedAssetEntryItemDetailId AND faeidb.LegalBookId = @LegalBookId
				WHERE fae.Status = 2
					AND CAST(fae.EntryDate AS DATE) <= @ClosingDate
					AND ISNULL(faeidb.LegalBookId,@LegalBookId) = @LegalBookId
				),
			_TransactionValueData as(
				SELECT 
					fatd.PhysicalAssetId, 
					SUM(IIF(fatd.TransactionType = 1, ISNULL(fatdb.Value,fatd.Value), 0)) ValorizationValue,
					SUM(IIF(fatd.TransactionType = 2, ISNULL(fatdb.Value,fatd.Value), 0)) DevaluationValue,
					SUM(fatdb.LifeTime * fatdb.UnitLifeTime) TransactionDays			
				FROM FixedAsset.FixedAssetTransaction fat WITH (NOLOCK)	
				JOIN FixedAsset.FixedAssetTransactionDetail fatd WITH (NOLOCK) ON fat.Id = fatd.FixedAssetTransactionId
				LEFT JOIN FixedAsset.FixedAssetTransactionDetailBook fatdb WITH (NOLOCK) ON fatd.Id = fatdb.FixedAssetTransactionDetailId
				WHERE fat.Status = 2
					AND ISNULL(fatdb.LegalBookId,@LegalBookId) = @LegalBookId
					AND CAST(fat.DocumentDate AS DATE) <= @ClosingDate
					AND fatd.PhysicalAssetId IS NOT NULL
				GROUP BY fatd.PhysicalAssetId
				),
			_PreviousDepreciationsData as(
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
				),
			_CurrentDepreciationData as(
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
				)

		INSERT INTO #Temp_DepreciationData
		SELECT 
			fapa.Id AS PhysicalAssetId,
			ISNULL(faib.DepreciatedDays, 0) AS InitialDepreciatedDays,
			ISNULL(faib.DepreciatedValue, 0) AS InitialDepreciatedValue,
			ISNULL(fadh.DepreciatedDays, 0) AS PreviousDepreciatedDays,
			ISNULL(fadh.DepreciatedValue, 0) AS PreviousDepreciatedValue,
			ISNULL(fad.DepreciateDays, 0) AS CurrentDepreciatedDays,
			ISNULL(fad.DepreciateValue, 0) AS CurrentDepreciatedValue,
			ISNULL(tvd.ValorizationValue, 0) AS ValorizationValue,
			ISNULL(tvd.DevaluationValue, 0) AS DevaluationValue
		FROM FixedAsset.FixedAssetPhysicalAsset fapa WITH(NOLOCK)
		LEFT JOIN _initialValueData faib WITH(NOLOCK) ON fapa.Plate = faib.Plate
		LEFT JOIN _PreviousDepreciationsData fadh WITH(NOLOCK) ON fapa.Id = fadh.FixedAssetPhysicalAssetId
		LEFT JOIN _CurrentDepreciationData fad WITH(NOLOCK) ON fapa.Id = fad.FixedAssetPhysicalAssetId
		LEFT JOIN _TransactionValueData tvd WITH(NOLOCK) ON fapa.Id = tvd.PhysicalAssetId;

		/**************************************************************************************/

		-- Consulta final usando tablas temporales ya filtradas
		INSERT INTO #Table_Result (
			LegalBookId, LegalBookCode, LegalBookName, LegalBookDescription, Plate, Serie, Model, AdquisitionType, AdquisitionDate, 
			AdquisitionTypeDescription, [Status], OutputRefund, StatusDescription, Depreciate, DepreciateDescription, 
			ItemCatalogId, ItemCatalogCode, ItemCatalogName, ItemCatalogDescription, ItemId, ItemCode, ItemName, ItemDescription, 
			ItemTypeId, ItemTypeCode, ItemTypeName, ItemTypeDescription, ResponsibleId, ResponsibleCode, ResponsibleNit, 
			ResponsibleDescription, LocationId, LocationCode, LocationName, LocationDescription, MainAccountId, 
			MainAccountNumber, MainAccountName, MainAccountDescription, DepreciationMainAccountId, DepreciationMainAccountNumber, 
			DepreciationMainAccountName, DepreciationMainAccountDescription, HistoricalValue, ValorizationValue, DevaluationValue, 
			TransactionValue, DepreciatedDays, DepreciateValue, AcumulatedDepreciation, FixedAssetPhysicalAssetId
		)
		SELECT 
			@LegalBookId, @LegalBookCode, @LegalBookName, @LegalBookCode + ' - ' + @LegalBookName, 
			fapa.Plate, fapa.Serie, fapa.Model, fapa.AdquisitionType, fapa.AdquisitionDate,        
			CASE fapa.AdquisitionType                                                              
				WHEN 1 THEN 'Compra Directa' 
				WHEN 3 THEN 'Comodato' 
				WHEN 4 THEN 'Donación' 
				WHEN 5 THEN 'Traspaso Bienes' 
				WHEN 6 THEN 'Otro Concepto' 
				WHEN 7 THEN 'Leasing Financiero' 
				WHEN 8 THEN 'Comodato Tercerizado' 
				WHEN 9 THEN 'Renting Financiero' 
				WHEN 10 THEN 'Renting Operativo' 
			END,
			fapa.Status, fapa.OutputRefund,                                                       
			CASE WHEN fapa.OutputRefund = 1 and fapa.Status = 0 THEN 'Devuelto' WHEN fapa.Status = 1 THEN 'Activo' ELSE 'Inactivo' END,
			fapa.Depreciate,                                                                      
			CASE WHEN fapa.Depreciate = 1 THEN 'Si' ELSE 'No' END,                                
			fapa.ItemCatalogId, fapa.ItemCatalogCode, fapa.ItemCatalogDescription,                
			fapa.ItemCatalogCode + ' - ' + fapa.ItemCatalogDescription,                           
			fapa.ItemId, fapa.ItemCode, fapa.ItemDescription, fapa.ItemCode + ' - ' + fapa.ItemDescription,   
			fapa.ItemTypeId, fapa.ItemTypeCode, fapa.ItemTypeName,                               
			fapa.ItemTypeCode + ' - ' + fapa.ItemTypeName,                                       
			fapa.ResponsibleId, fapa.ResponsibleCode, fapa.ResponsibleNit,                       
			fapa.ResponsibleCode + ' - ' + fapa.ResponsibleName,                                 
			fapa.LocationId, fapa.LocationCode, fapa.LocationName,                               
			fapa.LocationCode + ' - ' + fapa.LocationName,                                       
			ma.MainAccountId, ma.MainAccountNumber, ma.MainAccountName,                          
			ma.MainAccountNumber + ' - ' + ma.MainAccountName,                                   
			da.DepreciationMainAccountId, da.DepreciationMainAccountNumber,                      
			da.DepreciationMainAccountName,                                                      
			da.DepreciationMainAccountNumber + ' - ' + da.DepreciationMainAccountName,           
			COALESCE(fapa.HistoricalValue, fapa.FairValue),                                      
			dep.ValorizationValue, dep.DevaluationValue, dep.ValorizationValue,				     
			ISNULL(dep.InitialDepreciatedDays, 0) + ISNULL(dep.PreviousDepreciatedDays, 0) + ISNULL(dep.CurrentDepreciatedDays, 0), 
			ISNULL(dep.CurrentDepreciatedValue, 0),                                              
			ISNULL(dep.InitialDepreciatedValue, 0) + ISNULL(dep.PreviousDepreciatedValue, 0),    
			fapa.PhysicalAssetId                                                                 
		FROM #Temp_PhysicalAssets fapa
		LEFT JOIN #Temp_MainAccounts ma ON fapa.MainAccountId = ma.MainAccountId
		LEFT JOIN #Temp_DepreciationAccounts da ON fapa.PhysicalAssetId = da.PhysicalAssetId
		LEFT JOIN #Temp_DepreciationData dep ON fapa.PhysicalAssetId = dep.PhysicalAssetId
		

    	---se consulta los datos finales 
		SELECT * FROM #Table_Result

		--SE ELIMINAN LAS TABLAS TEMPORALES
		IF OBJECT_ID('tempdb..#Temp_PhysicalAssets') IS NOT NULL
		DROP TABLE #Temp_PhysicalAssets
		
		IF OBJECT_ID('tempdb..#Temp_MainAccounts') IS NOT NULL
		DROP TABLE #Temp_MainAccounts

		IF OBJECT_ID('tempdb..#Temp_DepreciationAccounts') IS NOT NULL
		DROP TABLE #Temp_DepreciationAccounts

		IF OBJECT_ID('tempdb..#Temp_DepreciationData') IS NOT NULL
		DROP TABLE #Temp_DepreciationData
		
		IF OBJECT_ID('tempdb..#Table_Result') IS NOT NULL
		DROP TABLE #Table_Result

	END TRY
	BEGIN CATCH	
		--SE ELIMINA LA TABLA TEMPORAL
		IF OBJECT_ID('tempdb..#Table_Result') IS NOT NULL
		DROP TABLE #Table_Result
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte histórico de activos fijos físicos (bienes inventariados) con toda su información de valor, depreciación acumulada, ubicación, responsable y cuenta contable, cortado a una fecha de cierre calculada según el año y mes indicados. Recibe filtros en XML —como estado del activo, tipo de adquisición, tipo de ítem, artículo específico y placa— que se descomponen mediante la función Split para aplicarlos dinámicamente. Consulta el libro contable legal (LegalBook) para obtener el contexto contable del reporte, y cruza la tabla de activos físicos (FixedAssetPhysicalAsset) con los detalles de entradas, salidas, devoluciones y datos de depreciación para producir un resultado consolidado que muestra por cada activo: placa, serie, modelo, fecha de adquisición, valores histórico y depreciado, días depreciados, depreciación acumulada, cuenta principal y cuenta de depreciación. Se usa en la consola de reportería de activos fijos para auditoría contable, inventario físico y seguimiento del valor residual de los bienes de la organización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte histórico de activos fijos físicos a una fecha de corte (último día del mes/año indicado), incluyendo valor histórico, valorizaciones, desvalorizaciones y depreciaciones acumuladas por libro contable.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con Year, Month, LegalBookId y filtros opcionales; El LegalBookId debe existir en GeneralLedger.LegalBook para obtener código, nombre, OfficialBook y TypeBook; Las listas de filtros (Status, AdquisitionTypes, Items, ItemTypes, Plates) deben ser cadenas separadas por comas convertibles a INT', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de corte siempre es el último día del mes indicado (DATEADD(DAY,-1, DATEADD(MONTH,1, DATEFROMPARTS(Year,Month,1)))); Solo se consideran documentos contables (entradas, salidas, devoluciones, transacciones, depreciaciones, saldos iniciales) con Status=2 (aprobados/confirmados); Los activos reportados siempre tienen AdquisitionDate <= ClosingDate; El HistoricalValue mostrado usa COALESCE(HistoricalValue, FairValue); DepreciatedDays totales = días iniciales + días previos + días del periodo actual; AcumulatedDepreciation = depreciación inicial + depreciaciones previas (no incluye la del periodo actual); Toda la información financiera se calcula en el contexto del LegalBookId solicitado; En caso de error se garantiza la liberación de #Table_Result y se retorna el código ''999''', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #Table_Result: Inserta una fila por activo físico que cumpla AdquisitionDate <= ClosingDate y los filtros aplicables, con descripciones calculadas de tipo de adquisición, estado y depreciación.; [RETURN_RESULT] RESULT: Devuelve SELECT * FROM #Table_Result con todos los activos consolidados; ante error en CATCH retorna una fila con CodeResult=''999'' y mensaje de error con número de línea.; [INSERT] #Temp_PhysicalAssets: Inserta activos cuyo Status efectivo a la fecha de corte se recalcula: si existe FixedAssetActiveOutput o FixedAssetEntryDevolution con Status=2 y DocumentDate posterior al ClosingDate, el activo se considera Status=1; en caso contrario, conserva su Status original.; [INSERT] #Temp_DepreciationAccounts: Selecciona la cuenta de depreciación según AdquisitionType: 3→LoanLeasingAccountId, 7→DepreciationLeasingAccountId, 9→FinancialRentingAccountId, en otro caso DepreciationAccountId.; [INSERT] #Temp_MainAccounts: Solo carga cuentas principales cuyo LegalBookId coincida con el libro solicitado.; [INSERT] #Temp_DepreciationData: Consolida saldos iniciales (FixedAssetInitialBalance Status=2 y DocumentDate<=ClosingDate; o FixedAssetEntry Status=2 y EntryDate<=ClosingDate), valorizaciones/devaluaciones (FixedAssetTransaction Status=2), depreciaciones previas (cierres anteriores al Year/Month) y depreciación del periodo (cierre = Year/Month).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AdquisitionType del activo → Asigna descripción textual (1=Compra Directa, 3=Comodato, 4=Donación, 5=Traspaso Bienes, 6=Otro Concepto, 7=Leasing Financiero, 8=Comodato Tercerizado, 9=Renting Financiero, 10=Renting Operativo) y selecciona la cuenta contable de depreciación correspondiente.; si OutputRefund=1 y Status=0 → StatusDescription=''Devuelto'' else Si Status=1 → ''Activo''; en otro caso → ''Inactivo''; si Existe documento de salida (FixedAssetActiveOutput) o devolución (FixedAssetEntryDevolution) con Status=2 cuya DocumentDate > ClosingDate → El activo se reporta con Status=1 (estaba activo a la fecha de corte) else Se conserva el Status actual del activo (fapa.Status); si @OfficialBook <> 1 y mah.LegalBookId = @LegalBookId → Se incluyen las cuentas homologadas en _MainAccount; cuando el libro es oficial, este CTE no aporta filas.; si fad.ClosingYear < @Year OR (ClosingYear=@Year AND ClosingMonth<@Month) → La depreciación se clasifica como ''Previa'' else Si ClosingYear=@Year y ClosingMonth=@Month se clasifica como depreciación ''Actual'' del periodo; si Cada filtro (@FilterByStatus/AdquisitionTypes/Items/ItemTypes) está activo → Restringe los activos a los Ids proporcionados; el filtro de Status incluye además activos con OutputRefund=1 cuando se filtra por estado=2', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHistoricalPhysicalAsset';
-- GO
