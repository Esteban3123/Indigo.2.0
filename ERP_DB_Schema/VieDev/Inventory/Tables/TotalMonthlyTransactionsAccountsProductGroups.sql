CREATE TABLE [Inventory].[TotalMonthlyTransactionsAccountsProductGroups] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [Year]           INT             NOT NULL,
    [Month]          INT             NOT NULL,
    [ProductGroupId] INT             NOT NULL,
    [MainAccountsId] INT             NOT NULL,
    [EntityId]       INT             NOT NULL,
    [EntityCode]     VARCHAR (20)    NOT NULL,
    [EntityType]     INT             NOT NULL,
    [Value]          DECIMAL (20, 2) NOT NULL,
    [Nature]         DECIMAL (20, 2) NOT NULL,
    [CreationUser]   VARCHAR (20)    NOT NULL,
    [CreationDate]   DATETIME        NOT NULL,
    CONSTRAINT [PK_TotalMonthlyTransactionsAccountsProductGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TotalMonthlyTransactionsAccountsProductGroups_MainAccounts] FOREIGN KEY ([MainAccountsId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TotalMonthlyTransactionsAccountsProductGroups_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza del movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del comprobante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del comprobante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'MainAccountsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes de los movimientos ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de los movimientos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TotalMonthlyTransactionsAccountsProductGroups', @level2type = N'COLUMN', @level2name = N'Id';

