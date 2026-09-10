CREATE TABLE [Billing].[BillingConceptAccountingPackage] (
    [Id]                              INT     IDENTITY (1, 1) NOT NULL,
    [BillingConceptId]                INT     NOT NULL,
    [UnitType]                        TINYINT NOT NULL,
    [ConceptMainAccountId]            INT     NOT NULL,
    [FavorableDeviationMainAccountId] INT     NOT NULL,
    [VariationPVMainAccountId]        INT     NULL,
    CONSTRAINT [PK_BillingConceptAccountingPackage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingConceptAccountingPackage_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_BillingConceptAccountingPackage_ConceptMainAccount] FOREIGN KEY ([ConceptMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccountingPackage_FavorableDeviationMainAccount] FOREIGN KEY ([FavorableDeviationMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccountingPackage_VariationPVMainAccount] FOREIGN KEY ([VariationPVMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal para variación de precio venta (PV). Referencia FK a GeneralLedger.MainAccounts. Nullable. Usado en análisis de desviaciones de precio en facturación y glosas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Variación PV', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal para registrar desviaciones favorables (ganancias por diferencia de precio). Referencia FK a GeneralLedger.MainAccounts. Clave para análisis contable de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de desviación favorable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal asociada al concepto de facturación. Referencia FK a GeneralLedger.MainAccounts. Vínculo contable directo del concepto de factura, glosa, receta o procedimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'ConceptMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la cuenta contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'ConceptMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'ConceptMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de unidad funcional/centro de atención para facturación: 1=Urgencias, 2=Hospitalización, 3=Quirófanos, 4=Servicios Ambulatorios. TINYINT. Define el contexto clínico del concepto facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de unidad  1 - Urgencias  2 - Hospitalizacion  3 - Quirofanos  4 - Servicios Ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de facturación (atención, procedimiento, recurso). Referencia FK a Billing.BillingConcept. Vincula el paquete contable a cada concepto de factura, RIPS, glosa o receta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (primary key, identity) de la relación entre concepto de facturación y su configuración contable (cuentas principales y tipo de unidad).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paquete contable de conceptos de facturación: relaciona cada concepto de facturación con las cuentas contables principales que se deben usar según el tipo de unidad, incluyendo la cuenta principal del concepto, la cuenta para desviaciones favorables y la cuenta de variación de precio de venta (PVP).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage';
