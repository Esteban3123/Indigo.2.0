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



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PaymentNotesAccountPayableAdvance__PaymentNoteId__AccountPayableId__AccountPayableShareId__AdvancePaymentId]
    ON [Payments].[PaymentNotesAccountPayableAdvance]([PaymentNoteId] ASC, [AccountPayableId] ASC, [AccountPayableShareId] ASC, [AdvancePaymentId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de ajuste según Resolución 167: devolución parcial de bienes/servicios, anulación de soporte, rebaja/descuento, ajuste de precio u otros; determina el tipo de modificación aplicada a la obligación de pago (INT, FK → ConceptAdjustment)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptAdjustmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conceptos de ajustes de una nota según resolución 167  1 Devolución parcial de los bienes y/o no aceptación parcial del servicio 2 Anulación del documento soporte   3 Rebaja o descuento parcial o total   4 Ajuste de precio 5 Otros', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptAdjustmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptAdjustmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en DECIMAL(18,2) por el cual se hará interface presupuestal en la modificación de la obligación; afecta el registro contable de la CXP o anticipo ajustado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ObligationModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor por el cual se hará interface en presupuesto en la modificación de la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ObligationModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'ObligationModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo resultante DECIMAL(18,2) de la Cuenta por Pagar o anticipo después de aplicar la nota de ajuste; refleja el nuevo saldo adeudado post-procesamiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Saldo de la CXP o del anticipo, Es decir que es el nuevo saldo de CXP o del anticipo despues de que se ejecuto la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo anterior DECIMAL(18,2) de la factura o anticipo antes de ser afectado por la nota; línea base para cálculo de diferencia ajustada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo anterior de la factura o del anticipo, es decir que es el saldo que tenia antes de ser afectada por la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor DECIMAL(18,2) NULL a ejecutar contra la cuota específica de la CXP; permite ajuste parcial por cuota en pagos fraccionados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjustmentValueShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se va a ejecutar a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjustmentValueShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjustmentValueShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje NUMERIC(18,2) que afecta el valor ajustado; usado para cálculos proporcionales de rebaja, descuento o ajuste sobre la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PercentageValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentage que afecta el valor ajustado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PercentageValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PercentageValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor DECIMAL(18,2) del ajuste aplicado; monto específico que modifica la obligación de pago (devolución, descuento, anulación, etc.)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ajustado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del anticipo (FK → AdvancePayments) siendo afectado por la nota; NULL si ajuste es solo sobre CXP', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anticipo que se esta afectando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de la cuota/plazo (FK → AccountPayableShares) de la CXP siendo afectada; NULL si es ajuste global', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota de la cuenta por pagar que se esta afectando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de la Cuenta por Pagar (FK → AccountPayable) siendo afectada por la nota de ajuste; NULL si es solo anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar que se esta afectando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de la nota de ajuste (FK → PaymentNotes) que afecta la factura, cuota o anticipo; clave de relación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nota que esta afectando la factura o el anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT IDENTITY (PK) de la relación muchos-a-muchos entre notas de ajuste, facturas/cuotas y anticipos; clave única de cada afectación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relacion enter notas , facturas y anticipos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del detalle de aplicación de notas de pago sobre cuentas por pagar y anticipos: vincula cada nota de pago con la cuenta por pagar, el anticipo o la cuota correspondiente, registrando los valores de ajuste, porcentajes, saldos anteriores y actuales, modificaciones de obligación y el concepto de ajuste utilizado. Sirve para el control de pagos, cruces de anticipos y conciliación de cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotesAccountPayableAdvance';
