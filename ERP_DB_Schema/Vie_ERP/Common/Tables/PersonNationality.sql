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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del País', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'CountryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'Id';

