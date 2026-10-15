CREATE TABLE [Inventory].[InventoryContractModificationAvailability] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryContractModificationId] INT             NOT NULL,
    [AvailabilityDetailId]            INT             NOT NULL,
    [Value]                           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_InventoryContractModificationAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContractModificationAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_InventoryContractModificationAvailability_InventoryContractModification] FOREIGN KEY ([InventoryContractModificationId]) REFERENCES [Inventory].[InventoryContractModification] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContractModificationAvailability] NOCHECK CONSTRAINT [FK_InventoryContractModificationAvailability_AvailabilityDetail];


GO
ALTER TABLE [Inventory].[InventoryContractModificationAvailability] NOCHECK CONSTRAINT [FK_InventoryContractModificationAvailability_InventoryContractModification];

