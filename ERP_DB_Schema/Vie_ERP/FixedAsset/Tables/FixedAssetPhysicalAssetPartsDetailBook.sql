CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetPartsDetailBook] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetPartsId]     INT             NOT NULL,
    [LegalBookId]              INT             NOT NULL,
    [LifeTime]                 INT             NOT NULL,
    [UnitLifeTime]             TINYINT         NOT NULL,
    [ValorizationDays]         INT             CONSTRAINT [DF_FixedAssetPhysicalAssetPartsDetailBook_ValorizationDays] DEFAULT ((0)) NOT NULL,
    [DaysPendingDepreciate]    INT             NOT NULL,
    [DepreciatedDays]          INT             NOT NULL,
    [DepreciationType]         TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAssetPartsDetailBook_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]      NUMERIC (18)    CONSTRAINT [DF_FixedAssetPhysicalAssetPartsDetailBook_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]         NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetPhysicalAssetPartsDetailBook_PercentageRescue] DEFAULT ((0)) NOT NULL,
    [Valorization]             DECIMAL (18, 2) NOT NULL,
    [Devaluation]              DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]            DECIMAL (18, 2) NOT NULL,
    [TransactionValue]         DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetPartsDetailBook_TransactionValue] DEFAULT ((0)) NOT NULL,
    [DepreciatedValue]         DECIMAL (18, 2) NOT NULL,
    [InflationAdjustmentValue] DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetPartsDetailBook_InflationAdjustmentValue] DEFAULT ((0)) NOT NULL,
    [ResidualValue]            DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetPartsDetailBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAssetPartsDetailBook_FixedAssetPhysicalAssetParts] FOREIGN KEY ([PhysicalAssetPartsId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetParts] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAssetPartsDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetPartsDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetPartsDetailBook_FixedAssetPhysicalAssetParts];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetPartsDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetPartsDetailBook_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que falta por depreciar o el valor residual o Valor en Libros    La formula para obtener el valor residual deberia ser   Valor En Libros = (Valor Historico + Transacciones - Depreciaciones + ValorAjustadoPorInflacion)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor del Ajuste por Inflacion Acumulado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que se ha depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de las transacciones, es decir las valorizaciones que afectaron el valor en libros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado del activo, el cual debe se calcula y debe coincidir con la siguiente formula     Valor Hostorico + Valorizaciones - Desvalorizaciones', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas la desvalorizaciones que se le han realizado al activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas las valorizaciones que se le han realizado al activo en el libro correspondiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias que ya fueron depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias pendientes por depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias que se ha valorizado el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'ValorizationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia  se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida Util expresada en dias , con el que se registra el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Libro contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la Parte del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetPartsDetailBook', @level2type = N'COLUMN', @level2name = N'Id';

