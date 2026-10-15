CREATE TABLE [Cost].[CostActivityStepAddictionalCost] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [Description]        VARCHAR (500)   NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostActivityStepAddictionalCost] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepAddictionalCost_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStepAddictionalCost] NOCHECK CONSTRAINT [FK_CostActivityStepAddictionalCost_CostActivityStep];

