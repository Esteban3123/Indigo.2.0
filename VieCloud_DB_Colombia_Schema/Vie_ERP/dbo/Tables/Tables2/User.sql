CREATE TABLE [dbo].[User] (
    [Id]                     INT           NOT NULL,
    [IdPerson]               INT           NOT NULL,
    [UserCode]               VARCHAR (20)  NOT NULL,
    [RollCode]               INT           NOT NULL,
    [GroupCode]              INT           NOT NULL,
    [Position]               VARCHAR (30)  NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla central de usuarios del sistema que almacena credenciales de acceso (contraseña, token de refresco), configuración de seguridad (bloqueo de cuenta, intentos fallidos, expiración de contraseña) y asociación con una persona mediante `IdPerson`. Incluye atributos de rol, grupo y tipo de perfil para control de acceso, así como campos de integración con Lync/Skype Empresarial y soporte multi-tenant mediante `TenantId`.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'User';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'User';
GO
