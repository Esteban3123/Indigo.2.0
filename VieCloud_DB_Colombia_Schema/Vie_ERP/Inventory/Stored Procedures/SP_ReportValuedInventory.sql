

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de inventario valorizado, mostrando las existencias físicas de productos (medicamentos, insumos y dispositivos médicos) por bodega con sus cantidades y costos convertidos a la moneda seleccionada. Combina el inventario físico (PhysicalInventory) con el catálogo de productos (InventoryProduct), grupos, subgrupos, tipos, clasificación ATC, unidades de medida, unidades de empaque y lotes/seriales (BatchSerial) para ofrecer una vista consolidada del stock valorizado. Admite filtros por producto, bodega, grupo y subgrupo de producto, y permite elegir si se incluyen o excluyen productos con cantidad cero. Para bodegas en consignación, aplica el costo especial de la lista de costos de consignación del proveedor; en caso contrario, usa el costo estándar del producto, con conversión de moneda oficial a la moneda de destino solicitada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportValuedInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportValuedInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de inventario físico valorizado por bodega y producto, aplicando filtros multi-valor (producto, bodega, grupo, subgrupo) y convirtiendo costos/precios desde la moneda oficial a la moneda destino, con tratamiento especial para bodegas de consignación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'GeneralLedger.CompanySettings debe tener configurada la OfficialCurrencyId; Los XML @xmlFilters y @xmlRange deben respetar el esquema /Data con los nodos esperados (Valorization, OrderBy, AccountsZero, TypeReport, ToCurrency / Product, Warehouse, Group, SubGroup); Los valores de Product/Warehouse/Group/SubGroup en el XML deben ser IDs enteros separados por coma (CAST a INT); Debe existir la moneda destino (@ToCurrency) en Common.Currency para que se devuelva el nombre de moneda', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La moneda origen para toda conversión de costos y precios es la OfficialCurrencyId definida en GeneralLedger.CompanySettings; Todas las conversiones monetarias se realizan a la fecha actual (Common.GETDATE()); Si no existe fecha de vencimiento en BatchSerial, se asume ''3000-01-01'' como valor por defecto; Si el producto no tiene FormulationType en ATC, se reporta como 0; Los filtros multi-valor se reciben como cadenas separadas por coma y se materializan vía dbo.Split; Los filtros sólo se aplican cuando el flag respectivo está activo (@FilterByX = 1); de lo contrario no restringen', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'inventario valorizado; bodega de consignación; lista de costos de consignación; lote y fecha de vencimiento; clasificación ATC; grupo y subgrupo de producto; moneda oficial y conversión de divisas; unidad de empaque; unidad de medida (peso, volumen, administración)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PhysicalInventory: Devuelve un resultset con datos de producto, bodega, lote, costos y precios convertidos a @ToCurrency, filtrado por los criterios indicados y por el flag @AccountsZero (productos con cantidad = 0 o <> 0); [RETURN_RESULT] (error handler): En caso de excepción, devuelve un único registro con Code=''999'', el mensaje de error (ERROR_MESSAGE) y la línea (ERROR_LINE) en lugar del reporte', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Product (XML de filtros) no es nulo ni vacío → Activa filtro por producto y carga IDs en tabla temporal vía dbo.Split por coma; si @Warehouse no es nulo ni vacío → Activa filtro por bodega y carga IDs en tabla temporal; si @Group no es nulo ni vacío → Activa filtro por grupo de producto y carga IDs; si @SubGroup no es nulo ni vacío → Activa filtro por subgrupo de producto y carga IDs; si wh.WarehouseConsignment = 1 AND s.ConsignmentInventoryCosting = 1 AND ccd.Status = 1 → Usa el CostNew de la lista de costos de consignación (ConsignmentCostListDetail) convertido a la moneda destino, con fallback a ip.ProductCost si es NULL else Usa ip.ProductCost del producto convertido de la moneda oficial a la moneda destino; si @AccountsZero = 0 → Sólo incluye registros con pi.Quantity <> 0 else Sólo incluye registros con pi.Quantity = 0 (cuentas en cero)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.CurrencyConverterWithDate; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Inventory.InventoryMeasurementUnit; Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Inventory.ProductType; Inventory.ATC; Inventory.BatchSerial; Inventory.PackagingUnit; Common.Currency; Common.Supplier; Inventory.ConsignmentCostListDetail; Inventory.ConsignmentCostList', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportValuedInventory';
-- GO
