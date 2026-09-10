CREATE TABLE [HumanTalent].[PVHiringFormsTemplateAnswerQuestions] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HiringFormsTemplateAnswersId] INT             NOT NULL,
    [HiringQuestionsId]            INT             NULL,
    [AnswerDescription]            VARCHAR (200)   NULL,
    [QuestionsValue]               DECIMAL (18, 2) NULL,
    [Result]                       DECIMAL (18, 2) NOT NULL,
    [Detail]                       VARCHAR (120)   NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    CONSTRAINT [PK_PVHiringFormsTemplateAnswerQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVHiringFormsTemplateAnswerQuestions_HiringFormsTemplateAnswers] FOREIGN KEY ([HiringFormsTemplateAnswersId]) REFERENCES [HumanTalent].[HiringFormsTemplateAnswers] ([Id]),
    CONSTRAINT [FK_PVHiringFormsTemplateAnswerQuestions_HiringQuestions] FOREIGN KEY ([HiringQuestionsId]) REFERENCES [HumanTalent].[HiringQuestions] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de respuesta a pregunta de selección; auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login (VARCHAR 20) del usuario que realizó la última modificación; trazabilidad de cambios en respuestas.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro; marca temporal de ingreso de respuesta a pregunta.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login (VARCHAR 20) del usuario que creó el registro; quién diligencia o registra la respuesta.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle adicional parametrizable (VARCHAR 120) para cada respuesta; campo opcional visible si se habilita en configuración, para recopilar información complementaria.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle registrado por cada respuesta, este campo se parametriza desde la creación de las respuestas, si este campo es marcado, se muestra visualmente para que sea diligenciado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (DECIMAL 18,2) resultante de la pregunta según respuesta: valor neto en selección única/texto libre, o regla de tres ponderada en selección múltiple.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado general de la pregunta teniendo en cuenta la respuesta seleccionada. en caso de ser seleccion unica o texto libre, tomara el valor neto de la pregunta, si es de seleccion multiple se hara regla de 3 con los valores de las opciones elegidas. ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (DECIMAL 18,2) parametrizado de la pregunta según maestro de configuración; base para cálculo de resultado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para almacenar, el valor de cada pregunta, segun lo parametrizado en el maestro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto o respuesta libre (VARCHAR 200) registrada por el usuario encargado del proceso de selección; contenido escrito de la respuesta.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto o respuesta escrita por usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la pregunta seleccionada o respondida; referencia a HiringQuestions.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringQuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las preguntas seleccionadas o respondidas por el encargado del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringQuestionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringQuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de cabecera/formulario plantilla de respuestas; referencia a HiringFormsTemplateAnswers.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringFormsTemplateAnswersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringFormsTemplateAnswersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringFormsTemplateAnswersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK, IDENTITY) del registro de respuesta a pregunta en proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuestas individuales a las preguntas de los formularios de contratación de talento humano. Registra el detalle de cada pregunta respondida dentro de un formulario de vinculación o selección de personal, incluyendo el valor asignado y el resultado obtenido.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions';
