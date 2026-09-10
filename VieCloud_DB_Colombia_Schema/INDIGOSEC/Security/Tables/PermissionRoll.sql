CREATE TABLE [Security].[PermissionRoll] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [IdRoll]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (5) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TimeStamp]   ROWVERSION  NOT NULL,
    CONSTRAINT [PK_PermissionRoll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PermissionRoll_Roll] FOREIGN KEY ([IdRoll]) REFERENCES [Security].[Roll] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Security_PermissionRoll_Action_ActionValue_IdRoll_IdForm]
    ON [Security].[PermissionRoll]([Action] ASC, [ActionValue] ASC, [IdRoll] ASC)
    INCLUDE([IdForm]);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionRoll_ActionValue_IdForm_IdRoll_Action_TimeStamp]
    ON [Security].[PermissionRoll]([ActionValue] ASC, [IdForm] ASC, [IdRoll] ASC)
    INCLUDE([Action], [TimeStamp]);

