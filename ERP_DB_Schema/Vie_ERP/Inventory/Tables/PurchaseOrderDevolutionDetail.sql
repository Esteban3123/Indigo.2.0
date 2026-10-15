CREATE TABLE [Inventory].[PurchaseOrderDevolutionDetail] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderDevolutionId] INT NOT NULL,
    [PurchaseOrderDetailId]     INT NOT NULL,
    [Quantity]                  INT NOT NULL,
    CONSTRAINT [PK_PurchaseOrderDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDetail] FOREIGN KEY ([PurchaseOrderDetailId]) REFERENCES [Inventory].[PurchaseOrderDetail] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDevolution] FOREIGN KEY ([PurchaseOrderDevolutionId]) REFERENCES [Inventory].[PurchaseOrderDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseOrderDevolutionDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDetail];


GO
ALTER TABLE [Inventory].[PurchaseOrderDevolutionDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDevolution];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a cancelar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de compra que se va a realizar la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

