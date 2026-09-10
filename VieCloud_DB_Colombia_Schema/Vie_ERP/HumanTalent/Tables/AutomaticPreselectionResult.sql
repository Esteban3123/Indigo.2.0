CREATE TABLE [HumanTalent].[AutomaticPreselectionResult] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT          NOT NULL,
    [AcademicInformation]         TINYINT      NOT NULL,
    [Position]                    TINYINT      NOT NULL,
    [Sector]                      TINYINT      NOT NULL,
    [ExperienceMonths]            TINYINT      NOT NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (20) NULL,
    [ModificationDate]            DATETIME     NULL,
    [Result]                      INT          NULL,
    [Area]                        TINYINT      NULL,
    CONSTRAINT [PK_AutomaticPreselectionResult] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AutomaticPreselectionResult_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área funcional del candidato: cumplimiento con el cargo (1=Sí cumple, 2=No cumple). Vinculado a unidad funcional, centro de atención, especialidad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Candidato Cumple con el cargo 1.Si, 2.No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado final de preselección automática del candidato (INT): puntuación o estado de aceptación/rechazo tras evaluación de criterios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del registro de preselección automática.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que realizó la última modificación del registro de preselección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de preselección automática del candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que creó el registro de preselección automática.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de experiencia laboral del candidato vs. perfil del cargo (TINYINT 0-1): 0=No cumple (0%), 1=Cumple 25%. Meses de experiencia requerida.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ExperienceMonths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la compración la experiencia del  candidato y el perfil  del cargo: 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ExperienceMonths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ExperienceMonths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de sector económico/industria del candidato vs. perfil del cargo (TINYINT 0-1): 0=No cumple (0%), 1=Cumple 25%. Sector de desempeño previo.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Sector';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la compración entre el cargo del  candidato y el perfil del cargo : 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Sector';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Sector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de cargo anterior del candidato vs. perfil del cargo solicitado (TINYINT 0-1): 0=No cumple (0%), 1=Cumple 25%. Alineación de puesto.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la compración entre el cargo del  candidato y el perfil del cargo : 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Position';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de formación académica del candidato vs. perfil del cargo (TINYINT 0-1): 0=No cumple (0%), 1=Cumple 25%. Titulación, certificaciones, educación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'AcademicInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la compración entre la formación academica del  candidato y el perfil del cargo: 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'AcademicInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'AcademicInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del proceso de selección asociado. Referencia a CandidatesSelectionProcess. Vincula candidato a convocatoria.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de candidato asociado a un proceso de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de resultado de preselección automática. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de la preselección automática de candidatos en procesos de selección de personal. Registra los puntajes obtenidos por cada aspirante en criterios como formación académica, cargo, sector, experiencia y área, junto con el resultado final del proceso automatizado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult';
