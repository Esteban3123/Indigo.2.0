-- =============================================
-- Author:      Diego A. Roldán L.
-- Create Date: 2021-11-29
-- Description: Sp para obtener los datos del reporte de orden de produccion
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_ProductionScheduleToReport]
(
    @CampaignDetailId Int
)
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @TableTemp AS TABLE (
		AtcId int
		, SupplyId int
		, CampaignDetailId int 
		, ItemCodeName varchar(500)
		, Concentration varchar(500)
		, BatchCodes varchar(max)
		, ExpirationDate VARCHAR(MAX)
		, IsCold bit
		, IsEnvironment bit
		, ProductionDate datetime
		, ProductionScheduleCode varchar(100)
		, UnitDoseTypeName varchar(500)
		, UserCode varchar(20)
		, CantidadLotes int
		, RequestQuantities int
		, AditionalQuantities int
		, DevolutionQuantities int
		, DispensingQuantities int 
		, CampaignStatus int
		, QFQualityName varchar(500)
		, QFProductionName varchar(500)
		, MSAuxName varchar(500)
		, TLUserName varchar(500)
);

WITH Cte_CampaignValidation AS (
	SELECT 
        cdv.CampaignDetailId,
        pr.ATCId,
        pr.SupplieId,
        bs.BatchCode,
        SUM(cdv.DeliveredQuantity) AS DeliveredQuantity,
        SUM(cdv.DevolutionQuantity) AS DevolutionQuantity,
		cdv.ItemType,
		bs.ExpirationDate 
	FROM  MixingStation.CampaignDetailValidation cdv WITH (NOLOCK)
	INNER JOIN Inventory.InventoryProduct pr WITH (NOLOCK) ON cdv.ProductId = pr.Id
	INNER JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON cdv.BatchSerialId = bs.Id
	WHERE cdv.CampaignDetailId = @CampaignDetailId
	GROUP BY cdv.CampaignDetailId, pr.ATCId, pr.SupplieId, bs.BatchCode, cdv.ItemType, bs.ExpirationDate--, rmdd.Quantity 
),
Cte_CampaigItems AS(
	SELECT	cdi.CampaignDetailId,
			cdi.ItemType,
			cdi.AtcId,
			atc.Name AS AtcName,
			cdi.SupplyId,
			su.SupplieName,
			cdi.ProductId,
			ipr.Name AS ProductName,
			ipr.ATCId ProductAtcId,
			ipr.SupplieId ProductSupplieId,
			CASE 
				WHEN cdi.AtcId IS NOT NULL THEN atc.Concentration 
				WHEN patc.Id IS NOT NULL THEN patc.Concentration
			END AS Concentration,
			CASE 
				WHEN cdi.AtcId IS NOT NULL THEN CONCAT(atc.Code, ' - ', atc.Name)
				WHEN cdi.SupplyId IS NOT NULL THEN CONCAT(su.Code, ' - ', su.SupplieName)
				WHEN patc.Id IS NOT NULL THEN CONCAT(patc.Code, ' - ', patc.Name)
				WHEN psu.Id IS NOT NULL THEN CONCAT(psu.Code, ' - ', psu.SupplieName)
				ELSE CONCAT(ipr.Code, ' - ', ipr.Name)
			END AS ItemCodeName,
			-------------------------------
			SUM(cdi.RequestQuantity) AS RequestQuantity 
	FROM MixingStation.CampaignDetailItems cdi WITH(NOLOCK)
	LEFT JOIN Inventory.ATC atc WITH(NOLOCK) ON cdi.AtcId = atc.Id
	LEFT JOIN Inventory.InventorySupplie su WITH(NOLOCK) ON cdi.SupplyId = su.Id
	LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON cdi.ProductId = ipr.Id
	LEFT JOIN Inventory.ATC patc WITH(NOLOCK) ON ipr.ATCId = patc.Id
	LEFT JOIN Inventory.InventorySupplie psu WITH(NOLOCK) ON ipr.SupplieId = psu.Id 
	WHERE cdi.CampaignDetailId = @CampaignDetailId
	GROUP BY cdi.CampaignDetailId, cdi.ItemType, cdi.AtcId, cdi.SupplyId, cdi.ProductId,
			 atc.Code, atc.Name, su.Code, su.SupplieName, ipr.Code, ipr.Name, ipr.ATCId, 
			 ipr.SupplieId, atc.Concentration, patc.Id, patc.Code, patc.Name, 
			 patc.Concentration, psu.Id, psu.Code, psu.SupplieName
),
cte_campaignUser AS (
    SELECT 
        cdu.Id,
        cdu.UserId,
        su.UserCode,
        p.Identification,
        p.Fullname,
        cdu.CampaignDetailId,
        cdu.UserRole
    FROM MixingStation.CampaignDetailUsers cdu WITH(NOLOCK)
    JOIN [Security].[User] su ON cdu.UserId = su.Id
    JOIN [Security].Person p ON su.IdPerson = p.Id
)

INSERT INTO @TableTemp(
	AtcId, 
	SupplyId, 
	CampaignDetailId , 
	ItemCodeName, 
	Concentration, 
	BatchCodes, 
	ExpirationDate, 
	IsCold,
	IsEnvironment, 
	ProductionDate,
	ProductionScheduleCode, 
	UnitDoseTypeName,
	UserCode, 
	CantidadLotes,
	RequestQuantities,
	AditionalQuantities, 
	DevolutionQuantities,
	DispensingQuantities,
	CampaignStatus,
	QFQualityName, 
	QFProductionName, 
	MSAuxName, 
	TLUserName 
)
SELECT 
	  ISNULL(cdi.ProductAtcId, cdi.AtcId) AS AtcId,
	  ISNULL(cdi.ProductSupplieId, cdi.SupplyId) AS SupplyId,
	  cd.Id AS CampaignDetailId,
	  cdi.ItemCodeName,
	  cdi.Concentration,
	  STRING_AGG(cv.BatchCode, ', ') AS BatchCode,
	  STRING_AGG(cv.ExpirationDate, ', ') AS ExpirationDate,
	  (SELECT TOP 1 IIF(Storage > 3, 1, 0) FROM Inventory.InventoryProduct WITH(NOLOCK) WHERE ISNULL(cdi.ProductAtcId, cdi.AtcId) = ATCId) AS IsCold,--revisar
	  (SELECT TOP 1 IIF(Storage < 3, 1, 0) FROM Inventory.InventoryProduct WITH(NOLOCK) WHERE ISNULL(cdi.ProductSupplieId, cdi.SupplyId) = SupplieId) AS IsEnvironment,
	  ps.CreationDate AS ProductionDate,
	  ps.Code AS ProductionScheduleCode,
	  udt.Description AS UnitDoseTypeName,
	  ps.CreationUser AS UserCode,
	  COUNT(cv.BatchCode) AS CantidadLotes,
	  IIF(cdi.ItemType <> 4, cdi.RequestQuantity, 0) AS RequestQuantities,
	  IIF(cdi.ItemType = 4, cdi.RequestQuantity, 0) AS AditionalQuantities,
	  SUM(cv.DevolutionQuantity) AS DevolutionQuantities,
	  SUM(cv.DeliveredQuantity) AS DispensingQuantities,
	  cd.CampaignStatus,
	  qfu.Fullname AS QFQualityName,
	  qfp.Fullname AS QFProductionName,
	  aux.Fullname AS MSAuxName,
	  tl.Fullname AS TLUserName
FROM MixingStation.ProductionSchedule ps WITH(NOLOCK)
INNER JOIN MixingStation.ProductionScheduleDetail psd WITH(NOLOCK) ON psd.ProductionScheduleId = ps.Id
INNER JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) ON psd.CampaignDetailId = cd.Id
INNER JOIN MixingStation.UnitDoseType udt WITH (NOLOCK) ON cd.UnitDoseTypeId = udt.Id
INNER JOIN Cte_CampaigItems cdi ON cd.Id = cdi.CampaignDetailId 
LEFT JOIN Cte_CampaignValidation cv ON cv.CampaignDetailId = cd.Id AND cdi.ItemType = cv.ItemType AND 
			(ISNULL(cdi.ProductAtcId, cdi.AtcId) = cv.ATCId OR ISNULL(cdi.ProductSupplieId, cdi.SupplyId) = cv.SupplieId)
OUTER APPLY (SELECT TOP 1 * FROM cte_campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId AND cdu.UserRole = 1 ) AS qfu
OUTER APPLY (SELECT TOP 1 * FROM cte_campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId AND cdu.UserRole = 2 ) AS qfp
OUTER APPLY (SELECT TOP 1 * FROM cte_campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId AND cdu.UserRole = 3 ) AS aux
OUTER APPLY (SELECT TOP 1 * FROM cte_campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId AND cdu.UserRole = 4 ) AS tl
WHERE psd.CampaignDetailId = @CampaignDetailId
GROUP BY cd.Id,
		cdi.ProductAtcId, cdi.AtcId,
		cdi.ProductSupplieId, cdi.SupplyId,
		cdi.ItemType,
		cdi.Concentration,
		cdi.ItemCodeName,
		cdi.RequestQuantity,
		ps.CreationDate,
		ps.Code,
		udt.Description,
		ps.CreationUser,
		cd.CampaignStatus,
		qfu.Fullname,
		qfp.Fullname,
		aux.Fullname,
		tl.Fullname

SELECT *
FROM @TableTemp

END