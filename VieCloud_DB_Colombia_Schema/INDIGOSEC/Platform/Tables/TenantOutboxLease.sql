CREATE TABLE [Platform].[TenantOutboxLease] (
    [TenantId]          UNIQUEIDENTIFIER NOT NULL,
    [LeaseOwner]        NVARCHAR (255)   NOT NULL,
    [LeaseExpiresAtUtc] DATETIME2 (7)    NOT NULL,
    [AcquiredAtUtc]     DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_TenantOutboxLease] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mecanismo de lease/lock por tenant para que el relay procese cada tenant desde UNA sola instancia/worker a la vez (Competing Consumers seguro — evita que dos workers dupliquen el drenado del mismo tenant en paralelo). PK simple por TenantId (un lease activo por tenant, cubre todas sus outbox). Las filas se crean/liberan/renuevan en runtime; este script no inserta datos iniciales.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxLease';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK logica hacia Platform.TenantCatalog.TenantId. Tenant sobre el que se tiene (o tuvo) el lease.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxLease', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la instancia del relay (worker) que sostiene actualmente el lease de este tenant (ej. nombre de instancia/host). Permite diagnosticar cual worker esta procesando un tenant dado.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxLease', @level2type = N'COLUMN', @level2name = N'LeaseOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que expira el lease si no se renueva. Pasado este instante, otro worker puede tomar el lease del tenant (recuperacion ante caida del owner actual sin quedar bloqueado indefinidamente).', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxLease', @level2type = N'COLUMN', @level2name = N'LeaseExpiresAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que el LeaseOwner actual adquirio el lease (o lo renovo por ultima vez).', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantOutboxLease', @level2type = N'COLUMN', @level2name = N'AcquiredAtUtc';
GO

