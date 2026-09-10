CREATE TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryDevolutionId] INT             NOT NULL,
    [FixedAssetEntryItemId]       INT             NOT NULL,
    [FixedAssetEntryItemDetailId] INT             NOT NULL,
    [UnitValue]                   DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]               DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]               NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                    DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]          NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]               DECIMAL (18, 2) NOT NULL,
    [TotalValue]                  DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]               NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetEntryDevolutionDetail_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryDevolution] FOREIGN KEY ([FixedAssetEntryDevolutionId]) REFERENCES [FixedAsset].[FixedAssetEntryDevolution] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItem] FOREIGN KEY ([FixedAssetEntryItemId]) REFERENCES [FixedAsset].[FixedAssetEntryItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItemDetail] FOREIGN KEY ([FixedAssetEntryItemDetailId]) REFERENCES [FixedAsset].[FixedAssetEntryItemDetail] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la retención en la fuente (RTF) aplicado al artículo en devolución. Tipo: NUMERIC(18,2). Se calcula multiplicando SubTotalValue por RTFPercentage. Tributo, retención, impuesto retenido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en la fuente (RTF) aplicado al ítem devuelto. Tipo: NUMERIC(5,2). Valor por defecto: 0. Usado para cálculo de tributo, retención fiscal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe final del detalle de devolución: SubTotalValue - DiscountValue + IvaValue + RTFValue. Tipo: DECIMAL(18,2). Total neto facturación, monto total cobrable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe del descuento en pesos. Tipo: DECIMAL(18,2). Calculado como SubTotalValue × (DiscountPercentage/100). Rebaja, bonificación, deducción comercial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento comercial aplicado al subtotal. Tipo: NUMERIC(5,2). Rebaja porcentual, bonificación porcentual.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe del Impuesto al Valor Agregado (IVA). Tipo: DECIMAL(18,2). Calculado como SubTotalValue × (IvaPercentage/100). Impuesto, tributario, gravamen fiscal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del Impuesto al Valor Agregado (IVA) cobrable. Tipo: NUMERIC(5,2). Alícuota IVA, gravamen, impuesto porcentual.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal de la línea de devolución antes de descuentos e impuestos. Tipo: DECIMAL(18,2). Calculado como UnitValue × cantidad de unidades. Base imponible.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle de la devolución. Es la multiplicacion de la cantidad por el valor unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del artículo devuelto en pesos. Tipo: DECIMAL(18,2). Precio unitario, valor por unidad, costo unitario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor Unitario del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea detallada del activo fijo (FK a FixedAssetEntryItemDetail). Tipo: INT. Referencia al desglose específico del artículo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de elemento de entrada de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ítem de entrada de activo fijo (FK a FixedAssetEntryItem). Tipo: INT. Referencia al artículo principal en la devolución.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de elemento de entrada de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de la devolución (FK a FixedAssetEntryDevolution). Tipo: INT. Referencia al documento padre.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolucion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla. Tipo: INT IDENTITY(1,1). Clave primaria, secuencial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de entradas de activos fijos. Registra cada ítem devuelto con sus valores unitarios, subtotales, IVA, descuentos, retenciones y totales asociados a una devolución de compra o ingreso de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail';
