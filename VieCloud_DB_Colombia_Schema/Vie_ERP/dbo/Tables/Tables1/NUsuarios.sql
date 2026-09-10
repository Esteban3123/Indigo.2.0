CREATE TABLE [dbo].[NUsuarios] (
    [Id]                     NVARCHAR (255) NULL,
    [IdPerson]               NVARCHAR (255) NULL,
    [UserCode]               NVARCHAR (255) NULL,
    [RollCode]               NVARCHAR (255) NULL,
    [GroupCode]              NVARCHAR (255) NULL,
    [Position]               NVARCHAR (255) NULL,
    [UserType]               NVARCHAR (255) NULL,
    [ChangePassword]         NVARCHAR (255) NULL,
    [DaysChangePassword]     NVARCHAR (255) NULL,
    [DateLastChangePassword] NVARCHAR (255) NULL,
    [DateExpiryAccount]      NVARCHAR (255) NULL,
    [TimeStamp]              NVARCHAR (255) NULL,
    [Password]               NVARCHAR (255) NULL,
    [State]                  NVARCHAR (255) NULL,
    [UserNameLync]           NVARCHAR (255) NULL,
    [AddressSingInLync]      NVARCHAR (255) NULL,
    [PasswordLync]           NVARCHAR (255) NULL,
    [PersonalNote]           NVARCHAR (255) NULL,
    [ViewForm]               NVARCHAR (255) NULL,
    [CodeInterface]          NVARCHAR (255) NULL,
    [IsLockedOut]            NVARCHAR (255) NULL,
    [FailedPasswordCount]    NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos fallidos (clave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'FailedPasswordCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'IsLockedOut';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'CodeInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'ViewForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota personal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'PersonalNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave Lync', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'PasswordLync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electronico de Inicio sesión en Lync', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'AddressSingInLync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario Lync', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'UserNameLync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de caducidad de la cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'DateExpiryAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último cambio de contraseña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'DateLastChangePassword';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de cambio de la clave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'DaysChangePassword';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cambio de clave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'ChangePassword';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'UserType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'GroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'RollCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla persona', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NUsuarios', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de gestión de usuarios del sistema que almacena credenciales de acceso (contraseña, política de cambio y días de vigencia), control de seguridad (bloqueo de cuenta, intentos fallidos, fecha de expiración) y asignación de permisos mediante rol, grupo y tipo de usuario. Cada registro se vincula a una persona a través de `IdPerson` y admite integración con Microsoft Lync mediante credenciales y dirección de inicio de sesión propias. También registra el cargo, formulario de vista preferido y código de interfaz asignado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NUsuarios';
GO
