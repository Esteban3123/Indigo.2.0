CREATE TABLE [Inventory].[ProductGroupFunctionalUnit] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductGroupId]    INT NOT NULL,
    [FunctionalUnitId]  INT NOT NULL,
    [CostAccountId]     INT NOT NULL,
    [SalesAccountId]    INT NOT NULL,
    [DiscountAccountId] INT NULL,
    CONSTRAINT [PK_ProductGroupFunctionalUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_DiscountAccount] FOREIGN KEY ([DiscountAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts] FOREIGN KEY ([CostAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts1] FOREIGN KEY ([SalesAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_DiscountAccount];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_FunctionalUnit];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts1];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_ProductGroup];

