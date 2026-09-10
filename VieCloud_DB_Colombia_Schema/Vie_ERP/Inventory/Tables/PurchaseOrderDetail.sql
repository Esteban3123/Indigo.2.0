CREATE TABLE [Inventory].[PurchaseOrderDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderId]         INT             NOT NULL,
    [ProductId]               INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [OutstandingQuantity]     INT             NOT NULL,
    [CancelledQuantity]       INT             NOT NULL,
    [Value]                   DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]           DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [PurchaseRequestDetailId] INT             NULL,
    [OrderSource]             TINYINT         CONSTRAINT [DF_PurchaseOrderDetail_OrderSource] DEFAULT ((0)) NOT NULL,
    [SourceCode]              VARCHAR (20)    NULL,
    [ProductHierarchyId]      INT             NULL,
    [HierarchyQuantity]       INT             NULL,
    CONSTRAINT [PK_PurchaseOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PurchaseOrderDetail] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_PurchaseOrderDetail_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [FK_Inventory.PurchaseOrderDetail_ProductHierarchy] FOREIGN KEY ([ProductHierarchyId]) REFERENCES [Inventory].[ProductHierarchy] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [Inventory].[PurchaseOrder] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDetail_PurchaseRequestDetail] FOREIGN KEY ([PurchaseRequestDetailId]) REFERENCES [Inventory].[PurchaseRequestDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [CK_PurchaseOrderDetail];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [CK_PurchaseOrderDetail_1];




GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [CK_PurchaseOrderDetail];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [CK_PurchaseOrderDetail_1];


GO



GO



GO



GO



GO

-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 20190409
-- Description:	Si la orden de compra es de una gestion de material de osteosintesis, se actualiza el numero de orden en los productos gestionados
-- =============================================
CREATE TRIGGER [Inventory].[UpdateProductOsteosynthesis]
   ON  [Inventory].[PurchaseOrderDetail]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON
	
	UPDATE HCP SET NUMORDEN = PO.Code, MATESTADO =2, CANTENTREG = POD.Quantity
	FROM INSERTED POD
	INNER JOIN Inventory.PurchaseOrder PO
	ON PO.Id = POD.PurchaseOrderId
	AND PO.OsteosynthesisEquipmentId IS NOT NULL 
	INNER JOIN Inventory.InventoryProduct IPR
	ON IPR.ID = POD.ProductId
	INNER JOIN .HCORDPROQD HCP
	ON HCP.AUTOPROCED = PO.OsteosynthesisEquipmentId
	AND HCP.CODPRODUC = IPR.Code
    -- Insert statements for trigger here

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de jerarquía del producto; unidades en nivel jerárquico de inventario (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'HierarchyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Jerarquía', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'HierarchyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'HierarchyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la jerarquía del producto; clave foránea a ProductHierarchy para clasificación multinivel de inventario (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductHierarchyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de la  jerarquía del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductHierarchyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductHierarchyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de solicitud de compra origen; identificador externo o referencia de requisición (VARCHAR 20, nullable)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de solicitud de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la orden: 0=Orden de compra, 1=Solicitud de compra, 2=Contrato; tipo TINYINT que identifica fuente del documento (default 0)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OrderSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen de la orden (0:Orden de compra, 1:Solicitud de compra, 2:Contrato)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OrderSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OrderSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del detalle de solicitud de compra de inventario; clave foránea a PurchaseRequestDetail si la orden proviene de requisición (INT, FK, nullable)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de detalle de solicitud de compra de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del registro en moneda; cálculo: SubTotalValue - DiscountValue + IvaValue; DECIMAL(18,2) con impuestos y descuentos aplicados', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del descuento aplicado; resultado de multiplicar SubTotalValue por DiscountPercentage; DECIMAL(18,2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento sobre subtotal; NUMERIC(5,2) rango 0-100 usado para cálculo de bonificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del impuesto IVA; resultado de multiplicar SubTotalValue por IvaPercentage; DECIMAL(18,2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del IVA a cobrar al producto; NUMERIC(5,2) rango 0-100, alícuota fiscal aplicada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del detalle; cálculo Quantity × Value antes de impuestos y descuentos; DECIMAL(18,2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del producto en moneda; precio de compra por unidad; DECIMAL(18,2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad cancelada/anulada del detalle de orden; unidades que no se entregarán; INT ≥ 0', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Cancelada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por entregar; unidades faltantes entre recibidas y ordenadas; INT, validado: 0 ≤ OutstandingQuantity ≤ Quantity', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente por entregar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del producto ordenado; unidades solicitadas en el detalle de compra; INT > 0 (validado por constraint)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del producto; clave foránea a InventoryProduct; nullable si la orden es por servicio (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto, puede ser nulo si la orden es por servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la cabecera de la orden de compra; clave foránea a PurchaseOrder (INT, FK obligatorio)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de la orden de compra; clave primaria clustered IDENTITY(1,1) (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las órdenes de compra de inventario: cada fila representa un ítem (producto) incluido en una orden de compra, con sus cantidades pedidas, pendientes y canceladas, precios unitarios, IVA, descuentos y totales. Permite controlar el avance de recepción de cada producto solicitado a un proveedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail';
