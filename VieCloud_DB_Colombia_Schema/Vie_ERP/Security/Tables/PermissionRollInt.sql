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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos asignados a cada rol del sistema sobre formularios o pantallas específicas. Define qué acciones (ver, crear, editar, eliminar) tiene habilitadas o deshabilitadas cada rol en cada módulo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol al que se le asigna el permiso (perfil de usuario, grupo de seguridad).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'IdRoll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'IdRoll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del formulario, pantalla o módulo al que aplica el permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la acción sobre la que se define el permiso (por ejemplo: ver, insertar, modificar, eliminar).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la acción está permitida o denegada para el rol en ese formulario (1 = permitido, 0 = denegado).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'ActionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'ActionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo de la última modificación del registro, usada para control de concurrencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionRollInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
