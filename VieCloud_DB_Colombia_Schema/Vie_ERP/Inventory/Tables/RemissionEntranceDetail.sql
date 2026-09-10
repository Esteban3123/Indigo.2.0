CREATE TABLE [Inventory].[RemissionEntranceDetail] (
    [Id]                                               INT             IDENTITY (1, 1) NOT NULL,
    [RemissionEntranceId]                              INT             NOT NULL,
    [RemissionSource]                                  TINYINT         NOT NULL,
    [SourceCode]                                       VARCHAR (20)    NULL,
    [PurchaseOrderDetailId]                            INT             NULL,
    [ContractDetailId]                                 INT             NULL,
    [ProductId]                                        INT             NOT NULL,
    [Quantity]                                         INT             NOT NULL,
    [UnitValue]                                        DECIMAL (18, 2) NOT NULL,
    [LastValue]                                        DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]                                    DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]                                    NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                                         DECIMAL (18, 2) NOT NULL,
    [TotalValue]                                       DECIMAL (18, 2) NOT NULL,
    [ConsignmentInventoryRemissionDetailBatchSerialId] INT             NULL,
    [GrossUnitValue]                                   DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]                               NUMERIC (5, 2)  NOT NULL,
    [NetDiscount]                                      DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_RemissionEntranceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionEntranceDetail_InventoryContract] FOREIGN KEY ([ContractDetailId]) REFERENCES [Inventory].[InventoryContractDetail] ([Id]),
    CONSTRAINT [FK_RemissionEntranceDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_RemissionEntranceDetail_PurchaseOrder] FOREIGN KEY ([PurchaseOrderDetailId]) REFERENCES [Inventory].[PurchaseOrderDetail] ([Id]),
    CONSTRAINT [FK_RemissionEntranceDetail_RemissionEntrance] FOREIGN KEY ([RemissionEntranceId]) REFERENCES [Inventory].[RemissionEntrance] ([Id])
);




GO



GO



GO



GO



GO
CREATE TRIGGER [Inventory].[tgg_ValidateBeforeDeleteRemissionEntranceDetailIfTheRemissionEntranceIsNotConfirmed]
   ON  [Inventory].[RemissionEntranceDetail] 
   AFTER DELETE
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from<br>
    -- interfering with SELECT statements.<br>
    SET NOCOUNT ON;

	IF EXISTS (
		SELECT re.Id
		FROM Inventory.RemissionEntrance re
		JOIN DELETED red ON re.Id = red.RemissionEntranceId
		WHERE re.Status = 2
	)
	BEGIN
		THROW 51000, 'Error generado por control de eliminacion desde trigger', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento neto aplicado (DECIMAL 18,2). Valor en moneda que reduce el subtotal tras aplicar el porcentaje de descuento al producto ingresado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'NetDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento neto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'NetDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'NetDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento (NUMERIC 5,2). Tasa porcentual aplicada al valor unitario bruto para calcular el descuento neto en la entrada de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario bruto (DECIMAL 18,2). Precio unitario del producto sin descuentos, antes de restar el porcentaje de descuento aplicable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'GrossUnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario bruto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'GrossUnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'GrossUnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote/serie en consignación (INT, FK). Referencia al detalle de lote/serie del producto en consignación asociado a este renglón de remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote del detalle de la Remision de Inventario en Consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del renglón (DECIMAL 18,2). Valor final calculado como: SubTotalValue - NetDiscount + IvaValue. Incluye impuesto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA (DECIMAL 18,2). Monto en moneda del impuesto al valor agregado aplicado al producto según el porcentaje configurado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del IVA (NUMERIC 5,2). Tasa impositiva sobre el producto (ej: 19%). Determina el monto de impuesto a cobrar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del detalle (DECIMAL 18,2). Resultado de multiplicar Quantity × UnitValue. Base antes de descuentos e impuesto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del último costo (DECIMAL 18,2). Costo unitario promedio ponderado del producto calculado en anteriores ingresos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'LastValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del ultimo costo del producto, esto se calcula con el metodo de promedio ponderado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'LastValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'LastValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del producto (DECIMAL 18,2). Precio unitario vigente en esta entrada de inventario/remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del producto (INT). Número de unidades del producto que ingresa en esta remisión de entrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto que va ingresar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto (INT, FK). Referencia al producto de inventario que está siendo recibido en esta entrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del contrato de inventarios (INT, FK, nullable). Requerido si RemissionSource = 3 (Contrato). Vincula al contrato de suministro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato de inventarios, solo se solicita si el origen es por contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de compra (INT, FK, nullable). Requerido si RemissionSource = 2 (OC). Referencia al renglón de la orden de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de compra, solo se solicita si el origen de la remision es por orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código fuente del documento (VARCHAR 20, nullable). Código o número del documento origen (OC, contrato, etc.) que generó esta entrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el codigo del documento que genero la entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la remisión (TINYINT). Tipo de fuente: 1=Ninguna, 2=Orden de compra, 3=Contrato. Determina qué FK aplica (PurchaseOrderDetailId o ContractDetailId).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3  - Contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de remisión (INT, FK). Referencia al encabezado de la remisión de entrada (RemissionEntrance) que contiene este detalle.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de remisión (INT, PK). Clave única del renglón en la tabla RemissionEntranceDetail. Identity(1,1).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada ítem registrado en una remisión de entrada al inventario: qué producto ingresó, en qué cantidad, a qué precio, con qué descuentos e impuestos, y desde qué fuente u orden de compra provino.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail';
