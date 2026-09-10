CREATE TABLE [Cost].[CostDirectDistributionSecondaryDetailRedistribution] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DirectDistributionSecondaryDetailId] INT             NOT NULL,
    [ProductionCenterId]                  INT             NOT NULL,
    [Percentage]                          NUMERIC (5, 2)  NOT NULL,
    [Value]                               DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDirectDistributionSecondaryDetailRedistribution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetailRedistribution_CostDirectDistributionSecondaryDetail] FOREIGN KEY ([DirectDistributionSecondaryDetailId]) REFERENCES [Cost].[CostDirectDistributionSecondaryDetail] ([Id]),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetailRedistribution_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) de la redistribución de costos; la sumatoria de todos los valores redistribuidos debe coincidir exactamente con el valor total del detalle de distribución secundaria parent.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la distribucion redistribuidad, la sumatoria debe concordar con el valor del detalle', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de redistribución (NUMERIC 5,2); la sumatoria de todos los porcentajes redistribuidos en este detalle debe totalizar 100% o concordar con el porcentaje asignado en el detalle parent.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje redistribuido, la sumatoria debe concordar con el porcentaje del detalle', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del centro de producción, unidad funcional operativa, o área generadora de costos que recibe la redistribución; referencia a CostProductionCenter.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de producción operativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del detalle de distribución secundaria parent que origina esta redistribución de costos; referencia a CostDirectDistributionSecondaryDetail.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de distribución secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) que identifica cada registro de redistribución secundaria en la tabla.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Redistribución secundaria del detalle de distribución directa de costos: registra cómo se reparte un costo entre centros de producción mediante un porcentaje y valor asignado a cada uno, dentro del proceso de distribución secundaria de costos directos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution';
