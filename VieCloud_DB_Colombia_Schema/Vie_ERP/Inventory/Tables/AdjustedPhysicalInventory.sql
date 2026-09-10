CREATE TABLE [Inventory].[AdjustedPhysicalInventory] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalInventoryId] INT NOT NULL,
    [QuantityPrevious]    INT NOT NULL,
    [Quantity]            INT NOT NULL,
    CONSTRAINT [PK_AdjustedPhysicalInventory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad ajustada o corregida de existencias en el recuento físico de inventario; cantidad final registrada tras auditoría de stock', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad anterior o cantidad previa al ajuste; cantidad registrada en el sistema antes de la corrección física de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad anterior', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia del recuento físico de inventario; clave foránea que vincula al registro maestro de auditoría de existencias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de inventario físico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la tabla AdjustedPhysicalInventory; clave primaria para cada registro de ajuste de cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ajustes realizados sobre un inventario físico: guarda la cantidad anterior y la cantidad corregida luego de un ajuste de inventario físico en el almacén o bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedPhysicalInventory';
