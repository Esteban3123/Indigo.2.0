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


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetailCost__FixedAssetDepreciationDetailId__INC__Id]
    ON [FixedAsset].[FixedAssetDepreciationDetailCost]([FixedAssetDepreciationDetailId] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetailCost__LocationId__INC__FixedAssetDepreciationDetailId]
    ON [FixedAsset].[FixedAssetDepreciationDetailCost]([LocationId] ASC)
    INCLUDE([FixedAssetDepreciationDetailId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la depreciacion del mes en el Centro de costo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de dias depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de amortización de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'Id';

