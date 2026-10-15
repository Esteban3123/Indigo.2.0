CREATE TABLE [Security].[PermissionRollInt] (
    [Id]          INT         NOT NULL,
    [IdRoll]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (5) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TimeStamp]   ROWVERSION  NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_IdRoll_IdForm_Action_ActionValue]
    ON [Security].[PermissionRollInt]([IdRoll] ASC, [IdForm] ASC, [Action] ASC, [ActionValue] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_IdForm_Action_IdRoll_ActionValue]
    ON [Security].[PermissionRollInt]([IdForm] ASC, [Action] ASC, [IdRoll] ASC, [ActionValue] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_PermissionRollInt_idform_Action]
    ON [Security].[PermissionRollInt]([IdForm] ASC, [Action] ASC, [ActionValue] ASC, [IdRoll] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_ActionValue_IdRoll_Action]
    ON [Security].[PermissionRollInt]([ActionValue] ASC, [IdRoll] ASC, [Action] ASC, [IdForm] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_ActionValue_IdForm_IdRoll_Action_TimeStamp]
    ON [Security].[PermissionRollInt]([ActionValue] ASC, [IdForm] ASC, [IdRoll] ASC, [Action] ASC, [TimeStamp] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_Action_ActionValue_IdRoll_IdForm]
    ON [Security].[PermissionRollInt]([Action] ASC, [ActionValue] ASC, [IdRoll] ASC, [IdForm] ASC);

