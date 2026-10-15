CREATE TABLE [Inventory].[RelatedSupplies] (
    [Id]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]       INT NOT NULL,
    [ProductId]   INT NOT NULL,
    [Amount]      INT NOT NULL,
    [DefaultLoad] BIT NOT NULL,
    CONSTRAINT [PK_RelatedSupplies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RelatedSupplies_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RelatedSupplies_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[RelatedSupplies] NOCHECK CONSTRAINT [FK_RelatedSupplies_ATC];


GO
ALTER TABLE [Inventory].[RelatedSupplies] NOCHECK CONSTRAINT [FK_RelatedSupplies_Product];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si carga por defecto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'DefaultLoad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de insumos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del insumo, este es un producto pero se aplica el filtro por la clase del tipo de producto que sea insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del atc el cual contiene los insumos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tabla de insumos relacionados', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Id';

