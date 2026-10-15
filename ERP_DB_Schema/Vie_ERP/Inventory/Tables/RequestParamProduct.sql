CREATE TABLE [Inventory].[RequestParamProduct] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [RequestParamId] INT          NOT NULL,
    [Type]           TINYINT      NOT NULL,
    [SupplieId]      INT          NULL,
    [ProductId]      INT          NULL,
    [Quantity]       INT          NOT NULL,
    [CreationUser]   VARCHAR (50) NOT NULL,
    [CreationDate]   DATETIME     NOT NULL,
    CONSTRAINT [PK_RequestParamProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamProduct_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_RequestParamProduct_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_RequestParamProduct_RequestParam] FOREIGN KEY ([RequestParamId]) REFERENCES [Inventory].[RequestParam] ([Id])
);


GO
ALTER TABLE [Inventory].[RequestParamProduct] NOCHECK CONSTRAINT [FK_RequestParamProduct_InventoryProduct];


GO
ALTER TABLE [Inventory].[RequestParamProduct] NOCHECK CONSTRAINT [FK_RequestParamProduct_InventorySupplie];


GO
ALTER TABLE [Inventory].[RequestParamProduct] NOCHECK CONSTRAINT [FK_RequestParamProduct_RequestParam];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParamProduct]
    ON [Inventory].[RequestParamProduct]([ProductId] ASC, [RequestParamId] ASC, [SupplieId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de la solicitud de parámetro de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Insumo  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la solicitud del parametro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamProduct', @level2type = N'COLUMN', @level2name = N'Id';

