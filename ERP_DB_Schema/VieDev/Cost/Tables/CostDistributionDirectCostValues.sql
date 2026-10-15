CREATE TABLE [Cost].[CostDistributionDirectCostValues] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [ByArea]                   DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByArea] DEFAULT ((0)) NOT NULL,
    [ByOfficialHours]          DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByOfficialHours] DEFAULT ((0)) NOT NULL,
    [BySupplyValue]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_BySupplyValue] DEFAULT ((0)) NOT NULL,
    [ByWorkmanship]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByWorkmanship] DEFAULT ((0)) NOT NULL,
    [ByAssetValue]             DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByAssetValue] DEFAULT ((0)) NOT NULL,
    [BySales]                  DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_BySales] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostValues_CostDistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostValues_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCostValues] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostValues_CostDistributionDirectCost];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostValues] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostValues_CostProductionCenter];

