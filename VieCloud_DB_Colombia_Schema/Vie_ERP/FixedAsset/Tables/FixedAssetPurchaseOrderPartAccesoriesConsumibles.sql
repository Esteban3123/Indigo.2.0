CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] (
    [Id]                                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrderEquipmentDetail] INT          NOT NULL,
    [IdEquipment]                              INT          NOT NULL,
    [IdPartAccesoriesConsumibles]              INT          NOT NULL,
    [DepreciatePart]                           BIT          NULL,
    [Value]                                    NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderPartAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([IdPartAccesoriesConsumibles]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPurchaseOrderEquipment] FOREIGN KEY ([IdFixedAssetPurchaseOrderEquipmentDetail]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del accesorio, repuesto o consumible incluido en la orden de compra de activo fijo; monto en NUMERIC(18) para cálculo de depreciación y contabilización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que determina si el accesorio, repuesto o consumible se deprecia contablemente; afecta tratamiento fiscal y vida útil del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parte depreciada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del catálogo de partes, accesorios o consumibles asociados; enlaza con FixedAssetPartsAccesoriesConsumables para trazabilidad de componentes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Parte Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del equipo o activo fijo principal al cual pertenecen los accesorios, repuestos o consumibles en esta línea de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del detalle de equipo en la orden de compra de activo fijo; referencia a FixedAssetPurchaseOrderEquipmentDetail para vincular línea con documento padre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Detalle de equipo de orden de compra de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipmentDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la tabla; clave primaria para registro individual de accesorio, repuesto o consumible en orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partes, accesorios y consumibles asociados a una orden de compra de equipos de activos fijos. Registra qué componentes adicionales se adquieren junto con cada equipo, si se deprecian por separado y su valor unitario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles';
