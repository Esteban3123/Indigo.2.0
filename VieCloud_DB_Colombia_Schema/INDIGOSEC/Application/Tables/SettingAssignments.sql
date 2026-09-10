CREATE TABLE [Application].[SettingAssignments] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [EnvironmentId] INT NOT NULL,
    [SettingId]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

