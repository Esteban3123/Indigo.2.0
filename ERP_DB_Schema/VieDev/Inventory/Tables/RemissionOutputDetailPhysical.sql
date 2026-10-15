CREATE TABLE [Inventory].[RemissionOutputDetailPhysical] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionOutputDetailId] INT NOT NULL,
    [PhysicalInventoryId]     INT NOT NULL,
    [Quantity]                INT NOT NULL,
    [OutstandingQuantity]     INT NOT NULL,
    CONSTRAINT [PK_RemissionOutputDetailPhysical] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionOutputDetailPhysical_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_RemissionOutputDetailPhysical_RemissionOutputDetail] FOREIGN KEY ([RemissionOutputDetailId]) REFERENCES [Inventory].[RemissionOutputDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionOutputDetailPhysical] NOCHECK CONSTRAINT [FK_RemissionOutputDetailPhysical_PhysicalInventory];


GO
ALTER TABLE [Inventory].[RemissionOutputDetailPhysical] NOCHECK CONSTRAINT [FK_RemissionOutputDetailPhysical_RemissionOutputDetail];

