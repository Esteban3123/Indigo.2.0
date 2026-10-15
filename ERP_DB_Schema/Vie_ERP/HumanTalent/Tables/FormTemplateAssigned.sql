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
ALTER TABLE [HumanTalent].[FormTemplateAssigned] NOCHECK CONSTRAINT [FK_FormTemplateAssigned_CandidatesSelectionProcess];


GO
ALTER TABLE [HumanTalent].[FormTemplateAssigned] NOCHECK CONSTRAINT [FK_FormTemplateAssigned_FormsTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del empleado al que se le trasfiere o asgina realizar dicha actividad o ejecutar dicha plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Assigned';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la pantilla asignada para que el empleado delegado, le haga la prueba al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para almacenar el id del del proceso en que se encuentra vinsulado el candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'CandiadateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormTemplateAssigned', @level2type = N'COLUMN', @level2name = N'Id';

