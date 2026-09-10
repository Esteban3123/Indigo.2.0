CREATE TABLE [rda].[Submission](
	[SubmissionId] [bigint] IDENTITY(1,1) NOT NULL,
	[LocalPatientId] [varchar](20) NULL,
	[DocumentType] [varchar](5) NULL,
	[DocumentNumber] [varchar](20) NULL,
	[SubmissionDate] [datetime2](7) NULL,
	[DataHash] [varchar](64) NULL,
	[IdHispaca] [varchar](20) NULL,
	[RdaBody] [varchar](max) NULL,
	[Created_At] [datetime] NULL,
	[Updated_At] [datetime] NULL,
	[Payload] [varchar](max) NULL,
	[Send] [bit] NULL,
 CONSTRAINT [PK__Submissi__449EE125E326DED7] PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
