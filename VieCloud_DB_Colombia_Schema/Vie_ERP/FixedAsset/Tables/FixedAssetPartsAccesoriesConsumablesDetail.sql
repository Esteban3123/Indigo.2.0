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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de salvamento/valor residual del bien (%). Requerido si DepreciationType=3 (Reducción de Saldos), se deja en cero para otros métodos. Numeric(5,2). Default=0. Rango 0-100%.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de unidades producidas del artículo (aplica solo si DepreciationType=4 Unidades Producción). Numeric(18,0). Default=0.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de depreciación aplicable: 1=Línea Recta (predeterminado), 2=Suma de Dígitos, 3=Reducción de Saldos, 4=Unidades de Producción. Numeric(TINYINT).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la vida útil: 1=Año, 2=Mes, 3=Día. Numeric(TINYINT).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Año,   2 - Mes,   3 - Dia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil del bien (duración). Expresada en años, meses o días según UnitLifeTime. Numeric(INT).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vida Util', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Libro Oficial (contable/legal) asociado al activo. Referencia (FK) a GeneralLedger.LegalBook. Numeric(INT).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Libro Oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte, accesorio o consumible vinculado. Referencia (FK) a FixedAssetPartsAccesoriesConsumables. Numeric(INT).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte o accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la línea de detalle de depreciación para partes, accesorios y consumibles (PK). Numeric(INT IDENTITY).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de depreciación y vida útil de las partes, accesorios y consumibles asociados a activos fijos. Registra los parámetros contables de cada componente según el libro legal correspondiente, incluyendo tipo de depreciación, unidades de producción totales y porcentaje de valor residual (salvamento).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumablesDetail';
