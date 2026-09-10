CREATE TABLE [Inventory].[SettingInventoryPBSControl] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [SettingInventoryId] INT NOT NULL,
    [DoseFrom]           INT NULL,
    [DoseTo]             INT NULL,
    [Color]              INT NOT NULL,
    CONSTRAINT [PK_SettingInventoryPBSControl] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingInventoryPBSControl_SettingInventory] FOREIGN KEY ([SettingInventoryId]) REFERENCES [Inventory].[SettingInventory] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color del indicador visual de control PBS (sistema de alerta por rango de dosis); código hexadecimal o paleta predefinida para visualizar estado de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color del indicador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis máxima o techo del rango de control PBS; límite superior en unidades de medicamento para activar alerta visual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis hasta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis mínima o piso del rango de control PBS; límite inferior en unidades de medicamento para activar alerta visual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis desde', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la Configuración del Inventario (FK); referencia a tabla SettingInventory para vincular parámetros de control PBS a un medicamento o bien', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificacion de la Configuración del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única de la tabla SettingInventoryPBSControl; llave primaria identity para cada rango de dosis con color indicador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de rangos de dosis y colores para el control visual del inventario PBS (Plan de Beneficios en Salud). Permite definir por configuración de inventario qué color mostrar según el rango de dosis (de - hasta), facilitando alertas visuales sobre niveles de stock de medicamentos o insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl';

GO
CREATE NONCLUSTERED INDEX [IX_SettingInventoryPBSControl_SettingInventoryId]
    ON [Inventory].[SettingInventoryPBSControl]([SettingInventoryId] ASC);
