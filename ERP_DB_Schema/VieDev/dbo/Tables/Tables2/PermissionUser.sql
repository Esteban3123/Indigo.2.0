CREATE TABLE [dbo].[PermissionUser] (
    [Id]          INT         NOT NULL,
    [IdUser]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (3) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TenantId]    SMALLINT    NULL,
    [TimeStamp]   ROWVERSION  NOT NULL
);

