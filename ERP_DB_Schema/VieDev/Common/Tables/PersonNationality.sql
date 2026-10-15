CREATE TABLE [Common].[PersonNationality] (
    [Id]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CountryId] INT NOT NULL,
    [PersonId]  INT NOT NULL,
    [State]     BIT NOT NULL,
    CONSTRAINT [PK_PersonNationality] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonNationality_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Common].[Country] ([Id]),
    CONSTRAINT [FK_PersonNationality_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[PersonNationality] NOCHECK CONSTRAINT [FK_PersonNationality_Country_CountryId];


GO
ALTER TABLE [Common].[PersonNationality] NOCHECK CONSTRAINT [FK_PersonNationality_Person_PersonId];

