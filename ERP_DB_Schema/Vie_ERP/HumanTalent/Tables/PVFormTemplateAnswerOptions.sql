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
ALTER TABLE [HumanTalent].[PVFormTemplateAnswerOptions] NOCHECK CONSTRAINT [FK_PVFormTemplateAnswerOptions_PVAnswerOption1];


GO
ALTER TABLE [HumanTalent].[PVFormTemplateAnswerOptions] NOCHECK CONSTRAINT [FK_PVFormTemplateAnswerOptions_PVFormsTemplateAnswerQuestions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor de cada opcion ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de las opciones que cada respuesta, seleccionada por la persona encargada del proceso de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVAnswerOptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'PVFormsTemplateAnswerQuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVFormTemplateAnswerOptions', @level2type = N'COLUMN', @level2name = N'Id';

