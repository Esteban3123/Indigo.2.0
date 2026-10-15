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
ALTER TABLE [Inventory].[RemissionEntranceDetail] NOCHECK CONSTRAINT [FK_RemissionEntranceDetail_InventoryContract];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetail] NOCHECK CONSTRAINT [FK_RemissionEntranceDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetail] NOCHECK CONSTRAINT [FK_RemissionEntranceDetail_PurchaseOrder];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetail] NOCHECK CONSTRAINT [FK_RemissionEntranceDetail_RemissionEntrance];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento neto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'NetDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario bruto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'GrossUnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote del detalle de la Remision de Inventario en Consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del ultimo costo del producto, esto se calcula con el metodo de promedio ponderado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'LastValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que va ingresar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de inventarios, solo se solicita si el origen es por contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de compra, solo se solicita si el origen de la remision es por orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el codigo del documento que genero la entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3  - Contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'Id';

