CREATE TABLE [Cost].[CostActivityStep] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityId] INT           NOT NULL,
    [Order]          INT           NOT NULL,
    [Description]    VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_CostActivityStep__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStep_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStep] NOCHECK CONSTRAINT [FK_CostActivityStep_CostActivity];

