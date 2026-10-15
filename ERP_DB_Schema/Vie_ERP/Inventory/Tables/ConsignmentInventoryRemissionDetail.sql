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
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetail] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_ConsignmentInventoryRemission];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetail] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_ConsignmentInventoryRemissionDetail];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetail] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_InventoryContract];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetail] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetail] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetail_PurchaseOrder];


GO
CREATE NONCLUSTERED INDEX [UX_ConsignmentInventoryRemissionDetail_ConsignmentInventoryRemissionId]
    ON [Inventory].[ConsignmentInventoryRemissionDetail]([ConsignmentInventoryRemissionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del ultimo costo del producto, esto se calcula con el metodo de promedio ponderado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'LastValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que va ingresar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la Remision de Inventario en Consignación, solo se solicita si el tipo de movimiento es de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de inventarios, solo se solicita si el origen es por contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de compra, solo se solicita si el origen de la remision es por orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el codigo del documento que genero la entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3 - Contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision del inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'Id';

