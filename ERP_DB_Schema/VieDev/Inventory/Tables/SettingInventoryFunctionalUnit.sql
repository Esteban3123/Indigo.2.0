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

