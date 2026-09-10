CREATE TABLE [Integrations].[Outbox] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [OccurredOn]    DATETIME       NOT NULL,
    [EventType]     NVARCHAR (200) NOT NULL,
    [AggregateType] NVARCHAR (100) NULL,
    [AggregateKey]  NVARCHAR (50)  NULL,
    [Payload]       NVARCHAR (MAX) NOT NULL,
    [IsPublished]   BIT            CONSTRAINT [DF_Outbox_IsPublished] DEFAULT ((0)) NOT NULL,
    [Action]        VARCHAR (100)  NOT NULL,
    CONSTRAINT [PK_Outbox_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Outbox_IsPublished]
    ON [Integrations].[Outbox]([IsPublished] ASC, [OccurredOn] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cola de salida (outbox) de eventos de integración: registra los eventos de negocio generados por el sistema que deben ser publicados hacia otros servicios o sistemas externos, garantizando la entrega confiable de mensajes.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del evento en la cola de salida.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que ocurrió el evento de negocio.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'OccurredOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'OccurredOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o nombre del evento generado, por ejemplo ''''PacienteCreado'''' o ''''IngresoActualizado''''.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'EventType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'EventType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad de negocio (agregado) que originó el evento, por ejemplo ''''Paciente'''', ''''Admisión'''', ''''Factura''''.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'AggregateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'AggregateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave o identificador de la entidad de negocio que generó el evento, por ejemplo el código del paciente o número de ingreso.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'AggregateKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'AggregateKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del evento en formato JSON u otro formato serializado; incluye todos los datos del mensaje que se enviará al sistema destino.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'Payload';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'Payload';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el evento ya fue publicado y enviado al sistema externo (1 = publicado, 0 = pendiente de envío).', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'IsPublished';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'IsPublished';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción de negocio que representa el evento, por ejemplo ''''Crear'''', ''''Actualizar'''', ''''Eliminar'''', ''''Notificar''''.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Outbox', @level2type = N'COLUMN', @level2name = N'Action';
