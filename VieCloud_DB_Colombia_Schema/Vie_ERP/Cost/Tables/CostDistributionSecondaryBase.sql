CREATE TABLE [Cost].[CostDistributionSecondaryBase] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryId] INT           NOT NULL,
    [DistributionType]        TINYINT       NOT NULL,
    [ImpactPoints]            TINYINT       NOT NULL,
    [MeasurementUnit]         TINYINT       NOT NULL,
    [Description]             VARCHAR (300) NOT NULL,
    [Area]                    BIT           NOT NULL,
    [OfficialHours]           BIT           NOT NULL,
    [SupplyValue]             BIT           NOT NULL,
    [WorkmanshipValue]        BIT           NOT NULL,
    [AssetValue]              BIT           NOT NULL,
    [CousinValue]             BIT           NOT NULL,
    [InvoiceValue]            BIT           NOT NULL,
    [MultipleBase]            TINYINT       NOT NULL,
    CONSTRAINT [PK_CostDistributionSecondaryBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionSecondaryBase_CostDistributionSecondary] FOREIGN KEY ([DistributionSecondaryId]) REFERENCES [Cost].[CostDistributionSecondary] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selector (TINYINT) de distribución múltiple (1=Base A, 2=Base B, 3=Base C, 4=Base D). Si hay múltiples, deben ser consecutivas (1,2,3,4)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para distribución por valor facturado. Especifica si se busca la distribución según montos de facturación a terceros', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Facturado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para distribución por valor del primo (variable relacionada). Base de búsqueda para asignación de costos indirectos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor del primo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para distribución por valor del activo. Especifica si se busca la distribución según depreciación o valor de equipos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Del Activo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para distribución por mano de obra. Especifica si se busca la distribución según costo de labor/servicios profesionales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para distribución por valor de suministros. Especifica si se busca la distribución según costo de insumos/materiales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Suministro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de distribución por horas de funcionario/profesional. Define si se usa tiempo laboral como base de asignación de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el tipo de distribucion de busqueda se va hacer por Horas Funcionario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de costeo por área en metros cuadrados. Solo se habilita cuando DistributionType=Buscada. Afecta cálculo de costos por m²', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la opcion de costeo es por Area en metros cuadrados, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la base de distribución secundaria de costos (VARCHAR 300). Identifica el propósito o concepto de la distribución', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para distribución (TINYINT): 1=Proporción, 2=Valor. Solo se habilita si DistributionType=Calculada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntos de incidencia o impacto (1-10, TINYINT). Escala de severidad que pondera la distribución secundaria de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución de costos (TINYINT): 1=Directa, 2=Calculada, 3=Buscada. Define el método de asignación de costos a unidades funcionales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de distribucion  1 - Directa  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la distribución secundaria de costos asociada, referencia a CostDistributionSecondary', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la distribucion ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la base de distribución secundaria de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bases secundarias para la distribución de costos: define los criterios, unidades de medida y componentes de valor (insumos, mano de obra, activos, facturación, etc.) que se aplican al repartir costos indirectos entre áreas o centros de costo en el módulo de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase';
