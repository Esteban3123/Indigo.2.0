CREATE TABLE [Taxes].[TaxesLiquidationDetailConcept] (
    [Id]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesLiquidationDetailId]  INT            NOT NULL,
    [TaxesLiquidationConceptId] INT            NOT NULL,
    [PercentageConcept]         NUMERIC (5, 2) NOT NULL,
    [BaseValue]                 NUMERIC (18)   NOT NULL,
    [Value]                     NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_TaxesLiquidationDetailConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationConcept] FOREIGN KEY ([TaxesLiquidationConceptId]) REFERENCES [Taxes].[TaxesLiquidationConcept] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationDetail] FOREIGN KEY ([TaxesLiquidationDetailId]) REFERENCES [Taxes].[TaxesLiquidationDetail] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetailConcept__TaxesLiquidationDetailId]
    ON [Taxes].[TaxesLiquidationDetailConcept]([TaxesLiquidationDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor calculado del concepto de impuesto; monto resultante de aplicar el porcentaje a la base (NUMERIC 18, sin decimales). Sinónimos: importe, monto, cantidad de impuesto, valor liquidado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base sobre el cual se calcula el porcentaje del concepto; base gravable o base de cálculo del impuesto (NUMERIC 18, sin decimales). Sinónimos: base imponible, monto base, valor antes de porcentaje.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tarifa aplicable al concepto de impuesto específico; expresado en formato numérico 0.00 a 100.00 (NUMERIC 5,2). Sinónimos: tasa, alícuota, porcentaje de retención, porcentaje tributario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'PercentageConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de porcentaje', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'PercentageConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'PercentageConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al concepto de impuesto asociado; vincula con tabla TaxesLiquidationConcept para obtener tipo de impuesto, retención o contribución. Sinónimos: ID concepto, concepto tributario, tipo de liquidación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TaxesLiquidationConceptId', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al detalle de liquidación de impuestos padre; vincula con tabla TaxesLiquidationDetail. Sinónimos: ID detalle liquidación, referencia detalle.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de liquidación de impuestos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY 1,1) de cada línea conceptual en el detalle de liquidación de impuestos. Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos aplicados en la liquidación de impuestos o contribuciones: registra cada concepto tributario (porcentaje, base gravable y valor calculado) asociado a una línea de liquidación de taxes.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept';
