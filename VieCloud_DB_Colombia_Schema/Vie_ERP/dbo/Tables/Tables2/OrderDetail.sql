CREATE TABLE [dbo].[OrderDetail] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderId]           INT     NOT NULL,
    [SupplierId]        INT     NOT NULL,
    [WarehouseId]       INT     NOT NULL,
    [QuantityOrdered]   INT     NOT NULL,
    [QuantityDelivered] INT     NOT NULL,
    [OutstandingAmount] INT     NOT NULL,
    [Status]            TINYINT CONSTRAINT [DF_OrderDetail_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_OrderDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_OrderDetail_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([Id]),
    CONSTRAINT [FK_OrderDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[Warehouse] ([Id])
);




GO



GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de órdenes de compra o suministro: registra cada línea de una orden indicando el proveedor, el almacén destino, las cantidades solicitadas y entregadas, y el saldo pendiente de entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle de orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la orden de compra o pedido al que pertenece este detalle.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'OrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'OrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proveedor o suministrador asociado a esta línea de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacén o bodega destino donde se recibe o despacha la mercancía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades solicitadas o pedidas en la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'QuantityOrdered';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'QuantityOrdered';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades ya entregadas o recibidas hasta el momento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'QuantityDelivered';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'QuantityDelivered';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por entregar o saldo de unidades aún no recibidas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la línea de detalle (por ejemplo: activa, completada, cancelada); valor por defecto 1 (activo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDetail', @level2type = N'COLUMN', @level2name = N'Status';
