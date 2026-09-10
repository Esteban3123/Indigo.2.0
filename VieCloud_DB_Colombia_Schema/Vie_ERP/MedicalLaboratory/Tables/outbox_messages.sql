/****** Object:  Table [MedicalLaboratory].[outbox_messages]    Script Date: 21/04/2026 7:56:08 a. m. ******/
CREATE TABLE [MedicalLaboratory].[outbox_messages] (
    [id]             UNIQUEIDENTIFIER   NOT NULL,
    [event_type]     NVARCHAR (256)     NOT NULL,
    [aggregate_id]   NVARCHAR (128)     NOT NULL,
    [payload]        NVARCHAR (MAX)     NOT NULL,
    [correlation_id] NVARCHAR (128)     NOT NULL,
    [causation_id]   NVARCHAR (128)     NOT NULL,
    [created_at]     DATETIMEOFFSET (7) CONSTRAINT [DF__outbox_me__creat__2B70B2D8] DEFAULT ([Common].[GETDATE]()) NOT NULL,
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
ALTER TABLE [MedicalLaboratory].[outbox_messages] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO


GO


GO


GO


GO


GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Implementa el patrón Outbox para el módulo de Laboratorio Médico, almacenando eventos de dominio pendientes de publicación hacia sistemas externos o colas de mensajería. Cada registro captura el tipo de evento, el agregado origen, el payload serializado y metadatos de enrutamiento como destino y tenant. Gestiona la resiliencia mediante control de reintentos con límite configurable (por defecto 10), estado del mensaje (`Pending` inicial) y programación del próximo intento, garantizando entrega eventual de eventos en arquitecturas distribuidas multiinquilino.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'TABLE', @level1name=N'outbox_messages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'TABLE', @level1name=N'outbox_messages';
GO
CREATE NONCLUSTERED INDEX [idx_outbox_pending]
    ON [MedicalLaboratory].[outbox_messages]([status] ASC, [priority] DESC, [created_at] ASC) WHERE ([status] IN ('Pending', 'Failed'));


GO
CREATE NONCLUSTERED INDEX [idx_outbox_aggregate]
    ON [MedicalLaboratory].[outbox_messages]([aggregate_id] ASC, [created_at] ASC);

