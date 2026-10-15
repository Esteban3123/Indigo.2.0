CREATE TABLE [GeneralLedger].[AccountingMovement] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LegalBookId]          INT           NOT NULL,
    [JournalVoucherTypeId] INT           NOT NULL,
    [VoucherDate]          DATETIME      NOT NULL,
    [Detail]               VARCHAR (500) NOT NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityId]             INT           NULL,
    [EntityName]           VARCHAR (250) NULL,
    [JournalVoucherXml]    XML           NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    CONSTRAINT [PK_AccountingMovement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingMovement_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_AccountingMovement_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_JournalVoucherTypes];


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_LegalBook];


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_AccountingMovement_NoDup]
    ON [GeneralLedger].[AccountingMovement]([EntityCode] ASC, [EntityId] ASC, [EntityName] ASC, [JournalVoucherTypeId] ASC, [LegalBookId] ASC, [VoucherDate] ASC) WHERE ([EntityCode] IS NOT NULL AND [EntityCode]<>'' AND [EntityId] IS NOT NULL AND [EntityId]<>(0) AND [ID]>=(3731314));


GO
CREATE NONCLUSTERED INDEX [IX_AccountingMovement_EntityName_EntityId]
    ON [GeneralLedger].[AccountingMovement]([EntityName] ASC, [EntityId] ASC);

