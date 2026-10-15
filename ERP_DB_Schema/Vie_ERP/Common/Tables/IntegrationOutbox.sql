CREATE TABLE [Common].[IntegrationOutbox](
	[EventId]       [uniqueidentifier]  NOT NULL,
	[OccurredAt]    [datetime2](3)      NOT NULL,
	[EventType]     [nvarchar](200)     NOT NULL,
	[AggregateId]   [nvarchar](100)     NOT NULL,
	[Payload]       [nvarchar](max)     NULL,
	[Status]        [tinyint]           NOT NULL,
	[LockedUntil]   [datetime2](3)      NULL,
	[Attempts]      [int]               NOT NULL,
	[LastError]     [nvarchar](2000)    NULL,
	[PublishedAt]   [datetime2](3)      NULL,
	[CreatedAt]     [datetime2](3)      NOT NULL,
 CONSTRAINT [PK_IntegrationOutbox] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Common].[IntegrationOutbox] ADD  CONSTRAINT [DF_IntegrationOutbox_Attempts]  DEFAULT ((0)) FOR [Attempts]
GO

ALTER TABLE [Common].[IntegrationOutbox] ADD  CONSTRAINT [DF_IntegrationOutbox_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador unico del evento de integracion. Se utiliza como MessageId en Service Bus y para idempotencia.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'EventId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora UTC en que el evento ocurrio y fue insertado en el Outbox. Se usa para orden deterministico durante el reclamo.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'OccurredAt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo del evento de integracion. Se recomienda el formato Dominio.NombreEvento.v1 para ruteo y versionado.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'EventType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la entidad o agregado de negocio (por ejemplo InvoiceId). Los workers usan este valor para consultar informacion adicional en la base de datos.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'AggregateId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Payload opcional del evento (por ejemplo JSON). Puede ser NULL cuando el evento actua solo como disparador y los workers consultan la base de datos usando AggregateId.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'Payload'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de procesamiento del evento. 0=Pendiente, 1=EnProceso (reclamado por dispatcher con lease), 2=Publicado.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora UTC hasta la cual el evento esta bloqueado por el dispatcher. Al expirar, el evento puede ser reclamado nuevamente.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'LockedUntil'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de intentos de reclamo del evento por el dispatcher. Se incrementa en cada intento.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'Attempts'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ultimo error registrado durante el intento de publicacion del evento. Util para diagnostico operativo.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'LastError'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora UTC en que el evento fue publicado exitosamente al broker de mensajeria.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'PublishedAt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora UTC en que se inserto el registro en el Outbox. Campo de auditoria.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox', @level2type=N'COLUMN',@level2name=N'CreatedAt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla Transactional Outbox. Contiene eventos de integracion generados dentro de transacciones de negocio y publicados posteriormente por un dispatcher central. Implementa una maquina de estados Pendiente -> EnProceso -> Publicado.' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'IntegrationOutbox'
GO
