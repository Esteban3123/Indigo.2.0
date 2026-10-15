CREATE TABLE [Treasury].[CashReceiptConceptUser] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [UserId]              INT          NOT NULL,
    [UserCode]            VARCHAR (50) NOT NULL,
    [CashReceipConceptId] INT          NOT NULL,
    CONSTRAINT [PK_CashReceiptConceptUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptConcept_CashReceiptConceptUser] FOREIGN KEY ([CashReceipConceptId]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptConceptUser] NOCHECK CONSTRAINT [FK_CashReceiptConcept_CashReceiptConceptUser];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de recepción de efectivo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConceptUser', @level2type = N'COLUMN', @level2name = N'CashReceipConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el código del usuario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConceptUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConceptUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConceptUser', @level2type = N'COLUMN', @level2name = N'Id';

