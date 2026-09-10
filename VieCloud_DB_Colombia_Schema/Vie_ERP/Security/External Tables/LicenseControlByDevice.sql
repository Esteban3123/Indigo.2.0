CREATE EXTERNAL TABLE [Security].[LicenseControlByDevice] (
    [Id] INT NOT NULL,
    [SuscriptionId] INT NOT NULL,
    [DeviceMAC] VARCHAR (200) NOT NULL,
    [DeviceName] VARCHAR (200) NOT NULL,
    [DeviceIPV4] VARCHAR (50) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'LicenseControlByDevice'
    );

GO