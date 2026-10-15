CREATE TABLE [Common].[PersonSportPractice] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]        INT NOT NULL,
    [SportPracticeId] INT NOT NULL,
    CONSTRAINT [PK_PersonSportPractice__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonSportPractice_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_PersonSportPractice_SportPractice] FOREIGN KEY ([SportPracticeId]) REFERENCES [Payroll].[SportPractice] ([Id]),
    CONSTRAINT [IX_SportPractice] UNIQUE NONCLUSTERED ([PersonId] ASC),
    CONSTRAINT [UQ_PersonSportPractice__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);


GO
ALTER TABLE [Common].[PersonSportPractice] NOCHECK CONSTRAINT [FK_PersonSportPractice_Person];


GO
ALTER TABLE [Common].[PersonSportPractice] NOCHECK CONSTRAINT [FK_PersonSportPractice_SportPractice];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foranea a tabla Deporte Practicado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'SportPracticeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foranea a Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de deportes practicados', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'Id';

