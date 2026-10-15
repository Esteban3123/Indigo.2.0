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
ALTER TABLE [Inventory].[BatchSerialRange] NOCHECK CONSTRAINT [FK_BatchSerialRange_SettingInventory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color de fondo para el rango', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor final del rango (Dias)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'EndRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor inicial del rango, Si es el primer registro este campo sera por defecto 0 (Dias)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'InitialRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del parámetro de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rango de lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BatchSerialRange', @level2type = N'COLUMN', @level2name = N'Id';

