CREATE PROCEDURE [MixingStation].[SP_ProductListCampaingDetails]
(
    @ATCEntityId INT,
	@StockId INT,
	@warehouseId INT,
	@PharmaceuticalFormId INT,
	@RequestMeasurementUnitCode VARCHAR(20),
	@AtcId INT,
	@Type INT,
	@MSClass INT
)
AS
BEGIN
    SET NOCOUNT ON
	SELECT DISTINCT atc.Id, 
		atc.Code AS ProductCode, 
		atc.NAME AS ProductName,
        atc.AbbreviationName AS ProductAbbreviationName,
		CAST((CASE 
				WHEN atc.FormulationType IN (1, 3) AND @Type = 1 AND @MSClass <> 2
					THEN atc.Weight
				ELSE COALESCE(atc.Volume, atc.Weight, 1)
				END) AS DECIMAL(18,2)
			) AS Concentration,
		MixingStation.CalculationQuantityMaterialRaw(
			FormulationType, 
			atc.Code, 
			(CASE WHEN atc.FormulationType = 2 THEN atc.Volume ELSE atc.Weight END), 
			@RequestMeasurementUnitCode
		) AS Dosis
		FROM  Inventory.ATC atc
		JOIN Inventory.InventoryProduct B WITH(NOLOCK) ON B.Status = 1 AND B.ATCId = atc.Id
		JOIN Inventory.ProductType pt WITH(NOLOCK) ON b.ProductTypeId = pt.Id
		JOIN Inventory.PhysicalInventory phy WITH(NOLOCK) ON phy.ProductId = B.Id
		JOIN Inventory.Warehouse E WITH(NOLOCK) ON phy.WarehouseId = E.Id
	WHERE ((@AtcId IS NOT NULL AND atc.Id = @AtcId) OR (@AtcId IS NULL AND atc.ATCEntityId = @ATCEntityId))
		AND atc.PharmaceuticalFormId = @PharmaceuticalFormId 
		AND E.Id IN (@StockId, @warehouseId) AND pt.Class <> 5 AND phy.Quantity > 0
	ORDER BY Concentration DESC

END