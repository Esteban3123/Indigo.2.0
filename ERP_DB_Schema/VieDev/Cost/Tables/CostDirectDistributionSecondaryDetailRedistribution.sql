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

