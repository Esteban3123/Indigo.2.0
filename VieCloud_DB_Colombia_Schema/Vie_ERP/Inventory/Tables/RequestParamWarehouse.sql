CREATE TABLE [Inventory].[RequestParamWarehouse] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [RequestParamId] INT NOT NULL,
    [WarehouseId]    INT NOT NULL,
    CONSTRAINT [PK_RequestParamWarehouse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamWarehouse_RequestParam] FOREIGN KEY ([RequestParamId]) REFERENCES [Inventory].[RequestParam] ([Id]),
    CONSTRAINT [FK_RequestParamWarehouse_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParamWarehouse]
    ON [Inventory].[RequestParamWarehouse]([WarehouseId] ASC, [RequestParamId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o bodega de inventario vinculado a la solicitud de parámetros. Referencia FK a Warehouse(Id). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del parámetro de solicitud de inventario asociado al almacén. Referencia FK a RequestParam(Id). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'RequestParamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del parámetro de solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'RequestParamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único primario (clave) de la relación entre parámetro de solicitud y almacén. Tipo INT IDENTITY. PK_RequestParamWarehouse.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los parámetros de solicitud de inventario con las bodegas o almacenes habilitados para esa configuración. Permite definir qué bodegas aplican a cada parámetro de pedido o requisición de materiales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse';
