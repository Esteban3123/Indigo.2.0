CREATE TABLE [Security].[PermissionUser] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [IdUser]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (3) NOT NULL,
    [TenantId]    SMALLINT    NULL,
    [ActionValue] BIT         NOT NULL,
    [TimeStamp]   ROWVERSION  NOT NULL,
    CONSTRAINT [PK_PermissionUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PermissionUser_User] FOREIGN KEY ([IdUser]) REFERENCES [Security].[User] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PermissionUser]
    ON [Security].[PermissionUser]([IdUser] ASC, [IdForm] ASC, [Action] ASC);

