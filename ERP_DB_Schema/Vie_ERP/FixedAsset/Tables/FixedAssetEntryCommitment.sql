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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con el cual se generará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del ingreso de activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Id';

