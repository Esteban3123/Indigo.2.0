CREATE TABLE [Treasury].[CrossingAccountDetailOtherConcept] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CrossingAccountId]     INT             NOT NULL,
    [TreasuryNoteConceptId] INT             NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [ThirdPartyId]          INT             NULL,
    [CostCenterId]          INT             NULL,
    [Nature]                TINYINT         NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [Detail]                VARCHAR (MAX)   CONSTRAINT [DF_CrossingAccountDetailOtherConcept_Detail] DEFAULT ('') NOT NULL,
    [IdCashFlowConcept]     INT             NULL,
    CONSTRAINT [PK_CrossingAccountDetailOtherConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_CrossingAccount] FOREIGN KEY ([CrossingAccountId]) REFERENCES [Treasury].[CrossingAccount] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_NoteConcepts] FOREIGN KEY ([TreasuryNoteConceptId]) REFERENCES [Treasury].[NoteConcepts] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CrossingAccountDetailOtherConcept] NOCHECK CONSTRAINT [FK_CrossingAccountDetailOtherConcept_CostCenter];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailOtherConcept] NOCHECK CONSTRAINT [FK_CrossingAccountDetailOtherConcept_CrossingAccount];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailOtherConcept] NOCHECK CONSTRAINT [FK_CrossingAccountDetailOtherConcept_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailOtherConcept] NOCHECK CONSTRAINT [FK_CrossingAccountDetailOtherConcept_MainAccounts];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailOtherConcept] NOCHECK CONSTRAINT [FK_CrossingAccountDetailOtherConcept_NoteConcepts];


GO
ALTER TABLE [Treasury].[CrossingAccountDetailOtherConcept] NOCHECK CONSTRAINT [FK_CrossingAccountDetailOtherConcept_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';

