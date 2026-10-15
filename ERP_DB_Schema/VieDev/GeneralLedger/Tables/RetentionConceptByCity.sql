CREATE TABLE [GeneralLedger].[RetentionConceptByCity] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RetentionConceptId] INT            NOT NULL,
    [CityId]             INT            NOT NULL,
    [Rate]               DECIMAL (5, 3) NOT NULL,
    [TimeStamp]          ROWVERSION     NOT NULL,
    CONSTRAINT [PK_RetentionConceptByCity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetentionConceptByCity_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_RetentionConceptByCity_RetentionConcept] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[RetentionConceptByCity] NOCHECK CONSTRAINT [FK_RetentionConceptByCity_City];


GO
ALTER TABLE [GeneralLedger].[RetentionConceptByCity] NOCHECK CONSTRAINT [FK_RetentionConceptByCity_RetentionConcept];

