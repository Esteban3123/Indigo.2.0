CREATE TABLE [Application].[EnvironmentSpaces] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Environment] VARCHAR (50)  NULL,
    [AppName]     VARCHAR (100) NULL,
    [ContainerId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

