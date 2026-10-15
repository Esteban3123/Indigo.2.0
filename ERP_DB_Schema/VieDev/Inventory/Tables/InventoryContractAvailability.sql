CREATE TABLE [Inventory].[InventoryContractAvailability] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryContractId]  INT             NOT NULL,
    [AvailabilityDetailId] INT             NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_InventoryContractAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContractAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_InventoryContractAvailability_InventoryContract] FOREIGN KEY ([InventoryContractId]) REFERENCES [Inventory].[InventoryContract] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContractAvailability] NOCHECK CONSTRAINT [FK_InventoryContractAvailability_AvailabilityDetail];


GO
ALTER TABLE [Inventory].[InventoryContractAvailability] NOCHECK CONSTRAINT [FK_InventoryContractAvailability_InventoryContract];

