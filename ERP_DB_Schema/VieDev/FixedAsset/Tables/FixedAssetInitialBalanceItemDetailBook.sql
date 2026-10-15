CREATE TABLE [FixedAsset].[FixedAssetInitialBalanceItemDetailBook] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetInitialBalanceItemId] INT             NOT NULL,
    [LegalBookId]                    INT             NOT NULL,
    [LifeTime]                       INT             NOT NULL,
    [UnitLifeTime]                   TINYINT         NOT NULL,
    [DaysPendingDepreciate]          INT             NOT NULL,
    [DepreciatedDays]                INT             NOT NULL,
    [DepreciationType]               TINYINT         CONSTRAINT [DF_FixedAssetInitialBalanceItemDetailBook_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]            NUMERIC (18)    CONSTRAINT [DF_FixedAssetInitialBalanceItemDetailBook_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]               NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetInitialBalanceItemDetailBook_PercentageRescue] DEFAULT ((0)) NOT NULL,
    [DepreciatedValue]               DECIMAL (18, 2) NOT NULL,
    [DepreciatedValuePart]           DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                  DECIMAL (18, 2) NOT NULL,
    [ResidualValuePart]              DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]                DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetInitialBalanceItemDetailBook_HistoricalValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetInitialBalanceItemDetailBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetInitialBalanceItemDetailBook_FixedAssetInitialBalanceItem] FOREIGN KEY ([FixedAssetInitialBalanceItemId]) REFERENCES [FixedAsset].[FixedAssetInitialBalanceItem] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItemDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItemDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItemDetailBook_FixedAssetInitialBalanceItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItemDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItemDetailBook_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia    se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';

