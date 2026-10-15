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
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_Inventory.PurchaseOrderDetail_ProductHierarchy];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseRequestDetail];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de Jerarquía', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'HierarchyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la  jerarquía del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductHierarchyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de solicitud de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen de la orden (0:Orden de compra, 1:Solicitud de compra, 2:Contrato)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OrderSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de detalle de solicitud de compra de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Cancelada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pendiente por entregar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto, puede ser nulo si la orden es por servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';

