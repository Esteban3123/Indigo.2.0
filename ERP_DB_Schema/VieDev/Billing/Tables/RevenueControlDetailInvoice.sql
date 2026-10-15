CREATE TABLE [Billing].[RevenueControlDetailInvoice] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueControlDetailId] INT          NOT NULL,
    [InvoiceId]              INT          NOT NULL,
    [Status]                 BIT          NOT NULL,
    [CreationUser]           VARCHAR (20) NOT NULL,
    [CreationDate]           DATETIME     NOT NULL,
    [ModificationUser]       VARCHAR (20) NULL,
    [ModificationDate]       DATETIME     NULL,
    CONSTRAINT [PK_RevenueControlDetailInvoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueControlDetailInvoice_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_RevenueControlDetailInvoice_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id])
);


GO
ALTER TABLE [Billing].[RevenueControlDetailInvoice] NOCHECK CONSTRAINT [FK_RevenueControlDetailInvoice_Invoice];


GO
ALTER TABLE [Billing].[RevenueControlDetailInvoice] NOCHECK CONSTRAINT [FK_RevenueControlDetailInvoice_RevenueControlDetail];

