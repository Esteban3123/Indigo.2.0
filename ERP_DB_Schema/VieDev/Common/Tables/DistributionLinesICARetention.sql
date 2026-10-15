CREATE TABLE [Common].[DistributionLinesICARetention] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionLineId]      INT NOT NULL,
    [OperatingUnitId]         INT NOT NULL,
    [AccountPayableConceptId] INT NOT NULL,
    CONSTRAINT [PK_DistributionLinesICARetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionLinesICARetention_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_DistributionLinesICARetention_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_DistributionLinesICARetention_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Common].[DistributionLinesICARetention] NOCHECK CONSTRAINT [FK_DistributionLinesICARetention_AccountPayableConcepts];


GO
ALTER TABLE [Common].[DistributionLinesICARetention] NOCHECK CONSTRAINT [FK_DistributionLinesICARetention_DistributionLines];


GO
ALTER TABLE [Common].[DistributionLinesICARetention] NOCHECK CONSTRAINT [FK_DistributionLinesICARetention_OperatingUnit];

