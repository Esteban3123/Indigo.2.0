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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de factura nueva', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'NewInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de factura anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'PreviousInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'Id';

