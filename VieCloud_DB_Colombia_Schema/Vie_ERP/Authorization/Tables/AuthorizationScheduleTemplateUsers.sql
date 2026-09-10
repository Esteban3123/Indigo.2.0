CREATE TABLE [Authorization].[AuthorizationScheduleTemplateUsers] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleTemplateId] INT          NOT NULL,
    [UserId]                          INT          NOT NULL,
    [UserCode]                        VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleTemplateUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleTemplateUsers_AuthorizationScheduleTemplate] FOREIGN KEY ([AuthorizationScheduleTemplateId]) REFERENCES [Authorization].[AuthorizationScheduleTemplate] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de usuario de seguridad (VARCHAR 50), identificador alfanumérico del profesional o administrador autorizado para acceder al sistema.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno (INT) del usuario de seguridad que posee permisos, vinculado a la cuenta de acceso del profesional de la salud o administrador.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la plantilla maestra de autorización de horarios, clave foránea que referencia la cabecera de configuración de permisos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de asignación de autorización de plantilla a usuario, clave primaria de la relación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de usuarios asignados a cada plantilla de agenda de autorización. Indica qué usuarios tienen acceso o están vinculados a una plantilla específica de programación de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateUsers';
