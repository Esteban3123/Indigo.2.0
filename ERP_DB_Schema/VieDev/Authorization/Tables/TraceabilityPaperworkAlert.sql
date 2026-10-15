CREATE TABLE [Authorization].[TraceabilityPaperworkAlert] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkId] INT           NOT NULL,
    [Comments]                VARCHAR (MAX) NOT NULL,
    [Status]                  BIT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    CONSTRAINT [PK_TraceabilityPaperworkAlert] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperworkAlert_TraceabilityPaperwork] FOREIGN KEY ([TraceabilityPaperworkId]) REFERENCES [Authorization].[TraceabilityPaperwork] ([Id])
);


GO
ALTER TABLE [Authorization].[TraceabilityPaperworkAlert] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkAlert_TraceabilityPaperwork];

