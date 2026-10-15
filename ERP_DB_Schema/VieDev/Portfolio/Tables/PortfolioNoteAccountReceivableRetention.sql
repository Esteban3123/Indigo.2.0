CREATE TABLE [Portfolio].[PortfolioNoteAccountReceivableRetention] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteAccountReceivableId] INT             NOT NULL,
    [InvoiceCustomerRetentionId]       INT             NOT NULL,
    [Nature]                           TINYINT         NOT NULL,
    [RetentionRate]                    NUMERIC (6, 3)  NOT NULL,
    [BaseValue]                        NUMERIC (18, 2) NOT NULL,
    [Value]                            NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceCustomerRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableRetention_InvoiceCustomerRetention] FOREIGN KEY ([InvoiceCustomerRetentionId]) REFERENCES [Billing].[InvoiceCustomerRetention] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableRetention_PortfolioNoteAccountReceivableAdvance] FOREIGN KEY ([PortfolioNoteAccountReceivableId]) REFERENCES [Portfolio].[PortfolioNoteAccountReceivableAdvance] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableRetention] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableRetention_InvoiceCustomerRetention];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableRetention] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableRetention_PortfolioNoteAccountReceivableAdvance];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la nota  1 - Débito  2 - Crédito    Las nota de naturaleza débito aumentan el valor de la retención, caso contrario realiza las notas de naturaleza crédito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Id';

