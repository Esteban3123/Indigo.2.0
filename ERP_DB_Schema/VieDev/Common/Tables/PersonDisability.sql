CREATE TABLE [Common].[PersonDisability] (
    [Id]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DisabilityId] INT NOT NULL,
    [PersonId]     INT NOT NULL,
    [Percentage]   INT NOT NULL,
    CONSTRAINT [PK_PersonDisability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDisability_Disability] FOREIGN KEY ([DisabilityId]) REFERENCES [Common].[Disability] ([Id]),
    CONSTRAINT [FK_PersonDisability_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[PersonDisability] NOCHECK CONSTRAINT [FK_PersonDisability_Disability];


GO
ALTER TABLE [Common].[PersonDisability] NOCHECK CONSTRAINT [FK_PersonDisability_Person];

