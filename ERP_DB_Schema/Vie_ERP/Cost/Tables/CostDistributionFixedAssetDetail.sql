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
CREATE NONCLUSTERED INDEX [IX_CostDistributionFixedAssetDetail_DistributionFixedAssetId]
    ON [Cost].[CostDistributionFixedAssetDetail]([DistributionFixedAssetId])
    INCLUDE ([Id], [ProductionCenterId], [HoursQuantity], [Proportion], [DepreciationValue]);


GO
CREATE NONCLUSTERED INDEX [IX_CostDistributionFixedAssetDetail_DistributionFixedAssetId_ProductionCenterId]
    ON [Cost].[CostDistributionFixedAssetDetail]([DistributionFixedAssetId], [ProductionCenterId])
    INCLUDE ([Id]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas usadas en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de proporcion que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la distribucion de activos fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';

