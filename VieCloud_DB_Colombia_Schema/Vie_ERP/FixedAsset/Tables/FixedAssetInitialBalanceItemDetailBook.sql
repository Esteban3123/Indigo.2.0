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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico del activo fijo por libro contable; costo original de adquisición registrado en la contabilidad. DECIMAL(18,2), predeterminado 0.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor Historico del activo por Libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual pendiente de depreciar de las partes componentes del activo; monto que aún falta amortizar. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor residual de las partes, es decir el valor que aun falta por depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual o de salvamento; importe que falta por depreciar del activo fijo. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que falta por depreciar o el valor residual', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe ya depreciado de las partes componentes del activo; porción de costo amortizado. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de las partes que se han depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor depreciado acumulado del activo fijo; monto total de depreciación registrada. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que se ha depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de salvamento o rescate (0-100); aplicable solo en método de reducción de saldos, se registra en cero para otros métodos. NUMERIC(5,2), predeterminado 0.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de unidades producidas del artículo; solo utilizado cuando el método de depreciación es por unidades de producción. NUMERIC(18), predeterminado 0.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de depreciación del activo: 1=Línea Recta, 2=Suma de Dígitos, 3=Reducción de Saldos, 4=Unidades de Producción. TINYINT, predeterminado 1.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días ya depreciados del activo fijo; período de vida útil consumido. INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias que ya fueron depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días pendientes por depreciar; diferencia entre vida útil total y días ya depreciados. INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias pendientes por depreciar, este campo se calcula restando la vida util expresada en años menos los dias depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la vida útil: 1=Año, 2=Mes, 3=Día; según lo digitado por el usuario. TINYINT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Año,   2 - Mes,   3 - Dia    se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil del activo expresada en días; período durante el cual se deprecia el bien. INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vida Util expresada en dias ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro contable (FK a GeneralLedger.LegalBook); registro contable donde se refleja la depreciación. INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Libro Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo de saldo inicial de activo fijo (FK a FixedAssetInitialBalanceItem); referencia al bien depreciable. INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de artículo de saldo inicial de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de autoincremento de la tabla; clave primaria del registro de depreciación por libro. INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle contable por libro legal de cada activo fijo incluido en el saldo inicial. Registra la vida útil, días depreciados, valores de depreciación acumulada, valor residual y valor histórico de cada activo según el libro contable (legal, NIIF, fiscal, etc.) al momento del ingreso del saldo inicial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemDetailBook';
