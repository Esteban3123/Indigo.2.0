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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'valor de ajuste', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';

