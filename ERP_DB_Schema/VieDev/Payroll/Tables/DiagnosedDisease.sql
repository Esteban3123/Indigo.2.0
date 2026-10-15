CREATE TABLE [Payroll].[DiagnosedDisease] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [State]            BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_DiagnosedDisease_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_DiagnosedDisease_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_DiagnosedDisease__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_DiagnosedDisease__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);

