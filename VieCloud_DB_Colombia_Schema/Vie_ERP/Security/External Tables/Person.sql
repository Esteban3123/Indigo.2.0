CREATE EXTERNAL TABLE [Security].[Person] (
    [Id] INT NOT NULL,
    [Identification] VARCHAR (15) NOT NULL,
    [IdentificationType] SMALLINT NOT NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    [SecondName] VARCHAR (50) NULL,
    [FirstLastName] VARCHAR (50) NOT NULL,
    [SecondLastName] VARCHAR (50) NULL,
    [Fullname] VARCHAR (250) NOT NULL,
    [BirthDay] DATETIME NULL,
    [Fingerprint] VARBINARY (MAX) NULL,
    [Gender] SMALLINT NOT NULL,
    [State] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Person'
    );

