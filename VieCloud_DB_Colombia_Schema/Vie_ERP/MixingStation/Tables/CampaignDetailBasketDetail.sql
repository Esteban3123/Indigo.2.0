CREATE TABLE [MixingStation].[CampaignDetailBasketDetail] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]  INT             NOT NULL,
    [ComponentType]     TINYINT         NOT NULL,
    [ProductId]         INT             NULL,
    [AtcId]             INT             NULL,
    [SupplieId]         INT             NULL,
    [Quantity]          DECIMAL (18, 6) NOT NULL,
    [MeasurementUnitId] INT             NULL,
    CONSTRAINT [PK_CampaignDetailBasketDeatail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de componente del detalle de canasta de campaña (TINYINT): 1=Medicamento/fármaco, 2=Insumo/suministro médico, 3=Producto otro. Clasifica el tipo de artículo inventariable incluido en la campaña de promoción o distribución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de componente:   1: Medicamento  2: Insumo  3: Producto de tipo otro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los componentes (medicamentos, insumos o principios activos ATC) que conforman la canasta o fórmula de mezcla de cada ítem de una campaña de preparación en la estación de mezclas. Registra qué ingredientes y en qué cantidad se usan por cada línea de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de componente en la canasta de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al ítem o línea de campaña de mezcla al que pertenece este componente; vincula con el detalle de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico (medicamento) que se usa como componente en la mezcla; puede ser nulo si el componente se expresa como ATC o insumo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ATC (clasificación anatómica, terapéutica y química) del principio activo utilizado como componente; aplica cuando no se especifica un producto concreto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del insumo o material de soporte (suero, diluyente, material de empaque) que forma parte de la canasta de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del componente requerida para la preparación de la mezcla, expresada con hasta seis decimales para alta precisión farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la cantidad del componente (por ejemplo: ml, mg, UI, gramos); referencia al catálogo de unidades de medida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
