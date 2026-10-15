CREATE TYPE [dbo].[EAP_detalle_compra] AS TABLE (
    [IdProducto]   INT             NULL,
    [PrecioCompra] DECIMAL (18, 2) NULL,
    [PrecioVenta]  DECIMAL (18, 2) NULL,
    [Cantidad]     INT             NULL,
    [MontoTotal]   DECIMAL (18, 2) NULL);

