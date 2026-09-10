CREATE EXTERNAL TABLE [Security].[Phone] (
    [Id] INT NOT NULL,
    [IdPerson] INT NOT NULL,
    [Phone] VARCHAR (15) NOT NULL,
    [IdPhoneType] SMALLINT NOT NULL,
    [State] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Phone'
    );

GO