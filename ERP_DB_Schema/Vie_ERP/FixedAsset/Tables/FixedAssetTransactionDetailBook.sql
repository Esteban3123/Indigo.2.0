CREATE TABLE [FixedAsset].[FixedAssetTransactionDetailBook] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetTransactionDetailId] INT             NOT NULL,
    [LegalBookId]                   INT             NOT NULL,
    [Value]                         DECIMAL (18, 2) NOT NULL,
    [LifeTime]                      INT             NOT NULL,
    [UnitLifeTime]                  TINYINT         NOT NULL,
    CONSTRAINT [PK_FixedAssetTransactionDetailBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetTransactionDetailBook_FixedAssetTransactionDetail] FOREIGN KEY ([FixedAssetTransactionDetailId]) REFERENCES [FixedAsset].[FixedAssetTransactionDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetTransactionDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetailBook_FixedAssetTransactionDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetailBook_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia  se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida Util expresada en dias ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor que se va afectar en ese libro contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetailBook', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Libro contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de transacción de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetTransactionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetailBook', @level2type = N'COLUMN', @level2name = N'Id';

