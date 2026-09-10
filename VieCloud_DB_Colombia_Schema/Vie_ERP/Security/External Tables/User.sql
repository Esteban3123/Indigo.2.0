CREATE EXTERNAL TABLE [Security].[User] (
    [Id] INT NOT NULL,
    [IdPerson] INT NOT NULL,
    [UserCode] VARCHAR (20) NOT NULL,
    [RollCode] INT NOT NULL,
    [GroupCode] INT NOT NULL,
    [Position] VARCHAR (150) NULL,
    [UserType] CHAR (1) NULL,
    [ChangePassword] BIT NULL,
    [DaysChangePassword] INT NULL,
    [DateLastChangePassword] DATETIME NULL,
    [DateExpiryAccount] DATETIME NULL,
    [Password] VARCHAR (50) NOT NULL,
    [State] BIT NOT NULL,
    [UserNameLync] VARCHAR (100) NULL,
    [AddressSingInLync] VARCHAR (100) NULL,
    [PasswordLync] VARCHAR (100) NULL,
    [PersonalNote] VARCHAR (100) NULL,
    [ViewForm] BIT NOT NULL,
    [CodeInterface] VARCHAR (12) NULL,
    [IsLockedOut] BIT NOT NULL,
    [FailedPasswordCount] INT NOT NULL,
    [ProfileType] CHAR (1) NULL,
    [Email] VARCHAR (60) NULL,
    [TimeStamp] ROWVERSION NOT NULL,
    [OldId] INT NULL,
    [RefreshToken] VARCHAR (100) NULL,
    [TenantId] INT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'User'
    );

