CREATE TABLE [Inventory].[ClosedMonth] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [Year]                         INT             NOT NULL,
    [Month]                        INT             NOT NULL,
    [InventoryNoLegalizedBalance]  DECIMAL (18, 2) NOT NULL,
    [InventoryLegalizedBalance]    DECIMAL (18, 2) NOT NULL,
    [AccountingNoLegalizedBalance] DECIMAL (18, 2) NOT NULL,
    [AccountingLegalizedBalance]   DECIMAL (18, 2) NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    CONSTRAINT [PK_Inventory_ClosedMonth] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo Legalizado Contable', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'AccountingLegalizedBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contabilidad Sin saldo legalizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'AccountingNoLegalizedBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Balance de Inventario Legalizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'InventoryLegalizedBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inventario sin saldo legalizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'InventoryNoLegalizedBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del cierre', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del cierre', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'Id';

