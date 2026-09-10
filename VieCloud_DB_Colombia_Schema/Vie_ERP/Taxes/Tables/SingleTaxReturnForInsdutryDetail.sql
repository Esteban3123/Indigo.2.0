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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del detalle de la declaración única de impuestos; suma bruta antes de deducciones o ajustes fiscales. NUMERIC(18), nullable.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total del detalle.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base imponible o valor gravable del detalle; monto sobre el cual se calcula la obligación tributaria. NUMERIC(18), nullable.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TaxableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor del impuesto.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TaxableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'TaxableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica (FK→TaxesEconomicActivities). Clasifica el sector o rama de negocio en la declaración. INT, requerido.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdEconomicActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la actividad económica.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdEconomicActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdEconomicActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado de la Declaración Única de Impuestos (DUI); vincula el detalle a su declaración padre. BIGINT, requerido, FK→SingleTaxReturnForInsdutry.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdSingleTaxReturnForInsdutry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de declaración única de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdSingleTaxReturnForInsdutry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'IdSingleTaxReturnForInsdutry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del detalle de inspección o renglón de la Declaración Única de Impuestos. INT IDENTITY, auto-incrementado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del detalle de la inspección de declaración única de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las actividades económicas incluidas en una declaración unificada de impuestos por industria y comercio. Registra los valores gravables y totales por cada actividad económica asociada a una declaración.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SingleTaxReturnForInsdutryDetail';
