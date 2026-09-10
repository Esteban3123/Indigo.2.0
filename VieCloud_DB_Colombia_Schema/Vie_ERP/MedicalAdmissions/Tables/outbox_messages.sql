/****** Object:  Table [MedicalAdmissions].[outbox_messages]    Script Date: 21/04/2026 8:08:48 a. m. ******/
CREATE TABLE [MedicalAdmissions].[outbox_messages] (
    [id]             UNIQUEIDENTIFIER   NOT NULL,
    [event_type]     NVARCHAR (256)     NOT NULL,
    [aggregate_id]   NVARCHAR (128)     NOT NULL,
    [payload]        NVARCHAR (MAX)     NOT NULL,
    [correlation_id] NVARCHAR (128)     NOT NULL,
    [causation_id]   NVARCHAR (128)     NOT NULL,
    [created_at]     DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    [processed_at]   DATETIMEOFFSET (7) NULL,
    [retry_count]    INT                DEFAULT ((0)) NOT NULL,
    [max_retries]    INT                DEFAULT ((10)) NOT NULL,
    [next_retry_at]  DATETIMEOFFSET (7) NULL,
    [status]         NVARCHAR (32)      DEFAULT ('Pending') NOT NULL,
    [error_message]  NVARCHAR (MAX)     NULL,
    [destination]    NVARCHAR (128)     NOT NULL,
    [priority]       INT                DEFAULT ((0)) NOT NULL,
    [tenant_id]      NVARCHAR (64)      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
ALTER TABLE [MedicalAdmissions].[outbox_messages] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO


GO


GO


GO


GO


GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de patrón *Outbox* para el módulo de admisiones médicas, que almacena eventos de dominio pendientes de publicación hacia sistemas externos o buses de mensajería. Cada registro guarda el tipo de evento, el agregado origen, el payload serializado y el destino, junto con metadatos de trazabilidad (correlation_id, causation_id, tenant_id). Implementa lógica de reintentos configurable (máximo 10 por defecto) con seguimiento de estado (`Pending`, procesado, error) y programación del próximo intento mediante `next_retry_at`.', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'TABLE', @level1name=N'outbox_messages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'TABLE', @level1name=N'outbox_messages';
GO
CREATE NONCLUSTERED INDEX [idx_outbox_pending]
    ON [MedicalAdmissions].[outbox_messages]([status] ASC, [priority] DESC, [created_at] ASC) WHERE ([status] IN ('Pending', 'Failed'));


GO
CREATE NONCLUSTERED INDEX [idx_outbox_aggregate]
    ON [MedicalAdmissions].[outbox_messages]([aggregate_id] ASC, [created_at] ASC);

