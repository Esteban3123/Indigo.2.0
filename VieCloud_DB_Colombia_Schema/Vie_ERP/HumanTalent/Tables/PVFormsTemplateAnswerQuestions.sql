CREATE TABLE [HumanTalent].[PVFormsTemplateAnswerQuestions] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FormsTemplateAnswersId] INT             NOT NULL,
    [QuestionsId]            INT             NULL,
    [AnswerDescription]      VARCHAR (200)   NULL,
    [QuestionsValue]         DECIMAL (18, 2) NULL,
    [Result]                 DECIMAL (18, 2) NOT NULL,
    [Detail]                 VARCHAR (120)   NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    CONSTRAINT [PK_PVFormsTemplateAnswerQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVFormsTemplateAnswerQuestions_FormsTemplateAnswers] FOREIGN KEY ([FormsTemplateAnswersId]) REFERENCES [HumanTalent].[FormsTemplateAnswers] ([Id]),
    CONSTRAINT [FK_PVFormsTemplateAnswerQuestions_Questions] FOREIGN KEY ([QuestionsId]) REFERENCES [HumanTalent].[Questions] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del registro; NULL si sin cambios posteriores; trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario (VARCHAR 20) que modifica el registro; NULL si sin cambios posteriores.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de respuesta a pregunta; timestamp de auditoría inicial.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario (VARCHAR 20) que crea el registro de respuesta; trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle parametrizado por respuesta (VARCHAR 120); se muestra visualmente solo si está marcado en configuración, permitiendo diligenciamiento adicional.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle registrado por cada respuesta, este campo se parametriza desde la creación de las respuestas, si este campo es marcado, se muestra visualmente para que sea diligenciado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado general calculado de la pregunta: valor neto si selección única/texto libre, o regla de tres para múltiple opción considerando valores de opciones elegidas (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado general de la pregunta teniendo en cuenta la respuesta seleccionada. en caso de ser seleccion unica o texto libre, tomara el valor neto de la pregunta, si es de seleccion multiple se hara regla de 3 con los valores de las opciones elegidas. ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (DECIMAL 18,2) de la pregunta según parametrización en maestro de preguntas; base para cálculo de resultado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para almacenar, el valor de cada pregunta, segun lo parametrizado en el maestro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto libre o respuesta escrita por el usuario evaluado; almacena contenido de selección única, múltiple o respuesta abierta (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto o respuesta escrita por usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la pregunta seleccionada o respondida por el encargado del proceso de selección, vinculada a tabla Questions.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las preguntas seleccionadas o respondidas por el encargado del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera (FK) que referencia la evaluación o formulario de respuestas plantilla completa en FormsTemplateAnswers.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'FormsTemplateAnswersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'FormsTemplateAnswersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'FormsTemplateAnswersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de respuesta a pregunta en formulario de evaluación de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuestas detalladas por pregunta de las evaluaciones de talento humano. Registra cada respuesta individual dada a una pregunta dentro de un formulario o evaluación de personal, incluyendo el valor asignado, el resultado obtenido y observaciones adicionales.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormsTemplateAnswerQuestions';
