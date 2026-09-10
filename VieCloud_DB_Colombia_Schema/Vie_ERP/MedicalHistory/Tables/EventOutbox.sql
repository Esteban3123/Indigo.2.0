CREATE TABLE [MedicalHistory].[EventOutbox] (
    [ID]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Aggregate_type] NVARCHAR (100)   NOT NULL,
    [Aggregate_id]   NVARCHAR (100)   NOT NULL,
    [Event_type]     NVARCHAR (100)   NOT NULL,
    [Payload]        NVARCHAR (MAX)   NOT NULL,
    [Status]         NVARCHAR (50)    DEFAULT ('pending') NOT NULL,
    [Retry_count]    INT              DEFAULT ((0)) NOT NULL,
    [Error_message]  NVARCHAR (MAX)   NULL,
    [Created_at]     DATETIME2 (7)    DEFAULT (sysutcdatetime()) NOT NULL,
    [Processed_at]   DATETIME2 (7)    NULL,
    [EntitySource]   VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cola de eventos de historia clínica (patrón Outbox) que registra los eventos de dominio generados por cambios clínicos, pendientes de ser enviados a otros sistemas o servicios. Garantiza la entrega confiable de eventos aunque ocurran fallas transitorias.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del evento en la cola, generado automáticamente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad clínica o de negocio que originó el evento (por ejemplo: HistoriaClínica, Admisión, Paciente).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Aggregate_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Aggregate_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad específica que generó el evento (por ejemplo: número de ingreso, código de paciente).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Aggregate_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Aggregate_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o tipo del evento ocurrido (por ejemplo: PacienteCreado, DiagnósticoRegistrado, OrdenEmitida).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Event_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Event_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del evento en formato JSON o texto, con todos los datos del cambio clínico o de negocio que se debe comunicar.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Payload';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Payload';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de procesamiento del evento: pendiente de envío (pending), procesado exitosamente, con error, etc.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos de reenvío del evento; permite controlar reintentos ante fallas de entrega.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Retry_count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Retry_count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del error ocurrido en el último intento de procesamiento del evento, útil para diagnóstico y soporte.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Error_message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Error_message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se generó y registró el evento en la cola.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Created_at';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Created_at';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que el evento fue procesado y enviado exitosamente al sistema destino.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Processed_at';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'Processed_at';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sistema o módulo de origen que generó el evento (por ejemplo: HistoriaClínica, Admisiones, Urgencias).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'EntitySource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'EventOutbox', @level2type = N'COLUMN', @level2name = N'EntitySource';
