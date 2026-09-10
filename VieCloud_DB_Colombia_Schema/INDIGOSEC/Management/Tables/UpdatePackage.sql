CREATE TABLE [Management].[UpdatePackage] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [NumberVersion]   INT           NOT NULL,
    [PackageVersion]  VARCHAR (12)  NOT NULL,
    [Description]     VARCHAR (MAX) CONSTRAINT [DF_UpdatePackage_Description] DEFAULT ('-') NOT NULL,
    [BuildDate]       DATETIME      NOT NULL,
    [CreationDate]    DATETIME      CONSTRAINT [DF_UpdatePackage_CreationDate] DEFAULT (getdate()) NOT NULL,
    [PackageFileName] VARCHAR (100) NOT NULL,
    [Status]          BIT           CONSTRAINT [DF_UpdatePackage_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UpdatePackage] PRIMARY KEY CLUSTERED ([Id] ASC)
);

