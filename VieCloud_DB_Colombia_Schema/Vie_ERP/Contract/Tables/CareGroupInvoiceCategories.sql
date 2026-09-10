CREATE TABLE [Contract].[CareGroupInvoiceCategories] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]         INT NOT NULL,
    [InvoiceCategoriesId] INT NOT NULL,
    CONSTRAINT [PK_CareGroupInvoiceCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupInvoiceCategories_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_CareGroupInvoiceCategories_InvoiceCategories] FOREIGN KEY ([InvoiceCategoriesId]) REFERENCES [Billing].[InvoiceCategories] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la categoría de facturación a la cual el grupo de atención tiene permiso. Referencia FK a Billing.InvoiceCategories. Determina qué tipos de servicios, procedimientos o conceptos de factura puede procesar el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la categoria a la que tiene permiso', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención (unidad funcional, centro de atención, red de prestadores) autorizado para usar la categoría de facturación. Referencia FK a Contract.CareGroup. Vincula el contrato con la estructura organizacional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la relación entre grupo de atención y categoría de facturación. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre grupos de atención y categorías de facturación. Indica qué categorías de factura aplican a cada grupo de atención dentro de un contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupInvoiceCategories';
