CREATE TABLE [Platform].[TenantOutboxCursor] (
    [TenantId]         UNIQUEIDENTIFIER NOT NULL,
    [OutboxName]       NVARCHAR (100)   DEFAULT ('Clinical.OutboxEvent') NOT NULL,
    [LastSyncVersion]  BIGINT           DEFAULT ((-1)) NOT NULL,
    [LastPollAtUtc]    DATETIME2 (7)    NULL,
    [LastSuccessAtUtc] DATETIME2 (7)    NULL,
    [ErrorCount]       INT              DEFAULT ((0)) NOT NULL,
    [UpdatedAtUtc]     DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_TenantOutboxCursor] PRIMARY KEY CLUSTERED ([TenantId] ASC, [OutboxName] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cursor persistente del relay (AzClinicalOutboxRelay) por tenant y por tabla outbox (Clinical.OutboxEvent, Hospitalization.OutboxEvent, Billing.OutboxEvent, rda.OutboxEvent, etc. — ver OutboxTableCatalog.cs). PK compuesta (TenantId, OutboxName): un mismo tenant tiene UN cursor independiente por cada outbox que drena (ADR-011 §Plan de Infraestructura, relay multi-tabla). El progreso se mide en SYS_CHANGE_VERSION de SQL Change Tracking de la base tenant, NO en OutboxId.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK logica hacia Platform.TenantCatalog.TenantId. Identifica de que tenant es este cursor.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre calificado Esquema.Tabla de la outbox que gobierna este cursor (ej. Clinical.OutboxEvent, Billing.OutboxEvent). Debe coincidir exactamente con las constantes de OutboxTableCatalog.cs — el nombre se interpola como identificador SQL en CHANGETABLE, por eso el relay valida su formato (IsValidIdentifier) antes de usarlo.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'OutboxName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultima SYS_CHANGE_VERSION de Change Tracking procesada exitosamente para este (tenant, outbox). Valor -1 = cursor recien dado de alta, pendiente de inicializacion (el relay lo setea a CHANGE_TRACKING_CURRENT_VERSION() en su primer ciclo, sin reprocesar historial previo). El relay solo avanza este valor cuando TODOS los mensajes de esa version fueron aceptados por Service Bus (ADR-011 §Reglas de Avance).', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'LastSyncVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC del ultimo intento de poll de este cursor (exitoso o no). Usado para monitoreo/alertas de tenants que dejaron de sondearse.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'LastPollAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC del ultimo poll que efectivamente avanzo LastSyncVersion (es decir, sin errores). Junto con LastPollAtUtc permite detectar un cursor que sondea pero siempre falla.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'LastSuccessAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador de errores consecutivos al procesar este cursor. Usado por el relay para backoff/circuit-breaker y para las alertas operativas (ver Platform.PoolThrottlingConfig para el throttling relacionado a nivel de pool).', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'ErrorCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de la ultima actualizacion de la fila (cualquier campo). Auditoria.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxCursor', @level2type = N'COLUMN', @level2name = N'UpdatedAtUtc';
GO

