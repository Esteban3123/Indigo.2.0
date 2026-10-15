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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento neto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'NetDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario bruto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'GrossUnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3  - Contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';

