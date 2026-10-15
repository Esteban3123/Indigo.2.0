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
ALTER TABLE [HumanTalent].[FormalEducation] NOCHECK CONSTRAINT [fk_AcademicInformationIdFE];


GO
ALTER TABLE [HumanTalent].[FormalEducation] NOCHECK CONSTRAINT [fk_EducationLevelIdFE];


GO
ALTER TABLE [HumanTalent].[FormalEducation] NOCHECK CONSTRAINT [fk_ExternEntitiesIdFE];


GO
ALTER TABLE [HumanTalent].[FormalEducation] NOCHECK CONSTRAINT [fk_FICandidateIdFE];


GO
ALTER TABLE [HumanTalent].[FormalEducation] NOCHECK CONSTRAINT [FK_FormalEducation_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cronograma', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Schedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarjeta Profesional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de las entidades externas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'ExternEntitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la  información académica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'DateStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado 1.Registrado 2.Confimar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de nivel de educación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormalEducation', @level2type = N'COLUMN', @level2name = N'Id';

