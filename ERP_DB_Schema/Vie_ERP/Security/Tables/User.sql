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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de usuario:  0:StandardUser - 1: Administrador Empresa - 2: Administrador Tenant - 3: Administrador Global - 4: Interno QA' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UserType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define el Tipo de Perfil: 1= Administrativo 2= Asistencial' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'ProfileType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo electronico, debe ser unico por usuario' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.' , @level0type=N'SCHEMA',@level0name=N'Security', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'TimeStamp'
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