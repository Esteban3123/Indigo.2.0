/****** Objeto: Table [Platform].[TenantCatalog] Fecha de script: 15/07/2026 9:37:17 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Platform].[TenantCatalog](
	[TenantId] [uniqueidentifier] NOT NULL,
	[TenantCode] [nvarchar](50) NOT NULL,
	[DatabaseName] [nvarchar](128) NOT NULL,
	[ServerName] [nvarchar](255) NOT NULL,
	[ElasticPoolName] [nvarchar](128) NULL,
	[Region] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TenantCatalog] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_TenantCatalog_Code] UNIQUE NONCLUSTERED 
(
	[TenantCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Platform].[TenantCatalog] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [Platform].[TenantCatalog] ADD  DEFAULT ('ACTIVE') FOR [Status]
GO




/****** Objeto: Table [Platform].[TenantOutboxCursor] Fecha de script: 15/07/2026 9:38:28 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Platform].[TenantOutboxCursor](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OutboxName] [nvarchar](100) NOT NULL,
	[LastSyncVersion] [bigint] NOT NULL,
	[LastPollAtUtc] [datetime2](7) NULL,
	[LastSuccessAtUtc] [datetime2](7) NULL,
	[ErrorCount] [int] NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TenantOutboxCursor] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC,
	[OutboxName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Platform].[TenantOutboxCursor] ADD  DEFAULT ('Clinical.OutboxEvent') FOR [OutboxName]
GO

ALTER TABLE [Platform].[TenantOutboxCursor] ADD  DEFAULT ((-1)) FOR [LastSyncVersion]
GO

ALTER TABLE [Platform].[TenantOutboxCursor] ADD  DEFAULT ((0)) FOR [ErrorCount]
GO


/****** Objeto: Table [Platform].[TenantOutboxLease] Fecha de script: 15/07/2026 9:37:51 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Platform].[TenantOutboxLease](
	[TenantId] [uniqueidentifier] NOT NULL,
	[LeaseOwner] [nvarchar](255) NOT NULL,
	[LeaseExpiresAtUtc] [datetime2](7) NOT NULL,
	[AcquiredAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TenantOutboxLease] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Objeto: Table [Platform].[PoolThrottlingConfig] Fecha de script: 15/07/2026 9:37:32 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Platform].[PoolThrottlingConfig](
	[ElasticPoolName] [nvarchar](128) NOT NULL,
	[MaxConcurrentTenants] [int] NOT NULL,
	[BatchSizePerTenant] [int] NOT NULL,
	[PollIntervalActiveMs] [int] NOT NULL,
	[PollIntervalIdleMs] [int] NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PoolThrottlingConfig] PRIMARY KEY CLUSTERED 
(
	[ElasticPoolName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Platform].[PoolThrottlingConfig] ADD  DEFAULT ((5)) FOR [MaxConcurrentTenants]
GO

ALTER TABLE [Platform].[PoolThrottlingConfig] ADD  DEFAULT ((100)) FOR [BatchSizePerTenant]
GO

ALTER TABLE [Platform].[PoolThrottlingConfig] ADD  DEFAULT ((5000)) FOR [PollIntervalActiveMs]
GO

ALTER TABLE [Platform].[PoolThrottlingConfig] ADD  DEFAULT ((30000)) FOR [PollIntervalIdleMs]
GO


---------------------------------llenado de tablas -----------------------------------


DECLARE @TenantId UNIQUEIDENTIFIER = NEWID();

update Platform.TenantCatalog set databasename = 'INDIGO636', elasticpoolname ='PRO-POOL', servername ='ssindigo.database.windows.net',region = 'local-Pro'
 
INSERT INTO Platform.TenantCatalog (
    TenantId,
    TenantCode,
    DatabaseName,
    ServerName,
    ElasticPoolName,
    Region,
    IsActive,
    Status,
    CreatedAtUtc,
    UpdatedAtUtc
)
VALUES (
    @TenantId,
    N'QA-HOMI-COLOMBIA',
    N'INDIGO636',
    N'ssindigo.database.windows.net',
    N'PRO-POOL',
    N'local-Pro',
    1,
    N'ACTIVE',
    SYSUTCDATETIME(),
    SYSUTCDATETIME()
);
 
INSERT INTO Platform.TenantOutboxCursor (
    TenantId,
    OutboxName,
    LastSyncVersion,
    UpdatedAtUtc
)
VALUES (
    @TenantId,
    N'Clinical.OutboxEvent',
    -1,
    SYSUTCDATETIME()
);
 
INSERT INTO Platform.TenantOutboxCursor (
    TenantId,
    OutboxName,
    LastSyncVersion,
    UpdatedAtUtc
)
VALUES (
    @TenantId,
    N'Billing.OutboxEvent',
    -1,
    SYSUTCDATETIME()
);
 
IF NOT EXISTS (
    SELECT 1
    FROM Platform.PoolThrottlingConfig
    WHERE ElasticPoolName = N'PRO-POOL'
)
BEGIN
    INSERT INTO Platform.PoolThrottlingConfig (
        ElasticPoolName,
        MaxConcurrentTenants,
        BatchSizePerTenant,
        PollIntervalActiveMs,
        PollIntervalIdleMs,
        UpdatedAtUtc
    )
    VALUES (
        N'PRO-POOL',
        2,
        50,
        5000,
        30000,
        SYSUTCDATETIME()
    );
END;
GO	
