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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de una orden de producción de mezclas farmacéuticas (campaña de preparación en estación de mezclas), identificada por su ID de detalle de campaña. Consolida en un único resultado los ítems solicitados (medicamentos ATC, insumos y productos de inventario), los lotes dispensados con sus fechas de vencimiento, las cantidades pedidas, adicionales, devueltas y dispensadas, las condiciones de almacenamiento (cadena de frío o temperatura ambiente), el código y fecha de la programación de producción, el tipo de dosis unitaria, y los nombres de los usuarios responsables por rol (Químico Farmacéutico de calidad, QF de producción, auxiliar y técnico líder). Se usa para imprimir o exportar el reporte formal de la orden de producción que acompaña cada campaña de preparación magistral o dosis unitaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProductionScheduleToReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProductionScheduleToReport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información detallada de una orden/programa de producción de la estación de mezclas (ítems solicitados, lotes validados, cantidades, almacenamiento y usuarios responsables por rol) para alimentar el reporte impreso de orden de producción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un CampaignDetail vinculado a un ProductionScheduleDetail para el @CampaignDetailId recibido; en caso contrario el resultado será vacío.; Los productos referenciados en CampaignDetailItems deben estar registrados en Inventory.InventoryProduct, Inventory.ATC o Inventory.InventorySupplie para poder componer ItemCodeName y Concentration.; Los usuarios asignados en CampaignDetailUsers deben tener correspondencia en Security.User y Security.Person para resolver nombres por rol.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte se acota siempre al @CampaignDetailId recibido tanto en CampaignDetailValidation, CampaignDetailItems como en ProductionScheduleDetail.; Las cantidades entregadas y devueltas se agregan SUM por (CampaignDetail, ATC/Supply, ItemType, BatchCode, ExpirationDate), garantizando totales por lote.; BatchCodes y ExpirationDate se devuelven como cadenas concatenadas (STRING_AGG con '', '') de todos los lotes validados del ítem.; Por cada rol (QF Calidad, QF Producción, Auxiliar MS, Líder de Turno) se devuelve a lo sumo un usuario por CampaignDetail (TOP 1).; El emparejamiento ítem-validación se hace por ItemType y por coincidencia de ATCId o SupplieId resueltos (ProductAtcId/AtcId o ProductSupplieId/SupplyId), nunca cruzando tipos.; Las consultas se realizan con NOLOCK en todas las tablas, asumiendo lectura sucia aceptable para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden/Programa de producción (ProductionSchedule); Campaña de preparación de mezclas; Detalle de campaña e ítems solicitados; Validación de entregas y devoluciones por lote; Clasificación ATC de medicamentos; Insumos / dispositivos médicos; Lote y fecha de vencimiento (BatchSerial); Cadena de frío vs almacenamiento ambiente (Storage); Dosis unitaria (UnitDoseType); Roles de usuario en estación de mezclas: QF Calidad, QF Producción, Auxiliar MS, Líder de Turno; Cantidades solicitadas, adicionales, devueltas y dispensadas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableTemp: Por cada combinación (CampaignDetail, ítem ATC/Insumo/Producto) ligada al ProductionSchedule del @CampaignDetailId, se inserta una fila consolidada con cantidades agregadas y datos de usuarios por rol.; [RETURN_RESULT] @TableTemp: Al final se devuelve SELECT * FROM @TableTemp como resultset del reporte de orden de producción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cdi.AtcId IS NOT NULL → Concentration y ItemCodeName se toman del catálogo Inventory.ATC (atc.Concentration y CONCAT(atc.Code,'' - '',atc.Name)). else Si cdi.SupplyId IS NOT NULL se usa Inventory.InventorySupplie; en caso contrario se resuelve vía el ATC/Supply asociado al InventoryProduct (patc/psu) y, en último caso, vía el propio InventoryProduct (ipr.Code - ipr.Name).; si cdi.ItemType <> 4 → La cantidad solicitada se imputa a RequestQuantities (cantidad regular). else Si ItemType = 4 la cantidad se imputa a AditionalQuantities (cantidad adicional).; si InventoryProduct.Storage > 3 para el ATC del ítem → IsCold = 1 (producto requiere cadena de frío). else IsCold = 0.; si InventoryProduct.Storage < 3 para el Supply del ítem → IsEnvironment = 1 (producto se almacena a temperatura ambiente). else IsEnvironment = 0.; si CampaignDetailUsers.UserRole = 1/2/3/4 → Se asigna el Fullname respectivamente a QFQualityName (1, QF Calidad), QFProductionName (2, QF Producción), MSAuxName (3, Auxiliar MS) y TLUserName (4, Líder de Turno) tomando TOP 1 por rol.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetailValidation; Inventory.InventoryProduct; Inventory.BatchSerial; MixingStation.CampaignDetailItems; Inventory.ATC; Inventory.InventorySupplie; MixingStation.CampaignDetailUsers; Security.User; Security.Person; MixingStation.ProductionSchedule; MixingStation.ProductionScheduleDetail; MixingStation.CampaignDetail; MixingStation.UnitDoseType', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductionScheduleToReport';
-- GO
