CREATE EXTERNAL TABLE [Security].[Suscriptions] (
    [Id] INT NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [ProductCatalogId] SMALLINT NOT NULL,
    [SuscriptionID] UNIQUEIDENTIFIER NOT NULL,
    [TypeOfLicensing] TINYINT NOT NULL,
    [NumberOfUsers] SMALLINT NULL,
    [NumberOfDevices] SMALLINT NULL,
    [BTIuV1Id] SMALLINT NULL,
    [BTIuV2Id] SMALLINT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Suscriptions'
    );

GO

