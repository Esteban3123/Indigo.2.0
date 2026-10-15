CREATE TABLE [MixingStation].[CMConfigurationSchedule] (
    [Id]                INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT      NOT NULL,
    [DayId]             TINYINT  NOT NULL,
    [StartTime]         DATETIME NOT NULL,
    [EndTime]           DATETIME NOT NULL,
    CONSTRAINT [PK_CMConfigurationSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMConfigurationSchedule_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMConfigurationSchedule] NOCHECK CONSTRAINT [FK_CMConfigurationSchedule_CMConfiguration];

