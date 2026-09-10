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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos individuales asignados a usuarios sobre formularios o pantallas del sistema. Controla qué acciones (ver, crear, editar, eliminar) puede realizar cada usuario en cada módulo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario al que se le asigna el permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del formulario o pantalla del sistema sobre el que aplica el permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de acción controlada, por ejemplo: ver, insertar, editar o eliminar.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario tiene habilitada (verdadero) o deshabilitada (falso) la acción sobre ese formulario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'ActionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'ActionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant o empresa en entornos multiempresa, indica a qué organización pertenece el permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo de la última modificación del registro, usada para control de concurrencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionUserInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
