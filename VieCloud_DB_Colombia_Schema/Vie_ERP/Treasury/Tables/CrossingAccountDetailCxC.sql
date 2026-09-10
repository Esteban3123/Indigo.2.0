CREATE TABLE [Treasury].[CrossingAccountDetailCxC] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CrossingAccountId]             INT             NOT NULL,
    [AccountReceivableId]           INT             NOT NULL,
    [AccountReceivableAccountingId] INT             NOT NULL,
    [MainAccountId]                 INT             NOT NULL,
    [CrossingValue]                 DECIMAL (18, 2) NOT NULL,
    [Detail]                        VARCHAR (MAX)   CONSTRAINT [DF_CrossingAccountDetailCxC_Detail] DEFAULT ('') NOT NULL,
    [IdCashFlowConcept]             INT             NULL,
    [ValueInCurrencyInvoice]        DECIMAL (20, 2) CONSTRAINT [DF__CrossingA__Value__166B7451] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CrossingAccountDetailCXC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossingAccountDetailCXC_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCXC_AccountReceivableAccounting] FOREIGN KEY ([AccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCxC_CrossingAccount1] FOREIGN KEY ([CrossingAccountId]) REFERENCES [Treasury].[CrossingAccount] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCxC_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCXC_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CrossingAccountDetailCxC_IdCashFlowConcept]
    ON [Treasury].[CrossingAccountDetailCxC]([IdCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del cruce expresado en la moneda original de la factura o comprobante de cobro, tipo DECIMAL(20,2), usado para reconciliación en divisa local', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor en la moneda de la factura.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de efectivo (tesorería) asociado al cruce, referencia a CashFlowConcept para categorización de movimientos de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre (VARCHAR MAX) con descripción detallada, justificación o notas del cruce de cuenta por cobrar, máximo 2000 caracteres', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto decimal (DECIMAL 18,2) del cruce o compensación aplicada a la cuenta por cobrar en la operación de tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del cruce de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal en el catálogo de mayores (GeneralLedger.MainAccounts), FK para trazabilidad contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro contable asociado a la cuenta por cobrar, referencia a Portfolio.AccountReceivableAccounting para auditoría', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar (deudor, factura pendiente), FK a Portfolio.AccountReceivable para vinculación de cobro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del encabezado o maestro del cruce de cuentas por cobrar, FK a Treasury.CrossingAccount que agrupa los detalles', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del cruce de cuentas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria numérica autoincrementable (IDENTITY 1,1), identificador único de cada detalle de cruce en la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de los cruces de cuentas por cobrar en tesorería: cada línea indica qué cuenta por cobrar y qué registro contable participan en un cruce, el valor aplicado y el concepto de flujo de caja asociado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC';
