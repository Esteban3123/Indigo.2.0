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
CREATE NONCLUSTERED INDEX [IX_CrossingAccountDetailOtherConcept_IdCashFlowConcept]
    ON [Treasury].[CrossingAccountDetailOtherConcept]([IdCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la Nota 1. Debito 2. Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id terceros', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de cuenta principal', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del concepto de nota del Tesoro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'TreasuryNoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de cuenta cruzada', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';

