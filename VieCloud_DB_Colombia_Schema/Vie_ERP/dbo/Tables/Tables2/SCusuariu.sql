CREATE TABLE [dbo].[SCusuariu] (
    [codusuari] CHAR (3)  NOT NULL,
    [nomusuari] CHAR (40) NOT NULL,
    [perseguim] CHAR (1)  NOT NULL,
    [percuenta] CHAR (1)  NOT NULL,
    [perarmado] CHAR (1)  NOT NULL,
    [peraudito] CHAR (1)  NOT NULL,
    [perradica] CHAR (1)  NOT NULL,
    [perjefefa] CHAR (1)  NOT NULL,
    [peradmini] CHAR (1)  NOT NULL,
    CONSTRAINT [PK_Seusuarios] PRIMARY KEY CLUSTERED ([codusuari] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil administrador del sistema, autoriza configuración global, gestión de usuarios, parámetros y acceso administrativo completo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peradmini';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Administrador del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peradmini';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peradmini';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil jefe de facturación, autoriza supervisión, aprobación y control de procesos de facturación y recaudos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perjefefa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Jefe de facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perjefefa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perjefefa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil radicación de cuentas, autoriza presentación formal de cuentas y facturas ante entidades pagadoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perradica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Radicacion de Cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perradica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perradica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil auditoría, autoriza revisión de RIPS, glosas, cumplimiento normativo y auditoria interna de procesos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peraudito';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peraudito';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peraudito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil armado de cuentas, autoriza generación, consolidación y preparación de cuentas para radicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perarmado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Armado de Cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perarmado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perarmado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil analista de cuentas, autoriza revisión, análisis y validación de cuentas médicas y facturación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'percuenta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Analista de Cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'percuenta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'percuenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil de salida y seguimiento, autoriza gestión de seguimiento post-atención y egreso de pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perseguim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Salida y Seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perseguim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perseguim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario, profesional de salud o administrativo registrado en el ERP/EHR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario, identificador único de 3 caracteres, clave primaria para autenticación y permisos en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios del sistema con sus permisos y roles de acceso. Registra cada operador habilitado y qué funciones puede realizar: seguimiento, cuentas, armado de documentos, auditoría, radicación, jefatura y administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu';
