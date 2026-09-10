CREATE TABLE [MixingStation].[CampaignDetailItems] (
    [Id]                                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]                        INT           NOT NULL,
    [AtcId]                                   INT           NULL,
    [SupplyId]                                INT           NULL,
    [ProductId]                               INT           NULL,
    [RequestQuantity]                         INT           NOT NULL,
    [QuantityStock]                           INT           NOT NULL,
    [QuantityWarehouse]                       INT           NOT NULL,
    [QuantityMaquila]                         INT           CONSTRAINT [DF_CampaignDetailItems_QuantityMaquila] DEFAULT ((0)) NOT NULL,
    [ItemType]                                TINYINT       CONSTRAINT [DF_CampaignDetailItems_ItemType] DEFAULT ((3)) NOT NULL,
    [QuantityRemnant]                         INT           CONSTRAINT [DF__CampaignD__Remna__00B07205] DEFAULT ((0)) NOT NULL,
    [CauseReprocessingRejectionId]            INT           NULL,
    [CauseReprocessingRejectionJustification] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CampaignDetailItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailItems_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_CauseReprocessingRejection] FOREIGN KEY ([CauseReprocessingRejectionId]) REFERENCES [MixingStation].[CauseReprocessingRejection] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_InventorySupplie] FOREIGN KEY ([SupplyId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de remanentes o sobrantes (INT, default 0). Registro de unidades no utilizadas, residuales o excedentes tras procesamiento de campaña en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityRemnant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Remanentes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityRemnant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityRemnant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ítem de campaña (TINYINT, default 3): 1=Principal, 2=Canasta, 3=Otro, 4=Solicitudes Manuales. Clasificación del producto, suministro o ATC en el detalle de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo:  1 - Pricipal  2 - Canasta  3 - Otro  4 - Solicitudes Manuales  ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ítems o líneas de detalle de cada campaña de preparación en la estación de mezclas (MixingStation). Registra los medicamentos, insumos o productos solicitados en cada campaña, con sus cantidades disponibles por origen (stock, bodega, maquila) y los motivos de rechazo o reprocesamiento cuando aplica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del ítem de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de campaña al que pertenece este ítem; vincula el ítem con su campaña de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ATC (clasificación anatómica, terapéutica y química) del medicamento asociado al ítem.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo o material relacionado con el ítem de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'SupplyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'SupplyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto farmacéutico o preparado asociado al ítem.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada o requerida del ítem para la campaña de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'RequestQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'RequestQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad disponible en stock (inventario en planta) para cubrir el ítem.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad disponible en bodega o almacén para cubrir el ítem.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad a obtener mediante maquila (producción o preparación por terceros) para cubrir el ítem.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityMaquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityMaquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al catálogo de causas de rechazo o reprocesamiento; indica por qué el ítem fue rechazado o requiere reproceso.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o descripción libre del motivo de rechazo o reprocesamiento del ítem de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionJustification';
