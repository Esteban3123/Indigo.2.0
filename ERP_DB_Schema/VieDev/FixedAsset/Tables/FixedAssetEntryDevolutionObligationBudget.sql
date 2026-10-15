CREATE TABLE [FixedAsset].[FixedAssetEntryDevolutionObligationBudget] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryDevolutionId] INT             NOT NULL,
    [ObligationDetailId]          INT             NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryDevolutionObligationBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_FixedAssetEntryDevolution] FOREIGN KEY ([FixedAssetEntryDevolutionId]) REFERENCES [FixedAsset].[FixedAssetEntryDevolution] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionObligationBudget] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_FixedAssetEntryDevolution];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionObligationBudget] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_ObligationDetail];

