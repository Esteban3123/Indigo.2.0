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
ALTER TABLE [Taxes].[TaxesLiquidationDetailConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationConcept];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetailConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationDetail];


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetailConcept__TaxesLiquidationDetailId]
    ON [Taxes].[TaxesLiquidationDetailConcept]([TaxesLiquidationDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor base', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de porcentaje', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'PercentageConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TaxesLiquidationConceptId', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de liquidación de impuestos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';

