CREATE TABLE [Cost].[CostActivityStepPayroll] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [PayrollPositionId]  INT             NOT NULL,
    [Hours]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepPayroll__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepPayroll_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepPayroll_PayrollPosition] FOREIGN KEY ([PayrollPositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStepPayroll] NOCHECK CONSTRAINT [FK_CostActivityStepPayroll_CostActivityStep];


GO
ALTER TABLE [Cost].[CostActivityStepPayroll] NOCHECK CONSTRAINT [FK_CostActivityStepPayroll_PayrollPosition];

