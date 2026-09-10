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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME), marca de auditoría de cuándo se actualizó la evaluación de competencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (VARCHAR 50), identificación de quién realizó la última actualización', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME), marca de auditoría de cuándo se registró la competencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador del registro (VARCHAR 50), identificación de quién registró la competencia en la entrevista', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción de nivel alcanzado (VARCHAR 2), evaluación del desempeño en la competencia (ej: A1, A2, B1, B2)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'LevelOption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opción de nivel', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'LevelOption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'LevelOption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la competencia evaluada en la entrevista psicológica, código único de la competencia laboral/profesional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CompetitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identidicacion de la competencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CompetitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'CompetitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entrevista psicológica (FK), referencia a PsychologicalInterview.Id, vincula cada competencia evaluada a su entrevista', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'PsychologicalInterviewId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Entrevista Psicológica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'PsychologicalInterviewId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'PsychologicalInterviewId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la relación competencia-entrevista psicológica, clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de competencias evaluadas en entrevistas psicológicas del proceso de selección de talento humano. Vincula cada entrevista psicológica con las competencias valoradas y el nivel alcanzado por el candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPsychologicalInterviewCompetences';
