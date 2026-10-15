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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con el cual se modificará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de nota asociado a la Cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'PaymentNotesAccountPayableAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Id';

