CREATE TABLE [Security].[TenantUsersInt] (
    [Id]            INT          NOT NULL,
    [TenantId]      SMALLINT     NOT NULL,
    [UserId]        INT          NOT NULL,
    [RollId]        INT          NOT NULL,
    [GroupId]       INT          NOT NULL,
    [Position]      VARCHAR (30) NULL,
    [UserType]      CHAR (1)     NOT NULL,
    [State]         BIT          NOT NULL,
    [CodeInterface] VARCHAR (12) NULL,
    [IsLockedOut]   BIT          NOT NULL,
    [ManageCompany] BIT          NOT NULL,
    [TimeStamp]     ROWVERSION   NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre usuarios y empresas (tenants) del sistema, con sus roles, grupos y permisos de acceso. Controla qué usuarios tienen acceso a cada empresa, su tipo, estado y si están bloqueados.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación usuario-empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la empresa u organización (tenant) a la que pertenece el usuario dentro del sistema multiempresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario al que se le asigna el acceso, rol y permisos en la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol asignado al usuario dentro de la empresa, determina sus permisos y accesos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'RollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'RollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de trabajo o perfil al que pertenece el usuario dentro de la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o posición del usuario dentro de la organización (ej: médico, enfermera, administrador).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario: indica la categoría o naturaleza del acceso (ej: interno, externo, administrativo, asistencial).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del usuario en la empresa; indica si el acceso está habilitado (1) o deshabilitado (0).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de interfaz o integración asociado al usuario, usado para conexiones con sistemas externos o módulos específicos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'CodeInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'CodeInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario está bloqueado (1) y no puede iniciar sesión, generalmente por intentos fallidos o suspensión manual.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'IsLockedOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'IsLockedOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario tiene permisos de administración sobre la empresa (1 = sí administra, 0 = no administra).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'ManageCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'ManageCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática del sistema que registra la última modificación del registro, usada para control de concurrencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsersInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
