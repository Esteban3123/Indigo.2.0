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

