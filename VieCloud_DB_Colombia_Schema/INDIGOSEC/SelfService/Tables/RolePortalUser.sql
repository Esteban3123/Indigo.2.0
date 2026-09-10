CREATE TABLE [SelfService].[RolePortalUser] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [PortalUserId] INT NOT NULL,
    [RoleId]       INT NOT NULL,
    CONSTRAINT [PK_RolePortalUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolePortalUser_PortalUser] FOREIGN KEY ([PortalUserId]) REFERENCES [SelfService].[PortalUser] ([Id]),
    CONSTRAINT [FK_RolePortalUser_Role] FOREIGN KEY ([RoleId]) REFERENCES [SelfService].[Role] ([Id])
);

