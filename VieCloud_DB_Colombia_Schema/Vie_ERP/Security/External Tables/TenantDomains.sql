CREATE EXTERNAL TABLE [Security].[TenantDomains] (
    [Id] TINYINT NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [DomainName] VARCHAR (100) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'TenantDomains'
    );

GO