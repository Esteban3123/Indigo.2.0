CREATE TABLE [HumanTalent].[TransferCandidateActivities] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT          NOT NULL,
    [EmployeeCode]                VARCHAR (20) NOT NULL,
    [FormTemplateId]              INT          NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (20) NULL,
    [ModificationDate]            DATETIME     NULL,
    [BossInterview]               TINYINT      NULL,
    [State]                       TINYINT      NULL,
    CONSTRAINT [PK_TransferCandidateActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferCandidateActivities_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id]),
    CONSTRAINT [FK_TransferCandidateActivities_FormsTemplate] FOREIGN KEY ([FormTemplateId]) REFERENCES [HumanTalent].[FormsTemplate] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la actividad del candidato: 1=Registrado, 2=Confirmado. Indica si la actividad de transferencia está en estado inicial o confirmada por el responsable.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1.Registrado 2.Confimar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de entrevista con jefe directo: 1=Sí realizó entrevista, 2=No realizó entrevista. Tinyint que marca si el candidato fue entrevistado por su supervisor o gerente.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'BossInterview';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tuvo entrevista con el Jefe 1.Si, 2.No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'BossInterview';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'BossInterview';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de actividad (DATETIME). Timestamp que registra cuándo fue actualizado por última vez.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que modificó el registro. Usuario responsable de la última actualización de la actividad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de actividad (DATETIME). Timestamp de cuándo se generó inicialmente la actividad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro. Usuario responsable de la generación inicial de la actividad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a FormsTemplate. ID del formulario o plantilla que el candidato debe completar en esta actividad de transferencia.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'FormTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla o formulario que debe realizar al candato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'FormTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'FormTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del empleado (VARCHAR 20) asignado para ejecutar/realizar la actividad con el candidato. Profesional o responsable de la gestión.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'EmployeeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del empleado al que se le trasfiere dicha actividad para que la realice al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'EmployeeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'EmployeeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a CandidatesSelectionProcess. ID que vincula la actividad a un proceso de selección específico del candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de candidato vinculadoa un proceso de selección en especifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la actividad de transferencia del candidato. Clave primaria de la tabla TransferCandidateActivities.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de actividades realizadas durante el proceso de traslado de candidatos en Talento Humano. Guarda el seguimiento de cada candidato en su proceso de selección para transferencia, incluyendo entrevistas con el jefe y el estado actual de la actividad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities';
