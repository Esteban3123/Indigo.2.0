CREATE TABLE [Inventory].[tmp_ClosedMonthInventory] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [IdRelacion]       INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ProductCost]      DECIMAL (18, 2) NULL,
    [FinalProductCost] NUMERIC (18, 2) NULL,
    [SellingPrice]     NUMERIC (18, 2) NULL,
    [ProductId]        INT             NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [WareHouseId]      INT             NULL,
    CONSTRAINT [PK_Tmp_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

