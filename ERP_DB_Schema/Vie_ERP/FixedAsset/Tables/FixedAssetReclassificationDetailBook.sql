CREATE TABLE [FixedAsset].[FixedAssetReclassificationDetailBook] (
    [Id]                                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetReclassificationDetailId] INT             NOT NULL,
    [LegalBookId]                        INT             NOT NULL,
    [DaysPendingDepreciate]              INT             NOT NULL,
    [DepreciatedDays]                    INT             NOT NULL,
    [Valorization]                       DECIMAL (18, 2) NOT NULL,
    [Devaluation]                        DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                      DECIMAL (18, 2) NOT NULL,
    [TransactionValue]                   DECIMAL (18, 2) NOT NULL,
    [DepreciatedValue]                   DECIMAL (18, 2) NOT NULL,
    [InflationAdjustmentValue]           DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                      DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]                    DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetReclassificationDetailBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetReclassificationDetailBook_FixedAssetReclassificationDetail] FOREIGN KEY ([FixedAssetReclassificationDetailId]) REFERENCES [FixedAsset].[FixedAssetReclassificationDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassificationDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassificationDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetReclassificationDetailBook_FixedAssetReclassificationDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassificationDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetReclassificationDetailBook_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor Historico del activo por Libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que falta por depreciar al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor del Ajuste por Inflacion Acumulado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que se ha depreciado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de las transacciones, es decir las valorizaciones al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'valor de ajuste', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas la desvalorizaciones que se le han realizado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas las valorizaciones que se le han realizado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias que ya fueron depreciados al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias pendientes por depreciar al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Libro Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Id';

