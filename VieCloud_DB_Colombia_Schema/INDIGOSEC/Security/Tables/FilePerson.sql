CREATE TABLE [Security].[FilePerson] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [IdPerson]  INT             NOT NULL,
    [Photo]     VARBINARY (MAX) NULL,
    [Signature] VARBINARY (MAX) NULL,
    [TimeStamp] ROWVERSION      NOT NULL,
    [State]     BIT             NOT NULL,
    CONSTRAINT [PK_FilesPerson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilePerson_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Security].[Person] ([Id]),
    CONSTRAINT [UQ__INFILEPR__3214EC060DA4EB0F] UNIQUE NONCLUSTERED ([Id] ASC)
);

