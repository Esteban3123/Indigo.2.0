CREATE TABLE [GeneralLedger].[RetentionConceptRanges] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RetentionId]    INT             NOT NULL,
    [Percentage]     DECIMAL (18, 2) NOT NULL,
    [ValueInitial]   DECIMAL (18, 2) NOT NULL,
    [ValueFinish]    DECIMAL (18, 2) NOT NULL,
    [ValueDeducted]  DECIMAL (18, 2) NOT NULL,
    [ValueIncrement] DECIMAL (5, 2)  NOT NULL,
    [UVTIncrement]   DECIMAL (18, 2) CONSTRAINT [DF_RetentionConceptRanges_UVTIncrement] DEFAULT ((0)) NOT NULL,
    [TimeStamp]      ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AccountingRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingRetention_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[RetentionConceptRanges] NOCHECK CONSTRAINT [FK_AccountingRetention_RetentionConcept];

