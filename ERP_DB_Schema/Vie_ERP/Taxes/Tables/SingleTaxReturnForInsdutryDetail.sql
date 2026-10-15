CREATE TABLE [Taxes].[SingleTaxReturnForInsdutryDetail] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSingleTaxReturnForInsdutry] BIGINT       NOT NULL,
    [IdEconomicActivity]           INT          NOT NULL,
    [TaxableValue]                 NUMERIC (18) NULL,
    [TotalValue]                   NUMERIC (18) NULL,
    CONSTRAINT [PK_SingleTaxReturnForInsdutryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SingleTaxReturnForInsdutryDetail_SingleTaxReturnForInsdutry] FOREIGN KEY ([IdSingleTaxReturnForInsdutry]) REFERENCES [Taxes].[SingleTaxReturnForInsdutry] ([Id]),
    CONSTRAINT [FK_SingleTaxReturnForInsdutryDetail_TaxesEconomicActivities] FOREIGN KEY ([IdEconomicActivity]) REFERENCES [Taxes].[TaxesEconomicActivities] ([Id])
);


GO
ALTER TABLE [Taxes].[SingleTaxReturnForInsdutryDetail] NOCHECK CONSTRAINT [FK_SingleTaxReturnForInsdutryDetail_SingleTaxReturnForInsdutry];


GO
ALTER TABLE [Taxes].[SingleTaxReturnForInsdutryDetail] NOCHECK CONSTRAINT [FK_SingleTaxReturnForInsdutryDetail_TaxesEconomicActivities];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del detalle.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor del impuesto.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TaxableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la actividad económica.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdEconomicActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de declaración única de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdSingleTaxReturnForInsdutry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de la inspección de declaración única de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'Id';

