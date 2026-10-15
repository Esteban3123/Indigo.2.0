CREATE TABLE [Cost].[CostInventoryGroup] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]            INT           NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [Name]                       VARCHAR (100) NOT NULL,
    [InventoryMeasurementUnitId] INT           NOT NULL,
    [Description]                VARCHAR (500) NOT NULL,
    [Status]                     BIT           NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CostInventoryGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInventoryGroup_InventoryMeasurementUnit] FOREIGN KEY ([InventoryMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CostInventoryGroup_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostInventoryGroup] NOCHECK CONSTRAINT [FK_CostInventoryGroup_InventoryMeasurementUnit];


GO
ALTER TABLE [Cost].[CostInventoryGroup] NOCHECK CONSTRAINT [FK_CostInventoryGroup_OperatingUnit];

