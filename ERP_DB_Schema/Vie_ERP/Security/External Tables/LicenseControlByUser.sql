CREATE EXTERNAL TABLE [Security].[LicenseControlByUser] (
    [Id] INT NOT NULL,
    [SuscriptionId] INT NOT NULL,
    [TenantUserId] INT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'LicenseControlByUser'
    );

GO