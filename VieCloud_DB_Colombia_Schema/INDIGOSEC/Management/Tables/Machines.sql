CREATE TABLE [Management].[Machines] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [UID]           VARCHAR (50)  NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [NickName]      VARCHAR (100) CONSTRAINT [DF_Machines_NickName_1] DEFAULT ('--') NOT NULL,
    [ClientVersion] VARCHAR (12)  NULL,
    [OSName]        VARCHAR (100) NOT NULL,
    [Architecture]  VARCHAR (5)   CONSTRAINT [DF_Machines_Architecture_1] DEFAULT ('--') NOT NULL,
    [IPs]           VARCHAR (500) CONSTRAINT [DF_Machines_IPs_1] DEFAULT ('--') NOT NULL,
    [MACs]          VARCHAR (500) CONSTRAINT [DF_Machines_MACs_1] DEFAULT ('--') NOT NULL,
    [RegDate]       DATETIME      NOT NULL,
    [LastUpDate]    DATETIME      CONSTRAINT [DF_Machines_LastUpDate_1] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Machines] PRIMARY KEY CLUSTERED ([Id] ASC)
);

