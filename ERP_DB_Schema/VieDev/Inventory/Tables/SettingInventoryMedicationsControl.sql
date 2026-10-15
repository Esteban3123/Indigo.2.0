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
ALTER TABLE [Inventory].[SettingInventoryMedicationsControl] NOCHECK CONSTRAINT [FK_SettingInventoryMedicationsControl_SettingInventory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color del indicador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis hasta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis desde', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'DoseFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificacion de la Configuración del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryMedicationsControl', @level2type = N'COLUMN', @level2name = N'Id';

