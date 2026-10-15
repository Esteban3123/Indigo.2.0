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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la categoria a la que tiene permiso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'Id';

