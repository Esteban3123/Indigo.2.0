

CREATE VIEW [FixedAsset].[VReportPurchaseOrder]
AS
SELECT 
    pod.Id AS idDetailPurchaseOrder, 
    po.Id AS idPurchaseOrder, 
    po.Code AS codePurchaseOrder, 
    po.PurchaseOrderDate AS datePurchaseOrder, 
    po.CreationUser AS CreationUser,
    po.[Status] AS statusPurchaseOrder, 
    po.Detail AS detailPurchaseOrder, 
    fe.Code AS codeEquipment, 
    fe.[Description] AS descriptionEquipment,
    pod.Quantity AS Quantity, 
    pod.UnitValue, 
    pod.TotalValue, 
    pod.IvaValue, 
    po.DeliverDate AS dateDeliver, 
    c.Code + ' - ' + c.Name AS citySupplier,
    tp.Nit + ' - ' + tp.Name AS descriptionThirdParty, 
    (SELECT TOP 1 Addresss 
     FROM Common.Address AS Ad 
     WHERE Ad.IdPerson = tp.PersonId) AS ThirdPartyAddress,
    (SELECT TOP 1 Phone 
     FROM Common.Phone AS Ph 
     WHERE Ph.IdPerson = tp.PersonId) AS ThirdPartyPhone,
    po.CurrencyId
FROM FixedAsset.FixedAssetPurchaseOrder po
INNER JOIN FixedAsset.FixedAssetPurchaseOrderItem pod ON pod.PurchaseOrderId = po.Id
INNER JOIN FixedAsset.FixedAssetItem fe ON fe.Id = pod.ItemId
INNER JOIN Common.SuppliersDistributionLines sdl ON sdl.Id = po.SupplierDistributionLineId
INNER JOIN Common.Supplier s ON s.Id = sdl.IdSupplier
INNER JOIN Common.ThirdParty tp ON tp.Id = s.IdThirdParty
INNER JOIN Common.Person p ON p.Id = tp.PersonId
INNER JOIN Common.City c ON c.Id = s.IdCity
INNER JOIN Common.Currency cu ON cu.Id = po.CurrencyId;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de órdenes de compra de activos fijos. Consolida en una sola consulta la cabecera de cada orden de compra (código, fecha, estado, detalle, fecha de entrega, moneda y usuario que la creó) junto con cada ítem o línea de detalle (equipo solicitado, cantidad, valor unitario, valor total e IVA). Enriquece la información con los datos del proveedor asociado: NIT, razón social, ciudad, dirección y teléfono, obtenidos a través de la cadena proveedor → tercero → persona. Sirve para reportería y seguimiento de compras de bienes de capital, permitiendo responder preguntas como ''¿qué equipos se compraron?'', ''¿a qué proveedor?'', ''¿cuánto costó?'' y ''¿cuál es el estado de la orden?''.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportPurchaseOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportPurchaseOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto los datos de las órdenes de compra de activos fijos junto con sus ítems, proveedor (tercero, ciudad, dirección y teléfono) y equipo, para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada orden debe tener al menos un ítem asociado (INNER JOIN con la tabla de ítems); La orden debe estar vinculada a una línea de distribución de proveedor existente; El proveedor debe estar asociado a un tercero y a una ciudad válidos; El tercero debe tener una persona asociada; La orden debe tener una moneda registrada en el catálogo de monedas', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen órdenes que tengan ítems, proveedor con línea de distribución, tercero, persona, ciudad y moneda; cualquier ausencia excluye la orden del reporte (todos los joins son INNER); Para cada tercero se reporta únicamente una dirección (TOP 1) y un teléfono (TOP 1), sin criterio de orden explícito; La descripción del proveedor se construye concatenando NIT y nombre del tercero; La ciudad del proveedor se presenta concatenando código y nombre', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Activo fijo; Ítem/Equipo; Proveedor; Tercero; Línea de distribución de proveedor; Ciudad; Moneda; Dirección de contacto; Teléfono de contacto; IVA; Fecha de entrega', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.VReportPurchaseOrder: Devuelve una fila por cada ítem de orden de compra de activo fijo, enriquecida con datos de proveedor, tercero, ciudad, primera dirección y primer teléfono de la persona del tercero', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPurchaseOrder; FixedAsset.FixedAssetPurchaseOrderItem; FixedAsset.FixedAssetItem; Common.SuppliersDistributionLines; Common.Supplier; Common.ThirdParty; Common.Person; Common.City; Common.Currency; Common.Address; Common.Phone', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportPurchaseOrder';
GO
