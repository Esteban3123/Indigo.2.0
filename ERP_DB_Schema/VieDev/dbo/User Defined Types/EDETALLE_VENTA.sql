CREATE TYPE [dbo].[EDETALLE_VENTA] AS TABLE (
    [IdProducto]  INT             NULL,
    [PrecioVenta] DECIMAL (20, 3) NULL,
    [Cantidad]    INT             NULL,
    [MontoTotal]  DECIMAL (20, 3) NULL);

