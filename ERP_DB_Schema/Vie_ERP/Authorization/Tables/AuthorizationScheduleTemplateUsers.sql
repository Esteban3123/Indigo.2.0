CREATE TABLE [Authorization].[AuthorizationScheduleTemplateUsers] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleTemplateId] INT          NOT NULL,
    [UserId]                          INT          NOT NULL,
    [UserCode]                        VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleTemplateUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleTemplateUsers_AuthorizationScheduleTemplate] FOREIGN KEY ([AuthorizationScheduleTemplateId]) REFERENCES [Authorization].[AuthorizationScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationScheduleTemplateUsers] NOCHECK CONSTRAINT [FK_AuthorizationScheduleTemplateUsers_AuthorizationScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'Id';

