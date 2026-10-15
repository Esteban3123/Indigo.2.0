CREATE TABLE [Cost].[ClosedMonthPayroll] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [CostCenterId]     INT             NOT NULL,
    [PositionId]       INT             NOT NULL,
    [TotalHoursWorked] INT             NOT NULL,
    [ValueTotal]       NUMERIC (18, 2) NOT NULL,
    [AverageCost]      NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthPayroll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthPayroll_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthPayroll_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthPayroll_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthPayroll] NOCHECK CONSTRAINT [FK_ClosedMonthPayroll_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthPayroll] NOCHECK CONSTRAINT [FK_ClosedMonthPayroll_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthPayroll] NOCHECK CONSTRAINT [FK_ClosedMonthPayroll_Position];

