CREATE TABLE [Cost].[ClosedMonthCUPSEntity] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]      INT             NOT NULL,
    [ProductionCenterId] INT             NOT NULL,
    [CUPSEntityId]       INT             NOT NULL,
    [Quantity]           INT             NOT NULL,
    [UnitValue]          DECIMAL (18, 2) NOT NULL,
    [EstimatedValue]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCUPSEntity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCUPSEntity_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntity_CUPSEntity] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntity_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_CUPSEntity];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_ProductionCenter];

