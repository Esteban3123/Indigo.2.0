CREATE TABLE [Treasury].[OutstandingChecks] (
    [Id]          INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckBook] INT    NOT NULL,
    [CheckNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_OutstandingChecks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OutstandingChecks_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);


GO
ALTER TABLE [Treasury].[OutstandingChecks] NOCHECK CONSTRAINT [FK_OutstandingChecks_Checkbooks];

