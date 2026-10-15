CREATE TABLE [Authorization].[TraceabilityPaperworkEventsAttachedDocuments] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkEventsId] INT             NOT NULL,
    [Description]                   VARCHAR (100)   NULL,
    [FileAttached]                  VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_TraceabilityPaperworkEventsAttachedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);

