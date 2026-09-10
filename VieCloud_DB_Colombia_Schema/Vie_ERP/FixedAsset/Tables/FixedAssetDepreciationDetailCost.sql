CREATE TABLE [FixedAsset].[FixedAssetDepreciationDetailCost] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetDepreciationDetailId] INT             NOT NULL,
    [MainAccountId]                  INT             NOT NULL,
    [ResponsibleId]                  INT             NOT NULL,
    [LocationId]                     INT             NOT NULL,
    [CostCenterId]                   INT             NOT NULL,
    [DepreciatedDays]                INT             NOT NULL,
    [DepreciationValue]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetDepreciationDetailCost__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetDepreciationDetailCost_FixedAssetDepreciationDetail] FOREIGN KEY ([FixedAssetDepreciationDetailId]) REFERENCES [FixedAsset].[FixedAssetDepreciationDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetDepreciationDetailCost_FixedAssetDepreciationDetail];


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetailCost__FixedAssetDepreciationDetailId__INC__Id]
    ON [FixedAsset].[FixedAssetDepreciationDetailCost]([FixedAssetDepreciationDetailId] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetailCost__LocationId__INC__FixedAssetDepreciationDetailId]
    ON [FixedAsset].[FixedAssetDepreciationDetailCost]([LocationId] ASC)
    INCLUDE([FixedAssetDepreciationDetailId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) de la depreciación del mes imputada al centro de costos. Monto en moneda de transacción para registros contables.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la depreciacion del mes en el Centro de costo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días depreciados en el período. Determina la porción de depreciación mensual aplicable según el tiempo de uso del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de dias depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos al que se imputa la depreciación del mes. Fundamental para asignación contable y análisis financiero por área.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de costos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la localización o sede física donde se encuentra el activo fijo. Referencia a ubicación geográfica o instalación del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable (persona, profesional o centro) del activo fijo. Trazabilidad de quién gestiona o es custodio del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable del activo fijo. Referencia a la estructura de cuentas de mayor para registro de depreciación acumulada o gasto.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de depreciación/amortización de activo fijo. Referencia FK a FixedAssetDepreciationDetail. Vincula el costo a su detalle padre de depreciación mensual.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de amortización de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la tabla de detalle de costo de depreciación de activos fijos (IDENTITY). Clave primaria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de costos de depreciación de activos fijos: registra el valor depreciado por cada combinación de cuenta contable, responsable, ubicación y centro de costo para un período de depreciación específico, permitiendo la distribución contable del gasto por depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetailCost';
