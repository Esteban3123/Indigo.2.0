CREATE TABLE [Integrations].[OutboxArchive] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Source]      VARCHAR (50)     NOT NULL,
    [EventType]   VARCHAR (100)    NOT NULL,
    [Payload]     NVARCHAR (MAX)   NULL,
    [AggregateId] VARCHAR (20)     NULL,
    [Status]      TINYINT          NOT NULL,
    [RetryCount]  INT              NOT NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [AvailableAt] DATETIME2 (7)    NOT NULL,
    [ProcessedAt] DATETIME2 (7)    NULL,
    [LastError]   NVARCHAR (MAX)   NULL,
    [ArchivedAt]  DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ArchivedBy]  VARCHAR (100)    NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de archivo que almacena mensajes del patrón Outbox que ya fueron procesados o descartados del buzón principal de integración. Registra el historial completo de eventos publicados hacia sistemas externos, incluyendo su tipo, carga útil, número de reintentos, errores ocurridos y estado final. Permite trazabilidad y auditoría de las integraciones, conservando quién y cuándo archivó cada mensaje.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'TABLE', @level1name=N'OutboxArchive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'TABLE', @level1name=N'OutboxArchive';
GO
