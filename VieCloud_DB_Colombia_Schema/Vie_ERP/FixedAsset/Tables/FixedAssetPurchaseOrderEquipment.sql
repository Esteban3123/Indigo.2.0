CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipment] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrder] INT           NOT NULL,
    [IdInventoryType]           INT           NOT NULL,
    [IdEquipmentType]           INT           NOT NULL,
    [IdEquipment]               INT           NOT NULL,
    [IdIvaCode]                 INT           NOT NULL,
    [Quantity]                  INT           NOT NULL,
    [OutstandingQuantity]       INT           NOT NULL,
    [ProductValue]              NUMERIC (18)  NOT NULL,
    [TotalValue]                NUMERIC (18)  NOT NULL,
    [TaxesValue]                NUMERIC (18)  NOT NULL,
    [IdTrademark]               INT           NOT NULL,
    [Model]                     VARCHAR (100) NOT NULL,
    [IdPolize]                  INT           NOT NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de póliza de seguro, garantía o cobertura asociada al equipo adquirido en activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Polize', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o versión específica (VARCHAR 100) del equipo; identificación técnica del bien adquirido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la marca o fabricante del equipo (proveedor de marca); filtro para búsquedas de proveedores.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la marca ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18) de impuestos (IVA u otros) aplicados a la línea de equipo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de impuestos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total línea (NUMERIC 18) = cantidad × valor unitario; subtotal sin impuestos de la línea.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valot total', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario (NUMERIC 18) del producto o equipo sin impuestos; precio base neto.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades (INT) pendiente por recibir, entregar o facturar del equipo adquirido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad pendiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades (INT) del equipo solicitadas en la orden de compra de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea hacia código de IVA aplicable; porcentaje impositivo asociado al producto (impuesto al valor agregado).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Iva Code', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del equipo específico, unidad o activo fijo individual dentro de la orden de compra.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo o categoría de equipo médico, hospitalario, administrativo u operativo a adquirir.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tipo de inventario (FixedAsset.FixedAssetInventoryType); clasificación contable del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Tipo de inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia orden de compra de activos fijos; referencia el documento maestro de adquisición.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Orden de compra de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la línea de equipo en orden de compra de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de detalle de equipos incluidos en órdenes de compra de activos fijos. Registra cada equipo solicitado en una orden de compra, con cantidades, valores, impuestos, marca, modelo y póliza asociada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment';
