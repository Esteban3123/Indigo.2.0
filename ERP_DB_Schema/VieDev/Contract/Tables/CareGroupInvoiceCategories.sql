CREATE TABLE [Contract].[CareGroupInvoiceCategories] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]         INT NOT NULL,
    [InvoiceCategoriesId] INT NOT NULL,
    CONSTRAINT [PK_CareGroupInvoiceCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupInvoiceCategories_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_CareGroupInvoiceCategories_InvoiceCategories] FOREIGN KEY ([InvoiceCategoriesId]) REFERENCES [Billing].[InvoiceCategories] ([Id])
);


GO
ALTER TABLE [Contract].[CareGroupInvoiceCategories] NOCHECK CONSTRAINT [FK_CareGroupInvoiceCategories_CareGroup];


GO
ALTER TABLE [Contract].[CareGroupInvoiceCategories] NOCHECK CONSTRAINT [FK_CareGroupInvoiceCategories_InvoiceCategories];

