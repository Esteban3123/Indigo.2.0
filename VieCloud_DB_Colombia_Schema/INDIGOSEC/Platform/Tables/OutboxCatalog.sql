CREATE TABLE [Platform].[OutboxCatalog]
(
    OutboxCatalogId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    OutboxName      nvarchar(200)     NOT NULL,
    IsActive        bit               NOT NULL CONSTRAINT DF_OutboxCatalog_IsActive DEFAULT (1),
    CreatedAtUtc    datetime2(3)      NOT NULL CONSTRAINT DF_OutboxCatalog_CreatedAtUtc DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT UQ_OutboxCatalog_OutboxName UNIQUE (OutboxName)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de outbox tables; única fuente de verdad para sembrar Platform.TenantOutboxCursor por cada tenant', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'OutboxCatalog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'OutboxCatalog', @level2type = N'COLUMN', @level2name = N'OutboxCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre calificado del outbox, ej. Billing.OutboxEvent', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'OutboxCatalog', @level2type = N'COLUMN', @level2name = N'OutboxName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el outbox se sigue sembrando en tenants nuevos', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'OutboxCatalog', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'OutboxCatalog', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';
