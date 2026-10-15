CREATE TABLE [HumanTalent].[PvPsychologicalInterviewCompetences] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PsychologicalInterviewId] INT          NOT NULL,
    [CompetitionId]            INT          NOT NULL,
    [LevelOption]              VARCHAR (2)  NOT NULL,
    [CreationUser]             VARCHAR (50) NOT NULL,
    [CreationDate]             DATETIME     NULL,
    [ModificationUser]         VARCHAR (50) NULL,
    [ModificationDate]         DATETIME     NULL,
    CONSTRAINT [PK_PvPsychologicalInterviewCompetences] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvPsychologicalInterviewCompetences_PsychologicalInterview] FOREIGN KEY ([PsychologicalInterviewId]) REFERENCES [HumanTalent].[PsychologicalInterview] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PvPsychologicalInterviewCompetences] NOCHECK CONSTRAINT [FK_PvPsychologicalInterviewCompetences_PsychologicalInterview];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción de nivel', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'LevelOption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidicacion de la competencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CompetitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Entrevista Psicológica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'PsychologicalInterviewId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'Id';

