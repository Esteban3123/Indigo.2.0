CREATE TABLE [SelfService].[RolesForms] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [FormId]           INT          NOT NULL,
    [RoleId]           INT          NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NULL,
    [CreationDate]     DATETIME     NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_RolesForms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolesForms_Form1] FOREIGN KEY ([FormId]) REFERENCES [SelfService].[Form] ([Id]),
    CONSTRAINT [FK_RolesForms_Role1] FOREIGN KEY ([RoleId]) REFERENCES [SelfService].[Role] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos de acceso que relacionan roles de usuario con formularios o pantallas del sistema de autoservicio. Controla qué formularios puede ver o usar cada rol dentro del portal.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de permiso rol-formulario.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario o pantalla del sistema al que se otorga acceso.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol de usuario al que se le asigna el acceso al formulario.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'RoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'RoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del permiso; indica si el rol tiene habilitado el acceso al formulario.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de permiso.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el permiso de acceso.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último usuario que modificó el registro de permiso.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del permiso.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolesForms', @level2type = N'COLUMN', @level2name = N'ModificationDate';
