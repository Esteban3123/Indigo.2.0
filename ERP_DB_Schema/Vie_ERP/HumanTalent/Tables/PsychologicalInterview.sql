CREATE TABLE [HumanTalent].[PsychologicalInterview] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT             NOT NULL,
    [FamilyConformation]          VARCHAR (800)   NULL,
    [Strengths]                   VARCHAR (800)   NULL,
    [PersonalityAspects]          VARCHAR (800)   NULL,
    [FutureProjects]              VARCHAR (800)   NULL,
    [CandidatesCharacteristics]   VARCHAR (800)   NULL,
    [Score]                       DECIMAL (18, 2) NULL,
    [CreationUser]                VARCHAR (50)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    [ModificationUser]            VARCHAR (50)    NULL,
    [ModificationDate]            DATETIME        NULL,
    CONSTRAINT [PK_PsychologicalInterview] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PsychologicalInterview_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PsychologicalInterview] NOCHECK CONSTRAINT [FK_PsychologicalInterview_CandidatesSelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'puntaje asignado para la entrevista psicologica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para ingresar las caracteristicas del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidatesCharacteristics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para ingresar los proyectos a futuro del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FutureProjects';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para ingresar los aspectos de personalidad que debe de mejorar el candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'PersonalityAspects';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para ingresar las mejores fortalezas del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Strengths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para ingresar la conformación del frupo familiar del candidato entrevistado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FamilyConformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de procesos de selección del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Id';

