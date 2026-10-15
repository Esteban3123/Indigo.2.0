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
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetailRedistribution] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetailRedistribution_CostDirectDistributionSecondaryDetail];


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetailRedistribution] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetailRedistribution_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la distribucion redistribuidad, la sumatoria debe concordar con el valor del detalle', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje redistribuido, la sumatoria debe concordar con el porcentaje del detalle', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de producción operativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de distribución secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetailRedistribution', @level2type = N'COLUMN', @level2name = N'Id';

