CREATE TABLE [dbo].[SEGrolesu] (
    [codigorol] CHAR (4)  NOT NULL,
    [descrirol] CHAR (60) NOT NULL,
    CONSTRAINT [PK_SEGrolesu] PRIMARY KEY CLUSTERED ([codigorol] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Rol, nombre o denominación del perfil de acceso que define responsabilidades y permisos del profesional de salud o personal administrativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'descrirol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'descrirol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'descrirol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Rol (PK), identificador único de 4 caracteres que clasifica permisos y funciones de usuario en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'codigorol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'codigorol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'codigorol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de roles de seguridad del sistema. Define los perfiles de acceso que se pueden asignar a los usuarios, como administrador, médico, enfermería, facturación, entre otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu';
