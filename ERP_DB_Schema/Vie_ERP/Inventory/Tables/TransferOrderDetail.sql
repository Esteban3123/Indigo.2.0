CREATE TABLE [Inventory].[TransferOrderDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferOrderId]               INT             NOT NULL,
    [InventoryRequestDetailId]      INT             NULL,
    [ProductId]                     INT             NOT NULL,
    [InventoryQuantity]             INT             NOT NULL,
    [Quantity]                      INT             NOT NULL,
    [Value]                         DECIMAL (18, 2) CONSTRAINT [DF_TransferOrderDetail_Value] DEFAULT ((0)) NOT NULL,
    [Description]                   VARCHAR (300)   NULL,
    [InventoryRequestDetailOtherId] INT             NULL,
    CONSTRAINT [PK_TransferOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KindsAgreements_InventoryRequestOtherDetail] FOREIGN KEY ([InventoryRequestDetailOtherId]) REFERENCES [Inventory].[InventoryRequestDetailOther] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_InventoryRequestDetail] FOREIGN KEY ([InventoryRequestDetailId]) REFERENCES [Inventory].[InventoryRequestDetail] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_TransferOrder] FOREIGN KEY ([TransferOrderId]) REFERENCES [Inventory].[TransferOrder] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_KindsAgreements_InventoryRequestOtherDetail];


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_InventoryRequestDetail];


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_TransferOrder];


GO
CREATE NONCLUSTERED INDEX [IX_TransferOrderDetail_TransferOrderId_Quantity_Value]
    ON [Inventory].[TransferOrderDetail]([TransferOrderId] ASC)
    INCLUDE([Quantity], [Value]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del otro detalle de solicitdes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailOtherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio del producto con el que se traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de items que se van a despachar en el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de items que se encuantran en el inventario fisico en el momento en que se crea el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de solicitud de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de orden de transferencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';

