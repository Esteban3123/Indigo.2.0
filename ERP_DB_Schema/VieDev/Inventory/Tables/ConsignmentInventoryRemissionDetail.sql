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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3 - Contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetail', @level2type = N'COLUMN', @level2name = N'RemissionSource';

