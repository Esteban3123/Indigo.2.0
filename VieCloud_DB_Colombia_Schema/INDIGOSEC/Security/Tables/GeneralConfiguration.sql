CREATE TABLE [Security].[GeneralConfiguration] (
    [Id]                  SMALLINT         IDENTITY (1, 1) NOT NULL,
    [HISAssemblyFileName] VARCHAR (50)     NOT NULL,
    [Tenant]              VARCHAR (100)    NULL,
    [SignUpSignIn]        VARCHAR (100)    NULL,
    [ClientId]            UNIQUEIDENTIFIER NULL,
    [RedirectUri]         VARCHAR (300)    NULL,
    [SessionLogoutUrl]    VARCHAR (300)    NULL,
    [AppFunctionURL]      VARCHAR (300)    NULL,
    [FunctionKey1]        VARCHAR (100)    NULL,
    [FunctionKey2]        VARCHAR (100)    NULL,
    [PasswordReset]       VARCHAR (100)    NULL,
    [ZEFLicenseName]      VARCHAR (200)    NULL,
    [ZEFLicenseKey]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_GeneralConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la informacion de configuración para la autenticacion por Azure B2C y relaciona el .EXE del EHR para la carga de los asemblys', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del ejecutable del aplicativo crystal', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'HISAssemblyFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de tenant para autenticación B2C', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'Tenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del flujo de usuario de autenticación B2C', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'SignUpSignIn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de aplicación registrada en Azure', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'ClientId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URI de redirección aceptada al devolver la respuesta de autenticación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'RedirectUri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de cierre de sesión', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'SessionLogoutUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetUserByEmail', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetUserConfigurationByUserId', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del flujo de usuario de autenticación B2C para restablecimiento de contraseña', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'PasswordReset';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la licencia para Z Entity Framework', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'ZEFLicenseName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la licencia para Z Entity Framework', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'GeneralConfiguration', @level2type = N'COLUMN', @level2name = N'ZEFLicenseKey';

