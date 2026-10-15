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
ALTER TABLE [Treasury].[CrossingAccountDetailCxP] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCxP_AccountPayable];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxP] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCxP_CrossingAccount];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxP] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCxP_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxP] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCxP_MainAccounts];


GO
CREATE NONCLUSTERED INDEX [IX_CrossingAccountDetailCxP_IdCashFlowConcept]
    ON [Treasury].[CrossingAccountDetailCxP]([IdCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en la moneda de la factura.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la cuenta de cruce de cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del cruce de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del cruce', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del cruce', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxP', @level2type = N'COLUMN', @level2name = N'Id';

