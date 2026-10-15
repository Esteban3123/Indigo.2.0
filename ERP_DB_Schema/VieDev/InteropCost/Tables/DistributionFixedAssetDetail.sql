CREATE TABLE [InteropCost].[DistributionFixedAssetDetail] (
    [Id]                       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionFixedAssetId] INT            NOT NULL,
    [ProductionCenterId]       INT            NOT NULL,
    [Proportion]               NUMERIC (5, 2) NOT NULL,
    [DepreciationValue]        NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_DistributionFixedAssetDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionFixedAssetDetail_DistributionFixedAsset] FOREIGN KEY ([DistributionFixedAssetId]) REFERENCES [InteropCost].[DistributionFixedAsset] ([Id]),
    CONSTRAINT [FK_DistributionFixedAssetDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionFixedAssetDetail] NOCHECK CONSTRAINT [FK_DistributionFixedAssetDetail_DistributionFixedAsset];


GO
ALTER TABLE [InteropCost].[DistributionFixedAssetDetail] NOCHECK CONSTRAINT [FK_DistributionFixedAssetDetail_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';

