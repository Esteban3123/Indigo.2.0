CREATE TABLE [Cost].[CostActivityStepFixedAsset] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [FixedAssetItemId]   INT             NOT NULL,
    [Hours]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepFixedAsset__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepFixedAsset_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepFixedAsset_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStepFixedAsset] NOCHECK CONSTRAINT [FK_CostActivityStepFixedAsset_CostActivityStep];


GO
ALTER TABLE [Cost].[CostActivityStepFixedAsset] NOCHECK CONSTRAINT [FK_CostActivityStepFixedAsset_FixedAssetItem];

