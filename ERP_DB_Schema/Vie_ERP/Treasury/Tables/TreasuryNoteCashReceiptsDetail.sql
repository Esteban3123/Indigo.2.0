CREATE TABLE [Treasury].[TreasuryNoteCashReceiptsDetail] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [TreasuryNoteId]  INT NOT NULL,
    [CashReceiptsId]  INT NOT NULL,
    CONSTRAINT [PK_TreasuryNoteCashReceiptsDetail] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [UQ_TreasuryNoteCashReceiptsDetail] UNIQUE NONCLUSTERED ([TreasuryNoteId] ASC, [CashReceiptsId] ASC),
    CONSTRAINT [FK_TreasuryNoteCashReceiptsDetail_CashReceipts] FOREIGN KEY ([CashReceiptsId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_TreasuryNoteCashReceiptsDetail_TreasuryNote] FOREIGN KEY ([TreasuryNoteId]) REFERENCES [Treasury].[TreasuryNote] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion a TreasuryNote.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'TreasuryNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion a CashReceipts.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';