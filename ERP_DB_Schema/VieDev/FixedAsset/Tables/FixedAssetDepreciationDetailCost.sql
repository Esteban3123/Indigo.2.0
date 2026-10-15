CREATE TABLE [FixedAsset].[FixedAssetDepreciationDetailCost] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetDepreciationDetailId] INT             NOT NULL,
    [MainAccountId]                  INT             NOT NULL,
    [ResponsibleId]                  INT             NOT NULL,
    [LocationId]                     INT             NOT NULL,
    [CostCenterId]                   INT             NOT NULL,
    [DepreciatedDays]                INT             NOT NULL,
    [DepreciationValue]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetDepreciationDetailCost__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetDepreciationDetailCost_FixedAssetDepreciationDetail] FOREIGN KEY ([FixedAssetDepreciationDetailId]) REFERENCES [FixedAsset].[FixedAssetDepreciationDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetDepreciationDetailCost_FixedAssetDepreciationDetail];

