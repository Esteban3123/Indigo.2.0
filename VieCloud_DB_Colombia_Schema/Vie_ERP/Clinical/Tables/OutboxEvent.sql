CREATE TABLE [Clinical].[OutboxEvent]
(
    [OutboxId]          BIGINT IDENTITY (1, 1) NOT NULL,
    [EventId]           UNIQUEIDENTIFIER
                            CONSTRAINT [DF_OutboxEvent_EventId]
                            DEFAULT (NEWID()) NOT NULL,
    [EventType]         NVARCHAR(100) NOT NULL,
    [AggregateType]     NVARCHAR(100) NOT NULL,
    [AggregateId]       NVARCHAR(255) NOT NULL,

    [BusinessEventHash] AS (
        HASHBYTES(
            'SHA2_256',
            CONCAT(
                [EventType],
                '|',
                [AggregateId],
                '|',
                CONVERT(NVARCHAR(30), [OccurredAtUtc], 126)
            )
        )
    ) PERSISTED NOT NULL,

    [PayloadJson]       NVARCHAR(MAX) NOT NULL,

    [PayloadHash] AS (
        HASHBYTES(
            'SHA2_256',
            CONVERT(VARBINARY(MAX), [PayloadJson])
        )
    ) PERSISTED NOT NULL,

    [OccurredAtUtc]     DATETIME2(7) NOT NULL,

    [CreatedAtUtc]      DATETIME2(7)
                            CONSTRAINT [DF_OutboxEvent_CreatedAtUtc]
                            DEFAULT (SYSUTCDATETIME()) NOT NULL,

    CONSTRAINT [PK_OutboxEvent]
        PRIMARY KEY CLUSTERED ([OutboxId] ASC),

    CONSTRAINT [UQ_OutboxEvent_BusinessEventHash]
        UNIQUE NONCLUSTERED ([BusinessEventHash] ASC)
);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad secuencial de la fila en el log append-only. Clave primaria clustered. NO es el cursor del relay: el avance del relay se gobierna por SYS_CHANGE_VERSION de Change Tracking, no por OutboxId (ver ADR-011, gap de identity bajo concurrencia).', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OutboxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unico del evento clinico. No se usa como MessageId de Service Bus: el relay construye el MessageId deterministico en memoria a partir del TenantId resuelto en el Control Plane, EventType y BusinessEventHash (ver ADR-010).', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del evento clinico. Forma parte del calculo de BusinessEventHash. Se recomienda el formato Dominio.NombreEvento.v1 para ruteo y versionado.', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del agregado de negocio que origino el evento (por ejemplo HistoriaClinica, Orden). Describe la entidad raiz a la que pertenece AggregateId.', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la instancia del agregado de negocio. Forma parte del calculo de BusinessEventHash. Los consumidores aguas abajo lo usan para correlacionar el evento con su agregado.', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash de unicidad de negocio DENTRO de esta base, calculado como SHA2_256 sobre EventType|AggregateId|OccurredAtUtc (ISO 8601, estilo 126). Columna calculada PERSISTED. NO incluye TenantId: la tabla es mono-tenant (el tenant es la base misma); la unicidad global se logra a nivel de mensaje, donde el relay antepone el TenantId resuelto desde el Control Plane (ver ADR-003 y ADR-010). Tiene indice unico para deduplicar inserciones concurrentes sin SELECT IF NOT EXISTS ni MERGE.', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'BusinessEventHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON del evento clinico. El relay lo publica tal cual a Service Bus; la normalizacion la realiza la Function 2 aguas abajo (ver ADR-001).', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash SHA2_256 del PayloadJson. Columna calculada PERSISTED. El relay lo valida antes de publicar como verificacion de integridad del payload (ver ADR-011).', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que ocurrio el evento de negocio. Forma parte del calculo de BusinessEventHash. La establece el productor clinico al momento del hecho.', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OccurredAtUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se inserto la fila en el Outbox. Campo de auditoria. Por defecto SYSUTCDATETIME().', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Outbox clinica append-only y MONO-TENANT (vive dentro de la base de un unico tenant; NO lleva TenantId ni ningun concepto multitenant). Es un log puro de hechos: SIN campos de estado (Status, LockedBy, PublishedAt, RetryCount, ProcessedAt). El relay (Function 1) la lee con SQL Change Tracking bajo snapshot isolation y nunca hace UPDATE/DELETE sobre ella; el progreso vive en Platform.TenantOutboxCursor (LastSyncVersion). Requiere habilitar Change Tracking en la base y en la tabla (ver Clinical/ChangeTracking-enablement.sql, ADR-003 y ADR-011).', @level0type = N'SCHEMA', @level0name = N'Clinical', @level1type = N'TABLE', @level1name = N'OutboxEvent';
