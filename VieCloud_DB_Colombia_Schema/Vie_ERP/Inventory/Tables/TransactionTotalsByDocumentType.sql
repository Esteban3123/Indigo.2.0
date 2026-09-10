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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de totales de transacciones (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20, identificación del operario/sistema)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la transacción afecta la contabilidad general (BIT: 1=Sí afecta, 0=No afecta)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'AffectAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta contabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'AffectAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'AffectAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de débitos/entradas de inventario por tipo de documento (DECIMAL 20,2, movimientos de entrada)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalDebit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Debito (Total entradas)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalDebit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalDebit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de créditos/salidas de inventario por tipo de documento (DECIMAL 20,2, movimientos de salida)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalCredit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Credito (Total salidas)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalCredit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TotalCredit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o clasificación de almacén (INT, referencia a catálogo: bodega, depósito, centro de distribución)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'WareHouseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'WareHouseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'WareHouseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de transacción de inventario (INT, referencia a catálogo: compra, devolución, ajuste, traspaso)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TransactionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de transanccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TransactionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'TransactionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal del grupo de productos (INT FK a GeneralLedger.MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'MainAccountsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'MainAccountsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'MainAccountsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de productos asociado (INT FK a Inventory.ProductGroup)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del período de los totales de movimientos por tipo de documento (INT: 1-12)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes del total de los movimientos por tipo de documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año del período de los totales de movimientos por tipo de documento (INT: ej. 2023, 2024)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del total de los movimientos por tipo de documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de totales de transacciones (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Totales de movimientos de inventario agrupados por tipo de documento contable, grupo de producto y tipo de bodega, acumulados por mes y año. Permite consolidar los débitos y créditos generados por las transacciones de inventario para su impacto en la contabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransactionTotalsByDocumentType';
