CREATE TABLE [Inventory].[BatchSerialRange] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingInventoryId] INT NOT NULL,
    [InitialRange]       INT NOT NULL,
    [EndRange]           INT NOT NULL,
    [Color]              INT NOT NULL,
    CONSTRAINT [PK_BatchSerialRange] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BatchSerialRange_SettingInventory] FOREIGN KEY ([SettingInventoryId]) REFERENCES [Inventory].[SettingInventory] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de color de fondo (INT) para visualización del rango de lotes en interfaz; facilita identificación visual de rangos de antigüedad o clasificación en inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color de fondo para el rango', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor final del rango en días; límite superior del período de validez o antigüedad de lotes; usado para segmentación de inventario por antigüedad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'EndRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor final del rango (Dias)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'EndRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'EndRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial del rango en días; si es el primer registro por defecto será 0; define el inicio del período de validez o control de antigüedad de lotes en inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'InitialRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor inicial del rango, Si es el primer registro este campo sera por defecto 0 (Dias)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'InitialRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'InitialRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la configuración de inventario asociada; referencia a SettingInventory que define los parámetros de control de lotes y series', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del parámetro de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT identity) del rango de lotes y series en inventario; clave primaria que identifica cada configuración de rango de lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rango de lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rangos de lotes o series del inventario: define los intervalos numéricos (inicio y fin) asociados a una configuración de inventario, con un color identificador para diferenciar visualmente cada rango de lote o número de serie.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange';

GO
CREATE NONCLUSTERED INDEX [IX_BatchSerialRange_SettingInventoryId]
    ON [Inventory].[BatchSerialRange]([SettingInventoryId] ASC);
