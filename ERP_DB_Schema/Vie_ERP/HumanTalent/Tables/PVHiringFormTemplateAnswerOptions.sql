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
ALTER TABLE [HumanTalent].[PVHiringFormTemplateAnswerOptions] NOCHECK CONSTRAINT [FK_PVHiringFormTemplateAnswerOptions_Pv_HiringsAnswers_Options];


GO
ALTER TABLE [HumanTalent].[PVHiringFormTemplateAnswerOptions] NOCHECK CONSTRAINT [FK_PVHiringFormTemplateAnswerOptions_PVHiringFormsTemplateAnswerQuestions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor de cada opcion ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de las opciones que cada respuesta, seleccionada por la persona encargada del proceso de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringAnswerOptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVHiringFormsTemplateAnswerQuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';

