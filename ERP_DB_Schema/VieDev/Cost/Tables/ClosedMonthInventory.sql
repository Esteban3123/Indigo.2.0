CREATE TABLE [Cost].[ClosedMonthInventory] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]        INT             NOT NULL,
    [CostCenterId]         INT             NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [AverageCost]          NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_CostInventoryGroup];

