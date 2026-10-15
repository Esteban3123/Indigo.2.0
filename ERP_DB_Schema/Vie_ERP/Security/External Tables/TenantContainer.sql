CREATE EXTERNAL TABLE [Security].[TenantContainer] (
    [Id] BIGINT NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [ContainerId] INT NOT NULL,
    [Principal] BIT NOT NULL,
    [State] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'TenantContainer'
    );

GO