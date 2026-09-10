CREATE TABLE [HumanTalent].[PVFormTemplateAnswerOptions] (
    [Id]                               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PVFormsTemplateAnswerQuestionsId] INT          NOT NULL,
    [PVAnswerOptionId]                 INT          NOT NULL,
    [Result]                           INT          NULL,
    [CreationUser]                     VARCHAR (20) NOT NULL,
    [CreationDate]                     DATETIME     NOT NULL,
    [ModificationUser]                 VARCHAR (20) NULL,
    [ModificationDate]                 DATETIME     NULL,
    CONSTRAINT [PK_PVFormTemplateAnswerOptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVFormTemplateAnswerOptions_PVAnswerOption1] FOREIGN KEY ([PVAnswerOptionId]) REFERENCES [HumanTalent].[PVAnswerOption] ([Id]),
    CONSTRAINT [FK_PVFormTemplateAnswerOptions_PVFormsTemplateAnswerQuestions] FOREIGN KEY ([PVFormsTemplateAnswerQuestionsId]) REFERENCES [HumanTalent].[PVFormsTemplateAnswerQuestions] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) de la última modificación; registra cuándo se actualizó esta opción de respuesta en la plantilla de evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que realizó la última modificación del registro; auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) de creación del registro; permite trazabilidad temporal de cuándo se registró esta opción en la plantilla.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que creó el registro; auditoría de origen del dato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico asignado a esta opción de respuesta; representa puntaje, peso o resultado de la alternativa seleccionada en la evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de cada opcion ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la opción de respuesta disponible (FK); referencia el catálogo de opciones seleccionadas por el evaluador o responsable del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVAnswerOptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de las opciones que cada respuesta, seleccionada por la persona encargada del proceso de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVAnswerOptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVAnswerOptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la pregunta padre (FK) en la plantilla de formulario; vincula esta opción a su pregunta raíz de evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVFormsTemplateAnswerQuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVFormsTemplateAnswerQuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVFormsTemplateAnswerQuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de opción de respuesta en plantilla de formulario de selección/evaluación de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opciones de respuesta asociadas a cada pregunta de una plantilla de evaluación o formulario en el módulo de Talento Humano. Relaciona cada pregunta con sus posibles respuestas y registra el resultado o puntaje correspondiente a cada opción seleccionada.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions';
