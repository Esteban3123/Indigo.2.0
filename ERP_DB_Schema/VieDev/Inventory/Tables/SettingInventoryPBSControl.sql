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
ALTER TABLE [Inventory].[SettingInventoryPBSControl] NOCHECK CONSTRAINT [FK_SettingInventoryPBSControl_SettingInventory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color del indicador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis hasta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis desde', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificacion de la Configuración del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryPBSControl', @level2type = N'COLUMN', @level2name = N'Id';

