CREATE TABLE [GeneralLedger].[JournalVoucherTypeConsecutive] (
    [Id]                   INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [JournalVoucherTypeId] INT    NOT NULL,
    [Year]                 INT    NOT NULL,
    [Consecutive]          BIGINT NOT NULL,
    [LegalBookId]          INT    CONSTRAINT [DF_JournalVoucherTypeConsecutive_LegalBookId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_JournalVoucherTypeConsecutive] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JournalVoucherTypeConsecutive_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_JournalVoucherTypeConsecutive_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[JournalVoucherTypeConsecutive] NOCHECK CONSTRAINT [FK_JournalVoucherTypeConsecutive_JournalVoucherTypes];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherTypeConsecutive] NOCHECK CONSTRAINT [FK_JournalVoucherTypeConsecutive_LegalBook];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_JournalVoucherTypeConsecutive]
    ON [GeneralLedger].[JournalVoucherTypeConsecutive]([JournalVoucherTypeId] ASC, [LegalBookId] ASC, [Year] ASC);

