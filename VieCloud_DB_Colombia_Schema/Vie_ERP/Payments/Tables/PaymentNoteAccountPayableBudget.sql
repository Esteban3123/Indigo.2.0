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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto decimal (18,2) que modifica la obligación presupuestal en el detalle seleccionado; importe de ajuste, crédito o débito a la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se modificará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de detalle de obligación presupuestal (FK a Budget.ObligationDetail); referencia al rubro, concepto o línea de gasto obligado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nota de pago asociada a cuenta por pagar anticipada (FK a PaymentNotesAccountPayableAdvance); vinculación del comprobante de pago con la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'PaymentNotesAccountPayableAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de nota asociado a la Cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'PaymentNotesAccountPayableAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'PaymentNotesAccountPayableAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de relación entre nota de pago, obligación presupuestal y monto de ajuste; clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle presupuestal de las notas de pago a cuentas por pagar, vinculando cada anticipo o nota de pago con la obligación presupuestal específica y el valor monetario comprometido o aplicado en cada partida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNoteAccountPayableBudget';
