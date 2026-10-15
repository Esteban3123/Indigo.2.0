CREATE TABLE [Security].[PermissionUserInt] (
    [Id]          INT         NOT NULL,
    [IdUser]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (3) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TenantId]    SMALLINT    NULL,
    [TimeStamp]   ROWVERSION  NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PermissionUserInt]
    ON [Security].[PermissionUserInt]([IdUser] ASC, [IdForm] ASC, [Action] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionUserInt_Action_ActionValue_IdUser_TenantId]
    ON [Security].[PermissionUserInt]([Action] ASC, [ActionValue] ASC, [IdUser] ASC, [TenantId] ASC, [IdForm] ASC);

