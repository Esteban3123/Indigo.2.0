CREATE TABLE [SelfService].[PortalUser] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [UserCode]       VARCHAR (20)  NOT NULL,
    [Password]       VARCHAR (100) NOT NULL,
    [LockedUser]     BIT           NOT NULL,
    [Status]         BIT           NOT NULL,
    [RefreshToken]   VARCHAR (50)  CONSTRAINT [DF__RG_Recove__Refre__662B2B3B] DEFAULT (' ') NOT NULL,
    [RandomPassword] BIT           CONSTRAINT [DF_PortalUser_RandomPassword_] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PortalUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraseña temporal generada aleatoriamente (BIT: 1=Sí/Temporal, 0=No/Usuario establecida); requiere cambio en primer acceso', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RandomPassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraseña Aleatoria (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RandomPassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RandomPassword';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token de refresco para renovación segura de sesiones en el portal (VARCHAR 50, PII sensible, por defecto espacio)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RefreshToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualizar Token', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RefreshToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RefreshToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario del portal (BIT: 1=Activo/Habilitado, 0=Inactivo/Deshabilitado); controla acceso a autoservicio', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de bloqueo del usuario (BIT: 1=Bloqueado, 0=Desbloqueado); previene acceso al portal por intentos fallidos o administración', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'LockedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario bloqueado', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'LockedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'LockedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña de usuario del portal de autoservicio, almacenada encriptada (VARCHAR 100, PII sensible)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraseña de Usuario del portal', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario único para acceso al portal de autoservicio (VARCHAR 20, login/nombre de usuario)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de usuario del portal (INT, clave primaria, generado automáticamente)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios registrados en el portal de autoservicio (Self Service). Guarda las credenciales de acceso, estado de bloqueo y token de sesión de cada usuario del portal web o aplicación de pacientes y/o profesionales.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser';
