CREATE TABLE [GeneralLedger].[GeneralLedgerBalance] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Month]         INT             NOT NULL,
    [Year]          INT             CONSTRAINT [DF_GeneralLedgerBalance_Year] DEFAULT ((2014)) NOT NULL,
    [IdMainAccount] INT             NOT NULL,
    [IdThirdParty]  INT             NULL,
    [IdCostCenter]  INT             NULL,
    [DebitValue]    DECIMAL (21, 5) CONSTRAINT [DF_AccountingBalance_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]   DECIMAL (21, 5) CONSTRAINT [DF_AccountingBalance_CreditValue] DEFAULT ((0)) NOT NULL,
    [TimeStamp]     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AccountingBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingBalance_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountingBalance_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountingBalance_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_MainAccount];




GO



GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_MainAccount];


GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GeneralLedgerBalance__Month__IdMainAccount__IdThirdParty__IdCostCenter__Year]
    ON [GeneralLedger].[GeneralLedgerBalance]([Month] ASC, [IdMainAccount] ASC, [IdThirdParty] ASC, [IdCostCenter] ASC, [Year] ASC);


GO
-- Índice optimizado para consultas del SP_ReportAuxiliar que filtran por Year/Month
CREATE NONCLUSTERED INDEX [IX_GeneralLedgerBalance__Year__Month__IdMainAccount]
    ON [GeneralLedger].[GeneralLedgerBalance]([Year] ASC, [Month] ASC, [IdMainAccount] ASC)
    INCLUDE ([IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) del evento contable: instante de creación, registro o modificación del balance. Auditoría de cambios en la contabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en crédito (DECIMAL 21,5). Monto acreditado en la cuenta contable para el período. Lado derecho del asiento contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en débito (DECIMAL 21,5). Monto debitado en la cuenta contable para el período. Lado izquierdo del asiento contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor debito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del Centro de Costo. Referencia a [Payroll].[CostCenter]. Unidad funcional, departamento o área de atención que genera el gasto.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del Tercero (Proveedor, Acreedor, Deudor). Referencia a [Common].[ThirdParty]. Entidad acreedora o deudora en la transacción contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la Cuenta Contable Principal. Referencia a [GeneralLedger].[MainAccounts]. Cuenta del Plan de Cuentas donde se registra el movimiento.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año del período contable acumulado. Valor entero (por defecto 2014). Ciclo fiscal para cierre y reporte contable anual.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año que se esta acumulando', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del período contable (1-12). Período mensual en que se registra y acumula el balance de la cuenta.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (IDENTITY INT). Clave primaria única de cada registro de balance contable en la tabla.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldos del libro mayor contable por período (mes y año), cuenta contable, tercero y centro de costo. Registra los valores acumulados al debe y al haber para el cierre contable mensual.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance';
