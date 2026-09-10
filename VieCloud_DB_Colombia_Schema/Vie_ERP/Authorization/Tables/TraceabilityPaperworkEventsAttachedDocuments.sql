CREATE TABLE [Authorization].[TraceabilityPaperworkEventsAttachedDocuments] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkEventsId] INT             NOT NULL,
    [Description]                   VARCHAR (100)   NULL,
    [FileAttached]                  VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_TraceabilityPaperworkEventsAttachedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento adjunto en formato binario (VARBINARY) de la trazabilidad del trámite de evento; contiene archivo, imagen o comprobante asociado al evento de autorización, procedimiento o glosa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'FileAttached';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento adjunto de la trazabilidad del trámite de evento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'FileAttached';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'FileAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual opcional (VARCHAR 100) del documento adjunto; permite identificar el tipo de archivo, contenido o propósito (ej: receta, diagnóstico, soporte de factura, comprobante de pago).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción opcional del documento adjunto de trazabilidad del trámite de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del evento de trazabilidad del trámite; enlaza con tabla TraceabilityPaperworkEvents para asociar el documento a un evento específico de autorización, ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la trazabilidad del trámite de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del registro del documento adjunto de trazabilidad; clave primaria que identifica cada documento vinculado a eventos de autorización y procesos de auditoría.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro del documento de trazabilidad del trámite de evento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos adjuntos vinculados a los eventos de trámites en el proceso de autorización. Guarda los archivos (imágenes, PDFs u otros soportes) que se cargan como evidencia o soporte de cada evento registrado en la trazabilidad de un trámite de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEventsAttachedDocuments';
