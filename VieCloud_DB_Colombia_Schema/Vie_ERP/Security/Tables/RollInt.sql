CREATE TABLE [Security].[RollInt] (
    [Id]          INT          NOT NULL,
    [RollCode]    CHAR (10)    NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp]   ROWVERSION   NOT NULL,
    [RollType]    TINYINT      NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de roles de seguridad del sistema. Define los perfiles de acceso y permisos que se asignan a los usuarios dentro de la plataforma Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único del rol.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código abreviado del rol, usado internamente para identificarlo en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'RollCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'RollCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción legible del rol, por ejemplo: Administrador, Médico, Facturador.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática que registra la última modificación del registro del rol.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o categoría del rol, permite clasificar roles por nivel o área funcional (por ejemplo: administrativo, clínico, sistema).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'RollType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'RollInt', @level2type = N'COLUMN', @level2name = N'RollType';
