CREATE TABLE [Platform].[PoolThrottlingConfig] (
    [ElasticPoolName]      NVARCHAR (128) NOT NULL,
    [MaxConcurrentTenants] INT            DEFAULT ((5)) NOT NULL,
    [BatchSizePerTenant]   INT            DEFAULT ((100)) NOT NULL,
    [PollIntervalActiveMs] INT            DEFAULT ((5000)) NOT NULL,
    [PollIntervalIdleMs]   INT            DEFAULT ((30000)) NOT NULL,
    [UpdatedAtUtc]         DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_PoolThrottlingConfig] PRIMARY KEY CLUSTERED ([ElasticPoolName] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuracion de throttling del relay POR Elastic Pool de Azure SQL (no por tenant individual). Varios tenants pueden compartir un mismo pool (Platform.TenantCatalog.ElasticPoolName); esta tabla limita cuantos de esos tenants procesa el relay en paralelo y a que ritmo, para no saturar el DTU/vCore del pool compartido. Una fila por ElasticPoolName; si un tenant tiene ElasticPoolName = NULL (base standalone), no aplica ninguna fila de esta tabla.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Elastic Pool de Azure SQL al que aplica esta configuracion. Debe coincidir con Platform.TenantCatalog.ElasticPoolName para los tenants que se quieran regular. PK de la tabla.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig', @level2type = N'COLUMN', @level2name = N'ElasticPoolName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero maximo de tenants de este pool que el relay puede procesar EN PARALELO en un mismo ciclo. Limita la concurrencia contra el pool compartido (default: 5).', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig', @level2type = N'COLUMN', @level2name = N'MaxConcurrentTenants';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad maxima de filas (cambios) que el relay lee/publica por tenant en un solo batch al drenar una outbox (default: 100). Controla el tamano del lote por ciclo de polling.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig', @level2type = N'COLUMN', @level2name = N'BatchSizePerTenant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo en milisegundos entre polls del relay cuando el tenant esta ACTIVO (con cambios recientes o pendientes) (default: 5000 ms). Ritmo mas frecuente que PollIntervalIdleMs para reducir latencia de punta a punta.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig', @level2type = N'COLUMN', @level2name = N'PollIntervalActiveMs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo en milisegundos entre polls del relay cuando el tenant esta INACTIVO/sin cambios recientes (default: 30000 ms). Mas espaciado que PollIntervalActiveMs para no gastar capacidad del pool sondeando tenants sin actividad.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig', @level2type = N'COLUMN', @level2name = N'PollIntervalIdleMs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de la ultima actualizacion de esta configuracion de throttling. Auditoria.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'PoolThrottlingConfig', @level2type = N'COLUMN', @level2name = N'UpdatedAtUtc';
GO

