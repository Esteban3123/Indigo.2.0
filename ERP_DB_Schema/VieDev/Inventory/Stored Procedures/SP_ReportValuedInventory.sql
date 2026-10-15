


CREATE PROCEDURE [Inventory].[SP_ReportValuedInventory]
	@xmlFilters AS XML,
	@xmlRange AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY


	DECLARE @Valorization int,
			@OrderBy int,
			@AccountsZero BIT,
			@TypeReport int,
			@ToCurrency int,
			--------------------------------------------
			@Product VARCHAR(MAX),
			@Warehouse VARCHAR(MAX),
			@Group VARCHAR(MAX),
			@SubGroup VARCHAR(MAX),
			@FilterByProduct BIT = 0,
			@FilterByWareHouse BIT = 0,
			@FilterByGroup BIT = 0,
			@FilterBySubGroup BIT = 0,
			@OfficialCurrency int
			---------------------  Tablas que almacen valores multiples  ----------------------
	DECLARE @Table_Product AS TABLE(Id INT)
	DECLARE @Table_Warehouse AS TABLE(Id INT)
	DECLARE @Table_Group AS TABLE(Id INT)
	DECLARE @Table_Subgroup AS TABLE(Id INT)
	


	BEGIN TRY
		--se obtiene la moneda oficial 
		select @OfficialCurrency = OfficialCurrencyId from GeneralLedger.CompanySettings
		
		--Se obtienen los datos de los criterios
		SELECT	@Valorization = t.x.value('Valorization[1]','int'),
				@OrderBy = t.x.value('OrderBy[1]','INT'),
				@AccountsZero = T.x.value('AccountsZero[1]', 'BIT'),
				@TypeReport = T.x.value('TypeReport[1]', 'INT'),
				@ToCurrency = t.x.value('ToCurrency[1]','int')
		FROM @xmlFilters.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@Product = t.x.value('Product[1]','VARCHAR(MAX)'),
				@Warehouse = t.x.value('Warehouse[1]','VARCHAR(MAX)'),
				@Group = t.x.value('Group[1]','VARCHAR(MAX)'),
				@SubGroup = t.x.value('SubGroup[1]','VARCHAR(MAX)')
		FROM @xmlRange.nodes('/Data') t(x)
		

		IF ISNULL(@Product, '') <> ''
			BEGIN
				SET @FilterByProduct = 1

				INSERT INTO @Table_Product
					SELECT CAST(Data AS INT) Data
					FROM dbo.Split(@Product, ',')
			END

		IF ISNULL(@Warehouse, '') <> ''
			BEGIN
				SET @FilterByWareHouse = 1

				INSERT INTO @Table_Warehouse
					SELECT CAST(Data AS INT) Data
					FROM dbo.Split(@Warehouse, ',')
			END

		IF ISNULL(@Group, '') <> ''
			BEGIN
				SET @FilterByGroup = 1

				INSERT INTO @Table_Group
					SELECT CAST(Data AS INT) Data
					FROM dbo.Split(@Group, ',')
			END

		IF ISNULL(@SubGroup, '') <> ''
			BEGIN
				SET @FilterBySubGroup = 1

				INSERT INTO @Table_SubGroup
					SELECT CAST(Data AS INT) Data
					FROM dbo.Split(@SubGroup, ',')
			END

		--se crea cte de la tabla InventoryMeasurementUnit
		;WITH cteInventoryMeasurementUnit AS (
			SELECT *
			FROM Inventory.InventoryMeasurementUnit
		)

		SELECT
			@OrderBy OrderBy,
			@Valorization Valorization,
			ip.Code ProductCode,
			ip.Name ProductName,
			Common.CurrencyConverterWithDate(ip.FinalProductCost, @OfficialCurrency,@ToCurrency,Common.GETDATE()) FinalProductCost,
			Common.CurrencyConverterWithDate(ip.SellingPrice, @OfficialCurrency,@ToCurrency,Common.GETDATE()) SellingPrice,
			IIF( wh.WarehouseConsignment = 1 AND s.ConsignmentInventoryCosting = 1 and ccd.Status = 1 ,
				isnull(Common.CurrencyConverterWithDate(ccd.CostNew, @OfficialCurrency, @ToCurrency, Common.GETDATE()), ip.ProductCost),        
				Common.CurrencyConverterWithDate(ip.ProductCost, @OfficialCurrency, @ToCurrency, Common.GETDATE())) ProductCost, 
			pi.Quantity Quantity,
			ip.PackagingUnitId ProductPackagingUnitId,
			pu.Name ProductPackagingUnitName,
			ip.ATCId ProductATCId, 
			atc.Concentration ATCConcentration,
			pg.Code ProductGroupCode,
			pg.Name ProductGroupName,
			psg.Code ProductSubGroupCode,
			psg.Name ProductSubGroupName,
			pt.Class ProductTypeClass,
			imu.Name MeasurementUnitName,
			isnull(atc.FormulationType,0) ATCFormulationType,
			imu2.Name WeightMeasureUnitName,
			imu3.Name VolumeMeasureUnitName,
			imu4.Name AdministrationUnitName,
			CONCAT(wh.Code, ' - ', wh.Name) WarehouseCodeName,
			isnull(bs.ExpirationDate, cast('3000-01-01' as Date)) ExpirationDate,
			cu.Name CurrencyName
		FROM Inventory.PhysicalInventory pi
		JOIN Inventory.Warehouse wh on pi.WarehouseId = wh.Id
		JOIN Inventory.InventoryProduct ip on pi.ProductId = ip.Id
		JOIN Inventory.ProductGroup pg on ip.ProductGroupId = pg.Id
		JOIN Inventory.ProductSubGroup psg on ip.ProductSubGroupId = psg.Id
		JOIN Inventory.ProductType pt on ip.ProductTypeId = pt.Id
		LEFT JOIN Inventory.ATC atc on ip.ATCId =atc.Id
		LEFT JOIN Inventory.BatchSerial bs on pi.BatchSerialId = bs.Id
		LEFT JOIN Inventory.PackagingUnit pu on ip.PackagingUnitId = pu.Id
		LEFT JOIN Inventory.InventoryMeasurementUnit imu on ip.MeasurementUnitId = imu.Id --REVISAR CTE
		LEFT JOIN cteInventoryMeasurementUnit imu2 on atc.WeightMeasureUnit = imu2.Id --REVISAR CTE
		LEFT JOIN cteInventoryMeasurementUnit imu3 on atc.VolumeMeasureUnit = imu3.Id --REVISAR CTE
		LEFT JOIN cteInventoryMeasurementUnit imu4 on atc.AdministrationUnitId = imu4.Id --REVISAR CTE
		LEFT JOIN @Table_Product tp ON ip.Id = tp.Id
		LEFT JOIN @Table_Warehouse twh ON wh.Id = twh.Id
		LEFT JOIN @Table_Group tg ON pg.Id = tg.Id
		LEFT JOIN @Table_Subgroup tsg ON psg.Id = tsg.Id
		LEFT JOIN Common.Currency cu on cu.Id = @ToCurrency
		LEFT JOIN Common.Supplier s ON s.Id = wh.SupplierId		
		LEFT JOIN ( select ccl.SupplierId,ccd.ProductId, ccd.CostNew, ccl.Status 
					FROM  [Inventory].[ConsignmentCostListDetail] ccd
					JOIN Inventory.ConsignmentCostList ccl ON ccl.Id = ccd.ConsignmentCostListId        
		) ccd ON ccd.SupplierId = wh.SupplierId and ip.Id = ccd.ProductId
		where (@FilterByProduct = 0 OR tp.Id IS NOT NULL)
		AND (@FilterByWareHouse = 0 OR twh.Id IS NOT NULL)
		AND (@FilterByGroup = 0 OR tg.Id IS NOT NULL)
		AND (@FilterBySubGroup = 0 OR tsg.Id IS NOT NULL)
		AND ((@AccountsZero = 0 AND pi.Quantity <> 0) OR (@AccountsZero = 1 AND pi.Quantity = 0))
		
	END TRY
	BEGIN CATCH
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END