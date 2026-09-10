CREATE TABLE [Treasury].[CrossingAccountDetailCxP] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CrossingAccountId]      INT             NOT NULL,
    [AccountPayableId]       INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [CrossingValue]          DECIMAL (18, 2) NOT NULL,
    [Detail]                 VARCHAR (MAX)   CONSTRAINT [DF_CrossingAccountDetailCxP_Detail] DEFAULT ('') NOT NULL,
    [IdCashFlowConcept]      INT             NULL,
    [ValueInCurrencyInvoice] DECIMAL (20, 2) CONSTRAINT [DF__CrossingA__Value__15775018] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CrossingAccountDetailCxP] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossingAccountDetailCxP_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCxP_CrossingAccount] FOREIGN KEY ([CrossingAccountId]) REFERENCES [Treasury].[CrossingAccount] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCxP_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailCxP_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CrossingAccountDetailCxP_IdCashFlowConcept]
    ON [Treasury].[CrossingAccountDetailCxP]([IdCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del cruce expresado en la moneda original de la factura o documento de pago; decimal(20,2) para conversiones y reconciliaciones de divisas en cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor en la moneda de la factura.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de efectivo asociado; referencia a CashFlowConcept para clasificación de movimientos de tesorería y efectivo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual detallada del cruce de cuentas por pagar; notas, referencias o justificación del movimiento contable y financiero.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la cuenta de cruce de cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto decimal(18,2) del cruce o compensación entre cuentas por pagar; valor principal de la transacción de liquidación o pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del cruce de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (Mayor); referencia a GeneralLedger.MainAccounts para registro contable y auditoría financiera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar (CxP) asociada; referencia a Payments.AccountPayable que vincula factura, proveedor o acreedor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado del cruce; referencia a CrossingAccount que agrupa los detalles del movimiento de compensación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del cruce', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de detalle del cruce de cuentas por pagar; clave primaria para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del cruce', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cruce de cuentas con cuentas por pagar (CxP) en tesorería. Registra cada línea del cruce entre una cuenta principal y una cuenta por pagar, con el valor cruzado y su equivalente en la moneda de la factura.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP';
