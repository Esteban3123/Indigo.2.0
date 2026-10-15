CREATE TABLE [Common].[PersonDiagnosedDisease] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]           INT NOT NULL,
    [DiagnosedDiseaseId] INT NOT NULL,
    CONSTRAINT [PK_PersonDiagnosedDisease__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDiagnosedDisease_DiagnosedDisease] FOREIGN KEY ([DiagnosedDiseaseId]) REFERENCES [Payroll].[DiagnosedDisease] ([Id]),
    CONSTRAINT [FK_PersonDiagnosedDisease_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [UQ_PersonDiagnosedDisease__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);


GO
ALTER TABLE [Common].[PersonDiagnosedDisease] NOCHECK CONSTRAINT [FK_PersonDiagnosedDisease_DiagnosedDisease];


GO
ALTER TABLE [Common].[PersonDiagnosedDisease] NOCHECK CONSTRAINT [FK_PersonDiagnosedDisease_Person];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foranea con enfermedad diagnosticada', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'DiagnosedDiseaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foranea con Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de enfermedades diagnosticadas de la persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'Id';

