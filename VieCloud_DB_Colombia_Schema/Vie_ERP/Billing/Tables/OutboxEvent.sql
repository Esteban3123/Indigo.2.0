CREATE TABLE [Billing].[OutboxEvent]
(
    -- -----------------------------------------------------------------------
    -- Identidad secuencial del log append-only. PK clustered.
    -- NO es el cursor del relay: el avance se gobierna por SYS_CHANGE_VERSION
    -- de Change Tracking, no por OutboxId (ADR-011, gap de identity bajo
    -- concurrencia).
    -- -----------------------------------------------------------------------
    [OutboxId]          BIGINT IDENTITY (1, 1) NOT NULL,

    -- GUID estable del evento de negocio (dedup a nivel de fila).
    -- NO se usa como MessageId de Service Bus: el relay construye el MessageId
    -- determinístico en memoria (TENANT:{TenantId}:OUTBOX:{EventType}:{BusinessEventHash})
    -- a partir del TenantId resuelto en el Control Plane (ADR-010).
    [EventId]           UNIQUEIDENTIFIER
                            CONSTRAINT [DF_BillingOutboxEvent_EventId]
                            DEFAULT (NEWID()) NOT NULL,

    -- Tipo del evento de facturación. Formato: Dominio.NombreEvento.v1
    -- Valor canónico inicial: billing.stay-committed.v1
    [EventType]         NVARCHAR(100) NOT NULL,

    -- Tipo del agregado de negocio que originó el evento.
    -- Para controles de estancia de facturación: AccountControlStay
    [AggregateType]     NVARCHAR(100) NOT NULL,

    -- Identificador de la instancia del agregado. Clave de correlación para
    -- consumidores aguas abajo. Formato: {AdmissionCode}:{StayId}
    [AggregateId]       NVARCHAR(255) NOT NULL,

    -- Hash de unicidad de negocio DENTRO de esta base.
    -- SHA2_256 sobre EventType|AggregateId|OccurredAtUtc (ISO 8601, estilo 126).
    -- Columna calculada PERSISTED. NO incluye TenantId (tabla mono-tenant).
    -- Índice único: deduplica inserciones concurrentes sin SELECT IF NOT EXISTS
    -- ni MERGE (ADR-003, ADR-010).
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

    -- Payload JSON del evento. El relay lo publica tal cual a Service Bus.
    -- El consumidor (AuthorizationStayConsumer) lo deserializa como StayControlFact.
    [PayloadJson]       NVARCHAR(MAX) NOT NULL,

    -- Hash SHA2_256 del PayloadJson. Columna calculada PERSISTED.
    -- El relay lo valida antes de publicar (integridad del payload, ADR-011).
    [PayloadHash] AS (
        HASHBYTES(
            'SHA2_256',
            CONVERT(VARBINARY(MAX), [PayloadJson])
        )
    ) PERSISTED NOT NULL,

    -- Fecha y hora UTC en que ocurrió el evento de negocio (insert en
    -- Billing.AccountControlStays). Parte del cálculo de BusinessEventHash —
    -- debe ser estable ante reintentos de la misma transacción.
    [OccurredAtUtc]     DATETIME2(7) NOT NULL,

    -- Fecha y hora UTC de inserción en la outbox. Auditoría.
    [CreatedAtUtc]      DATETIME2(7)
                            CONSTRAINT [DF_BillingOutboxEvent_CreatedAtUtc]
                            DEFAULT (SYSUTCDATETIME()) NOT NULL,

    CONSTRAINT [PK_BillingOutboxEvent]
        PRIMARY KEY CLUSTERED ([OutboxId] ASC),

    CONSTRAINT [UQ_BillingOutboxEvent_BusinessEventHash]
        UNIQUE NONCLUSTERED ([BusinessEventHash] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Outbox canónica del bounded context de Facturación/Liquidación de estancias. Append-only y mono-tenant (vive dentro de la base de un único tenant; NO lleva TenantId). Log puro de hechos de facturación de estancias: SIN campos de estado (Status, LockedBy, PublishedAt, RetryCount). El relay (AzClinicalOutboxRelay) la lee con SQL Change Tracking bajo snapshot isolation y nunca hace UPDATE/DELETE sobre ella; el progreso vive en Platform.TenantOutboxCursor (LastSyncVersion). El productor es el módulo ERP al insertar en Billing.AccountControlStays. Requiere habilitar Change Tracking en la base y en la tabla (ver Billing/ChangeTracking-enablement.sql).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad secuencial del log append-only. PK clustered. NO es el cursor del relay: el avance se gobierna por SYS_CHANGE_VERSION de Change Tracking (ADR-011).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OutboxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del evento de facturación de estancia. No se usa como MessageId de Service Bus; el relay construye el MessageId determinístico a partir del TenantId del Control Plane, EventType y BusinessEventHash (ADR-010).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del evento de facturación. Formato recomendado: Dominio.NombreEvento.v1. Valor canónico: billing.stay-committed.v1.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del agregado de negocio que originó el evento. Para controles de estancia: AccountControlStay.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la instancia del agregado. Formato: {AdmissionCode}:{StayId} (Billing.AccountControlStays.AdmissionCode + StayId). Clave de correlación para el consumidor de autorización.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash SHA2_256 sobre EventType|AggregateId|OccurredAtUtc. Columna calculada PERSISTED. Garantiza unicidad de negocio dentro de la base sin MERGE (ADR-003, ADR-010).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'BusinessEventHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON del evento de facturación de estancia. El relay lo publica tal cual a Service Bus. El consumidor AuthorizationStayConsumer lo deserializa como StayControlFact.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash SHA2_256 del PayloadJson. Columna calculada PERSISTED. El relay valida este hash antes de publicar para detectar corrupción del payload (ADR-011).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que ocurrió el hecho de negocio (insert en Billing.AccountControlStays). Parte del cálculo de BusinessEventHash; debe ser estable ante reintentos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OccurredAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de inserción en la outbox. Campo de auditoría. Por defecto SYSUTCDATETIME().', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';
GO
