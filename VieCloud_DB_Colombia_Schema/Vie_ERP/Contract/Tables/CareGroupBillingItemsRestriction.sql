CREATE TABLE [Contract].[CareGroupBillingItemsRestriction] (
    [Id]                        INT IDENTITY (1, 1) NOT NULL,
    [CareGroupId]               INT NOT NULL,
    [BillingItemsRestrictionId] INT NOT NULL,
    CONSTRAINT [PK_CareGroupBillingItemsRestriction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupBillingItemsRestriction_BillingItemsRestriction] FOREIGN KEY ([BillingItemsRestrictionId]) REFERENCES [Contract].[BillingItemsRestriction] ([Id]),
    CONSTRAINT [FK_CareGroupBillingItemsRestriction_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la restricción de artículos/ítems de facturación (FK a BillingItemsRestriction). Define qué códigos, procedimientos, servicios o medicamentos están limitados o excluidos en la facturación del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la restricción del articulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención, unidad funcional o red de prestadores (FK a CareGroup). Agrupa centros de atención, clínicas o puntos de servicio bajo un mismo contrato o plan.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincremental (PK) de la relación entre grupo de atención y restricción de facturación. Clave primaria de la tabla de asociación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Restricciones de ítems de facturación asociadas a grupos de atención (care groups) dentro de los contratos. Permite definir qué servicios o conceptos de cobro están limitados o excluidos para un grupo de atención específico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction';
