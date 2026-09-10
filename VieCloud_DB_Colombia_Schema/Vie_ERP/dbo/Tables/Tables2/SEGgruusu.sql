CREATE TABLE [dbo].[SEGgruusu] (
    [codgrupou] CHAR (3)  NOT NULL,
    [descrigru] CHAR (60) NOT NULL,
    CONSTRAINT [PK_SEGgrupus] PRIMARY KEY CLUSTERED ([codgrupou] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del grupo de usuarios. Texto que identifica el propósito, función o rol del grupo (ej: Administrativo, Médicos, Enfermería, Facturación). Tipo: CHAR(60). Utilizado para búsquedas por nombre o categoría de grupo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'descrigru';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'descrigru';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'descrigru';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del grupo de usuarios. Identificador alfanumérico de 3 caracteres (CHAR(3)) que clasifica grupos de roles y permisos en el sistema. Clave primaria. Empleado en asignación de perfiles de acceso y autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'codgrupou';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo de Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'codgrupou';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'codgrupou';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos de usuarios del sistema de seguridad. Permite clasificar y organizar los usuarios por roles o perfiles de acceso dentro de la aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu';
