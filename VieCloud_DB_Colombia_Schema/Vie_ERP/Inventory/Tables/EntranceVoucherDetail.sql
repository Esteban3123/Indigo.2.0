CREATE TABLE [Inventory].[EntranceVoucherDetail] (
    [Id]                                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherId]                                INT             NOT NULL,
    [ProductId]                                        INT             NOT NULL,
    [Quantity]                                         INT             NOT NULL,
    [EntranceSource]                                   TINYINT         NOT NULL,
    [SourceCode]                                       VARCHAR (20)    NULL,
    [PurchaseOrderDetailId]                            INT             NULL,
    [ContractDetailId]                                 INT             NULL,
    [RemissionEntranceDetailBatchSerialId]             INT             NULL,
    [UnitValue]                                        DECIMAL (18, 2) NOT NULL,
    [LastValue]                                        DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]                                    DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]                                    NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                                         DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]                               NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]                                    DECIMAL (18, 2) NOT NULL,
    [TotalValue]                                       DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]                                    NUMERIC (5, 2)  CONSTRAINT [DF_EntranceVoucherDetail_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                                         DECIMAL (18, 2) NOT NULL,
    [ConsignmentInventoryRemissionDetailBatchSerialId] INT             NULL,
    [NetoValue]                                        DECIMAL (18, 2) CONSTRAINT [DF__EntranceV__NetoV__49810403] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDetail_ConsignmentInventoryRemissionDetailBatchSerial] FOREIGN KEY ([ConsignmentInventoryRemissionDetailBatchSerialId]) REFERENCES [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDetail_EntranceVoucher] FOREIGN KEY ([EntranceVoucherId]) REFERENCES [Inventory].[EntranceVoucher] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDetail_InventoryContractDetail] FOREIGN KEY ([ContractDetailId]) REFERENCES [Inventory].[InventoryContractDetail] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDetail_PurchaseOrderDetail] FOREIGN KEY ([PurchaseOrderDetailId]) REFERENCES [Inventory].[PurchaseOrderDetail] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDetail_RemissionEntranceDetailBatchSerial] FOREIGN KEY ([RemissionEntranceDetailBatchSerialId]) REFERENCES [Inventory].[RemissionEntranceDetailBatchSerial] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto del item (DECIMAL 18,2). Monto final después de aplicar descuentos, IVA y retención en la fuente. Cálculo: SubTotalValue - DiscountValue + IvaValue - RTFValue.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'NetoValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor neto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'NetoValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'NetoValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) al detalle de remisión de inventario en consignación. Vincula el lote/serial del envío consignado. FK → Inventory.ConsignmentInventoryRemissionDetailBatchSerial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision de inventario en consignacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos (DECIMAL 18,2) de retención en la fuente (RTF) a cobrar sobre el item. Se calcula aplicando RTFPercentage al SubTotalValue.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en la fuente (NUMERIC 5,2). Tasa aplicada al subtotal del item para calcular RTFValue. Defecto 0%.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del registro (DECIMAL 18,2). Resultado: SubTotalValue - DiscountValue + IvaValue. Monto facturado antes de retenciones adicionales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del descuento (DECIMAL 18,2) aplicado al item. Se obtiene multiplicando SubTotalValue × DiscountPercentage.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento (NUMERIC 5,2) aplicado al subtotal del item. Comercial, por volumen o negociado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del impuesto al valor agregado (DECIMAL 18,2) del producto. Calculado como SubTotalValue × IvaPercentage.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA (NUMERIC 5,2) a cobrar sobre el producto. Típicamente 0%, 5%, 8%, 16%, 19% según legislación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del detalle (DECIMAL 18,2). Multiplicación de Quantity × UnitValue. Base antes de descuentos e impuestos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último costo del producto (DECIMAL 18,2) registrado en el sistema. Calculado con método de promedio ponderado. Histórico de referencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'LastValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del ultimo costo del producto, esto se calcula con el metodo de promedio ponderado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'LastValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'LastValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario (DECIMAL 18,2) del producto en este comprobante de entrada. Precio acordado por unidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) al detalle de remisión de entrada (lote/serial). Rastreo de origen del producto. FK → Inventory.RemissionEntranceDetailBatchSerial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) al detalle del contrato de compra (item contratado). FK → Inventory.InventoryContractDetail.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) al detalle de la orden de compra. Vínculo con OC que originó la entrada. FK → Inventory.PurchaseOrderDetail.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento fuente (VARCHAR 20) que generó la entrada. Ej: número de OC, contrato, remisión. Trazabilidad documental.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el codigo del documento que genero la entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la entrada (TINYINT): 1=Ninguno, 2=Orden de Compra, 3=Contrato Fijo, 4=Remisión de Entrada, 5=Remisión Consignación. Clasificación de procedencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el origen de la entrada  1 - Ninguna  2 - Orden de compra  3 - Contrato (Fijo)  4 - Remision de entrada  5 - Remision de inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades (INT) del producto ingresado en este detalle. Debe coincidir con documento fuente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) al producto. Identificador único del bien/insumo/medicamento ingresado. FK → Inventory.InventoryProduct.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) a la cabecera del comprobante de entrada. Agrupa todos los detalles del recibo. FK → Inventory.EntranceVoucher.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle del comprobante de entrada. Primary Key. Clave técnica de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los comprobantes de entrada al inventario. Registra cada ítem (producto, cantidad, valores, impuestos y descuentos) que compone un comprobante de ingreso de mercancía al almacén, ya sea por orden de compra, contrato, remisión o consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail';
