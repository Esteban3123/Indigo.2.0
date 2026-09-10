CREATE TABLE [MixingStation].[NPTConfiguration] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialWeight] DECIMAL (18, 2) NOT NULL,
    [EndWeight]     DECIMAL (18, 2) NOT NULL,
    [ProductId]     INT             NOT NULL,
    CONSTRAINT [PK_NPTConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NPTConfiguration_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto (FK a Inventory.InventoryProduct) asociado a la configuración NPT, referencia al insumo, medicamento o componente farmacéutico usado en nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso final en kilogramos o gramos (DECIMAL 18,2) del producto en la configuración NPT, punto de culminación o límite máximo de la mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'EndWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Peso final', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'EndWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'EndWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso inicial en kilogramos o gramos (DECIMAL 18,2) del producto en la configuración de nutrición parenteral total (NPT), punto de partida de la mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'InitialWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Peso inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'InitialWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'InitialWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la configuración NPT, consecutivo autoincrementable de la tabla MixingStation.NPTConfiguration', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rangos de peso inicial y final para la configuración de nutrición parenteral total (NPT) en la estación de mezclas, asociados a un producto específico. Permite definir los parámetros de dosificación según el peso del paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration';
