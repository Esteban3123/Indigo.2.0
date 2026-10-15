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
ALTER TABLE [Treasury].[CrossingAccountDetailCxC] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCXC_AccountReceivable];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxC] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCXC_AccountReceivableAccounting];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxC] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCxC_CrossingAccount1];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxC] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCxC_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailCxC] NOCHECK CONSTRAINT [FK_CrossingAccountDetailCXC_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en la moneda de la factura.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailCxC', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';

