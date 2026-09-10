CREATE TABLE [GeneralLedger].[OutboxEvent]
(
    [OutboxId]          BIGINT IDENTITY (1, 1) NOT NULL,
    [EventId]           UNIQUEIDENTIFIER CONSTRAINT [DF_GeneralLedgerOutboxEvent_EventId] DEFAULT (NEWID()) NOT NULL,
    [EventType]         NVARCHAR(100) NOT NULL,
    [AggregateType]     NVARCHAR(100) NOT NULL,
    [AggregateId]       NVARCHAR(255) NOT NULL,
    [BusinessEventHash] AS (HASHBYTES('SHA2_256',CONCAT([EventType],'|',[AggregateId],'|',CONVERT(NVARCHAR(30), [OccurredAtUtc], 126)))) PERSISTED NOT NULL,
    [PayloadJson]       NVARCHAR(MAX) NOT NULL,
    [PayloadHash] AS (HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX), [PayloadJson]))) PERSISTED NOT NULL,
    [OccurredAtUtc]     DATETIME2(7) NOT NULL,
    [CreatedAtUtc]      DATETIME2(7) CONSTRAINT [DF_GeneralLedgerOutboxEvent_CreatedAtUtc] DEFAULT (SYSUTCDATETIME()) NOT NULL,

    CONSTRAINT [PK_GeneralLedgerOutboxEvent] PRIMARY KEY CLUSTERED ([OutboxId] ASC),
    CONSTRAINT [UQ_GeneralLedgerOutboxEvent_BusinessEventHash] UNIQUE NONCLUSTERED ([BusinessEventHash] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Outbox canónica del bounded context de Contabilidad (GeneralLedger). Append-only y mono-tenant (vive dentro de la base de un único tenant; NO lleva TenantId). Log puro de hechos contables: SIN campos de estado (Status, LockedBy, PublishedAt, RetryCount). Escrita únicamente por GeneralLedger.SP_CreateAndValidateJournalVoucherMovement. El relay la lee con SQL Change Tracking bajo snapshot isolation y nunca hace UPDATE/DELETE sobre ella.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad secuencial del log append-only. PK clustered. NO es el cursor del relay: el avance se gobierna por SYS_CHANGE_VERSION de Change Tracking.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OutboxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del evento contable. No se usa como MessageId de Service Bus; el relay construye el MessageId determinístico a partir del TenantId del Control Plane, EventType y BusinessEventHash.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del evento contable. Formato recomendado: Dominio.NombreEvento.v1. Valores canónicos: Accounting.MovementCreated.v1, Accounting.MovementUpdate.v1.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del agregado de negocio que originó el evento. Para movimientos contables: AccountingMovement.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la instancia del agregado (GeneralLedger.AccountingMovement.Id, o el EntityId de origen cuando aplica). Clave de correlación para el consumidor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash SHA2_256 sobre EventType|AggregateId|OccurredAtUtc. Columna calculada PERSISTED. Garantiza unicidad de negocio dentro de la base sin MERGE.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'BusinessEventHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON del evento contable. El relay lo publica tal cual a Service Bus.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash SHA2_256 del PayloadJson. Columna calculada PERSISTED. El relay valida este hash antes de publicar para detectar corrupción del payload.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que ocurrió el hecho de negocio. Parte del cálculo de BusinessEventHash; debe ser estable ante reintentos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OccurredAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de inserción en la outbox. Campo de auditoría. Por defecto SYSUTCDATETIME().', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';
GO
