CREATE TABLE [Chat].[GroupUser] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [IdUser] INT           NOT NULL,
    [Name]   VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupUser_User] FOREIGN KEY ([IdUser]) REFERENCES [Security].[User] ([Id])
);

