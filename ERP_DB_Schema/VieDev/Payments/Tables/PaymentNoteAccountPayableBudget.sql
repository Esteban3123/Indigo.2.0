CREATE TABLE [Payments].[PaymentNoteAccountPayableBudget] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentNotesAccountPayableAdvanceId] INT             NOT NULL,
    [ObligationDetailId]                  INT             NOT NULL,
    [Value]                               DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_PaymentNoteAccountPayableBudget__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentNoteAccountPayableBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_PaymentNoteAccountPayableBudget_PaymentNotesAccountPayableAdvance] FOREIGN KEY ([PaymentNotesAccountPayableAdvanceId]) REFERENCES [Payments].[PaymentNotesAccountPayableAdvance] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentNoteAccountPayableBudget] NOCHECK CONSTRAINT [FK_PaymentNoteAccountPayableBudget_ObligationDetail];


GO
ALTER TABLE [Payments].[PaymentNoteAccountPayableBudget] NOCHECK CONSTRAINT [FK_PaymentNoteAccountPayableBudget_PaymentNotesAccountPayableAdvance];

