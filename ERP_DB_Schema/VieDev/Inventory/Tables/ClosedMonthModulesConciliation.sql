CREATE TABLE [Inventory].[ClosedMonthModulesConciliation] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [Year]                  INT             NOT NULL,
    [Month]                 INT             NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [TotalDebitAccounting]  DECIMAL (20, 2) NOT NULL,
    [TotalCreditAccounting] DECIMAL (20, 2) NOT NULL,
    [TotalDebitInventory]   DECIMAL (20, 2) NOT NULL,
    [TotalCreditInventory]  DECIMAL (20, 2) NOT NULL,
    CONSTRAINT [PK_ClosedMonthModulesConciliation] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_ClosedMonthModulesConciliation_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total Creditos de  inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditInventory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total Debitos de inventario ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitInventory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total Creditos de  contabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total Debitos de contabilidad ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de los movimientos , se obtiene del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del documento de la transaccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes  de la conciliacion de los movimientos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de la conciliacion de los movimientos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que guarda los totales de movimientos por tipo de documento por mes para el cierre de mes de inventario, es la conciliacion de movimientos entre contabilidad e inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation';

