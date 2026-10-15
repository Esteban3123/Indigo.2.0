CREATE TABLE [Inventory].[GroupAccountBalance] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Year]            INT             NOT NULL,
    [Month]           INT             NOT NULL,
    [ProductGroupId]  INT             NOT NULL,
    [MainAccountsId]  INT             NOT NULL,
    [TransactionType] INT             NOT NULL,
    [CreditValue]     DECIMAL (20, 2) NOT NULL,
    [DebitValue]      DECIMAL (20, 2) NOT NULL,
    [InitialBalance]  DECIMAL (20, 2) NOT NULL,
    [FinalBalance]    DECIMAL (20, 2) NOT NULL,
    [CreationUser]    VARCHAR (20)    NOT NULL,
    [CreationDate]    DATETIME        NOT NULL,
    CONSTRAINT [PK_GroupAccountBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupAccountBalance_MainAccounts] FOREIGN KEY ([MainAccountsId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GroupAccountBalance_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo Final', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'FinalBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo Inicial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Debito (Total entradas)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Credito (Total salidas)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de transanccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'TransactionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'MainAccountsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes de los movimientos ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de los movimientos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'GroupAccountBalance', @level2type = N'COLUMN', @level2name = N'Id';

