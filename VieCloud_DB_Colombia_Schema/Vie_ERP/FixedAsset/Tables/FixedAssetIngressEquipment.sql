CREATE TABLE [FixedAsset].[FixedAssetIngressEquipment] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngress] INT           NOT NULL,
    [IdInventoryType]     INT           NOT NULL,
    [IdEquipmentType]     INT           NOT NULL,
    [IdEquipment]         INT           NOT NULL,
    [IdIvaCode]           INT           NOT NULL,
    [Quantity]            INT           NOT NULL,
    [ProductValue]        NUMERIC (18)  NOT NULL,
    [TotalValue]          NUMERIC (18)  NOT NULL,
    [TaxesValue]          NUMERIC (18)  NOT NULL,
    [IdTrademark]         INT           NOT NULL,
    [Model]               VARCHAR (100) NOT NULL,
    [IdPolize]            INT           NOT NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipment] FOREIGN KEY ([IdEquipment]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetPoliza] FOREIGN KEY ([IdPolize]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetTrademark] FOREIGN KEY ([IdTrademark]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_GeneralLedgerIVA] FOREIGN KEY ([IdIvaCode]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la póliza de seguro vinculada al ingreso del equipo (FK a FixedAssetPolicy). Póliza, cobertura, aseguramiento de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o versión específica del equipo (VARCHAR 100). Descripción técnica, variante de fabricación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la marca fabricante del equipo (FK a FixedAssetTrademark). Fabricante, proveedor, laboratorio.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico en pesos del impuesto aplicado al equipo (NUMERIC 18). IVA adicional, retención, gravamen fiscal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Impuesto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total en pesos del equipo incluyendo producto e impuestos (NUMERIC 18). Costo final, precio neto, monto facturado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario o base en pesos del equipo sin impuestos (NUMERIC 18). Costo sin IVA, precio neto, valor comercial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del equipo ingresadas (INT). Número de equipos, stock, volumen de ingreso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código de IVA aplicable al equipo (FK a GeneralLedgerIVA). Alícuota, tarifa fiscal, régimen tributario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Código Iva', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del equipo específico en la tabla maestra (FK a FixedAssetItem). Activo fijo, bien inmueble, recurso tecnológico.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la clasificación o tipo de equipo (FK a FixedAssetItemType). Categoría, familia de activos, clase funcional.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de inventario al que pertenece el equipo (FK a FixedAssetInventoryType). Clasificación contable, grupo de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del movimiento o ingreso de activos fijos que contiene este equipo (FK a FixedAssetIngress). Documento de entrada, recepción, acta de ingreso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Ingreso del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerable (IDENTITY INT) de cada línea de equipo en el ingreso. Clave primaria, correlativo interno.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de equipos incluidos en un ingreso o compra de activos fijos. Registra cada equipo recibido con su tipo, cantidad, valores unitarios y totales, impuestos, marca, modelo y póliza asociada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment';
