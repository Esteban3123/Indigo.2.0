CREATE TABLE [HumanTalent].[PVImmediateBossInterviewsCompetition] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ImmediateBossInterviewId] INT             NOT NULL,
    [CompetitionId]            INT             NULL,
    [TechnicalKnowledge]       INT             NULL,
    [Score]                    DECIMAL (18, 2) NULL,
    [CreationUser]             VARCHAR (50)    NOT NULL,
    [CreationDate]             DATETIME        NOT NULL,
    [ModificationUser]         VARCHAR (50)    NULL,
    [ModificationDate]         DATETIME        NULL,
    CONSTRAINT [PK_PVImmediateBossInterviewsCompetition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVImmediateBossInterviewsCompetition_Competition] FOREIGN KEY ([CompetitionId]) REFERENCES [Learning].[Competition] ([Id]),
    CONSTRAINT [FK_PVImmediateBossInterviewsCompetition_ImmediateBossInterview] FOREIGN KEY ([ImmediateBossInterviewId]) REFERENCES [HumanTalent].[ImmediateBossInterview] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de evaluación de competencia en entrevista con jefe inmediato (DATETIME)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro; identificación del profesional de recursos humanos o evaluador (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de evaluación de competencia en entrevista (DATETIME)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; identificación del jefe inmediato o evaluador (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o calificación numérica asignado a la competencia evaluada en la entrevista, rango decimal 0-100 (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Score';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Score';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel o puntuación de conocimientos técnicos evaluados durante la entrevista con el jefe inmediato (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'TechnicalKnowledge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conocimientos técnicos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'TechnicalKnowledge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'TechnicalKnowledge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la competencia evaluada; clave foránea a tabla Learning.Competition para vinculación con catálogo de competencias (FK INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CompetitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la competencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CompetitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CompetitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entrevista con jefe inmediato; clave foránea a tabla HumanTalent.ImmediateBossInterview para trazabilidad de evaluación (FK INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ImmediateBossInterviewId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entrevista con el jefe inmediato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ImmediateBossInterviewId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ImmediateBossInterviewId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) del registro de evaluación de competencia en entrevista (INT PK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de competencias evaluadas durante las entrevistas realizadas por el jefe inmediato en los procesos de selección de talento humano. Almacena el puntaje y conocimiento técnico asignado a cada competencia valorada por competidor o candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition';
