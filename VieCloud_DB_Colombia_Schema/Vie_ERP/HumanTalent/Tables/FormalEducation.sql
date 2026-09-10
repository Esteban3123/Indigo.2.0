CREATE TABLE [HumanTalent].[FormalEducation] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]           INT          NULL,
    [EducationLevelId]      INT          NOT NULL,
    [State]                 TINYINT      NOT NULL,
    [DateStart]             DATE         NOT NULL,
    [DateEnd]               DATE         NULL,
    [AcademicInformationId] INT          NOT NULL,
    [ExternEntitiesId]      INT          NOT NULL,
    [ProfessionalCard]      VARCHAR (20) NULL,
    [Schedule]              INT          NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [EmployeeId]            INT          NULL,
    CONSTRAINT [PK__FormalEd__3214EC07F042BBA9] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_AcademicInformationIdFE] FOREIGN KEY ([AcademicInformationId]) REFERENCES [HumanTalent].[AcademicInformation] ([id]),
    CONSTRAINT [fk_EducationLevelIdFE] FOREIGN KEY ([EducationLevelId]) REFERENCES [HumanTalent].[EducationLevel] ([Id]),
    CONSTRAINT [fk_ExternEntitiesIdFE] FOREIGN KEY ([ExternEntitiesId]) REFERENCES [StaffPick].[ExternEntities] ([Id]),
    CONSTRAINT [fk_FICandidateIdFE] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id]),
    CONSTRAINT [FK_FormalEducation_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del empleado asociado a este registro de formación formal; FK a tabla Employee en módulo Payroll', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro; tipo DATETIME, auditoria, puede ser nula', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro; VARCHAR(20), auditoria, puede ser nulo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro; tipo DATETIME, auditoria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de formación académica; VARCHAR(20), auditoria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cronograma o jornada de estudio (diurno, nocturno, fin de semana); INT tipo enumerado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Schedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cronograma', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Schedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Schedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta profesional o cédula profesional expedida; VARCHAR(20), PII_Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarjeta Profesional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la entidad externa prestadora de educación (universidad, escuela, instituto); FK a tabla ExternEntities', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ExternEntitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de las entidades externas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ExternEntitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ExternEntitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la información académica asociada (institución, programa, título); FK a tabla AcademicInformation', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de la  información académica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización o conclusión de la formación académica formal; tipo DATE, puede ser nula', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de finalización', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la formación académica formal; tipo DATE', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Registrado, 2=Confirmado; TINYINT de auditoría', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1.Registrado 2.Confimar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del nivel de educación alcanzado (primaria, secundaria, técnico, profesional, posgrado); FK a tabla EducationLevel', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de nivel de educación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del candidato vinculado a este registro de educación formal; FK a tabla Candidate', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de formación académica formal en la tabla FormalEducation', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formación académica formal de candidatos y empleados: registra estudios realizados (bachillerato, técnico, tecnólogo, universitario, posgrado), incluyendo institución, nivel educativo, fechas y tarjeta profesional. Usada en gestión de talento humano, hojas de vida y perfiles de cargo.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation';
