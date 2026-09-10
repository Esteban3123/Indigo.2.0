CREATE TABLE [Security].[Person] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Identification]     VARCHAR (15)    NOT NULL,
    [IdentificationType] SMALLINT        NOT NULL,
    [FirstName]          VARCHAR (50)    NOT NULL,
    [SecondName]         VARCHAR (50)    NULL,
    [FirstLastName]      VARCHAR (50)    NOT NULL,
    [SecondLastName]     VARCHAR (50)    NULL,
    [Fullname]           VARCHAR (250)   NOT NULL,
    [BirthDay]           DATETIME        NULL,
    [Fingerprint]        VARBINARY (MAX) NULL,
    [Gender]             SMALLINT        NOT NULL,
    [State]              BIT             NOT NULL,
    CONSTRAINT [PK_Person_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Person] UNIQUE NONCLUSTERED ([Identification] ASC)
);

