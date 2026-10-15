CREATE TABLE [Common].[Department] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CountryId]        INT          NOT NULL,
    [Code]             VARCHAR (3)  NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_Department_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_Department_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Department__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Department_Country] FOREIGN KEY ([CountryId]) REFERENCES [Common].[Country] ([Id])
);


GO
ALTER TABLE [Common].[Department] NOCHECK CONSTRAINT [FK_Department_Country];

