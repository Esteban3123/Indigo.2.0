CREATE TABLE [Inventory].[AdjustedPhysicalInventory] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalInventoryId] INT NOT NULL,
    [QuantityPrevious]    INT NOT NULL,
    [Quantity]            INT NOT NULL,
    CONSTRAINT [PK_AdjustedPhysicalInventory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

