CREATE TABLE [Common].[FilePerson] (
    [Id]           UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [IdPerson]     INT              NOT NULL,
    [Photo]        VARBINARY (MAX)  NULL,
    [Signature]    VARBINARY (MAX)  NULL,
    [TimeStamp]    ROWVERSION       NOT NULL,
    [State]        BIT              NOT NULL,
    [Synchronized] CHAR (1)         NOT NULL,
    CONSTRAINT [PK_FilesPerson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilesPerson_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[FilePerson] NOCHECK CONSTRAINT [FK_FilesPerson_Person];

