CREATE TABLE [dbo].[INusuariu] (
    [codusuari] CHAR (3)  NOT NULL,
    [nomusuari] CHAR (40) NOT NULL,
    [passusuar] CHAR (21) NOT NULL,
    [igrunivel] CHAR (3)  NOT NULL,
    [codusudgh] CHAR (3)  NOT NULL,
    [usuactivo] INT       NOT NULL,
    CONSTRAINT [PK_INusuariu__codusuari] PRIMARY KEY CLUSTERED ([codusuari] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado del usuario: 1=Activo/habilitado, 0=Inactivo/deshabilitado para login y operaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'usuactivo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Activo 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'usuactivo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'usuactivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de referencia del usuario en DGH (Dirección General de Hospitales), vínculo con estructura organizacional externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusudgh';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusudgh';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusudgh';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de nivel o perfil de permisos del usuario (rol: administrativo, médico, enfermería, facturación, RIPS, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'igrunivel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'igrunivel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'igrunivel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave/contraseña encriptada del usuario (CHAR 21), credencial para autenticación en Indigo Vie Cloud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'passusuar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'passusuar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'passusuar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo o razón social del usuario, profesional de salud, administrativo o staff del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (PK), identificador de 3 caracteres para autenticación y acceso al sistema ERP/EHR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios del sistema con sus credenciales de acceso, nivel de permisos y estado activo. Permite controlar quién puede ingresar al ERP/EHR y con qué privilegios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu';
