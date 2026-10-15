CREATE TABLE [MixingStation].[CMConfigurationUsers] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [UserCode]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CMConfigurationUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMConfigurationUsers_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMConfigurationUsers] NOCHECK CONSTRAINT [FK_CMConfigurationUsers_CMConfiguration];

