CREATE TABLE [Authorization].[TraceabilityPaperworkEventsAttachedDocuments] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkEventsId] INT             NOT NULL,
    [Description]                   VARCHAR (100)   NULL,
    [FileAttached]                  VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_TraceabilityPaperworkEventsAttachedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento adjunto de la trazabilidad del trámite de evento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'FileAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción opcional del documento adjunto de trazabilidad del trámite de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la trazabilidad del trámite de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro del documento de trazabilidad del trámite de evento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Id';

