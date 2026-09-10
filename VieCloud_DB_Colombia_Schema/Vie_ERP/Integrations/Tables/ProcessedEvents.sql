CREATE TABLE [Integrations].[ProcessedEvents] (
    [EventId]       NVARCHAR (128) NOT NULL,
    [ProcessedAt]   DATETIME2 (3)  DEFAULT (sysutcdatetime()) NOT NULL,
    [ProcessorName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_ProcessedEvents] PRIMARY KEY CLUSTERED ([EventId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ProcessedEvents_ProcessedAt]
    ON [Integrations].[ProcessedEvents]([ProcessedAt] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de eventos de integración ya procesados. Permite controlar qué mensajes o eventos externos fueron recibidos y procesados, evitando duplicados en los flujos de integración del sistema.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del evento de integración procesado, usado para rastrear y evitar reprocesamiento del mismo mensaje.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents', @level2type = N'COLUMN', @level2name = N'EventId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents', @level2type = N'COLUMN', @level2name = N'EventId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que el evento fue procesado por el sistema de integración.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents', @level2type = N'COLUMN', @level2name = N'ProcessedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents', @level2type = N'COLUMN', @level2name = N'ProcessedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del procesador, servicio o componente que ejecutó el procesamiento del evento.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents', @level2type = N'COLUMN', @level2name = N'ProcessorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'ProcessedEvents', @level2type = N'COLUMN', @level2name = N'ProcessorName';
