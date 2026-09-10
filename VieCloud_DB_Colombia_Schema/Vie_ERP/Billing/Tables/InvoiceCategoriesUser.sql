CREATE TABLE [Billing].[InvoiceCategoriesUser] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceCategoriesId] INT          NOT NULL,
    [UserId]              INT          NOT NULL,
    [UserCode]            VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_InvoiceCategoriesUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceCategoriesUser_InvoiceCategories] FOREIGN KEY ([InvoiceCategoriesId]) REFERENCES [Billing].[InvoiceCategories] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario de seguridad; identificador de login o credencial del operador; VARCHAR(50); permite auditoría y trazabilidad de permisos en facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario de seguridad autorizado; FK implícita al catálogo de usuarios; vinculación de permisos de facturación por usuario; INT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la categoría de factura (RIPS, glosa, contrato, urgencia, procedimiento, etc.) para la que se otorga permiso; FK a [Billing].[InvoiceCategories]; INT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la categoria a la que tiene permiso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'InvoiceCategoriesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de autorización o asignación de permiso; clave primaria clustered; INT IDENTITY; mapea usuario a categoría factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre categorías de facturación y los usuarios autorizados para usarlas. Permite controlar qué usuarios o roles pueden generar facturas bajo cada categoría de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCategoriesUser';
