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
ALTER TABLE [HumanTalent].[PVHiringFormsTemplateAnswerQuestions] NOCHECK CONSTRAINT [FK_PVHiringFormsTemplateAnswerQuestions_HiringFormsTemplateAnswers];


GO
ALTER TABLE [HumanTalent].[PVHiringFormsTemplateAnswerQuestions] NOCHECK CONSTRAINT [FK_PVHiringFormsTemplateAnswerQuestions_HiringQuestions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle registrado por cada respuesta, este campo se parametriza desde la creación de las respuestas, si este campo es marcado, se muestra visualmente para que sea diligenciado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado general de la pregunta teniendo en cuenta la respuesta seleccionada. en caso de ser seleccion unica o texto libre, tomara el valor neto de la pregunta, si es de seleccion multiple se hara regla de 3 con los valores de las opciones elegidas. ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para almacenar, el valor de cada pregunta, segun lo parametrizado en el maestro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto o respuesta escrita por usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'AnswerDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las preguntas seleccionadas o respondidas por el encargado del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringQuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'HiringFormsTemplateAnswersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateAnswerQuestions', @level2type = N'COLUMN', @level2name = N'Id';

