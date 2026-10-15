CREATE TABLE [Payments].[PaymentNotesAccountPayableAdvance] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentNoteId]               INT             NOT NULL,
    [AccountPayableId]            INT             NULL,
    [AccountPayableShareId]       INT             NULL,
    [AdvancePaymentId]            INT             NULL,
    [AdjusmentValue]              DECIMAL (18, 2) NOT NULL,
    [PercentageValue]             NUMERIC (18, 2) NOT NULL,
    [AdjustmentValueShare]        DECIMAL (18, 2) NULL,
    [PreviousBalance]             DECIMAL (18, 2) NOT NULL,
    [Balance]                     DECIMAL (18, 2) NOT NULL,
    [ObligationModificationValue] DECIMAL (18)    CONSTRAINT [DF_PaymentNotesAccountPayableAdvance_ObligationModificationValue] DEFAULT ((0)) NOT NULL,
    [ConceptAdjustmentId]         INT             NULL,
    CONSTRAINT [PK_PaymentNotesAccountPayableAdvance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_AccountPayableShares] FOREIGN KEY ([AccountPayableShareId]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_AdvancePayments] FOREIGN KEY ([AdvancePaymentId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_PaymentNotes] FOREIGN KEY ([PaymentNoteId]) REFERENCES [Payments].[PaymentNotes] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentNotesAccountPayableAdvance] NOCHECK CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_AccountPayable];


GO
ALTER TABLE [Payments].[PaymentNotesAccountPayableAdvance] NOCHECK CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_AccountPayableShares];


GO
ALTER TABLE [Payments].[PaymentNotesAccountPayableAdvance] NOCHECK CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_AdvancePayments];


GO
ALTER TABLE [Payments].[PaymentNotesAccountPayableAdvance] NOCHECK CONSTRAINT [FK_PaymentNotesAccountPayableAdvance_PaymentNotes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conceptos de ajustes de una nota según resolución 167  1 Devolución parcial de los bienes y/o no aceptación parcial del servicio 2 Anulación del documento soporte   3 Rebaja o descuento parcial o total   4 Ajuste de precio 5 Otros', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptAdjustmentId';

