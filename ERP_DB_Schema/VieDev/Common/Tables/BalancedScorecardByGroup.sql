CREATE TABLE [Common].[BalancedScorecardByGroup] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UserGroup]           INT NOT NULL,
    [BalancedScorecardId] INT NOT NULL,
    CONSTRAINT [PK_BalancedScorecardByGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BalancedScorecardByGroup_BalancedScorecard] FOREIGN KEY ([BalancedScorecardId]) REFERENCES [Common].[BalancedScorecard] ([Id])
);


GO
ALTER TABLE [Common].[BalancedScorecardByGroup] NOCHECK CONSTRAINT [FK_BalancedScorecardByGroup_BalancedScorecard];

