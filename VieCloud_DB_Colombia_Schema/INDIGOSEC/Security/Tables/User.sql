CREATE TABLE [Security].[User] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPerson]                 INT           NOT NULL,
    [UserCode]                 VARCHAR (20)  NOT NULL,
    [RollCode]                 INT           NOT NULL,
    [GroupCode]                INT           NOT NULL,
    [Position]                 VARCHAR (150) NULL,
    [UserType]                 CHAR (1)      NULL,
    [ChangePassword]           BIT           NULL,
    [DaysChangePassword]       INT           NULL,
    [DateLastChangePassword]   DATETIME      NULL,
    [DateExpiryAccount]        DATETIME      NULL,
    [Password]                 VARCHAR (50)  NOT NULL,
    [State]                    BIT           NOT NULL,
    [UserNameLync]             VARCHAR (100) NULL,
    [AddressSingInLync]        VARCHAR (100) NULL,
    [PasswordLync]             VARCHAR (100) NULL,
    [PersonalNote]             VARCHAR (100) NULL,
    [ViewForm]                 BIT           NOT NULL,
    [CodeInterface]            VARCHAR (12)  NULL,
    [IsLockedOut]              BIT           CONSTRAINT [DF_User_IsLockedOut] DEFAULT ((0)) NOT NULL,
    [FailedPasswordCount]      INT           CONSTRAINT [DF_User_FailedPasswordCount] DEFAULT ((0)) NOT NULL,
    [ProfileType]              CHAR (1)      NULL,
    [Email]                    VARCHAR (60)  NULL,
    [TimeStamp]                ROWVERSION    NOT NULL,
    [OldId]                    INT           NULL,
    [RefreshToken]             VARCHAR (100) NULL,
    [TenantId]                 SMALLINT      NULL,
    [CreationUser]             VARCHAR (20)  DEFAULT ((999)) NOT NULL,
    [CreationDate]             DATETIME      DEFAULT (getdate()) NOT NULL,
    [ModificationUser]         VARCHAR (20)  NULL,
    [ModificationDate]         DATETIME      NULL,
    [ElectronicSignatureToken] VARCHAR (100) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_SEGUSUARU] UNIQUE NONCLUSTERED ([IdPerson] ASC, [Email] ASC),
    CONSTRAINT [IX_UserCode] UNIQUE NONCLUSTERED ([UserCode] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Email_IdPerson_ProfileType_UserCode_UserType]
    ON [Security].[User]([Email] ASC)
    INCLUDE([IdPerson], [ProfileType], [UserCode], [UserType]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario:  0:StandardUser - 1: Administrador Empresa - 2: Administrador Tenant - 3: Administrador Global', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el Tipo de Perfil: 1= Administrativo 2= Asistencial', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ProfileType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electronico, debe ser unico por usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token único para la firma electronica del usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ElectronicSignatureToken';

