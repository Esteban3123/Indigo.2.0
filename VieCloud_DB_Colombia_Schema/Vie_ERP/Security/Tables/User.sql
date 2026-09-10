CREATE TABLE [Security].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPerson] [int] NOT NULL,
	[UserCode] [varchar](20) NOT NULL,
	[RollCode] [int] NOT NULL,
	[GroupCode] [int] NOT NULL,
	[Position] [varchar](150) NULL,
	[UserType] [char](1) NULL,
	[ChangePassword] [bit] NULL,
	[DaysChangePassword] [int] NULL,
	[DateLastChangePassword] [datetime] NULL,
	[DateExpiryAccount] [datetime] NULL,
	[Password] [varchar](50) NOT NULL,
	[State] [bit] NOT NULL,
	[UserNameLync] [varchar](100) NULL,
	[AddressSingInLync] [varchar](100) NULL,
	[PasswordLync] [varchar](100) NULL,
	[PersonalNote] [varchar](100) NULL,
	[ViewForm] [bit] NOT NULL,
	[CodeInterface] [varchar](12) NULL,
	[IsLockedOut] [bit] NOT NULL,
	[FailedPasswordCount] [int] NOT NULL,
	[ProfileType] [char](1) NULL,
	[Email] [varchar](60) NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[OldId] [int] NULL,
	[RefreshToken] [varchar](100) NULL,
	[TenantId] [smallint] NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationUser] [varchar](20) NULL,
	[ModificationDate] [datetime] NULL,
	[ElectronicSignatureToken] [varchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SEGUSUARU] UNIQUE NONCLUSTERED 
(
	[IdPerson] ASC,
	[Email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UserCode] UNIQUE NONCLUSTERED 
(
	[UserCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Security].[User] ADD  CONSTRAINT [DF_User_IsLockedOut]  DEFAULT ((0)) FOR [IsLockedOut]
GO

ALTER TABLE [Security].[User] ADD  CONSTRAINT [DF_User_FailedPasswordCount]  DEFAULT ((0)) FOR [FailedPasswordCount]
GO

ALTER TABLE [Security].[User] ADD  DEFAULT ((999)) FOR [CreationUser]
GO

ALTER TABLE [Security].[User] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

GO

GO

GO

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario Creación' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Creación' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario Modificación' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'ModificationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha Modificación' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'ModificationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Token único para la firma electronica del usuario' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'ElectronicSignatureToken'
GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios del sistema con sus credenciales de acceso, roles, grupos y configuración de seguridad. Registra todos los usuarios habilitados para ingresar al ERP/EHR Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del usuario en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la persona asociada al usuario (vincula con el registro de persona o empleado).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o nombre de usuario utilizado para iniciar sesión (login).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol asignado al usuario, define los permisos y accesos dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'RollCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'RollCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de seguridad al que pertenece el usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'GroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'GroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o posición del usuario dentro de la organización.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario (clasificación: interno, externo, administrativo, asistencial, etc.).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario debe cambiar su contraseña en el próximo inicio de sesión.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días permitidos entre cambios de contraseña (política de caducidad de clave).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'DaysChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'DaysChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio de contraseña realizado por el usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'DateLastChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'DateLastChangePassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o expiración de la cuenta de usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'DateExpiryAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'DateExpiryAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña del usuario para acceder al sistema (almacenada de forma cifrada o hasheada).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del usuario (1=activo, 0=inactivo).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario para la integración con Microsoft Lync/Skype Empresarial.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserNameLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserNameLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de inicio de sesión o SIP address para la plataforma Lync/Skype.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'AddressSingInLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'AddressSingInLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña del usuario para la integración con Microsoft Lync/Skype Empresarial.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PasswordLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PasswordLync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota personal o comentario asociado al usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PersonalNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PersonalNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario tiene permiso para visualizar formularios en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ViewForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ViewForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de interfaz o integración extterna asociada al usuario.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'CodeInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'CodeInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la cuenta del usuario está bloqueada por intentos fallidos u otra causa de seguridad.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IsLockedOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IsLockedOut';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número acumulado de intentos fallidos de inicio de sesión con contraseña incorrecta.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'FailedPasswordCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'FailedPasswordCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de perfil del usuario (determina configuración visual o funcional del sistema).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ProfileType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ProfileType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del usuario, usado para notificaciones y recuperación de cuenta.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo de la última modificación del registro, usada para control de concurrencia.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador anterior del usuario, útil para migraciones o trazabilidad histórica.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'OldId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'OldId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token de actualización de sesión para autenticación basada en JWT u OAuth.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'RefreshToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'RefreshToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant o empresa en entornos multiempresa (multi-tenant).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'TenantId';

GO
CREATE NONCLUSTERED INDEX [IX_User_UserCode]
    ON [Security].[User]([UserCode] ASC)
    INCLUDE([IdPerson], [Id], [RollCode]);
