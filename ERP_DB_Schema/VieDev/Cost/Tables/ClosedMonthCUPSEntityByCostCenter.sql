CREATE TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId] INT             NOT NULL,
    [CostCenterId]  INT             NOT NULL,
    [CUPSEntityId]  INT             NOT NULL,
    [Quantity]      INT             NOT NULL,
    [TotalSales]    DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCUPSEntityByCostCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CUPSEntity];

