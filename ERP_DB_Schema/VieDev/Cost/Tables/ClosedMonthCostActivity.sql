CREATE TABLE [Cost].[ClosedMonthCostActivity] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]          INT             NOT NULL,
    [CostActivityId]         INT             NOT NULL,
    [CUPSEntityId]           INT             NOT NULL,
    [CostProductionCenterId] INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [FixedAssetValue]        DECIMAL (24, 6) NOT NULL,
    [PayrollValue]           DECIMAL (24, 6) NOT NULL,
    [InventoryValue]         DECIMAL (24, 6) NOT NULL,
    [AddictionalValue]       DECIMAL (24, 6) NOT NULL,
    [UnitValue]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCostActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCostActivity_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostProductionCenter] FOREIGN KEY ([CostProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CostActivity];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CostProductionCenter];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity];

