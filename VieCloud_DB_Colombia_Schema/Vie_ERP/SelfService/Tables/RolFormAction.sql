CREATE TABLE [SelfService].[RolFormAction] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [RoleId]       INT NOT NULL,
    [FormActionId] INT NOT NULL,
    [Status]       BIT CONSTRAINT [DF_RolFormAction_Status] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RolFormAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolFormAction_FormAction] FOREIGN KEY ([FormActionId]) REFERENCES [SelfService].[FormAction] ([Id]),
    CONSTRAINT [FK_RolFormAction_Role] FOREIGN KEY ([RoleId]) REFERENCES [SelfService].[Role] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos asignados a roles de usuario sobre acciones específicas de formularios o pantallas del sistema. Controla qué operaciones (crear, editar, eliminar, ver) puede ejecutar cada rol en cada módulo.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del permiso asignado.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol de usuario al que se le asigna el permiso (perfil, grupo de acceso).', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'RoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'RoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la acción o función del formulario/pantalla que se está permitiendo (botón, operación, acción).', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'FormActionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'FormActionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del permiso: activo (1) o inactivo (0). Indica si el rol tiene habilitada o deshabilitada la acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolFormAction', @level2type = N'COLUMN', @level2name = N'Status';
