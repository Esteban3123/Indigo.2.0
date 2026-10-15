CREATE TABLE [Billing].[InvoiceCategoriesUser] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceCategoriesId] INT          NOT NULL,
    [UserId]              INT          NOT NULL,
    [UserCode]            VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_InvoiceCategoriesUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceCategoriesUser_InvoiceCategories] FOREIGN KEY ([InvoiceCategoriesId]) REFERENCES [Billing].[InvoiceCategories] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceCategoriesUser] NOCHECK CONSTRAINT [FK_InvoiceCategoriesUser_InvoiceCategories];

