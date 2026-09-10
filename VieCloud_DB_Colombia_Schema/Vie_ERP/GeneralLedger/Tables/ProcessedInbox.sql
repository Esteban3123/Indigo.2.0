-- =============================================================================
-- Bandeja de entrada (INBOX) de IDEMPOTENCIA de los consumidores de mensajería del
-- dominio de Contabilidad (GeneralLedger).
-- =============================================================================
CREATE TABLE [GeneralLedger].[ProcessedInbox] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsumerName]       NVARCHAR (100) NOT NULL,
    [MessageId]          NVARCHAR (500) NOT NULL,
    [TenantId]           NVARCHAR (64)  NULL,
    [IgnoredAsDuplicate] BIT            NOT NULL CONSTRAINT [DF_GeneralLedgerProcessedInbox_IgnoredAsDuplicate] DEFAULT ((0)),
    [ProcessedAtUtc]     DATETIME2 (7)  NOT NULL CONSTRAINT [DF_GeneralLedgerProcessedInbox_ProcessedAtUtc] DEFAULT (sysutcdatetime()),
    CONSTRAINT [PK_GeneralLedgerProcessedInbox] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_GeneralLedgerProcessedInbox_Consumer_Message] UNIQUE NONCLUSTERED ([ConsumerName] ASC, [MessageId] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandeja de entrada (INBOX) de idempotencia de los consumidores de mensajería del dominio de Contabilidad (GeneralLedger). Cada fila registra, en la misma transacción que su efecto de negocio, el MessageId determinístico de un mensaje ya procesado desde [GeneralLedger].[OutboxEvent]. La inserción es la guardia de exactly-once: el índice único (ConsumerName, MessageId) rechaza reentregas y republicaciones, evitando reaplicar el efecto. Mono-tenant: vive en la base del tenant; [TenantId] es auditoría, no parte de la llave.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (BIGINT IDENTITY) de la fila del inbox. Clave primaria clustered. Es solo la identidad de la fila; la deduplicación se gobierna por el índice único (ConsumerName, MessageId), no por este Id.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre lógico del consumidor que procesó el mensaje. Forma parte de la llave de deduplicación junto con [MessageId], de modo que distintos consumidores puedan compartir esta tabla sin colisionar entre sí.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ConsumerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MessageId determinístico del mensaje de Service Bus tal como lo emite el productor/relay. Es la identidad estable del mensaje a través de reentregas y republicaciones. Junto con [ConsumerName] forma la llave única de deduplicación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant resuelto en el Control Plane para el mensaje. Se guarda como AUDITORÍA; NO forma parte de la llave de deduplicación, porque la tabla es mono-tenant (vive en la base del tenant). NULL si el tenant aún no se resolvió al registrar la fila.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca (BIT) que indica que la fila se registró para un mensaje detectado como DUPLICADO (el efecto de negocio NO se reaplicó). 1 = el mensaje ya había sido procesado (reentrega/republicación) y se ignoró; 0 = primer procesamiento exitoso del mensaje. Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'IgnoredAsDuplicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC (DATETIME2(7)) en que se registró el procesamiento del mensaje. Por defecto sysutcdatetime(). Sustenta auditoría y la purga/retención operativa del inbox.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ProcessedAtUtc';
GO
