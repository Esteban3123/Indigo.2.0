CREATE EXTERNAL TABLE [Security].[TenantRoll] (
    [Id] INT NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [RollId] INT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'TenantRoll'
    );

GO