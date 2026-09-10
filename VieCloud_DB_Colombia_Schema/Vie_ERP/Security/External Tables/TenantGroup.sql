CREATE EXTERNAL TABLE [Security].[TenantGroup] (
    [Id] INT NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [GroupId] INT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'TenantGroup'
    );

GO