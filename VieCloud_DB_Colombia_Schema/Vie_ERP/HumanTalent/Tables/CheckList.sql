CREATE TABLE [HumanTalent].[CheckList] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT          NOT NULL,
    [CheckListDocumentsId]        INT          NOT NULL,
    [Verification]                TINYINT      NULL,
    [Observation]                 VARCHAR (50) NULL,
    [CreationUser]                VARCHAR (50) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (50) NULL,
    [ModificationDate]            DATETIME     NULL,
    CONSTRAINT [PK_CheckList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckList_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id]),
    CONSTRAINT [FK_CheckList_CheckListDocuments] FOREIGN KEY ([CheckListDocumentsId]) REFERENCES [HumanTalent].[CheckListDocuments] ([Id])
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación (DATETIME, NULL). Timestamp de la última actualización del registro de verificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó por última vez el checklist (VARCHAR 50, NULL). Identificación del responsable de la última actualización del registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME, NOT NULL). Timestamp del momento en que se generó el checklist de verificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró el checklist (VARCHAR 50, NOT NULL). Identificación del responsable que creó el registro de verificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o comentarios adicionales sobre la verificación del documento (VARCHAR 50, NULL). Campo para registrar notas, hallazgos o aclaraciones del proceso de validación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciónes', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Observation';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de verificación del documento (TINYINT NULL): 1=Sí (verificado/conforme), 2=No (no verificado/falta), 3=N/A (no aplica). Indica si el documento cumple con los requisitos.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Verification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para seleccionar la verificacion : 1=Si, 2=No, 3= N/A', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Verification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Verification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proceso de selección de candidatos (FK CandidatesSelectionProcess). Referencia al proceso de reclutamiento y evaluación del candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de procesos de selección de candidatos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de checklist de verificación de candidatos.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de verificación de documentos del checklist en el proceso de selección de candidatos de talento humano. Indica qué documentos fueron revisados, si fueron verificados y las observaciones correspondientes.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList';
