CREATE TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryDevolutionId] INT             NOT NULL,
    [FixedAssetEntryItemId]       INT             NOT NULL,
    [FixedAssetEntryItemDetailId] INT             NOT NULL,
    [UnitValue]                   DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]               DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]               NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                    DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]          NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]               DECIMAL (18, 2) NOT NULL,
    [TotalValue]                  DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]               NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetEntryDevolutionDetail_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryDevolution] FOREIGN KEY ([FixedAssetEntryDevolutionId]) REFERENCES [FixedAsset].[FixedAssetEntryDevolution] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItem] FOREIGN KEY ([FixedAssetEntryItemId]) REFERENCES [FixedAsset].[FixedAssetEntryItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItemDetail] FOREIGN KEY ([FixedAssetEntryItemDetailId]) REFERENCES [FixedAsset].[FixedAssetEntryItemDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryDevolution];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItemDetail];

