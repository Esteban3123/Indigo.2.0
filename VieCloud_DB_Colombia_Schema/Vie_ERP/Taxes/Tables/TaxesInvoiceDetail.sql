CREATE TABLE [Taxes].[TaxesInvoiceDetail] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesInvoiceId]       INT            NOT NULL,
    [LiquidationConceptId] INT            NOT NULL,
    [PercentageConcept]    NUMERIC (5, 2) NOT NULL,
    [BaseValue]            NUMERIC (18)   NOT NULL,
    [Value]                NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_TaxesInvoiceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesInvoiceDetail_TaxesInvoice] FOREIGN KEY ([TaxesInvoiceId]) REFERENCES [Taxes].[TaxesInvoice] ([Id]),
    CONSTRAINT [FK_TaxesInvoiceDetail_TaxesLiquidationConcept] FOREIGN KEY ([LiquidationConceptId]) REFERENCES [Taxes].[TaxesLiquidationConcept] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario calculado del detalle de factura de impuestos (impuesto, retención, descuento, recargo). Resultado de BaseValue × PercentageConcept/100. Tipo: NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del detalle de factura de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base sobre el cual se aplica el porcentaje para calcular el impuesto o concepto de liquidación. Monto antes de aplicar el porcentaje. Tipo: NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tasa del concepto de liquidación a aplicar sobre la base (ej: IVA 19%, retención 2%). Tipo: NUMERIC(5,2).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'PercentageConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del concepto.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'PercentageConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'PercentageConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del concepto de liquidación (FK a TaxesLiquidationConcept). Referencia al tipo de impuesto, retención, descuento o recargo. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de liquidación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la factura de impuestos (FK a TaxesInvoice). Vincula el detalle a la factura principal de impuestos/liquidación. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de factura de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle (clave primaria). Secuencia incremental para rastrear cada línea de concepto de liquidación. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos tributarios aplicados en cada factura de impuestos. Registra el desglose por concepto de liquidación, con su porcentaje, base gravable y valor calculado del impuesto o retención correspondiente.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail';
