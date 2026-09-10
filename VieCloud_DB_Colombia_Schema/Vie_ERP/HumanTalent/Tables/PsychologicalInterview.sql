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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de entrevista psicológica; DATETIME, auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación; VARCHAR(50), auditoría de cambios, PII', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de entrevista psicológica; DATETIME, auditoría', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; VARCHAR(50), auditoría, PII', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o calificación numérica asignado en la entrevista psicológica; DECIMAL(18,2), rango evaluación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Score';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'puntaje asignado para la entrevista psicologica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Score';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Características, comportamientos y atributos personales del candidato identificados en la entrevista; VARCHAR(800), análisis psicológico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidatesCharacteristics';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para ingresar las caracteristicas del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidatesCharacteristics';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidatesCharacteristics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proyectos, metas y planes a futuro del candidato expresados durante la entrevista; VARCHAR(800), aspiraciones profesionales', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FutureProjects';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para ingresar los proyectos a futuro del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FutureProjects';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FutureProjects';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aspectos de personalidad, áreas de mejora y desarrollo detectados en la evaluación psicológica; VARCHAR(800), recomendaciones de desarrollo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'PersonalityAspects';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para ingresar los aspectos de personalidad que debe de mejorar el candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'PersonalityAspects';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'PersonalityAspects';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fortalezas, competencias y habilidades positivas identificadas del candidato; VARCHAR(800), análisis de capacidades', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Strengths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para ingresar las mejores fortalezas del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Strengths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Strengths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Composición, estructura y dinámica del grupo familiar del candidato entrevistado; VARCHAR(800), contexto social', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FamilyConformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para ingresar la conformación del frupo familiar del candidato entrevistado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FamilyConformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'FamilyConformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proceso de selección de candidatos al cual pertenece esta entrevista; INT, FK a CandidatesSelectionProcess', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de procesos de selección del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entrevista psicológica; INT IDENTITY, clave primaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los resultados de la entrevista psicológica realizada a un candidato durante el proceso de selección de talento humano. Contiene información sobre la conformación familiar, fortalezas, rasgos de personalidad, proyectos a futuro, características del candidato y el puntaje obtenido en la evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PsychologicalInterview';
