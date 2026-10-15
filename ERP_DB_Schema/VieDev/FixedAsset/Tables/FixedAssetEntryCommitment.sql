CREATE TABLE [FixedAsset].[FixedAssetEntryCommitment] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryId]  INT             NOT NULL,
    [CommitmentDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryCommitment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryCommitment_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryCommitment_FixedAssetEntry] FOREIGN KEY ([FixedAssetEntryId]) REFERENCES [FixedAsset].[FixedAssetEntry] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryCommitment] NOCHECK CONSTRAINT [FK_FixedAssetEntryCommitment_CommitmentDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryCommitment] NOCHECK CONSTRAINT [FK_FixedAssetEntryCommitment_FixedAssetEntry];

