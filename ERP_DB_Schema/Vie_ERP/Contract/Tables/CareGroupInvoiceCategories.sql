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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la categoria a la que tiene permiso', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'Id';

