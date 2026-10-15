CREATE TABLE [HumanTalent].[SelectionActivities] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [Name]                         VARCHAR (80)    NOT NULL,
    [Citation]                     TINYINT         NULL,
    [TemplateEmailNotificationsId] INT             NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [State]                        BIT             NULL,
    [Weight]                       DECIMAL (18, 2) CONSTRAINT [DF__Selection__Weigh__6CF3921B] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SelectionActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SelectionActivities_TemplateEmailNotifications] FOREIGN KEY ([TemplateEmailNotificationsId]) REFERENCES [HumanTalent].[TemplateEmailNotifications] ([Id])
);


GO
ALTER TABLE [HumanTalent].[SelectionActivities] NOCHECK CONSTRAINT [FK_SelectionActivities_TemplateEmailNotifications];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que realiza el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla que se utilizara la enviar la notificación al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'TemplateEmailNotificationsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Citación: 0: - No es requerico notificar por Email. 1- Se debe notificar por Email.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'Citation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la actividad, tipo alfanumérico ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionActivities', @level2type = N'COLUMN', @level2name = N'Id';

