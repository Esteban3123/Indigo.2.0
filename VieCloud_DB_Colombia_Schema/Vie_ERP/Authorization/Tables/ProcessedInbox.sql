-- =============================================================================
-- NUEVO (2026-06-15) — Hallazgo HIGH H1 (ADR-010 Capa 4: Inbox transaccional).
-- Bandeja de entrada (INBOX) de IDEMPOTENCIA de los consumidores de mensajería del
-- dominio de Autorización. Implementa la Capa 4 del patrón de deduplicación de ADR-010:
-- el consumidor registra, EN LA MISMA TRANSACCIÓN que su efecto de negocio, el MessageId
-- determinístico de cada mensaje procesado. La inserción es la guardia de exactly-once:
-- si el MessageId ya existe (reintrega de Service Bus / republicación), el UNIQUE la
-- rechaza y el consumidor trata el mensaje como duplicado, en lugar de volver a aplicar
-- el efecto. Ver docs/architecture/ADR-010-DEDUPLICACION.md y AUTORIZACION-CONSUMER-DESIGN.md.
--
-- POR QUÉ ESTA TABLA (vs. la dedup por llave de negocio que reemplaza):
--   El consumidor de Autorización deduplicaba por la llave de negocio del control
--   (Admission + SubjectType + SubjectCode + Folio) mediante un SELECT previo NO
--   transaccional. Eso (a) descarta órdenes RE-VERSIONADAS legítimas que comparten la
--   misma llave de negocio y (b) no es atómico frente a entregas concurrentes. El INBOX
--   transaccional por MessageId determinístico (emitido por el Normalizer, ADR-010 Capa 2)
--   es la fuente de verdad de "ya procesé este mensaje"; la llave de negocio se conserva
--   SOLO como defensa adicional, no como sustituto.
--
-- MONO-TENANT POR DISEÑO: la tabla vive en la base del tenant (una base por tenant,
--   ADR-002/003). [TenantId] se guarda como AUDITORÍA del tenant resuelto en el Control
--   Plane, NO como parte de la llave de deduplicación: la unicidad es (ConsumerName,
--   MessageId) dentro de esta base. [ConsumerName] permite que varios consumidores
--   compartan la tabla sin colisionar entre sí.
--
-- APPEND-ONLY: cada fila representa un mensaje ya visto. No se actualiza (salvo, en el
--   propio INSERT, la marca [IgnoredAsDuplicate] cuando el flujo detecta el duplicado por
--   la vía de negocio antes del choque de UNIQUE). No se borra aquí; la purga/retención es
--   una tarea operativa separada (igual que [Integrations].[ProcessedEvents]).
-- =============================================================================
CREATE TABLE [Authorization].[ProcessedInbox] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsumerName]       NVARCHAR (100) NOT NULL,
    [MessageId]          NVARCHAR (500) NOT NULL,
    [TenantId]           NVARCHAR (64)  NULL,
    [IgnoredAsDuplicate] BIT            NOT NULL CONSTRAINT [DF_ProcessedInbox_IgnoredAsDuplicate] DEFAULT ((0)),
    [ProcessedAtUtc]     DATETIME2 (7)  NOT NULL CONSTRAINT [DF_ProcessedInbox_ProcessedAtUtc] DEFAULT (sysutcdatetime()),
    CONSTRAINT [PK_ProcessedInbox] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_ProcessedInbox_Consumer_Message] UNIQUE NONCLUSTERED ([ConsumerName] ASC, [MessageId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandeja de entrada (INBOX) de idempotencia de los consumidores de mensajería del dominio de Autorización (Capa 4 de ADR-010). Cada fila registra, en la misma transacción que su efecto de negocio, el MessageId determinístico de un mensaje ya procesado por un consumidor. La inserción es la guardia de exactly-once: el índice único (ConsumerName, MessageId) rechaza reentregas y republicaciones, evitando reaplicar el efecto. Reemplaza la deduplicación NO transaccional por llave de negocio (que descartaba órdenes re-versionadas). Mono-tenant: vive en la base del tenant; [TenantId] es auditoría, no parte de la llave.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (BIGINT IDENTITY) de la fila del inbox. Clave primaria clustered. Es solo la identidad de la fila; la deduplicación se gobierna por el índice único (ConsumerName, MessageId), no por este Id.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre lógico del consumidor que procesó el mensaje (NVARCHAR 100), por ejemplo ''AuthorizationOrderConsumer''. Forma parte de la llave de deduplicación junto con [MessageId], de modo que distintos consumidores puedan compartir esta tabla sin colisionar entre sí.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ConsumerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ConsumerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MessageId determinístico del mensaje de Service Bus tal como lo emite el productor/normalizer (ADR-010 Capa 2). Es la identidad estable del mensaje a través de reentregas y republicaciones. Junto con [ConsumerName] forma la llave única de deduplicación. NVARCHAR 500 para acomodar MessageIds compuestos (p.ej. con prefijo de tenant).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant (NVARCHAR 64, NULL) resuelto en el Control Plane para el mensaje. Se guarda como AUDITORÍA; NO forma parte de la llave de deduplicación, porque la tabla es mono-tenant (vive en la base del tenant). NULL si el tenant aún no se resolvió al registrar la fila.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'TenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca (BIT) que indica que la fila se registró para un mensaje detectado como DUPLICADO (el efecto de negocio NO se reaplicó). 1 = el mensaje ya había sido procesado (reentrega/republicación) y se ignoró; 0 = primer procesamiento exitoso del mensaje. Por defecto 0. Permite distinguir, en auditoría, los procesamientos efectivos de las reentregas absorbidas.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'IgnoredAsDuplicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'IgnoredAsDuplicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC (DATETIME2(7)) en que se registró el procesamiento del mensaje. Por defecto sysutcdatetime() (reloj del servidor, UTC). Sustenta auditoría y la purga/retención operativa del inbox.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ProcessedAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ProcessedAtUtc';
