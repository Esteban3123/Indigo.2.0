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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del deporte o actividad física practicada por la persona. Referencia a tabla Payroll.SportPractice. Tipo INT. Vincula la práctica deportiva específica al registro de la persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'SportPracticeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foranea a tabla Deporte Practicado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'SportPracticeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'SportPracticeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la persona que practica el deporte. Referencia a tabla Common.Person (cédula, documento, identificación del paciente, profesional o empleado). Tipo INT. Clave única por persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foranea a Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementado (INT IDENTITY) del registro de relación entre persona y deporte practicado. Clave primaria de la tabla. Generado automáticamente en SQL Server.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de deportes practicados', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona a cada persona con los deportes o actividades físicas que practica. Permite registrar múltiples disciplinas deportivas asociadas a un mismo individuo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonSportPractice';
