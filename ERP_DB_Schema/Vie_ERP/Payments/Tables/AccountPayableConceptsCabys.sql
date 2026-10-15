CREATE TABLE [Payments].[AccountPayableConceptsCabys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountPayableConceptId] [int] NOT NULL,
	[CabysCode] [varchar](20) NOT NULL,
	[CabysDescription] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Payments].[AccountPayableConceptsCabys]  WITH CHECK ADD  CONSTRAINT [FK_AccountPayableConceptsCabys_Concept] FOREIGN KEY([AccountPayableConceptId])
REFERENCES [Payments].[AccountPayableConcepts] ([Id])
GO

ALTER TABLE [Payments].[AccountPayableConceptsCabys] CHECK CONSTRAINT [FK_AccountPayableConceptsCabys_Concept]
GO

