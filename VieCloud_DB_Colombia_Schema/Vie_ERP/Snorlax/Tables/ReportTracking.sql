
CREATE TABLE [Snorlax].[ReportTracking](
	[Id] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[AdmissionNumber] [char](10) NOT NULL,
	[ReportType] [varchar](100) NOT NULL,
	[PatientCode] [varchar](32) NULL,
	[FolioNumber] [varchar](32) NULL,
	[StoryType] [int] NULL,
	[ClinicalHistoryCode] [varchar](64) NULL,
	[CareCenter] [varchar](32) NULL,
	[Status] [varchar](20) NOT NULL,
	[BlobUrl] [nvarchar](max) NULL,
	[OwnerId] [varchar](200) NULL,
	[LeaseExpiresAtUtc] [datetime2](7) NULL,
	[Attempts] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[CompletedAtUtc] [datetime2](7) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[RenderMs] [int] NULL,
	[PipelineMs] [int] NULL,
 CONSTRAINT [PK_ReportTracking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Snorlax].[ReportTracking] ADD  CONSTRAINT [DF_ReportTracking_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [Snorlax].[ReportTracking] ADD  CONSTRAINT [DF_ReportTracking_Status]  DEFAULT ('Processing') FOR [Status]
GO

ALTER TABLE [Snorlax].[ReportTracking] ADD  CONSTRAINT [DF_ReportTracking_Attempts]  DEFAULT ((0)) FOR [Attempts]
GO

ALTER TABLE [Snorlax].[ReportTracking] ADD  CONSTRAINT [DF_ReportTracking_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAtUtc]
GO


