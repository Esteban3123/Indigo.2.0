CREATE TABLE [Billing].[InvoicePortfolioAdvance] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]          INT             NOT NULL,
    [PortfolioAdvanceId] INT             NOT NULL,
    [Value]              NUMERIC (20, 2) NOT NULL,
    CONSTRAINT [PK_InvoicePortfolioAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoicePortfolioAdvance_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoicePortfolioAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id])
);


GO
ALTER TABLE [Billing].[InvoicePortfolioAdvance] NOCHECK CONSTRAINT [FK_InvoicePortfolioAdvance_Invoice];


GO
ALTER TABLE [Billing].[InvoicePortfolioAdvance] NOCHECK CONSTRAINT [FK_InvoicePortfolioAdvance_PortfolioAdvance];


GO
CREATE NONCLUSTERED INDEX [IX_InvoicePortfolioAdvance__InvoiceId]
    ON [Billing].[InvoicePortfolioAdvance]([InvoiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Cruzado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Anticipo del Paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';

