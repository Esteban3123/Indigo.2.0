CREATE TABLE [Inventory].[InventoryContractDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryContractId] INT             NOT NULL,
    [ProductId]           INT             NOT NULL,
    [Quantity]            INT             NOT NULL,
    [OutstandingQuantity] INT             NOT NULL,
    [CancelledQuantity]   INT             NOT NULL,
    [Value]               DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]       DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]       NUMERIC (5, 2)  NOT NULL,
    [IvaValue]            DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]  NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]       DECIMAL (18, 2) NOT NULL,
    [TotalValue]          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_InventoryContractDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContractDetail_InventoryContract] FOREIGN KEY ([InventoryContractId]) REFERENCES [Inventory].[InventoryContract] ([Id]),
    CONSTRAINT [FK_InventoryContractDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del detalle de contrato (DECIMAL 18,2). Cálculo: SubTotalValue - DiscountValue + IvaValue. Monto final a facturar/pagar del producto en el contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del descuento aplicado (DECIMAL 18,2). Se calcula multiplicando SubTotalValue × DiscountPercentage. Rebaja otorgada al producto en el contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento aplicado al producto (NUMERIC 5,2). Tanto por ciento de reducción sobre el subtotal del detalle del contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del IVA calculado (DECIMAL 18,2). Se obtiene multiplicando SubTotalValue × IvaPercentage. Impuesto al valor agregado del producto en el contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA/impuesto a cobrar (NUMERIC 5,2). Alícuota tributaria aplicada sobre el subtotal del producto en el contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del detalle del contrato (DECIMAL 18,2). Resultado de Quantity × Value. Base para cálculo de descuentos e impuestos del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del producto (DECIMAL 18,2). Precio por unidad del artículo en el contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades canceladas/anuladas (INT). Unidades del producto que fueron descartadas o eliminadas del contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Cancelada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por entregar (INT). Unidades del producto que aún no han sido recibidas/entregadas en el contrato de inventarios. Equivalente a cantidad en tránsito.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente por entregar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto (INT). Número de artículos solicitados en el detalle del contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto (INT, FK). Referencia única al artículo/bien en el catálogo de inventarios del contrato. Clave foránea a InventoryProduct.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato de inventarios padre (INT, FK). Referencia única al encabezado del contrato de compra/suministro. Clave foránea a InventoryContract.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'InventoryContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del contrato de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'InventoryContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'InventoryContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del contrato (INT, PK IDENTITY). Clave primaria del registro de línea individual en el contrato de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos incluidos en un contrato de inventario/compras. Registra las cantidades pactadas, pendientes y canceladas de cada ítem, junto con precios unitarios, IVA, descuentos y valor total por línea de contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractDetail';
