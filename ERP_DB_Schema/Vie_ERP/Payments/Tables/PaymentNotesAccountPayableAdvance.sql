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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PaymentNotesAccountPayableAdvance__PaymentNoteId__AccountPayableId__AccountPayableShareId__AdvancePaymentId]
    ON [Payments].[PaymentNotesAccountPayableAdvance]([PaymentNoteId] ASC, [AccountPayableId] ASC, [AccountPayableShareId] ASC, [AdvancePaymentId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conceptos de ajustes de una nota según resolución 167  1 Devolución parcial de los bienes y/o no aceptación parcial del servicio 2 Anulación del documento soporte   3 Rebaja o descuento parcial o total   4 Ajuste de precio 5 Otros', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptAdjustmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor por el cual se hará interface en presupuesto en la modificación de la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ObligationModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Saldo de la CXP o del anticipo, Es decir que es el nuevo saldo de CXP o del anticipo despues de que se ejecuto la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo anterior de la factura o del anticipo, es decir que es el saldo que tenia antes de ser afectada por la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se va a ejecutar a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjustmentValueShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentage que afecta el valor ajustado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PercentageValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ajustado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anticipo que se esta afectando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota de la cuenta por pagar que se esta afectando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar que se esta afectando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la nota que esta afectando la factura o el anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la relacion enter notas , facturas y anticipos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Id';

