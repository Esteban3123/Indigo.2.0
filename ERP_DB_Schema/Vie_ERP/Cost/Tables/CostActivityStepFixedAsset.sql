CREATE TABLE [Cost].[CostActivityStepFixedAsset] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [FixedAssetItemId]   INT             NOT NULL,
    [Hours]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepFixedAsset__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepFixedAsset_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepFixedAsset_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStepFixedAsset] NOCHECK CONSTRAINT [FK_CostActivityStepFixedAsset_CostActivityStep];


GO
ALTER TABLE [Cost].[CostActivityStepFixedAsset] NOCHECK CONSTRAINT [FK_CostActivityStepFixedAsset_FixedAssetItem];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStepFixedAsset__CostActivityStepId__FixedAssetItemId]
    ON [Cost].[CostActivityStepFixedAsset]([CostActivityStepId] ASC, [FixedAssetItemId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas requeridas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Artículo de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';

