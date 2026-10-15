CREATE TABLE [HumanTalent].[PVHiringFormsTemplateQuestions] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HiringFormsTemplateId] INT          NOT NULL,
    [QuestionsId]           INT          NOT NULL,
    [Score]                 INT          NOT NULL,
    [Head]                  INT          NOT NULL,
    [Obligatory]            TINYINT      NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK_PVHiringFormsTemplateQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVHiringFormsTemplateQuestions_HiringFormsTemplate] FOREIGN KEY ([HiringFormsTemplateId]) REFERENCES [HumanTalent].[HiringFormsTemplate] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVHiringFormsTemplateQuestions] NOCHECK CONSTRAINT [FK_PVHiringFormsTemplateQuestions_HiringFormsTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que realiza el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para saber si la pregunta es obligatoria o no : 0- No obligatoria, 1- obligatoria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden en que se listaran o mostraran las preguntas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'Head';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntuacion o valor de cada pregunta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las preguntas parametrizadas por el usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'QuestionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'HiringFormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVHiringFormsTemplateQuestions', @level2type = N'COLUMN', @level2name = N'Id';

