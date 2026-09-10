CREATE TABLE [Security].[UserInt] (
    [Id]                     INT           NOT NULL,
    [IdPerson]               INT           NOT NULL,
    [UserCode]               VARCHAR (20)  NOT NULL,
    [RollCode]               INT           NOT NULL,
    [GroupCode]              INT           NOT NULL,
    [Position]               VARCHAR (150) NULL,
    [UserType]               CHAR (1)      NULL,
    [ChangePassword]         BIT           NULL,
    [DaysChangePassword]     INT           NULL,
    [DateLastChangePassword] DATETIME      NULL,
    [DateExpiryAccount]      DATETIME      NULL,
    [Password]               VARCHAR (50)  NOT NULL,
    [State]                  BIT           NOT NULL,
    [UserNameLync]           VARCHAR (100) NULL,
    [AddressSingInLync]      VARCHAR (100) NULL,
    [PasswordLync]           VARCHAR (100) NULL,
    [PersonalNote]           VARCHAR (100) NULL,
    [ViewForm]               BIT           NOT NULL,
    [CodeInterface]          VARCHAR (12)  NULL,
    [IsLockedOut]            BIT           NOT NULL,
    [FailedPasswordCount]    INT           NOT NULL,
    [ProfileType]            CHAR (1)      NULL,
    [Email]                  VARCHAR (60)  NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [OldId]                  INT           NULL,
    [RefreshToken]           VARCHAR (100) NULL,
    [TenantId]               INT           NULL
);
GO
CREATE NONCLUSTERED INDEX IX_UserInt_UserCode_Performance
ON Security.UserInt (UserCode)
INCLUDE (IdPerson);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios internos del sistema con sus credenciales de acceso, roles, grupos y configuración de seguridad. Registra la información de autenticación y control de acceso de cada usuario de la plataforma Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la persona asociada al usuario; vincula con el maestro de personas o empleados.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o nombre de usuario para iniciar sesión en el sistema (login).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol asignado al usuario; determina los permisos y funcionalidades disponibles.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'RollCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'RollCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de seguridad al que pertenece el usuario; agrupa usuarios con permisos similares.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'GroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'GroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o posición laboral del usuario dentro de la organización.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario (ej: administrador, operativo, auditor); clasifica el perfil de acceso general.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario debe cambiar su contraseña en el próximo inicio de sesión (sí/no).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'ChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'ChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de vigencia de la contraseña antes de requerir cambio obligatorio.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'DaysChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'DaysChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio de contraseña realizado por el usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'DateLastChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'DateLastChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o expiración de la cuenta de usuario; después de esta fecha el acceso queda inhabilitado.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'DateExpiryAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'DateExpiryAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña cifrada del usuario para autenticación en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del usuario; indica si puede o no ingresar al sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario en Microsoft Lync / Skype Empresarial para comunicación interna.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'UserNameLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'UserNameLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de inicio de sesión en Lync (SIP address); usada para integración de mensajería.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'AddressSingInLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'AddressSingInLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña del usuario para el servicio de mensajería Lync / Skype Empresarial.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'PasswordLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'PasswordLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota personal o comentario libre asociado al usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'PersonalNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'PersonalNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario tiene habilitada la visualización de formularios en el sistema (sí/no).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'ViewForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'ViewForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de interfaz o integración externa asignado al usuario; usado para intercambio de datos con otros sistemas.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'CodeInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'CodeInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la cuenta del usuario está bloqueada por intentos fallidos de acceso u otras causas de seguridad.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'IsLockedOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'IsLockedOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador de intentos fallidos de ingreso de contraseña; se usa para bloquear la cuenta por seguridad.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'FailedPasswordCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'FailedPasswordCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de perfil del usuario; puede clasificar niveles de acceso o vistas personalizadas.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'ProfileType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'ProfileType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del usuario; usado para notificaciones, recuperación de contraseña y comunicaciones del sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo interna del sistema para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador anterior del usuario en migraciones o sistemas legados; permite trazabilidad histórica.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'OldId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'OldId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token de refresco para autenticación OAuth / JWT; permite renovar la sesión sin volver a ingresar credenciales.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'RefreshToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'RefreshToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant u organización a la que pertenece el usuario en entornos multi-empresa o multi-institución.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserInt', @level2type = N'COLUMN', @level2name = N'TenantId';
