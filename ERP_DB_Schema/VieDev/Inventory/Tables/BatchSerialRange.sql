CREATE TABLE [Inventory].[BatchSerialRange] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingInventoryId] INT NOT NULL,
    [InitialRange]       INT NOT NULL,
    [EndRange]           INT NOT NULL,
    [Color]              INT NOT NULL,
    CONSTRAINT [PK_BatchSerialRange] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BatchSerialRange_SettingInventory] FOREIGN KEY ([SettingInventoryId]) REFERENCES [Inventory].[SettingInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[BatchSerialRange] NOCHECK CONSTRAINT [FK_BatchSerialRange_SettingInventory];

