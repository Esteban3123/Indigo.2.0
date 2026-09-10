CREATE TYPE [dbo].[EDETALLE_COMPRA] AS TABLE (
    [IdProducto]   INT             NULL,
    [PrecioCompra] DECIMAL (20, 3) NULL,
    [PrecioVenta]  DECIMAL (20, 3) NULL,
    [Cantidad]     INT             NULL,
    [MontoTotal]   DECIMAL (20, 3) NULL);

