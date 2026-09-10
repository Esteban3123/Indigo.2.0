CREATE TABLE [Inventory].[SettingInventoryMedicationsControl] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [SettingInventoryId] INT NOT NULL,
    [DoseFrom]           INT NULL,
    [DoseTo]             INT NULL,
    [Color]              INT NOT NULL,
    CONSTRAINT [PK_SettingInventoryMedicationsControl] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingInventoryMedicationsControl_SettingInventory] FOREIGN KEY ([SettingInventoryId]) REFERENCES [Inventory].[SettingInventory] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color del indicador visual de control de medicamentos; código numérico (INT) que representa el color de alerta o estado en el rango de dosis configurado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color del indicador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis hasta; límite superior del rango de dosificación de medicamento en unidades de control de inventario farmacéutico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis hasta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis desde; límite inferior del rango de dosificación de medicamento en unidades de control de inventario farmacéutico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis desde', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la Configuración del Inventario; clave foránea (FK) que referencia la configuración maestro de control de medicamentos en Inventory.SettingInventory', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificacion de la Configuración del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única de la tabla; clave primaria (PK) Identity INT que identifica cada regla de control de dosis de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de rangos de control visual para medicamentos en el inventario. Define intervalos de dosis y el color asociado a cada rango, permitiendo identificar visualmente el estado o nivel de los medicamentos según la cantidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl';

GO
CREATE NONCLUSTERED INDEX [IX_SettingInventoryMedicationsControl_SettingInventoryId]
    ON [Inventory].[SettingInventoryMedicationsControl]([SettingInventoryId] ASC);
