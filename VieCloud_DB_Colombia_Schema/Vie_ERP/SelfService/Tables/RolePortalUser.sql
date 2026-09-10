CREATE TABLE [SelfService].[RolePortalUser] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [PortalUserId] INT NOT NULL,
    [RoleId]       INT NOT NULL,
    CONSTRAINT [PK_RolePortalUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolePortalUser_PortalUser] FOREIGN KEY ([PortalUserId]) REFERENCES [SelfService].[PortalUser] ([Id]),
    CONSTRAINT [FK_RolePortalUser_Role] FOREIGN KEY ([RoleId]) REFERENCES [SelfService].[Role] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre usuarios del portal de autoservicio y los roles que tienen asignados. Permite controlar qué permisos y accesos tiene cada usuario dentro del portal.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de asignación de rol al usuario.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario del portal de autoservicio al que se le asigna el rol.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser', @level2type = N'COLUMN', @level2name = N'PortalUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser', @level2type = N'COLUMN', @level2name = N'PortalUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol asignado al usuario, define sus permisos y nivel de acceso en el portal.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser', @level2type = N'COLUMN', @level2name = N'RoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'RolePortalUser', @level2type = N'COLUMN', @level2name = N'RoleId';
