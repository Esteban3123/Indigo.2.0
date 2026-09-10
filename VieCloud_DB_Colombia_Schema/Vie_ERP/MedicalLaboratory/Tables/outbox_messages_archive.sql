/****** Object:  Table [MedicalLaboratory].[outbox_messages_archive]    Script Date: 21/04/2026 7:58:08 a. m. ******/
CREATE TABLE [MedicalLaboratory].[outbox_messages_archive](
	[id] [uniqueidentifier] NOT NULL,
	[event_type] [nvarchar](512) NOT NULL,
	[aggregate_id] [nvarchar](128) NOT NULL,
	[payload] [nvarchar](max) NOT NULL,
	[correlation_id] [nvarchar](128) NOT NULL,
	[causation_id] [nvarchar](128) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[processed_at] [datetimeoffset](7) NULL,
	[retry_count] [int] NOT NULL,
	[max_retries] [int] NOT NULL,
	[next_retry_at] [datetimeoffset](7) NULL,
	[status] [nvarchar](64) NOT NULL,
	[error_message] [nvarchar](max) NULL,
	[destination] [nvarchar](256) NOT NULL,
	[priority] [int] NOT NULL,
	[tenant_id] [nvarchar](128) NOT NULL,
	[archived_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_ML_outbox_archive] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [MedicalLaboratory].[outbox_messages_archive] ADD  DEFAULT ((0)) FOR [retry_count]
GO

ALTER TABLE [MedicalLaboratory].[outbox_messages_archive] ADD  DEFAULT ((10)) FOR [max_retries]
GO

ALTER TABLE [MedicalLaboratory].[outbox_messages_archive] ADD  DEFAULT ((0)) FOR [priority]
GO

ALTER TABLE [MedicalLaboratory].[outbox_messages_archive] ADD  CONSTRAINT [DF_ML_archive_archived_at]  DEFAULT ([Common].[GETDATE]()) FOR [archived_at]
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de archivo del patrón Outbox en el dominio de Laboratorio Médico; almacena mensajes de eventos (domain events) que ya fueron procesados o agotaron sus reintentos (máximo 10 por defecto), preservándolos para auditoría o trazabilidad. Registra el tipo de evento, agregado, carga útil, destino, estado final, conteo de reintentos y errores ocurridos. Soporta multitenancy mediante `tenant_id` y correlación distribuida a través de `correlation_id` y `causation_id`.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'TABLE', @level1name=N'outbox_messages_archive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'TABLE', @level1name=N'outbox_messages_archive';
GO
