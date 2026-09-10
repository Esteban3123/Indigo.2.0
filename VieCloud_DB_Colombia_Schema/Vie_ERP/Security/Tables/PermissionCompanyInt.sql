CREATE TABLE [Security].[PermissionCompanyInt] (
    [Id]                     INT NOT NULL,
    [IdUser]                 INT NOT NULL,
    [IdContainer]            INT NOT NULL,
    [IdOperatingUnitDefault] INT NOT NULL,
    [Permission]             BIT NOT NULL,
    [Administrator]          BIT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos de acceso por empresa o contenedor asignados a usuarios del sistema. Controla qué usuarios tienen permiso o rol de administrador sobre cada unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario al que se le asigna el permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contenedor o empresa sobre la cual aplica el permiso.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'IdContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'IdContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad operativa predeterminada asignada al usuario dentro del contenedor.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'IdOperatingUnitDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'IdOperatingUnitDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario tiene permiso de acceso activo (sí/no).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'Permission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'Permission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario tiene rol de administrador sobre el contenedor o empresa (sí/no).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'Administrator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompanyInt', @level2type = N'COLUMN', @level2name = N'Administrator';
