CREATE TABLE [Chat].[UsersGroupUser] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [IdGroupUser] INT NOT NULL,
    [IdUser]      INT NOT NULL,
    CONSTRAINT [PK_UsersGroupUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersGroupUser_GroupUser] FOREIGN KEY ([IdGroupUser]) REFERENCES [Chat].[GroupUser] ([Id]),
    CONSTRAINT [FK_UsersGroupUser_User] FOREIGN KEY ([IdUser]) REFERENCES [Security].[User] ([Id])
);

