CREATE TABLE [MixingStation].[CMConfigurationExceptions] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT           NOT NULL,
    [StopDate]          DATETIME      NOT NULL,
    [ReasonForStop]     VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_CMConfigurationExceptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMConfigurationExceptions_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMConfigurationExceptions] NOCHECK CONSTRAINT [FK_CMConfigurationExceptions_CMConfiguration];

