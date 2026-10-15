CREATE TABLE [FixedAsset].[FixedAssetPartsAccesoriesConsumablesDetail] (
    [Id]                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PartsAccesoriesConsumiblesId] INT            NOT NULL,
    [LegalBookId]                  INT            NOT NULL,
    [LifeTime]                     INT            NOT NULL,
    [UnitLifeTime]                 TINYINT        NOT NULL,
    [DepreciationType]             TINYINT        CONSTRAINT [DF_FixedAssetPartsAccesoriesConsumablesDetail_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]          NUMERIC (18)   CONSTRAINT [DF_FixedAssetPartsAccesoriesConsumablesDetail_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]             NUMERIC (5, 2) CONSTRAINT [DF_FixedAssetPartsAccesoriesConsumablesDetail_PercentageRescue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetPartsAccesoriesConsumablesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPartsAccesoriesConsumablesDetail_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartsAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_FixedAssetPartsAccesoriesConsumablesDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPartsAccesoriesConsumablesDetail] NOCHECK CONSTRAINT [FK_FixedAssetPartsAccesoriesConsumablesDetail_FixedAssetPartsAccesoriesConsumables];


GO
ALTER TABLE [FixedAsset].[FixedAssetPartsAccesoriesConsumablesDetail] NOCHECK CONSTRAINT [FK_FixedAssetPartsAccesoriesConsumablesDetail_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida Util', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Libro Oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la parte o accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'Id';

