CREATE TABLE [AccountManagement].[ManagementAreasUser] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [ManagementAreasId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [Usercode]          VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ManagementAreasUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManagementAreasUser_ManagementAreas] FOREIGN KEY ([ManagementAreasId]) REFERENCES [AccountManagement].[ManagementAreas] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (VARCHAR 20), identificador de login o credencial del personal de salud asignado al área de gestión', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Usercode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Usercode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Usercode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario en el sistema, clave de referencia al perfil del profesional o administrador', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del área de gestión (FK a ManagementAreas), vincula usuario a unidad funcional, centro de atención o departamento', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'ManagementAreasId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la area de gestion', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'ManagementAreasId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'ManagementAreasId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT Identity), identificador único de la relación usuario-área de gestión en la tabla de asignaciones', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los usuarios del sistema con las áreas de gestión a las que pertenecen, controlando qué área de gestión tiene asignado cada usuario.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser';
