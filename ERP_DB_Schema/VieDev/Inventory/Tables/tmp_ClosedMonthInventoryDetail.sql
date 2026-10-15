CREATE TABLE [Inventory].[tmp_ClosedMonthInventoryDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [Quantity]               INT             NOT NULL,
    [WarehouseId]            INT             NOT NULL,
    [BatchSerialId]          INT             NULL,
    [ClosedMonthInventoryId] INT             NOT NULL,
    [CostTotal]              DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Tmp_Inventory_ClosedMonthInventoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);

