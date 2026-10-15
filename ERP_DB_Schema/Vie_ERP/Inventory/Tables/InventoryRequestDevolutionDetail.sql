CREATE TABLE [Inventory].[InventoryRequestDevolutionDetail] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryRequestDevolutionId] INT NOT NULL,
    [InventoryRequestDetailId]     INT NOT NULL,
    [Quantity]                     INT NOT NULL,
    CONSTRAINT [PK_InventoryRequestDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryRequestDevolutionDetail_InventoryRequestDetail] FOREIGN KEY ([InventoryRequestDetailId]) REFERENCES [Inventory].[InventoryRequestDetail] ([Id]),
    CONSTRAINT [FK_InventoryRequestDevolutionDetail_InventoryRequestDevolution] FOREIGN KEY ([InventoryRequestDevolutionId]) REFERENCES [Inventory].[InventoryRequestDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryRequestDevolutionDetail] NOCHECK CONSTRAINT [FK_InventoryRequestDevolutionDetail_InventoryRequestDetail];


GO
ALTER TABLE [Inventory].[InventoryRequestDevolutionDetail] NOCHECK CONSTRAINT [FK_InventoryRequestDevolutionDetail_InventoryRequestDevolution];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

