/****** Objeto: Table [SecuritySync].[OutboxEvent] Fecha de script: 15/07/2026 6:01:06 a. m. ******/
CREATE TABLE [SecuritySync].[OutboxEvent](
	[OutboxId] [bigint] IDENTITY(1,1) NOT NULL,
	[OccurredAtUtc] [datetime2](7) NOT NULL,
	[SourceDatabase] [sysname] NOT NULL,
	[SourceSchema] [sysname] NOT NULL,
	[SourceTable] [sysname] NOT NULL,
	[Operation] [char](1) NOT NULL,
	[AggregateType] [varchar](32) NOT NULL,
	[AggregateId] [int] NOT NULL,
	[AggregateKeyJson] [nvarchar](1000) NOT NULL,
	[RoutingBeforeJson] [nvarchar](max) NULL,
	[RoutingAfterJson] [nvarchar](max) NULL,
	[TriggerName] [sysname] NOT NULL,
	[AppName] [nvarchar](128) NULL,
	[LoginName] [nvarchar](128) NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OutboxEvent] PRIMARY KEY CLUSTERED 
(
	[OutboxId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [SecuritySync].[OutboxEvent] ADD  CONSTRAINT [DF_OutboxEvent_OccurredAtUtc]  DEFAULT (sysutcdatetime()) FOR [OccurredAtUtc]
GO

ALTER TABLE [SecuritySync].[OutboxEvent] ADD  CONSTRAINT [DF_OutboxEvent_SourceDatabase]  DEFAULT (db_name()) FOR [SourceDatabase]
GO

ALTER TABLE [SecuritySync].[OutboxEvent] ADD  CONSTRAINT [DF_OutboxEvent_AppName]  DEFAULT (app_name()) FOR [AppName]
GO

ALTER TABLE [SecuritySync].[OutboxEvent] ADD  CONSTRAINT [DF_OutboxEvent_LoginName]  DEFAULT (suser_sname()) FOR [LoginName]
GO

ALTER TABLE [SecuritySync].[OutboxEvent] ADD  CONSTRAINT [DF_OutboxEvent_CreatedAtUtc]  DEFAULT (sysutcdatetime()) FOR [CreatedAtUtc]
GO

ALTER TABLE [SecuritySync].[OutboxEvent]  WITH CHECK ADD  CONSTRAINT [CK_OutboxEvent_AggregateKeyJson_IsJson] CHECK  ((isjson([AggregateKeyJson])=(1)))
GO

ALTER TABLE [SecuritySync].[OutboxEvent] CHECK CONSTRAINT [CK_OutboxEvent_AggregateKeyJson_IsJson]
GO

ALTER TABLE [SecuritySync].[OutboxEvent]  WITH CHECK ADD  CONSTRAINT [CK_OutboxEvent_AggregateType] CHECK  (([AggregateType]='Group' OR [AggregateType]='Role' OR [AggregateType]='User'))
GO

ALTER TABLE [SecuritySync].[OutboxEvent] CHECK CONSTRAINT [CK_OutboxEvent_AggregateType]
GO

ALTER TABLE [SecuritySync].[OutboxEvent]  WITH CHECK ADD  CONSTRAINT [CK_OutboxEvent_Operation] CHECK  (([Operation]='U' OR [Operation]='I'))
GO

ALTER TABLE [SecuritySync].[OutboxEvent] CHECK CONSTRAINT [CK_OutboxEvent_Operation]
GO

ALTER TABLE [SecuritySync].[OutboxEvent]  WITH CHECK ADD  CONSTRAINT [CK_OutboxEvent_RoutingAfterJson_IsJson] CHECK  (([RoutingAfterJson] IS NULL OR isjson([RoutingAfterJson])=(1)))
GO

ALTER TABLE [SecuritySync].[OutboxEvent] CHECK CONSTRAINT [CK_OutboxEvent_RoutingAfterJson_IsJson]
GO

ALTER TABLE [SecuritySync].[OutboxEvent]  WITH CHECK ADD  CONSTRAINT [CK_OutboxEvent_RoutingBeforeJson_IsJson] CHECK  (([RoutingBeforeJson] IS NULL OR isjson([RoutingBeforeJson])=(1)))
GO

ALTER TABLE [SecuritySync].[OutboxEvent] CHECK CONSTRAINT [CK_OutboxEvent_RoutingBeforeJson_IsJson]
GO


