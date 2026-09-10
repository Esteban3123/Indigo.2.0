CREATE PROCEDURE [Inventory].[SP_SismedGetDetailRecordData]
    @dateStart DATE,
    @dateEnd DATE
AS
BEGIN
    SET NOCOUNT ON

    -- Compras: origen Inventory.EntranceVoucherDetail. Mismos joins/filtros que SP_SismedListCandidateTransactions.
    SELECT
        CAST(1 AS TINYINT) AS TransactionType,
        evd.Id AS SourceDocumentId,
        ip.Id AS ProductId,
        ip.Code AS ProductCode,
        ip.HealthRegistration AS HealthRegistration,
        ev.InvoiceNumber AS InvoiceNumber,
        ev.InvoiceDate AS InvoiceDate,
        ev.Cufe AS CUFE,
        evd.Quantity AS Quantity,
        evd.UnitValue AS UnitValue,
        evd.TotalValue AS TotalValue,
        tp.Nit AS ThirdPartyNit,
        tp.CodeDivipola AS ThirdPartyCityCode
    FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
    JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
    JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
    JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
    JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
    JOIN Common.Supplier sup WITH (NOLOCK) ON ev.SupplierId = sup.Id
    JOIN Common.ThirdParty tp WITH (NOLOCK) ON sup.IdThirdParty = tp.Id
    WHERE ev.Status = 2
        AND ev.DocumentDate BETWEEN @dateStart AND @dateEnd
        AND ISNULL(atc.ProductNPT, 0) = 0

    UNION ALL

    -- Venta por servicio clinico: origen Billing.ServiceOrderDetail. Mismos joins/filtros que SP_SismedListCandidateTransactions.
    SELECT
        CAST(2 AS TINYINT) AS TransactionType,
        sod.Id AS SourceDocumentId,
        ip.Id AS ProductId,
        ip.Code AS ProductCode,
        ip.HealthRegistration AS HealthRegistration,
        i.InvoiceNumber AS InvoiceNumber,
        i.InvoiceDate AS InvoiceDate,
        i.CUFE AS CUFE,
        sod.InvoicedQuantity AS Quantity,
        sod.RateManualSalePrice AS UnitValue,
        sod.GrandTotalSalesPrice AS TotalValue,
        tp.Nit AS ThirdPartyNit,
        tp.CodeDivipola AS ThirdPartyCityCode
    FROM Billing.Invoice i WITH (NOLOCK)
    JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
    JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
    JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
    JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
    JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
    JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.Id
    WHERE i.Status = 1
        AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd
        AND ISNULL(atc.ProductNPT, 0) = 0

    UNION ALL

    -- Venta directa de farmacia: origen Inventory.DocumentInvoiceProductSalesDetail. Mismos joins/filtros que SP_SismedListCandidateTransactions.
    SELECT
        CAST(3 AS TINYINT) AS TransactionType,
        dipsd.Id AS SourceDocumentId,
        ip.Id AS ProductId,
        ip.Code AS ProductCode,
        ip.HealthRegistration AS HealthRegistration,
        i.InvoiceNumber AS InvoiceNumber,
        i.InvoiceDate AS InvoiceDate,
        i.CUFE AS CUFE,
        dipsd.Quantity AS Quantity,
        dipsd.SalePrice AS UnitValue,
        dipsd.TotalValue AS TotalValue,
        tp.Nit AS ThirdPartyNit,
        tp.CodeDivipola AS ThirdPartyCityCode
    FROM Billing.Invoice i WITH (NOLOCK)
    JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
    JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
    JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON dipsd.ProductId = ip.Id
    JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
    JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
    JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.Id
    WHERE i.Status = 1
        AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd
        AND ISNULL(atc.ProductNPT, 0) = 0
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extrae los datos fuente necesarios para construir el Registro de Detalle MED101 (Circular 021 de 2026) en un rango de fechas, a nivel de linea de detalle. Reutiliza exactamente los mismos joins y filtros de origen que SP_SismedListCandidateTransactions, agregando las columnas de producto, factura, cantidades, valores y tercero requeridas por el anexo tecnico MED101.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SismedGetDetailRecordData';
GO
