CREATE TABLE [WebAppointment].[UserContainers] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [UserId]      INT      NOT NULL,
    [ContainerId] INT      NOT NULL,
    [CreatedAt]   DATETIME DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserContainers_Containers] FOREIGN KEY ([ContainerId]) REFERENCES [Security].[Containers] ([Id]),
    CONSTRAINT [FK_UserContainers_Users] FOREIGN KEY ([UserId]) REFERENCES [WebAppointment].[Users] ([Id]),
    CONSTRAINT [UQ_UserContainers] UNIQUE NONCLUSTERED ([UserId] ASC, [ContainerId] ASC)
);

