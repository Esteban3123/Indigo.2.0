CREATE TABLE [Billing].[BasicBillingDetailItem] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BasicBillingDetailId] INT NOT NULL,
    [PhysicalInventoryId]  INT NOT NULL,
    [Quantity]             INT NOT NULL,
    CONSTRAINT [PK_BasicBillingDetailItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingDetailItem_BasicBillingDetail] FOREIGN KEY ([BasicBillingDetailId]) REFERENCES [Billing].[BasicBillingDetail] ([Id]),
    CONSTRAINT [FK_BasicBillingDetailItem_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBillingDetailItem] NOCHECK CONSTRAINT [FK_BasicBillingDetailItem_BasicBillingDetail];


GO
ALTER TABLE [Billing].[BasicBillingDetailItem] NOCHECK CONSTRAINT [FK_BasicBillingDetailItem_PhysicalInventory];

