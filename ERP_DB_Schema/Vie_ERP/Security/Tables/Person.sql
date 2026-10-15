CREATE TABLE [Security].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identification] [varchar](15) NOT NULL,
	[IdentificationType] [smallint] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[SecondName] [varchar](50) NULL,
	[FirstLastName] [varchar](50) NOT NULL,
	[SecondLastName] [varchar](50) NULL,
	[Fullname] [varchar](250) NOT NULL,
	[BirthDay] [datetime] NULL,
	[Fingerprint] [varbinary](max) NULL,
	[Gender] [smallint] NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Person_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Person] UNIQUE NONCLUSTERED 
(
	[Identification] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO