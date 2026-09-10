CREATE TABLE [Inventory].[ConsignmentInventoryRemissionDetail] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsignmentInventoryRemissionId]       INT             NOT NULL,
    [RemissionSource]                       TINYINT         NOT NULL,
    [SourceCode]                            VARCHAR (20)    NULL,
    [PurchaseOrderDetailId]                 INT             NULL,
    [ContractDetailId]                      INT             NULL,
    [ProductId]                             INT             NOT NULL,
    [ConsignmentInventoryRemissionDetailId] INT             NULL,
    [Quantity]                              INT             NOT NULL,
    [UnitValue]                             DECIMAL (18, 2) NOT NULL,
    [LastValue]                             DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]                         DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]                         NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                              DECIMAL (18, 2) NOT NULL,
    [TotalValue]                            DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_ConsignmentInventoryRemissionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_ConsignmentInventoryRemission] FOREIGN KEY ([ConsignmentInventoryRemissionId]) REFERENCES [Inventory].[ConsignmentInventoryRemission] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_ConsignmentInventoryRemissionDetail] FOREIGN KEY ([ConsignmentInventoryRemissionDetailId]) REFERENCES [Inventory].[ConsignmentInventoryRemissionDetail] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_InventoryContract] FOREIGN KEY ([ContractDetailId]) REFERENCES [Inventory].[InventoryContractDetail] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_PurchaseOrder] FOREIGN KEY ([PurchaseOrderDetailId]) REFERENCES [Inventory].[PurchaseOrderDetail] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [UX_ConsignmentInventoryRemissionDetail_ConsignmentInventoryRemissionId]
    ON [Inventory].[ConsignmentInventoryRemissionDetail]([ConsignmentInventoryRemissionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ConsignmentInventoryRemissionDetail_Remission_Product]
    ON [Inventory].[ConsignmentInventoryRemissionDetail]([ConsignmentInventoryRemissionId] ASC, [ProductId] ASC)
    INCLUDE([UnitValue], [IvaPercentage]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del detalle (SubTotalValue - Descuento + IvaValue). DECIMAL(18,2). Monto final a facturar o registrar en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA calculado sobre el producto. DECIMAL(18,2). Impuesto al valor agregado aplicado al detalle.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA aplicable al producto. NUMERIC(5,2). Alícuota fiscal a cobrar (ej: 5%, 19%).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del detalle: Cantidad × Valor unitario. DECIMAL(18,2). Base antes de impuestos y descuentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del último costo del producto calculado por promedio ponderado. DECIMAL(18,2). Costo histórico para valorización de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'LastValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del ultimo costo del producto, esto se calcula con el metodo de promedio ponderado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'LastValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'LastValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del producto en la remisión. DECIMAL(18,2). Precio por unidad del artículo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto que ingresa en consignación. INT. Número de ítems a recepcionar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto que va ingresar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK autoreferencial a detalle anterior en caso de reposición. INT. ID del detalle de remisión original si es movimiento de reposición.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la Remision de Inventario en Consignación, solo se solicita si el tipo de movimiento es de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a InventoryProduct. INT. Identificador único del producto/artículo que ingresa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a InventoryContractDetail. INT. Requerido si origen es contrato de suministro de inventarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato de inventarios, solo se solicita si el origen es por contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a PurchaseOrderDetail. INT. Requerido si origen es orden de compra (OC).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de compra, solo se solicita si el origen de la remision es por orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento origen (OC, contrato, etc). VARCHAR(20). Referencia al número de documento generador.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el codigo del documento que genero la entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la remisión: 1=Ninguno, 2=Orden de Compra, 3=Contrato. TINYINT. Tipo de fuente del movimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3 - Contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a cabecera de remisión (ConsignmentInventoryRemission). INT. Identificador del movimiento padre.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria del detalle de remisión en consignación. INT IDENTITY. Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision del inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos incluidos en cada remisión de inventario en consignación. Registra las líneas de artículos con cantidades, valores unitarios, subtotales, IVA y totales por cada producto remisionado, vinculando la remisión con órdenes de compra o contratos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail';
