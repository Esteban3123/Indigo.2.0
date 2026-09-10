CREATE TABLE [HumanTalent].[PVHiringFormTemplateAnswerOptions] (
    [Id]                                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PVHiringFormsTemplateAnswerQuestionsId] INT          NOT NULL,
    [PVHiringAnswerOptionId]                 INT          NOT NULL,
    [Result]                                 INT          NULL,
    [CreationUser]                           VARCHAR (20) NOT NULL,
    [CreationDate]                           DATETIME     NOT NULL,
    [ModificationUser]                       VARCHAR (20) NULL,
    [ModificationDate]                       DATETIME     NULL,
    CONSTRAINT [PK_PVHiringFormTemplateAnswerOptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVHiringFormTemplateAnswerOptions_Pv_HiringsAnswers_Options] FOREIGN KEY ([PVHiringAnswerOptionId]) REFERENCES [HumanTalent].[Pv_HiringsAnswers_Options] ([Id]),
    CONSTRAINT [FK_PVHiringFormTemplateAnswerOptions_PVHiringFormsTemplateAnswerQuestions] FOREIGN KEY ([PVHiringFormsTemplateAnswerQuestionsId]) REFERENCES [HumanTalent].[PVHiringFormsTemplateAnswerQuestions] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de opción de respuesta en el formulario de selección (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que realizó la última modificación del registro (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de opción de respuesta en el formulario de selección (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico asignado a cada opción de respuesta, usado en la evaluación o puntuación del proceso de selección (INT NULL)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de cada opcion ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la opción de respuesta seleccionada por el responsable del proceso de selección o reclutamiento (FK → Pv_HiringsAnswers_Options.Id, INT NOT NULL)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringAnswerOptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de las opciones que cada respuesta, seleccionada por la persona encargada del proceso de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringAnswerOptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringAnswerOptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la pregunta plantilla (cabecera) a la cual pertenece esta opción de respuesta (FK → PVHiringFormsTemplateAnswerQuestions.Id, INT NOT NULL)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringFormsTemplateAnswerQuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringFormsTemplateAnswerQuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringFormsTemplateAnswerQuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de opción de respuesta en el formulario de selección (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opciones de respuesta asignadas a cada pregunta dentro de las plantillas de formularios de contratación de talento humano. Relaciona cada pregunta con sus posibles respuestas y registra el resultado o puntaje asociado a cada opción.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions';
