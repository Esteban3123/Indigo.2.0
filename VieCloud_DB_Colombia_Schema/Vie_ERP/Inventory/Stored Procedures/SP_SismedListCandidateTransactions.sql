CREATE PROCEDURE [Inventory].[SP_SismedListCandidateTransactions]
    @dateStart DATE,
    @dateEnd DATE
AS
BEGIN
    SET NOCOUNT ON

    -- Compras: origen Inventory.EntranceVoucherDetail. Mismo filtro que SP_ReportSISMEDCompras.
    SELECT
        CAST(1 AS TINYINT) AS TransactionType,
        evd.Id AS SourceDocumentId,
        ip.Id AS ProductId,
        ev.DocumentDate AS TransactionDate
    FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
    JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
    JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
    JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
    JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
    WHERE ev.Status = 2
        AND ev.DocumentDate BETWEEN @dateStart AND @dateEnd
        AND ISNULL(atc.ProductNPT, 0) = 0

    UNION ALL

    -- Venta por servicio clinico: origen Billing.ServiceOrderDetail. Mismo filtro que SP_ReportSISMEDVentas.
    SELECT
        CAST(2 AS TINYINT) AS TransactionType,
        sod.Id AS SourceDocumentId,
        ip.Id AS ProductId,
        i.InvoiceDate AS TransactionDate
    FROM Billing.Invoice i WITH (NOLOCK)
    JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
    JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
    JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
    JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
    JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
    WHERE i.Status = 1
        AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd
        AND ISNULL(atc.ProductNPT, 0) = 0

    UNION ALL

    -- Venta directa de farmacia: origen Inventory.DocumentInvoiceProductSalesDetail. Mismo filtro que SP_ReportSISMEDVentas.
    SELECT
        CAST(3 AS TINYINT) AS TransactionType,
        dipsd.Id AS SourceDocumentId,
        ip.Id AS ProductId,
        i.InvoiceDate AS TransactionDate
    FROM Billing.Invoice i WITH (NOLOCK)
    JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
    JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
    JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON dipsd.ProductId = ip.Id
    JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
    JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
    WHERE i.Status = 1
        AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd
        AND ISNULL(atc.ProductNPT, 0) = 0
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extrae las transacciones de medicamentos candidatas a reporte SISMED (Circular 021 de 2026) en un rango de fechas, a nivel de linea de detalle (una fila por producto), combinando compras (EntranceVoucherDetail), ventas por servicio clinico (ServiceOrderDetail) y ventas directas de farmacia (DocumentInvoiceProductSalesDetail). Reutiliza exactamente los mismos filtros de origen que SP_ReportSISMEDCompras y SP_ReportSISMEDVentas (Circular 006), sin agregar por mes ni aplicar exclusiones regulatorias; esa clasificacion la aplica el proceso de consolidacion en la capa de aplicacion.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SismedListCandidateTransactions';
GO