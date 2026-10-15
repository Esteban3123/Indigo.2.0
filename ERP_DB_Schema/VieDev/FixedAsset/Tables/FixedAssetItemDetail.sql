CREATE TABLE [FixedAsset].[FixedAssetItemDetail] (
    [Id]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId]    INT            NOT NULL,
    [LegalBookId]         INT            NOT NULL,
    [LifeTime]            INT            NOT NULL,
    [UnitLifeTime]        TINYINT        NOT NULL,
    [DepreciationType]    TINYINT        CONSTRAINT [DF_FixedAssetItemDetail_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit] NUMERIC (18)   CONSTRAINT [DF_FixedAssetItemDetail_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]    NUMERIC (5, 2) CONSTRAINT [DF_FixedAssetItemDetail_PercentageRescue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEquipmentDetail_FixedAssetEquipment] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentDetail_FixedAssetEquipment];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentDetail_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemDetail', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';

