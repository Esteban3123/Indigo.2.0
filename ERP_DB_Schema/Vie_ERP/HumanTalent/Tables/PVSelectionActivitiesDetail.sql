CREATE TABLE [HumanTalent].[PVSelectionActivitiesDetail] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SelectionActivitiesId] INT          NOT NULL,
    [FormsTemplateId]       INT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK_PVSelectionActivitiesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVSelectionActivitiesDetail_FormsTemplate] FOREIGN KEY ([FormsTemplateId]) REFERENCES [HumanTalent].[FormsTemplate] ([Id]),
    CONSTRAINT [FK_PVSelectionActivitiesDetail_SelectionActivities] FOREIGN KEY ([SelectionActivitiesId]) REFERENCES [HumanTalent].[SelectionActivities] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVSelectionActivitiesDetail] NOCHECK CONSTRAINT [FK_PVSelectionActivitiesDetail_FormsTemplate];


GO
ALTER TABLE [HumanTalent].[PVSelectionActivitiesDetail] NOCHECK CONSTRAINT [FK_PVSelectionActivitiesDetail_SelectionActivities];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modifiación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario que modifica el registro ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de formulario o plantillas (Contienen preguntas y desde ahí inicia el proceso de preguntas y respuestas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'Id';

