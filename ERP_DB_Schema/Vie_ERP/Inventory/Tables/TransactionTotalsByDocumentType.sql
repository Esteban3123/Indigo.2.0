CREATE TABLE [Inventory].[TransactionTotalsByDocumentType] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Year]             INT             NOT NULL,
    [Month]            INT             NOT NULL,
    [ProductGroupId]   INT             NOT NULL,
    [MainAccountsId]   INT             NOT NULL,
    [TransactionType]  INT             NOT NULL,
    [WareHouseType]    INT             NOT NULL,
    [TotalCredit]      DECIMAL (20, 2) NOT NULL,
    [TotalDebit]       DECIMAL (20, 2) NOT NULL,
    [AffectAccounting] BIT             NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    CONSTRAINT [PK_TransactionTotalsByDocumentType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransactionTotalsByDocumentType_MainAccounts] FOREIGN KEY ([MainAccountsId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TransactionTotalsByDocumentType_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta contabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'AffectAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Debito (Total entradas)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalDebit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Credito (Total salidas)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalCredit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'WareHouseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de transanccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TransactionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'MainAccountsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del total de los movimientos por tipo de documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del total de los movimientos por tipo de documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Id';

