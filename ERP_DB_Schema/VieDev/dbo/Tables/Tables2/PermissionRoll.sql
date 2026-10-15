CREATE TABLE [dbo].[PermissionRoll] (
    [Id]          INT         NOT NULL,
    [IdRoll]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (5) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TimeStamp]   ROWVERSION  NOT NULL
);

