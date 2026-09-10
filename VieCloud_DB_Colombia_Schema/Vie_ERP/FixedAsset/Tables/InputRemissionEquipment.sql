CREATE TABLE [FixedAsset].[InputRemissionEquipment] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemission]      INT           NOT NULL,
    [RemissionSource]       TINYINT       NOT NULL,
    [SourceCode]            VARCHAR (20)  NULL,
    [PurchaseOrderDetailId] INT           NULL,
    [IdInventoryType]       INT           NOT NULL,
    [IdEquipmentType]       INT           NOT NULL,
    [IdEquipment]           INT           NOT NULL,
    [IdIvaCode]             INT           NOT NULL,
    [Quantity]              INT           NOT NULL,
    [ProductValue]          NUMERIC (18)  NOT NULL,
    [TotalValue]            NUMERIC (18)  NOT NULL,
    [TaxesValue]            NUMERIC (18)  NOT NULL,
    [IdTrademark]           INT           NOT NULL,
    [Model]                 VARCHAR (100) NOT NULL,
    [IdPolize]              INT           NOT NULL,
    CONSTRAINT [PK_InputRemissionEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InputRemissionEquipment_InventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de póliza de seguro o cobertura del equipo médico/administrativo en remisión de entrada (FK, INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id polize', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o versión del equipo, marca comercial y descripción técnica (VARCHAR 100, búsqueda: modelo equipo médico)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de marca fabricante del equipo en inventario fijo (FK, INT, búsqueda: fabricante, marca comercial)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de impuestos aplicados (IVA u otros), NUMERIC(18,2), monto fiscal del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de impuesto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del equipo incluyendo producto e impuestos (NUMERIC 18, monto final facturado)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario o base del producto sin impuestos (NUMERIC 18, precio neto del equipo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades de equipo recibidas en la remisión de entrada (INT, número de artículos)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código o tarifa de IVA aplicada al equipo (FK, INT, búsqueda: impuesto IVA, tarifa fiscal)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Código Iva', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del equipo específico en registro de activos fijos (FK, INT, búsqueda: equipo, activo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de categoría/tipo de equipo (médico, administrativo, etc.) (FK, INT, búsqueda: clase equipo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de inventario (FK a FixedAssetInventoryType, INT, búsqueda: tipo inventario)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del renglón/línea de orden de compra origen (FK, INT NULL, búsqueda: OC, compra)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento fuente importado (referencia: factura, OC, remisión original) (VARCHAR 20 NULL)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el codigo de del documento el cua fue importado el item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la remisión: 1=Sin origen, 2=Orden de compra (TINYINT, búsqueda: fuente remisión, procedencia)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la remisión de entrada padre, agrupa equipos recibidos (FK, INT, búsqueda: entrada inventario)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInputRemission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id remisión de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInputRemission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInputRemission';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro equipo en remisión de entrada (PK, INT IDENTITY, búsqueda: remisión equipo entrada)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del equipo de remisión de entrada ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de equipos incluidos en una remisión de entrada de activos fijos. Registra cada equipo recibido en una remisión de ingreso, con su tipo, cantidad, valores de compra, impuestos, marca, modelo y póliza de seguro asociada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment';
