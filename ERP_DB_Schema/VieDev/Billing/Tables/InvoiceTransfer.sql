CREATE TABLE [Billing].[InvoiceTransfer] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PreviousInvoiceId] INT NOT NULL,
    [NewInvoiceId]      INT NOT NULL,
    CONSTRAINT [PK_InvoiceTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceTransfer_Invoice] FOREIGN KEY ([PreviousInvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceTransfer_Invoice1] FOREIGN KEY ([NewInvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceTransfer] NOCHECK CONSTRAINT [FK_InvoiceTransfer_Invoice];


GO
ALTER TABLE [Billing].[InvoiceTransfer] NOCHECK CONSTRAINT [FK_InvoiceTransfer_Invoice1];

