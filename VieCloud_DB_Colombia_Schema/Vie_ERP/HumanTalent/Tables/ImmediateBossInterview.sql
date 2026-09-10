CREATE TABLE [HumanTalent].[ImmediateBossInterview] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT             NOT NULL,
    [ProfileCompliance]           TINYINT         NULL,
    [ProcessContinuation]         TINYINT         NULL,
    [SelectionHead]               TINYINT         NULL,
    [Observation]                 VARCHAR (300)   NULL,
    [Total]                       DECIMAL (18, 2) NULL,
    [CreationUser]                VARCHAR (50)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    [ModificationUser]            VARCHAR (50)    NULL,
    [ModificationDate]            DATETIME        NULL,
    CONSTRAINT [PK_ImmediateBossInterview] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ImmediateBossInterview_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de entrevista con jefe inmediato (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de entrevista (VARCHAR 50, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de entrevista con jefe inmediato (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de entrevista con jefe inmediato (VARCHAR 50, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creaciónd de Usuarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación total de la entrevista con jefe inmediato (DECIMAL 18,2, suma de evaluaciones)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Total';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Total';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y comentarios de la entrevista con jefe inmediato (VARCHAR 300, texto libre)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del candidato según jefe inmediato: 1=Aceptado, 2=Segundo Orden, 3=Descartado (TINYINT, indicador de ranking)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'SelectionHead';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que determina el orden de la selección teniendo como opciones: 1: 1, 2:2 y3:Descartado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'SelectionHead';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'SelectionHead';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de continuidad del candidato en el proceso de selección: 0=No continúa, 1=Continúa (TINYINT, booleano)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProcessContinuation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que determina si el candidato continua o no en el proceso => 0: No y 1:Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProcessContinuation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProcessContinuation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de cumplimiento del perfil requerido: 0=No cumple, 1=Cumple (TINYINT, booleano)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProfileCompliance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que determina si el candidato cumple con el perfil => 0: No y 1:Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProfileCompliance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProfileCompliance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proceso de selección de candidatos (INT, FK a CandidatesSelectionProcess)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de procesos de selección de candidatos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de entrevista con jefe inmediato (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los resultados de la entrevista realizada por el jefe inmediato durante el proceso de selección de un candidato, incluyendo evaluaciones de perfil, continuidad del proceso y puntaje total.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview';
