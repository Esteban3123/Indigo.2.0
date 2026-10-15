CREATE TABLE [Cost].[CostDistributionFixedAssetInitial] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     INT             NOT NULL,
    [FixedAssetPhysicalAssetId] INT             NOT NULL,
    [ProductionCenterId]        INT             NOT NULL,
    [DepreciatedDays]           INT             NOT NULL,
    [HoursQuantity]             INT             NOT NULL,
    [DepreciationValue]         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDistributionFixedAssetInitial] PRIMARY KEY CLUSTERED ([Id] ASC)
);

