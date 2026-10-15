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
ALTER TABLE [Inventory].[EntranceVoucherDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDetail_ConsignmentInventoryRemissionDetailBatchSerial];


GO
ALTER TABLE [Inventory].[EntranceVoucherDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDetail_EntranceVoucher];


GO
ALTER TABLE [Inventory].[EntranceVoucherDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDetail_InventoryContractDetail];


GO
ALTER TABLE [Inventory].[EntranceVoucherDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[EntranceVoucherDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDetail_PurchaseOrderDetail];


GO
ALTER TABLE [Inventory].[EntranceVoucherDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDetail_RemissionEntranceDetailBatchSerial];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la entrada  1 - Ninguna  2 - Orden de compra  3 - Contrato (Fijo)  4 - Remision de entrada  5 - Remision de inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetail', @level2type = N'COLUMN', @level2name = N'EntranceSource';

