CREATE TABLE [FixedAsset].[FixedAssetEntryItemDetailPartBook] (
    [Id]                              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryItemDetailPartId] INT            NULL,
    [LegalBookId]                     INT            NOT NULL,
    [LifeTime]                        INT            NOT NULL,
    [UnitLifeTime]                    TINYINT        NOT NULL,
    [DepreciationType]                TINYINT        CONSTRAINT [DF_FixedAssetEntryItemDetailPartBook_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]             NUMERIC (18)   CONSTRAINT [DF_FixedAssetEntryItemDetailPartBook_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]                NUMERIC (5, 2) CONSTRAINT [DF_FixedAssetEntryItemDetailPartBook_PercentageRescue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryItemDetailPartBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryItemDetailPartBook_FixedAssetEntryItemDetailPart] FOREIGN KEY ([FixedAssetEntryItemDetailPartId]) REFERENCES [FixedAsset].[FixedAssetEntryItemDetailPart] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItemDetailPartBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de valor de salvamento/residual (NUMERIC 5,2), requerido solo si DepreciationType=3 (Reducción de Saldos), de lo contrario=0', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de unidades producidas del artículo (NUMERIC 18,0), aplicable solo si DepreciationType=4 (Unidades de Producción), en caso contrario=0', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de depreciación del activo (TINYINT): 1=Línea Recta, 2=Suma de Dígitos, 3=Reducción de Saldos, 4=Unidades de Producción', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la vida útil: 1=Año, 2=Mes, 3=Día (TINYINT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Año,   2 - Mes,   3 - Dia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil del activo fijo, valor numérico en unidades según UnitLifeTime (años, meses o días)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vida Util', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el Id del Libro Oficial/Contable en GeneralLedger.LegalBook para registro contable del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Libro Oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el Id del detalle componente o parte del activo fijo en FixedAssetEntryItemDetailPart', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailPartId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la parte del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailPartId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailPartId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la tabla FixedAssetEntryItemDetailPartBook', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración contable y de depreciación por libro legal para cada componente (parte) de un activo fijo registrado. Define el método de depreciación, la vida útil y el valor de rescate que aplican según el libro contable (por ejemplo, fiscal o NIIF).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetailPartBook';
