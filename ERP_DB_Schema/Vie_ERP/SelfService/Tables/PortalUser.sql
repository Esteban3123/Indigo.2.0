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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña Aleatoria (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RandomPassword';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualizar Token', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'RefreshToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario bloqueado', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'LockedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña de Usuario del portal', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUser', @level2type = N'COLUMN', @level2name = N'Id';

