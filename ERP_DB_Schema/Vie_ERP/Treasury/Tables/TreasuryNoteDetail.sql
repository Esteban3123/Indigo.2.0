CREATE TABLE [Treasury].[TreasuryNoteDetail] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TreasuryNoteId]    INT             NOT NULL,
    [NoteConceptId]     INT             NOT NULL,
    [MainAccountId]     INT             NOT NULL,
    [ThirdPartyId]      INT             NULL,
    [CostCenterId]      INT             NULL,
    [Nature]            TINYINT         NOT NULL,
    [Value]             DECIMAL (18, 2) NOT NULL,
    [IdCashFlowConcept] INT             NULL,
    CONSTRAINT [PK_TreasuryNoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryNoteDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_TreasuryNoteDetail_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_TreasuryNoteDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TreasuryNoteDetail_NoteConcepts] FOREIGN KEY ([NoteConceptId]) REFERENCES [Treasury].[NoteConcepts] ([Id]),
    CONSTRAINT [FK_TreasuryNoteDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_TreasuryNoteDetail_TreasuryNote] FOREIGN KEY ([TreasuryNoteId]) REFERENCES [Treasury].[TreasuryNote] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasuryNoteDetail] NOCHECK CONSTRAINT [FK_TreasuryNoteDetail_CostCenter];


GO
ALTER TABLE [Treasury].[TreasuryNoteDetail] NOCHECK CONSTRAINT [FK_TreasuryNoteDetail_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[TreasuryNoteDetail] NOCHECK CONSTRAINT [FK_TreasuryNoteDetail_MainAccounts];


GO
ALTER TABLE [Treasury].[TreasuryNoteDetail] NOCHECK CONSTRAINT [FK_TreasuryNoteDetail_NoteConcepts];


GO
ALTER TABLE [Treasury].[TreasuryNoteDetail] NOCHECK CONSTRAINT [FK_TreasuryNoteDetail_ThirdParty];


GO
ALTER TABLE [Treasury].[TreasuryNoteDetail] NOCHECK CONSTRAINT [FK_TreasuryNoteDetail_TreasuryNote];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo si maneja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero si maneja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable  del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'NoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'TreasuryNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';

