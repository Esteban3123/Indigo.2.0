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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece el valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la cuota(Debito=1, Credito=2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota a la cual pertenece', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteShares', @level2type = N'COLUMN', @level2name = N'Id';

