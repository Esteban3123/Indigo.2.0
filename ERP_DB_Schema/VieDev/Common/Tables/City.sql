CREATE TABLE [Common].[City] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DepartamentId]         INT           NOT NULL,
    [Code]                  VARCHAR (5)   NOT NULL,
    [Name]                  VARCHAR (100) NOT NULL,
    [State]                 BIT           NOT NULL,
    [CreationUser]          VARCHAR (20)  CONSTRAINT [DF_City_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]          DATETIME      CONSTRAINT [DF_City_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [ICARetentionConceptId] INT           NULL,
    CONSTRAINT [PK_City__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_Department] FOREIGN KEY ([DepartamentId]) REFERENCES [Common].[Department] ([Id]),
    CONSTRAINT [FK_City_RetentionConcepts] FOREIGN KEY ([ICARetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [UQ_City__Code__DepartamentId] UNIQUE NONCLUSTERED ([Code] ASC, [DepartamentId] ASC)
);


GO
ALTER TABLE [Common].[City] NOCHECK CONSTRAINT [FK_City_Department];


GO
ALTER TABLE [Common].[City] NOCHECK CONSTRAINT [FK_City_RetentionConcepts];

