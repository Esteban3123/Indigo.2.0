CREATE EXTERNAL TABLE [Security].[Tenant] (
    [Id] SMALLINT NOT NULL,
    [Name] VARCHAR (200) NOT NULL,
    [Status] TINYINT NOT NULL,
    [WorkFlowStatus] TINYINT NOT NULL,
    [CompanyType] TINYINT NOT NULL,
    [CompanyNit] VARCHAR (15) NOT NULL,
    [RepresentationLegal] VARCHAR (100) NOT NULL,
    [CountryId] TINYINT NOT NULL,
    [City] VARCHAR (50) NOT NULL,
    [Address] VARCHAR (50) NOT NULL,
    [Telephone] VARCHAR (20) NOT NULL,
    [KeyCode] VARCHAR (20) NOT NULL,
    [CloudType] TINYINT NOT NULL,
    [AuthenticationType] TINYINT NOT NULL,
    [TimeStamp] ROWVERSION NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Tenant'
    );

GO