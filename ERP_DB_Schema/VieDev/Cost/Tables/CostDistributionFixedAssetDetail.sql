CREATE TABLE [Cost].[CostDistributionFixedAssetDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionFixedAssetId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [Proportion]               NUMERIC (7, 4)  NOT NULL,
    [DepreciationValue]        DECIMAL (18, 2) NOT NULL,
    [HoursQuantity]            INT             CONSTRAINT [DF_CostDistributionFixedAssetDetail_HoursQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionFixedAssetDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionFixedAssetDetail_CostDistributionFixedAsset] FOREIGN KEY ([DistributionFixedAssetId]) REFERENCES [Cost].[CostDistributionFixedAsset] ([Id]),
    CONSTRAINT [FK_CostDistributionFixedAssetDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionFixedAssetDetail] NOCHECK CONSTRAINT [FK_CostDistributionFixedAssetDetail_CostDistributionFixedAsset];


GO
ALTER TABLE [Cost].[CostDistributionFixedAssetDetail] NOCHECK CONSTRAINT [FK_CostDistributionFixedAssetDetail_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';

