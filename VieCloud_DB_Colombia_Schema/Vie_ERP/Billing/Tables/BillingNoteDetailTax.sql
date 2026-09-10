CREATE TABLE [Billing].[BillingNoteDetailTax] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingNoteDetailId] INT             NOT NULL,
    [TaxPercentage]       NUMERIC (5, 2)  NOT NULL,
    [TaxValue]            NUMERIC (20, 2) NOT NULL,
    [BaseValue]           NUMERIC (20, 2) CONSTRAINT [DF_BillingNoteDetailTax_BaseValue] DEFAULT ((0)) NOT NULL,
    [IVAId]     INT NULL,
    CONSTRAINT [PK_BillingNoteDetailTax] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingNoteDetailTax_BillingNoteDetail] FOREIGN KEY ([BillingNoteDetailId]) REFERENCES [Billing].[BillingNoteDetail] ([Id]),
    CONSTRAINT [FK_BillingNoteDetailTax_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base o monto antes de impuestos sobre el cual se calcula el tributo (IVA, ICE, etc.); numérico de hasta 20 dígitos con 2 decimales, usado en facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base para el calculo del impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total del impuesto (IVA, ICE u otro tributo) calculado y aplicado al detalle de la nota de facturación; numérico de hasta 20 dígitos con 2 decimales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de tributación aplicado (ej: 19% IVA, 5% ICE); numérico de 5 dígitos con 2 decimales; usado en cálculo de impuestos de atención en salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del Impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia el detalle específico de la nota de facturación/factura en [Billing].[BillingNoteDetail]; vincul con línea de atención o procedimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BillingNoteDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Detalle de la Nota ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BillingNoteDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BillingNoteDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (identity) del registro de impuesto en la nota de facturación; clave primaria de la tabla BillingNoteDetailTax.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuestos aplicados a cada ítem o detalle de una nota de facturación (nota débito o crédito). Registra el porcentaje de impuesto, el valor base gravable y el monto del impuesto calculado para cada línea de la nota.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax';
