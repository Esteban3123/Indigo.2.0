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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas adicionales o comentarios descriptivos sobre la distribución de la cuota en la nota de pago (VARCHAR 200, nullable)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la cuota distribuida, establece el monto asignado a esta línea de pago (DECIMAL 18, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto contable asociado a la nota de pago, referencia a AccountPayableConceptNotes (FK, INT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable de la cuota: Débito=1 o Crédito=2, indica si es cargo o abono (TINYINT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la cuota(Debito=1, Credito=2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuota o pago a la cual pertenece esta distribución, referencia a AccountPayableShares (FK, INT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota a la cual pertenece', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nota de pago padre, referencia a PaymentNotes, vincula el registro a la nota de pago principal (FK, INT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de distribución de cuota en nota de pago (INT IDENTITY, clave primaria, auto-incrementado)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de distribución o participación de las notas de pago (débito/crédito) entre las cuotas o compromisos de cuentas por pagar, indicando el concepto contable, la naturaleza del movimiento y el valor asignado a cada porción.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares';
