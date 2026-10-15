CREATE TABLE [Inventory].[AdjustedPhysicalInventory] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalInventoryId] INT NOT NULL,
    [QuantityPrevious]    INT NOT NULL,
    [Quantity]            INT NOT NULL,
    CONSTRAINT [PK_AdjustedPhysicalInventory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad anterior', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de inventario físico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';

