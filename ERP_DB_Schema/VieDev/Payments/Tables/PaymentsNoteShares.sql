CREATE TABLE [Payments].[PaymentsNoteShares] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPaymentsNote]               INT           NOT NULL,
    [IdAccountPayableShares]       INT           NOT NULL,
    [Nature]                       TINYINT       NOT NULL,
    [IdAccountPayableConceptNotes] INT           NOT NULL,
    [Value]                        DECIMAL (18)  NOT NULL,
    [Comments]                     VARCHAR (200) NULL,
    CONSTRAINT [PK_PaymentsNoteShares] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentsNoteShare_AccountPayableShares] FOREIGN KEY ([IdAccountPayableShares]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_PaymentsNoteShare_PaymentsNote] FOREIGN KEY ([IdPaymentsNote]) REFERENCES [Payments].[PaymentNotes] ([Id]),
    CONSTRAINT [FK_PaymentsNoteShares_AccountPayableConceptNotes] FOREIGN KEY ([IdAccountPayableConceptNotes]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentsNoteShares] NOCHECK CONSTRAINT [FK_PaymentsNoteShare_AccountPayableShares];


GO
ALTER TABLE [Payments].[PaymentsNoteShares] NOCHECK CONSTRAINT [FK_PaymentsNoteShare_PaymentsNote];


GO
ALTER TABLE [Payments].[PaymentsNoteShares] NOCHECK CONSTRAINT [FK_PaymentsNoteShares_AccountPayableConceptNotes];

