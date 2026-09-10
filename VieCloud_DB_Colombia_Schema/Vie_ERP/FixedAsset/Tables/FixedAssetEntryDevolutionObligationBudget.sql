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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor decimal (hasta 18 dígitos, 2 decimales) que modifica la obligación presupuestal en el detalle seleccionado; monto ajustado en devolutivo de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se modificará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de obligación presupuestal vinculado; referencia a Budget.ObligationDetail para tracking de obligaciones contables', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera/encabezado de devolución de activo fijo; referencia a FixedAsset.FixedAssetEntryDevolution que agrupa la transacción', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria, IDENTITY) del registro de vínculo entre devolución y obligación presupuestal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la relación entre devoluciones de ingresos de activos fijos y las obligaciones presupuestales asociadas, indicando el valor comprometido de cada obligación para una devolución específica.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionObligationBudget';
