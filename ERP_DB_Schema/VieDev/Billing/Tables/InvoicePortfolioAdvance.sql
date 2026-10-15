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

