CREATE TABLE [Inventory].[SettingInventoryFunctionalUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingInventoryId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    [CostAccountId]      INT NOT NULL,
    [SalesAccountId]     INT NOT NULL,
    CONSTRAINT [PK_SettingInventoryFunctionalUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_MainAccounts] FOREIGN KEY ([CostAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_MainAccounts1] FOREIGN KEY ([SalesAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_SettingInventory] FOREIGN KEY ([SettingInventoryId]) REFERENCES [Inventory].[SettingInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[SettingInventoryFunctionalUnit] NOCHECK CONSTRAINT [FK_SettingInventoryFunctionalUnit_FunctionalUnit];


GO
ALTER TABLE [Inventory].[SettingInventoryFunctionalUnit] NOCHECK CONSTRAINT [FK_SettingInventoryFunctionalUnit_MainAccounts];


GO
ALTER TABLE [Inventory].[SettingInventoryFunctionalUnit] NOCHECK CONSTRAINT [FK_SettingInventoryFunctionalUnit_MainAccounts1];


GO
ALTER TABLE [Inventory].[SettingInventoryFunctionalUnit] NOCHECK CONSTRAINT [FK_SettingInventoryFunctionalUnit_SettingInventory];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SettingInventoryFunctionalUnit__FunctionalUnitId__SettingInventoryId]
    ON [Inventory].[SettingInventoryFunctionalUnit]([FunctionalUnitId] ASC, [SettingInventoryId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cuenta de ventas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable del costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

