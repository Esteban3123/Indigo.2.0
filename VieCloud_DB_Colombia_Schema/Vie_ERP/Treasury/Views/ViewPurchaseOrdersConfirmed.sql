CREATE VIEW Treasury.ViewPurchaseOrdersConfirmed
AS
    /* INVENTARIOS */
    SELECT
        po.Id,
        SourceModule                = N'Inventario',
        Code                        = po.Code,
        OrderDate                   = po.DocumentDate,
        DeliverDate                 = po.DeliveredDate,
        SupplierId                  = s.Id,
        IdThirdParty                = s.IdThirdParty,
        SupplierName                = s.Name,
        SupplierDistributionLineId  = po.SupplierDistributionLineId,
        Status                      = po.Status,
        StatusName                  = CASE po.Status
                                        WHEN 1 THEN N'Registrado'
                                        WHEN 2 THEN N'Confirmado'
                                        WHEN 3 THEN N'Anulado'
                                        ELSE N''
                                      END,
        TotalValue                  = po.TotalValue
    FROM Inventory.PurchaseOrder AS po
    INNER JOIN Common.SuppliersDistributionLines AS sdl
        ON sdl.Id = po.SupplierDistributionLineId
    INNER JOIN Common.Supplier AS s
        ON s.Id = sdl.IdSupplier
    WHERE po.Status = 2

    UNION ALL

    /* ACTIVOS FIJOS */
    SELECT
        fpo.Id,
        SourceModule                = N'Activos Fijos',
        Code                        = fpo.Code,
        OrderDate                   = fpo.PurchaseOrderDate,
        DeliverDate                 = fpo.DeliverDate,
        SupplierId                  = s.Id,
        IdThirdParty                = s.IdThirdParty,
        SupplierName                = s.Name,
        SupplierDistributionLineId  = fpo.SupplierDistributionLineId,
        Status                      = fpo.Status,
        StatusName                  = CASE fpo.Status
                                        WHEN 1 THEN N'Registrado'
                                        WHEN 2 THEN N'Confirmado'
                                        WHEN 3 THEN N'Anulado'
                                        ELSE N''
                                      END,
        TotalValue                  = CAST(NULL AS DECIMAL(18,2))
    FROM FixedAsset.FixedAssetPurchaseOrder AS fpo
    INNER JOIN Common.SuppliersDistributionLines AS sdl
        ON sdl.Id = fpo.SupplierDistributionLineId
    INNER JOIN Common.Supplier AS s
        ON s.Id = sdl.IdSupplier
    WHERE fpo.Status = 2;

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en una sola lista las órdenes de compra confirmadas provenientes de Inventario y de Activos Fijos junto con datos del proveedor, para consumo desde Tesorería.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener estado Confirmado (Status = 2); Cada orden debe tener una línea de distribución de proveedor asociada existente; La línea de distribución debe referenciar un proveedor válido', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes confirmadas (Status = 2), aunque el CASE contempla otros estados; Las órdenes de Activos Fijos nunca exponen TotalValue (siempre NULL); Toda fila incluye obligatoriamente proveedor (SupplierId, IdThirdParty, SupplierName) por los INNER JOIN; El módulo de origen queda identificado mediante la constante SourceModule', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Orden de compra de activos fijos; Proveedor; Tercero; Línea de distribución de proveedor; Estado de orden (Registrado/Confirmado/Anulado); Fecha de entrega; Inventario; Activos Fijos', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve la unión (UNION ALL) de órdenes de Inventory.PurchaseOrder y FixedAsset.FixedAssetPurchaseOrder con Status = 2, etiquetadas por SourceModule (''Inventario'' o ''Activos Fijos'')', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si po.Status / fpo.Status = 1 → StatusName = ''Registrado''; si po.Status / fpo.Status = 2 → StatusName = ''Confirmado''; si po.Status / fpo.Status = 3 → StatusName = ''Anulado''; si Status fuera de {1,2,3} → StatusName = '''' (cadena vacía); si Origen del registro = FixedAsset.FixedAssetPurchaseOrder → TotalValue se expone como NULL (DECIMAL(18,2)) else Para Inventario se expone el TotalValue real de la orden', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseOrder; Common.SuppliersDistributionLines; Common.Supplier; FixedAsset.FixedAssetPurchaseOrder', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewPurchaseOrdersConfirmed';
GO
