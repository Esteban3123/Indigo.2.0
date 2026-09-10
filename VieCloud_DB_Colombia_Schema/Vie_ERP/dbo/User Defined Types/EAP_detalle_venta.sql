CREATE TYPE [dbo].[EAP_detalle_venta] AS TABLE (
    [IdProducto]  INT             NULL,
    [PrecioVenta] DECIMAL (18, 2) NULL,
    [Cantidad]    INT             NULL,
    [SubTotal]    DECIMAL (18, 2) NULL);

