CREATE TABLE [MixingStation].[CMWarehouse] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdMixingStation] INT     NOT NULL,
    [IdWarehouse]     INT     NOT NULL,
    [WarehouseType]   TINYINT NOT NULL,
    [StateWH]         BIT     NOT NULL,
    CONSTRAINT [PK_CMWarehouse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMWarehouse_CMConfiguration] FOREIGN KEY ([IdMixingStation]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMWarehouse_WareHouse] FOREIGN KEY ([IdWarehouse]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT): 1=Activo, 0=Inactivo. Indica si la asociación almacén-estación de mezcla está operativa.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'StateWH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: 1:Activo, 0:Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'StateWH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'StateWH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de almacén (TINYINT): 1=Materia Prima Stock, 2=Almacén, 3=En Proceso, 4=Terminado, 5=Inventario de Control, 6=Inventario de Remanente. Categoría de depósito/bodega.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de almacen:  1-Materia Prima Stock  2-Almacén  3-En Proceso  4-Terminado  5-Inventario de Control  6-Inventario de Remanente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del maestro de almacenes. Referencia a [Inventory].[Warehouse]. Código único de bodega/depósito.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del maestro de almacenes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdWarehouse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de estación de mezcla. Referencia a [MixingStation].[CMConfiguration]. Código único de área de mezclado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de Estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) de la tabla. Clave primaria única del vínculo almacén-estación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada estación de mezcla (mixing station) con los almacenes o bodegas que tiene asignados, indicando el tipo de bodega y si está activa o inactiva. Permite saber qué depósitos de insumos o medicamentos están vinculados a cada punto de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse';
