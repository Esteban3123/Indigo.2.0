CREATE TABLE [Admissions].[OutboxEvent]
(
    [OutboxId]          BIGINT IDENTITY (1, 1) NOT NULL,
    [EventId]           UNIQUEIDENTIFIER
                            CONSTRAINT [DF_AdmissionsOutboxEvent_EventId]
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
                            CONSTRAINT [DF_AdmissionsOutboxEvent_CreatedAtUtc]
                            DEFAULT (SYSUTCDATETIME()) NOT NULL,

    CONSTRAINT [PK_AdmissionsOutboxEvent]
        PRIMARY KEY CLUSTERED ([OutboxId] ASC),

    CONSTRAINT [UQ_AdmissionsOutboxEvent_BusinessEventHash]
        UNIQUE NONCLUSTERED ([BusinessEventHash] ASC)
);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad secuencial de la fila en el log append-only. Clave primaria clustered. NO es el cursor del relay: el avance del relay se gobierna por SYS_CHANGE_VERSION de Change Tracking, no por OutboxId (mismo criterio que rda.OutboxEvent, ADR-011).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OutboxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unico del evento de ingresos hospitalarios. No se usa como MessageId de Service Bus: el relay construye el MessageId deterministico en memoria a partir del TenantId resuelto en el Control Plane, EventType y BusinessEventHash (ver ADR-010).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del evento del ingreso. Forma parte del calculo de BusinessEventHash. Formato Dominio.NombreEvento.v1: clinical.admission-registered.v1 (alta, emitido por codigo desde IND_ADM_GrabaIngresosTransaccion) o clinical.admission-status-changed.v1 (cambio de estado, emitido por el trigger TR_ADINGRESO_AdmissionStatusChanged sobre dbo.ADINGRESO). Ver docs/PBI_BD_DISTRIBUCION_AUTORIZADORES_INGRESOS.md.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'EventType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del agregado de negocio que origino el evento. Constante ''Admission'' para esta outbox — describe la entidad raiz a la que pertenece AggregateId.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la instancia del agregado: el NUMINGRES (AdmissionNumber) de dbo.ADINGRESO. Forma parte del calculo de BusinessEventHash. Los consumidores aguas abajo (Normalizer, consumer de distribucion de autorizadores) lo usan para correlacionar el evento con el ingreso.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'AggregateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash de unicidad de negocio DENTRO de esta base, calculado como SHA2_256 sobre EventType|AggregateId|OccurredAtUtc (ISO 8601, estilo 126). Columna calculada PERSISTED. NO incluye TenantId: la tabla es mono-tenant (el tenant es la base misma); la unicidad global se logra a nivel de mensaje, donde el relay antepone el TenantId resuelto desde el Control Plane. Tiene indice unico para deduplicar inserciones concurrentes (codigo app + trigger de BD) sin SELECT IF NOT EXISTS ni MERGE.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'BusinessEventHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON del evento de ingreso. El relay lo publica tal cual a Service Bus; la normalizacion/lectura de ADINGRESO (ADR-004) la realiza la Function 2 aguas abajo. Contrato canonico camelCase: admissionNumber, patientCode, admissionDate, admissionStatus, entityCode, careCenterCode.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash SHA2_256 del PayloadJson. Columna calculada PERSISTED. El relay lo valida antes de publicar como verificacion de integridad del payload.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'PayloadHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que ocurrio el evento de negocio (alta o cambio de estado del ingreso). Forma parte del calculo de BusinessEventHash. La establece el productor (codigo app o trigger) al momento del hecho.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'OccurredAtUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se inserto la fila en el Outbox. Campo de auditoria. Por defecto SYSUTCDATETIME().', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Outbox canonica del bounded context de Admisiones (ingresos hospitalarios), append-only y MONO-TENANT, siguiendo el mismo patron que rda.OutboxEvent/Clinical.OutboxEvent (Outbox + Change Tracking, nunca UPDLOCK/status mutable). Alimenta la feature de Distribucion de Usuarios de Autorizaciones: alta de ingreso (clinical.admission-registered.v1, emitida por codigo desde EHR_ServicesCore en la misma transaccion) y cambio de estado (clinical.admission-status-changed.v1, emitida por el trigger TR_ADINGRESO_AdmissionStatusChanged sobre dbo.ADINGRESO). Es un log puro de hechos: SIN campos de estado (Status, LockedBy, RetryCount, ProcessedAt). El relay (Indigo.AzClinicalOutboxRelay) la lee con SQL Change Tracking; el progreso vive en Platform.TenantOutboxCursor (LastSyncVersion). Requiere habilitar Change Tracking en la base y en la tabla (ver Admissions/ChangeTracking-enablement.sql). Registrar el eventType en Relay:AllowedEventTypes y la tabla en Indigo.AzClinicalOutboxRelay.Logic.OutboxTableCatalog (opt-in via Relay:OutboxTables) antes de dar de alta el tenant. Ver docs/PBI_BD_DISTRIBUCION_AUTORIZADORES_INGRESOS.md.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'OutboxEvent';
