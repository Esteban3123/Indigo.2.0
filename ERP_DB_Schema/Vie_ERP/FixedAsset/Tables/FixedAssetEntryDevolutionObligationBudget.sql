CREATE TABLE [FixedAsset].[FixedAssetEntryDevolutionObligationBudget] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryDevolutionId] INT             NOT NULL,
    [ObligationDetailId]          INT             NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryDevolutionObligationBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_FixedAssetEntryDevolution] FOREIGN KEY ([FixedAssetEntryDevolutionId]) REFERENCES [FixedAsset].[FixedAssetEntryDevolution] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionObligationBudget] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_FixedAssetEntryDevolution];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionObligationBudget] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionObligationBudget_ObligationDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con el cual se modificará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';

