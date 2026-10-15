CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId]              INT             NOT NULL,
    [LegalBookId]                  INT             NOT NULL,
    [LifeTime]                     INT             NOT NULL,
    [UnitLifeTime]                 TINYINT         NOT NULL,
    [ValorizationDays]             INT             CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_ValorizationDays] DEFAULT ((0)) NOT NULL,
    [DaysPendingDepreciate]        INT             NOT NULL,
    [DepreciatedDays]              INT             NOT NULL,
    [DepreciationType]             TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]          NUMERIC (18)    CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]             NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_PercentageRescue] DEFAULT ((0)) NOT NULL,
    [Valorization]                 DECIMAL (18, 2) NOT NULL,
    [Devaluation]                  DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                DECIMAL (18, 2) NOT NULL,
    [TransactionValue]             DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_TransactionValue] DEFAULT ((0)) NOT NULL,
    [DepreciatedValue]             DECIMAL (18, 2) NOT NULL,
    [DepreciatedValuePart]         DECIMAL (18, 2) NOT NULL,
    [InflationAdjustmentValue]     DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_InflationAdjustmentValue] DEFAULT ((0)) NOT NULL,
    [ResidualValue]                DECIMAL (18, 2) NOT NULL,
    [ResidualValuePart]            DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]              DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_HistoricalValue] DEFAULT ((0)) NOT NULL,
    [ApplyMinimunAmount]           BIT             CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_ApplyMinimunAmount] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountDepreciated] DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__73580D95] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetDetailBook__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_FixedAssetPhysicalAssetDetailBook_ValidateHistoricalValue] CHECK ([HistoricalValue]=(([DepreciatedValue]+[ResidualValue])-[TransactionValue])),
    CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [CK_FixedAssetPhysicalAssetDetailBook_ValidateHistoricalValue];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetPhysicalAssetDetailBook__PhysicalAssetId__LegalBookId]
    ON [FixedAsset].[FixedAssetPhysicalAssetDetailBook]([PhysicalAssetId] ASC, [LegalBookId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que falta por depreciar o el valor en libros    La formula para obtener el valor residual deberia ser   Valor En Libros = (Valor Historico + Transacciones - Depreciaciones + ValorAjustadoPorInflacion)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado del activo, el cual debe se calcula y debe coincidir con la siguiente formula     Valor Hostorico + Valorizaciones - Desvalorizaciones', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias pendientes por depreciar    Nota: Este campo esta expresado en dias y cuando se registra el activo se registra la vida util con la que se ingreso y cuando se realice una transaccion que afecte el tiempo de vida util del activo este campo aumenta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia    se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';

