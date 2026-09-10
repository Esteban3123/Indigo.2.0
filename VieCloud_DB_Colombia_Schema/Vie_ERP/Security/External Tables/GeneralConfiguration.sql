CREATE EXTERNAL TABLE [Security].[GeneralConfiguration] (
    [Id] SMALLINT NOT NULL,
    [HISAssemblyFileName] VARCHAR (50) NOT NULL,
    [Tenant] VARCHAR (100) NULL,
    [SignUpSignIn] VARCHAR (100) NULL,
    [ClientId] UNIQUEIDENTIFIER NULL,
    [RedirectUri] VARCHAR (300) NULL,
    [SessionLogoutUrl] VARCHAR (300) NULL,
    [AppFunctionURL] VARCHAR (300) NULL,
    [FunctionKey1] VARCHAR (100) NULL,
    [FunctionKey2] VARCHAR (100) NULL,
    [PasswordReset] VARCHAR (100) NULL,
    [ZEFLicenseName] VARCHAR (200) NULL,
    [ZEFLicenseKey] UNIQUEIDENTIFIER NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'GeneralConfiguration'
    );

GO