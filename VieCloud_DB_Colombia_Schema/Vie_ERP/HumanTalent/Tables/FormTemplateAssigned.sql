CREATE TABLE [HumanTalent].[FormTemplateAssigned] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandiadateSelectionProcessId] INT          NOT NULL,
    [FormsTemplateId]              INT          NOT NULL,
    [Assigned]                     VARCHAR (20) NOT NULL,
    [CreationUser]                 VARCHAR (20) NOT NULL,
    [CreationDate]                 DATETIME     NOT NULL,
    [ModificationUser]             VARCHAR (20) NULL,
    [ModificationDate]             DATETIME     NULL,
    CONSTRAINT [PK_FormTemplateAssigned] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormTemplateAssigned_CandidatesSelectionProcess] FOREIGN KEY ([CandiadateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id]),
    CONSTRAINT [FK_FormTemplateAssigned_FormsTemplate] FOREIGN KEY ([FormsTemplateId]) REFERENCES [HumanTalent].[FormsTemplate] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de asignación de plantilla; DATETIME, auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que realizó la última modificación del registro; VARCHAR(20), auditoría', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de asignación; DATETIME, trazabilidad de auditoría', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que creó el registro de asignación de plantilla; VARCHAR(20), auditoría', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del empleado, delegado o profesional designado para ejecutar, aplicar o administrar la plantilla de prueba al candidato; VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Assigned';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del empleado al que se le trasfiere o asgina realizar dicha actividad o ejecutar dicha plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Assigned';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Assigned';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plantilla de formulario/prueba asignada al empleado delegado para evaluar al candidato en el proceso de selección; INT, FK a FormsTemplate', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la pantilla asignada para que el empleado delegado, le haga la prueba al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proceso de selección de candidatos en el que está vinculado y evaluado el candidato; INT, FK a CandidatesSelectionProcess', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CandiadateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para almacenar el id del del proceso en que se encuentra vinsulado el candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CandiadateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CandiadateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de asignación de plantilla de formulario; INT IDENTITY, auditoría y trazabilidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de plantillas de formularios asignadas a procesos de selección de candidatos en el módulo de Talento Humano. Permite hacer seguimiento de qué formulario fue asignado, por quién y cuándo, dentro de cada proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned';
